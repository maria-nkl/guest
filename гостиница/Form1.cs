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
            db = new Database("Host=46.160.139.91;Port=5432;Database=hotel;Username=postgres123;Password=root");
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
            labelUserInfo.Text = $"Пользователь: {SessionUser.FullName}\nРоль: {SessionUser.RoleName}";
            HighlightPanelsWithRequests();

            // 1. Обслуживающий персонал не может нажимать на кнопку1
            if (SessionUser.RoleName == "Обслуживающий персонал")
            {
                button1.Enabled = false;
            }

            // 2. Только администратор может нажимать на кнопку2
            if (SessionUser.RoleName != "Администратор")
            {
                button2.Enabled = false;
            }
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

        private void HighlightPanelsWithRequests()
        {
            using (var connection = new Npgsql.NpgsqlConnection("Host=46.160.139.91;Port=5432;Database=hotel;Username=postgres123;Password=root"))
            {
                connection.Open();

                foreach (Control control in this.Controls)
                {
                    if (control is Panel panel)
                    {
                        // Предполагаем, что Label внутри панели содержит номер комнаты
                        Label label = panel.Controls.OfType<Label>().FirstOrDefault();
                        if (label != null && int.TryParse(label.Text, out int roomNumber))
                        {
                            string query = "SELECT request_details FROM rooms WHERE room_number = @roomNumber";
                            using (var cmd = new Npgsql.NpgsqlCommand(query, connection))
                            {
                                cmd.Parameters.AddWithValue("@roomNumber", roomNumber);
                                object result = cmd.ExecuteScalar();

                                if (result != null && !string.IsNullOrWhiteSpace(result.ToString()))
                                {
                                    panel.BackColor = Color.Red;
                                }
                                else
                                {
                                    panel.BackColor = SystemColors.ButtonShadow; ; // или другой стандартный цвет
                                }
                            }
                        }
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form7 form7 = new Form7();
            form7.ShowDialog();
        }
    }
}
