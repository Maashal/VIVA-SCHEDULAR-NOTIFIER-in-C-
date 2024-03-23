using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FontAwesome.Sharp;
using Viva_Scheduler_And_Notifier_Final_Year.UserControls;

namespace Viva_Scheduler_And_Notifier_Final_Year
{
    public partial class Form1 : Form
    {
        public static Color Base = Color.FromArgb(26, 26, 46);
        public static Color NavigationPanel = Color.FromArgb(22, 33, 62);
        public static Color TopPanel = Color.FromArgb(15, 52, 96);
        public static Color TextColor = Color.FromArgb(200, 200, 200);

        HomeUC HUC;
        Settings S;
        BatchViva BV;
        
        public Form1()
        {
            InitializeComponent();
        }

        IconButton currentbtn;
        private void ActivateButton(object senderbtn)
        {
            DeactivateButton();
            currentbtn = (IconButton)senderbtn;
            currentbtn.BackColor = Base;
            currentbtn.IconSize = 40;
            currentbtn.ForeColor = Color.White;
            currentbtn.IconColor = Color.White;
        }

        private void DeactivateButton()
        {
            if (currentbtn != null)
            {
                currentbtn.BackColor = NavigationPanel;
                currentbtn.IconSize = 30;
                currentbtn.ForeColor = TextColor;
                currentbtn.IconColor = TextColor;
            }
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);

            try
            {
                HUC.Dispose();

                HUC = new HomeUC();
                HUC.Dock = DockStyle.Fill;
                panelContent.Controls.Add(HUC);
                HUC.BringToFront();
            }
            catch
            {
                HUC = new HomeUC();
                HUC.Dock = DockStyle.Fill;
                panelContent.Controls.Add(HUC);
                HUC.BringToFront();
            }
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);

            try
            {
                S.Dispose();
                S = new Settings();
                S.Dock = DockStyle.Fill;
                panelContent.Controls.Add(S);
                S.BringToFront();
            }
            catch
            {

                S = new Settings();
                S.Dock = DockStyle.Fill;
                panelContent.Controls.Add(S);
                S.BringToFront();
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btnHome.PerformClick();
        }

        private void btnBatchViva_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);

            try
            {
                BV.Dispose();
                BV = new BatchViva();
                BV.Dock = DockStyle.Fill;
                panelContent.Controls.Add(BV);
                BV.BringToFront();
            }
            catch
            {

                BV = new BatchViva();
                BV.Dock = DockStyle.Fill;
                panelContent.Controls.Add(BV);
                BV.BringToFront();
            }
        }
    }
}
