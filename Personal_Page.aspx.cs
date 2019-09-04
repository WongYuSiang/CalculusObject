using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;
using System.Data.SqlClient;
using System.Data;
using System.Threading;

namespace CalculusObject.WebPage
{
    public partial class Personal_Page : System.Web.UI.Page
    {
        class ChapterCount
        {
           public int Doingtime;
           public int Correcttime;
           public string chapter;
           public int[] abilityYes = new int[8];//答對
           public int[] abilityTotal = new int[8];//答錯
           public string[] abilityName = new string[8];//答錯
           public ChapterCount(string Chapter)
            {
                this.Doingtime = 1;
                this.chapter = Chapter;
                this.Correcttime = 0;
                for (int i = 0; i < 8; i++)
                {
                    this.abilityYes[i] = 0;
                    this.abilityTotal[i] = 0;
                    this.abilityName[i] = "";
                    //p.s持續0就是沒有選能力
                }
            }
        }
        class TimeCount
        {
            public int week;
            public DateTime Time;
            public int Doingtime;
            public TimeCount(DateTime getTime,int weeks)
            {
                this.Doingtime = 0;
                this.Time = getTime;
                this.week = weeks;
            }
        }
        SqlCommand cmd;
        SqlConnection AllData_Connection = null;
        SqlDataReader DataBase_Reader;//資料庫讀取
        string DataBase_Language;
        string StudentID = "";
        protected void Page_Load(object sender, EventArgs e)
        {

          //  ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript: ShowProgressBar(); ", true);

            #region Use是否上線
            if (Request.Cookies["CCMATH"] != null)
            {
                
                AllData_Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConnection"].ConnectionString); //宣告一個資料庫連結
                AllData_Connection.Open();//開啟AllData資料庫
                DataBase_Language = "Select * from PersonalInformation Where Account=@Account and Passsword=@Passsword";//資料庫語法
                string MD5_A = "";
                string MD5_P = "";
                string name = "";
                string Sclass = "";
                string classID = "";
                Boolean check = false;
                
                    MD5_A = Server.HtmlEncode(Request.Cookies["CCMATH"]["UserID"].ToString());
                    MD5_P = Server.HtmlEncode(Request.Cookies["CCMATH"]["Password"].ToString());
                    
                    cmd = new SqlCommand(DataBase_Language, AllData_Connection);//使用SQL  
                    cmd.Parameters.Add("Account", SqlDbType.Char).Value = MD5_A;
                    cmd.Parameters.Add("Passsword", SqlDbType.Char).Value = MD5_P;
                    DataBase_Reader = cmd.ExecuteReader();

                    while (DataBase_Reader.Read())
                    {
                        check = true;
                    Sclass= (string)DataBase_Reader["Class"];//學生本身班級
                    StudentID = (string)DataBase_Reader["StudentID"];
                    name = (string)DataBase_Reader["Name"];
                    classID = (string)DataBase_Reader["ClassID"];
                    }
                DataBase_Reader.Close();
                //cmd.Dispose();   
                DataBase_Language = "Select * from Class Where ID=@classID ";//資料庫語法
                cmd = new SqlCommand(DataBase_Language, AllData_Connection);//使用SQL  
                cmd.Parameters.Add("classID", SqlDbType.Char).Value = classID;

                string CS = "";
                DataBase_Reader = cmd.ExecuteReader();
                while (DataBase_Reader.Read())
                {
                    CS= (string)DataBase_Reader["ClassName"];
                }

                
                Label_name.Text = name;
                Label_SC.Text = Sclass;
                Label_C.Text = CS;
                AllData_Connection.Close();//開啟AllData資料庫
                if (!check)
                {
                    Server.Transfer("~/Account/Login.aspx");
                }
                
               
            }
            else
            {
                Server.Transfer("~/Account/Login.aspx");
            }
            #endregion
            if (IsPostBack)
            {
             
            }

           // ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript: HideProgressBar(); ", true);
        }
        protected new void Unload(object sender, EventArgs e)
        {
            
        }
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        public void AbilitySee(string chapter)
        {
            int[] _y = new int[] { -1,-1,-1,-1,-1,-1,-1,-1 };            
            String[] AbilityName = new String[] { "N", "N", "N", "N", "N", "N", "N", "N" };
           // Title title = _chartChapter.Titles.Add("[" + chapter + "]能力分析圖");
          //  title.Font = new System.Drawing.Font("Arial", 12, FontStyle.Bold);
           // _chartChapter.Series[0].Label = "["+chapter + "]能力分析圖";
           
            // _chartChapter.Series[0]
            #region 資料讀取-屬性能力
            AllData_Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConnection"].ConnectionString); //宣告一個資料庫連結
            AllData_Connection.Open();//開啟AllData資料庫
                    DataBase_Language = "Select * from PersonalAbility where ID = '" + StudentID + "'";//資料庫語法
                    cmd = new SqlCommand(DataBase_Language, AllData_Connection);//使用SQL  
                    DataBase_Reader = cmd.ExecuteReader();
                    while (DataBase_Reader.Read())
                    {

                        // Label_AB.Text +=chapter[o];
                        int tempAbility = (int)DataBase_Reader[chapter.Trim()];
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
            DataBase_Reader.Close();
            AllData_Connection.Close();
            #endregion
            #region 資料讀取-屬性能力名稱
            AllData_Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConnection"].ConnectionString); //宣告一個資料庫連結
                    AllData_Connection.Open();//開啟AllData資料庫
                    DataBase_Language =" Select * from Ability";//資料庫語法
                    cmd = new SqlCommand(DataBase_Language, AllData_Connection);//使用SQL  
                    DataBase_Reader = cmd.ExecuteReader();
                    
                        int tempIndex = 0;
                    while (DataBase_Reader.Read())
                    {
                        try
                        {
                            string TestDataNumber = (string)DataBase_Reader[chapter.Trim()];
                            AbilityName[tempIndex] = TestDataNumber;
                            tempIndex++;
                        }
                        catch { }
                        
                    }
                    DataBase_Reader.Close();
                    AllData_Connection.Close();
            #endregion
            Table tempTable=new Table();//表格
            Label AbilitySeeL = new Label() { Text = "[ " + chapter + " ]的能力分析<br/>" };
            PanelAB.Controls.Add(AbilitySeeL);
            //Label_AbilitySee.Text += "[ " + chapter + " ]的能力分析<br/>";
                    int totalScore = 0, AbilityScore = 0;//tS=>總分,AS=>個人
                    for (int index = 0; index < 8; index++)
            {

                /*TABLE*/
               
                TableRow row = new TableRow();
                TableCell cell1 = new TableCell();
                cell1.Text = "";
                


                TableCell cell2 = new TableCell();
                cell2.Text = "";
                TableCell cell3 = new TableCell();
                cell3.Text = "";
                //cell2.Font.Size = FontUnit.Large; //設定文字尺寸
                // cell2.ForeColor = System.Drawing.Color.Blue; //設定文字顏色
                // cell2.BackColor = System.Drawing.Color.Red; //設定文字顏色

                /**/
                if (_y[index] != -1 && AbilityName[index].Trim()!="N")
                {
                    totalScore += 3;

                    
                    //  Label_AbilitySee.Text += "&nbsp&nbsp" + (index + 1) + "." + AbilityName[index] + "  ";
                    switch (_y[index])
                    { 
                        case 0:
                            cell1.Text = "———";
                            cell2.Text = (index + 1) + "." + AbilityName[index];
                            cell3.Text = "★優先學習";
                            cell3.BackColor = System.Drawing.ColorTranslator.FromHtml("#d71345");
                            //Label_AbilitySee.Text += "&nbsp;[———]&nbsp;" + (index + 1) + "." + AbilityName[index] + "&nbsp;<font color=" + "#d71345" + ">請多多加油!</font><br/>";

                            break;
                        case 1:
                            cell1.Text = "★——";
                            cell2.Text = (index + 1) + "." + AbilityName[index];
                            cell3.Text = "還有進步空間!";
                            cell3.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff461f");
                            //Label_AbilitySee.Text += "&nbsp;[★——]&nbsp;" + (index + 1) + "." + AbilityName[index] + "&nbsp;<font color=" + "#ff461f" + ">還有進步空間!</font><br/>";
                            AbilityScore += 1;
                            break;
                        case 2:
                            cell1.Text = "★★—";
                            cell2.Text = (index + 1) + "." + AbilityName[index];
                            cell3.Text = "到達一般水準!";
                            cell3.BackColor = System.Drawing.ColorTranslator.FromHtml("#f47920");
                            // Label_AbilitySee.Text += "&nbsp;[★★—]&nbsp;" + (index + 1) + "." + AbilityName[index] + "&nbsp;<font color=" + "#f47920" + ">到達一般水準!</font><br/>";
                            AbilityScore += 2;
                            break;
                        case 3:
                            cell1.Text = "★★★";
                            cell2.Text = (index + 1) + "." + AbilityName[index];
                            cell3.Text = "Mission Complete!";
                            cell3.BackColor = System.Drawing.ColorTranslator.FromHtml("#00bc12");
                            //  Label_AbilitySee.Text += "&nbsp;[★★★]&nbsp;" + (index + 1) + "." + AbilityName[index] + "&nbsp;<font color=" + "#00bc12" + ">Mission Complete!</font><br/>";
                            AbilityScore += 3;
                            break;
                    }                                 
                }
                row.Cells.Add(cell1);
                row.Cells.Add(cell2);
                row.Cells.Add(cell3);
                tempTable.Rows.Add(row);
                PanelAB.Controls.Add(tempTable);
            }
            Label StarSeeL = new Label() { Text = "》目前得星數:" + AbilityScore + "/" + totalScore + "<br/><br/>" };
            PanelAB.Controls.Add(StarSeeL);
           // Label_AbilitySee.Text += "》目前得星數:" + AbilityScore + "/" + totalScore + "<br/><br/>";
            
        }

        protected void CheckBoxList_Chapter_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void Button_getAll_Click(object sender, EventArgs e)
        {
            
            for (int i = 0; i < CheckBoxList_Chapter.Items.Count; i++)
            {
                CheckBoxList_Chapter.Items[i].Selected = true;
               
            }
            Button_getAll.Visible = false;
            Button_NotAll0.Visible = true;
        }

        protected void Chart1_Load(object sender, EventArgs e)
        {
            foreach (var series in Chart_ChapterCount.Series)
            {
                series.Points.Clear();
            }
            foreach (var series in Chart_DoingTime.Series)
            {
                series.Points.Clear();
            }
            
            //  SqlDataSource1.SelectCommand = "Select * from PersonalInformation where StudentID = '" + StudentID + "'";
            /*產生圖表*/
            CheckBoxList_Chapter_SelectedIndexChanged(sender, e);
            //
            List<TimeCount> WeekTime = new List<TimeCount>();
            //WeekTime.Clear();
            List<ChapterCount> ChapterChart = new List<ChapterCount>();
            string Group = "";//群組
            #region 取得群組
            //找尋做過次數
            AllData_Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConnection"].ConnectionString); //宣告一個資料庫連結
            AllData_Connection.Open();//開啟AllData資料庫
            DataBase_Language = "Select Groups from PersonalInformation where StudentID = '" + StudentID + "'";//資料庫語法
            cmd = new SqlCommand(DataBase_Language, AllData_Connection);//使用SQL  
            DataBase_Reader = cmd.ExecuteReader();

            while (DataBase_Reader.Read())
            {
                Group = ((string)DataBase_Reader["Groups"]).Trim();

            }
            AllData_Connection.Close();//開啟AllData資料庫
            #endregion
            DateTime Btime = DateTime.Now;//開始時間
            DateTime Etime = DateTime.Now;//結束時間
            DateTime Temptime = DateTime.Now;//結束時間
            #region 時間設定
            //時間設定

            AllData_Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConnection"].ConnectionString); //宣告一個資料庫連結
            AllData_Connection.Open();//開啟AllData資料庫
            DataBase_Language = "Select * from Groups where ID = '" + Group + "'";//資料庫語法
            cmd = new SqlCommand(DataBase_Language, AllData_Connection);//使用SQL  
            DataBase_Reader = cmd.ExecuteReader();
            while (DataBase_Reader.Read())
            {
                Btime = ((DateTime)DataBase_Reader["Begin_time"]);
                Etime = ((DateTime)DataBase_Reader["End_time"]);
                Temptime = Btime;
            }

            AllData_Connection.Close();//開啟AllData資料庫
            int week = 0;
            while (Temptime < Etime)
            {
                week += 1;
                Temptime = Temptime.AddDays(7);
                WeekTime.Add(new TimeCount(Temptime, week));

            }
            #endregion
            #region 生成章節圖表
            /*找尋做過次數*/
            AllData_Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConnection"].ConnectionString); //宣告一個資料庫連結
            AllData_Connection.Open();//開啟AllData資料庫
            DataBase_Language = "Select * from FloatData where StudentID = '" + StudentID + "'";//資料庫語法
            cmd = new SqlCommand(DataBase_Language, AllData_Connection);//使用SQL  
            DataBase_Reader = cmd.ExecuteReader();
            Boolean Insertdata = false;
            while (DataBase_Reader.Read())
            {
                //Chapter圖表
                Insertdata = false;
                string Reply = "X";
                try
                { Reply = ((string)DataBase_Reader["Reply"]).Trim(); }
                catch
                { Reply = "X"; }

                string Chapter = ((string)DataBase_Reader["Chapter"]).Trim();
                for (int i = 0; i < ChapterChart.Count; i++)
                {
                    if (ChapterChart[i].chapter.Trim() == Chapter.Trim())
                    {
                        ChapterChart[i].Doingtime++;
                        Insertdata = true;
                        //正確率
                        if (Reply == "A")
                            ChapterChart[i].Correcttime++;
                    }
                }
                if (!Insertdata)
                {
                    ChapterChart.Add(new ChapterCount(Chapter));
                    //正確率
                    if (Reply == "A")
                        ChapterChart[ChapterChart.Count - 1].Correcttime++;
                }
                //Time圖表
                try
                {
                    DateTime gTime = ((DateTime)DataBase_Reader["EndTime"]);
                    for (int i = 0; i < WeekTime.Count; i++)
                    {
                        if (WeekTime[i].Time > gTime)
                        {
                            WeekTime[i].Doingtime++;
                            break;
                        }
                    }
                }
                catch
                { }

            }
            AllData_Connection.Close();//開啟AllData資料庫
                                       //成立章節圖表

            //
            for (int po = 0; po < ChapterChart.Count; po++)
            {
                this.Chart_ChapterCount.Series["Chapter"].Points.AddXY(ChapterChart[po].chapter, ChapterChart[po].Doingtime);
                this.Chart_ChapterCount.Series["Correct"].Points.AddXY(ChapterChart[po].chapter, ChapterChart[po].Correcttime);
                this.Chart_ChapterCount.Series["Chapter"].Points[po].AxisLabel = ChapterChart[po].chapter;
                this.Chart_ChapterCount.Series["Correct"].Points[po].Label = "答對率" + (ChapterChart[po].Correcttime * 100.0 / ChapterChart[po].Doingtime).ToString("0.00") + "%";
                //  this.Chart_ChapterCorrect.Series["Correct"].Points.AddXY(ChapterChart[po].chapter, (ChapterChart[po].Correcttime * 1 / ChapterChart[po].Doingtime));
            }
            /*
            foreach (ChapterCount temp in ChapterChart)
            {
                this.Chart_ChapterCount.Series["Chapter"].Points.AddXY(temp.chapter, temp.Doingtime);
                this.Chart_ChapterCount.Series["Correct"].Points.AddXY(temp.chapter, temp.Correcttime);

                this.Chart_ChapterCount.Series["Correct"].Points[0].Label = "答對率" + (temp.Correcttime * 1.000 / temp.Doingtime).ToString("0.00") + "%";
                this.Chart_ChapterCorrect.Series["Correct"].Points.AddXY(temp.chapter, (temp.Correcttime * 1 / temp.Doingtime));
            }
             * -*/
            //成立每周圖表
            foreach (TimeCount temp in WeekTime)
            {
                DataPoint DP = new DataPoint(temp.week, temp.Doingtime);
                DP.AxisLabel = "第" + Environment.NewLine + temp.week + Environment.NewLine + "週";
                this.Chart_DoingTime.Series["week"].Points.Add(DP);

                //this.Chart_DoingTime.Series["week"].Points[i].
            }

            #endregion
            SqlDataSource_TEST.SelectCommand = "SELECT TestContent,Score,TestID,StudentID FROM ExamsList where StudentID ='" + StudentID.Trim() + "'";
            GridView_Test.DataBind();
            for (int i = 0; i < GridView_Test.Rows.Count; i++)
            {

                if (int.Parse(GridView_Test.Rows[i].Cells[2].Text) == -1)
                {
                    GridView_Test.Rows[i].Cells[0].Enabled = false;
                    GridView_Test.Rows[i].Cells[2].Text = "-";
                }


            }



            //----------------- 

        }

        protected void GridView_Test_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string tempSelected = ((HiddenField)GridView_Test.SelectedRow.Cells[0].FindControl("HiddenField1")).Value;

            Label field = (Label)GridView_Test.Rows[GridView_Test.SelectedIndex].Cells[0].FindControl("Label3");
            string tempSelected = GridView_Test.Rows[GridView_Test.SelectedIndex].Cells[3].Text;
            List<ChapterCount> chapterlist = new List<ChapterCount>();
            AllData_Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConnection"].ConnectionString); //宣告一個資料庫連結
            AllData_Connection.Open();//開啟AllData資料庫
            DataBase_Language = "Select * from FloatData where StudentID = '" + StudentID + "' AND TestID ='" + tempSelected+"'";//資料庫語法
            cmd = new SqlCommand(DataBase_Language, AllData_Connection);//使用SQL  
            DataBase_Reader = cmd.ExecuteReader();
            Boolean getCH=false;
            #region 曲線取值
            while (DataBase_Reader.Read())
            {
                getCH=true;
                string tempChapter = (string)DataBase_Reader["Chapter"];
                int skills = (int)DataBase_Reader["SkillsUnit"];
                string Reply = "X";
                if (!DataBase_Reader.IsDBNull(DataBase_Reader.GetOrdinal("Reply")))
                { Reply = (string)DataBase_Reader["Reply"]; }
                for (int tc = 0; tc < chapterlist.Count; tc++)
                {
                    if (tempChapter.Trim() == chapterlist[tc].chapter)
                    {
                        getCH =false;
                        while (skills != 0)
                        {
                            int ab = skills % 10;
                            if (Reply.Trim() == "A")//2
                            {
                                chapterlist[tc].abilityYes[ab-1]++;
                            }
                            chapterlist[tc].abilityTotal[ab - 1]++;
                            skills = skills / 10;
                        }
                    }                
                }
                //-------------沒有章節情況下
                if (getCH == true)
                {
                    ChapterCount tempCC = new ChapterCount(tempChapter.Trim());
                   
                    while (skills != 0)
                    {
                        int ab = skills % 10;
                        if (Reply.Trim() == "A")//2
                        {
                            tempCC.abilityYes[ab - 1]++;
                        }
                        tempCC.abilityTotal[ab - 1]++;
                        skills = skills / 10;
                    }
                    chapterlist.Add(tempCC);
                }

            }
            AllData_Connection.Close();//開啟AllData資料庫
            #endregion
            // Label2.Text = tempSelected;
            #region 資料讀取-屬性能力名稱
            for (int tc = 0; tc < chapterlist.Count; tc++)
            {
                
                AllData_Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConnection"].ConnectionString); //宣告一個資料庫連結
                AllData_Connection.Open();//開啟AllData資料庫
                DataBase_Language = " Select * from Ability";//資料庫語法
                cmd = new SqlCommand(DataBase_Language, AllData_Connection);//使用SQL  
                DataBase_Reader = cmd.ExecuteReader();

                int tempIndex = 0;
                while (DataBase_Reader.Read())
                {
                    try
                    {
                        string TestDataNumber = (string)DataBase_Reader[chapterlist[tc].chapter.Trim()];
                        chapterlist[tc].abilityName[tempIndex] = TestDataNumber;
                        tempIndex++;
                    }
                    catch { }

                }
                AllData_Connection.Close();//開啟AllData資料庫

            }
            #endregion
            #region 畫圖
            for (int tc = 0; tc < chapterlist.Count; tc++)
            {
                Chart Chart1 = new Chart();
                Chart1.Width = 500;
                Chart1.Height = 300;
                Chart1.Series.Add("Series1");
                Chart1.ChartAreas.Add("ChartArea1");
                Chart1.Series["Series1"].ChartType = SeriesChartType.Bar; //橫條圖
                Chart1.Series["Series1"].IsValueShownAsLabel = true;

                //----------
                Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true; //3D效果
                Chart1.ChartAreas["ChartArea1"].Area3DStyle.IsClustered = true; //並排顯示
                Chart1.ChartAreas["ChartArea1"].Area3DStyle.Rotation = 40; //垂直角度
                Chart1.ChartAreas["ChartArea1"].Area3DStyle.Inclination = 50; //水平角度
                Chart1.ChartAreas["ChartArea1"].Area3DStyle.PointDepth = 30; //數據條深度
                Chart1.ChartAreas["ChartArea1"].Area3DStyle.WallWidth = 0; //外牆寬度
                Chart1.ChartAreas["ChartArea1"].Area3DStyle.LightStyle = LightStyle.Realistic; //光源
                Chart1.ChartAreas["ChartArea1"].BackColor = Color.FromArgb(240, 240, 240); //背景色
                Chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineColor = Color.FromArgb(150, 150, 150);
                //X 軸線顏色
                Chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor = Color.FromArgb(150, 150, 150);
                //Y 軸線顏色
                Chart1.Series["Series1"].MarkerSize = 16;
                Chart1.ChartAreas["ChartArea1"].AxisX.Title = "答題率(%)";
                Chart1.ChartAreas["ChartArea1"].AxisY.Title = "屬性";
                Chart1.ChartAreas["ChartArea1"].AxisX.Interval=1;
                //------
                Title title = new Title();
                title.Text = "[" + chapterlist[tc].chapter + "] 能力答題率";
                title.Font = new System.Drawing.Font("Trebuchet MS", 14F, FontStyle.Bold);
                Chart1.Titles.Add(title);
                
                for (int U = 0; U< chapterlist[tc].abilityTotal.Length; U++)
                {
                    if (chapterlist[tc].abilityTotal[U] != 0)
                    {
                        Chart1.Series["Series1"].Points.AddXY(U + 1, chapterlist[tc].abilityYes[U] * 100 / chapterlist[tc].abilityTotal[U]);
                        
                    }
                    
                }
                //Chart1.Series["Series1"].IsValueShownAsLabel = true;
                for(int i=0;i<Chart1.Series["Series1"].Points.Count;i++)
                {
                    Chart1.Series["Series1"].Points[i].Label = "#VALX." + chapterlist[tc].abilityName[Convert.ToInt16(Chart1.Series["Series1"].Points[i].XValue) - 1] + "(#VALY %," + chapterlist[tc].abilityYes[Convert.ToInt16(Chart1.Series["Series1"].Points[i].XValue) - 1] + "/" + chapterlist[tc].abilityTotal[Convert.ToInt16(Chart1.Series["Series1"].Points[i].XValue) - 1] + ")";

                    if (Chart1.Series["Series1"].Points[i].YValues[0] > 60)
                        Chart1.Series["Series1"].Points[i].Color = Color.Green;
                    else
                        Chart1.Series["Series1"].Points[i].Color = Color.Red;
                            ;
                }
              //  Chart1.DataBind();
                //Chart1.Series["Series1"]["BarLabelStyle"]="Top";
                Panel_chart.Controls.Add(Chart1);
            }
            #endregion
            // this.GridView_Test.Columns[3].Visible = false;
            
            
        }

        protected void GridView_Test_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[3].Visible = false;
            }
        }

        protected void GridView_Test_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void Button_view_Click(object sender, EventArgs e)
        {
          //  ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript: ShowProgressBar(); ", true);
            PanelAB.Controls.Clear();
            bool noHaveSelectChapter = true;
            //Label_AbilitySee.Text = "";
            for (int i = 0; i < CheckBoxList_Chapter.Items.Count; i++)
            {
                if (CheckBoxList_Chapter.Items[i].Selected == true)
                {
                    AbilitySee(CheckBoxList_Chapter.Items[i].Text.Trim());
                    noHaveSelectChapter = false;
                }
            }
            if (noHaveSelectChapter)
            {
                Label NotSeeL = new Label() { Text = "請在上面選擇章節了解能力狀況" };
                PanelAB.Controls.Add(NotSeeL);
                //Label_AbilitySee.Text = "請在上面選擇章節了解能力狀況";
            }
          //  ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript: HideProgressBar(); ", true);
        }

        protected void PanelAB_PreRender(object sender, EventArgs e)
        {
           
        }

        protected void PanelAB_Unload(object sender, EventArgs e)
        {
            
        }

        protected void Button_NotAll0_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < CheckBoxList_Chapter.Items.Count; i++)
            {
                CheckBoxList_Chapter.Items[i].Selected = false;

            }
            Button_getAll.Visible = true;
            Button_NotAll0.Visible = false;
        }
    }
}
