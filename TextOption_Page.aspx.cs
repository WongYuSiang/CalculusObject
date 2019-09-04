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
namespace CalculusObject.webpage
{
    public partial class text_page : System.Web.UI.Page
    {
        static string studentID;
        static string studemtName;
        SqlConnection AllData_Connection = null;
        SqlCommand cmd;
        SqlDataReader DataBase_Reader;//資料庫讀取
        static List<string> testID = new List<string>();
        public class Qnumber
        {
            public string ID = "";
            public int Score = 0;
           public Qnumber(string id,int score)
           {
               this.ID = id;
               this.Score = score;
           }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            #region Use是否上線
            AllData_Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConnection"].ConnectionString); //宣告一個資料庫連結
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
            cmd = new SqlCommand(DataBase_Language, AllData_Connection);//使用SQL  
            cmd.Parameters.Add("Account", SqlDbType.Char).Value = MD5_A;
            cmd.Parameters.Add("Passsword", SqlDbType.Char).Value = MD5_P;
            DataBase_Reader = cmd.ExecuteReader();
            Boolean check = false;
            while (DataBase_Reader.Read())
            {
                check = true;
                studentID = (string)DataBase_Reader["StudentID"];
                studemtName = (string)DataBase_Reader["Name"];
            }
            AllData_Connection.Close();//開啟AllData資料庫
            if (!check)
            {
                Server.Transfer("~/Account/Login.aspx");
            }
            #endregion
            if (!IsPostBack)
            {
                #region 分類
                string TLR = "Select * from TestTimeData where ( Convert(DateTime,Begin_time) <  CURRENT_TIMESTAMP AND Convert(DateTime,End_time) > CURRENT_TIMESTAMP )";
                string TLN = "Select * from TestTimeData where ( Convert(DateTime,Begin_time) >  CURRENT_TIMESTAMP OR Convert(DateTime,End_time) < CURRENT_TIMESTAMP ) ";

                List<Qnumber> Qnumberset = new List<Qnumber>();
               
                List<string> BeenDone =new List<string>();
                //
                AllData_Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConnection"].ConnectionString); //宣告一個資料庫連結
                AllData_Connection.Open();//開啟AllData資料庫
                 DataBase_Language = "Select * from ExamsList Where StudentID=@StudentID";//資料庫語法

                cmd = new SqlCommand(DataBase_Language, AllData_Connection);//使用SQL  
                cmd.Parameters.Add("StudentID", SqlDbType.Char).Value = studentID;
                DataBase_Reader = cmd.ExecuteReader();
                while (DataBase_Reader.Read())
                {
                    Qnumberset.Add(new Qnumber((string)DataBase_Reader["TestID"], (int)DataBase_Reader["Score"]));
                  
                }
                AllData_Connection.Close();
                if (Qnumberset.Count != 0)
                {
                    string Sqlstring = "AND [TestID] IN('" + Qnumberset[0].ID + "'";
                    for (int u = 1; u < Qnumberset.Count; u++)
                    {
                        Sqlstring += ",'" + Qnumberset[u].ID + "'";
                    }
                    Sqlstring += ")";
                    TLR += Sqlstring;
                    TLN += Sqlstring;
                }
                else
                {
                    string Sqlstring = "AND [TestID] ='Null'";//沒有題目下
                    TLR += Sqlstring;
                    TLN += Sqlstring;
                }
                SqlDataSource_TestList.SelectCommand = TLR;
                GridView_EXAM.DataBind();
               SqlDataSource_TestList0.SelectCommand = TLN;
                GridView_EXAM0.DataBind();
                //其他
                for (int i = 0; i < GridView_EXAM.Rows.Count; i++)
                {
                    DateTime beginTime = Convert.ToDateTime(GridView_EXAM.Rows[i].Cells[3].Text.ToString());
                    DateTime endTime = Convert.ToDateTime(GridView_EXAM.Rows[i].Cells[4].Text.ToString());
                    string rowID = GridView_EXAM.Rows[i].Cells[1].Text.ToString();
                    if (beginTime != null && endTime != null)
                    {
                        if (beginTime > DateTime.Now || endTime < DateTime.Now)
                        {
                            GridView_EXAM.Rows[i].Cells[0].Enabled = true;

                        }
                        foreach (Qnumber Qrow in Qnumberset)
                        {
                            if (Qrow.ID.Equals(rowID))
                            {
                                if (Qrow.Score >= 0)
                                {
                                    GridView_EXAM.Rows[i].Cells[0].Enabled = false;
                                    GridView_EXAM.Rows[i].Cells[0].Text = "考試以作答完畢";
                                }
                               
                            }
                            }


                        }
                    }
                }
                for (int i = 0; i < GridView_EXAM0.Rows.Count; i++)
                {
                    DateTime beginTime = Convert.ToDateTime(GridView_EXAM0.Rows[i].Cells[2].Text.ToString());
                    DateTime endTime = Convert.ToDateTime(GridView_EXAM0.Rows[i].Cells[3].Text.ToString());
                    GridView_EXAM0.Rows[i].Cells[0].Enabled = false;
                    if (beginTime > DateTime.Now)
                        GridView_EXAM0.Rows[i].Cells[0].Text = "尚未開始";
                    if (endTime < DateTime.Now)
                        GridView_EXAM0.Rows[i].Cells[0].Text = "已經結束";

                //考試

                // SqlDataSource_TestList.SelectCommand = "Select * from TestTimeData where Begin_time > GETDATE() AND End_time < GETDATE()";
                //DATEDIFF(DAY, '17530101', '2009-01-19') % 7 / 5
                #endregion
                HttpCookie DElETCookie = Request.Cookies["CCMATHTEST"];
                if (DElETCookie != null)
                {
                    HttpCookie DCookie = new HttpCookie("CCMATHTEST");
                    DCookie.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(DCookie);
                }
                
                if (this.Request.Cookies["CCMATHTEST"] != null)
                {

                }
                HttpCookie DElETCCookie = Request.Cookies["CCMATHTESTTN"];
                if (DElETCCookie != null)
                {
                    HttpCookie DCookie = new HttpCookie("CCMATHTESTTN");
                    DCookie.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(DCookie);
                    
                }
            }
        }

        protected void GridView_EXAM_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                String selectnumber = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "TestID"));
                testID.Add(selectnumber);

            }      
        }

        protected void GridView_EXAM_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            int index = GridView_EXAM.SelectedRow.DataItemIndex;
            GridViewRow row = GridView_EXAM.SelectedRow;
            //String selectContent = row.FindControl("TestContent").ToString();
            // int i = Convert.ToInt32(GridView_EXAM.SelectedIndex);
            String selectContent = GridView_EXAM.Rows[index].Cells[2].Text;
            HttpCookie myCookie = new HttpCookie("CCMATHTEST");

            myCookie.Values.Add("SID", studentID);//學號
            myCookie.Values.Add("Name", HttpUtility.UrlEncodeUnicode(studemtName));//姓名
            myCookie.Values.Add("Context", HttpUtility.UrlEncodeUnicode(selectContent));//考試名稱
            myCookie.Values.Add("textID", testID[index]);//考試名稱
          //  myCookie.Values.Add("totalNumber", "");//總考題數
            myCookie.Values.Add("PersonReplynum", "1");//目前考到幾題
            myCookie.Expires = DateTime.Now.AddDays(5);
            myCookie.Path = "~/WebPage";
            Response.AppendCookie(myCookie);
            Response.Redirect("~/WebPage/inexam.aspx");

        }

        bool ReturnValue()
        {
            return false;
        }

        protected void GridView_EXAM_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if ((e.Row.RowType == DataControlRowType.DataRow) || (e.Row.RowType == DataControlRowType.Header) || (e.Row.RowType == DataControlRowType.Footer))
            {
                e.Row.Cells[1].Visible = false;
            }
        }
        

    }
}