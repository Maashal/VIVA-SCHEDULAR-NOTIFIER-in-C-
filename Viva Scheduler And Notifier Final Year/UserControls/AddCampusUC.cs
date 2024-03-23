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
    public partial class AddCampusUC : UserControl
    {
        public AddCampusUC()
        {
            InitializeComponent();
        }

        string ConStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Data\VSN.mdf;Integrated Security=True";
        SqlConnection con;
        string query;
        SqlCommand com;
        SqlDataReader dr;
        private void AddCampusUC_Load(object sender, EventArgs e)
        {
            try
            {
                CBNearestCampus.Items.Clear();
                // Loading Campuses
                con = new SqlConnection(ConStr);
                query = "Select Campus_Code From Campus";
                com = new SqlCommand(query, con);
                con.Open();

                dr = com.ExecuteReader();

                while (dr.Read())
                {
                    CBNearestCampus.Items.Add(dr.GetValue(0));
                }
                con.Close();
                com.Dispose();
            }
            catch (Exception exp)
            {
                MessageBox.Show("An Error Occured While Reading Database" + Environment.NewLine + exp.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (TBCCode.Text == "" || TBCName.Text == "" || CBCType.Text == "")
            {
                MessageBox.Show("Please fill required fields", "Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                try
                {
                    query = "Insert Into Campus (Campus_Code, Campus_Name, Campus_Address, Campus_Type, Nearest_Campus, Distance_To_Campus) Values ('" + TBCCode.Text + "', '" + TBCName.Text + "', '" + TBCAddress.Text + "', '" + CBCType.Text + "', '" + CBNearestCampus.Text + "', '" + TBDistanceToCampus.Text + "')";
                    con = new SqlConnection(ConStr);
                    com = new SqlCommand(query, con);

                    con.Open();
                    com.ExecuteNonQuery();

                    MessageBox.Show("Data Added Successfully", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception exp)
                {
                    MessageBox.Show("An Error Occured While Writing To Database" + Environment.NewLine + exp.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (TBCCode.Text == "" || TBCName.Text == "" || CBCType.Text == "")
            {
                MessageBox.Show("Please fill required fields", "Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                try
                {
                    query = "Update Campus Set Campus_Name = '" + TBCName.Text + "', Campus_Address = '" + TBCAddress.Text + "', Campus_Type = '" + CBCType.Text + "', Nearest_Campus = '" + CBNearestCampus.Text + "', Distance_To_Campus = '" + TBDistanceToCampus.Text + "' Where Campus_Code = '" + TBCCode.Text + "'";
                    con = new SqlConnection(ConStr);
                    com = new SqlCommand(query, con);
                    con.Open();

                    com.ExecuteNonQuery();

                    MessageBox.Show("Data Updated Successfully", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception exp)
                {
                    MessageBox.Show("An Error Occured While Writing To Database" + Environment.NewLine + exp.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void CBCType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CBCType.Text == "VU Owned")
            {
                TBDistanceToCampus.Enabled = false;
                CBNearestCampus.Text = TBCCode.Text;
                CBNearestCampus.Enabled = false;
            }
            else
            {
                TBDistanceToCampus.Enabled = true;
                CBNearestCampus.Text = "";
                CBNearestCampus.Enabled = true;
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Parent.Controls.Remove(this);
        }

        private void TBDistanceToCampus_Leave(object sender, EventArgs e)
        {
            if(TBDistanceToCampus.Text == "")
            {
                TBDistanceToCampus.Text = "0";
            }
        }
    }
}
