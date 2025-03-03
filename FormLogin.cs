using System;
using System.Windows.Forms;

namespace DSA_SuperMarket_Management_System
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();

            // Set button1 properties
            button1.Text = "Login";
            button1.BackColor = System.Drawing.Color.Blue;
            button1.ForeColor = System.Drawing.Color.White;
            button1.FlatStyle = FlatStyle.Flat;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            // Handle text change event if needed
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Handle text change event if needed
        }


        private void button1_Click(object sender, EventArgs e)
        {
            // Check if password entered is correct
            if (textBox2.Text == "123")
            {
                // Open MainForm when the correct password is entered
                MainForm mainForm = new MainForm();
                mainForm.Show();
                this.Hide(); // Hide login form
            }
            else
            {
                MessageBox.Show("Incorrect password. Please try again.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
