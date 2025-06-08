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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            textBoxPassword.PasswordChar = '●'; // скрытие пароля
        }

        private void buttonLogin_Click(object sender, EventArgs e)
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
                using (var connection = new NpgsqlConnection("Host=localhost;Database=hotel;Username=postgres;Password=root"))
                {
                    connection.Open();

                    string query = @"
                        SELECT u.full_name, r.role_name
                        FROM users u
                        JOIN roles r ON u.role_id = r.id
                        WHERE u.login = @login AND u.password = @password";

                    using (var cmd = new NpgsqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@login", login);
                        cmd.Parameters.AddWithValue("@password", password);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Успешный вход
                                SessionUser.FullName = reader.GetString(0);
                                SessionUser.RoleName = reader.GetString(1);

                                /*Form1 mainForm = new Form1();
                                this.Hide();
                                mainForm.Show();*/
                                this.DialogResult = DialogResult.OK;
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Неверный логин или пароль.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка подключения: " + ex.Message);
            }
        }
    }
}
