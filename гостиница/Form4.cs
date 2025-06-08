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

    public partial class Form4 : Form
    {
        private readonly Database db;
        public Form4()
        {
            InitializeComponent();
            db = new Database("Host=46.160.139.91;Port=5432;Database=hotel;Username=postgres123;Password=root");
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Получаем данные из текстовых полей
                string fullName = full_name.Text.Trim();
                string phone = number_phone.Text.Trim();

                // Проверяем, что обязательные поля заполнены
                if (string.IsNullOrEmpty(fullName))
                {
                    MessageBox.Show("Пожалуйста, введите ФИО или наименование организации");
                    full_name.Focus(); // Устанавливаем фокус на поле
                    return;
                }

                // Валидация номера телефона
                if (string.IsNullOrEmpty(phone) || !System.Text.RegularExpressions.Regex.IsMatch(phone, @"^\+?[0-9\s\-\(\)]{7,}$"))
                {
                    MessageBox.Show("Пожалуйста, введите корректный номер телефона");
                    number_phone.Focus();
                    return;
                }


                // Добавляем гостя в базу данных
                bool success = db.AddGuest(fullName, phone);

                if (success)
                {
                    MessageBox.Show("Гость успешно добавлен!");

                    // Очищаем поля после успешного добавления
                    full_name.Text = "";
                    number_phone.Text = "";

                    // Закрываем форму после успешного добавления
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Не удалось добавить гостя");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении гостя: {ex.Message}");
            }
        }
    }
}
