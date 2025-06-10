using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace гостиница
{
    public partial class Form6 : Form
    {
        private static readonly HttpClient client = new HttpClient();

        public Form6()
        {
            InitializeComponent();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            textBoxPassword.PasswordChar = '●'; // скрытие пароля
        }

        private async void buttonLogin_Click(object sender, EventArgs e)
        {
            string login = textBoxLogin.Text.Trim();
            string password = textBoxPassword.Text.Trim();

            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Пожалуйста, введите логин и пароль.");
                return;
            }

            try
            {
                var loginData = new { login = login, password = password };
                string json = JsonSerializer.Serialize(loginData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                string apiUrl = "http://localhost:5000/api/Users/login";

                HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();

                    // API возвращает JSON с именем и ролью
                    var userInfo = JsonSerializer.Deserialize<UserInfo>(responseBody);

                    if (userInfo != null && !string.IsNullOrEmpty(userInfo.fullName))
                    {
                        SessionUser.fullName = userInfo.fullName;
                        SessionUser.roleName = userInfo.roleName;

                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Неверный логин или пароль.");
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка аутентификации: Неверный логин или пароль.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка подключения к API: " + ex.Message);
            }
        }

        private class UserInfo
        {
            public string fullName { get; set; }
            public string roleName { get; set; }
        }
    }
}
