using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace DSA_SuperMarket_Management_System
{
    public partial class MainForm : Form
    {
        private Guna2Panel titleBar;
        private Guna2Button closeButton, toggleDarkMode;
        private Guna2Panel sidebar;
        private Guna2Button btnItems, btnUsers;
        private Guna2ShadowForm shadowEffect;
        private bool isDarkMode = false;

        public MainForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 240, 240);

            ApplyShadow();
            CreateCustomTitleBar();
            CreateSidebar();
        }

        private void ApplyShadow()
        {
            shadowEffect = new Guna2ShadowForm();
            shadowEffect.SetShadowForm(this);
        }

        private void CreateCustomTitleBar()
        {
            titleBar = new Guna2Panel
            {
                Height = 50,
                Dock = DockStyle.Top,
                BackColor = Color.Navy,
                BorderRadius = 5
            };
            this.Controls.Add(titleBar);

            Guna2HtmlLabel titleLabel = new Guna2HtmlLabel
            {
                Text = "SuperMarket Management",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(15, 15)
            };
            titleBar.Controls.Add(titleLabel);

            toggleDarkMode = new Guna2Button
            {
                Text = "☾",
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Width = 40,
                Height = 40,
                Dock = DockStyle.Right
            };
            toggleDarkMode.Click += ToggleDarkMode;
            titleBar.Controls.Add(toggleDarkMode);

            closeButton = new Guna2Button
            {
                Text = "X",
                ForeColor = Color.White,
                BackColor = Color.Red,
                BorderRadius = 5,
                Width = 40,
                Height = 40,
                Dock = DockStyle.Right
            };
            closeButton.Click += (s, e) => this.Close();
            titleBar.Controls.Add(closeButton);

            titleBar.MouseDown += (s, e) =>
            {
                this.Capture = false;
                Message msg = Message.Create(this.Handle, 0xA1, (IntPtr)0x2, IntPtr.Zero);
                this.WndProc(ref msg);
            };
        }
        
        private void CreateSidebar()
        {
            sidebar = new Guna2Panel
            {
                Width = 220,
                Dock = DockStyle.Left,
                BackColor = Color.FromArgb(30, 30, 60),
                BorderRadius = 5
            };
            this.Controls.Add(sidebar);

            btnItems = new Guna2Button
            {
                Text = "Manage Items",
                Dock = DockStyle.Top,
                Height = 60,
                BorderRadius = 5,
                FillColor = Color.FromArgb(45, 45, 80),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Animated = true
            };
            btnItems.Click += btnItems_Click;

            btnUsers = new Guna2Button
            {
                Text = "Manage Users",
                Dock = DockStyle.Top,
                Height = 60,
                BorderRadius = 5,
                FillColor = Color.FromArgb(45, 45, 80),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Animated = true
            };
            btnUsers.Click += btnUsers_Click;

            sidebar.Controls.Add(btnUsers);
            sidebar.Controls.Add(btnItems);
        }

        private void ToggleDarkMode(object sender, EventArgs e)
        {
            isDarkMode = !isDarkMode;
            this.BackColor = isDarkMode ? Color.FromArgb(30, 30, 30) : Color.FromArgb(240, 240, 240);
            sidebar.BackColor = isDarkMode ? Color.FromArgb(20, 20, 40) : Color.FromArgb(30, 30, 60);
            titleBar.BackColor = isDarkMode ? Color.Black : Color.Navy;
            btnItems.FillColor = isDarkMode ? Color.FromArgb(50, 50, 70) : Color.FromArgb(45, 45, 80);
            btnUsers.FillColor = isDarkMode ? Color.FromArgb(50, 50, 70) : Color.FromArgb(45, 45, 80);
            titleBar.Controls[0].ForeColor = isDarkMode ? Color.LightGray : Color.White;
            toggleDarkMode.Text = isDarkMode ? "☼" : "☾";
        }

        private void btnItems_Click(object sender, EventArgs e)
        {
            FormItem formItem = new FormItem();
            this.Hide();
            formItem.ShowDialog();
            this.Show();
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            FormUser formUser = new FormUser();
            this.Hide();
            formUser.ShowDialog();
            this.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormItem formItem = new FormItem();
            this.Hide();
            formItem.ShowDialog();
            this.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
        }
    }
}
