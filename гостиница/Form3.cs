using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace гостиница
{
    public partial class Form3 : Form
    {
        private readonly Database db;

        public Form3()
        {
            InitializeComponent();
            db = new Database("Host=localhost;Database=hotel;Username=postgres;Password=root");
            dataGridViewRoomsOrg.SelectionChanged += DataGridViewRoomsOrg_SelectionChanged;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            ConfigureDataGridView();
            LoadAllGuests();
            LoadAllOrganizations();
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

            // Добавляем колонки, соответствующие структуре таблицы guests
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "id",
                HeaderText = "ID",
                DataPropertyName = "id",
                Width = 50,
                Visible = false // Скрываем ID, если не нужно показывать
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "full_name",
                HeaderText = "ФИО",
                DataPropertyName = "full_name_or_organization",
                Width = 200
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "phone",
                HeaderText = "Телефон",
                DataPropertyName = "phone",
                Width = 120
            });

            dataGridView1.SelectionChanged += DataGridView_SelectionChanged;



            dataGridView2.AutoGenerateColumns = false;
            dataGridView2.ReadOnly = true;
            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.MultiSelect = false;
            dataGridView2.Columns.Clear();

            // Добавляем колонки, соответствующие структуре таблицы guests
            dataGridView2.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "id",
                HeaderText = "ID",
                DataPropertyName = "id",
                Width = 50,
                Visible = false // Скрываем ID, если не нужно показывать
            });

            dataGridView2.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "full_name",
                HeaderText = "Организация",
                DataPropertyName = "full_name_or_organization",
                Width = 200
            });

            dataGridView2.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "phone",
                HeaderText = "Телефон",
                DataPropertyName = "phone",
                Width = 120
            });

            dataGridView2.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "discount",
                HeaderText = "Скидка",
                DataPropertyName = "discount",
                Width = 80
            });

            dataGridView2.SelectionChanged += DataGridView_SelectionChanged;
        }

        private void DataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                string guestName = selectedRow.Cells["full_name"].Value?.ToString() ?? "не указано";
                labelGuest.Text = $"Выбранный гость: {guestName}";
            }

            if (dataGridView2.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView2.SelectedRows[0];
                string guestName = selectedRow.Cells["full_name"].Value?.ToString() ?? "не указано";
                labelOrg.Text = $"Выбранный гость: {guestName}";
            }
        }

        private void LoadAllGuests()
        {
            try
            {
                DataTable allGuests = db.GetAllGuests();
                dataGridView1.DataSource = allGuests;

                // Обновляем статусную строку
                labelStatus.Text = $"Всего гостей: {allGuests.Rows.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}");
            }

        }

        private void LoadAllOrganizations()
        {
            try
            {
                DataTable allOrganizations = db.GetAllOrganizations();
                dataGridView2.DataSource = allOrganizations;

                // Обновляем статусную строку
                labelStatusOrg.Text = $"Всего гостей: {allOrganizations.Rows.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}");
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void new_guest_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            if (form4.ShowDialog() == DialogResult.OK)
            {
                LoadAllGuests();
            }
        }

        // Добавляем кнопку для обновления данных
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadAllGuests();
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

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            string searchText = textBoxSearch.Text.Trim();

            if (string.IsNullOrWhiteSpace(searchText))
            {
                LoadAllGuests();
                return;
            }

            try
            {
                DataTable searchResults = db.SearchGuests(searchText);
                dataGridView1.DataSource = searchResults;

                if (searchResults.Rows.Count == 0)
                {
                    MessageBox.Show("Гости не найдены");
                    LoadAllGuests();
                }
                else
                {
                    labelStatus.Text = $"Найдено гостей: {searchResults.Rows.Count}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка поиска: {ex.Message}");
                LoadAllGuests();
            }
        }

        private void new_org_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            if (form5.ShowDialog() == DialogResult.OK)
            {
                LoadAllOrganizations();
            }
        }

        private void SearchOrg_Click(object sender, EventArgs e)
        {
            string searchText = textBoxSearchOrg.Text.Trim();

            if (string.IsNullOrWhiteSpace(searchText))
            {
                LoadAllOrganizations();
                return;
            }

            try
            {
                DataTable searchResults = db.SearchOrganizations(searchText);
                dataGridView2.DataSource = searchResults;

                if (searchResults.Rows.Count == 0)
                {
                    MessageBox.Show("Организации не найдены");
                    LoadAllOrganizations();
                }
                else
                {
                    labelStatusOrg.Text = $"Найдено организаций: {searchResults.Rows.Count}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка поиска: {ex.Message}");
                LoadAllOrganizations();
            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void searchRoomChast_Click(object sender, EventArgs e)
        {
            try
            {
                // Получаем выбранные даты из monthCalendar1
                DateTime startDate = monthCalendar1.SelectionStart;
                DateTime endDate = monthCalendar1.SelectionEnd;

                // Получаем выбранную категорию и минимальную вместимость
                string selectedCategory = comboBoxCategory.SelectedItem?.ToString();
                int minCapacity = (int)kolvoGuests.Value;

                // Проверяем, выбрана ли категория
                if (string.IsNullOrEmpty(selectedCategory))
                {
                    MessageBox.Show("Выберите категорию номера!");
                    return;
                }

                // Подключение к базе данных (используем Npgsql)
                using (var connection = new NpgsqlConnection("Host=localhost;Database=hotel;Username=postgres;Password=root"))
                {
                    connection.Open();

                    // SQL-запрос для поиска свободных номеров
                    string query = @"
                SELECT r.room_number, r.floor, r.category, r.capacity, r.price_per_day
                FROM rooms r
                WHERE r.category = @category
                AND r.capacity >= @minCapacity
                AND NOT EXISTS (
                    SELECT 1 FROM bookings b
                    WHERE b.room_number = r.room_number
                    AND (
                        (b.start_date <= @endDate AND b.end_date >= @startDate)
                    )
                )
                ORDER BY r.room_number";

                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@category", selectedCategory);
                        command.Parameters.AddWithValue("@minCapacity", minCapacity);
                        command.Parameters.AddWithValue("@startDate", startDate);
                        command.Parameters.AddWithValue("@endDate", endDate);

                        // Заполняем DataGridView
                        var adapter = new NpgsqlDataAdapter(command);
                        var dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        dataGridViewRoomsChast.DataSource = dataTable;
                    }
                }
                dataGridViewRoomsChast.SelectionChanged += dataGridViewRoomsChast_SelectionChanged;
                if (dataGridViewRoomsChast.Rows.Count > 0)
                {
                    dataGridViewRoomsChast.ClearSelection();
                    labelselectroom.Text = "Выбран номер: (не выбран)";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void dataGridViewRoomsChast_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridViewRoomsChast_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewRoomsChast.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridViewRoomsChast.SelectedRows[0];
                string roomNumber = selectedRow.Cells["room_number"].Value?.ToString() ?? "не указан";
                labelselectroom.Text = $"Выбран номер: {roomNumber}";
            }
            else
            {
                labelselectroom.Text = "Выбран номер: (не выбран)";
            }
        }

        private void dataGridViewRoomsOrg_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridViewRoomsOrg_Click(object sender, EventArgs e)
        {

        }

        private void searchRoomsOrg_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime startDate = monthCalendar2.SelectionStart;
                DateTime endDate = monthCalendar2.SelectionEnd;
                int requiredCapacity = (int)kolvoguestsOrg.Value;

                using (var connection = new NpgsqlConnection("Host=localhost;Database=hotel;Username=postgres;Password=root"))
                {
                    connection.Open();

                    // 1. Получаем все свободные номера
                    string freeRoomsQuery = @"
                SELECT r.room_number, r.floor, r.category, r.capacity, r.price_per_day
                FROM rooms r
                WHERE NOT EXISTS (
                    SELECT 1 FROM bookings b
                    WHERE b.room_number = r.room_number
                    AND (b.start_date <= @endDate AND b.end_date >= @startDate)
                )
                ORDER BY r.floor, r.capacity ASC"; // Сначала маленькие номера

                    var adapter = new NpgsqlDataAdapter(freeRoomsQuery, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@startDate", startDate);
                    adapter.SelectCommand.Parameters.AddWithValue("@endDate", endDate);

                    DataTable freeRooms = new DataTable();
                    adapter.Fill(freeRooms);

                    // 2. Алгоритм выбора номеров
                    DataTable result = freeRooms.Clone();
                    int remainingCapacity = requiredCapacity;
                    var roomsByFloor = freeRooms.AsEnumerable()
                        .GroupBy(r => r.Field<int>("floor"))
                        .OrderBy(g => g.Key); // Сортировка по номеру этажа

                    // Сначала пробуем найти один этаж
                    foreach (var floorGroup in roomsByFloor)
                    {
                        int floorCapacity = floorGroup.Sum(r => r.Field<int>("capacity"));
                        if (floorCapacity >= remainingCapacity)
                        {
                            foreach (DataRow row in floorGroup.OrderByDescending(r => r.Field<int>("capacity")))
                            {
                                result.ImportRow(row);
                                remainingCapacity -= row.Field<int>("capacity");
                                if (remainingCapacity <= 0) break;
                            }
                            break;
                        }
                    }

                    // Если не хватило одного этажа - добираем с других
                    if (remainingCapacity > 0)
                    {
                        foreach (var floorGroup in roomsByFloor)
                        {
                            foreach (DataRow row in floorGroup.OrderByDescending(r => r.Field<int>("capacity")))
                            {
                                if (!result.AsEnumerable().Any(r => r.Field<int>("room_number") == row.Field<int>("room_number")))
                                {
                                    result.ImportRow(row);
                                    remainingCapacity -= row.Field<int>("capacity");
                                    if (remainingCapacity <= 0) break;
                                }
                            }
                            if (remainingCapacity <= 0) break;
                        }
                    }

                    // 3. Если все равно не хватает - показываем что есть
                    if (remainingCapacity > 0)
                    {
                        MessageBox.Show($"Недостаточно мест! Доступно только {requiredCapacity - remainingCapacity} из {requiredCapacity}");
                    }

                    // 4. Настройка DataGridView
                    dataGridViewRoomsOrg.DataSource = result;
                    dataGridViewRoomsOrg.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dataGridViewRoomsOrg.MultiSelect = true;
                    dataGridViewRoomsOrg.ClearSelection();

                    UpdateSelectedRoomsLabel();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void UpdateSelectedRoomsLabel()
        {
            if (dataGridViewRoomsOrg.SelectedRows.Count == 0)
            {
                labelselectrooms.Text = "Номера не выбраны";
                return;
            }

            var selectedRooms = new List<string>();
            int totalCapacity = 0;
            var floors = new HashSet<int>();

            foreach (DataGridViewRow row in dataGridViewRoomsOrg.SelectedRows)
            {
                if (row.Cells["room_number"].Value != null)
                {
                    string roomNumber = row.Cells["room_number"].Value.ToString();
                    selectedRooms.Add(roomNumber);

                    if (row.Cells["capacity"].Value != null)
                        totalCapacity += Convert.ToInt32(row.Cells["capacity"].Value);

                    if (row.Cells["floor"].Value != null)
                        floors.Add(Convert.ToInt32(row.Cells["floor"].Value));
                }
            }

            DateTime startDate = monthCalendar2.SelectionStart;
            DateTime endDate = monthCalendar2.SelectionEnd;
            int numberOfDays = (endDate - startDate).Days;
            if (numberOfDays == 0) numberOfDays = 1;

            decimal totalRoomCostPerDay = 0;
            foreach (DataGridViewRow row in dataGridViewRoomsOrg.SelectedRows)
            {
                if (decimal.TryParse(row.Cells["price_per_day"].Value?.ToString(), out decimal pricePerDay))
                {
                    totalRoomCostPerDay += pricePerDay;
                }
                else
                {
                    MessageBox.Show("Ошибка: неверный формат 'price_per_day' у одного из номеров.");
                    return;
                }
            }

            decimal totalRoomCost = totalRoomCostPerDay * numberOfDays;

            decimal totalServiceCostPerDay = 0;
            decimal totalServiceCost = 0;
            List<string> selectedServices = checkedListBoxSelectServicesOrg.CheckedItems.Cast<string>().ToList();

            decimal discountPercent = 0;
            if (dataGridView2.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите организацию.");
                return;
            }

            string orgName = dataGridView2.SelectedRows[0].Cells["full_name"].Value?.ToString();

            using (var connection = new NpgsqlConnection("Host=localhost;Database=hotel;Username=postgres;Password=root"))
            {
                connection.Open();

                // Получаем скидку
                string discountQuery = "SELECT discount FROM guests WHERE full_name_or_organization = @orgName";
                using (var cmd = new NpgsqlCommand(discountQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@orgName", orgName);
                    object result = cmd.ExecuteScalar();
                    if (result != null && decimal.TryParse(result.ToString().Replace("%", ""), out decimal parsedDiscount))
                    {
                        discountPercent = parsedDiscount;
                    }
                }

                // Стоимость услуг
                foreach (string serviceName in selectedServices)
                {
                    string query = "SELECT price FROM services WHERE service_name = @name";
                    using (var cmd = new NpgsqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@name", serviceName);
                        object result = cmd.ExecuteScalar();
                        if (result != null && decimal.TryParse(result.ToString(), out decimal price))
                        {
                            totalServiceCostPerDay += price;
                        }
                        else
                        {
                            MessageBox.Show($"Ошибка при получении цены для услуги '{serviceName}'");
                            return;
                        }
                    }
                }
            }

            totalServiceCost = totalServiceCostPerDay * numberOfDays;
            decimal totalBeforeDiscount = totalRoomCost + totalServiceCost;
            decimal discountAmount = totalBeforeDiscount * (discountPercent / 100);
            decimal totalCost = totalBeforeDiscount - discountAmount;

            string floorsText = floors.Count == 1
                ? $"Этаж: {floors.First()}"
                : $"Этажи: {string.Join(", ", floors.OrderBy(f => f))}";

            labelselectrooms.Text = $"Выбраны номера: {string.Join(", ", selectedRooms)}\n" +
                                    $"{floorsText}\n" +
                                    $"Общая вместимость: {totalCapacity}\n" +
                                    $"Сумма за номера: {totalRoomCost:N2} руб. ({totalRoomCostPerDay:N2} руб./день)\n" +
                                    $"Сумма за услуги: {totalServiceCost:N2} руб. ({totalServiceCostPerDay:N2} руб./день)\n" +
                                    $"Скидка организации: {discountPercent}% (-{discountAmount:N2} руб.)\n" +
                                    $"Итого за {numberOfDays} дн.: {totalCost:N2} руб.";
        }


        // Обработчик изменения выбора
        private void DataGridViewRoomsOrg_SelectionChanged(object sender, EventArgs e)
        {
            UpdateSelectedRoomsLabel();
        }

        private void buttonBookRoomsOrg_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Проверка выбранных данных
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
                int guestId = Convert.ToInt32(dataGridView2.SelectedRows[0].Cells["id"].Value);

                // 2. Получаем ID выбранных услуг по их названиям
                List<int> selectedServices = new List<int>();
                using (var connection = new NpgsqlConnection("Host=localhost;Database=hotel;Username=postgres;Password=root"))
                {
                    connection.Open();

                    foreach (string serviceName in checkedListBoxSelectServicesOrg.CheckedItems)
                    {
                        string query = "SELECT id FROM services WHERE service_name = @serviceName";
                        using (var command = new NpgsqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@serviceName", serviceName);
                            object result = command.ExecuteScalar();
                            if (result != null)
                            {
                                selectedServices.Add(Convert.ToInt32(result));
                            }
                        }
                    }
                }

                // 3. Создаем бронирование для каждого номера
                using (var connection = new NpgsqlConnection("Host=localhost;Database=hotel;Username=postgres;Password=root"))
                {
                    connection.Open();

                    // Получаем скидку из таблицы guests
                    decimal discountPercentage = 0;
                    string discountQuery = "SELECT discount FROM guests WHERE id = @guestId";
                    using (var discountCommand = new NpgsqlCommand(discountQuery, connection))
                    {
                        discountCommand.Parameters.AddWithValue("@guestId", guestId);
                        object discountResult = discountCommand.ExecuteScalar();
                        if (discountResult != null && discountResult.ToString() != "None")
                        {
                            string discountStr = discountResult.ToString().Trim().Replace("%", "");
                            if (decimal.TryParse(discountStr, out var parsed))
                            {
                                discountPercentage = parsed;
                            }
                        }
                    }

                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            foreach (DataGridViewRow row in dataGridViewRoomsOrg.SelectedRows)
                            {
                                int roomNumber = Convert.ToInt32(row.Cells["room_number"].Value);

                                // Проверка доступности номера
                                string checkQuery = @"
                    SELECT COUNT(*) FROM bookings 
                    WHERE room_number = @roomNumber 
                    AND (start_date <= @endDate AND end_date >= @startDate)";

                                using (var checkCommand = new NpgsqlCommand(checkQuery, connection, transaction))
                                {
                                    checkCommand.Parameters.AddWithValue("@roomNumber", roomNumber);
                                    checkCommand.Parameters.AddWithValue("@startDate", startDate);
                                    checkCommand.Parameters.AddWithValue("@endDate", endDate);

                                    if ((long)checkCommand.ExecuteScalar() > 0)
                                    {
                                        MessageBox.Show($"Номер {roomNumber} уже забронирован на выбранные даты!");
                                        continue;
                                    }
                                }

                                // Получаем цену номера
                                decimal roomPricePerDay = 0;
                                string priceQuery = "SELECT price_per_day FROM rooms WHERE room_number = @roomNumber";
                                using (var priceCommand = new NpgsqlCommand(priceQuery, connection, transaction))
                                {
                                    priceCommand.Parameters.AddWithValue("@roomNumber", roomNumber);
                                    object priceResult = priceCommand.ExecuteScalar();
                                    if (priceResult != null)
                                    {
                                        roomPricePerDay = Convert.ToDecimal(priceResult);
                                    }
                                }

                                // Получаем сумму стоимости услуг в сутки
                                decimal servicesPerDayTotal = 0;
                                if (selectedServices.Any())
                                {
                                    string serviceQuery = "SELECT price FROM services WHERE id = ANY(@ids)";
                                    using (var serviceCommand = new NpgsqlCommand(serviceQuery, connection, transaction))
                                    {
                                        serviceCommand.Parameters.AddWithValue("@ids", selectedServices.ToArray());
                                        using (var reader = serviceCommand.ExecuteReader())
                                        {
                                            while (reader.Read())
                                            {
                                                servicesPerDayTotal += reader.GetDecimal(0);
                                            }
                                        }
                                    }
                                }

                                int days = (endDate - startDate).Days + 1;
                                decimal totalPrice = (roomPricePerDay + servicesPerDayTotal) * days;

                                // Применение скидки
                                if (discountPercentage > 0)
                                {
                                    totalPrice = totalPrice * (1 - discountPercentage / 100);
                                }

                                // Создание бронирования
                                string insertQuery = @"
                    INSERT INTO bookings 
                    (room_number, start_date, end_date, guest_id, services, total_price) 
                    VALUES 
                    (@roomNumber, @startDate, @endDate, @guestId, @services, @totalPrice)";

                                using (var insertCommand = new NpgsqlCommand(insertQuery, connection, transaction))
                                {
                                    insertCommand.Parameters.AddWithValue("@roomNumber", roomNumber);
                                    insertCommand.Parameters.AddWithValue("@startDate", startDate);
                                    insertCommand.Parameters.AddWithValue("@endDate", endDate);
                                    insertCommand.Parameters.AddWithValue("@guestId", guestId);
                                    insertCommand.Parameters.AddWithValue("@services", selectedServices.Any() ? selectedServices.ToArray() : Array.Empty<int>());
                                    insertCommand.Parameters.AddWithValue("@totalPrice", totalPrice);

                                    insertCommand.ExecuteNonQuery();
                                }
                            }

                            transaction.Commit();
                            MessageBox.Show("Бронирование успешно создано!");
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show($"Ошибка при бронировании: {ex.Message}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void buttonBookingChast_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Проверка выбранных данных
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
                int guestId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["id"].Value);

                // 2. Получаем ID выбранных услуг по их названиям
                List<int> selectedServices = new List<int>();
                using (var connection = new NpgsqlConnection("Host=localhost;Database=hotel;Username=postgres;Password=root"))
                {
                    connection.Open();

                    foreach (string serviceName in checkedListBoxSelectServicesChast.CheckedItems)
                    {
                        string query = "SELECT id FROM services WHERE service_name = @serviceName";
                        using (var command = new NpgsqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@serviceName", serviceName);
                            object result = command.ExecuteScalar();
                            if (result != null)
                            {
                                selectedServices.Add(Convert.ToInt32(result));
                            }
                        }
                    }
                }

                // 3. Создаем бронирование
                using (var connection = new NpgsqlConnection("Host=localhost;Database=hotel;Username=postgres;Password=root"))
                {
                    connection.Open();

                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            int roomNumber = Convert.ToInt32(dataGridViewRoomsChast.SelectedRows[0].Cells["room_number"].Value);

                            // Проверка доступности номера
                            string checkQuery = @"
                        SELECT COUNT(*) FROM bookings 
                        WHERE room_number = @roomNumber 
                        AND (start_date <= @endDate AND end_date >= @startDate)";

                            using (var checkCommand = new NpgsqlCommand(checkQuery, connection, transaction))
                            {
                                checkCommand.Parameters.AddWithValue("@roomNumber", roomNumber);
                                checkCommand.Parameters.AddWithValue("@startDate", startDate);
                                checkCommand.Parameters.AddWithValue("@endDate", endDate);

                                if ((long)checkCommand.ExecuteScalar() > 0)
                                {
                                    MessageBox.Show($"Номер {roomNumber} уже забронирован на выбранные даты!");
                                    return;
                                }
                            }

                            // Получаем цену номера
                            decimal roomPricePerDay = 0;
                            string priceQuery = "SELECT price_per_day FROM rooms WHERE room_number = @roomNumber";
                            using (var priceCommand = new NpgsqlCommand(priceQuery, connection, transaction))
                            {
                                priceCommand.Parameters.AddWithValue("@roomNumber", roomNumber);
                                object priceResult = priceCommand.ExecuteScalar();
                                if (priceResult != null)
                                {
                                    roomPricePerDay = Convert.ToDecimal(priceResult);
                                }
                            }

                            // Получаем сумму стоимости услуг в сутки
                            decimal servicesPerDayTotal = 0;
                            if (selectedServices.Any())
                            {
                                string serviceQuery = "SELECT price FROM services WHERE id = ANY(@ids)";
                                using (var serviceCommand = new NpgsqlCommand(serviceQuery, connection, transaction))
                                {
                                    serviceCommand.Parameters.AddWithValue("@ids", selectedServices.ToArray());
                                    using (var reader = serviceCommand.ExecuteReader())
                                    {
                                        while (reader.Read())
                                        {
                                            servicesPerDayTotal += reader.GetDecimal(0);
                                        }
                                    }
                                }
                            }

                            int days = (endDate - startDate).Days + 1;
                            decimal totalPrice = (roomPricePerDay + servicesPerDayTotal) * days;

                            // Создание бронирования
                            string insertQuery = @"
                        INSERT INTO bookings 
                        (room_number, start_date, end_date, guest_id, services, total_price) 
                        VALUES 
                        (@roomNumber, @startDate, @endDate, @guestId, @services, @totalPrice)";

                            using (var insertCommand = new NpgsqlCommand(insertQuery, connection, transaction))
                            {
                                insertCommand.Parameters.AddWithValue("@roomNumber", roomNumber);
                                insertCommand.Parameters.AddWithValue("@startDate", startDate);
                                insertCommand.Parameters.AddWithValue("@endDate", endDate);
                                insertCommand.Parameters.AddWithValue("@guestId", guestId);
                                insertCommand.Parameters.AddWithValue("@services", selectedServices.Any() ? selectedServices.ToArray() : Array.Empty<int>());
                                insertCommand.Parameters.AddWithValue("@totalPrice", totalPrice);

                                insertCommand.ExecuteNonQuery();
                            }

                            transaction.Commit();
                            MessageBox.Show("Бронирование успешно создано!");
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show($"Ошибка при бронировании: {ex.Message}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void label5_Click_1(object sender, EventArgs e)
        {

        }
    }
}