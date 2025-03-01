using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DSA_SuperMarket_Management_System
{
    public partial class FormItem : Form
    {
        private string connectionString = "Data Source=supermarket.db;Version=3;"; 

        public FormItem()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen; 
            CustomizeDataGridView();
            CustomizeUI(); 
            LoadItemsToGrid();
        }

        // ✅ Improve DataGridView Styling
        private void CustomizeDataGridView()
        {
            dataGridView1.BorderStyle = BorderStyle.FixedSingle;
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
            dataGridView1.AllowUserToAddRows = false; // Prevents blank row at bottom
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // ✅ Auto adjust columns
        }

        // ✅ Improve UI Styling (Button & Input Fields)
        private void CustomizeUI()
        {
            button1.BackColor = Color.Navy;
            button1.ForeColor = Color.White;
            button1.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;

            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is TextBox)
                {
                    TextBox txt = (TextBox)ctrl;
                    txt.Font = new Font("Segoe UI", 10);
                    txt.BackColor = Color.White;
                    txt.ForeColor = Color.Black;
                    txt.BorderStyle = BorderStyle.FixedSingle;
                }
            }
        }

        private void LoadItemsToGrid()
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT Id, ItemCode AS 'Item Code', Category, ExpiryDate AS 'Expiry Date', ManufactureDate AS 'Manufacture Date', GrossAmount AS 'Gross Amount', NetAmount AS 'Net Amount', Quantity FROM Items"; // ✅ Better column names

                using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt; 
                    dataGridView1.Refresh(); // ✅ Ensure real-time update
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string itemCode = textBox1.Text;
            string category = comboBox1.SelectedItem?.ToString();
            string expiryDate = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            string manufactureDate = dateTimePicker2.Value.ToString("yyyy-MM-dd");
            double grossAmount;
            double netAmount;
            int quantity;

            // ✅ Validate input
            if (string.IsNullOrEmpty(itemCode) || string.IsNullOrEmpty(category))
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

            // ✅ Insert into database using ItemDatabase class
            ItemDatabase.InsertItem(itemCode, category, expiryDate, manufactureDate, grossAmount, netAmount, quantity);
            MessageBox.Show("Item added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            LoadItemsToGrid(); // ✅ Refresh DataGridView after adding an item

           
            textBox1.Clear();
            comboBox1.SelectedIndex = -1;
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
        }

        private void FormItem_Load(object sender, EventArgs e)
        {
            LoadItemsToGrid(); 
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
