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
        // Событие, которое срабатывает при нажатии кнопки
        public event Action<Color> PanelColorChanged;


        public Form2(string labelText, string guestName, string guestPhone, string numberFloor)
        {
            InitializeComponent();
            number.Text = labelText;
            floor.Text = numberFloor;
            FIO.Text = guestName;
            phone.Text = guestPhone;
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

            Color newColor = Color.Red;

            // Вызываем событие и передаем новый цвет
            PanelColorChanged?.Invoke(newColor);

        }

        private void completed_Click(object sender, EventArgs e)
        {
            completed.Enabled = false;
            task.Enabled = true;
            Text_task.Enabled=true;
            Text_task.Text=string.Empty;
            Color newColor = SystemColors.ButtonShadow;

            // Вызываем событие и передаем новый цвет
            PanelColorChanged?.Invoke(newColor);
        }
    }
}
