using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
namespace CalculusObject.Account
{
    public partial class CPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            
            Boolean error;
            error = true;
            Label_ERROR.Text = "";
            /*
          string MD5_P = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5").ToLower();
          */
            SqlCommand cmd;
            SqlConnection AllData_Connection = null;
            SqlDataReader DataBase_Reader;//資料庫讀取
            #region Use是否上線
            AllData_Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConnection"].ConnectionString); //宣告一個資料庫連結
            AllData_Connection.Open();//開啟AllData資料庫
            string DataBase_Language = "Select * from PersonalInformation Where Account=@Account ";//資料庫語法
            string MD5_A = "";
            string MD5_P = "";
            try
            {
                MD5_A = Server.HtmlEncode(Request.Cookies["CCMATH"]["UserID"].ToString());
                MD5_P = Server.HtmlEncode(Request.Cookies["CCMATH"]["Password"].ToString());


            }
            catch
            {
             //   Server.Transfer("~/Account/Login.aspx");
            }
            //  Session["CCMATHUserID"] = MD5_A;
            // Session["CCMATHPassword"] = MD5_P;
            cmd = new SqlCommand(DataBase_Language, AllData_Connection);//使用SQL  
            cmd.Parameters.Add("Account", SqlDbType.Char).Value = MD5_A;
            //cmd.Parameters.Add("Passsword", SqlDbType.Char).Value = MD5_P;
            DataBase_Reader = cmd.ExecuteReader();
            Boolean check = false;
            while (DataBase_Reader.Read())
            {
                check = true;
                string password = (string)DataBase_Reader["Passsword"];
                string check_old = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(TextBox_old.Text.Trim(), "MD5").ToLower();
                if (password.Trim() != check_old.Trim())
                {
                    error = false;
                    Label_ERROR.Text += "舊密碼錯誤\n";
                }
               // Purview = (string)DataBase_Reader["Purview"];
            }
            AllData_Connection.Close();//開啟AllData資料庫

            if (TextBox_new1.Text.Trim() != TextBox_new2.Text.Trim())
            {
                error = false;
                Label_ERROR.Text += "新密碼不一致\n";
            }
            if (check && error)
            {
                string passwordMD5 = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(TextBox_new1.Text.Trim(), "MD5").ToLower();
                AllData_Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConnection"].ConnectionString); //宣告一個資料庫連結
                AllData_Connection.Open();//開啟AllData資料庫
                DataBase_Language = "UPDATE PersonalInformation set Passsword=@Passsword Where Account=@Account";

                //DataBase_Language = "update * from ExerciseData s where Chapter = '" + chapterSelect + "'";//資料庫語法
                cmd = new SqlCommand(DataBase_Language, AllData_Connection);//使用SQL  
                cmd.Parameters.Add("Account", SqlDbType.Char).Value = MD5_A;
                cmd.Parameters.Add("Passsword", SqlDbType.Char).Value = passwordMD5;
                cmd.ExecuteNonQuery();
                AllData_Connection.Close();
                Session["CCMATHUserID"]=MD5_A ;
                Session["CCMATHPassword"] = passwordMD5;
            }
            #endregion
        }
        }
    }
