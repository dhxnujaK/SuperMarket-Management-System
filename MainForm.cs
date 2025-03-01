using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms; // ✅ Install via NuGet: Install-Package Guna.UI2.WinForms

namespace DSA_SuperMarket_Management_System
{
    public partial class MainForm : Form
    {
        private Panel titleBar;
        private Button closeButton;
        private Panel sidebar;
        private Button btnItems, btnUsers;
        private Guna2ShadowForm shadowEffect; // ✅ Shadow effect for a modern look

        public MainForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None; // ✅ Remove Windows border
            this.StartPosition = FormStartPosition.CenterScreen; // ✅ Center form
            this.BackColor = Color.FromArgb(240, 240, 240); // ✅ Modern background color

            ApplyShadow(); // ✅ Apply shadow effect
            CreateCustomTitleBar(); // ✅ Custom title bar
            CreateSidebar(); // ✅ Modern sidebar
        }

        // ✅ Apply shadow effect for modern UI
        private void ApplyShadow()
        {
            shadowEffect = new Guna2ShadowForm();
            shadowEffect.SetShadowForm(this);
        }

        // ✅ Create a custom title bar
        private void CreateCustomTitleBar()
        {
            titleBar = new Panel
            {
                Height = 40,
                Dock = DockStyle.Top,
                BackColor = Color.Navy
            };
            this.Controls.Add(titleBar);

            Label titleLabel = new Label
            {
                Text = "SuperMarket Management",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(10, 10)
            };
            titleBar.Controls.Add(titleLabel);

            closeButton = new Button
            {
                Text = "X",
                ForeColor = Color.White,
                BackColor = Color.Navy,
                FlatStyle = FlatStyle.Flat,
                Width = 40,
                Height = 40,
                Dock = DockStyle.Right
            };
            closeButton.FlatAppearance.BorderSize = 0;
            closeButton.Click += (s, e) => this.Close();
            titleBar.Controls.Add(closeButton);

            // ✅ Enable dragging of the form
            titleBar.MouseDown += (s, e) =>
            {
                this.Capture = false;
                Message msg = Message.Create(this.Handle, 0xA1, (IntPtr)0x2, IntPtr.Zero);
                this.WndProc(ref msg);
            };
        }

        // ✅ Create a sidebar menu
        private void CreateSidebar()
        {
            sidebar = new Panel
            {
                Width = 200,
                Dock = DockStyle.Left,
                BackColor = Color.FromArgb(30, 30, 60)
            };
            this.Controls.Add(sidebar);

            btnItems = new Button
            {
                Text = "Manage Items",
                Dock = DockStyle.Top,
                Height = 50,
                BackColor = Color.FromArgb(45, 45, 80),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat
            };
            btnItems.FlatAppearance.BorderSize = 0;
            btnItems.Click += btnItems_Click;

            btnUsers = new Button
            {
                Text = "Manage Users",
                Dock = DockStyle.Top,
                Height = 50,
                BackColor = Color.FromArgb(45, 45, 80),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat
            };
            btnUsers.FlatAppearance.BorderSize = 0;
            btnUsers.Click += btnUsers_Click;

            sidebar.Controls.Add(btnUsers);
            sidebar.Controls.Add(btnItems);
        }

        // ✅ Button click event to open FormItem
        private void btnItems_Click(object sender, EventArgs e)
        {
            FormItem formItem = new FormItem();
            this.Hide(); // Hide MainForm
            formItem.ShowDialog(); // Open FormItem in modal mode
            this.Show(); // Show MainForm again after FormItem is closed
        }

        // ✅ Button click event to open FormUser
        private void btnUsers_Click(object sender, EventArgs e)
        {
            FormUser formUser = new FormUser();
            this.Hide(); // Hide MainForm
            formUser.ShowDialog(); // Open FormUser in modal mode
            this.Show(); // Show MainForm again after FormUser is closed
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormItem formItem = new FormItem();
            this.Hide(); // Hide MainForm
            formItem.ShowDialog(); // Open FormItem in modal mode
            this.Show(); // Show MainForm again after FormItem is closed
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
        }
    }
}
