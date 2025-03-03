using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace DSA_SuperMarket_Management_System
{
    public partial class FormItem : Form
    {
        private string connectionString = "Data Source=supermarket.db;Version=3;";
        private Guna2ShadowForm shadowEffect;
        private Guna2Button toggleDarkMode;
        private bool isDarkMode = false;

        // Data structures
        private BinarySearchTree<Item> itemBST = new BinarySearchTree<Item>();
        private sLinkedList<Item> itemList = new sLinkedList<Item>();
        private DArray<Item> itemArray = new DArray<Item>();

        public FormItem()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 240, 240);

            ApplyShadow();
            CustomizeDataGridView();
            CustomizeUI();
            LoadItemsToGrid();
            LoadItemsToDataStructures();
        }

        private void ApplyShadow()
        {
            shadowEffect = new Guna2ShadowForm();
            shadowEffect.SetShadowForm(this);
        }

        private void CustomizeDataGridView()
        {
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dataGridView1.DefaultCellStyle.BackColor = Color.WhiteSmoke;
            dataGridView1.DefaultCellStyle.ForeColor = Color.Black;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.DarkBlue;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void CustomizeUI()
        {
            toggleDarkMode = new Guna2Button
            {
                Text = "☾",
                ForeColor = Color.White,
                Width = 40,
                Height = 40,
                Location = new Point(10, 10),
                BorderRadius = 5
            };
            toggleDarkMode.Click += ToggleDarkMode;
            this.Controls.Add(toggleDarkMode);

            button1.BackColor = Color.Navy;
            button1.ForeColor = Color.White;
            button1.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;

            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is TextBox txt)
                {
                    txt.Font = new Font("Segoe UI", 10);
                    txt.BackColor = Color.White;
                    txt.ForeColor = Color.Black;
                    txt.BorderStyle = BorderStyle.FixedSingle;
                }
                else if (ctrl is Label lbl)
                {
                    lbl.ForeColor = Color.Black;
                }
            }
        }

        private void ToggleDarkMode(object sender, EventArgs e)
        {
            isDarkMode = !isDarkMode;

            this.BackColor = isDarkMode ? Color.FromArgb(30, 30, 30) : Color.FromArgb(240, 240, 240);
            dataGridView1.BackgroundColor = isDarkMode ? Color.FromArgb(40, 40, 40) : Color.WhiteSmoke;
            dataGridView1.DefaultCellStyle.BackColor = isDarkMode ? Color.FromArgb(50, 50, 50) : Color.WhiteSmoke;
            dataGridView1.DefaultCellStyle.ForeColor = isDarkMode ? Color.White : Color.Black;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = isDarkMode ? Color.Black : Color.Navy;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = isDarkMode ? Color.White : Color.White;

            button1.BackColor = isDarkMode ? Color.FromArgb(50, 50, 80) : Color.Navy;
            button1.ForeColor = isDarkMode ? Color.White : Color.White;

            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is TextBox txt)
                {
                    txt.BackColor = isDarkMode ? Color.FromArgb(50, 50, 50) : Color.White;
                    txt.ForeColor = isDarkMode ? Color.White : Color.Black;
                }
                else if (ctrl is Label lbl)
                {
                    lbl.ForeColor = isDarkMode ? Color.White : Color.Black;
                }
            }

            toggleDarkMode.Text = isDarkMode ? "☼" : "☾";
        }
        private void UpdateDataStructures(List<Item> items)
        {
            itemBST = new BinarySearchTree<Item>();
            itemList = new sLinkedList<Item>();
            itemArray = new DArray<Item>();

            foreach (var item in items)
            {
                itemBST.InsertKey(item);
                itemList.AddLast(item);
                itemArray.Add(item);
            }
        }

        private void LoadItemsToDataStructures()
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT ItemName, ItemCode, Category, ExpiryDate, ManufactureDate, GrossAmount, NetAmount, Quantity FROM Items";

                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Item item = new Item
                            {
                                ItemName = reader["ItemName"].ToString(),
                                ItemCode = reader["ItemCode"].ToString(),
                                Category = reader["Category"].ToString(),
                                ExpiryDate = reader["ExpiryDate"].ToString(),
                                ManufactureDate = reader["ManufactureDate"].ToString(),
                                GrossAmount = Convert.ToDouble(reader["GrossAmount"]),
                                NetAmount = Convert.ToDouble(reader["NetAmount"]),
                                Quantity = Convert.ToInt32(reader["Quantity"])
                            };

                            itemBST.InsertKey(item);
                            itemList.AddLast(item);
                            itemArray.Add(item);
                        }
                    }
                }
            }
        }


        public void LoadItemsToGrid()
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT Id, ItemName, ItemCode, Category, ExpiryDate, ManufactureDate, GrossAmount, NetAmount, Quantity FROM Items";

                using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                    dataGridView1.Refresh();
                }
            }
        }

            private void button1_Click(object sender, EventArgs e)
        {
            string itemName = textBox5.Text;
            string itemCode = textBox1.Text;
            string category = comboBox1.SelectedItem?.ToString();
            string expiryDate = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            string manufactureDate = dateTimePicker2.Value.ToString("yyyy-MM-dd");
            double grossAmount;
            double netAmount;
            int quantity;

            if (string.IsNullOrEmpty(itemName) || string.IsNullOrEmpty(itemCode) || string.IsNullOrEmpty(category))
            {
                MessageBox.Show("Please fill all required fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!double.TryParse(textBox2.Text, out grossAmount) ||
                !double.TryParse(textBox3.Text, out netAmount) ||
                !int.TryParse(textBox4.Text, out quantity))
            {
                MessageBox.Show("Invalid numeric values. Please enter correct values.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ItemDatabase.InsertItem(itemName, itemCode, category, expiryDate, manufactureDate, grossAmount, netAmount, quantity);
            MessageBox.Show("Item added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            LoadItemsToGrid();

            textBox5.Clear();
            textBox1.Clear();
            comboBox1.SelectedIndex = -1;
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
        }

        /*private void FormItem_Load(object sender, EventArgs e)
        {
            LoadItemsToGrid();
        }*/
        private async void FormItem_Load(object sender, EventArgs e)
        {
            FormLoading loadingScreen = new FormLoading();
            loadingScreen.Show();

            await Task.Run(() =>
            {
                // Load data in a background thread
                DataTable dt = LoadItemsFromDatabase();
                List<Item> items = LoadItemsToDataStructuresInBackground();

                // Ensure UI updates happen on the main thread
                this.Invoke(new Action(() =>
                {
                    dataGridView1.DataSource = dt;
                    dataGridView1.Refresh();
                    UpdateDataStructures(items);
                    loadingScreen.Close();
                }));
            });
        }
        private List<Item> LoadItemsToDataStructuresInBackground()
        {
            List<Item> items = new List<Item>();
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT ItemName, ItemCode, Category, ExpiryDate, ManufactureDate, GrossAmount, NetAmount, Quantity FROM Items";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Item item = new Item
                            {
                                ItemName = reader["ItemName"].ToString(),
                                ItemCode = reader["ItemCode"].ToString(),
                                Category = reader["Category"].ToString(),
                                ExpiryDate = reader["ExpiryDate"].ToString(),
                                ManufactureDate = reader["ManufactureDate"].ToString(),
                                GrossAmount = Convert.ToDouble(reader["GrossAmount"]),
                                NetAmount = Convert.ToDouble(reader["NetAmount"]),
                                Quantity = Convert.ToInt32(reader["Quantity"])
                            };
                            items.Add(item);
                        }
                    }
                }
            }
            return items;
        }

        private DataTable LoadItemsFromDatabase()
        {
            DataTable dt = new DataTable();
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT Id, ItemName, ItemCode, Category, ExpiryDate, ManufactureDate, GrossAmount, NetAmount, Quantity FROM Items";
                using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conn))
                {
                    adapter.Fill(dt);
                }
            }
            return dt;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        // All event handlers retained
        private void label1_Click(object sender, EventArgs e) { }
        private void label5_Click(object sender, EventArgs e) { }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void dateTimePicker2_ValueChanged(object sender, EventArgs e) { }
        private void textBox2_TextChanged(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) { }
        private void label6_Click(object sender, EventArgs e) { }
        private void label1_Click_1(object sender, EventArgs e) { }
        private void textBox4_TextChanged(object sender, EventArgs e) { }
        private void textBox3_TextChanged(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void label7_Click(object sender, EventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void label8_Click(object sender, EventArgs e) { }
        private void textBox5_TextChanged(object sender, EventArgs e) { }

        private void button2_Click(object sender, EventArgs e)
        {
            FormItemEdit formItemEdit = new FormItemEdit(itemBST, itemList, itemArray);
            formItemEdit.ShowDialog();
            LoadItemsToGrid();
            LoadItemsToDataStructures();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormItemSort formItemSort = new FormItemSort(itemBST, itemList, itemArray);
            formItemSort.ShowDialog();
            LoadItemsToGrid();
            LoadItemsToDataStructures();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
