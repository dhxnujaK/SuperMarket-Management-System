using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace DSA_SuperMarket_Management_System
{
    public partial class FormUserSort : Form
    {
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
            comboBox1.Items.AddRange(new string[] { "Quick Sort", "Merge Sort", "Insertion Sort", "Selection Sort", "Bubble Sort" });

            // Populate Column Names
            comboBox2.Items.AddRange(new string[] { "Id", "Name", "NIC", "Contact Number" });

            // Populate Data Structures
            comboBox3.Items.AddRange(new string[] { "Dynamic Array", "Linked List", "Binary Search Tree" });
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

            MessageBox.Show($"Sorting Completed!\nTime Taken: {stopwatch.ElapsedMilliseconds} ms", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void SortWithKey<TValue>(List<User> users, string algorithm, Func<User, TValue> keySelector) where TValue : IComparable<TValue>
        {
            switch (algorithm)
            {
                case "Quick Sort":
                    new QuickSort().Sort(users, keySelector);
                    break;
                case "Merge Sort":
                    new MergeSort().Sort(users, keySelector);
                    break;
                case "Insertion Sort":
                    new InsertionSort().Sort(users, keySelector);
                    break;
                case "Selection Sort":
                    new SelectionSort().Sort(users, keySelector);
                    break;
                case "Bubble Sort":
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Handle sorting selection change
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Handle column selection change
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Handle another selection change
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // Handle label click if needed
        }

        private void label2_Click(object sender, EventArgs e)
        {
            // Handle label click if needed
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
    }
}
