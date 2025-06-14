﻿using System;
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
        private Guna2Button closeButton, toggleDarkMode, btnLogout;
        private Guna2Panel sidebar;
        private Guna2Button btnItems, btnUsers;
        private Guna2ShadowForm shadowEffect;
        private bool isDarkMode = false;

        public MainForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(173, 216, 230);

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
                Height = 100,
                Dock = DockStyle.Top,
                BackColor = Color.Navy,
                BorderRadius = 5
            };
            this.Controls.Add(titleBar);

            Guna2HtmlLabel titleLabel = new Guna2HtmlLabel
            {
                Text = "Super Market Management",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 35, FontStyle.Bold),
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
            closeButton.Click += (s, e) => Application.Exit();
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

            // Add Logout Button
            btnLogout = new Guna2Button
            {
                Text = "Logout",
                Dock = DockStyle.Bottom,
                Height = 60,
                BorderRadius = 5,
                FillColor = Color.FromArgb(255, 69, 58), // Red color for logout
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Animated = true
            };
            btnLogout.Click += btnLogout_Click;  // Link to the Logout functionality

            sidebar.Controls.Add(btnLogout);  // Add Logout button to the sidebar
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

        private async void btnItems_Click(object sender, EventArgs e)
        {
            FormLoading loadingScreen = new FormLoading();
            await loadingScreen.ShowLoadingScreen(1000); // Properly wait for animation

            this.Hide();
            FormItem formItem = new FormItem();
            this.Hide();
            formItem.ShowDialog();
            this.Show();
        }

        private async void btnUsers_Click(object sender, EventArgs e)
        {
            FormLoading loadingScreen = new FormLoading();
            await loadingScreen.ShowLoadingScreen(1000); // Properly wait for animation

            this.Hide();
            FormUser formUser = new FormUser();
            this.Hide();
            formUser.ShowDialog();
            this.Show();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            // Hide the current form (MainForm)
            this.Hide();

            // Create and show the login form
            FormLogin formLogin = new FormLogin();
            formLogin.ShowDialog(); // Show the login form
        }

        private void label2_Click(object sender, EventArgs e) { }

        private void MainForm_Load(object sender, EventArgs e) { }
    }
}
