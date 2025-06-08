using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace гостиница
{
    public partial class Form3 : Form
    {
        private static readonly HttpClient client = new HttpClient();
        private const string apiBaseUrl = "https://localhost:7029/api"; // Замените на ваш URL API

        public Form3()
        {
            InitializeComponent();
            dataGridViewRoomsOrg.SelectionChanged += DataGridViewRoomsOrg_SelectionChanged;
        }

        private async void Form3_Load(object sender, EventArgs e)
        {
            ConfigureDataGridView();
            await LoadAllGuestsAsync();
            await LoadAllOrganizationsAsync();
            labelGuest.Text = "Выбранный гость: ";
            labelOrg.Text = "Выбранный гость: ";
        }

        private void ConfigureDataGridView()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.Columns.Clear();

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Id",
                HeaderText = "ID",
                DataPropertyName = "Id",
                Width = 50,
                Visible = false
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "FullNameOrOrganization",
                HeaderText = "ФИО",
                DataPropertyName = "FullNameOrOrganization",
                Width = 200
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Phone",
                HeaderText = "Телефон",
                DataPropertyName = "Phone",
                Width = 120
            });

            dataGridView1.SelectionChanged += DataGridView_SelectionChanged;

            dataGridView2.AutoGenerateColumns = false;
            dataGridView2.ReadOnly = true;
            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.MultiSelect = false;
            dataGridView2.Columns.Clear();

            dataGridView2.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Id",
                HeaderText = "ID",
                DataPropertyName = "Id",
                Width = 50,
                Visible = false
            });

            dataGridView2.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "FullNameOrOrganization",
                HeaderText = "Организация",
                DataPropertyName = "FullNameOrOrganization",
                Width = 200
            });

            dataGridView2.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Phone",
                HeaderText = "Телефон",
                DataPropertyName = "Phone",
                Width = 120
            });

            dataGridView2.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Discount",
                HeaderText = "Скидка",
                DataPropertyName = "Discount",
                Width = 80
            });

            dataGridView2.SelectionChanged += DataGridView_SelectionChanged;
        }

        private void DataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                string guestName = selectedRow.Cells["FullNameOrOrganization"].Value?.ToString() ?? "не указано";
                labelGuest.Text = $"Выбранный гость: {guestName}";
            }

            if (dataGridView2.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView2.SelectedRows[0];
                string guestName = selectedRow.Cells["FullNameOrOrganization"].Value?.ToString() ?? "не указано";
                labelOrg.Text = $"Выбранный гость: {guestName}";
            }
        }

        private async Task LoadAllGuestsAsync()
        {
            try
            {
                var response = await client.GetAsync($"{apiBaseUrl}/Guests?isOrganization=false");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    var guests = JsonSerializer.Deserialize<List<Guest>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    dataGridView1.DataSource = guests;
                    labelStatus.Text = $"Всего гостей: {guests.Count}";
                }
                else
                {
                    MessageBox.Show("Не удалось загрузить список гостей");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}");
            }
        }

        private async Task LoadAllOrganizationsAsync()
        {
            try
            {
                var response = await client.GetAsync($"{apiBaseUrl}/Organizations?isOrganization=true");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    var organizations = JsonSerializer.Deserialize<List<Guest>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    dataGridView2.DataSource = organizations;
                    labelStatusOrg.Text = $"Всего организаций: {organizations.Count}";
                }
                else
                {
                    MessageBox.Show("Не удалось загрузить список организаций");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}");
            }
        }

        private void new_guest_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            if (form4.ShowDialog() == DialogResult.OK)
            {
                LoadAllGuestsAsync();
            }
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await LoadAllGuestsAsync();
        }

        private async void buttonSearch_Click(object sender, EventArgs e)
        {
            string searchText = textBoxSearch.Text.Trim();

            if (string.IsNullOrWhiteSpace(searchText))
            {
                await LoadAllGuestsAsync();
                return;
            }

            try
            {
                var response = await client.GetAsync($"{apiBaseUrl}/Guests/search?query={searchText}&isOrganization=false");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    var guests = JsonSerializer.Deserialize<List<Guest>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    dataGridView1.DataSource = guests;
                    labelStatus.Text = $"Найдено гостей: {guests.Count}";
                }
                else
                {
                    MessageBox.Show("Гости не найдены");
                    await LoadAllGuestsAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка поиска: {ex.Message}");
                await LoadAllGuestsAsync();
            }
        }

        private void new_org_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            if (form5.ShowDialog() == DialogResult.OK)
            {
                LoadAllOrganizationsAsync();
            }
        }

        private async void SearchOrg_Click(object sender, EventArgs e)
        {
            string searchText = textBoxSearchOrg.Text.Trim();

            if (string.IsNullOrWhiteSpace(searchText))
            {
                await LoadAllOrganizationsAsync();
                return;
            }

            try
            {
                var response = await client.GetAsync($"{apiBaseUrl}/Organizations/search?query={searchText}&isOrganization=true");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    var organizations = JsonSerializer.Deserialize<List<Guest>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    dataGridView2.DataSource = organizations;
                    labelStatusOrg.Text = $"Найдено организаций: {organizations.Count}";
                }
                else
                {
                    MessageBox.Show("Организации не найдены");
                    await LoadAllOrganizationsAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка поиска: {ex.Message}");
                await LoadAllOrganizationsAsync();
            }
        }

        private async void searchRoomChast_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime startDate = monthCalendar1.SelectionStart;
                DateTime endDate = monthCalendar1.SelectionEnd;
                string selectedCategory = comboBoxCategory.SelectedItem?.ToString();
                int minCapacity = (int)kolvoGuests.Value;

                if (string.IsNullOrEmpty(selectedCategory))
                {
                    MessageBox.Show("Выберите категорию номера!");
                    return;
                }

                // Логируем параметры запроса
                Console.WriteLine($"Запрос: startDate={startDate}, endDate={endDate}, Category={selectedCategory}, Capacity={minCapacity}");

                using (var client = new HttpClient())
                {
                    string url = $"{apiBaseUrl}/Rooms/search?" +
                                $"startDate={startDate:yyyy-MM-dd}&" +
                                $"endDate={endDate:yyyy-MM-dd}&" +
                                $"category={Uri.EscapeDataString(selectedCategory)}&" +
                                $"minCapacity={minCapacity}";

                    Console.WriteLine($"URL запроса: {url}");

                    var response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"Ответ API: {json}");

                        var rooms = JsonSerializer.Deserialize<List<Room>>(json,
                            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                        dataGridViewRoomsChast.DataSource = rooms;

                        if (rooms.Count > 0)
                        {
                            dataGridViewRoomsChast.ClearSelection();
                            labelselectroom.Text = "Выбран номер: (не выбран)";
                        }
                        else
                        {
                            MessageBox.Show("Свободные номера не найдены");
                        }
                    }
                    else
                    {
                        string errorContent = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($"Ошибка сервера: {response.StatusCode}\n{errorContent}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
                Console.WriteLine($"Ошибка: {ex}");
            }
        }

        private void dataGridViewRoomsChast_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewRoomsChast.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridViewRoomsChast.SelectedRows[0];
                string roomNumber = selectedRow.Cells["RoomNumber"].Value?.ToString() ?? "не указан";
                labelselectroom.Text = $"Выбран номер: {roomNumber}";
            }
            else
            {
                labelselectroom.Text = "Выбран номер: (не выбран)";
            }
        }

        private async void searchRoomsOrg_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime startDate = monthCalendar2.SelectionStart;
                DateTime endDate = monthCalendar2.SelectionEnd;
                int requiredCapacity = (int)kolvoguestsOrg.Value;

                // Формируем URL запроса
                string url = $"{apiBaseUrl}/Rooms/searchForOrganization?" +
                           $"startDate={startDate:yyyy-MM-dd}&" +
                           $"endDate={endDate:yyyy-MM-dd}&" +
                           $"minCapacity=1"; // Ищем все номера с вместимостью от 1

                Console.WriteLine($"Запрос свободных номеров: {url}");

                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    var allRooms = JsonSerializer.Deserialize<List<Room>>(json,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    Console.WriteLine($"Получено номеров: {allRooms?.Count ?? 0}");

                    if (allRooms == null || allRooms.Count == 0)
                    {
                        MessageBox.Show("Свободные номера не найдены");
                        dataGridViewRoomsOrg.DataSource = null;
                        return;
                    }

                    // Создаем DataTable для результатов
                    DataTable result = new DataTable();
                    result.Columns.Add("RoomNumber", typeof(int));
                    result.Columns.Add("Floor", typeof(int));
                    result.Columns.Add("Category", typeof(string));
                    result.Columns.Add("Capacity", typeof(int));
                    result.Columns.Add("PricePerDay", typeof(decimal));

                    // Алгоритм подбора номеров
                    var selectedRooms = new List<Room>();
                    int remainingCapacity = requiredCapacity;

                    // 1. Сортируем номера по этажам и вместимости
                    var roomsByFloor = allRooms
                        .OrderBy(r => r.Floor)
                        .ThenByDescending(r => r.Capacity)
                        .ToList();

                    // 2. Пытаемся найти номера на одном этаже
                    foreach (var FloorGroup in allRooms.GroupBy(r => r.Floor).OrderBy(g => g.Key))
                    {
                        int FloorCapacity = FloorGroup.Sum(r => r.Capacity);
                        if (FloorCapacity >= remainingCapacity)
                        {
                            foreach (var room in FloorGroup.OrderByDescending(r => r.Capacity))
                            {
                                selectedRooms.Add(room);
                                result.Rows.Add(room.RoomNumber, room.Floor, room.Category,
                                              room.Capacity, room.PricePerDay);
                                remainingCapacity -= room.Capacity;
                                if (remainingCapacity <= 0) break;
                            }
                            break;
                        }
                    }

                    // 3. Если не хватило одного этажа - добираем с других
                    if (remainingCapacity > 0)
                    {
                        selectedRooms.Clear();
                        result.Clear();
                        remainingCapacity = requiredCapacity;

                        foreach (var room in allRooms.OrderByDescending(r => r.Capacity))
                        {
                            selectedRooms.Add(room);
                            result.Rows.Add(room.RoomNumber, room.Floor, room.Category,
                                          room.Capacity, room.PricePerDay);
                            remainingCapacity -= room.Capacity;
                            if (remainingCapacity <= 0) break;
                        }
                    }

                    // 4. Отображаем результаты
                    if (remainingCapacity > 0)
                    {
                        MessageBox.Show($"Недостаточно мест! Доступно только {requiredCapacity - remainingCapacity} из {requiredCapacity}");
                    }

                    dataGridViewRoomsOrg.DataSource = result;
                    dataGridViewRoomsOrg.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dataGridViewRoomsOrg.MultiSelect = true;
                    dataGridViewRoomsOrg.ClearSelection();

                    UpdateSelectedRoomsLabel();
                }
                else
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Ошибка: {response.StatusCode}\n{errorContent}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при поиске номеров: {ex.Message}");
                Console.WriteLine($"Ошибка: {ex}");
            }
        }



        private async void UpdateSelectedRoomsLabel()
        {
            if (dataGridViewRoomsOrg.SelectedRows.Count == 0)
            {
                labelselectrooms.Text = "Номера не выбраны";
                return;
            }

            var selectedRooms = new List<string>();
            int totalCapacity = 0;
            var Floors = new HashSet<int>();

            foreach (DataGridViewRow row in dataGridViewRoomsOrg.SelectedRows)
            {
                if (row.Cells["RoomNumber"].Value != null)
                {
                    string roomNumber = row.Cells["RoomNumber"].Value.ToString();
                    selectedRooms.Add(roomNumber);

                    if (row.Cells["Capacity"].Value != null)
                        totalCapacity += Convert.ToInt32(row.Cells["Capacity"].Value);

                    if (row.Cells["Floor"].Value != null)
                        Floors.Add(Convert.ToInt32(row.Cells["Floor"].Value));
                }
            }

            DateTime startDate = monthCalendar2.SelectionStart;
            DateTime endDate = monthCalendar2.SelectionEnd;
            int numberOfDays = (endDate - startDate).Days;
            if (numberOfDays == 0) numberOfDays = 1;

            decimal totalRoomCostPerDay = 0;
            foreach (DataGridViewRow row in dataGridViewRoomsOrg.SelectedRows)
            {
                if (decimal.TryParse(row.Cells["PricePerDay"].Value?.ToString(), out decimal pricePerDay))
                {
                    totalRoomCostPerDay += pricePerDay;
                }
            }

            decimal totalRoomCost = totalRoomCostPerDay * numberOfDays;

            decimal totalServiceCostPerDay = 0;
            decimal totalServiceCost = 0;
            List<string> selectedServices = checkedListBoxSelectServicesOrg.CheckedItems.Cast<string>().ToList();

            decimal DiscountPercent = 0;
            if (dataGridView2.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите организацию.");
                return;
            }

            int guestId = Convert.ToInt32(dataGridView2.SelectedRows[0].Cells["id"].Value);

            try
            {
                // Получаем скидку организации
                var response = await client.GetAsync($"{apiBaseUrl}/Organizations/{guestId}");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    var guest = JsonSerializer.Deserialize<Guest>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    if (!string.IsNullOrWhiteSpace(guest.Discount))
                    {
                        var cleaned = guest.Discount.Replace("%", "").Trim();
                        if (decimal.TryParse(cleaned, out decimal parsedDiscount))
                        {
                            DiscountPercent = parsedDiscount;
                        }
                    }
                }

                // Получаем цены услуг
                foreach (string serviceName in selectedServices)
                {
                    var serviceResponse = await client.GetAsync($"{apiBaseUrl}/Services?name={serviceName}");
                    if (serviceResponse.IsSuccessStatusCode)
                    {
                        string serviceJson = await serviceResponse.Content.ReadAsStringAsync();
                        var services = JsonSerializer.Deserialize<List<Service>>(serviceJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                        if (services.Any())
                        {
                            totalServiceCostPerDay += services.First().Price;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при расчете стоимости: {ex.Message}");
                return;
            }

            totalServiceCost = totalServiceCostPerDay * numberOfDays;
            decimal totalBeforeDiscount = totalRoomCost + totalServiceCost;
            decimal DiscountAmount = totalBeforeDiscount * (DiscountPercent / 100);
            decimal totalCost = totalBeforeDiscount - DiscountAmount;

            string FloorsText = Floors.Count == 1
                ? $"Этаж: {Floors.First()}"
                : $"Этажи: {string.Join(", ", Floors.OrderBy(f => f))}";

            labelselectrooms.Text = $"Выбраны номера: {string.Join(", ", selectedRooms)}\n" +
                                    $"{FloorsText}\n" +
                                    $"Общая вместимость: {totalCapacity}\n" +
                                    $"Сумма за номера: {totalRoomCost:N2} руб. ({totalRoomCostPerDay:N2} руб./день)\n" +
                                    $"Сумма за услуги: {totalServiceCost:N2} руб. ({totalServiceCostPerDay:N2} руб./день)\n" +
                                    $"Скидка организации: {DiscountPercent}% (-{DiscountAmount:N2} руб.)\n" +
                                    $"Итого за {numberOfDays} дн.: {totalCost:N2} руб.";
        }

        private void DataGridViewRoomsOrg_SelectionChanged(object sender, EventArgs e)
        {
            UpdateSelectedRoomsLabel();
        }

        private async void buttonBookRoomsOrg_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewRoomsOrg.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Выберите номера для бронирования!");
                    return;
                }

                if (dataGridView2.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Выберите организацию для бронирования!");
                    return;
                }

                DateTime startDate = monthCalendar2.SelectionStart;
                DateTime endDate = monthCalendar2.SelectionEnd;
                int guestId = Convert.ToInt32(dataGridView2.SelectedRows[0].Cells["Id"].Value);

                List<int> selectedServices = new List<int>();
                foreach (string serviceName in checkedListBoxSelectServicesOrg.CheckedItems)
                {
                    var getResponse = await client.GetAsync($"{apiBaseUrl}/Services?name={serviceName}");
                    if (getResponse.IsSuccessStatusCode)
                    {
                        string json = await getResponse.Content.ReadAsStringAsync();
                        var services = JsonSerializer.Deserialize<List<Service>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                        if (services.Any())
                        {
                            selectedServices.Add(services.First().Id);
                        }
                    }
                }

                var bookings = new List<BookingRequest>();
                foreach (DataGridViewRow row in dataGridViewRoomsOrg.SelectedRows)
                {
                    int roomNumber = Convert.ToInt32(row.Cells["RoomNumber"].Value);
                    decimal pricePerDay = Convert.ToDecimal(row.Cells["PricePerDay"].Value);

                    int days = (endDate - startDate).Days + 1;
                    decimal totalPrice = pricePerDay * days;

                    bookings.Add(new BookingRequest
                    {
                        RoomNumber = roomNumber,
                        StartDate = startDate,
                        EndDate = endDate,
                        GuestId = guestId,
                        Services = selectedServices,
                        TotalPrice = totalPrice
                    });
                }

                var content = new StringContent(JsonSerializer.Serialize(bookings), Encoding.UTF8, "application/json");
                var postResponse = await client.PostAsync($"{apiBaseUrl}/Bookings/bulk", content);

                if (postResponse.IsSuccessStatusCode)
                {
                    MessageBox.Show("Бронирование успешно создано!");
                }
                else
                {
                    MessageBox.Show("Не удалось создать бронирование");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private async void buttonBookingChast_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewRoomsChast.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Выберите номер для бронирования!");
                    return;
                }

                if (dataGridView1.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Выберите гостя для бронирования!");
                    return;
                }

                DateTime startDate = monthCalendar1.SelectionStart;
                DateTime endDate = monthCalendar1.SelectionEnd;
                int guestId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id"].Value);
                int roomNumber = Convert.ToInt32(dataGridViewRoomsChast.SelectedRows[0].Cells["RoomNumber"].Value);

                List<int> selectedServices = new List<int>();
                foreach (string serviceName in checkedListBoxSelectServicesChast.CheckedItems)
                {
                    var getResponse = await client.GetAsync($"{apiBaseUrl}/Services?name={serviceName}");
                    if (getResponse.IsSuccessStatusCode)
                    {
                        string json = await getResponse.Content.ReadAsStringAsync();
                        var services = JsonSerializer.Deserialize<List<Service>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                        if (services.Any())
                        {
                            selectedServices.Add(services.First().Id);
                        }
                    }
                }

                var booking = new BookingRequest
                {
                    RoomNumber = roomNumber,
                    StartDate = startDate,
                    EndDate = endDate,
                    GuestId = guestId,
                    Services = selectedServices,
                    TotalPrice = 0 // Рассчитается на сервере
                };

                var content = new StringContent(JsonSerializer.Serialize(booking), Encoding.UTF8, "application/json");
                var postResponse = await client.PostAsync($"{apiBaseUrl}/Bookings", content);

                if (postResponse.IsSuccessStatusCode)
                {
                    MessageBox.Show("Бронирование успешно создано!");
                }
                else
                {
                    MessageBox.Show("Не удалось создать бронирование");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }

        }


        // Классы для десериализации JSON
        public class Guest
        {
            public int Id { get; set; }
            public string FullNameOrOrganization { get; set; }
            public string Phone { get; set; }
            public string Discount { get; set; }
            public bool IsOrganization { get; set; }
        }

        public class Room
        {
            public int RoomNumber { get; set; }
            public int Floor { get; set; }
            public string Category { get; set; }
            public int Capacity { get; set; }
            public decimal PricePerDay { get; set; }
        }

        public class Service
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }
        }

        public class BookingRequest
        {
            public int RoomNumber { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public int GuestId { get; set; }
            public List<int> Services { get; set; }
            public decimal TotalPrice { get; set; }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void labelGuest_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void monthCalendar2_DateChanged(object sender, DateRangeEventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click_1(object sender, EventArgs e)
        {

        }
        private void dataGridViewRoomsOrg_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void dataGridViewRoomsChast_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void dataGridViewRoomsOrg_Click(object sender, EventArgs e)
        {

        }
        private void tabPage2_Click(object sender, EventArgs e)
        {

        }
    }
}