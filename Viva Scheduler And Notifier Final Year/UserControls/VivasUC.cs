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
    public partial class VivasUC : UserControl
    {
        string ConStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Data\VSN.mdf;Integrated Security=True";
        SqlConnection con;
        SqlCommand com;
        SqlDataReader dr;
        SqlDataAdapter da;
        string query;

        public VivasUC()
        {
            InitializeComponent();
            LoadList();
        }

        private void DGV_SelectionChanged(object sender, EventArgs e)
        {
            if (DGV.SelectedCells.Count > 0)
            {
                string VID = DGV.SelectedCells[0].Value.ToString();
                if (!String.IsNullOrWhiteSpace(VID))
                {
                    try
                    {
                        query = "Select * From Viva Where Viva_ID = '" + VID + "'";
                        con = new SqlConnection(ConStr);
                        com = new SqlCommand(query, con);
                        con.Open();
                        dr = com.ExecuteReader();
                        dr.Read();

                        labVivaID.Text = dr.GetValue(0).ToString();
                        labStudentID.Text = dr.GetValue(1).ToString();
                        labVivaType.Text = dr.GetValue(2).ToString();
                        labVivaDateTime.Text = Convert.ToDateTime(dr.GetValue(3)).ToShortDateString() + " " + Convert.ToDateTime(dr.GetValue(4)).ToShortTimeString();
                        labVivaStation.Text = dr.GetValue(5).ToString();
                        labVivaStatus.Text = dr.GetValue(6).ToString();
                        labRescheduled.Text = dr.GetValue(7).ToString();

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
            query = "Select Viva_ID as 'ID', Student_ID as 'Student ID', Viva_Status as 'Status' From Viva";
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
                string VID = DGV.SelectedCells[0].Value.ToString();

                var result = MessageBox.Show("Are you sure you want to delete " + VID + "?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    if (!String.IsNullOrWhiteSpace(VID))
                    {
                        try
                        {
                            query = "Delete From Viva Where Viva_ID = '" + VID + "'";
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
            AddVivaUC AVUC = new AddVivaUC();
            Parent.Controls.Add(AVUC);
            AVUC.Dock = DockStyle.Fill;
            AVUC.BringToFront();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            // Getting next Viva ID
            string query = "SELECT IDENT_CURRENT('Viva') as Id";
            SqlConnection con = new SqlConnection(ConStr);
            con.Open();
            SqlCommand com = new SqlCommand(query, con);
            SqlDataReader dr = com.ExecuteReader();
            dr.Read();
            int Reschedule_Viva_Id = Convert.ToInt32(dr["Id"].ToString());
            if (Reschedule_Viva_Id > 1)
            {
                Reschedule_Viva_Id++;
            }

            dr.Close();
            com.Dispose();

            RescheduleViva RV = new RescheduleViva(labStudentID.Text, labVivaType.Text, Convert.ToDateTime(labVivaDateTime.Text), labVivaStation.Text);
            RV.ShowDialog();

            query = "Update Viva Set Viva_Status = 'Rescheduled', Rescheduled_Viva_ID = '" + Reschedule_Viva_Id + "' Where Viva_ID = '" + labVivaID.Text + "'";
            con = new SqlConnection(ConStr);
            con.Open();
            com = new SqlCommand(query, con);
        }
    }
}
