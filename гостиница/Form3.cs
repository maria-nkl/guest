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
    }
}