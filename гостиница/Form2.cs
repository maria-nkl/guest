using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace гостиница
{
    public partial class Form2 : Form
    {

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        // Событие для изменения цвета панели (чтобы сообщать в Form1)
        public event Action<Color> PanelColorChanged;

        // Функции для взаимодействия с API, которые передадим из Form1
        private readonly Func<int, Task<string>> _getRequestDetailsAsync;
        private readonly Func<int, string, Task<bool>> _updateRequestDetailsAsync;
        private readonly Func<int, Task<bool>> _clearRequestDetailsAsync;

        private readonly int _roomNumber;

        public Form2(
            string labelText, string guestName, string guestPhone, string numberFloor,
            string capacity, string category, string startDate, string endDate,
            List<string> activeServices,
            Func<int, Task<string>> getRequestDetailsAsync,
            Func<int, string, Task<bool>> updateRequestDetailsAsync,
            Func<int, Task<bool>> clearRequestDetailsAsync
        )
        {
            InitializeComponent();

            // Сохраняем функции для вызова API
            _getRequestDetailsAsync = getRequestDetailsAsync ?? throw new ArgumentNullException(nameof(getRequestDetailsAsync));
            _updateRequestDetailsAsync = updateRequestDetailsAsync ?? throw new ArgumentNullException(nameof(updateRequestDetailsAsync));
            _clearRequestDetailsAsync = clearRequestDetailsAsync ?? throw new ArgumentNullException(nameof(clearRequestDetailsAsync));

            // Инициализация UI
            number.Text = labelText ?? "Нет данных";
            floor.Text = numberFloor ?? "Нет данных";
            FIO.Text = guestName ?? "Номер свободен";
            phone.Text = guestPhone ?? "-";
            kolvo_mest.Text = capacity ?? "Нет данных";
            room_category.Text = category ?? "Нет данных";
            data_start.Text = startDate ?? "-";
            data_end.Text = endDate ?? "-";

            checkedListBoxServices.Items.Clear();
            if (activeServices != null)
            {
                checkedListBoxServices.Items.Add("Завтрак", activeServices.Contains("Завтрак"));
                checkedListBoxServices.Items.Add("Ужин", activeServices.Contains("Ужин"));
                checkedListBoxServices.Items.Add("Уборка", activeServices.Contains("Уборка"));
                checkedListBoxServices.Items.Add("Развлечения", activeServices.Contains("Развлечения"));
            }

            if (!int.TryParse(labelText, out _roomNumber))
            {
                MessageBox.Show("Некорректный номер комнаты");
                this.Close();
            }

            // Асинхронно загрузим детали заявки
            LoadRequestDetailsAsync();
        }

        private async void LoadRequestDetailsAsync()
        {
            try
            {
                string requestDetails = await _getRequestDetailsAsync(_roomNumber);
                if (!string.IsNullOrEmpty(requestDetails))
                {
                    Text_task.Text = requestDetails;
                    Text_task.Enabled = false;
                    task.Enabled = false;
                    completed.Enabled = true;

                    PanelColorChanged?.Invoke(Color.Red);
                }
                else
                {
                    Text_task.Text = "";
                    Text_task.Enabled = true;
                    task.Enabled = true;
                    completed.Enabled = false;

                    PanelColorChanged?.Invoke(SystemColors.ButtonShadow);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки заявки: " + ex.Message);
            }
        }

        private async void task_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Text_task.Text))
            {
                MessageBox.Show("Введите текст задания.");
                return;
            }

            try
            {
                bool success = await _updateRequestDetailsAsync(_roomNumber, Text_task.Text);
                if (success)
                {
                    completed.Enabled = true;
                    task.Enabled = false;
                    Text_task.Enabled = false;
                    PanelColorChanged?.Invoke(Color.Red);
                    MessageBox.Show("Задание сохранено.");
                }
                else
                {
                    MessageBox.Show("Не удалось сохранить задание.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении задания: " + ex.Message);
            }
        }

        private async void completed_Click(object sender, EventArgs e)
        {
            try
            {
                bool success = await _clearRequestDetailsAsync(_roomNumber);
                if (success)
                {
                    completed.Enabled = false;
                    task.Enabled = true;
                    Text_task.Enabled = true;
                    Text_task.Text = string.Empty;
                    PanelColorChanged?.Invoke(SystemColors.ButtonShadow);
                    MessageBox.Show("Задание завершено.");
                }
                else
                {
                    MessageBox.Show("Не удалось удалить задание.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при удалении задания: " + ex.Message);
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
    }
}
