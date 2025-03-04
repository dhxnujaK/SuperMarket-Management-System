using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace DSA_SuperMarket_Management_System
{
    public partial class FormLoading : Form
    {
        private Guna2CircleProgressBar progressBar;
        private Label loadingLabel;

        public FormLoading()
        {
            //InitializeComponent();
            SetupUI();
        }

        private void SetupUI()
        {
            // Set Fullscreen Mode
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.TopMost = true;

            // Progress Bar
            progressBar = new Guna2CircleProgressBar
            {
                Size = new System.Drawing.Size(150, 150),
                Anchor = AnchorStyles.None,
                ProgressColor = System.Drawing.Color.DeepSkyBlue,
                ProgressColor2 = System.Drawing.Color.Cyan,
                ShadowDecoration = { Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle },
                Value = 0,
                Minimum = 0,
                Maximum = 100,
                AnimationSpeed = 3.0F // Faster animation
            };
            this.Controls.Add(progressBar);

            // Center the progress bar dynamically
            progressBar.Left = (this.ClientSize.Width - progressBar.Width) / 2;
            progressBar.Top = (this.ClientSize.Height - progressBar.Height) / 2 - 50;

            // Loading Label
            loadingLabel = new Label
            {
                Text = "Loading, please wait...",
                ForeColor = System.Drawing.Color.White,
                Font = new System.Drawing.Font("Segoe UI", 18, System.Drawing.FontStyle.Bold),
                AutoSize = true,
                Anchor = AnchorStyles.None
            };
            this.Controls.Add(loadingLabel);

            // Center the label dynamically
            loadingLabel.Left = (this.ClientSize.Width - loadingLabel.Width) / 2;
            loadingLabel.Top = progressBar.Bottom + 20;
        }

        public async Task ShowLoadingScreen(int delayMilliseconds)
        {
            this.Show();
            for (int i = 0; i <= 100; i += 5)
            {
                progressBar.Value = i;
                await Task.Delay(delayMilliseconds / 20);
            }
            this.Close();
        }
    }
}
