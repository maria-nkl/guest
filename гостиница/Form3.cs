using System;
using System.Data;
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
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            ConfigureDataGridView();
            LoadAllGuests();
            labelGuest.Text = "Выбранный гость: ";
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
                HeaderText = "ФИО / Организация",
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

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "discount",
                HeaderText = "Скидка",
                DataPropertyName = "discount",
                Width = 80
            });

            dataGridView1.SelectionChanged += DataGridView1_SelectionChanged;
        }

        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                string guestName = selectedRow.Cells["full_name"].Value?.ToString() ?? "не указано";
                labelGuest.Text = $"Выбранный гость: {guestName}";
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

        private void button1_Click(object sender, EventArgs e)
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
    }
}