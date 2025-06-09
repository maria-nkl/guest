using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace гостиница
{
    public partial class Form7 : Form
    {
        private static readonly HttpClient client = new HttpClient();
        private const string apiBaseUrl = "https://localhost:7029/api"; // замените на ваш адрес API
        private Dictionary<int, string> serviceNames = new Dictionary<int, string>();


        public Form7()
        {
            InitializeComponent();
            LoadServices();
            LoadAndDisplayBookings();
        }

        private async void Form7_Load(object sender, EventArgs e)
        {
            await LoadAndDisplayBookings();
        }
        private async Task LoadServices()
        {
            try
            {
                var response = await client.GetAsync($"{apiBaseUrl}/services");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var services = JsonSerializer.Deserialize<List<ServiceDto>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                serviceNames = services.ToDictionary(s => s.id, s => s.serviceName);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке списка услуг: {ex.Message}");
            }
        }

        private async Task LoadAndDisplayBookings()
        {
            try
            {
                var response = await client.GetAsync($"{apiBaseUrl}/bookings");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var bookings = JsonSerializer.Deserialize<List<BookingDto>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                DisplayBookings(bookings);
                UpdateStatistics(bookings);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке бронирований: {ex.Message}");
            }
        }

        private void DisplayBookings(List<BookingDto> bookings)
        {
            var table = new DataTable();
            table.Columns.Add("Номер комнаты");
            table.Columns.Add("Дата начала");
            table.Columns.Add("Дата окончания");
            table.Columns.Add("Гость");
            table.Columns.Add("Услуги");
            table.Columns.Add("Итоговая цена");

            foreach (var booking in bookings)
            {
                var serviceNamesList = booking.Services?
                    .Select(id => serviceNames.TryGetValue(id, out var name) ? name : $"ID:{id}")
                    .ToList();

                table.Rows.Add(
                    booking.RoomNumber,
                    booking.StartDate.ToShortDateString(),
                    booking.EndDate.ToShortDateString(),
                    booking.GuestName,
                    string.Join(", ", serviceNamesList ?? new List<string>()),
                    booking.TotalPrice
                );
            }

            dataGridViewBookings.DataSource = table;
            dataGridViewBookings.Columns["Услуги"].Width = 200;
        }

        private void UpdateStatistics(List<BookingDto> bookings)
        {
            int total = bookings.Count;
            int current = 0;
            int upcoming = 0;
            int past = 0; // добавляем счётчик прошедших бронирований
            DateTime today = DateTime.Today;

            foreach (var booking in bookings)
            {
                if (today >= booking.StartDate && today <= booking.EndDate)
                    current++;
                else if (booking.StartDate > today)
                    upcoming++;
                else if (booking.EndDate < today)
                    past++;
            }

            labelStatus.Text = $"📋 Всего бронирований: {total}\n" +
                               $"🟢 Текущих: {current}\n" +
                               $"🕓 Будущих: {upcoming}\n" +
                               $"✔️ Прошедших: {past}";
        }

        public class BookingDto
        {
            public int RoomNumber { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public string GuestName { get; set; }
            public List<int> Services { get; set; }
            public decimal TotalPrice { get; set; }
        }
        public class ServiceDto
        {
            public int id { get; set; }
            public string serviceName { get; set; }
        }

        private void labelStatus_Click(object sender, EventArgs e)
        {

        }
    }
}
