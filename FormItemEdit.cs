using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using System.Data.SQLite;

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
            DebugPrintDataStructures();

            currentItem = null;

            var bstNode = itemBST.Search(searchCode);
            if (bstNode != null && bstNode.Data != null && bstNode.Data.ItemCode.Equals(searchCode, StringComparison.OrdinalIgnoreCase))
            {
                currentItem = bstNode.Data;
            }

            if (currentItem == null)
            {
                currentItem = itemList.Find(x => x.ItemCode.Equals(searchCode, StringComparison.OrdinalIgnoreCase));
            }

            if (currentItem == null)
            {
                currentItem = itemArray.SearchItem(x => x.ItemCode.Equals(searchCode, StringComparison.OrdinalIgnoreCase));
            }

            if (currentItem != null)
            {
                textBox5.Text = currentItem.Quantity.ToString();     // ✅ Quantity
                textBox1.Text = currentItem.ItemName;          // ✅ Item Name
                textBox2.Text = currentItem.ItemCode;          // ✅ Item Code
                comboBox1.SelectedItem = currentItem.Category; // ✅ Category
                dateTimePicker1.Value = DateTime.Parse(currentItem.ExpiryDate); // ✅ Expiry Date
                dateTimePicker2.Value = DateTime.Parse(currentItem.ManufactureDate); // ✅ Manufacture Date
                textBox3.Text = currentItem.GrossAmount.ToString();  // ✅ Gross Amount
               
                textBox4.Text = currentItem.NetAmount.ToString();    // ✅ Net Amount
            }


            else
            {
                MessageBox.Show("Item not found!", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (currentItem == null)
            {
                MessageBox.Show("No item selected for update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string itemName = textBox1.Text.Trim();
            string itemCode = textBox2.Text.Trim();
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

            if (!double.TryParse(textBox3.Text, out grossAmount) ||
                !double.TryParse(textBox4.Text, out netAmount) ||
                !int.TryParse(textBox5.Text, out quantity))
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

            itemBST.Delete(currentItem);
            itemList.Update(itemCode, updatedItem);

            int index = itemArray.Find(x => x.ItemCode.Equals(itemCode, StringComparison.OrdinalIgnoreCase));
            if (index != -1)
            {
                itemArray.Update(index, updatedItem);
            }

            itemBST.InsertKey(updatedItem);

            UpdateDatabase(updatedItem);

            MessageBox.Show("Item updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void DebugPrintDataStructures()
        {
            Console.WriteLine("===== Debug: Checking Data Structures =====");

            Console.WriteLine("BST Items:");
            itemBST.PrintTree(); // Make sure `PrintTree()` prints all elements in BST

            Console.WriteLine("Linked List Items:");
            Node<Item> current = itemList.Head;
            while (current != null)
            {
                Console.WriteLine($"Linked List Item: {current.Data.ItemCode}");
                current = current.Next;
            }

            Console.WriteLine("Dynamic Array Items:");
            for (int i = 0; i < itemArray.Count; i++)
            {
                Console.WriteLine($"Dynamic Array Item: {itemArray.GetAt(i).ItemCode}");
            }
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
    }
}
