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
    public partial class Exam_Option : System.Web.UI.Page
    {
        static string StudentID;
       // static Boolean All = true;
        static string group;
        SqlDataReader DataBase_Reader;//資料庫讀取
        static String selectnumber="";
        SqlCommand cmd;
        static DateTime EndTime = DateTime.Now;
        static DateTime BeginTime = DateTime.Now;
        protected void Page_Load(object sender, EventArgs e)
        {
            Label_END.Text = EndTime.ToLongDateString() + EndTime.ToLongTimeString();
            Label_BEGIN.Text = BeginTime.ToLongDateString() + BeginTime.ToLongTimeString();
            #region Use是否上線
            SqlConnection AllData_Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConnection"].ConnectionString); //宣告一個資料庫連結
            AllData_Connection.Open();//開啟AllData資料庫
          string  DataBase_Language = "Select * from PersonalInformation Where Account=@Account and Passsword=@Passsword";//資料庫語法
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
                group = (string)DataBase_Reader["Groups"];
                Session["CCMATHGroups"]=group;
            }
            AllData_Connection.Close();//開啟AllData資料庫
            if (!check)
            {
                Server.Transfer("~/Account/Login.aspx");
            }
            #endregion

            if (!IsPostBack)
            {
                string SelectGroup = DropDownList_Group.SelectedValue;
                SqlDataSource_stust.SelectCommand = "SELECT StudentID,Groups FROM PersonalInformation WHERE (Purview = 'Student')AND(Groups='" + SelectGroup.Trim() + "')";
                SqlDataSource_stust.DataBind();
                SqlDataSource1.DataBind();
            }
            GridView2.DataBind();
            Label_say.Text = "考試人數:" + GridView2.Rows.Count + "人";
        }

        protected void Button3_Click(object sender, EventArgs e)//起始時間
        { 
            LabelTime.Visible = true;
            LabelTime.Text = "選擇起始時間";
            CalendarTime.Visible = true;
            Label_J.Visible = true;
            DropDownListHour.Visible = true;
            DropDownListMin.Visible = true;
            Button_Begin.Visible = true;
            Button_End.Visible = false;
        }

        protected void Button4_Click(object sender, EventArgs e)//結束時間
        {
            LabelTime.Visible = true;
            LabelTime.Text = "選擇結束時間";
            CalendarTime.Visible = true;
            Label_J.Visible = true;
            DropDownListHour.Visible = true;
            DropDownListMin.Visible = true;
            Button_Begin.Visible = false;
            Button_End.Visible = true;
        }

        protected void Button_End_Click(object sender, EventArgs e)//按下結束時間"確定"按鍵
        {
            LabelTime.Visible = false;
            CalendarTime.Visible = false;
            Label_J.Visible = false;
            DropDownListHour.Visible = false;
            DropDownListMin.Visible = false;
            Button_Begin.Visible = false;
            Button_End.Visible = false;
            EndTime = CalendarTime.SelectedDate;//選取時間
            double HourTime, MinTime;
            HourTime = Convert.ToDouble(DropDownListHour.SelectedValue.ToString());//取得HOUR
            MinTime = Convert.ToDouble(DropDownListMin.SelectedValue.ToString());//取得MIN
            EndTime = EndTime.AddHours(HourTime).AddMinutes(MinTime);
            Label_END.Text = EndTime.ToLongDateString() + EndTime.ToLongTimeString();
        
        }

        protected void Button_Begin_Click(object sender, EventArgs e)//按下開始時間"確定"按鍵
        {
            LabelTime.Visible = false;
            CalendarTime.Visible = false;
            Label_J.Visible = false;
            DropDownListHour.Visible = false;
            DropDownListMin.Visible = false;
            Button_Begin.Visible = false;
            Button_End.Visible = false;
            BeginTime = CalendarTime.SelectedDate;//選取時間
            double HourTime,MinTime;
            HourTime = Convert.ToDouble(DropDownListHour.SelectedValue.ToString());
            MinTime = Convert.ToDouble(DropDownListMin.SelectedValue.ToString());
            BeginTime = BeginTime.AddHours(HourTime).AddMinutes(MinTime);
            Label_BEGIN.Text = BeginTime.ToLongDateString()+BeginTime.ToLongTimeString();
        }
        
        protected void Button1_Click(object sender, EventArgs e)//按下確定新增鍵
        {
            string gg = DateTime.Now.ToString("yyyyMMddHHmmss");
            Label_Exception.Text = "";
            try
            {
                int result = DateTime.Compare(BeginTime, EndTime);
                if (!(result < 0))
                {
                    Label_Exception.Text = "起始考試時間不能比結束時間晚，請再重新輸入";
                }
                else if (string.Compare(TextBox_Range.Text.ToString(), "") == 0)
                {
                    Label_Exception.Text = "考試範圍請不要輸入空白";
                }
                else
                {
                    //--------------------------------------------------OK選好之後仔入資料庫
                    SqlConnection AllData_Connection = null;
                    AllData_Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConnection"].ConnectionString); //宣告一個資料庫連結
                    AllData_Connection.Open();//開啟AllData資料庫
                    string DataBase_Language = "INSERT INTO TestTimeData(TestID,TestContent,Begin_time,End_time,TeacherID,RandCount,Groups)VALUES(@TestID,@TestContent,@BeginTime,@EndTime,@TeacherID,@RandCount,@Groups)";//資料庫語法  
                    cmd = new SqlCommand(DataBase_Language, AllData_Connection);//使用SQL 
                    cmd.Parameters.Add("BeginTime", SqlDbType.DateTime).Value = BeginTime;
                    cmd.Parameters.Add("EndTime", SqlDbType.DateTime).Value = EndTime;
                    cmd.Parameters.Add("TestContent", SqlDbType.NVarChar).Value = TextBox_Range.Text.ToString();
                    cmd.Parameters.Add("TestID", SqlDbType.Char).Value =  gg;
                    cmd.Parameters.Add("Groups", SqlDbType.Char).Value = DropDownList_Group.SelectedValue;
                    //cmd.Parameters.Add("Count", SqlDbType.Int).Value = DropDownList1.SelectedValue;
                    cmd.Parameters.Add("TeacherID", SqlDbType.NVarChar).Value = StudentID;

                    cmd.Parameters.Add("RandCount", SqlDbType.Int).Value =Int32.Parse( TextBox_RandCount.Text);
                    /*Class*/
                    /* string classStr = "";
                     for (int tempClass = 0; tempClass < CheckBoxList_class.Items.Count; tempClass++)
                     {
                         if (CheckBoxList_class.Items[tempClass].Selected == true)
                             classStr += CheckBoxList_class.Items[tempClass].Value + ",";
                     }
                     cmd.Parameters.Add("Class", SqlDbType.NVarChar).Value = classStr.Trim();*/
                    cmd.Connection = AllData_Connection;
                    cmd.ExecuteNonQuery();
                    AllData_Connection.Close();

                    //加入 ExamsList
                    AllData_Connection.Open();//開啟AllData資料庫
                    DataBase_Language = "DELETE FROM ExamsList where TestID=@TestID"; ;//資料庫語法  
                    cmd = new SqlCommand(DataBase_Language, AllData_Connection);//使用SQL             
                    cmd.Parameters.Add("TestID", SqlDbType.Char).Value =  gg;
                    cmd.ExecuteNonQuery();
                    AllData_Connection.Close();

                    for (int i = 0; i < GridView_Stust.Rows.Count; i++)
                    {
                        if (((CheckBox)GridView_Stust.Rows[i].FindControl("CheckBox1")).Checked == true)
                        {
                            AllData_Connection.Open();//開啟AllData資料庫
                            DataBase_Language = "INSERT INTO ExamsList(TestID,StudentID,TestContent,Score,StudentName,CreateTime)VALUES(@TestID,@StudentID,@TestContent,@Score,@StudentName,@CreateTime)";//資料庫語法  
                            cmd = new SqlCommand(DataBase_Language, AllData_Connection);//使用SQL             
                            cmd.Parameters.Add("StudentID", SqlDbType.Char).Value = GridView_Stust.Rows[i].Cells[1].Text;
                            cmd.Parameters.Add("StudentName", SqlDbType.Char).Value = GridView_Stust.Rows[i].Cells[2].Text;
                            cmd.Parameters.Add("CreateTime", SqlDbType.Char).Value = DateTime.Now;
                            cmd.Parameters.Add("TestContent", SqlDbType.NVarChar).Value = TextBox_Range.Text.ToString();
                            cmd.Parameters.Add("Score", SqlDbType.Int).Value = -1;
                            cmd.Parameters.Add("TestID", SqlDbType.Char).Value =  gg;
                            cmd.ExecuteNonQuery();
                            AllData_Connection.Close();
                        }
                    }
                    AllData_Connection.Open();//開啟AllData資料庫
                    DataBase_Language = "INSERT INTO ExamsList(TestID,StudentID,TestContent,Score,StudentName,CreateTime)VALUES(@TestID,@StudentID,@TestContent,@Score,@StudentName,@CreateTime)";//資料庫語法  
                    cmd = new SqlCommand(DataBase_Language, AllData_Connection);//使用SQL             
                    cmd.Parameters.Add("StudentID", SqlDbType.Char).Value = StudentID.Trim();
                    cmd.Parameters.Add("CreateTime", SqlDbType.Char).Value = DateTime.Now;
                    cmd.Parameters.Add("StudentName", SqlDbType.Char).Value = "(-出題老師-)";
                    cmd.Parameters.Add("TestContent", SqlDbType.NVarChar).Value = TextBox_Range.Text.ToString();
                    cmd.Parameters.Add("Score", SqlDbType.Int).Value = -1;
                    cmd.Parameters.Add("TestID", SqlDbType.Char).Value = gg;
                    cmd.ExecuteNonQuery();
                    AllData_Connection.Close();
                    //
                    ClientScriptManager CSM = Page.ClientScript;
                    if (!ReturnValue())
                    {
                        string strconfirm = "<script>if(!window.confirm('新增一筆" + gg + "完成')){}</script>";
                        CSM.RegisterClientScriptBlock(this.GetType(), "Confirm", strconfirm, false);
                    }
                }
                Label_Exception.Text = "擷取成功，已完成輸入";
            }
            catch
            {
                Label_Exception.Text = "擷取失敗，請重新選取";
            }
            
           
                    
                  
            
            
            
            GridView1.DataSourceID = "SqlDataSource1";
            GridView2.DataSourceID = "SqlDataSource_Doing";
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

            int i = Convert.ToInt32(GridView1.SelectedIndex);

            selectnumber = GridView1.Rows[i].Cells[1].Text;
            DropDownList2.Visible = true;
            Label_range.Visible = true;
            TextBox_Range.Text = GridView1.Rows[i].Cells[2].Text;
            Label_ID.Visible = true;
            apperControl();
            DropDownList2.DataSourceID = "SqlDataSource1";
            DropDownList2.DataBind();
            DropDownList2.SelectedValue = selectnumber;                        
            Button1.Visible = false;
            Button_fix.Visible = true;
            EndTime = Convert.ToDateTime(GridView1.Rows[i].Cells[4].Text);
            BeginTime = Convert.ToDateTime(GridView1.Rows[i].Cells[3].Text);
            Label_END.Text = EndTime.ToLongDateString() + EndTime.ToLongTimeString();
            Label_BEGIN.Text = BeginTime.ToLongDateString() + BeginTime.ToLongTimeString();
            TextBox_RandCount.Text = GridView1.Rows[i].Cells[7].Text;
            //是否跟改
            Boolean open = false;
            if (Convert.ToDateTime(GridView1.Rows[i].Cells[3].Text) < DateTime.Now)
                open = false;
            else
                open = true;

            DropDownList2.Enabled = open;
            TextBox_Range.Enabled = open;
            Button1.Enabled = open;
            Button_fix.Enabled = open;
            TextBox_RandCount.Enabled = open;
            Button3.Enabled = open;
            Button4.Enabled=open;
            // SELECT [TestID], [StudentID] FROM [ExamsList]
            SqlDataSource_Doing.SelectCommand = "SELECT TestID,StudentID FROM ExamsList WHERE (TestID='" + selectnumber + "')";
            SqlDataSource_Doing.DataBind();
            GridView2.DataBind();
            Label_say.Text = "考試人數:" + GridView2.Rows.Count+"人";
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Button_new_Click(object sender, EventArgs e)
        {
            DropDownList2.Enabled = true;
            TextBox_Range.Enabled = true;
            Button1.Enabled = true;
            Button_fix.Enabled = true;
            TextBox_RandCount.Enabled = true;
            Button3.Enabled = true;
            Button4.Enabled = true;
            Label_range.Visible = true;
            Label_ID.Visible = false;
            TextBox_Range.Visible = true;
            DropDownList2.Visible = false;
            Button1.Visible = true;
            Button_fix.Visible = false;
            apperControl();
        }

        protected void Button_fix_Click(object sender, EventArgs e)
        {
            SqlConnection AllData_Connection = null;
            AllData_Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConnection"].ConnectionString); //宣告一個資料庫連結
            AllData_Connection.Open();//開啟AllData資料庫
            string DataBase_Language = "UPDATE TestTimeData set TestContent=@Range,begin_time=@BeginTime,end_time=@EndTime,RandCount=@Count,TeacherID=@TeacherID WHERE TestID=@ID";//資料庫語法  
            cmd = new SqlCommand(DataBase_Language, AllData_Connection);//使用SQL 
            cmd.Parameters.Add("BeginTime", SqlDbType.DateTime).Value = BeginTime;
            cmd.Parameters.Add("EndTime", SqlDbType.DateTime).Value = EndTime;
            cmd.Parameters.Add("Range", SqlDbType.NVarChar).Value = TextBox_Range.Text.ToString();
            cmd.Parameters.Add("ID", SqlDbType.Char).Value = DropDownList2.SelectedValue;
            cmd.Parameters.Add("Count", SqlDbType.Int).Value = Int32.Parse(TextBox_RandCount.Text);
            cmd.Parameters.Add("TeacherID", SqlDbType.NVarChar).Value = StudentID;
            /*Class*/
           /* string classStr = "";
            for (int tempClass = 0; tempClass < CheckBoxList_class.Items.Count; tempClass++)
            {
                if (CheckBoxList_class.Items[tempClass].Selected == true)
                    classStr += CheckBoxList_class.Items[tempClass].Value + ",";
            }
            cmd.Parameters.Add("Class", SqlDbType.NVarChar).Value = classStr.Trim();
            */
            cmd.Connection = AllData_Connection;
            cmd.ExecuteNonQuery();
            AllData_Connection.Close();
            AllData_Connection.Open();//開啟AllData資料庫
            DataBase_Language = "DELETE FROM ExamsList where TestID=@TestID"; ;//資料庫語法  
            cmd = new SqlCommand(DataBase_Language, AllData_Connection);//使用SQL             
            cmd.Parameters.Add("TestID", SqlDbType.Char).Value = DropDownList2.SelectedValue;
            cmd.ExecuteNonQuery();
            AllData_Connection.Close();
            for (int i = 0; i < GridView_Stust.Rows.Count; i++)
            {
                if (((CheckBox)GridView_Stust.Rows[i].FindControl("CheckBox1")).Checked == true)
                {
                    AllData_Connection.Open();//開啟AllData資料庫
                    DataBase_Language = "INSERT INTO ExamsList(TestID,StudentID,TestContent,Score,StudentName,CreateTime)VALUES(@TestID,@StudentID,@TestContent,@Score,@StudentName,@CreateTime)";//資料庫語法  
                    cmd = new SqlCommand(DataBase_Language, AllData_Connection);//使用SQL             
                    cmd.Parameters.Add("StudentID", SqlDbType.Char).Value = GridView_Stust.Rows[i].Cells[1].Text;
                    cmd.Parameters.Add("TestID", SqlDbType.Char).Value = DropDownList2.SelectedValue;
                    cmd.Parameters.Add("StudentName", SqlDbType.Char).Value = GridView_Stust.Rows[i].Cells[2].Text;
                    cmd.Parameters.Add("CreateTime", SqlDbType.Char).Value = DateTime.Now;
                    cmd.Parameters.Add("TestContent", SqlDbType.NVarChar).Value = TextBox_Range.Text.ToString();
                    cmd.Parameters.Add("Score", SqlDbType.Int).Value = -1;
                    cmd.ExecuteNonQuery();
                    AllData_Connection.Close();
                }
            }
            GridView1.DataSourceID = "SqlDataSource1";
            GridView2.DataSourceID = "SqlDataSource_Doing";
            //
            SqlDataSource_Doing.SelectCommand = "SELECT TestID,StudentID FROM ExamsList WHERE (TestID='" + DropDownList2.SelectedValue + "')";
            SqlDataSource_stust.DataBind();
        }
        void apperControl()
        {
            Label_range.Visible = true;
            TextBox_Range.Visible = true;
            Label10.Visible = true;
            TextBox_RandCount.Visible = true;
            Label4.Visible = true;
            Button3.Visible = true;
            Label7.Visible = true;
            Label_END.Visible = true;
            Button4.Visible = true;
            Label_BEGIN.Visible = true;
        }

        protected void Button_Delete_Click(object sender, EventArgs e)
        {
            if (selectnumber != string.Empty)
            {

                SqlConnection AllData_Connection = null;
                AllData_Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConnection"].ConnectionString); //宣告一個資料庫連結
                AllData_Connection.Open();//開啟AllData資料庫
                string Del = string.Format("DELETE FROM TestTimeData WHERE TestID = '{0}'", selectnumber);
                cmd = new SqlCommand(Del, AllData_Connection);
                cmd.Connection = AllData_Connection;
                //執行刪除
                cmd.ExecuteNonQuery(); 
                AllData_Connection.Close();
                AllData_Connection.Open();//開啟AllData資料庫
                Del = string.Format("DELETE FROM SelectedData WHERE TextID = '{0}'", selectnumber);
                cmd = new SqlCommand(Del, AllData_Connection);
                cmd.Connection = AllData_Connection;
                //執行刪除
                cmd.ExecuteNonQuery();
                AllData_Connection.Close();
                AllData_Connection.Open();//開啟AllData資料庫
                Del = string.Format("DELETE FROM ExamsList WHERE TestID = '{0}'", selectnumber);
                cmd = new SqlCommand(Del, AllData_Connection);
                cmd.Connection = AllData_Connection;
                //執行刪除
                cmd.ExecuteNonQuery();
                AllData_Connection.Close();
                ClientScriptManager CSM = Page.ClientScript;
                if (!ReturnValue())
                {
                    string strconfirm = "<script>alert('刪除完成')</script>";
                    CSM.RegisterClientScriptBlock(this.GetType(), "Confirm", strconfirm, false);
                }
            }
            else
            {
                ClientScriptManager CSM = Page.ClientScript;
                if (!ReturnValue())
                {
                    string strconfirm = "<script>alert('抱歉你還沒選擇要刪除哪一筆資料'))</script>";
                    CSM.RegisterClientScriptBlock(this.GetType(), "Confirm", strconfirm, false);
                }
            }
            GridView1.DataSourceID = "SqlDataSource1"; 
        }
        bool ReturnValue()
        {
            return false;
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
          //20161114  
           
            string SN="";
            if (e.CommandName == "copyP")
            {
                for (int i = 0; i < GridView_Stust.Rows.Count; i++)
                {
                    ((CheckBox)GridView_Stust.Rows[i].FindControl("CheckBox1")).Checked = false ;
                    
                }
                List<string> listID = new List<string>();
                int pk_key = Convert.ToInt32(e.CommandArgument);
                SN = GridView1.Rows[pk_key].Cells[1].Text;
                Label_copy.Text = SN + "複製考試人員";
                SqlConnection AllData_Connection = null;
                AllData_Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConnection"].ConnectionString); //宣告一個資料庫連結
                AllData_Connection.Open();
                string DataBase_Language = "Select StudentID from ExamsList where TestID = '" + SN + "'";//資料庫語法
                cmd = new SqlCommand(DataBase_Language, AllData_Connection);//使用SQL  
                DataBase_Reader = cmd.ExecuteReader();

                while (DataBase_Reader.Read())
                {
                    listID.Add((string)DataBase_Reader["StudentID"]);

                }
                for (int ilist = 0; ilist < listID.Count; ilist++)
                {
                    for (int i = 0; i < GridView_Stust.Rows.Count; i++)
                    {
                        if (GridView_Stust.Rows[i].Cells[1].Text.Trim() == listID[ilist].Trim())
                        { 
                            //.FindControl("Label1")).Text.ToString().Trim();
                            ((CheckBox)GridView_Stust.Rows[i].FindControl("CheckBox1")).Checked = true;
                            break;
                        }
                    }
                
                }
                    AllData_Connection.Close();
                //執行刪除
               
            }
            if (e.CommandName == "copyQ")
            {
                int pk_key = Convert.ToInt32(e.CommandArgument);
                SN = GridView1.Rows[pk_key].Cells[1].Text;
                Label_copy.Text = SN + "複製考試題目";

            }
        }

        protected void DropDownList_Group_SelectedIndexChanged(object sender, EventArgs e)
        {
            string SelectGroup = DropDownList_Group.SelectedValue;
            //SELECT [StudentID], [Name], [Class], [Groups] FROM [PersonalInformation] ORDER BY [StudentID] DESC
            SqlDataSource_stust.SelectCommand = "SELECT StudentID,Name,Class,Groups FROM PersonalInformation  WHERE (Purview = 'Student')AND(Groups='" + SelectGroup.Trim() + "') ORDER BY StudentID DESC";
            SqlDataSource_stust.DataBind();
        }

        protected void Button_AllP_Click(object sender, EventArgs e)
        {
           
                for (int i = 0; i < GridView_Stust.Rows.Count; i++)
                    ((CheckBox)GridView_Stust.Rows[i].Cells[0].FindControl("CheckBox1")).Checked = true;
                

            
            
        }

        protected void CheckBoxList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string SelectGroup = group;
            int count = 0;
            count = 0;
            string str = "";
            foreach (ListItem li in CheckBoxList1.Items)
            {
                if (li.Selected)
                {
                    if (count == 0)
                    {
                        str = "('" + li.Value+"'";
                        count++;
                    }
                    else
                    {
                        str += ",'" + li.Value + "'";
                    }
                }
            }
            if (count != 0)
            {
                str = str + ")";
                string sr = "SELECT StudentID,Name,Class,Groups FROM PersonalInformation  WHERE (Purview = 'Student')AND(Groups='" + SelectGroup.Trim() + "') AND ClassID IN" + str + "  ORDER BY StudentID DESC";
                SqlDataSource_stust.SelectCommand = sr;
            }
            else
            {
                SqlDataSource_stust.SelectCommand = "SELECT StudentID,Name,Class,Groups FROM PersonalInformation  WHERE (Purview = 'Student')AND(Groups='" + SelectGroup.Trim() + "') ORDER BY StudentID DESC";
            }
            
                
            SqlDataSource_stust.DataBind();
        }

        protected void Button_AllD_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < GridView_Stust.Rows.Count; i++)
                ((CheckBox)GridView_Stust.Rows[i].Cells[0].FindControl("CheckBox1")).Checked = true;
        }

        protected void GridView_Stust_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
            {
                //要隱藏的欄位    
                e.Row.Cells[1].Visible = false;
               e.Row.Cells[5].Visible = false;
                e.Row.Cells[6].Visible = false;
                e.Row.Cells[10].Visible = false;
            }
        }
    }
}