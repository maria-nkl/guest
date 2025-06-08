/*using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace гостиница
{
    *//*public class RoomInfo
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
    }*//*

    internal class Database
    {
        private readonly string connectionString;

        public Database(string connectionString)
        {
            this.connectionString = connectionString;
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    // Создание таблицы Ролей
                    using (var command = new NpgsqlCommand(@"
                        CREATE TABLE IF NOT EXISTS roles (
                            id SERIAL PRIMARY KEY,
                            role_name TEXT NOT NULL,
                            position TEXT NOT NULL
                        )", connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    // Создание таблицы Пользователей
                    using (var command = new NpgsqlCommand(@"
                        CREATE TABLE IF NOT EXISTS users (
                            id SERIAL PRIMARY KEY,
                            login TEXT NOT NULL UNIQUE,
                            password TEXT NOT NULL,
                            role_id INTEGER REFERENCES roles(id),
                            full_name TEXT NOT NULL
                        )", connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    // Создание таблицы Гостей
                    using (var command = new NpgsqlCommand(@"
                        CREATE TABLE IF NOT EXISTS guests (
                            id SERIAL PRIMARY KEY,
                            full_name_or_organization TEXT NOT NULL,
                            phone TEXT NOT NULL,
                            discount TEXT DEFAULT 'None',
                            is_organization BOOLEAN DEFAULT FALSE
                        )", connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    // Создание таблицы Номера
                    using (var command = new NpgsqlCommand(@"
                        CREATE TABLE IF NOT EXISTS rooms (
                            room_number INTEGER PRIMARY KEY,
                            floor INTEGER NOT NULL,
                            category TEXT NOT NULL,
                            capacity INTEGER NOT NULL,
                            price_per_day DECIMAL(10,2) NOT NULL,
                            request_details TEXT DEFAULT ''
                        )", connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    // Создание таблицы Услуги
                    using (var command = new NpgsqlCommand(@"
                        CREATE TABLE IF NOT EXISTS services (
                            id SERIAL PRIMARY KEY,
                            service_name TEXT NOT NULL,
                            price DECIMAL(10,2) NOT NULL
                        )", connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    // Создание таблицы Бронирования
                    using (var command = new NpgsqlCommand(@"
                        CREATE TABLE IF NOT EXISTS bookings (
                            id SERIAL PRIMARY KEY,
                            room_number INTEGER REFERENCES rooms(room_number),
                            start_date DATE NOT NULL,
                            end_date DATE NOT NULL,
                            guest_id INTEGER REFERENCES guests(id),
                            services INTEGER[] DEFAULT ARRAY[]::INTEGER[],
                            price_per_day DECIMAL(10,2) DEFAULT 0.00
                        )", connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    // Проверка на пустые таблицы и заполнение начальными данными
                    CheckAndSeedData(connection);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка инициализации БД: {ex.Message}");
            }
        }

        private void CheckAndSeedData(NpgsqlConnection connection)
        {
            // Проверка и заполнение таблицы Ролей
            using (var checkCommand = new NpgsqlCommand("SELECT COUNT(*) FROM roles", connection))
            {
                if ((long)checkCommand.ExecuteScalar() == 0)
                {
                    using (var insertCommand = new NpgsqlCommand(@"
                        INSERT INTO roles (role_name, position) VALUES 
                        ('Администратор', 'Администратор системы'),
                        ('Менеджер', 'Менеджер по бронированию'),
                        ('Горничная', 'Обслуживающий персонал')", connection))
                    {
                        insertCommand.ExecuteNonQuery();
                    }
                }
            }

            // Проверка и заполнение таблицы Пользователей
            using (var checkCommand = new NpgsqlCommand("SELECT COUNT(*) FROM users", connection))
            {
                if ((long)checkCommand.ExecuteScalar() == 0)
                {
                    using (var insertCommand = new NpgsqlCommand(@"
                        INSERT INTO users (login, password, role_id, full_name) VALUES 
                        ('admin', 'admin123', 1, 'Иванов Иван Иванович'),
                        ('manager', 'manager123', 2, 'Петрова Мария Сергеевна')", connection))
                    {
                        insertCommand.ExecuteNonQuery();
                    }
                }
            }

            // Проверка и заполнение таблицы Гостей
            using (var checkCommand = new NpgsqlCommand("SELECT COUNT(*) FROM guests", connection))
            {
                if ((long)checkCommand.ExecuteScalar() == 0)
                {
                    using (var insertCommand = new NpgsqlCommand(@"
            INSERT INTO guests (full_name_or_organization, phone, discount, is_organization) VALUES 
            ('Иванов Алексей Петрович', '+79161234567', 'None', FALSE),
            ('ООО Ромашка', '+74951234567', '10%', TRUE),
            ('Сидорова Мария Ивановна', '+79261234567', 'None', FALSE)", connection))
                    {
                        insertCommand.ExecuteNonQuery();
                    }
                }
            }

            // Проверка и заполнение таблицы Номера
            using (var checkCommand = new NpgsqlCommand("SELECT COUNT(*) FROM rooms", connection))
            {
                if ((long)checkCommand.ExecuteScalar() == 0)
                {
                    using (var insertCommand = new NpgsqlCommand(@"
                        INSERT INTO rooms (room_number, floor, category, capacity, price_per_day) VALUES 
                        (101, 1, 'Стандарт', 2, 3500.00),
                        (204, 2, 'Эконом', 2, 2000.00),
                        (303, 3, 'Люкс', 4, 5500.00),
                        (405, 4, 'Стандарт', 2, 3500.00)", connection))
                    {
                        insertCommand.ExecuteNonQuery();
                    }
                }
            }

            // Проверка и заполнение таблицы Услуги
            using (var checkCommand = new NpgsqlCommand("SELECT COUNT(*) FROM services", connection))
            {
                if ((long)checkCommand.ExecuteScalar() == 0)
                {
                    using (var insertCommand = new NpgsqlCommand(@"
                        INSERT INTO services (service_name, price) VALUES 
                        ('Завтрак', 500.00),
                        ('Ужин', 800.00),
                        ('СПА-процедуры', 2500.00),
                        ('Трансфер', 1500.00)", connection))
                    {
                        insertCommand.ExecuteNonQuery();
                    }
                }
            }

            // Проверка и заполнение таблицы Бронирования
            using (var checkCommand = new NpgsqlCommand("SELECT COUNT(*) FROM bookings", connection))
            {
                if ((long)checkCommand.ExecuteScalar() == 0)
                {
                    using (var insertCommand = new NpgsqlCommand(@"
                        INSERT INTO bookings (room_number, start_date, end_date, guest_id, services) VALUES 
                        (101, '2025-05-10', '2025-05-20', 1, ARRAY[1,4]),
                        (204, '2025-05-01', '2025-05-10', 2, ARRAY[1,2]),
                        (303, '2025-05-05', '2025-05-15', 3, ARRAY[]::INTEGER[])", connection))
                    {
                        insertCommand.ExecuteNonQuery();
                    }
                }
            }
        }

        // Методы для работы с таблицами...
        public string GetGuestByRoomNumber(int roomNumber, bool currentOnly = true)
        {
            try
            {
                string query = @"
                SELECT g.full_name_or_organization
                FROM guests g
                JOIN bookings b ON g.id = b.guest_id
                WHERE b.room_number = @number";

                if (currentOnly)
                {
                    query += " AND CURRENT_DATE BETWEEN b.start_date AND b.end_date";
                }

                using (var connection = new NpgsqlConnection(connectionString))
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@number", roomNumber);
                    connection.Open();
                    return command.ExecuteScalar()?.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка получения данных: {ex.Message}");
                return null;
            }
        }

        public string GetGuestNumberByRoomNumber(int roomNumber, bool currentOnly = true)
        {
            try
            {
                string query = @"
                SELECT g.phone
                FROM guests g
                JOIN bookings b ON g.id = b.guest_id
                WHERE b.room_number = @number";

                if (currentOnly)
                {
                    query += " AND CURRENT_DATE BETWEEN b.start_date AND b.end_date";
                }

                using (var connection = new NpgsqlConnection(connectionString))
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@number", roomNumber);
                    connection.Open();
                    return command.ExecuteScalar()?.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка получения данных: {ex.Message}");
                return null;
            }
        }
        public string GetFloorNumberByRoomNumber(int roomNumber)
        {
            try
            {
                string query = @"
                SELECT r.floor
                FROM rooms r
                WHERE room_number = @number";

                using (var connection = new NpgsqlConnection(connectionString))
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@number", roomNumber);
                    connection.Open();
                    var result = command.ExecuteScalar();
                    return result?.ToString() ?? "Не указан";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка получения данных: {ex.Message}");
                return null;
            }
        }
        // Метод для поиска гостей по ФИО
        public DataTable SearchGuests(string searchText)
        {
            var result = new DataTable();

            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    using (var command = new NpgsqlCommand(
                        "SELECT full_name_or_organization, phone FROM guests WHERE full_name_or_organization ILIKE @search and is_organization = 'FALSE'",
                        connection))
                    {
                        command.Parameters.AddWithValue("@search", $"%{searchText}%");

                        using (var adapter = new NpgsqlDataAdapter(command))
                        {
                            adapter.Fill(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка поиска: {ex.Message}");
            }

            return result;
        }

        // Метод для поиска организаций
        public DataTable SearchOrganizations(string searchText)
        {
            var result = new DataTable();

            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    using (var command = new NpgsqlCommand(
                        "SELECT full_name_or_organization, phone, discount FROM guests WHERE full_name_or_organization ILIKE @search and is_organization = 'TRUE'",
                        connection))
                    {
                        command.Parameters.AddWithValue("@search", $"%{searchText}%");

                        using (var adapter = new NpgsqlDataAdapter(command))
                        {
                            adapter.Fill(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка поиска: {ex.Message}");
            }

            return result;
        }


        public DataTable GetAllGuests()
        {
            var result = new DataTable();

            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                using (var command = new NpgsqlCommand(
                    "SELECT id, full_name_or_organization, phone FROM guests WHERE is_organization = 'FALSE'",
                    connection))
                {
                    connection.Open();
                    using (var adapter = new NpgsqlDataAdapter(command))
                    {
                        adapter.Fill(result);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка получения данных: {ex.Message}");
            }

            return result;
        }

        public DataTable GetAllOrganizations()
        {
            var result = new DataTable();

            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                using (var command = new NpgsqlCommand(
                    "SELECT id, full_name_or_organization, phone, discount FROM guests WHERE is_organization = 'TRUE'",
                    connection))
                {
                    connection.Open();
                    using (var adapter = new NpgsqlDataAdapter(command))
                    {
                        adapter.Fill(result);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка получения данных: {ex.Message}");
            }

            return result;
        }

        public DataTable GetAllRooms()
        {
            var result = new DataTable();

            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                using (var command = new NpgsqlCommand(
                    "SELECT number, floor, category, capacity, price_per_day FROM rooms",
                    connection))
                {
                    connection.Open();
                    using (var adapter = new NpgsqlDataAdapter(command))
                    {
                        adapter.Fill(result);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка получения данных: {ex.Message}");
            }

            return result;
        }

        public bool AddGuest(string fullNameOrOrganization, string phone)
        {
            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                using (var command = new NpgsqlCommand(
                    "INSERT INTO guests (full_name_or_organization, phone) VALUES (@fullName, @phone)",
                    connection))
                {
                    command.Parameters.AddWithValue("@fullName", fullNameOrOrganization);
                    command.Parameters.AddWithValue("@phone", phone);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении гостя в БД: {ex.Message}");
                return false;
            }
        }

        public bool AddOrganization(string fullNameOrOrganization, string phone, string discount)
        {
            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                using (var command = new NpgsqlCommand(
                    "INSERT INTO guests (full_name_or_organization, phone, discount, is_organization) VALUES (@fullName, @phone, @discount, TRUE)",
                    connection))
                {
                    command.Parameters.AddWithValue("@fullName", fullNameOrOrganization);
                    command.Parameters.AddWithValue("@phone", phone);
                    command.Parameters.AddWithValue("@discount", discount);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении гостя в БД: {ex.Message}");
                return false;
            }
        }


        public RoomInfo GetRoomInfo(int roomNumber)
        {
            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT floor, capacity, category FROM rooms WHERE room_number = @roomNumber";

                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@roomNumber", roomNumber);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new RoomInfo
                                {
                                    Floor = reader.GetInt32(0),
                                    Capacity = reader.GetInt32(1),
                                    Category = reader.GetString(2)
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка получения информации о номере: {ex.Message}");
            }
            return null;
        }

        /// <summary>
        /// Получает информацию о текущем бронировании номера
        /// </summary>
        public BookingInfo GetCurrentBookingInfo(int roomNumber)
        {
            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                SELECT b.id, g.full_name_or_organization, g.phone, b.start_date, b.end_date 
                FROM bookings b
                JOIN guests g ON b.guest_id = g.id
                WHERE b.room_number = @roomNumber 
                AND CURRENT_DATE BETWEEN b.start_date AND b.end_date";

                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@roomNumber", roomNumber);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new BookingInfo
                                {
                                    BookingId = reader.GetInt32(0),
                                    GuestName = reader.GetString(1),
                                    GuestPhone = reader.GetString(2),
                                    StartDate = reader.GetDateTime(3),
                                    EndDate = reader.GetDateTime(4)
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка получения информации о бронировании: {ex.Message}");
            }
            return null;
        }

        /// <summary>
        /// Получает список услуг для бронирования
        /// </summary>
        public List<string> GetBookingServices(int bookingId)
        {
            var services = new List<string>();

            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                SELECT s.service_name 
                FROM bookings b
                JOIN UNNEST(b.services) AS service_id ON true
                JOIN services s ON s.id = service_id
                WHERE b.id = @bookingId";

                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@bookingId", bookingId);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                services.Add(reader.GetString(0));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка получения списка услуг: {ex.Message}");
            }
            return services;
        }


    }
}*/