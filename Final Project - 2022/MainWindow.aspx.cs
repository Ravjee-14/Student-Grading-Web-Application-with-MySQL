using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace Final_Project___2022
{
    public partial class MainWindow : System.Web.UI.Page
    {
        Student myStudent = new Student();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                string mainconn = ConfigurationManager.ConnectionStrings["Mysqlconnection"].ConnectionString;
                MySqlConnection con = new MySqlConnection(mainconn);

                con.Open();
                //SQL Query to login
                String Query = "SELECT Student.Student_FName AS Name, Student.Student_Surname AS Surname, Alegbra, Calculus, Programming, S_Databases AS Databases_, Student_Average AS Average, Student_Grade AS Grade FROM Grades INNER JOIN Student ON Student.Student_number = Grades.Student_Number WHERE Student.Student_number = @Student_ID; ";
                MySqlCommand cmd = new MySqlCommand(Query, con);
                cmd.Parameters.AddWithValue("@Student_ID", txtStdNum.Text);

                MySqlDataReader reader = cmd.ExecuteReader();
                GridView1.DataSource = reader;
                GridView1.DataBind();

                con.Close();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Please Insert Student ID');</script>");
            }
        }
    }
}