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
    

    public partial class AdaptiveTestaspx : System.Web.UI.Page
    {
        static string Purview;
        static int Diff;
        static int QDiff,QSk;
        static int LevelTotalTimes=0;
        static int LevelCorrectTimes = 0;
        static string CT;
        static string TT;
      // static int QLevel = 0; 
        static string file = "";
        static List<string> didQ = new List<string>();
        class chapterAbility
        {
            public string Chapter;
            public int[] AbilityLevel= new int [8];//等級
            public double[] Adjust = new double[8];//調整上升
             public Boolean[] SelectedTest=new Boolean[8];

            public chapterAbility(string chapter)
            {
                for (int P = 0; P < 8; P++)
                {
                    this.Chapter = chapter;
                    this.AbilityLevel[P] =-1;
                    this.Adjust[P] = 0;
                    this.SelectedTest[P]=false;
                }
                    
            }
        }
        static List<chapterAbility> StudentAbility = new List<chapterAbility>();
        static DateTime StartTime;
        static string TestID = "";
        static int  diff = 0;
        static int correctTimes=0;//本題曾經答對次數
        static int QSkillUnit;
        string studentID;
        string DataBase_Language;
        static string SelectedChapter;
        SqlCommand cmd;
        SqlConnection AllData_Connection = null;
        SqlDataReader DataBase_Reader;//資料庫讀取
        Random rand=new Random(DateTime.Now.Millisecond);
        static string[] option_set = { "D", "A", "C", "B" };//選項暫存
        
        static int minDoneTimes = -1;//本題作答次數
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
                Purview = (string)DataBase_Reader["Purview"];
            }
            AllData_Connection.Close();//開啟AllData資料庫
            if (!check)
            {
                Server.Transfer("~/Account/Login.aspx");
            }
            #endregion

            #region  一開始執行
            if (!IsPostBack)//一開始
            {
                StudentAbility.Clear();
                didQ.Clear();
                List<string> chapter = new List<string>();//章節

                #region first Get Chapter &Ability
                HttpContext req = HttpContext.Current;
                file = "";
                try
                {
                    file = req.Items["SendChapter"].ToString();
                }
                catch
                {
                    Server.Transfer("~/WebPage/AdaptiveOption.aspx");
                }

                //************************************************************
                //找出章節
                string[] temp_arr = file.Split(',');
                for (int i = 0; i < temp_arr.Length; i++)
                {
                    if (temp_arr[i] != string.Empty)
                    {
                        string[] temp_getAbility = temp_arr[i].Split('.');
                        Boolean YesGetchapter = true;
                        foreach (chapterAbility tempAbility in StudentAbility)
                        {
                            if (string.Equals(tempAbility.Chapter, temp_getAbility[0]))
                            {
                                YesGetchapter = false;
                                break;
                            }
                        }
                        if (YesGetchapter)
                        {
                            StudentAbility.Add(new chapterAbility(temp_getAbility[0]));
                        }
                    }

                }
                //************************************************************
                //擷取所有Ability
                for (int n = 0; n < StudentAbility.Count; n++)
                {
                    AllData_Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConnection"].ConnectionString); //宣告一個資料庫連結
                    AllData_Connection.Open();//開啟AllData資料庫
                    DataBase_Language = "Select * from PersonalAbility where ID = '" + studentID + "'";//資料庫語法
                    cmd = new SqlCommand(DataBase_Language, AllData_Connection);//使用SQL  
                    DataBase_Reader = cmd.ExecuteReader();
                    while (DataBase_Reader.Read())
                    {
                         string[] tempAdjust;
                        // Label_AB.Text +=chapter[o];
                        int tempAbility = 0;
                        try {
                            tempAbility = (int)DataBase_Reader[StudentAbility[n].Chapter.Trim()];
                        } catch (Exception ex)
                        {
                            Response.Redirect("~/WebPage/Error_Page.aspx");

                        }
                        
                        string AdjustStr=(string)DataBase_Reader[StudentAbility[n].Chapter.Trim() + "Adjust"];
                        
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
                    //*********************************************************
                    //找出同學挑選的屬性
                    temp_arr = file.Split(',');
                    for (int indexString = 0; indexString < temp_arr.Length; indexString++)
                    {
                        
                        if (temp_arr[indexString] != string.Empty)
                        {
                            string[] temp_getAbility = temp_arr[indexString].Split('.');
                            for (int indexChapter = 0; indexChapter < StudentAbility.Count; indexChapter++)
                            {
                                if (string.Equals(StudentAbility[indexChapter].Chapter, temp_getAbility[0]))
                                {
                                    StudentAbility[indexChapter].SelectedTest[(Convert.ToInt32(temp_getAbility[1]) - 1)] = true;
                                    break;
                                }
                            }

                        }

                    }
                #endregion
          /*          #region 刪除之前FloatData紀錄
                    //執行刪除之前紀錄
                    AllData_Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConnection"].ConnectionString); //宣告一個資料庫連結
                    AllData_Connection.Open();//開啟AllData資料庫
                    DataBase_Language = string.Format("DELETE FROM FloatData WHERE StudentID = '{0}'", studentID);
                    cmd = new SqlCommand(DataBase_Language, AllData_Connection);
                    cmd.Connection = AllData_Connection;
                    cmd.ExecuteNonQuery();//執行
                    AllData_Connection.Close();//關閉AllData資料
                    #endregion
                    //設定題目 
           * */
                    setQ();
                }
            #endregion

            }
        }
        protected void Button_Next_Click(object sender, EventArgs e)
        {
            RadioButton_Option1.Enabled = true;
            RadioButton_Option2.Enabled = true;
            RadioButton_Option3.Enabled = true;
            RadioButton_Option4.Enabled = true;
            Button_Next.Visible = false;
            Button_Check.Visible = true;
            setQ();//設定題目
        }

       
        public void setQ()//設定題目
        {
            
            TestID = "";
            minDoneTimes = -1;
            int tempDonetime = -1;
            int ChooseAbility = 0;//測試找Ability
            //初始化
            RadioButton_Option1.Checked = false;
            RadioButton_Option2.Checked = false;
            RadioButton_Option3.Checked = false;
            RadioButton_Option4.Checked = false;
            //題目代號:C05010001
            chapterAbility SelectedCA = new chapterAbility("");//選到的STUDENT 屬性
            /*選擇題目*/

            for (int minLevel = 1; minLevel <= 3; minLevel++)//不包含0((初始設定
            {
                
                /*--------------------------*/
                foreach (chapterAbility tempStudentCA in StudentAbility)//章節
                {
                    for (int indexAbility = 7; indexAbility >=0; indexAbility--)//從屬性最高開始
                    {
                        int StudentAbilityLevel = tempStudentCA.AbilityLevel[indexAbility];
                        if (StudentAbilityLevel == 0)
                            StudentAbilityLevel = 1;
                        
                        #region   挑選題目方式
                        if ((minLevel == StudentAbilityLevel) && (tempStudentCA.SelectedTest[indexAbility] == true))
                        {
                            //屬性類似+是否有選到題
                            AllData_Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConnection"].ConnectionString); //宣告一個資料庫連結
                            AllData_Connection.Open();//開啟AllData資料庫
                            SelectedCA.Chapter = tempStudentCA.Chapter;
                            SelectedCA.AbilityLevel = tempStudentCA.AbilityLevel;

                            DataBase_Language = "SELECT * FROM ExerciseData WHERE Chapter = '" + SelectedCA.Chapter.Trim() + "' AND( Difficulty=" + minLevel + "OR Difficulty=" + (minLevel+1) + " ) AND AdpterOrTest=" + 1;//資料庫語法
                            cmd = new SqlCommand(DataBase_Language, AllData_Connection);//使用SQL  
                            DataBase_Reader = cmd.ExecuteReader();
                            Boolean getQ = false;
                            while (DataBase_Reader.Read())
                            {
                                getQ = false;//體目包含是否有屬性
                                int tempSkillUnit = (int)DataBase_Reader["SkillsUnit"];
                                while (tempSkillUnit != 0)
                                {
                                    string seeID = (string)DataBase_Reader["ID"];//查看用
                                    tempDonetime = (int)DataBase_Reader["DoneTimes"];
                                    if (((tempSkillUnit % 10)) == (indexAbility+1))
                                    {
                                        getQ = true;
                                        break;
                                    }
                                    else
                                        tempSkillUnit = tempSkillUnit / 10;
                                }
                                string tempNumber = (string)DataBase_Reader["ID"];//站存ID
                                if (((minDoneTimes >= tempDonetime) || (minDoneTimes == -1)) && getQ)//找最低題數
                                {
                                    Boolean evenDidNo = true;
                                    foreach (string tempDid in didQ)
                                    {
                                        if (string.Equals(tempNumber, tempDid))
                                        {
                                            evenDidNo = false;
                                        }
                                    }
                                    if (evenDidNo)
                                    {
                                        //載入題目

                                        QSk = 0; QDiff = 0;

                                        QSk = tempSkillUnit;
                                        QDiff = Diff;
                                        Diff = minLevel;
                                        CT="User"+Diff.ToString().Trim();
                                        TT = "User" + Diff.ToString().Trim()+"Total";
                                        LevelCorrectTimes = (int)DataBase_Reader[CT];
                                        LevelTotalTimes = (int)DataBase_Reader[TT];
                                        TestID = (string)DataBase_Reader["ID"];
                                        ChooseAbility = indexAbility + 1;
                                        minDoneTimes = tempDonetime;
                                        SelectedChapter = tempStudentCA.Chapter;
                                        correctTimes = (int)DataBase_Reader["CorrectTimes"];
                                        QSkillUnit = (int)DataBase_Reader["SkillsUnit"];
                                      //  Label_Q.Text = "ID:" + TestID + "/章節:" + SelectedChapter + "/技能:" + QSkillUnit + "/做過:" + minDoneTimes + "/等級:" + minLevel;
                                        Diff = minLevel;
                                    }

                                }
                            }
                            AllData_Connection.Close();//關閉AllData資料庫 
                        }
                        #endregion
                    }
                        
                }
                if (TestID != string.Empty)
                {
                    break;
                }//準備出題
            
            }
            
            /*************************例外******************/
            if (TestID == string.Empty)//找不到題目
            {
                Response.Redirect("~/WebPage/Error_Page.aspx");
                //Server.Transfer("~/WebPage/Error_Page.aspx");
            }
            if (didQ.Count >5)//做超過N提
            {
                Button_Exit.Visible = true;
            }
            #region 顯示題目
            //*********************************************
            for (int t = 0; t < option_set.Length; t++)
                {
                    int A = t;
                    int B = rand.Next(option_set.Length);
                    string temp = option_set[A];                                                
                    option_set[A] = option_set[B];
                    option_set[B] = temp;
                }//隨機選項排序
            //Label_text.Text = chapterSelect + TestID;
             Label_text.Text = "";
            
            //圖片
            System.Drawing.Image image_set = System.Drawing.Image.FromFile(Server.MapPath("~/Images/Adaptive/" + TestID.Trim() + "Q.png"));
            Image_Ques.ImageUrl = "~/Images/Adaptive/" + TestID.Trim() + "Q.png";
            Image_Ques.Width = image_set.Width;
            Image_Ques.Height = image_set.Height;
            Image_Option1.ImageUrl = "~/Images/Adaptive/" + TestID.Trim() + option_set[0].Trim() + ".png";
            Image_Option2.ImageUrl = "~/Images/Adaptive/" + TestID.Trim() + option_set[1].Trim() + ".png";
            Image_Option3.ImageUrl = "~/Images/Adaptive/" + TestID.Trim() + option_set[2].Trim() + ".png";
            Image_Option4.ImageUrl = "~/Images/Adaptive/" + TestID.Trim() + option_set[3].Trim() + ".png";
            #endregion
            StartTime = DateTime.Now;
        }

        protected void Button_Check_Click(object sender, EventArgs e)
        {
            Boolean yesOrNo = false;
            int chooseAnser=-1; 
            Button_Next.Visible = true;
            Button_Check.Visible = false;
            Char SelectedOption='X';
            //選項設定
            if (RadioButton_Option1.Checked == true)
                chooseAnser = 0;
           else if (RadioButton_Option2.Checked == true)
                chooseAnser = 1;
            else if (RadioButton_Option3.Checked == true)
                chooseAnser = 2;
            else if (RadioButton_Option4.Checked == true)
                chooseAnser = 3;
            //確認答案
            if (chooseAnser != -1)
            {
                if (string.Equals("A", option_set[chooseAnser]))
                    yesOrNo = true;
                else
                    yesOrNo = false;
                SelectedOption= char.Parse(option_set[chooseAnser]);
            }
            else
                yesOrNo = false;
            //是否答對
            minDoneTimes += 1;
            LevelTotalTimes += 1;
            //做過題目+1
            if (yesOrNo)
            {
                Label_text.Text = "答對";
                Label_text.ForeColor = System.Drawing.Color.Green;
                correctTimes += 1;
                LevelCorrectTimes += 1;
            }
                
            else
            {
                Label_text.Text = "答錯";
                Label_text.ForeColor = System.Drawing.Color.Red;
            }
            //**
            CheckDoing(yesOrNo, QSkillUnit, SelectedChapter);
            AdjustAbility(QSkillUnit);
            //**
            if (Purview.Trim() == "student")
            {
                AllData_Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConnection"].ConnectionString); //宣告一個資料庫連結
                AllData_Connection.Open();//開啟AllData資料庫
                DataBase_Language = string.Format("UPDATE ExerciseData set DoneTimes='{0}' ,CorrectTimes='{1}' ,{2}='{3}',{4}='{5}' WHERE ID='{6}'", minDoneTimes, correctTimes, CT, LevelCorrectTimes, TT, LevelTotalTimes, TestID);
                //DataBase_Language = "update * from ExerciseData s where Chapter = '" + chapterSelect + "'";//資料庫語法
                cmd = new SqlCommand(DataBase_Language, AllData_Connection);//使用SQL  
                cmd.ExecuteNonQuery();
                AllData_Connection.Close();
            }
             //更新題目作答次數
            
            //加入FloatData
            AllData_Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConnection"].ConnectionString); //宣告一個資料庫連結
            AllData_Connection.Open();//開啟AllData資料庫
            DataBase_Language = string.Format("INSERT INTO FloatData(StudentID,QuestionID,EndTime,StartTime,Chapter,Reply,SaveOption,TestType,Difficulty,SkillsUnit)VALUES(@StudentID,@QuestionID,@EndTime,@StartTime,@Chapter,@Reply,@SaveOption,@TestType,@Difficulty,@SkillsUnit)");
            cmd = new SqlCommand(DataBase_Language, AllData_Connection);//使用SQL 
            cmd.Parameters.Add("StudentID", SqlDbType.Char).Value = studentID;
            cmd.Parameters.Add("QuestionID", SqlDbType.Char).Value = TestID;
            cmd.Parameters.Add("EndTime", SqlDbType.DateTime).Value = DateTime.Now;
            cmd.Parameters.Add("StartTime", SqlDbType.DateTime).Value = StartTime;
            cmd.Parameters.Add("Chapter", SqlDbType.Char).Value = SelectedChapter;
            cmd.Parameters.Add("Reply", SqlDbType.Char).Value = SelectedOption;
            cmd.Parameters.Add("SaveOption", SqlDbType.Char).Value = option_set[0] + option_set[1] + option_set[2] + option_set[3];
            cmd.Parameters.Add("TestType", SqlDbType.Int).Value = 2;
            cmd.Parameters.Add("Difficulty", SqlDbType.Int).Value = QDiff;
            cmd.Parameters.Add("SkillsUnit", SqlDbType.Int).Value = QSk;
            //Diff
            // cmd.Parameters.Add("TestType", SqlDbType.Int).Value = 2;
            cmd.ExecuteNonQuery();
            AllData_Connection.Close();
            /**/
            didQ.Add(TestID);
            RadioButton_Option1.Enabled = false;
            RadioButton_Option2.Enabled = false;
            RadioButton_Option3.Enabled = false;
            RadioButton_Option4.Enabled = false;
        }
        public void CheckDoing(Boolean YesORNo,int Skill,string chapter)
        {
            //需要做一個平均分給其他屬性的程式
            //也要做一個能夠調整屬性等級程式
            int tempSkill=Skill;
            int AbilityCount = 0;
            double []tempQability={0,0,0,0,0,0,0,0};
            
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
        public void AdjustAbility(int qSkillUnit)
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
                for (int K = 0; K <8; K++)
                {
                    OrigionAbilitydata += StudentAbility[indexChapter].AbilityLevel[K] * Convert.ToInt32((Math.Pow(10.0, Convert.ToDouble((7-K).ToString()))).ToString());
                    if (StudentAbility[indexChapter].Adjust[K] > 1)
                    {
                        StudentAbility[indexChapter].AbilityLevel[K]++;
                        choose = true;                                              
                        StudentAbility[indexChapter].Adjust[K] = 0;

                    }
                    else if (StudentAbility[indexChapter].Adjust[K] <-1)
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
                    Abilitydata += StudentAbility[indexChapter].AbilityLevel[K] * Convert.ToInt32((Math.Pow(10.0, Convert.ToDouble((7-K).ToString()))).ToString());
                    Adjustdata += StudentAbility[indexChapter].Adjust[K].ToString("0.0")+",";
                    
                    
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
                    ,studentID);
                cmd = new SqlCommand(DataBase_Language, AllData_Connection);//使用SQL  
                cmd.ExecuteNonQuery();
                AllData_Connection.Close();
                //**************************
            }

            if (choose)
            {
                AllData_Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConnection"].ConnectionString); //宣告一個資料庫連結
                AllData_Connection.Open();//開啟AllData資料庫
                DataBase_Language = string.Format("INSERT INTO StudentAdjust(StudentID,QuestionID,Time,Chapter,BeforeAbility,QuestionDiffculty,QuestionSkillUnit,AfterAbility)VALUES(@StudentID,@QuestionID,@Time,@Chapter,@BeforeAbility,@QuestionDiffculty,@QuestionSkillUnit,@AfterAbility)");
                cmd = new SqlCommand(DataBase_Language, AllData_Connection);//使用SQL 
                cmd.Parameters.Add("StudentID", SqlDbType.Char).Value = studentID;
                cmd.Parameters.Add("QuestionID", SqlDbType.Char).Value = TestID;
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
        }//更改能力

        protected void Button_Exit_Click(object sender, EventArgs e)
        {
            /*
           AdjustAbility();
                HttpContext Context = HttpContext.Current;//宣告這個可以傳資料
                Context.Items.Add("SendChapter", file);//內建一個子資料傳值
                Server.Transfer("~/WebPage/Learning_Page.aspx");
          
          */
            Server.Transfer("~/WebPage/AdaptiveOption.aspx");
        }
    }
}