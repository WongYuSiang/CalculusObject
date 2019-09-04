using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
namespace CalculusObject.WebPage
{
    public partial class Learning_Page : System.Web.UI.Page
    {
        static string file;
        protected void Page_Load(object sender, EventArgs e)
        {
            #region Use是否上線
            SqlConnection AllData_Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConnection"].ConnectionString); //宣告一個資料庫連結
            AllData_Connection.Open();//開啟AllData資料庫
            string DataBase_Language = "Select * from PersonalInformation Where Account=@Account and Passsword=@Passsword";//資料庫語法
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
            SqlCommand cmd = new SqlCommand(DataBase_Language, AllData_Connection);//使用SQL  
            cmd.Parameters.Add("Account", SqlDbType.Char).Value = MD5_A;
            cmd.Parameters.Add("Passsword", SqlDbType.Char).Value = MD5_P;
            SqlDataReader DataBase_Reader = cmd.ExecuteReader();
            Boolean check = false;
            while (DataBase_Reader.Read())
            {
                check = true;
              //  StudentID = (string)DataBase_Reader["StudentID"];
            }
            AllData_Connection.Close();//開啟AllData資料庫
            if (!check)
            {
                Server.Transfer("~/Account/Login.aspx");
            }
            #endregion
            if (!IsPostBack)//一開始
            {
                try
                {
                    HttpContext req = HttpContext.Current;
                    file = req.Items["SendChapter"].ToString();
                }
                catch
                {
                    Server.Transfer("~/WebPage/Error_Page.aspx");
                }
            } 
        }

        protected void Button_RestartTest_Click(object sender, EventArgs e)
        {
            HttpContext Context = HttpContext.Current;//宣告這個可以傳資料
            Context.Items.Add("SendChapter", file);//內建一個子資料傳值
            Server.Transfer("~/WebPage/AdaptiveTest.aspx");//換一個網站
        }
    }
}