using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using System.Diagnostics;

namespace DSA_SuperMarket_Management_System
{
    public partial class FormItemSort : Form
    {
        private BinarySearchTree<Item> itemBST;
        private sLinkedList<Item> itemList;
        private DArray<Item> itemArray;
        private string connectionString = "Data Source=supermarket.db;Version=3;";
        private ComboBox comboBoxSortAlgorithm = new ComboBox();
        private ComboBox comboBoxSortColumn = new ComboBox();

        public FormItemSort(BinarySearchTree<Item> bst, sLinkedList<Item> list, DArray<Item> array)
        {
            InitializeComponent();
            itemBST = bst;
            itemList = list;
            itemArray = array;

            
            comboBox1.Items.Add("QuickSort");
            comboBox1.Items.Add("MergeSort");
            comboBox1.Items.Add("InsertionSort");
            comboBox1.Items.Add("SelectionSort");
            comboBox1.Items.Add("BubbleSort");

            
            comboBox2.Items.Add("ItemName");
            comboBox2.Items.Add("ItemCode");
            comboBox2.Items.Add("Category");
            comboBox2.Items.Add("ExpiryDate");
            comboBox2.Items.Add("ManufactureDate");
            comboBox2.Items.Add("GrossAmount");
            comboBox2.Items.Add("NetAmount");
            comboBox2.Items.Add("Quantity");
        }




        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null || comboBox2.SelectedItem == null)
            {
                MessageBox.Show("Please select a sorting algorithm and column.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string selectedAlgorithm = comboBox1.SelectedItem.ToString();
            string selectedColumn = comboBox2.SelectedItem.ToString();

            // Select Data Structure to Sort
            List<Item> items = new List<Item>();

            switch (comboBox3.SelectedItem.ToString())
            {
                case "Dynamic Array":
                    items = ConvertDArrayToList(itemArray);
                    break;
                case "Linked List":
                    items = ConvertLinkedListToList(itemList);
                    break;
                case "Binary Search Tree":
                    items = ConvertBSTToList(itemBST);
                    break;
                default:
                    MessageBox.Show("Please select a valid data structure.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
            }

            // Delete existing records from the database
            DeleteAllItemsFromDatabase();

            // Perform Sorting
            Stopwatch stopwatch = Stopwatch.StartNew(); // Start timing
            SortItems(items, selectedAlgorithm, selectedColumn);
            stopwatch.Stop(); // Stop timing

            // Update Data Structures and Database
            UpdateDataStructures(items);
            UpdateDatabase(items);

            MessageBox.Show($"Sorting Completed!\nTime Taken: {stopwatch.ElapsedMilliseconds} ms", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void DeleteAllItemsFromDatabase()
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string deleteQuery = "DELETE FROM Items"; // Delete all data in the Items table
                using (SQLiteCommand cmd = new SQLiteCommand(deleteQuery, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void UpdateDatabase(List<Item> sortedItems)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (SQLiteTransaction transaction = conn.BeginTransaction())
                {
                    string insertQuery = "INSERT OR REPLACE INTO Items (ItemName, ItemCode, Category, ExpiryDate, ManufactureDate, GrossAmount, NetAmount, Quantity) " +
                                         "VALUES (@name, @code, @category, @expiry, @manufacture, @gross, @net, @quantity)";

                    using (SQLiteCommand insertCmd = new SQLiteCommand(insertQuery, conn))
                    {
                        foreach (var item in sortedItems)
                        {
                            insertCmd.Parameters.Clear();
                            insertCmd.Parameters.AddWithValue("@name", item.ItemName);
                            insertCmd.Parameters.AddWithValue("@code", item.ItemCode); 
                            insertCmd.Parameters.AddWithValue("@category", item.Category);
                            insertCmd.Parameters.AddWithValue("@expiry", item.ExpiryDate);
                            insertCmd.Parameters.AddWithValue("@manufacture", item.ManufactureDate);
                            insertCmd.Parameters.AddWithValue("@gross", item.GrossAmount);
                            insertCmd.Parameters.AddWithValue("@net", item.NetAmount);
                            insertCmd.Parameters.AddWithValue("@quantity", item.Quantity);

                            insertCmd.ExecuteNonQuery();
                        }
                    }

                    transaction.Commit();
                }
            }
        }


        private void SortWithKey<TValue>(List<Item> items, string algorithm, Func<Item, TValue> keySelector) where TValue : IComparable<TValue>
        {
            switch (algorithm)
            {
                case "QuickSort":
                    new QuickSort().Sort(items, keySelector);
                    break;
                case "MergeSort":
                    new MergeSort().Sort(items, keySelector);
                    break;
                case "InsertionSort":
                    new InsertionSort().Sort(items, keySelector);
                    break;
                case "SelectionSort":
                    new SelectionSort().Sort(items, keySelector);
                    break;
                case "BubbleSort":
                    new BubbleSort().Sort(items, keySelector);
                    break;
            }
        }

        private void SortItems(List<Item> items, string algorithm, string column)
        {
            // Get the correct sorting key type dynamically
            switch (column)
            {
                case "Id":
                    SortWithKey(items, algorithm, item => item.Id);
                    break;
                case "Item Name":
                    SortWithKey(items, algorithm, item => item.ItemName);
                    break;
                case "Item Code":
                    SortWithKey(items, algorithm, item => item.ItemCode);
                    break;
                case "Category":
                    SortWithKey(items, algorithm, item => item.Category);
                    break;
                case "Quantity":
                    SortWithKey(items, algorithm, item => item.Quantity);
                    break;
                case "Expiry Date":
                    SortWithKey(items, algorithm, item => DateTime.Parse(item.ExpiryDate));
                    break;
                case "Manufacture Date":
                    SortWithKey(items, algorithm, item => DateTime.Parse(item.ManufactureDate));
                    break;
                case "Gross Amount":
                    SortWithKey(items, algorithm, item => item.GrossAmount);
                    break;
                case "Net Amount":
                    SortWithKey(items, algorithm, item => item.NetAmount);
                    break;
                default:
                    MessageBox.Show("Invalid column selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
            }
        }




        private IComparable GetColumnValue(Item item, string column)
        {
            return column switch
            {
                "ItemName" => item.ItemName,
                "ItemCode" => item.ItemCode,
                "Category" => item.Category,
                "ExpiryDate" => DateTime.Parse(item.ExpiryDate),
                "ManufactureDate" => DateTime.Parse(item.ManufactureDate),
                "GrossAmount" => item.GrossAmount,
                "NetAmount" => item.NetAmount,
                "Quantity" => item.Quantity,
                _ => throw new ArgumentException("Invalid column")
            };
        }

        private void FormItemSort_Load(object sender, EventArgs e)
        {
            // Initialize sorting options and column options when form loads
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(new string[] { "QuickSort", "MergeSort", "InsertionSort", "SelectionSort", "BubbleSort" });

            comboBox2.Items.Clear();
            comboBox2.Items.AddRange(new string[] { "Item Name", "Item Code", "Category", "Expiry Date", "Manufacture Date", "Gross Amount", "Net Amount", "Quantity" });

            comboBox1.SelectedIndex = 0; // Default selection
            comboBox2.SelectedIndex = 0; // Default selection
        }

        private void UpdateDataStructures(List<Item> sortedItems)
        {
            switch (comboBox3.SelectedItem.ToString())
            {
                case "Dynamic Array":
                    itemArray = new DArray<Item>();
                    foreach (var item in sortedItems)
                    {
                        itemArray.Add(item);
                    }
                    break;

                case "Linked List":
                    itemList = new sLinkedList<Item>();
                    foreach (var item in sortedItems)
                    {
                        itemList.AddLast(item);
                    }
                    break;

                case "Binary Search Tree":
                    itemBST = new BinarySearchTree<Item>();
                    foreach (var item in sortedItems)
                    {
                        itemBST.InsertKey(item);
                    }
                    break;
            }

            // Ensure sorted data is copied to all data structures
            foreach (var item in sortedItems)
            {
                itemArray.Add(item);
                itemList.AddLast(item);
                itemBST.InsertKey(item);
            }
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
        private List<Item> ConvertDArrayToList(DArray<Item> array)
        {
            List<Item> list = new List<Item>();
            for (int i = 0; i < array.Count; i++)
            {
                list.Add(array.GetAt(i));
            }
            return list;
        }
        private List<Item> ConvertLinkedListToList(sLinkedList<Item> linkedList)
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
        private List<Item> ConvertBSTToList(BinarySearchTree<Item> bst)
        {
            return bst.GetSortedList(); 
        }


        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            //button1_Click(sender, e);
        }
    }
}