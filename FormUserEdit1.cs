using System;
using System.Windows.Forms;

namespace DSA_SuperMarket_Management_System
{
    public partial class FormUserEdit1 : Form
    {
        private sLinkedList<User> userList;

        public FormUserEdit1()
        {
            InitializeComponent();
        }

        // Constructor that accepts userList
        public FormUserEdit1(sLinkedList<User> userList)
        {
            InitializeComponent();
            this.userList = userList;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e) // Search Button
        {
            string nic = textBox1.Text.Trim();
            if (string.IsNullOrEmpty(nic))
            {
                MessageBox.Show("Please enter an NIC number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            User foundUser = userList.Find(user => user.NIC == nic);
            if (foundUser != null)
            {
                textBox2.Text = foundUser.Name;
                textBox3.Text = foundUser.ContactNumber;
            }
            else
            {
                MessageBox.Show("User not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox2.Clear();
                textBox3.Clear();
            }
        }
    }
}
