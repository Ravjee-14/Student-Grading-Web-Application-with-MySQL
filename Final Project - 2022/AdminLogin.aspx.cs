using System;
using System.Text;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;

namespace Final_Project___2022
{
    public partial class AdminLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        static string Encrypt(string value)
        {
            //used to encrypt password
            using (MD5CryptoServiceProvider MD5 = new MD5CryptoServiceProvider())
            {
                UTF8Encoding utf8 = new UTF8Encoding();
                byte[] data = MD5.ComputeHash(utf8.GetBytes(value));
                return Convert.ToBase64String(data);
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["Mysqlconnection"].ConnectionString;
            MySqlConnection con = new MySqlConnection(mainconn);
            string encryptedPassword = Encrypt(txtPassword.Text);

            con.Open();
            //SQL Query to login
            String Query = "SELECT COUNT(1) FROM AdmissionsOfficer WHERE Admissions_Number = @Admin_ID AND Admissions_Password = @Admin_Password;";
            MySqlCommand cmd = new MySqlCommand(Query, con);
            cmd.Parameters.AddWithValue("@Admin_ID", txtAdminNum.Text);
            cmd.Parameters.AddWithValue("@Admin_Password", encryptedPassword);
            int count = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();

            //Loops used to perform certain actions like grant access or give an error message
            if (count == 1)
            {
                //used to open Main window
                Response.Redirect("AdminMainWindow.aspx");
            }
            else
            {
                Response.Write("<script>alert('Incorrect Username and Password Combination');</script>");
            }

            try
            {

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }
}