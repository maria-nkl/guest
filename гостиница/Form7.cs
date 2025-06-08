using Npgsql;
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
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }

        private void labelStatus_Click(object sender, EventArgs e)
        {

        }

        private void Form7_Load(object sender, EventArgs e)
        {
            LoadBookings();
            UpdateStatistics();
        }

        private void LoadBookings()
        {
            using (var conn = new NpgsqlConnection("Host=localhost;Database=hotel;Username=postgres;Password=root"))
            {
                conn.Open();

                string query = @"
                    SELECT 
                        b.room_number, 
                        b.start_date, 
                        b.end_date, 
                        g.full_name_or_organization AS guest_name,
                        b.services,
                        b.total_price
                    FROM bookings b
                    JOIN guests g ON b.guest_id = g.id
                    ORDER BY b.start_date DESC";

                var adapter = new NpgsqlDataAdapter(query, conn);
                var table = new DataTable();
                adapter.Fill(table);
                dataGridViewBookings.DataSource = table;
            }
        }

        private void UpdateStatistics()
        {
            using (var conn = new NpgsqlConnection("Host=localhost;Database=hotel;Username=postgres;Password=root"))
            {
                conn.Open();

                int total = 0;
                int current = 0;
                int upcoming = 0;

                string totalQuery = "SELECT COUNT(*) FROM bookings";
                string currentQuery = "SELECT COUNT(*) FROM bookings WHERE CURRENT_DATE BETWEEN start_date AND end_date";
                string futureQuery = "SELECT COUNT(*) FROM bookings WHERE start_date > CURRENT_DATE";

                using (var cmd = new NpgsqlCommand(totalQuery, conn))
                    total = Convert.ToInt32(cmd.ExecuteScalar());

                using (var cmd = new NpgsqlCommand(currentQuery, conn))
                    current = Convert.ToInt32(cmd.ExecuteScalar());

                using (var cmd = new NpgsqlCommand(futureQuery, conn))
                    upcoming = Convert.ToInt32(cmd.ExecuteScalar());

                labelStatus.Text = $"📋 Всего бронирований: {total}\n" +
                                  $"🟢 Текущих: {current}\n" +
                                  $"🕓 Будущих: {upcoming}";
            }
        }
    }
}
