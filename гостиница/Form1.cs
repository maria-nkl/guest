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
                    // Получаем информацию о номере
                    var roomInfo = db.GetRoomInfo(roomNumber);
                    if (roomInfo == null)
                    {
                        MessageBox.Show("Не удалось получить информацию о номере");
                        return;
                    }

                    var bookingInfo = db.GetCurrentBookingInfo(roomNumber);
                    List<string> activeServices = bookingInfo != null ?
                        db.GetBookingServices(bookingInfo.BookingId) :
                        new List<string>();

                    // Создаем форму с полной информацией
                    Form2 form2 = new Form2(
                        labelText: panelLabel.Text,
                        guestName: bookingInfo?.GuestName,
                        guestPhone: bookingInfo?.GuestPhone,
                        numberFloor: roomInfo.Floor.ToString(),
                        capacity: roomInfo.Capacity.ToString(),
                        category: roomInfo.Category,
                        startDate: bookingInfo?.StartDate.ToShortDateString(),
                        endDate: bookingInfo?.EndDate.ToShortDateString(),
                        activeServices: activeServices
                    );

                    form2.PanelColorChanged += (newColor) =>
                    {
                        clickedPanel.BackColor = newColor;
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
