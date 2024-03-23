using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Viva_Scheduler_And_Notifier_Final_Year.UserControls
{
    public partial class HomeUC : UserControl
    {
        public HomeUC()
        {
            InitializeComponent();
        }

        private void btnCampus_Click(object sender, EventArgs e)
        {
            CampusesUC CUC = new CampusesUC();
            Parent.Controls.Add(CUC);
            CUC.Dock = DockStyle.Fill;
            CUC.BringToFront();
        }

        private void btnStudents_Click(object sender, EventArgs e)
        {
            StudentsUC SUC = new StudentsUC();
            Parent.Controls.Add(SUC);
            SUC.Dock = DockStyle.Fill;
            SUC.BringToFront();
        }

        private void btnViva_Click(object sender, EventArgs e)
        {
            VivasUC VUC = new VivasUC();
            Parent.Controls.Add(VUC);
            VUC.Dock = DockStyle.Fill;
            VUC.BringToFront();
        }
    }
}
