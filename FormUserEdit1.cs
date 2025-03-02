using System;
using System.Windows.Forms;
using System.Data.SQLite;

namespace DSA_SuperMarket_Management_System
{
    public partial class FormUserEdit1 : Form
    {
        private sLinkedList<User> userList;
        private string connectionString = "Data Source=UserDatabase.db;Version=3;";
        private User currentUser;

        public FormUserEdit1()
        {
            InitializeComponent();
        }

        public FormUserEdit1(sLinkedList<User> userList)
        {
            InitializeComponent();
            this.userList = userList;
        }

        private void button1_Click(object sender, EventArgs e) // Search Button
        {
            string nic = textBox1.Text.Trim();
            if (string.IsNullOrEmpty(nic))
            {
                MessageBox.Show("Please enter an NIC number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            currentUser = userList.Find(user => user.NIC == nic);
            if (currentUser != null)
            {
                textBox2.Text = currentUser.Name;
                textBox3.Text = currentUser.ContactNumber;
            }
            else
            {
                MessageBox.Show("User not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox2.Clear();
                textBox3.Clear();
            }
        }

        private void button2_Click(object sender, EventArgs e) // Save Button
        {
            if (currentUser == null)
            {
                MessageBox.Show("No user selected for update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string name = textBox2.Text.Trim();
            string contactNumber = textBox3.Text.Trim();

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(contactNumber))
            {
                MessageBox.Show("Please fill in all required fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            User updatedUser = new User(currentUser.Id, name, currentUser.NIC, contactNumber);
            userList.Update(user => user.NIC == currentUser.NIC, updatedUser);
            UpdateDatabase(updatedUser);
            

            MessageBox.Show("User updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e) 
        {
            if (currentUser == null)
            {
                MessageBox.Show("No user selected to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Are you sure you want to delete this user?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                
                userList.Remove(x => x.NIC.Equals(currentUser.NIC, StringComparison.OrdinalIgnoreCase));
                DeleteFromDatabase(currentUser.NIC);
                

                MessageBox.Show("User deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void UpdateDatabase(User updatedUser)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE Users SET Name = @Name, ContactNumber = @ContactNumber WHERE NIC = @NIC";

                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", updatedUser.Name);
                    cmd.Parameters.AddWithValue("@ContactNumber", updatedUser.ContactNumber);
                    cmd.Parameters.AddWithValue("@NIC", updatedUser.NIC);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void DeleteFromDatabase(string nic)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Users WHERE NIC = @NIC";

                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@NIC", nic);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormUserEdit1_Load(object sender, EventArgs e)
        {
            // Any required initialization when form loads
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
