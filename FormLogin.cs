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
            this.FormBorderStyle = FormBorderStyle.None; 
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = System.Drawing.Color.White;

            
            Guna2Panel mainPanel = new Guna2Panel
            {
                Dock = DockStyle.Fill,
                BackColor = System.Drawing.Color.White
            };
            this.Controls.Add(mainPanel);

            
            Guna2Button btnClose = new Guna2Button
            {
                Text = "X",
                Size = new System.Drawing.Size(40, 40),
                Location = new System.Drawing.Point(this.ClientSize.Width - 50, 10),
                FillColor = System.Drawing.Color.Red,
                ForeColor = System.Drawing.Color.White,
                Anchor = AnchorStyles.Top | AnchorStyles.Right 
            };
            btnClose.Click += (sender, e) => { this.Close(); };
            mainPanel.Controls.Add(btnClose);

            
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

            



           
            Guna2TextBox txtUsername = new Guna2TextBox
            {
                PlaceholderText = "Username",
                Size = new System.Drawing.Size(300, 40),
                Margin = new Padding(0, 10, 0, 10)
            };

            
            Guna2TextBox txtPassword = new Guna2TextBox
            {
                PlaceholderText = "Password",
                Size = new System.Drawing.Size(300, 40),
                UseSystemPasswordChar = true,
                Margin = new Padding(0, 10, 0, 10)
            };

            
            Guna2Button btnLogin = new Guna2Button
            {
                Text = "Login",
                Size = new System.Drawing.Size(300, 45),
                FillColor = System.Drawing.Color.Blue,
                ForeColor = System.Drawing.Color.White,
                Margin = new Padding(0, 10, 0, 10)
            };

            
            TableLayoutPanel layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 5,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Padding = new Padding(0, 20, 0, 20)
            };

            
            layout.Controls.Add(txtUsername);
            layout.Controls.Add(txtPassword);
            layout.Controls.Add(btnLogin);

            
            centerPanel.Controls.Add(layout);

            
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
            
        }
    }
}
