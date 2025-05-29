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

    public partial class Form1 : Form
    {
        private readonly Database db;
        public Form1()
        {
            InitializeComponent();
            db = new Database("Host=localhost;Database=hotel;Username=postgres;Password=root");
            SubscribePanels();
        }

        private void SubscribePanels()
        {
            foreach (Control control in this.Controls)
            {
                if (control is Panel)
                {
                    control.Click += Panel_Click;
                    control.Cursor = Cursors.Hand;
                }
            }
        }

        private void Panel_Click(object sender, EventArgs e)
        {
            Panel clickedPanel = sender as Panel;

            if (clickedPanel != null)
            {
                Label panelLabel = clickedPanel.Controls.OfType<Label>().FirstOrDefault();

                if (panelLabel != null && int.TryParse(panelLabel.Text, out int roomNumber))
                {

                    // Получаем информацию о госте
                    string guestInfo = db.GetGuestByRoomNumber(roomNumber);
                    string phoneNumber = db.GetGuestNumberByRoomNumber(roomNumber);
                    string floorNumber = db.GetFloorNumberByRoomNumber(roomNumber);

                    // Формируем строки для отображения
                    string displayName = guestInfo ?? "Номер свободен";
                    string displayPhone = !string.IsNullOrEmpty(phoneNumber) ? phoneNumber : "-";
                    string displayFloor = floorNumber;

                    // Создаем форму и передаем данные
                    Form2 form2 = new Form2(
                        labelText: panelLabel.Text,
                        guestName: displayName,
                        guestPhone: displayPhone,
                        numberFloor: displayFloor
                    );

                    // Подписываемся на событие изменения цвета
                    form2.PanelColorChanged += (newColor) =>
                    {
                        clickedPanel.BackColor = newColor; // Меняем цвет панели
                    };


                    form2.ShowDialog();
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Создаем экземпляр Form3
            Form3 form3 = new Form3();

            // Отображаем Form3 как модальное окно (блокирует родительскую форму)
            form3.ShowDialog();

            // Или, если хотите показать немодальное окно (можно переключаться между формами):
            // form3.Show();
        }
    }
}
