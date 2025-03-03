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
        private BinarySearchTree<User> userBST;
        private sLinkedList<User> userList;
        private DArray<User> userArray;

        public FormUserSort(BinarySearchTree<User> bst, sLinkedList<User> list, DArray<User> array)
        {
            InitializeComponent();
            userBST = bst;
            userList = list;
            userArray = array;

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
            comboBox2.Items.Add("ContactNumber");

            // Populate Data Structures
            comboBox3.Items.Add("Dynamic Array");
            comboBox3.Items.Add("Linked List");
            comboBox3.Items.Add("Binary Search Tree");
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

        private void SortUsers(List<User> users, string algorithm, string column)
        {
            switch (column)
            {
                case "Id":
                    users.Sort((x, y) => x.Id.CompareTo(y.Id));
                    break;
                case "Name":
                    users.Sort((x, y) => x.Name.CompareTo(y.Name));
                    break;
                case "NIC":
                    users.Sort((x, y) => x.NIC.CompareTo(y.NIC));
                    break;
                case "ContactNumber":
                    users.Sort((x, y) => x.ContactNumber.CompareTo(y.ContactNumber));
                    break;
            }

            /*//  Debugging: Print the sorted list to console
            Console.WriteLine("Sorted Users:");
            foreach (var user in users)
            {
                Console.WriteLine($"{user.Id} - {user.Name} - {user.NIC} - {user.ContactNumber}");
            }*/
        }


        private void UpdateDataStructures(List<User> sortedUsers)
        {
            // First, clear all existing data structures
            userArray = new DArray<User>();
            userList = new sLinkedList<User>();
            userBST = new BinarySearchTree<User>();

            // Add sorted users back into all the data structures
            foreach (var user in sortedUsers)
            {
                userArray.Add(user);
                userList.AddLast(user);
                userBST.InsertKey(user);
            }

            // 🔹 Ensure that all data structures are updated and reflect the sorted order.
            Console.WriteLine("Data structures updated with sorted data!"); // 🔹 Debugging log
        }



        private void UpdateDatabase(List<User> sortedUsers)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                // 🔹 Ensure PRAGMA runs BEFORE starting the transaction
                using (SQLiteCommand pragmaCmd = new SQLiteCommand("PRAGMA synchronous = FULL;", conn))
                {
                    pragmaCmd.ExecuteNonQuery();
                }

                using (SQLiteTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // 🔹 Clear existing table before inserting sorted data
                        string deleteQuery = "DELETE FROM Users";
                        using (SQLiteCommand deleteCmd = new SQLiteCommand(deleteQuery, conn, transaction))
                        {
                            deleteCmd.ExecuteNonQuery();
                        }

                        // 🔹 Reinsert users in sorted order
                        string insertQuery = "INSERT INTO Users (Id, Name, NIC, ContactNumber) VALUES (@id, @name, @nic, @contact)";
                        using (SQLiteCommand insertCmd = new SQLiteCommand(insertQuery, conn, transaction))
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