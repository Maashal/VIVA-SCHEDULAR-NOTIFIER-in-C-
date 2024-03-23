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
    public partial class Settings : UserControl
    {

        string ConStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Data\VSN.mdf;Integrated Security=True";
        SqlConnection con;
        string query;
        SqlCommand com;
        public Settings()
        {
            InitializeComponent();
        }


        private void CBShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if(CBShowPassword.Checked)
            {
                TBPassword.UseSystemPasswordChar = false;
            }
            else
            {
                TBPassword.UseSystemPasswordChar = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(TBEmailAddress.Text) && TBEmailAddress.Text.Contains('@') && TBEmailAddress.Text.Contains(".com") && !String.IsNullOrWhiteSpace(TBPassword.Text))
            {
                try
                {
                    // Deleting old credentials
                    query = "Delete From Credentials";
                    con = new SqlConnection(ConStr);
                    con.Open();
                    com = new SqlCommand(query, con);
                    com.ExecuteNonQuery();
                    com.Dispose();

                    // Adding New Credentials
                    query = "Insert Into Credentials Values('" + TBEmailAddress.Text + "', '" + TBPassword.Text + "')";
                    com = new SqlCommand(query, con);
                    com.ExecuteNonQuery();
                    com.Dispose();
                    con.Close();

                    TBEmailAddress.Text = "";
                    TBPassword.Text = "";

                    MessageBox.Show("Saved Successfully", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch
                {
                    MessageBox.Show("Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
    }
}
