using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;


namespace CalculusObject.WebPage.exam
{
    public partial class inexam : System.Web.UI.Page
    {
        
        string DataBase_Language;
        Random rand = new Random(DateTime.Now.Millisecond);
        protected void Page_Load(object sender, EventArgs e)
        {    //初始化
            if (!IsPostBack)
            {
                string TestID="";
                string studentID = "";
                int QuestionSet=0;
                HttpCookie mycookie = Request.Cookies["CCMATHTEST"];
                if (mycookie != null)
                {
                    Label_ID.Text = mycookie.Values["SID"];
                    studentID= mycookie.Values["SID"];
                    Label_class.Text = HttpUtility.UrlDecode(mycookie.Values["Name"]);
                    Label_textId.Text =mycookie.Values["textID"];
                    TestID = HttpUtility.UrlDecode(mycookie.Values["textID"]);
                    Label_context.Text = HttpUtility.UrlDecode(mycookie.Values["Context"]);
                   
                    //Label_Qnum.Text = mycookie.Values["totalNumber"];
                   // QuestionSet = Convert.ToInt16(Label_Qnum.Text.Trim());

                }
                else
                {
                    Server.Transfer("~/WebPage/TestQuestion.aspx");
                }
                //------------------------------------
                #region 題目設定
                if (TestID != string.Empty)
                {
                    SqlConnection AllData_Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConnection"].ConnectionString); //宣告一個資料庫連結
                    SqlCommand cmd;
                    SqlDataReader DataBase_Reader;
                    AllData_Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConnection"].ConnectionString); //宣告一個資料庫連結
                    AllData_Connection.Open();//開啟AllData資料庫



                    #region 刪除
                    /*刪除*/                  
                    DataBase_Language = string.Format("DELETE FROM FloatData WHERE StudentID = '{0}' AND TestID='{1}'", studentID, TestID);
                    cmd = new SqlCommand(DataBase_Language, AllData_Connection);//使用SQL  
                   
                    cmd.Connection = AllData_Connection;
                    cmd.ExecuteNonQuery();
                    #endregion
                    /*---*/
                    #region 找題目
                    /*找題目*/
                    Response.Write("ID：" + TestID);
                   // TestID = TestID.Trim();
                    DataBase_Language = "Select * from SelectedData where TextID = '" + TestID.Trim() + "'";//資料庫語法
                    cmd = new SqlCommand(DataBase_Language, AllData_Connection);//使用SQL  
                    DataBase_Reader = cmd.ExecuteReader();
                    List<string> TestData = new List<string>();
                    List<string> RandData = new List<string>();
                    //初始化
                    TestData.Clear();
                    RandData.Clear();
                    while (DataBase_Reader.Read())
                    {
                        int Way = (int)DataBase_Reader["Way"];
                        string doNumber = (string)DataBase_Reader["doNumber"];
                        switch (Way)
                        {

                            case 1://必出題目                                
                                TestData.Add(doNumber);
                                break;
                            case 2://隨機選擇
                                RandData.Add(doNumber);
                                break;

                        }
                       

                    }
                    DataBase_Reader.Close();
                    #endregion
                    
                    int index = 0;
                    string temp = "";
                    int RandCount = 0;
                    int TotalCount = 0;
                    

                    try
                    {
                        //隨機打亂
                        for (int i = 0; i < RandData.Count; i++)
                        {
                            index = rand.Next(0, RandData.Count - 1);
                            if (index != i)
                            {
                                temp = RandData[i];
                                RandData[i] = RandData[index];
                                RandData[index] = temp;
                            }

                        }
                        
                        DataBase_Language = "Select * from TestTimeData where TestID= '" + TestID.Trim() + "'";//資料庫語法
                        cmd = new SqlCommand(DataBase_Language, AllData_Connection);//使用SQL  
                        DataBase_Reader = cmd.ExecuteReader();

                        while (DataBase_Reader.Read())
                        {
                            RandCount = (int)DataBase_Reader["RandCount"];
                            TotalCount = (int)DataBase_Reader["TotalCount"];
                            QuestionSet = TotalCount;
                        }
                        DataBase_Reader.Close();
                        //加入隨機選題目
                        for (int tempRandCount = 0; tempRandCount < RandCount; tempRandCount++)
                        {
                            if (TestData.Count < QuestionSet)
                                TestData.Add(RandData[tempRandCount].Trim());

                        }
                        //一一把題目匯入 
                        string[] option_set = { "C", "B", "D", "A" };// 選項
                        string OptionSet = "";//打亂後存入選項
                        int diff = 0;//困難度
                        string TestDataNumber = "";
                        int SKill=0;
                        //每一題設定
                        for (int Ques = 1; Ques <= TestData.Count; Ques++)
                        {
                            //選項打亂
                            for (int t = 0; t < option_set.Length; t++)
                            {
                                int A = t;
                                int B = rand.Next(option_set.Length);
                                string temp_S = option_set[A];
                                option_set[A] = option_set[B];
                                option_set[B] = temp_S;
                            }
                            OptionSet = option_set[0] + option_set[1] + option_set[2] + option_set[3];
                            TestDataNumber = TestData[Ques - 1];//實際作答題號
                            DataBase_Language = "Select * from ExerciseData where ID = '" + TestDataNumber + "'";//資料庫語法
                            cmd = new SqlCommand(DataBase_Language, AllData_Connection);//使用SQL  
                            DataBase_Reader = cmd.ExecuteReader();
                            string Chapter = "";
                            while (DataBase_Reader.Read())
                            {
                                SKill = (int)DataBase_Reader["SkillsUnit"];
                                Chapter = (string)DataBase_Reader["Chapter"];
                                diff = (int)DataBase_Reader["Difficulty"];
                            }

                            DataBase_Reader.Close();//停用上一個CMD用法
                                                    //
                                                    //載入到浮動資料
                            char Reply = 'X';
                            string AddNew = string.Format("INSERT INTO FloatData(StudentID, QuestionID,SaveOption,NumberQ,TestType,Chapter,TestID,SkillsUnit,Difficulty,Reply) VALUES('{0}','{1}','{2}',{3},{4},'{5}','{6}','{7}','{8}','{9}')", studentID, TestDataNumber, OptionSet, Ques, 1, Chapter, TestID, SKill, diff,'X');//要改
                            cmd = new SqlCommand(AddNew, AllData_Connection);//使用SQL  
                            cmd.Connection = AllData_Connection;
                            cmd.ExecuteNonQuery();
                            DataBase_Reader.Close();
                        }


                        //cookie 更新.
                        HttpCookie CCcookie = new HttpCookie("CCMATHTESTTN");

                        CCcookie.Values.Add("TN", TestData.Count.ToString());
                        CCcookie.Expires = DateTime.Now.AddDays(5);
                       // Response.Cookies["CCMATHTESTTN"].Domain = "";
                        Response.Cookies.Add(CCcookie);
                        Response.AppendCookie(CCcookie);
                       // Response.Redirect("~/WebPage/Exam_Page.aspx");
                        // Response.Cookies["CCMATHTEST"].Values["totalNumber"] = TestData.Count.ToString();
                        // mycookie.Values[]= TestData.Count.ToString();
                        Label_Qnum.Text = CCcookie.Values["TN"];
                    }
                    catch
                    {

                    }
                    AllData_Connection.Close();//開啟AllData資料庫
                }
                else
                {
                    Server.Transfer("~/WebPage/AdaptiveOption.aspx");
                }

                #endregion

               
            }

        }

        protected void Button_in_Click(object sender, EventArgs e)
        {
            HttpCookie mycookie = Request.Cookies["CCMATHTEST"];
            Response.Cookies.Add(mycookie);//加入cookie
                                           //mycookie.Expires = DateTime.Now.AddDays(2);
            HttpCookie CCcookie = Request.Cookies["CCMATHTESTTN"];
            if (CCcookie != null)
            {
                Response.Cookies.Add(CCcookie);
                Response.Redirect("~/WebPage/Exam_Page.aspx");
            }
            else
            {
                HttpCookie cCCcookie = new HttpCookie("CCMATHTESTTN");
                cCCcookie.Values.Add("TN", Label_Qnum.Text);
                cCCcookie.Expires = DateTime.Now.AddDays(5);
                // Response.Cookies["CCMATHTESTTN"].Domain = "";
                Response.Cookies.Add(cCCcookie);

                Response.Redirect("~/WebPage/Exam_Page.aspx");
            }
           /* //CCcookie.Values.Add("TN", TestData.Count.ToString());
            CCcookie.Expires = DateTime.Now.AddDays(5);
            //Response.Cookies["CCMATHTESTTN"].Domain = "";
            Response.Cookies.Add(CCcookie);
            Response.AppendCookie(CCcookie);
            // Response.Redirect("~/WebPage/Exam_Page.aspx");
            // Response.Cookies["CCMATHTEST"].Values["totalNumber"] = TestData.Count.ToString();
            // mycookie.Values[]= TestData.Count.ToString();
            Label_Qnum.Text = CCcookie.Values["TN"];*/
            
        }
    }
}