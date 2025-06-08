using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace гостиница
{
    public partial class Form2 : Form
    {

        public event Action<Color> PanelColorChanged;

        // Событие, которое срабатывает при нажатии кнопки
        public Form2(string labelText, string guestName, string guestPhone, string numberFloor,
            string capacity, string category, string startDate, string endDate,
            List<string> activeServices)
        {
            InitializeComponent();

            // Убедитесь, что все элементы инициализированы
            if (number == null || floor == null || kolvo_mest == null || room_category == null ||
                FIO == null || phone == null || data_start == null || data_end == null ||
                checkedListBoxServices == null)
            {
                MessageBox.Show("Ошибка инициализации элементов формы");
                return;
            }

            // Основная информация
            number.Text = labelText ?? "Нет данных";
            floor.Text = numberFloor ?? "Нет данных";
            FIO.Text = guestName ?? "Номер свободен";
            phone.Text = guestPhone ?? "-";

            // Дополнительная информация о номере
            kolvo_mest.Text = capacity ?? "Нет данных";
            room_category.Text = category ?? "Нет данных";
            data_start.Text = startDate ?? "-";
            data_end.Text = endDate ?? "-";

            // Заполняем услуги
            checkedListBoxServices.Items.Clear();
            if (activeServices != null)
            {
                checkedListBoxServices.Items.Add("Завтрак", activeServices.Contains("Завтрак"));
                checkedListBoxServices.Items.Add("Ужин", activeServices.Contains("Ужин"));
                checkedListBoxServices.Items.Add("Уборка", activeServices.Contains("Уборка"));
                checkedListBoxServices.Items.Add("Развлечения", activeServices.Contains("Развлечения"));
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            if (!int.TryParse(number.Text, out int roomNumber))
                return;

            using (var connection = new Npgsql.NpgsqlConnection("Host=46.160.139.91;Port=5432;Database=hotel;Username=postgres123;Password=root"))
            {
                connection.Open();
                string query = "SELECT request_details FROM rooms WHERE room_number = @roomNumber";

                using (var cmd = new Npgsql.NpgsqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@roomNumber", roomNumber);
                    object result = cmd.ExecuteScalar();

                    if (result != null && !string.IsNullOrWhiteSpace(result.ToString()))
                    {
                        // Задание уже есть
                        Text_task.Text = result.ToString();
                        Text_task.Enabled = false;
                        task.Enabled = false;
                        completed.Enabled = true;

                        // Сообщаем в Form1, что нужна красная подсветка
                        PanelColorChanged?.Invoke(Color.Red);
                    }
                    else
                    {
                        // Задания нет
                        Text_task.Text = "";
                        Text_task.Enabled = true;
                        task.Enabled = true;
                        completed.Enabled = false;

                        PanelColorChanged?.Invoke(SystemColors.ButtonShadow);
                    }
                }
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void task_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Text_task.Text))
            {
                MessageBox.Show("Введите текст задания.");
                return;
            }

            if (!int.TryParse(number.Text, out int roomNumber))
            {
                MessageBox.Show("Некорректный номер комнаты.");
                return;
            }

            try
            {
                using (var connection = new Npgsql.NpgsqlConnection("Host=46.160.139.91;Port=5432;Database=hotel;Username=postgres123;Password=root"))
                {
                    connection.Open();
                    string updateQuery = "UPDATE rooms SET request_details = @details WHERE room_number = @roomNumber";

                    using (var cmd = new Npgsql.NpgsqlCommand(updateQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@details", Text_task.Text);
                        cmd.Parameters.AddWithValue("@roomNumber", roomNumber);
                        cmd.ExecuteNonQuery();
                    }
                }

                completed.Enabled = true;
                task.Enabled = false;
                Text_task.Enabled = false;
                PanelColorChanged?.Invoke(Color.Red);
                MessageBox.Show("Задание сохранено.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении задания: " + ex.Message);
            }

        }

        private void completed_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(number.Text, out int roomNumber))
            {
                MessageBox.Show("Некорректный номер комнаты.");
                return;
            }

            try
            {
                using (var connection = new Npgsql.NpgsqlConnection("Host=46.160.139.91;Port=5432;Database=hotel;Username=postgres123;Password=root"))
                {
                    connection.Open();
                    string clearQuery = "UPDATE rooms SET request_details = NULL WHERE room_number = @roomNumber";

                    using (var cmd = new Npgsql.NpgsqlCommand(clearQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@roomNumber", roomNumber);
                        cmd.ExecuteNonQuery();
                    }
                }

                completed.Enabled = false;
                task.Enabled = true;
                Text_task.Enabled = true;
                Text_task.Text = string.Empty;
                PanelColorChanged?.Invoke(SystemColors.ButtonShadow);
                MessageBox.Show("Задание завершено.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при удалении задания: " + ex.Message);
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
