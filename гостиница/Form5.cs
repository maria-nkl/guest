using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace гостиница
{
    public partial class Form5 : Form
    {
        private static readonly HttpClient client = new HttpClient();
        private const string apiBaseUrl = "https://localhost:7029/api";

        public Form5()
        {
            InitializeComponent();
        }

        private async void add_organization_Click(object sender, EventArgs e)
        {
            try
            {
                string fullName = full_name_organization.Text.Trim();
                string phone = number_phone.Text.Trim();
                string discount = sale.Text.Trim();

                if (string.IsNullOrEmpty(fullName))
                {
                    MessageBox.Show("Пожалуйста, введите наименование организации");
                    full_name_organization.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(phone) || !Regex.IsMatch(phone, @"^\+?[0-9\s\-\(\)]{7,}$"))
                {
                    MessageBox.Show("Пожалуйста, введите корректный номер телефона");
                    number_phone.Focus();
                    return;
                }

                if (!string.IsNullOrEmpty(discount) && !discount.EndsWith("%") && discount.ToLower() != "none")
                {
                    MessageBox.Show("Скидка должна быть в формате 'X%' или 'None'");
                    sale.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(discount))
                {
                    discount = "None";
                }

                var organization = new Guest
                {
                    FullNameOrOrganization = fullName,
                    Phone = phone,
                    Discount = discount,
                    IsOrganization = true
                };

                var json = JsonSerializer.Serialize(organization);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync($"{apiBaseUrl}/Guests", content);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Организация успешно добавлена!");
                    full_name_organization.Text = "";
                    number_phone.Text = "";
                    sale.Text = "";
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
            public string Discount { get; set; }
            public bool IsOrganization { get; set; }
        }
    }
}

