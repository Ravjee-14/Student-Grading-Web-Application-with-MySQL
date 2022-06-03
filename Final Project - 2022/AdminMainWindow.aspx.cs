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
    public partial class AdminMainWindow : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string mainconn = ConfigurationManager.ConnectionStrings["Mysqlconnection"].ConnectionString;
                MySqlConnection con = new MySqlConnection(mainconn);

                con.Open();
                //SQL Query to login
                String Query = "SELECT Student_Number, Student_Name, Student_Surname, Student_IDNum, Student_DOB from StudentReg;";
                MySqlCommand cmd = new MySqlCommand(Query, con);
                MySqlDataReader reader = cmd.ExecuteReader();
                GridView1.DataSource = reader;
                GridView1.DataBind();

                con.Close();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Unable to retrieve information');</script>");
            }

        }

        protected void btnGet_Click(object sender, EventArgs e)
        {
            try
            {
                string mainconn = ConfigurationManager.ConnectionStrings["Mysqlconnection"].ConnectionString;
                MySqlConnection con = new MySqlConnection(mainconn);

                con.Open();
                //SQL Query to login
                String Query = "SELECT Student_Number, Student_Name, Student_Surname, Student_IDNum, Student_DOB from StudentReg Where Student_Number = @Student_ID;";
                MySqlCommand cmd = new MySqlCommand(Query, con);
                cmd.Parameters.AddWithValue("@Student_ID", txtStdNum.Text);

                MySqlDataReader reader = cmd.ExecuteReader();
                GridView1.DataSource = reader;
                GridView1.DataBind();

                con.Close();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Please input a Student ID');</script>");
            }

        }

        protected void btnAccept_Click(object sender, EventArgs e)
        {
            try
            {
                string mainconn = ConfigurationManager.ConnectionStrings["Mysqlconnection"].ConnectionString;
                MySqlConnection con = new MySqlConnection(mainconn);

                //To Insert student info into Student Table
                con.Open();
                String Query = "INSERT INTO Student(Student_Number, Student_FName, Student_Surname) SELECT Student_Number, Student_Name, Student_Surname FROM StudentReg WHERE Student_Number = @Student_ID; SET SQL_SAFE_UPDATES = 0; DELETE FROM StudentReg WHERE Student_Number = @Student_ID; SET SQL_SAFE_UPDATES = 1; INSERT INTO LOGIN(Username, Password) SELECT Student_Number, Student_Password FROM StudentReg WHERE Student_Number = @Student_ID;";
                MySqlCommand cmd = new MySqlCommand(Query, con);
                cmd.Parameters.AddWithValue("@Student_ID", txtStdNum.Text);

                MySqlDataReader reader = cmd.ExecuteReader();
                GridView1.DataSource = reader;
                GridView1.DataBind();
                con.Close();

                Response.Write("<script>alert('Student Successfully created. \n!! Student information moved to student database !!');</script>");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Please Insert Student ID');</script>");
            }

        }

        protected void Deny_Click(object sender, EventArgs e)
        {
            try
            {
                string mainconn = ConfigurationManager.ConnectionStrings["Mysqlconnection"].ConnectionString;
                MySqlConnection con = new MySqlConnection(mainconn);

                con.Open();
                //SQL Query to login
                String Query = "SET SQL_SAFE_UPDATES = 0; DELETE FROM StudentReg WHERE Student_Number = @Student_ID; SET SQL_SAFE_UPDATES = 1; ";
                MySqlCommand cmd = new MySqlCommand(Query, con);
                cmd.Parameters.AddWithValue("@Student_ID", txtStdNum.Text);

                MySqlDataReader reader = cmd.ExecuteReader();
                GridView1.DataSource = reader;
                GridView1.DataBind();

                con.Close();

                Response.Write("<script>alert('Student Successfully Denied');</script>");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Please Insert Student ID');</script>");
            }
        }
    }
}