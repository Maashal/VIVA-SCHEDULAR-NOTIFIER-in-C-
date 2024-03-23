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
    public partial class BatchViva : UserControl
    {
        string ConStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Data\VSN.mdf;Integrated Security=True";
        SqlConnection con;
        string query;
        SqlCommand com;
        SqlDataReader dr;

        public BatchViva()
        {
            InitializeComponent();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            OpenFileDialog OFD = new OpenFileDialog();
            OFD.Multiselect = false;
            OFD.ShowDialog();
            OFD.Filter = "Text | *.txt";
            string filename = OFD.FileName;

            string content = System.IO.File.ReadAllText(filename);
            string[] records = content.Split('\n');

            foreach (string record in records)
            {
                string[] items = record.Split(' ');
                StudentListItem SLI = new StudentListItem();
                SLI.TBStudentID.Text = items[0];
                SLI.TBCampusID.Text = items[1];

                FLP.Controls.Add(SLI);
            }
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            int totalVivas = FLP.Controls.Count;
            int VivasPerDay = Convert.ToInt32(TBVivaPerDay.Text);

            int VivasRemaining = totalVivas;
            int Count = 0;

            string CurrentStudentID;
            string StudyCenter;
            string CampusType;
            string CurrentVivaStationID;
            string CurrentVivaStaionAddress;
            DateTime CurrentVivaDate;
            DateTime CurrentVivaTime;

            CurrentVivaDate = pickerStartDate.Value;
            CurrentVivaTime = pickerStartTime.Value;

            foreach (StudentListItem SLI in FLP.Controls)
            {
                CurrentStudentID = SLI.TBStudentID.Text;
                StudyCenter = SLI.TBStudentID.Text;

                if (StudyCenter == "OPKST")
                {
                    CurrentVivaStationID = "Home";
                    CurrentVivaStaionAddress = "Home";
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

                    StudyCenter = dr.GetValue(0).ToString();
                    CurrentVivaStaionAddress = dr.GetValue(2).ToString();
                    CampusType = dr.GetValue(3).ToString();
                    CurrentVivaStationID = dr.GetValue(4).ToString();

                    if (CampusType != "Private")
                    {
                        CurrentVivaStationID = StudyCenter;
                    }
                }
                SendEmail(CurrentStudentID, CurrentVivaStationID, CurrentVivaDate, CurrentVivaTime, CurrentVivaStaionAddress, "");

                CurrentVivaTime = CurrentVivaTime.AddMinutes(Convert.ToDouble(SLI.TBDuration.Text));

                Count++;

                if (Count == VivasPerDay)
                {
                    CurrentVivaTime = pickerStartTime.Value;
                    CurrentVivaDate.AddDays(Convert.ToDouble(1));
                    Count = 0;
                }
            }

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
