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
    public partial class StudentsUC : UserControl
    {
        string ConStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Data\VSN.mdf;Integrated Security=True";
        SqlConnection con;
        SqlCommand com;
        SqlDataReader dr;
        SqlDataAdapter da;
        string query;

        public StudentsUC()
        {
            InitializeComponent();
            LoadList();
        }

        private void DGV_SelectionChanged(object sender, EventArgs e)
        {
            if (DGV.SelectedCells.Count > 0)
            {
                string SID = DGV.SelectedCells[0].Value.ToString();
                if (!String.IsNullOrWhiteSpace(SID))
                {
                    try
                    {
                        query = "Select * From Student Where Student_ID = '" + SID + "'";
                        con = new SqlConnection(ConStr);
                        com = new SqlCommand(query, con);
                        con.Open();
                        dr = com.ExecuteReader();
                        dr.Read();

                        labStudentID.Text = dr.GetValue(0).ToString();
                        labFirstName.Text = dr.GetValue(1).ToString();
                        labLastName.Text = dr.GetValue(2).ToString();
                        labStudyCenter.Text = dr.GetValue(3).ToString();

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
            query = "Select Student_ID as 'Student ID' From Student";
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
                string SID = DGV.SelectedCells[0].Value.ToString();

                var result = MessageBox.Show("Are you sure you want to delete " + SID + "?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    if (!String.IsNullOrWhiteSpace(SID))
                    {
                        try
                        {
                            query = "Delete From Student Where Student_ID = '" + SID + "'";
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

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadList();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            AddStudentUC ASUC = new AddStudentUC();
            ASUC.btnUpdate.Enabled = false;
            ASUC.btnUpdate.Hide();
            Parent.Controls.Add(ASUC);
            ASUC.Dock = DockStyle.Fill;
            ASUC.BringToFront();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            AddStudentUC ASUC = new AddStudentUC();
            ASUC.btnAdd.Enabled = false;
            ASUC.btnAdd.Hide();
            ASUC.btnUpdate.BringToFront();
            ASUC.TBStudentID.ReadOnly = true;

            ASUC.TBStudentID.Text = labStudentID.Text;
            ASUC.TBFirstName.Text = labFirstName.Text;
            ASUC.TBLastName.Text = labLastName.Text;
            ASUC.CBStudyCenter.Text = labStudyCenter.Text;

            Parent.Controls.Add(ASUC);
            ASUC.Dock = DockStyle.Fill;
            ASUC.BringToFront();
        }
    }
}
