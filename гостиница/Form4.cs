using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace гостиница
{
    public partial class Form4 : Form
    {
        private static readonly HttpClient client = new HttpClient();
        private const string apiBaseUrl = "https://localhost:7029/api"; // замените при необходимости

        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string fullName = full_name.Text.Trim();
                string phone = number_phone.Text.Trim();

                if (string.IsNullOrEmpty(fullName))
                {
                    MessageBox.Show("Пожалуйста, введите ФИО или наименование организации");
                    full_name.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(phone) || !Regex.IsMatch(phone, @"^\+?[0-9\s\-\(\)]{7,}$"))
                {
                    MessageBox.Show("Пожалуйста, введите корректный номер телефона");
                    number_phone.Focus();
                    return;
                }

                var guest = new Guest
                {
                    FullNameOrOrganization = fullName,
                    Phone = phone,
                    IsOrganization = false
                };

                var json = JsonSerializer.Serialize(guest);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync($"{apiBaseUrl}/Guests", content);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Гость успешно добавлен!");
                    full_name.Text = "";
                    number_phone.Text = "";
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    string error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Ошибка при добавлении: {response.StatusCode}\\n{error}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        public class Guest
        {
            public string FullNameOrOrganization { get; set; }
            public string Phone { get; set; }
            public bool IsOrganization { get; set; }
        }
    }
}