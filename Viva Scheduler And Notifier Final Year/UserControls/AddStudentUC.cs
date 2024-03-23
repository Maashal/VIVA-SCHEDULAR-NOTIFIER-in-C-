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
    public partial class AddStudentUC : UserControl
    {

        string ConStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Data\VSN.mdf;Integrated Security=True";
        SqlConnection con;
        string query;
        SqlCommand com;
        SqlDataReader dr;

        public AddStudentUC()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (TBStudentID.Text == "" || TBFirstName.Text == "" || CBStudyCenter.Text == "")
            {
                MessageBox.Show("Please fill required fields", "Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                try
                {
                    query = "Insert Into Student (Student_ID, First_Name, Last_Name, Study_Center) Values ('" + TBStudentID.Text + "', '" + TBFirstName.Text + "', '" + TBLastName.Text + "', '" + CBStudyCenter.Text + "')";
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

        private void AddStudentUC_Load(object sender, EventArgs e)
        {
            try
            {
                CBStudyCenter.Items.Clear();
                // Loading Campuses
                con = new SqlConnection(ConStr);
                query = "Select Campus_Code From Campus";
                com = new SqlCommand(query, con);
                con.Open();

                dr = com.ExecuteReader();

                while (dr.Read())
                {
                    CBStudyCenter.Items.Add(dr.GetValue(0));
                }

                // Adding OPKST
                CBStudyCenter.Items.Add("OPKST");

                con.Close();
                com.Dispose();
            }
            catch (Exception exp)
            {
                MessageBox.Show("An Error Occured While Reading Database" + Environment.NewLine + exp.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (TBStudentID.Text == "" || TBFirstName.Text == "" || CBStudyCenter.Text == "")
            {
                MessageBox.Show("Please fill required fields", "Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                try
                {
                    query = "Update Student Set First_Name = '" + TBFirstName.Text + "', Last_Name = '" + TBLastName.Text + "', Study_Center = '" + CBStudyCenter.Text + "' Where Student_ID = '" + TBStudentID.Text + "'";
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

        private void btnBack_Click(object sender, EventArgs e)
        {
            Parent.Controls.Remove(this);
        }
    }
}
