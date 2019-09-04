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
    public partial class TeachingMaterials : System.Web.UI.Page
    {
       static int[] _y = new int[] { -1, -1, -1, -1, -1, -1, -1, -1 };
        SqlConnection AllData_Connection = null;
        string StudentID;
        SqlCommand cmd;
        SqlDataReader DataBase_Reader;//資料庫讀取
        static string vedioTag = "";
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
            cmd = new SqlCommand(DataBase_Language, AllData_Connection);//使用SQL  
            cmd.Parameters.Add("Account", SqlDbType.Char).Value = MD5_A;
            cmd.Parameters.Add("Passsword", SqlDbType.Char).Value = MD5_P;
            DataBase_Reader = cmd.ExecuteReader();
            Boolean check = false;
            while (DataBase_Reader.Read())
            {
                check = true;
                 StudentID = (string)DataBase_Reader["StudentID"];
            }
            AllData_Connection.Close();//開啟AllData資料庫
            if (!check)
            {
                Server.Transfer("~/Account/Login.aspx");
            }
            #endregion
            if (IsPostBack)
            {
               SqlDataSource_video.SelectCommand= "SELECT * FROM [Materials]";
               vedioTag = "OqSBJMUbCNA";
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
            string docName = "https://www.youtube.com/embed/";

            System.Web.UI.HtmlControls.HtmlControl pdfFrame;
            pdfFrame = (System.Web.UI.HtmlControls.HtmlControl)this.FindControl("VideoHere");
            docName += "OqSBJMUbCNA";
            pdfFrame.Attributes["src"] = docName;
             * */
            string docName = "https://www.youtube.com/embed/";
            int index = GridView1.SelectedRow.DataItemIndex;
            String selectContent = GridView1.Rows[index].Cells[2].Text;
            vedioTag = selectContent;//加入影片代碼
            docName += selectContent.Trim();
            VideoHere.Attributes["src"] = docName;
            Label_THS.Visible = false;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            VideoHere.Attributes["width"] = "420";
            VideoHere.Attributes["height"] = "345";

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            VideoHere.Attributes["width"] = "800";
            VideoHere.Attributes["height"] = "600";
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            VideoHere.Attributes["width"] = "500";
            VideoHere.Attributes["height"] = "400";
        }

        protected void Button_send_Click(object sender, EventArgs e)
        {
            try
            {
                Label_THS.Visible = true;
                string replystring = TextBox_reply.Text;
                int diff = int.Parse(RadioButtonList1.SelectedItem.Value);
                string vTag = vedioTag;

                //***
                
                AllData_Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConnection"].ConnectionString); //宣告一個資料庫連結                
                AllData_Connection.Open();//開啟AllData資料庫
                string DataBase_Language = "INSERT INTO Reply(StudentID,VideoTag,ConsiderDiff,Suggest,Time)VALUES(@StudentID,@VideoTag,@ConsiderDiff,@Suggest,@Time)";//資料庫語法  
                cmd = new SqlCommand(DataBase_Language, AllData_Connection);//使用SQL             
                cmd.Parameters.Add("StudentID", SqlDbType.Char).Value = StudentID;
                cmd.Parameters.Add("Suggest", SqlDbType.NVarChar).Value = replystring;
                cmd.Parameters.Add("VideoTag", SqlDbType.NVarChar).Value = vTag;
                cmd.Parameters.Add("ConsiderDiff", SqlDbType.Int).Value = diff;
                cmd.Parameters.Add("Time", SqlDbType.DateTime).Value =DateTime.Now;
                cmd.ExecuteNonQuery();
                AllData_Connection.Close();
                Label_THS.Text = "感謝給予回覆ヽ(✿ﾟ▽ﾟ)ノ";
                //
            }
            catch(Exception ex)
            {
                Label_THS.Text = "抱歉,傳送失敗，請再試ㄧ次";
            }
            

            
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Chapter = DropDownList1.SelectedValue.Trim();
             
            #region 資料讀取-屬性能力
            AllData_Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConnection"].ConnectionString); //宣告一個資料庫連結
            AllData_Connection.Open();//開啟AllData資料庫
            string DataBase_Language = "Select * from PersonalAbility where ID = '" + StudentID + "'";//資料庫語法
            cmd = new SqlCommand(DataBase_Language, AllData_Connection);//使用SQL  
            DataBase_Reader = cmd.ExecuteReader();
            while (DataBase_Reader.Read())
            {

                // Label_AB.Text +=chapter[o];
                int tempAbility = (int)DataBase_Reader[Chapter.Trim()];
                for (int K = 7; K >= 0; K--)
                {
                    //有問題2016/08/10____屬性配對有7&8不好處理
                    _y[K] = tempAbility % 10;
                    tempAbility = tempAbility / 10;


                }
                for (int K = 0; K < 7; K++)
                {
                    if (_y[K] == -1)
                    {
                        _y[K] = _y[K + 1];
                        _y[K + 1] = 0;
                    }//互換

                }//讓他7&8排序

            }
            #endregion

            //  ----------------------------------- 
            string A =
            SqlDataSource_video.SelectCommand = "SELECT[MainContent], [VideoTag], [Cheapter], [Difficulty], [SkillsUnit] FROM[Materials] WHERE([Cheapter] = '" + Chapter.Trim() + "')";
        }

        protected void GridView1_RowCreated(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {
                    int diff = int.Parse(e.Row.Cells[4].Text);
                    int Abil = int.Parse(e.Row.Cells[5].Text);
                    while (Abil != 0)
                    {
                        int t = 0;
                        t = Abil % 10;

                        if (_y[t - 1] == diff)
                        {
                            ((Label)e.Row.Cells[6].FindControl("Label_set")).Visible = true;
                            string AA = ((Label)e.Row.Cells[6].FindControl("Label_set")).Text;
                        }
                        else
                        { ((Label)e.Row.Cells[6].FindControl("Label_set")).Visible = false; }
                        Abil = Abil / 10;
                    }
                }
                catch { }
                
                e.Row.Cells[2].Visible = false;
            }
        }

       


       
    }
}