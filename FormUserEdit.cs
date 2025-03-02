using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DSA_SuperMarket_Management_System
{
    public partial class FormUserEdit : Form
    {
        private sLinkedList<User> userList;

        // Constructor that accepts a linked list of users
        public FormUserEdit(sLinkedList<User> userList)
        {
            InitializeComponent();
            this.userList = userList;
        }

        private void FormUserEdit_Load(object sender, EventArgs e)
        {
            // Perform any initialization tasks when the form loads
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // Label click event handler (can be removed if not needed)
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Handle text changes in textBox1 (if required)
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Implement button click functionality (e.g., saving changes)
        }
    }
}
