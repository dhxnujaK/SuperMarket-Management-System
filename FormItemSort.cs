using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows.Forms;

namespace DSA_SuperMarket_Management_System
{
    public partial class FormItemSort : Form
    {
        private BinarySearchTree<Item> itemBST;
        private sLinkedList<Item> itemList;
        private DArray<Item> itemArray;
        private string connectionString = "Data Source=supermarket.db;Version=3;";

        public FormItemSort(BinarySearchTree<Item> bst, sLinkedList<Item> list, DArray<Item> array)
        {
            InitializeComponent();
            this.itemBST = bst;
            this.itemList = list;
            this.itemArray = array;

            LoadSortingAlgorithms();
            LoadColumnNames();
        }

        private void FormItemSort_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
        }

        private void LoadSortingAlgorithms()
        {
            comboBox1.Items.Add("Bubble Sort");
            comboBox1.Items.Add("Insertion Sort");
            comboBox1.Items.Add("Merge Sort");
            comboBox1.Items.Add("Quick Sort");
            comboBox1.Items.Add("Selection Sort");
        }

        private void LoadColumnNames()
        {
            comboBox2.Items.Add("Id");
            comboBox2.Items.Add("Item Name");
            comboBox2.Items.Add("Item Code");
            comboBox2.Items.Add("Quantity");
            comboBox2.Items.Add("Expiry Date");
            comboBox2.Items.Add("Manufacture Date");
            comboBox2.Items.Add("Gross Amount");
            comboBox2.Items.Add("Net Amount");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedAlgorithm = comboBox1.SelectedItem?.ToString();
            Console.WriteLine($"Selected Sorting Algorithm: {selectedAlgorithm}");
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedColumn = comboBox2.SelectedItem?.ToString();
            Console.WriteLine($"Selected Sorting Column: {selectedColumn}");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null || comboBox2.SelectedItem == null)
            {
                MessageBox.Show("Please select both a sorting algorithm and a column to sort by.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string selectedAlgorithm = comboBox1.SelectedItem.ToString();
            string selectedColumn = comboBox2.SelectedItem.ToString();

            SortDataStructures(selectedAlgorithm, selectedColumn);
            UpdateDatabase();

            MessageBox.Show($"Sorted using {selectedAlgorithm} by {selectedColumn} and database updated.",
                            "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Close();
        }

        private void SortDataStructures(string algorithm, string selectedColumn)
        {
            List<Item> sortedList = ConvertToList(itemList);

            Comparison<Item> comparison = Item.GetComparison(selectedColumn);

            sortedList.Sort(comparison);

            itemList = new sLinkedList<Item>();
            itemBST = new BinarySearchTree<Item>();
            itemArray = new DArray<Item>();

            foreach (var item in sortedList)
            {
                itemList.AddLast(item);
                itemBST.InsertKey(item);
                itemArray.Add(item);
            }
        }

        private List<Item> ConvertToList(sLinkedList<Item> linkedList)
        {
            List<Item> list = new List<Item>();
            Node<Item>? current = linkedList.Head;
            while (current != null)
            {
                list.Add(current.Data);
                current = current.Next;
            }
            return list;
        }

        private void UpdateDatabase()
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                string deleteQuery = "DELETE FROM Items";
                using (SQLiteCommand deleteCmd = new SQLiteCommand(deleteQuery, conn))
                {
                    deleteCmd.ExecuteNonQuery();
                }

                foreach (var item in ConvertToList(itemList))
                {
                    string insertQuery = "INSERT INTO Items (Id, ItemName, ItemCode, Category, ExpiryDate, ManufactureDate, GrossAmount, NetAmount, Quantity) " +
                                         "VALUES (@Id, @ItemName, @ItemCode, @Category, @ExpiryDate, @ManufactureDate, @GrossAmount, @NetAmount, @Quantity)";

                    using (SQLiteCommand insertCmd = new SQLiteCommand(insertQuery, conn))
                    {
                        insertCmd.Parameters.AddWithValue("@Id", item.Id);
                        insertCmd.Parameters.AddWithValue("@ItemName", item.ItemName);
                        insertCmd.Parameters.AddWithValue("@ItemCode", item.ItemCode);
                        insertCmd.Parameters.AddWithValue("@Category", item.Category);
                        insertCmd.Parameters.AddWithValue("@ExpiryDate", item.ExpiryDate);
                        insertCmd.Parameters.AddWithValue("@ManufactureDate", item.ManufactureDate);
                        insertCmd.Parameters.AddWithValue("@GrossAmount", item.GrossAmount);
                        insertCmd.Parameters.AddWithValue("@NetAmount", item.NetAmount);
                        insertCmd.Parameters.AddWithValue("@Quantity", item.Quantity);

                        insertCmd.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
