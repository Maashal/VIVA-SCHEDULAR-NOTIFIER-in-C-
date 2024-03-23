using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Viva_Scheduler_And_Notifier_Final_Year
{
    public partial class RescheduleViva : Form
    {
        string ConStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Data\VSN.mdf;Integrated Security=True";
        SqlConnection con;
        string query;
        SqlCommand com;


        string Std_ID;
        string Viva_Type;
        DateTime Viva_Old_Date;
        string Viva_Station;
        
        public RescheduleViva(string Student_ID, string Type, DateTime Date, string Station)
        {
            InitializeComponent();

            Std_ID = Student_ID;
            Viva_Type = Type;
            Viva_Old_Date = Date;
            Viva_Station = Station;
        }

        private void btnReschedule_Click(object sender, EventArgs e)
        {
            try
            {
                query = "Insert Into Viva (Student_ID, Viva_Type, Viva_Date, Viva_Time, Viva_Station, Viva_Status) Values ('" + Std_ID + "', '" + Viva_Type + "', " + PickerDate.Value + ", " + PickerTime.Value + ", '" + Viva_Station + "', 'Pending')";
                con = new SqlConnection(ConStr);
                com = new SqlCommand(query, con);

                con.Open();
                com.ExecuteNonQuery();

                SendRescheduledEmail(Std_ID, Viva_Station, PickerDate.Value, PickerTime.Value, Viva_Station, "", Viva_Old_Date);

                this.Dispose();

            }
            catch (Exception exp)
            {
                MessageBox.Show("An Error Occured While Writing To Database" + Environment.NewLine + exp.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void SendRescheduledEmail(string StdID, string CampusID, DateTime VivaDate, DateTime VivaTime, string address, string VivaType, DateTime OldVivaDate)
        {
            string EmailContent = System.IO.File.ReadAllText("Rescheduled Email Template.htm");
            EmailContent.Replace("[OLD VIVA DATE]", OldVivaDate.ToShortDateString());
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
