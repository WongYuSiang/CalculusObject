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
    public partial class QuestionDatabase_Page : System.Web.UI.Page
    {
        GridViewRow row_get;
        static int randCount = 0;
        SqlConnection AllData_Connection = null;
        SqlCommand cmd;

        SqlDataReader DataBase_Reader;//資料庫讀取
        List<string> deleteReturn = new List<string>();
        protected void Page_Load(object sender, EventArgs e)
        {





            ListBox1.SelectionMode = ListSelectionMode.Multiple;
            ListBox2.SelectionMode = ListSelectionMode.Multiple;
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
                // Server.Transfer("~/Account/Login.aspx");
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
                // StudentID = (string)DataBase_Reader["StudentID"];
            }
            AllData_Connection.Close();//開啟AllData資料庫
            if (!check)
            {
                Server.Transfer("~/Account/Login.aspx");
            }
            #endregion
            if (!IsPostBack)
            {
                /*  AllData_Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConnection"].ConnectionString); //宣告一個資料庫連結
                  AllData_Connection.Open();
                   DataBase_Language = "Select * from TestTimeData where TestID = '" + row.Cells[1].Text + "'";//資料庫語法
                  cmd = new SqlCommand(DataBase_Language, AllData_Connection);//使用SQL  
                  DataBase_Reader = cmd.ExecuteReader();

                  while (DataBase_Reader.Read())
                  {
                      Label_getContent.Text = (string)DataBase_Reader["TestContent"];
                      randCount = (int)DataBase_Reader["RandCount"];
                  }
                  AllData_Connection.Close();

                  */

            }
            else
            {
                Label_Exception.Text = "";

                row_get = GridView_testDetail.SelectedRow;
            }


        }



        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = Convert.ToInt32(GridView1.SelectedIndex);

            String selectnumber = ((Label)GridView1.Rows[i].FindControl("Label1")).Text.ToString().Trim();
            PrintPic(selectnumber);

        }

        protected void ButtonAddTime_Click(object sender, EventArgs e)
        {
            Server.Transfer("~/WebPage/Exam_Option.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)//選入基本題
        {
            string DataBase_Language;
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                CheckBox cb = (CheckBox)GridView1.Rows[i].FindControl("CheckBoxInTo");
                if (cb.Checked == true)
                {
                    //關閉
                    ListBox1.Items.Add(((Label)GridView1.Rows[i].FindControl("Label1")).Text.ToString().Trim());
                    cb.Checked = false;
                }

            }
            /***登入***/
            int TotalCount = ListBox1.Items.Count;

            /*更新資料表*/

            //  ListBox1.DataBind();
            // Label_talk.Text = "目前考題:(必)" + ListBox1.Items.Count + ",(隨)" + ListBox2.Items.Count;


            Label_textS.Text = "題目設定基本" + ListBox1.Items.Count + "題<br>隨機以" + ListBox2.Items.Count + "選擇" + TextBox2.Text + "題";
        }

        void PrintPic(String selectnumber)//顯示圖片
        {
            int with = 200;
            Label_Exception.Text = "";
            try
            {
                //擷取Q.png資料+調整適當大小
                System.Drawing.Image image_set = System.Drawing.Image.FromFile(Server.MapPath("~/Images/Adaptive/" + selectnumber + "Q.png"));
                Image_Q.ImageUrl = "~/Images/Adaptive/" + (selectnumber + "Q.png").Trim();
                Image_Q.Width = (int)with;
                Image_Q.Height = (int)with * image_set.Height / image_set.Width;
                //擷取A.png資料+調整適當大小
                image_set = System.Drawing.Image.FromFile(Server.MapPath("~/Images/Adaptive/" + selectnumber + "A.png"));
                Image_A.ImageUrl = "~/Images/Adaptive/" + (selectnumber + "A.png").Trim();
                Image_A.Width = (int)with;
                Image_A.Height = (int)with * image_set.Height / image_set.Width;
                //B
                image_set = System.Drawing.Image.FromFile(Server.MapPath("~/Images/Adaptive/" + selectnumber + "B.png"));
                Image_B.ImageUrl = "~/Images/Adaptive/" + (selectnumber + "B.png").Trim();
                Image_B.Width = (int)with;
                Image_B.Height = (int)with * image_set.Height / image_set.Width;
                //C
                image_set = System.Drawing.Image.FromFile(Server.MapPath("~/Images/Adaptive/" + selectnumber + "C.png"));
                Image_C.ImageUrl = "~/Images/Adaptive/" + (selectnumber + "C.png").Trim();
                Image_C.Width = (int)with;
                Image_C.Height = (int)with * image_set.Height / image_set.Width;
                //D
                image_set = System.Drawing.Image.FromFile(Server.MapPath("~/Images/Adaptive/" + selectnumber + "D.png"));
                Image_D.ImageUrl = "~/Images/Adaptive/" + (selectnumber + "D.png").Trim();
                Image_D.Width = (int)with;
                Image_D.Height = (int)with * image_set.Height / image_set.Width;
            }
            catch
            {
                Label_Exception.Text = "顯示錯誤請重新選擇";
            }


        }

        protected void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            String selectnumber = ListBox1.SelectedValue.ToString().Trim();
            PrintPic(selectnumber);
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                CheckBox cb = (CheckBox)GridView1.Rows[i].FindControl("CheckBoxInTo");
                if (cb.Checked == true)
                {
                    /*  SqlConnection AllData_Connection = null;
                      AllData_Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConnection"].ConnectionString); //宣告一個資料庫連結
                      AllData_Connection.Open();//開啟AllData資料庫
                      string DataBase_Language = "INSERT INTO SelectedData(TextID,doNumber,Way)VALUES(@Range,@Number,2)";//資料庫語法  
                      cmd = new SqlCommand(DataBase_Language, AllData_Connection);//使用SQL 
                      cmd.Parameters.Add("Range", SqlDbType.NVarChar).Value = row.Cells[1].Text;
                      cmd.Parameters.Add("Number", SqlDbType.Char).Value = ((Label)GridView1.Rows[i].FindControl("Label1")).Text.ToString().Trim();
                      cmd.Connection = AllData_Connection;
                      cmd.ExecuteNonQuery();
                      AllData_Connection.Close();
                      */
                    //關閉
                    ListBox2.Items.Add(((Label)GridView1.Rows[i].FindControl("Label1")).Text.ToString().Trim());
                    cb.Checked = false;
                }


            }
            Label_textS.Text = "題目設定基本" + ListBox1.Items.Count + "題<br>隨機以" + ListBox2.Items.Count + "選擇" + TextBox2.Text + "題";
        }
        protected void option_choose()//自己建立
        {
            Label_TEXT.Text = "";
            string abitilty = "";
            List<string> getAllabitilty = new List<string>();
            List<int> getDifficulty = new List<int>();
            List<int> getAT = new List<int>();
            Boolean errorCheck = false;

            for (int o = 0; o < CheckBoxList_AT.Items.Count; o++)
            {
                if (CheckBoxList_AT.Items[o].Selected == true)
                {
                    getAT.Add(Convert.ToInt16(CheckBoxList_Difficulty.Items[o].Value.ToString()));
                }
            }


            /*找出必要的*/
            for (int j = 0; j < CheckBoxList_Difficulty.Items.Count; j++)
            {
                if (CheckBoxList_Difficulty.Items[j].Selected == true)
                {
                    getDifficulty.Add(Convert.ToInt16(CheckBoxList_Difficulty.Items[j].Text.ToString()));
                }
            }
            /*找困難度*/
            for (int i = 1; i < CheckBoxList_SkillUnit.Items.Count; i++)
            {
                if (CheckBoxList_SkillUnit.Items[i].Selected == true)
                {
                    abitilty += i;
                    errorCheck = true;
                    /*找出必要的*/
                    AllData_Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConnection"].ConnectionString); //宣告一個資料庫連結
                    AllData_Connection.Open();//開啟AllData資料庫
                    string DataBase_Language = "Select SkillsUnit from ExerciseData";//資料庫語法
                    cmd = new SqlCommand(DataBase_Language, AllData_Connection);//使用SQL  
                    DataBase_Reader = cmd.ExecuteReader();
                    while (DataBase_Reader.Read())
                    {
                        int SkillsUnit = (int)DataBase_Reader["SkillsUnit"];
                        if (Convert.ToString(SkillsUnit).Contains(i.ToString()))
                        {
                            if (!getAllabitilty.Contains(Convert.ToString(SkillsUnit)))
                            {
                                getAllabitilty.Add(Convert.ToString(SkillsUnit));
                            }
                        }

                    }
                    AllData_Connection.Close();
                }


            }
            string Sqlstring = "SELECT [ID],[Difficulty],[SkillsUnit],[Chapter]FROM[ExerciseData] WHERE[Chapter]='" + DropDownList1.SelectedValue.Trim() + "'";
            //SkillsUnit
            if (getAllabitilty.Count != 0)
            {
                Sqlstring += "AND [SkillsUnit] IN('" + getAllabitilty[0] + "'";
                for (int u = 1; u < getAllabitilty.Count; u++)
                {
                    Sqlstring += ",'" + getAllabitilty[u] + "'";
                }
                Sqlstring += ")";
            }

            //測驗或考試
            if (getAT.Count != 0)
            {
                Sqlstring += "AND [AdpterOrTest] IN('" + getAT[0] + "'";
                for (int u = 1; u < getAT.Count; u++)
                {
                    Sqlstring += ",'" + getAT[u] + "'";
                }
                Sqlstring += ")";
            }
            //Difficulty
            if (getDifficulty.Count != 0)
            {
                Sqlstring += "AND [Difficulty] IN('" + getDifficulty[0] + "'";
                for (int k = 1; k < getDifficulty.Count; k++)
                {
                    Sqlstring += ",'" + getDifficulty[k] + "'";
                }
                Sqlstring += ")";
            }
            if (CheckBoxList_SkillUnit.Items[0].Selected == true || errorCheck)
            {
                SqlDataSource1.SelectCommand = Sqlstring;
            }
            else
            { Label_TEXT.Text = "尚未選擇能力，請再重新點選"; }


            /*顯示目前選過的題目*/

        }
        protected void Button5_Click(object sender, EventArgs e)//刪除隨機題
        {

            string str = "";
            int count = 0;

            foreach (ListItem li in ListBox2.Items)
            {
                if (li.Selected)
                {
                    if (count == 0)
                    {
                        str = "('" + li.Text + "'";
                        count++;
                        deleteReturn.Add(li.Text);
                    }
                    else
                    {
                        str += ",'" + li.Text + "'";
                        deleteReturn.Add(li.Text);
                    }
                }
            }
            if (count != 0)
            {
                str = str + ")";
                /* AllData_Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConnection"].ConnectionString); //宣告一個資料庫連結
                 AllData_Connection.Open();//開啟AllData資料庫
                 string DataBase_Language = "DELETE FROM SelectedData WHERE Way=2 AND doNumber IN" + str + " AND TextID=@TextID";//資料庫語法  
                 cmd = new SqlCommand(DataBase_Language, AllData_Connection);//使用SQL 
                 cmd.Parameters.Add("TextID", SqlDbType.Char).Value = row.Cells[1].Text;
                 cmd.Connection = AllData_Connection;
                 cmd.ExecuteNonQuery();
                 AllData_Connection.Close();*/
                //關閉
                //   SqlDataSource3.SelectCommand = "SELECT DISTINCT doNumber FROM SelectedData WHERE ((Way = 2) AND (TextID = " + row.Cells[1].Text + "))";
            }
            Label_textS.Text = "題目設定基本" + ListBox1.Items.Count + "題<br>隨機以" + ListBox2.Items.Count + "選擇" + TextBox2.Text + "題";
        }

        protected void ButtonDelete1_Click(object sender, EventArgs e)//刪除基本題
        {
            while (ListBox1.SelectedItem != null) ListBox1.Items.Remove(ListBox1.SelectedItem);


            Label_textS.Text = "題目設定基本" + ListBox1.Items.Count + "題<br>隨機以" + ListBox2.Items.Count + "選擇" + TextBox2.Text + "題";
        }

        protected void ListBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            while (ListBox2.SelectedItem != null) ListBox2.Items.Remove(ListBox2.SelectedItem);
        }

        protected void DropDownList1_SelectedIndexChanged1(object sender, EventArgs e)
        {
            String selectchapter = DropDownList1.SelectedValue.ToString();
            //資料庫
            if (selectchapter == string.Empty)
            {
                selectchapter = "1-1";
            }
            AllData_Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConnection"].ConnectionString); //宣告一個資料庫連結
            AllData_Connection.Open();//開啟AllData資料庫
            string DataBase_Language = "Select * from Ability";//資料庫語法
            cmd = new SqlCommand(DataBase_Language, AllData_Connection);//使用SQL  
            DataBase_Reader = cmd.ExecuteReader();
            CheckBoxList_SkillUnit.Items.Clear();
            CheckBoxList_SkillUnit.Items.Add(new ListItem("全部", "0"));
            while (DataBase_Reader.Read())
            {

                try
                {
                    int AbilityValue = (int)DataBase_Reader["AbilityValue"];
                    string TestDataNumber = (string)DataBase_Reader[selectchapter.Trim()];

                    if (!(string.Compare(TestDataNumber, "") == 0))
                    {
                        CheckBoxList_SkillUnit.Items.Add(new ListItem("(" + AbilityValue + ")" + TestDataNumber, AbilityValue.ToString()));
                    }
                }
                catch
                { }

            }
            AllData_Connection.Close();
            for (int U = 1; U < CheckBoxList_Difficulty.Items.Count; U++)
            {
                CheckBoxList_Difficulty.Items[U].Selected = true;
            }
            for (int U = 1; U < CheckBoxList_SkillUnit.Items.Count; U++)
            {
                CheckBoxList_SkillUnit.Items[U].Selected = true;
            }
            option_choose();
        }

        protected void CheckBoxList1_SelectedIndexChanged(object sender, EventArgs e)//選能力
        {

            if (CheckBoxList_SkillUnit.Items[0].Selected)//
            {
                for (int U = 1; U < CheckBoxList_SkillUnit.Items.Count; U++)
                {
                    CheckBoxList_SkillUnit.Items[U].Selected = false;
                    CheckBoxList_SkillUnit.Items[U].Enabled = false;
                }
            }
            else
            {
                for (int U = 1; U < CheckBoxList_SkillUnit.Items.Count; U++)
                {

                    CheckBoxList_SkillUnit.Items[U].Enabled = true;
                }
                CheckBoxList_SkillUnit.Items[0].Selected = false;
            }

            option_choose();
        }

        protected void Button_Select_Click(object sender, EventArgs e)
        {


        }

        protected void DropDownListRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*   AllData_Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConnection"].ConnectionString); //宣告一個資料庫連結
               AllData_Connection.Open();
               string DataBase_Language = "Select * from TestTimeData where TestID = '" + row.Cells[1].Text + "'";//資料庫語法
               cmd = new SqlCommand(DataBase_Language, AllData_Connection);//使用SQL  
               DataBase_Reader = cmd.ExecuteReader();

               while (DataBase_Reader.Read())
               {
                   Label_getContent.Text = (string)DataBase_Reader["TestContent"];
                   randCount = (int)DataBase_Reader["RandCount"];
               }
               AllData_Connection.Close();
               */
            //   Label_talk.Text = "目前考題:(必)" + ListBox1.Items.Count + ",(隨)" + ListBox2.Items.Count;
            ListBox1.DataBind();
            ListBox2.DataBind();
            GridView1_PreRender(sender, e);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }
        protected void ListBox_PreRender(object sender, EventArgs e)
        {
            /*   AllData_Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConnection"].ConnectionString); //宣告一個資料庫連結
               AllData_Connection.Open();
               string DataBase_Language = "Select * from TestTimeData where TestID = '" + row.Cells[1].Text + "'";//資料庫語法
               cmd = new SqlCommand(DataBase_Language, AllData_Connection);//使用SQL  
               DataBase_Reader = cmd.ExecuteReader();

               while (DataBase_Reader.Read())
               {
          //         Label_getContent.Text = (string)DataBase_Reader["TestContent"];
                   randCount = (int)DataBase_Reader["RandCount"];
               }
               AllData_Connection.Close();
               */
            /*  Label_Exception.Text = "";
              Label_talk.Text = "目前考題:(必考)" + ListBox1.Items.Count + "題\t(亂取)" + ListBox2.Items.Count + "題/(選)" + randCount+"題";
              if (randCount > ListBox2.Items.Count)
              {
                  //Label_Exception.Text += "<br>[▲]目前設定'亂數題數'小於'挑選題目'可能造成題數減少";
              }
              if ((ListBox1.Items.Count + randCount) > 25)
              {
                  Label_Exception.Text += "<br>[▲]目前題目設定題數過多，請刪減題目";
              }
              if ((ListBox1.Items.Count + ListBox1.Items.Count) == 0)
              {
                  Label_Exception.Text += "<br>[▲]目前題目過少";
              }

              */
        }

        protected void GridView1_PreRender(object sender, EventArgs e)
        {


            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                for (int L1Items = 0; L1Items < ListBox1.Items.Count; L1Items++)
                {
                    string numberG = ((Label)GridView1.Rows[i].FindControl("Label1")).Text.ToString().Trim();
                    if (ListBox1.Items[L1Items].Text.Trim().Equals(numberG))
                    {
                        ((CheckBox)GridView1.Rows[i].FindControl("CheckBoxInTo")).Visible = false;

                        ((Label)GridView1.Rows[i].FindControl("Label_sureGet")).Visible = true;
                        ((Label)GridView1.Rows[i].FindControl("Label_sureGet")).Text = "以選取至'基本'";
                    }


                }
            }

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                for (int L2Items = 0; L2Items < ListBox2.Items.Count; L2Items++)
                {
                    string numberG = ((Label)GridView1.Rows[i].FindControl("Label1")).Text.ToString().Trim();
                    if (ListBox2.Items[L2Items].Text.Trim().Equals(numberG))
                    {
                        ((CheckBox)GridView1.Rows[i].FindControl("CheckBoxInTo")).Visible = false;
                        ((Label)GridView1.Rows[i].FindControl("Label_sureGet")).Visible = true;
                        ((Label)GridView1.Rows[i].FindControl("Label_sureGet")).Text = "以選取至'隨機'";
                    }

                }
            }

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                for (int DItems = 0; DItems < deleteReturn.Count; DItems++)
                {
                    string numberG = ((Label)GridView1.Rows[i].FindControl("Label1")).Text.ToString().Trim();
                    if (deleteReturn[DItems].Trim().Equals(numberG))
                    {
                        ((CheckBox)GridView1.Rows[i].FindControl("CheckBoxInTo")).Visible = true;

                        ((Label)GridView1.Rows[i].FindControl("Label_sureGet")).Visible = false;
                        //  ((Label)GridView1.Rows[i].FindControl("Label_sureGet")).Text = "以選取至'基本'";

                    }


                }
            }
            deleteReturn.Clear();
        }

        protected void GridView_testDetail_SelectedIndexChanged(object sender, EventArgs e)
        {
            row_get = GridView_testDetail.SelectedRow;
            AllData_Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConnection"].ConnectionString); //宣告一個資料庫連結
            AllData_Connection.Open();
            string DataBase_Language = "Select * from TestTimeData where TestID = '" + row_get.Cells[1].Text + "'";//資料庫語法
            cmd = new SqlCommand(DataBase_Language, AllData_Connection);//使用SQL  
            DataBase_Reader = cmd.ExecuteReader();

            while (DataBase_Reader.Read())
            {
                Label_getContent.Text = (string)DataBase_Reader["TestContent"];
                Label_getID.Text = (string)DataBase_Reader["TestID"];
                randCount = (int)DataBase_Reader["RandCount"];

            }
            TextBox2.Text = randCount.ToString();
            AllData_Connection.Close();
            //   Label_talk.Text = "目前考題:(必)" + ListBox1.Items.Count + ",(隨)" + ListBox2.Items.Count;
            ListBox1.DataBind();
            ListBox2.DataBind();
            GridView1_PreRender(sender, e);
            Label_textS.Text = "題目設定基本" + ListBox1.Items.Count + "題<br>隨機以" + ListBox2.Items.Count + "選擇" + TextBox2.Text + "題";
        }



        protected void Button_Sure_Click(object sender, EventArgs e)
        {
            Label_Exception.Text = "";

            //測試匯入資料
            try
            { //刪除"DELETE FROM 資料表 WHERE 欄位 = '{0}'"
                SqlConnection AllData_Connection = null;
                AllData_Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConnection"].ConnectionString); //宣告一個資料庫連結
                AllData_Connection.Open();//開啟AllData資料庫
                string DataBase_Language = "DELETE FROM SelectedData WHERE  TextID= @Range";//資料庫語法  
                cmd = new SqlCommand(DataBase_Language, AllData_Connection);//使用SQL 
                cmd.Parameters.Add("Range", SqlDbType.NVarChar).Value = Label_getID.Text;
                //cmd.Parameters.Add("Number", SqlDbType.Char).Value = ListBox1.Items[i].Text;
                cmd.Connection = AllData_Connection;
                cmd.ExecuteNonQuery();
                AllData_Connection.Close();

                //新增
                for (int i = 0; i < ListBox1.Items.Count; i++)
                {

                    //  SqlConnection AllData_Connection = null;
                    AllData_Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConnection"].ConnectionString); //宣告一個資料庫連結
                    AllData_Connection.Open();//開啟AllData資料庫
                    DataBase_Language = "INSERT INTO SelectedData(TextID,doNumber,Way)VALUES(@Range,@Number,1)";//資料庫語法  
                    cmd = new SqlCommand(DataBase_Language, AllData_Connection);//使用SQL 
                    cmd.Parameters.Add("Range", SqlDbType.NVarChar).Value = Label_getID.Text;
                    cmd.Parameters.Add("Number", SqlDbType.Char).Value = ListBox1.Items[i].Text;
                    cmd.Connection = AllData_Connection;
                    cmd.ExecuteNonQuery();
                    AllData_Connection.Close();
                }
                for (int i = 0; i < ListBox2.Items.Count; i++)
                {

                    AllData_Connection = null;
                    AllData_Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConnection"].ConnectionString); //宣告一個資料庫連結
                    AllData_Connection.Open();//開啟AllData資料庫
                    DataBase_Language = "INSERT INTO SelectedData(TextID,doNumber,Way)VALUES(@Range,@Number,2)";//資料庫語法  
                    cmd = new SqlCommand(DataBase_Language, AllData_Connection);//使用SQL 
                    cmd.Parameters.Add("Range", SqlDbType.NVarChar).Value = Label_getID.Text;
                    cmd.Parameters.Add("Number", SqlDbType.Char).Value = ListBox2.Items[i].Text;
                    cmd.Connection = AllData_Connection;
                    cmd.ExecuteNonQuery();
                    AllData_Connection.Close();
                }
                Label_Exception.Text += "已完成匯入";

            }
            catch (Exception ex)
            {
                Label_Exception.Text += "匯入失敗";
            }
            //回傳題目設定
            int anInteger;
            try
            {
                anInteger = Convert.ToInt32(TextBox2.Text);
                SqlConnection AllData_Connection = null;
                AllData_Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConnection"].ConnectionString); //宣告一個資料庫連結
                AllData_Connection.Open();//開啟AllData資料庫
                string DataBase_Language = "update TestTimeData set RandCount=@RandCount,TotalCount=@TotalCount,MustCount=@MustCount WHERE  TestID= @Range";//資料庫語法  
                cmd = new SqlCommand(DataBase_Language, AllData_Connection);//使用SQL 
                cmd.Parameters.Add("Range", SqlDbType.NVarChar).Value = Label_getID.Text.Trim();
                cmd.Parameters.Add("RandCount", SqlDbType.Int).Value = anInteger;
                cmd.Parameters.Add("TotalCount", SqlDbType.Int).Value = (ListBox1.Items.Count + anInteger);
                cmd.Parameters.Add("MustCount", SqlDbType.Int).Value = ListBox1.Items.Count;
                //cmd.Parameters.Add("Number", SqlDbType.Char).Value = ListBox1.Items[i].Text;
                cmd.Connection = AllData_Connection;
                cmd.ExecuteNonQuery();
                AllData_Connection.Close();

            }
            catch { Label_Exception.Text = "請選擇題數請用數字\n"; }


            //SqlDataSource_test.DataBind();
            GridView_testDetail.DataBind();





        }

        protected void GridView_testDetail_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
            {
                //要隱藏的欄位    
                e.Row.Cells[1].Visible = false;
            }
        }

        protected void CheckBoxList1_SelectedIndexChanged1(object sender, EventArgs e)
        {


            option_choose();
        }

        protected void GridView1_Load(object sender, EventArgs e)
        {

        }

        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {
           /* if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if(!(e.Row.Cells[5].Text ==""))
                {
                    if (Convert.ToInt32(e.Row.Cells[5].Text) == 2)
                {
                    e.Row.Cells[5].ForeColor = System.Drawing.Color.Red;
                    //-- 把第五格的資料列（記錄）"格子"，變成紅色。
                    e.Row.Cells[5].Font.Bold = true;
                }
                }
            }
            */
        }

        protected void Button_Y_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                CheckBox it = (CheckBox)GridView1.Rows[i].FindControl("CheckBoxInTo");
                it.Checked = true;
            }
            
        }

        protected void Button_N_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                CheckBox it = (CheckBox)GridView1.Rows[i].FindControl("CheckBoxInTo");
                it.Checked = false;
            }
        }
    }
}