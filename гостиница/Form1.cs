using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace гостиница
{
    public partial class Form1 : Form
    {
        private static readonly HttpClient client = new HttpClient();
        private const string apiBaseUrl = "https://localhost:7029/api";

        public Form1()
        {
            InitializeComponent();
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

        private async void Panel_Click(object sender, EventArgs e)
        {
            Panel clickedPanel = sender as Panel;

            if (clickedPanel != null)
            {
                Label panelLabel = clickedPanel.Controls.OfType<Label>().FirstOrDefault();

                if (panelLabel != null && int.TryParse(panelLabel.Text, out int roomNumber))
                {
                    var roomInfo = await GetRoomInfoAsync(roomNumber);
                    if (roomInfo == null)
                    {
                        MessageBox.Show("Не удалось получить информацию о номере");
                        return;
                    }

                    var bookingInfo = await GetCurrentBookingInfoAsync(roomNumber);
                    List<string> activeServices = bookingInfo != null ?
                        await GetBookingServicesAsync(bookingInfo.BookingId) :
                        new List<string>();

                    Form2 form2 = new Form2(
                        labelText: panelLabel.Text,
                        guestName: bookingInfo?.GuestName,
                        guestPhone: bookingInfo?.GuestPhone,
                        numberFloor: roomInfo.Floor.ToString(),
                        capacity: roomInfo.Capacity.ToString(),
                        category: roomInfo.Category,
                        startDate: bookingInfo?.StartDate.ToShortDateString(),
                        endDate: bookingInfo?.EndDate.ToShortDateString(),
                        activeServices: activeServices,
                        getRequestDetailsAsync: GetRequestDetailsFromApiAsync,
                        updateRequestDetailsAsync: UpdateRequestDetailsApiAsync,
                        clearRequestDetailsAsync: ClearRequestDetailsApiAsync
                    );


                    form2.PanelColorChanged += (newColor) =>
                    {
                        clickedPanel.BackColor = newColor;
                    };

                    form2.ShowDialog();
                }
            }
        }

        private async Task<RoomInfo> GetRoomInfoAsync(int roomNumber)
        {
            var response = await client.GetAsync($"{apiBaseUrl}/Rooms/{roomNumber}/info");
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<RoomInfo>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            return null;
        }

        private async Task<BookingInfo> GetCurrentBookingInfoAsync(int roomNumber)
        {
            var response = await client.GetAsync($"{apiBaseUrl}/Rooms/{roomNumber}/current-booking");
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<BookingInfo>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            return null;
        }

        private async Task<List<string>> GetBookingServicesAsync(int bookingId)
        {
            var response = await client.GetAsync($"{apiBaseUrl}/Bookings/{bookingId}/services");
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<string>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            return new List<string>();
        }

        private async Task HighlightPanelsWithRequestsAsync()
        {
            var response = await client.GetAsync($"{apiBaseUrl}/Rooms/with-requests");
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();

                var roomsWithRequests = JsonSerializer.Deserialize<List<RoomWithRequest>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                // Создаем HashSet для быстрого поиска
                var roomsSet = new HashSet<int>(roomsWithRequests.Select(r => r.RoomNumber));

                foreach (Control control in this.Controls)
                {
                    if (control is Panel panel)
                    {
                        Label label = panel.Controls.OfType<Label>().FirstOrDefault();
                        if (label != null && int.TryParse(label.Text, out int roomNumber))
                        {
                            if (roomsSet.Contains(roomNumber))
                            {
                                panel.BackColor = Color.Red;
                            }
                            else
                            {
                                panel.BackColor = SystemColors.ButtonShadow;
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Не удалось загрузить список номеров с заявками");
            }
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            labelUserInfo.Text = $"Пользователь: {SessionUser.fullName}\nРоль: {SessionUser.roleName}";
            await HighlightPanelsWithRequestsAsync();

            if (SessionUser.roleName == "Обслуживающий персонал")
            {
                button1.Enabled = false;
            }

            if (SessionUser.roleName != "Администратор")
            {
                button2.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form7 form7 = new Form7();
            form7.ShowDialog();
        }

        private async Task<string> GetRequestDetailsFromApiAsync(int roomNumber)
        {
            var response = await client.GetAsync($"{apiBaseUrl}/Rooms/{roomNumber}");
            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();
            var room = JsonSerializer.Deserialize<RoomWithRequest>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return room?.RequestDetails;
        }

        private async Task<bool> UpdateRequestDetailsApiAsync(int roomNumber, string details)
        {
            var content = new StringContent(JsonSerializer.Serialize(details), Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"{apiBaseUrl}/Rooms/{roomNumber}/request", content);
            return response.IsSuccessStatusCode;
        }

        private async Task<bool> ClearRequestDetailsApiAsync(int roomNumber)
        {
            var response = await client.DeleteAsync($"{apiBaseUrl}/Rooms/{roomNumber}/request");
            return response.IsSuccessStatusCode;
        }


        private void panel5_Paint(object sender, PaintEventArgs e)
        {
            // Оставляем пустым, если не нужно
        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {
            // Оставляем пустым, если не нужно
        }
    }

    // Классы для десериализации JSON из API
    public class RoomInfo
    {
        public int Floor { get; set; }
        public int Capacity { get; set; }
        public string Category { get; set; }
    }

    public class BookingInfo
    {
        public int BookingId { get; set; }
        public string GuestName { get; set; }
        public string GuestPhone { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    // Новый класс для подсветки комнат с заявками
    public class RoomWithRequest
    {
        [JsonPropertyName("roomNumber")]
        public int RoomNumber { get; set; }

        [JsonPropertyName("requestDetails")]
        public string RequestDetails { get; set; }

    }



}
