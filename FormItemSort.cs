using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Windows.Forms;
using Guna.UI2.WinForms;

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

            // Populate Sorting Algorithms
            comboBox1.Items.Add("QuickSort");
            comboBox1.Items.Add("MergeSort");
            comboBox1.Items.Add("InsertionSort");
            comboBox1.Items.Add("SelectionSort");
            comboBox1.Items.Add("BubbleSort");

            // Populate Column Names
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

            // Get data from LinkedList (or another structure)
            List<Item> items = new List<Item>();
            Node<Item> current = itemList.Head;
            while (current != null)
            {
                items.Add(current.Data);
                current = current.Next;
            }

            // Perform Sorting
            SortItems(items, selectedAlgorithm, selectedColumn);

            // Update Data Structures and Database
            UpdateDataStructures(items);
            UpdateDatabase(items);

            MessageBox.Show("Items sorted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
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
            itemBST = new BinarySearchTree<Item>();
            itemList = new sLinkedList<Item>();
            itemArray = new DArray<Item>();

            foreach (var item in sortedItems)
            {
                itemBST.InsertKey(item);
                itemList.AddLast(item);
                itemArray.Add(item);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
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
                            insertCmd.Parameters.AddWithValue("@code", item.ItemCode); // Ensure UNIQUE constraint is respected
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


    }
}