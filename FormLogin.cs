using System;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace DSA_SuperMarket_Management_System
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None; // No window borders
            this.WindowState = FormWindowState.Maximized; // Fullscreen
            this.BackColor = System.Drawing.Color.White;

            // Parent Panel (For Centering)
            Guna2Panel mainPanel = new Guna2Panel
            {
                Dock = DockStyle.Fill,
                BackColor = System.Drawing.Color.White
            };
            this.Controls.Add(mainPanel);

            // Close Button (Top-Right)
            Guna2Button btnClose = new Guna2Button
            {
                Text = "X",
                Size = new System.Drawing.Size(40, 40),
                Location = new System.Drawing.Point(this.ClientSize.Width - 50, 10),
                FillColor = System.Drawing.Color.Red,
                ForeColor = System.Drawing.Color.White,
                Anchor = AnchorStyles.Top | AnchorStyles.Right // Ensures position even on resize
            };
            btnClose.Click += (sender, e) => { this.Close(); };
            mainPanel.Controls.Add(btnClose);

            // Centering Panel (Holds Form Elements)
            Guna2Panel centerPanel = new Guna2Panel
            {
                Size = new System.Drawing.Size(400, 350),
                BackColor = System.Drawing.Color.Transparent,
                Location = new System.Drawing.Point(
                    (this.ClientSize.Width - 400) / 2,
                    (this.ClientSize.Height - 350) / 2
                )
            };
            mainPanel.Controls.Add(centerPanel);

            // Title Label



            // Username Field
            Guna2TextBox txtUsername = new Guna2TextBox
            {
                PlaceholderText = "Username",
                Size = new System.Drawing.Size(300, 40),
                Margin = new Padding(0, 10, 0, 10)
            };

            // Password Field (Hidden Characters)
            Guna2TextBox txtPassword = new Guna2TextBox
            {
                PlaceholderText = "Password",
                Size = new System.Drawing.Size(300, 40),
                UseSystemPasswordChar = true,
                Margin = new Padding(0, 10, 0, 10)
            };

            // Login Button
            Guna2Button btnLogin = new Guna2Button
            {
                Text = "Login",
                Size = new System.Drawing.Size(300, 45),
                FillColor = System.Drawing.Color.Blue,
                ForeColor = System.Drawing.Color.White,
                Margin = new Padding(0, 10, 0, 10)
            };

            // Table Layout for Centered Elements
            TableLayoutPanel layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 5,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Padding = new Padding(0, 20, 0, 20)
            };

            //layout.Controls.Add(lblTitle);
            layout.Controls.Add(txtUsername);
            layout.Controls.Add(txtPassword);
            layout.Controls.Add(btnLogin);

            // Adding Layout to Center Panel
            centerPanel.Controls.Add(layout);

            // Login Button Action
            btnLogin.Click += (sender, e) =>
            {
                if (txtUsername.Text == "admin" && txtPassword.Text == "admin")
                {
                    MainForm mainForm = new MainForm();
                    mainForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Incorrect username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            // Adjust Close Button Position on Resize
            this.Resize += (sender, e) =>
            {
                btnClose.Location = new System.Drawing.Point(this.ClientSize.Width - 50, 10);
                centerPanel.Location = new System.Drawing.Point(
                    (this.ClientSize.Width - 400) / 2,
                    (this.ClientSize.Height - 350) / 2
                );
            };

        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            // Runs when the form loads
        }
    }
}
