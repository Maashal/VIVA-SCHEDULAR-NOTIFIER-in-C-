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
    public partial class AddVivaUC : UserControl
    {
        string ConStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Data\VSN.mdf;Integrated Security=True";
        SqlConnection con;
        string query;
        SqlCommand com;
        SqlDataReader dr;


        string CampusCode, CampusName, CampusAddress, CampusType, NearestCampus, DistanceToCampus;

        public AddVivaUC()
        {
            InitializeComponent();
        }

        private void CBStudentID_Leave(object sender, EventArgs e)
        {
            // Loading Study Campus
            query = "Select Study_Center From Student Where Student_ID = '" + CBStudentID.Text + "'";
            con = new SqlConnection(ConStr);
            con.Open();
            com = new SqlCommand(query, con);
            dr = com.ExecuteReader();
            dr.Read();

            string StudyCenter = dr.GetValue(0).ToString();

            dr.Close();
            com.Dispose();
            con.Close();

            if (StudyCenter == "OPKST")
            {
                CBVivaStation.Text = "Home";
            }
            else
            {
                // Loading StudyCenter Details
                query = "Select * From Campus Where Campus_Code = '" + StudyCenter + "'";
                con = new SqlConnection(ConStr);
                con.Open();
                com = new SqlCommand(query, con);
                dr = com.ExecuteReader();
                dr.Read();

                CampusCode = dr.GetValue(0).ToString();
                CampusName = dr.GetValue(1).ToString();
                CampusAddress = dr.GetValue(2).ToString();
                CampusType = dr.GetValue(3).ToString();
                NearestCampus = dr.GetValue(4).ToString();
                DistanceToCampus = dr.GetValue(5).ToString();

                if (CampusType == "Private")
                {
                    CBVivaStation.Text = NearestCampus;
                }
                else
                {
                    CBVivaStation.Text = CampusCode;
                }
            }
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (CBStudentID.Text == "")
            {
                MessageBox.Show("Please fill required fields", "Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                try
                {
                    query = "Insert Into Viva (Student_ID, Viva_Type, Viva_Date, Viva_Time, Viva_Station, Viva_Status) Values ('" + CBStudentID.Text + "', '" + CBVivaType.Text + "', '" + pickerVivaDate.Value + "', '" + pickerVivaTime.Value + "', '" + CBVivaStation.Text + "', 'Pending')";
                    con = new SqlConnection(ConStr);
                    com = new SqlCommand(query, con);

                    con.Open();
                    com.ExecuteNonQuery();

                    SendEmail(CBStudentID.Text, CBVivaStation.Text, pickerVivaDate.Value, pickerVivaTime.Value, CampusAddress, CBVivaType.Text);


                    MessageBox.Show("Data Added Successfully", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Parent.Controls.Remove(this);
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

        private void AddVivaUC_Load(object sender, EventArgs e)
        {
            query = "Select Student_ID From Student";
            con = new SqlConnection(ConStr);
            con.Open();
            com = new SqlCommand(query, con);
            dr = com.ExecuteReader();
            CBStudentID.Items.Clear();
            while (dr.Read())
            {
                CBStudentID.Items.Add(dr.GetValue(0).ToString());
            }
            dr.Close();
            com.Dispose();
            con.Close();
        }


        void SendEmail(string StdID, string CampusID, DateTime VivaDate, DateTime VivaTime, string address, string VivaType)
        {
            string EmailContent = System.IO.File.ReadAllText("Email Template.htm");
            EmailContent.Replace("[Student ID]", StdID);
            EmailContent.Replace("[VIVA DATE]", VivaDate.ToShortDateString());
            EmailContent.Replace("[VIVA Time]", VivaTime.ToShortTimeString());
            EmailContent.Replace("[VIVA Station]", CampusID + " " + address);

            // Sending Student
            Email MyEmail = new Email();
            MyEmail.SendMail(StdID + "@vu.edu.pk", "Viva Notification", EmailContent);

            if (CampusID != "Home")
            {
                // Sending Campus
                MyEmail.SendMail(CampusID + "@vu.edu.pk", "Viva Notification", EmailContent);
            }
        }
    }
}
