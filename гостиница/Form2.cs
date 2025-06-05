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

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void task_Click(object sender, EventArgs e)
        {
            completed.Enabled = true;
            task.Enabled = false;
            Text_task.Enabled = false;
            PanelColorChanged?.Invoke(Color.Red);

        }

        private void completed_Click(object sender, EventArgs e)
        {
            completed.Enabled = false;
            task.Enabled = true;
            Text_task.Enabled = true;
            Text_task.Text = string.Empty;
            PanelColorChanged?.Invoke(SystemColors.ButtonShadow);
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
