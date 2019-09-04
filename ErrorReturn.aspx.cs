using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
namespace CalculusObject.WebPage
{
    public partial class ErrorReturn : System.Web.UI.Page
    {
        SqlConnection AllData_Connection = null;
        SqlCommand cmd;
        SqlDataReader DataBase_Reader;//資料庫讀取
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button_Simmit_Click(object sender, EventArgs e)
        {
            if (TextBox_class.Text.Trim() == "" || TextBox_SID.Text.Trim() == "")
            {
                Label_R.Text = "請填入學號或是學校帳號";

            }
            else
            {
                try
                {
                    AllData_Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConnection"].ConnectionString); //宣告一個資料庫連結
                    AllData_Connection.Open();//開啟AllData資料庫
                    string DataBase_Language = "INSERT INTO ReturnError(StudentID,Name,Class,Content,Situation,Time)VALUES(@StudentID,@Name,@Class,@Content,@Situation,@Time)";//資料庫語法
                    cmd = new SqlCommand(DataBase_Language, AllData_Connection);//使用SQL
                    cmd.Parameters.Add("StudentID", SqlDbType.Char).Value = TextBox_SID.Text;
                    cmd.Parameters.Add("Name", SqlDbType.Char).Value = TextBox_Name.Text;
                    cmd.Parameters.Add("Class", SqlDbType.Char).Value = TextBox_class.Text;
                    cmd.Parameters.Add("Content", SqlDbType.Char).Value = TextBox_text.Text.Trim();
                    cmd.Parameters.Add("Situation", SqlDbType.Char).Value = DropDownList_sit.SelectedValue;
                    cmd.Parameters.Add("Time", SqlDbType.Char).Value = DateTime.Now;
                    // cmd.Parameters.Add("Name", SqlDbType.Char).Value = MD5_P;
                    cmd.ExecuteNonQuery();

                    AllData_Connection.Close();//開啟AllData資料庫

                    Label_R.Text = "輸出以完成";
                }
                catch
                {
                    Label_R.Text = "現在傳送有些問題,請稍後";
                }


            }
            

        }
    }
}