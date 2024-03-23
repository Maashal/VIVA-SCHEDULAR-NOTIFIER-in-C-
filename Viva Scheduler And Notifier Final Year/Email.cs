using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Data.SqlClient;

namespace Viva_Scheduler_And_Notifier_Final_Year
{
    class Email
    {

        // Loading Credentials
        string ConStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Data\VSN.mdf;Integrated Security=True";
        SqlConnection con;
        string query;
        SqlCommand com;
        SqlDataReader dr;

        private string[] GetCredentials()
        {
            string[] Credentials = { "", "" };
            con = new SqlConnection(ConStr);
            con.Open();
            query = "Select * From Credentials";
            com = new SqlCommand(query, con);

            dr = com.ExecuteReader();
            dr.Read();

            Credentials[0] = dr.GetValue(0).ToString();
            Credentials[1] = dr.GetValue(1).ToString();

            return Credentials;
        }

        public int SendMail(string To, string Subject, string Content)
        {
            try
            {
                //Sending the email.
                //Now we must create a new Smtp client to send our email.
                SmtpClient client = new SmtpClient("smtp.gmail.com", 25);   //smtp.gmail.com // For Gmail
                                                                            //smtp.live.com // Windows live / Hotmail
                                                                            //smtp.mail.yahoo.com // Yahoo
                                                                            //smtp.aim.com // AIM
                                                                            //my.inbox.com // Inbox


                //Authentication.
                //This is where the valid email account comes into play. You must have a valid email account(with password) to give our program a place to send the mail from.
                string[] Credentials = GetCredentials();
                NetworkCredential cred = new NetworkCredential(Credentials[0], Credentials[1]);

                //To send an email we must first create a new mailMessage(an email) to send.
                MailMessage Msg = new MailMessage();

                // Sender e-mail address.
                Msg.From = new MailAddress(Credentials[0]);//Nothing But Above Credentials or your credentials (*******@gmail.com)

                // Recipient e-mail address.
                Msg.To.Add(To);

                // Assign the subject of our message.
                Msg.Subject = Subject;

                // Create the content(body) of our message.
                Msg.Body = Content;

                Msg.IsBodyHtml = true;

                // Send our account login details to the client.
                client.Credentials = cred;

                //Enabling SSL(Secure Sockets Layer, encyription) is reqiured by most email providers to send mail
                client.EnableSsl = true;
                // Send our email.
                client.Send(Msg);
            }
            catch
            {
                return 0;
            }
            return 1;
        }
    }
}
