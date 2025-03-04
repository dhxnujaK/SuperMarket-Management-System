using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using System.Data.SQLite;
using System.Diagnostics;

namespace DSA_SuperMarket_Management_System
{
    public partial class FormItemEdit : Form
    {
        private BinarySearchTree<Item> itemBST;
        private sLinkedList<Item> itemList;
        private DArray<Item> itemArray;
        private string connectionString = "Data Source=supermarket.db;Version=3;";
        private Item currentItem;

        public FormItemEdit(BinarySearchTree<Item> bst, sLinkedList<Item> list, DArray<Item> array)
        {
            InitializeComponent();
            this.itemBST = bst;
            this.itemList = list;
            this.itemArray = array;
        }

        private void label9_Click(object sender, EventArgs e) { }

        private void textBox6_TextChanged(object sender, EventArgs e) { }


        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string searchCode = textBox6.Text.Trim();
            if (string.IsNullOrEmpty(searchCode))
            {
                MessageBox.Show("Please enter an Item Code to search.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
                  

            string selectedDataStructure = comboBox2.SelectedItem?.ToString();
            Stopwatch stopwatch = Stopwatch.StartNew(); 

            currentItem = null;

            switch (selectedDataStructure)
            {
                case "Binary Search Tree":
                    var bstNode = itemBST.Search(searchCode);
                    if (bstNode != null && bstNode.Data != null)
                    {
                        currentItem = bstNode.Data;
                    }
                    break;

                case "Linked List":
                    currentItem = itemList.Find(x => x.ItemCode.Equals(searchCode, StringComparison.OrdinalIgnoreCase));
                    break;

                case "Dynamic Array":
                    currentItem = itemArray.SearchItem(x => x.ItemCode.Equals(searchCode, StringComparison.OrdinalIgnoreCase));
                    break;

                default:
                    MessageBox.Show("Please select a valid data structure.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
            }

            stopwatch.Stop(); 
            double elapsedTime = stopwatch.ElapsedTicks / (double)Stopwatch.Frequency * 1000; // Convert to milliseconds

            if (currentItem != null)
            {
                textBox5.Text = currentItem.ItemName;
                textBox1.Text = currentItem.ItemCode;
                comboBox1.SelectedItem = currentItem.Category;
                dateTimePicker1.Value = DateTime.Parse(currentItem.ExpiryDate);
                dateTimePicker2.Value = DateTime.Parse(currentItem.ManufactureDate);
                textBox2.Text = currentItem.GrossAmount.ToString();
                textBox3.Text = currentItem.NetAmount.ToString();
                textBox4.Text = currentItem.Quantity.ToString();

                MessageBox.Show($"Item found in {selectedDataStructure}!\nTime Taken: {elapsedTime:F6} ms",
                    "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Item not found!", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        }

       

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click_1(object sender, EventArgs e)
        {
            if (currentItem == null)
            {
                MessageBox.Show("No item selected for update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string itemName = textBox5.Text.Trim();
            string itemCode = textBox1.Text.Trim();
            string category = comboBox1.SelectedItem?.ToString();
            string expiryDate = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            string manufactureDate = dateTimePicker2.Value.ToString("yyyy-MM-dd");
            double grossAmount;
            double netAmount;
            int quantity;

            if (string.IsNullOrEmpty(itemName) || string.IsNullOrEmpty(itemCode) || string.IsNullOrEmpty(category))
            {
                MessageBox.Show("Please fill in all required fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!double.TryParse(textBox2.Text, out grossAmount) ||
                !double.TryParse(textBox3.Text, out netAmount) ||
                !int.TryParse(textBox4.Text, out quantity))
            {
                MessageBox.Show("Invalid numeric values. Please enter valid numbers.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Item updatedItem = new Item
            {
                ItemName = itemName,
                ItemCode = itemCode,
                Category = category,
                ExpiryDate = expiryDate,
                ManufactureDate = manufactureDate,
                GrossAmount = grossAmount,
                NetAmount = netAmount,
                Quantity = quantity
            };

            string selectedDataStructure = comboBox2.SelectedItem?.ToString();
            Stopwatch stopwatch = Stopwatch.StartNew(); 

            
            switch (selectedDataStructure)
            {
                case "Binary Search Tree":
                    itemBST.Delete(currentItem);
                    itemBST.InsertKey(updatedItem);
                    break;

                case "Linked List":
                    itemList.Update(x => x.ItemCode.Equals(itemCode, StringComparison.OrdinalIgnoreCase), updatedItem);
                    break;

                case "Dynamic Array":
                    int index = itemArray.Find(x => x.ItemCode.Equals(itemCode, StringComparison.OrdinalIgnoreCase));
                    if (index != -1)
                    {
                        itemArray.Update(index, updatedItem);
                    }
                    break;

                default:
                    MessageBox.Show("Please select a valid data structure.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
            }

            stopwatch.Stop(); 
            double elapsedTime = stopwatch.ElapsedTicks / (double)Stopwatch.Frequency * 1000;

            
            itemBST.Delete(currentItem);
            itemBST.InsertKey(updatedItem);
            itemList.Update(x => x.ItemCode.Equals(itemCode, StringComparison.OrdinalIgnoreCase), updatedItem);
            int idx = itemArray.Find(x => x.ItemCode.Equals(itemCode, StringComparison.OrdinalIgnoreCase));
            if (idx != -1)
            {
                itemArray.Update(idx, updatedItem);
            }

            UpdateDatabase(updatedItem);

            MessageBox.Show($"Item updated successfully!\nTime Taken for {selectedDataStructure}: {elapsedTime:F6} ms",
                "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void UpdateDatabase(Item updatedItem)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE Items SET ItemName = @ItemName, Category = @Category, ExpiryDate = @ExpiryDate, " +
                               "ManufactureDate = @ManufactureDate, GrossAmount = @GrossAmount, NetAmount = @NetAmount, " +
                               "Quantity = @Quantity WHERE ItemCode = @ItemCode";

                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ItemName", updatedItem.ItemName);
                    cmd.Parameters.AddWithValue("@Category", updatedItem.Category);
                    cmd.Parameters.AddWithValue("@ExpiryDate", updatedItem.ExpiryDate);
                    cmd.Parameters.AddWithValue("@ManufactureDate", updatedItem.ManufactureDate);
                    cmd.Parameters.AddWithValue("@GrossAmount", updatedItem.GrossAmount);
                    cmd.Parameters.AddWithValue("@NetAmount", updatedItem.NetAmount);
                    cmd.Parameters.AddWithValue("@Quantity", updatedItem.Quantity);
                    cmd.Parameters.AddWithValue("@ItemCode", updatedItem.ItemCode);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {

            if (currentItem == null)
            {
                MessageBox.Show("No item selected to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Are you sure you want to delete this item?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                string selectedDataStructure = comboBox2.SelectedItem?.ToString();
                Stopwatch stopwatch = Stopwatch.StartNew(); 

                
                switch (selectedDataStructure)
                {
                    case "Binary Search Tree":
                        itemBST.Delete(currentItem);
                        break;

                    case "Linked List":
                        itemList.Remove(x => x.ItemCode.Equals(currentItem.ItemCode, StringComparison.OrdinalIgnoreCase));
                        break;

                    case "Dynamic Array":
                        int index = itemArray.Find(x => x.ItemCode.Equals(currentItem.ItemCode, StringComparison.OrdinalIgnoreCase));
                        if (index != -1)
                        {
                            itemArray.RemoveAt(index);
                        }
                        break;

                    default:
                        MessageBox.Show("Please select a valid data structure.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                }

                stopwatch.Stop();
                double elapsedTime = stopwatch.ElapsedTicks / (double)Stopwatch.Frequency * 1000;

                
                itemBST.Delete(currentItem);
                itemList.Remove(x => x.ItemCode.Equals(currentItem.ItemCode, StringComparison.OrdinalIgnoreCase));
                int idx = itemArray.Find(x => x.ItemCode.Equals(currentItem.ItemCode, StringComparison.OrdinalIgnoreCase));
                if (idx != -1)
                {
                    itemArray.RemoveAt(idx);
                }

                
                DeleteFromDatabase(currentItem.ItemCode);

                MessageBox.Show($"Item deleted successfully!\nTime Taken for {selectedDataStructure}: {elapsedTime:F6} ms",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }
        private void DeleteFromDatabase(string itemCode)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Items WHERE ItemCode = @ItemCode";

                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ItemCode", itemCode);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void FormItemEdit_Load(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

