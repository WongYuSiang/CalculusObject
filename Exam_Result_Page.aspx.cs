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
    public partial class Exam_Result_Page1 : System.Web.UI.Page
    {
        static int LevelTotalTimes = 0;
        static int LevelCorrectTimes = 0;
        static string CT;
        static string TT;
        static int QSkillUnit;
        static string SelectedChapter;
        static int Diff;
        string DataBase_Language;
      static string TestID = "";
      static string studentID = "";
        SqlConnection AllData_Connection = null;
        SqlCommand cmd;
        SqlDataReader DataBase_Reader;//資料庫讀取
        class chapterAbility
        {
            public string Chapter;
            public int[] AbilityLevel = new int[8];//等級
            public double[] Adjust = new double[8];//調整上升
            public Boolean[] SelectedTest = new Boolean[8];

            public chapterAbility(string chapter)
            {
                for (int P = 0; P < 8; P++)
                {
                    this.Chapter = chapter;
                    this.AbilityLevel[P] = -1;
                    this.Adjust[P] = 0;
                    this.SelectedTest[P] = false;
                }

            }
        }
        static List<chapterAbility> StudentAbility = new List<chapterAbility>();
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

            #region 顯示成績
            
            if (!IsPostBack)
            {
                #region chapter
                AllData_Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConnection"].ConnectionString); //宣告一個資料庫連結
                AllData_Connection.Open();//開啟AllData資料庫
                DataBase_Language = "Select * from FloatData Where StudentID=@StudentID and TestID =@TestID";//資料庫語法

                cmd = new SqlCommand(DataBase_Language, AllData_Connection);//使用SQL  
                cmd.Parameters.Add("StudentID", SqlDbType.Char).Value = studentID;
                cmd.Parameters.Add("TestID", SqlDbType.Char).Value = TestID;
                DataBase_Reader = cmd.ExecuteReader();
                Boolean yesOrNo = false;
                while (DataBase_Reader.Read())
                {
                    SelectedChapter = (string)DataBase_Reader["Chapter"];
                   // Diff = (int)DataBase_Reader["Difficulty"];
                    Boolean YesGetchapter = true;
                    foreach (chapterAbility tempAbility in StudentAbility)
                    {
                        if (string.Equals(tempAbility.Chapter, SelectedChapter))
                        {
                            YesGetchapter = false;
                            break;
                        }
                    }
                    if (YesGetchapter)
                    {
                        StudentAbility.Add(new chapterAbility(SelectedChapter));
                    }                 
                }
                #endregion

                #region getAbility
                for (int n = 0; n < StudentAbility.Count; n++)
                {
                    AllData_Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConnection"].ConnectionString); //宣告一個資料庫連結
                    AllData_Connection.Open();//開啟AllData資料庫
                    string selAB = "Select * from PersonalAbility where ID = '" + studentID + "'";//資料庫語法
                    cmd = new SqlCommand(selAB, AllData_Connection);//使用SQL  
                    DataBase_Reader = cmd.ExecuteReader();
                    while (DataBase_Reader.Read())
                    {
                        string[] tempAdjust;
                        // Label_AB.Text +=chapter[o];
                        int tempAbility = (int)DataBase_Reader[StudentAbility[n].Chapter.Trim()];
                        string AdjustStr = (string)DataBase_Reader[StudentAbility[n].Chapter.Trim() + "Adjust"];

                        if (AdjustStr == string.Empty)
                            AdjustStr = "0,0,0,0,0,0,0,0";

                        tempAdjust = (AdjustStr).Split(',');

                        int[] level = { -1, -1, -1, -1, -1, -1, -1, -1 };
                        for (int K = 7; K >= 0; K--)
                        {
                            //有問題2016/08/10____屬性配對有7&8不好處理
                            level[K] = tempAbility % 10;
                            tempAbility = tempAbility / 10;


                        }
                        for (int K = 0; K < 7; K++)//讓他7&8排序
                        {
                            if (level[K] == -1)
                            {
                                level[K] = level[K + 1];
                                level[K + 1] = -1;
                            }//互換

                        }
                        for (int K = 0; K < 8; K++)
                        {
                            StudentAbility[n].AbilityLevel[K] = level[K];
                            if (StudentAbility[n].Adjust.Length < tempAdjust.Length)
                                StudentAbility[n].Adjust[K] = Convert.ToDouble(tempAdjust[K]);
                            else
                                StudentAbility[n].Adjust[K] = 0.0;
                        }
                    }
                }
                #endregion

                //*----------------------------
                int AllQ=0, CQ=0;
                HttpCookie mycookie = Request.Cookies["CCMATHTEST"];
                if (mycookie != null)
                {
                    TestID = mycookie.Values["textID"];
                }
                    AllData_Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConnection"].ConnectionString); //宣告一個資料庫連結
                AllData_Connection.Open();//開啟AllData資料庫
                DataBase_Language = "Select * from FloatData Where StudentID=@StudentID and TestID =@TestID";//資料庫語法
                AllQ = 0; CQ = 0;
                cmd = new SqlCommand(DataBase_Language, AllData_Connection);//使用SQL  
                cmd.Parameters.Add("StudentID", SqlDbType.Char).Value = studentID;
                cmd.Parameters.Add("TestID", SqlDbType.Char).Value = TestID;
                DataBase_Reader = cmd.ExecuteReader();
                yesOrNo = false;
                while (DataBase_Reader.Read())
                {
                    int Diff = (int)DataBase_Reader["Difficulty"];
                    SelectedChapter = (string)DataBase_Reader["Chapter"];
                    string SelectedQuestionID = (string)DataBase_Reader["QuestionID"];
                    Diff = (int)DataBase_Reader["Difficulty"];
                    Boolean YesGetchapter = true;
                    foreach (chapterAbility tempAbility in StudentAbility)
                    {
                        if (string.Equals(tempAbility.Chapter, SelectedChapter))
                        {
                            YesGetchapter = false;
                            break;
                        }
                    }
                    if (YesGetchapter)
                    {
                        StudentAbility.Add(new chapterAbility(SelectedChapter));
                    }
                    yesOrNo = false;
                    string Reply = "X";
                    if (!DataBase_Reader.IsDBNull(DataBase_Reader.GetOrdinal("Reply")))
                    { Reply = (string)DataBase_Reader["Reply"]; }


                    QSkillUnit = (int)DataBase_Reader["SkillsUnit"];
                   if (Reply.Trim() == "A")
                   {
                       CQ++;
                       yesOrNo = true;
                   }
                   AllQ++;
                   CheckDoing(yesOrNo, QSkillUnit, SelectedChapter);
                   AdjustAbility(QSkillUnit, SelectedQuestionID);
                }
                AllData_Connection.Close();
                Label_score.Text = "分數:" + (CQ * 100 / AllQ)+"分";
                //分數說明
                if (CQ * 100 / AllQ == 0)
                    Label_say.Text = "<font color=\"#802A2A\">鴨蛋0分，好好看一下題目!</font>";
                else if (CQ * 100 / AllQ < 50)
                    Label_say.Text = "<font color=\"#B0171F\">沒有及格，你還有很大進步空間~</font>";
                else if (CQ * 100 / AllQ <60)
                    Label_say.Text = "<font color=\"#FF4500\">差一點點就及格了!好可惜~ 下次會更好</font>";
                else if (CQ * 100 / AllQ < 80)
                    Label_say.Text = "<font color=\"#73B839\">有及格，還有小觀念不懂，多多練習加油!</font>";
                else if (CQ * 100 / AllQ < 90)
                    Label_say.Text = "<font color=\"#9400D3\">不錯的成績，可以看更深入的內容!</font>";
                else if (CQ * 100 / AllQ < 100)
                    Label_say.Text = "<font color=\"#5F9EA0\">差一點點就能滿分了!小編我好興奮!</font>";
                else
                    Label_say.Text = "<font color=\"#5F9EA0\">☆★☆★☆★☆★☆太逆天了!滿分!★☆★☆★☆★☆★☆</font>";
                AllData_Connection.Open();//開啟AllData資料庫

                //string update = string.Format("UPDATE FloatData set Reply='{0}' where StudentID='{1}' AND NumberQ ='{2}' AND TestType='{3}'  ", OptionSet, studentID, PersonReply, 1);
                string update = string.Format("UPDATE ExamsList set Score={0} where StudentID='{1}'  AND TestID='{2}'", (CQ * 100 / AllQ), studentID, TestID);
                cmd = new SqlCommand(update, AllData_Connection);
                cmd.Connection = AllData_Connection;
                //執行更新
                cmd.ExecuteNonQuery();
                AllData_Connection.Close();
            }
            #endregion
        }
        public void CheckDoing(Boolean YesORNo, int Skill, string chapter)
        {
            //需要做一個平均分給其他屬性的程式
            //也要做一個能夠調整屬性等級程式
            int tempSkill = Skill;
            int AbilityCount = 0;
            double[] tempQability = { 0, 0, 0, 0, 0, 0, 0, 0 };

            while (tempSkill != 0)
            {
                if (YesORNo)
                {
                    tempQability[((tempSkill % 10) - 1)] = 1;
                }
                else
                {
                    tempQability[((tempSkill % 10) - 1)] = -1;
                }
                AbilityCount++;
                tempSkill = tempSkill / 10;
            }
            for (int i = 0; i < 8; i++)
            {
                tempQability[i] = tempQability[i] / AbilityCount;
            }
            for (int indexChapter = 0; indexChapter < StudentAbility.Count; indexChapter++)
            {
                if (string.Equals(chapter, StudentAbility[indexChapter].Chapter))
                {
                    for (int i = 0; i < 8; i++)
                    {
                        StudentAbility[indexChapter].Adjust[i] = StudentAbility[indexChapter].Adjust[i] + tempQability[i];

                    }
                }

            }

        }
        public void AdjustAbility(int qSkillUnit,string qID)
        {
            Boolean choose = false;
            int OrigionAbilitydata = 0;
            string Adjustdata = "";
            int Abilitydata = 0;
            for (int indexChapter = 0; indexChapter < StudentAbility.Count; indexChapter++)
            {
                OrigionAbilitydata = 0;
                Abilitydata = 0;
                Adjustdata = "";
                for (int K = 0; K < 8; K++)
                {
                    OrigionAbilitydata += StudentAbility[indexChapter].AbilityLevel[K] * Convert.ToInt32((Math.Pow(10.0, Convert.ToDouble((7 - K).ToString()))).ToString());
                    if (StudentAbility[indexChapter].Adjust[K] > 1)
                    {
                        StudentAbility[indexChapter].AbilityLevel[K]++;
                        choose = true;
                        StudentAbility[indexChapter].Adjust[K] = 0;

                    }
                    else if (StudentAbility[indexChapter].Adjust[K] < -1)
                    {
                        choose = true;
                        StudentAbility[indexChapter].AbilityLevel[K]--;
                        StudentAbility[indexChapter].Adjust[K] = 0;
                    }

                    if (StudentAbility[indexChapter].AbilityLevel[K] > 3)
                    {
                        StudentAbility[indexChapter].AbilityLevel[K] = 3;
                    }
                    if (StudentAbility[indexChapter].AbilityLevel[K] < 0)
                    {
                        StudentAbility[indexChapter].AbilityLevel[K] = 0;
                    }
                    Abilitydata += StudentAbility[indexChapter].AbilityLevel[K] * Convert.ToInt32((Math.Pow(10.0, Convert.ToDouble((7 - K).ToString()))).ToString());
                    Adjustdata += StudentAbility[indexChapter].Adjust[K].ToString("0.0") + ",";
                    

                    //同學調整技能狀況

                }
                //******************
                AllData_Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConnection"].ConnectionString); //宣告一個資料庫連結
                AllData_Connection.Open();//開啟AllData資料庫
                //string AddNew = string.Format("INSERT INTO 資料表(欄位1, 欄位2) VALUES('{0}','{1}')", 值1, 值2);
                DataBase_Language = string.Format("update PersonalAbility set [{0}]={1},[{2}]='{3}' WHERE ID='{4}'"
                    , StudentAbility[indexChapter].Chapter.Trim()
                    , Abilitydata
                    , StudentAbility[indexChapter].Chapter.Trim() + "Adjust"
                    , Adjustdata
                    , studentID);
                cmd = new SqlCommand(DataBase_Language, AllData_Connection);//使用SQL  
                cmd.ExecuteNonQuery();
                AllData_Connection.Close();
                //**************************
            }
            #region 更改能力
            if (choose)
            {
                AllData_Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConnection"].ConnectionString); //宣告一個資料庫連結
                AllData_Connection.Open();//開啟AllData資料庫
                DataBase_Language = string.Format("INSERT INTO StudentAdjust(StudentID,QuestionID,Time,Chapter,BeforeAbility,QuestionDiffculty,QuestionSkillUnit,AfterAbility)VALUES(@StudentID,@QuestionID,@Time,@Chapter,@BeforeAbility,@QuestionDiffculty,@QuestionSkillUnit,@AfterAbility)");
                cmd = new SqlCommand(DataBase_Language, AllData_Connection);//使用SQL 
                cmd.Parameters.Add("StudentID", SqlDbType.Char).Value = studentID;
                cmd.Parameters.Add("QuestionID", SqlDbType.Char).Value = qID;
                cmd.Parameters.Add("Time", SqlDbType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add("BeforeAbility", SqlDbType.Int).Value = OrigionAbilitydata;
                cmd.Parameters.Add("Chapter", SqlDbType.Char).Value = SelectedChapter;
                cmd.Parameters.Add("AfterAbility", SqlDbType.Int).Value = Abilitydata;
                cmd.Parameters.Add("QuestionSkillUnit", SqlDbType.Char).Value = qSkillUnit;
                //cmd.Parameters.Add("BeforeAbility", SqlDbType.Int).Value = OrigionAbilitydata;
                //cmd.Parameters.Add("TestType", SqlDbType.Int).Value = 2;
                //QuestionDiffculty
                cmd.Parameters.Add("QuestionDiffculty", SqlDbType.Int).Value = Diff;
                cmd.ExecuteNonQuery();
                AllData_Connection.Close();


            }
            #endregion
        }
            
    }
}