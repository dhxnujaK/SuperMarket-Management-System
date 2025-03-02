using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace DSA_SuperMarket_Management_System
{
    public partial class FormUser : Form
    {
        private readonly string connectionString = "Data Source=UserDatabase.db;Version=3;";

        // Using generics with User type
        private BinarySearchTree<User> userBST = new BinarySearchTree<User>();
        private sLinkedList<User> userList = new sLinkedList<User>();
        private DArray<User> userArray = new DArray<User>();

        public FormUser()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            CloseRunningInstances();
            InitializeDatabase();
            CustomizeDataGridView();
            CustomizeUI();
            LoadUserData();
        }

        private void CloseRunningInstances()
        {
            string processName = "DSA SuperMarket Management System";
            foreach (var process in Process.GetProcessesByName(processName))
            {
                if (process.Id != Process.GetCurrentProcess().Id)
                {
                    process.Kill();
                    process.WaitForExit();
                }
            }
        }

        private void InitializeDatabase()
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string createTableQuery = @"
                        CREATE TABLE IF NOT EXISTS Users (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT, 
                            Name TEXT NOT NULL, 
                            NIC TEXT UNIQUE NOT NULL, 
                            ContactNumber TEXT NOT NULL)";

                    using (SQLiteCommand command = new SQLiteCommand(createTableQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Database Initialization Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadUserData()
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string selectQuery = "SELECT Id, Name, NIC, ContactNumber FROM Users";

                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(selectQuery, connection))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        if (dataGridView1 != null)
                        {
                            dataGridView1.DataSource = dataTable;
                            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                            dataGridView1.AutoResizeColumns();
                            dataGridView1.Refresh();
                        }

                        // Clearing data structures before reloading
                        userBST = new BinarySearchTree<User>();
                        userList = new sLinkedList<User>();
                        userArray = new DArray<User>();

                        foreach (DataRow row in dataTable.Rows)
                        {
                            int id = Convert.ToInt32(row["Id"]);
                            string name = row["Name"].ToString();
                            string nic = row["NIC"].ToString();
                            string contact = row["ContactNumber"].ToString();

                            User user = new User(id, name, nic, contact);
                            userBST.InsertKey(user);
                            userList.AddLast(user);
                            userArray.Add(user);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error Loading Data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text.Trim();
            string nic = textBox2.Text.Trim();
            string contactNumber = textBox4.Text.Trim();

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(nic) || string.IsNullOrWhiteSpace(contactNumber))
            {
                MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!IsValidNIC(nic))
            {
                MessageBox.Show("Invalid NIC format. Please enter a valid NIC.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string checkNICQuery = "SELECT COUNT(*) FROM Users WHERE NIC = @NIC";
                    using (SQLiteCommand checkCommand = new SQLiteCommand(checkNICQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@NIC", nic);
                        int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                        if (count > 0)
                        {
                            MessageBox.Show("User with this NIC already exists.", "Duplicate NIC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    string insertQuery = "INSERT INTO Users (Name, NIC, ContactNumber) VALUES (@Name, @NIC, @ContactNumber)";
                    using (SQLiteCommand command = new SQLiteCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Name", name);
                        command.Parameters.AddWithValue("@NIC", nic);
                        command.Parameters.AddWithValue("@ContactNumber", contactNumber);
                        command.ExecuteNonQuery();
                    }

                    MessageBox.Show("User saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadUserData();

                    textBox1.Clear();
                    textBox2.Clear();
                    textBox4.Clear();
                }
                catch (SQLiteException ex)
                {
                    if (ex.ErrorCode == (int)SQLiteErrorCode.Constraint)
                        MessageBox.Show("This NIC already exists in the database.", "Duplicate Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                        MessageBox.Show("Database Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unexpected Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool IsValidNIC(string nic)
        {
            return nic.Length == 10 || nic.Length == 12;
        }

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
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void CustomizeUI()
        {
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
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void textBox2_TextChanged(object sender, EventArgs e) { }
        private void textBox4_TextChanged(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void FormUser_Load(object sender, EventArgs e) { }
    }
}
