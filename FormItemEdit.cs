using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace DSA_SuperMarket_Management_System
{
    public partial class FormItemEdit : Form
    {
        private BinarySearchTree<Item> itemBST;
        private sLinkedList<Item> itemList;
        private DArray<Item> itemArray;

        public FormItemEdit(BinarySearchTree<Item> bst, sLinkedList<Item> list, DArray<Item> array)
        {
            InitializeComponent();
            this.itemBST = bst;
            this.itemList = list;
            this.itemArray = array;
        }

        private void label9_Click(object sender, EventArgs e) { }

        private void textBox6_TextChanged(object sender, EventArgs e) { }

        // 🔹 **Search Item by Item Code**
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string searchCode = textBox6.Text.Trim();
            if (string.IsNullOrEmpty(searchCode))
            {
                MessageBox.Show("Please enter an Item Code to search.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Item foundItem = null;

            // ✅ **Search in BST**
            var bstNode = itemBST.Search(searchCode);
            if (bstNode != null)
            {
                foundItem = bstNode.Data;
            }

            // ✅ **Search in Linked List if not found**
            if (foundItem == null)
            {
                foundItem = itemList.Find(x => x.ItemCode == searchCode);
            }

            // ✅ **Search in Dynamic Array if not found**
            if (foundItem == null)
            {
                foundItem = itemArray.SearchItem(x => x.ItemCode == searchCode);
            }

            // ✅ **Display the item in the text fields**
            if (foundItem != null)
            {
                textBox1.Text = foundItem.ItemName;
                textBox2.Text = foundItem.ItemCode;
                comboBox1.SelectedItem = foundItem.Category;
                dateTimePicker1.Value = DateTime.Parse(foundItem.ExpiryDate);
                dateTimePicker2.Value = DateTime.Parse(foundItem.ManufactureDate);
                textBox3.Text = foundItem.GrossAmount.ToString();
                textBox4.Text = foundItem.NetAmount.ToString();
                textBox5.Text = foundItem.Quantity.ToString();
            }
            else
            {
                MessageBox.Show("Item not found!", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // 🔹 **Update Item**
        private void guna2Button2_Click(object sender, EventArgs e)
        {
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

            // ✅ **Create Updated Item**
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

            // ✅ **Update in BST**
            itemBST.Delete(updatedItem);
            itemBST.InsertKey(updatedItem);

            // ✅ **Update in Linked List**
            itemList.Update(itemCode, updatedItem);

            // ✅ **Update in Dynamic Array**
            int index = itemArray.Find(x => x.ItemCode == itemCode);

            if (index != -1)  // ✅ If the item is found
            {
                itemArray.Update(index, updatedItem);
            }
            else
            {
                MessageBox.Show("Item not found in array!", "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            MessageBox.Show("Item updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}
