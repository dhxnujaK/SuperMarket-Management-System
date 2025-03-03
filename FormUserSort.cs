using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;

namespace DSA_SuperMarket_Management_System
{
    public partial class FormUserSort : Form
    {
        private string connectionString = "Data Source=UserDatabase.db;Version=3;";
        private BinarySearchTree<User> userBST = new BinarySearchTree<User>();
        private sLinkedList<User> userList = new sLinkedList<User>();
        private DArray<User> userArray = new DArray<User>();

        public FormUserSort()
        {
            InitializeComponent();

            // Populate Sorting Algorithms
            comboBox1.Items.Add("QuickSort");
            comboBox1.Items.Add("MergeSort");
            comboBox1.Items.Add("InsertionSort");
            comboBox1.Items.Add("SelectionSort");
            comboBox1.Items.Add("BubbleSort");

            // Populate Column Names
            comboBox2.Items.Add("Id");
            comboBox2.Items.Add("Name");
            comboBox2.Items.Add("NIC");
            comboBox2.Items.Add("Contact Number");

            // Populate Data Structures
            comboBox3.Items.Add("Dynamic Array");
            comboBox3.Items.Add("Linked List");
            comboBox3.Items.Add("Binary Search Tree");

            LoadUsersIntoDataStructures();
        }

        private void LoadUsersIntoDataStructures()
        {
            List<User> users = LoadUsersFromDatabase();

            foreach (var user in users)
            {
                userBST.InsertKey(user);
                userList.AddLast(user);
                userArray.Add(user);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null || comboBox2.SelectedItem == null || comboBox3.SelectedItem == null)
            {
                MessageBox.Show("Please select a sorting algorithm, column, and data structure.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string selectedAlgorithm = comboBox1.SelectedItem.ToString();
            string selectedColumn = comboBox2.SelectedItem.ToString();
            string selectedDataStructure = comboBox3.SelectedItem.ToString();

            List<User> users = selectedDataStructure switch
            {
                "Dynamic Array" => ConvertDArrayToList(userArray),
                "Linked List" => ConvertLinkedListToList(userList),
                "Binary Search Tree" => ConvertBSTToList(userBST),
                _ => new List<User>()
            };

            Stopwatch stopwatch = Stopwatch.StartNew();
            SortUsers(users, selectedAlgorithm, selectedColumn);
            stopwatch.Stop();

            UpdateDataStructures(users);
            UpdateDatabase(users);

            MessageBox.Show($"Sorting Completed!\nTime Taken: {stopwatch.ElapsedMilliseconds} ms", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void SortWithKey<TValue>(List<User> users, string algorithm, Func<User, TValue> keySelector) where TValue : IComparable<TValue>
        {
            switch (algorithm)
            {
                case "QuickSort":
                    new QuickSort().Sort(users, keySelector);
                    break;
                case "MergeSort":
                    new MergeSort().Sort(users, keySelector);
                    break;
                case "InsertionSort":
                    new InsertionSort().Sort(users, keySelector);
                    break;
                case "SelectionSort":
                    new SelectionSort().Sort(users, keySelector);
                    break;
                case "BubbleSort":
                    new BubbleSort().Sort(users, keySelector);
                    break;
            }
        }

        private void SortUsers(List<User> users, string algorithm, string column)
        {
            switch (column)
            {
                case "Id":
                    SortWithKey(users, algorithm, user => user.Id);
                    break;
                case "Name":
                    SortWithKey(users, algorithm, user => user.Name);
                    break;
                case "NIC":
                    SortWithKey(users, algorithm, user => user.NIC);
                    break;
                case "Contact Number":
                    SortWithKey(users, algorithm, user => user.ContactNumber);
                    break;
                default:
                    MessageBox.Show("Invalid column selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
            }
        }

        private void UpdateDataStructures(List<User> sortedUsers)
        {
            userArray = new DArray<User>();
            userList = new sLinkedList<User>();
            userBST = new BinarySearchTree<User>();

            foreach (var user in sortedUsers)
            {
                userArray.Add(user);
                userList.AddLast(user);
                userBST.InsertKey(user);
            }
        }

        private void UpdateDatabase(List<User> sortedUsers)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (SQLiteTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // Clear existing table to reinsert sorted data
                        string deleteQuery = "DELETE FROM Users";
                        using (SQLiteCommand deleteCmd = new SQLiteCommand(deleteQuery, conn))
                        {
                            deleteCmd.ExecuteNonQuery();
                        }

                        // Reinsert users in sorted order
                        string insertQuery = "INSERT INTO Users (Id, Name, NIC, ContactNumber) VALUES (@id, @name, @nic, @contact)";
                        using (SQLiteCommand insertCmd = new SQLiteCommand(insertQuery, conn))
                        {
                            foreach (var user in sortedUsers)
                            {
                                insertCmd.Parameters.Clear();
                                insertCmd.Parameters.AddWithValue("@id", user.Id);
                                insertCmd.Parameters.AddWithValue("@name", user.Name);
                                insertCmd.Parameters.AddWithValue("@nic", user.NIC);
                                insertCmd.Parameters.AddWithValue("@contact", user.ContactNumber);
                                insertCmd.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                        MessageBox.Show("Database updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Database Update Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }


        private List<User> LoadUsersFromDatabase()
        {
            List<User> users = new List<User>();
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT Id, Name, NIC, ContactNumber FROM Users";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(new User
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            NIC = reader.GetString(2),
                            ContactNumber = reader.GetString(3)
                        });
                    }
                }
            }
            return users;
        }

        private List<User> ConvertDArrayToList(DArray<User> array)
        {
            List<User> list = new List<User>();
            for (int i = 0; i < array.Count; i++)
            {
                list.Add(array.GetAt(i));
            }
            return list;
        }

        private List<User> ConvertLinkedListToList(sLinkedList<User> linkedList)
        {
            List<User> list = new List<User>();
            Node<User>? current = linkedList.Head;
            while (current != null)
            {
                list.Add(current.Data);
                current = current.Next;
            }
            return list;
        }

        private List<User> ConvertBSTToList(BinarySearchTree<User> bst)
        {
            return bst.GetSortedList();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) { }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e) { }
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e) { }
        private void FormUserSort_Load(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
    }

}
