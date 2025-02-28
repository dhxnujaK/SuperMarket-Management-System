using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DSA_SuperMarket_Management_System
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormItem formItem = new FormItem();
            this.Hide(); // Hide Form1
            formItem.ShowDialog(); // Open FormItem in modal mode
            this.Show(); // Show Form1 again after FormItem is closed
        }
    }
}
