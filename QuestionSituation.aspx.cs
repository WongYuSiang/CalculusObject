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
    public partial class QuestionSituation : System.Web.UI.Page
    {
        static string studentID = "";
        SqlConnection AllData_Connection = null;
        SqlCommand cmd;
        SqlDataReader DataBase_Reader;//資料庫讀取
        string DataBase_Language;
        protected void Page_Load(object sender, EventArgs e)
        {
            #region Use是否上線
            AllData_Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConnection"].ConnectionString); //宣告一個資料庫連結
            AllData_Connection.Open();//開啟AllData資料庫
            DataBase_Language = "Select * from PersonalInformation Where Account=@Account and Passsword=@Passsword";//資料庫語法
            string MD5_A = "";
            string MD5_P = "";
            try
            {
                MD5_A = Server.HtmlEncode(Request.Cookies["CCMATH"]["UserID"].ToString());
                MD5_P = Server.HtmlEncode(Request.Cookies["CCMATH"]["Password"].ToString());


            }
            catch
            {
                Server.Transfer("~/Account/Login.aspx");
            }
            //  Session["CCMATHUserID"] = MD5_A;
            // Session["CCMATHPassword"] = MD5_P;
            cmd = new SqlCommand(DataBase_Language, AllData_Connection);//使用SQL  
            cmd.Parameters.Add("Account", SqlDbType.Char).Value = MD5_A;
            cmd.Parameters.Add("Passsword", SqlDbType.Char).Value = MD5_P;
            DataBase_Reader = cmd.ExecuteReader();
            Boolean check = false;
            while (DataBase_Reader.Read())
            {
                check = true;
                studentID = (string)DataBase_Reader["StudentID"];
                //    Purview = (string)DataBase_Reader["Purview"];
            }
            AllData_Connection.Close();//開啟AllData資料庫
            if (!check)
            {
                Server.Transfer("~/Account/Login.aspx");
            }
            #endregion
        }

        protected void RadioButtonList2_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void GridView_S_PreRender(object sender, EventArgs e)
        {
            for (int i = 0; i < GridView_S.Rows.Count; i++)
            {

                if (int.Parse(GridView_S.Rows[i].Cells[3].Text) == -1)
                {
                    //  GridView_S.Rows[i].Cells[0].Enabled = false;
                    GridView_S.Rows[i].Cells[3].Text = "缺考";
                }


            }
        }
    }
}