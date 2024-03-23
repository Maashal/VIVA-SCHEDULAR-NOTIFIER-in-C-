using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Viva_Scheduler_And_Notifier_Final_Year.UserControls
{
    public partial class CampusesUC : UserControl
    {
        string ConStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Data\VSN.mdf;Integrated Security=True";
        SqlConnection con;
        SqlCommand com;
        SqlDataReader dr;
        SqlDataAdapter da;
        string query;
        public CampusesUC()
        {
            InitializeComponent();
            LoadList();
        }

        private void DGV_SelectionChanged(object sender, EventArgs e)
        {
            if (DGV.SelectedCells.Count > 0)
            {
                string CC = DGV.SelectedCells[0].Value.ToString();
                if (!String.IsNullOrWhiteSpace(CC))
                {
                    try
                    {
                        query = "Select * From Campus Where Campus_Code = '" + CC + "'";
                        con = new SqlConnection(ConStr);
                        com = new SqlCommand(query, con);
                        con.Open();
                        dr = com.ExecuteReader();
                        dr.Read();

                        labCCode.Text = dr.GetValue(0).ToString();
                        labCName.Text = dr.GetValue(1).ToString();
                        labCAddress.Text = dr.GetValue(2).ToString();
                        labCType.Text = dr.GetValue(3).ToString();
                        labVivaStation.Text = dr.GetValue(4).ToString();
                        labDistVivaStation.Text = dr.GetValue(5).ToString();

                        dr.Close();
                        con.Close();
                        com.Dispose();
                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show("An Error Occured While Reading Database" + Environment.NewLine + exp.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        void LoadList()
        {
            // Loading Campuses
            con = new SqlConnection(ConStr);
            query = "Select Campus_Code as 'Campus Code' From Campus";
            da = new SqlDataAdapter(query, con);
            con.Open();

            DataTable DT = new DataTable();

            da.Fill(DT);
            DGV.DataSource = DT;
            con.Close();
            da.Dispose();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (DGV.SelectedCells.Count > 0)
            {
                string CC = DGV.SelectedCells[0].Value.ToString();

                var result = MessageBox.Show("Are you sure you want to delete " + CC + "?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    if (!String.IsNullOrWhiteSpace(CC))
                    {
                        try
                        {
                            query = "Delete From Campus Where Campus_Code = '" + CC + "'";
                            con = new SqlConnection(ConStr);
                            com = new SqlCommand(query, con);
                            con.Open();
                            com.ExecuteNonQuery();

                            con.Close();
                            com.Dispose();

                            LoadList();
                        }
                        catch (Exception exp)
                        {
                            MessageBox.Show("An Error Occured While Deleting Record" + Environment.NewLine + exp.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Parent.Controls.Remove(this);
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            AddCampusUC ACUC = new AddCampusUC();
            ACUC.btnUpdate.Enabled = false;
            ACUC.btnUpdate.Hide();
            Parent.Controls.Add(ACUC);
            ACUC.Dock = DockStyle.Fill;
            ACUC.BringToFront();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if(labCCode.Text != "N/A")
            {
                AddCampusUC ACUC = new AddCampusUC();

                ACUC.TBCCode.Text = labCCode.Text;
                ACUC.TBCName.Text = labCName.Text;
                ACUC.TBCAddress.Text = labCAddress.Text;
                ACUC.CBCType.Text = labCType.Text;
                ACUC.CBNearestCampus.Text = labVivaStation.Text;
                ACUC.TBDistanceToCampus.Text = labDistVivaStation.Text;

                ACUC.TBCCode.ReadOnly = true;
                ACUC.btnUpdate.Enabled = true;
                ACUC.btnAdd.Enabled = false;
                ACUC.btnAdd.Hide();
                Parent.Controls.Add(ACUC);
                ACUC.Dock = DockStyle.Fill;
                ACUC.BringToFront();
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadList();
        }
    }
}
