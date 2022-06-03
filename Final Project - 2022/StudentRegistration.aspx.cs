using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace Final_Project___2022
{
    public partial class Registration : System.Web.UI.Page
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

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                string encryptedPassword = Encrypt(txtPassword.Text);

                string mainconn = ConfigurationManager.ConnectionStrings["Mysqlconnection"].ConnectionString;
                MySqlConnection con = new MySqlConnection(mainconn);

                con.Open();
                //SQL Query to login
                String Query = "INSERT INTO StudentReg(Student_Number, Student_Name, Student_Surname, Student_IDNum, Student_DOB, Student_Password) VALUES(@Student_ID, @Student_Name, @Student_Surname, @Student_IDNum, @Student_DOB, @Student_Password);";
                MySqlCommand cmd = new MySqlCommand(Query, con);

                cmd.Parameters.AddWithValue("@Student_ID", txtStdNum.Text);
                cmd.Parameters.AddWithValue("@Student_Name", txtName.Text);
                cmd.Parameters.AddWithValue("@Student_Surname", txtSurname.Text);
                cmd.Parameters.AddWithValue("@Student_IDNum", txtIDNum.Text);
                cmd.Parameters.AddWithValue("@Student_DOB", txtDOB.Text);
                cmd.Parameters.AddWithValue("@Student_Password", encryptedPassword);

                MySqlDataReader reader = cmd.ExecuteReader();

                con.Close();

                Response.Write("<script>alert('Your Information has been sent to our admissions team. It will take up to 3 days for your portal to open');</script>");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Missing Deatils');</script>");
            }
        }
    }
}