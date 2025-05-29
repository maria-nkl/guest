using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace гостиница
{
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
                            discount TEXT DEFAULT 'None'
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
                            price_per_day DECIMAL(10,2) NOT NULL
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
                            services INTEGER[] DEFAULT ARRAY[]::INTEGER[]
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
                        INSERT INTO guests (full_name_or_organization, phone, discount) VALUES 
                        ('Иванов Алексей Петрович', '+79161234567', '5%'),
                        ('ООО Ромашка', '+74951234567', '10%'),
                        ('Сидорова Мария Ивановна', '+79261234567', 'None')", connection))
                    {
                        insertCommand.ExecuteNonQuery();
                    }
                }
            }

            // Проверка и заполнение таблицы Номера
            using (var checkCommand = new NpgsqlCommand("SELECT COUNT(*) FROM rooms", connection))
            {
                if ((long) checkCommand.ExecuteScalar() == 0)
                {
                    using (var insertCommand = new NpgsqlCommand(@"
                        INSERT INTO rooms (number, floor, category, capacity, price_per_day) VALUES 
                        (101, 1, 'Стандарт', 2, 2500.00),
                        (204, 2, 'Комфорт', 2, 3500.00),
                        (303, 3, 'Люкс', 4, 5500.00),
                        (405, 4, 'Президентский', 2, 8500.00)", connection))
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
                        "SELECT room_number, guest_surname FROM rooms WHERE guest_surname ILIKE @search",
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
                    "SELECT id, full_name_or_organization, phone, discount FROM guests",
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

        public bool AddGuest(string fullNameOrOrganization, string phone, string discount)
        {
            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                using (var command = new NpgsqlCommand(
                    "INSERT INTO guests (full_name_or_organization, phone, discount) VALUES (@fullName, @phone, @discount)",
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
    }
}