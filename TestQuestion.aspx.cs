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
namespace CalculusObject.WebPage
{
    public partial class TestQuestion : System.Web.UI.Page
    {
        static string TestID = "";
        string studentID = "";
        static List<string> TestData = new List<string>();
        string DataBase_Language="";
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlCommand cmd;
            SqlConnection AllData_Connection = null;
            SqlDataReader DataBase_Reader;//資料庫讀取
            #region Use是否上線
             AllData_Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConnection"].ConnectionString); //宣告一個資料庫連結
            AllData_Connection.Open();//開啟AllData資料庫
            string DataBase_Language = "Select * from PersonalInformation Where Account=@Account and Passsword=@Passsword";//資料庫語法
            string MD5_A = "";
            string MD5_P = "";
            string Group = "";
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
                Group = (string)DataBase_Reader["Groups"];
            }

            AllData_Connection.Close();//開啟AllData資料庫
            if (!check)
            {
                Server.Transfer("~/Account/Login.aspx");
            }
            #endregion
            Label_group.Text = Group.Trim();


        }
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
       public class Questdata
       {
           public string testid;
           public int[] optiontotal = { 0, 0, 0, 0 };
           public int way;
           public Questdata(string testID, int A, int B, int C, int D,int Way)
           {
               this.testid = testID;
               this.optiontotal[0] = A;
               this.optiontotal[1] = B;
               this.optiontotal[2] = C;
               this.optiontotal[3] = D;
               this.way = Way;
           }
        }

       

        protected void Button_text_Click(object sender, EventArgs e)
        {
            
            List<Questdata> Qdata = new List<Questdata>();
            TestID = DropDownList1.SelectedValue;
            SqlCommand cmd;
            SqlConnection AllData_Connection = null;
            SqlDataReader DataBase_Reader;//資料庫讀取
            AllData_Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConnection"].ConnectionString); //宣告一個資料庫連結
            #region 取得ID
            AllData_Connection.Open();//開啟AllData資料庫
            DataBase_Language = "Select * from SelectedData where TextID= '" + TestID.Trim() + "'";//資料庫語法
            cmd = new SqlCommand(DataBase_Language, AllData_Connection);//使用SQL  
            DataBase_Reader = cmd.ExecuteReader();
            
            while (DataBase_Reader.Read())
            {
             
                string doNumber = (string)DataBase_Reader["doNumber"];
                int way = (int)DataBase_Reader["Way"];
                Qdata.Add(new Questdata(doNumber, 0, 0, 0, 0,way));   
            }
            DataBase_Reader.Close();
            AllData_Connection.Close();//close AllData資料庫
            #endregion
            #region 取得答題狀況
            AllData_Connection.Open();//開啟AllData資料庫
            DataBase_Language = "Select * from FloatData where TestID= '" + TestID.Trim() + "'";//資料庫語法
            cmd = new SqlCommand(DataBase_Language, AllData_Connection);//使用SQL  
            DataBase_Reader = cmd.ExecuteReader();

            while (DataBase_Reader.Read())
            {
                string Reply;
                string doNumber = (string)DataBase_Reader["QuestionID"];
                if (DataBase_Reader["Reply"] == DBNull.Value)
                {
                    Reply = "X";
                }
                else
                {
                    Reply = (string)DataBase_Reader["Reply"];
                }
              //  Reply = (string)DataBase_Reader["Reply"]==DBNull.Value? "X" : (string)DataBase_Reader["Reply"];
                for(int u=0;u<Qdata.Count;u++)
                {
                    if (doNumber.Trim() == Qdata[u].testid.Trim())
                    {
                        if (Reply == "A")
                            Qdata[u].optiontotal[0]++;
                        if (Reply == "B")
                            Qdata[u].optiontotal[1]++;
                        if (Reply == "C")
                            Qdata[u].optiontotal[2]++;
                        if (Reply == "D")
                            Qdata[u].optiontotal[3]++;
                    }
                
                }
            }
            DataBase_Reader.Close();
            AllData_Connection.Close();//close AllData資料庫
            #endregion
            for (int u = 0; u < Qdata.Count; u++)
            {

                int total=Qdata[u].optiontotal[0]+Qdata[u].optiontotal[1]+Qdata[u].optiontotal[2]+Qdata[u].optiontotal[3];
                if (total != 0)
                {
                    #region 畫圖
                    if (Qdata[u].way == 1)
                    {
                        Panel_onImage.Controls.Add(new Label() { Text = "第" + (u + 1) + "題"+ Qdata[u].testid.Trim() + "(必選)" }); 
                    }
                    else
                    {
                        Panel_onImage.Controls.Add(new Label() { Text = "第" + (u + 1) + "題"+ Qdata[u].testid.Trim() + "(隨機)" });
                    }
                    
                    Panel_onImage.Controls.Add(new Label() { Text = "<br>" });
                    Panel_onImage.Controls.Add(new System.Web.UI.WebControls.Image() { ImageUrl = "~/Images/Adaptive/" + Qdata[u].testid.Trim() + "Q.png", Width = 500 });
                    Panel_onImage.Controls.Add(new Label() { Text = "<br><HR color=#E6E8EA size=1 >" });
                    Label label = new Label();
                    label.Text = "正確解答<br>";
                    label.BackColor = System.Drawing.Color.GreenYellow;
                    label.Font.Size = 16;
                    Panel_onImage.Controls.Add(label);
                    Panel_onImage.Controls.Add(new System.Web.UI.WebControls.Image() { ImageUrl = "~/Images/Adaptive/" + Qdata[u].testid.Trim() + "A.png", Width = 200 });
                    Panel_onImage.Controls.Add(new Label() { Text = "<br>選正解人數:" + Qdata[u].optiontotal[0] + "人(" + (Qdata[u].optiontotal[0]*100 /total)+ "%)<HR color=#E6E8EA size=1 >" });
                    Label label2 = new Label();
                    label2.Text = "<br>其他選項<br>";
                    label2.BackColor = System.Drawing.Color.MediumVioletRed;
                    label2.Font.Size = 14;
                    Panel_onImage.Controls.Add(label2);
                    
                    Panel_onImage.Controls.Add(new System.Web.UI.WebControls.Image() { ImageUrl = "~/Images/Adaptive/" + Qdata[u].testid.Trim() + "B.png", Width = 200 });
                    Panel_onImage.Controls.Add(new Label() { Text = "<br>人數:" + Qdata[u].optiontotal[1] + "人(" + (Qdata[u].optiontotal[1] * 100 / total) + "%)<br><HR color=#E6E8EA size=1 >" });
                    Panel_onImage.Controls.Add(new System.Web.UI.WebControls.Image() { ImageUrl = "~/Images/Adaptive/" + Qdata[u].testid.Trim() + "C.png", Width = 200 });
                    Panel_onImage.Controls.Add(new Label() { Text = "<br>人數:" + Qdata[u].optiontotal[2] + "人(" + (Qdata[u].optiontotal[2] * 100 / total) + "%)<br><HR color=#E6E8EA size=1 >" });
                    Panel_onImage.Controls.Add(new System.Web.UI.WebControls.Image() { ImageUrl = "~/Images/Adaptive/" + Qdata[u].testid.Trim() + "D.png", Width = 200 });
                    Panel_onImage.Controls.Add(new Label() { Text = "<br>人數:" + Qdata[u].optiontotal[3] + "人(" + (Qdata[u].optiontotal[3] * 100 / total) + "%)<br><HR color=#E6E8EA size=1 >" });
                    Panel_onImage.Controls.Add(new Button() { Text = "查看答題情況" });
                    Panel_onImage.Controls.Add(new Label() { Text = "<HR color=#00FF00 size=10 >" });
                    #endregion 畫圖
                }
                else
                {
                    #region 畫圖
                    if (Qdata[u].way == 1)
                    {
                        Panel_onImage.Controls.Add(new Label() { Text = "第" + (u + 1) + "題(必選)(無人作答)" });
                    }
                    else
                    {
                        Panel_onImage.Controls.Add(new Label() { Text = "第" + (u + 1) + "題(隨機)(無人作答)" });
                    }
                    //Panel_onImage.Controls.Add(new Label() { Text = "第" + (u + 1) + "題(無人作答)"});
                    Panel_onImage.Controls.Add(new Label() { Text = "<br>" });
                    Panel_onImage.Controls.Add(new System.Web.UI.WebControls.Image() { ImageUrl = "~/Images/Adaptive/" + Qdata[u].testid.Trim() + "Q.png", Width = 500 });
                    Panel_onImage.Controls.Add(new Label() { Text = "<br>" });
                    Panel_onImage.Controls.Add(new Label() { Text = "正確解答<br>" });
                    Panel_onImage.Controls.Add(new System.Web.UI.WebControls.Image() { ImageUrl = "~/Images/Adaptive/" + Qdata[u].testid.Trim() + "A.png", Width = 200 });
                    Panel_onImage.Controls.Add(new Label() { Text = "<br>其他選項<br>" });
                    Panel_onImage.Controls.Add(new System.Web.UI.WebControls.Image() { ImageUrl = "~/Images/Adaptive/" + Qdata[u].testid.Trim() + "B.png", Width = 200 });
                    Panel_onImage.Controls.Add(new Label() { Text = "<br>" });
                    Panel_onImage.Controls.Add(new System.Web.UI.WebControls.Image() { ImageUrl = "~/Images/Adaptive/" + Qdata[u].testid.Trim() + "C.png", Width = 200 });
                    Panel_onImage.Controls.Add(new Label() { Text = "<br>" });
                    Panel_onImage.Controls.Add(new System.Web.UI.WebControls.Image() { ImageUrl = "~/Images/Adaptive/" + Qdata[u].testid.Trim() + "D.png", Width = 200 });
                    Panel_onImage.Controls.Add(new Label() { Text = "<br>" });
                    Panel_onImage.Controls.Add(new Button() { Text = "查看答題情況" });
                    Panel_onImage.Controls.Add(new Label() { Text = "<br>--------------------------------------------------------------------------------------------------------------------------------<br>" });
                    #endregion 畫圖
                }
                

            }
            //-----------------------------------------------------------------------
            List<ChapterCount> chapterlist = new List<ChapterCount>();
            AllData_Connection.Open();//開啟AllData資料庫
            DataBase_Language = "Select * from FloatData where  TestID ='" + TestID + "'";//資料庫語法
            cmd = new SqlCommand(DataBase_Language, AllData_Connection);//使用SQL  
            DataBase_Reader = cmd.ExecuteReader();
            Boolean getCH = false;
            #region 曲線取值
            while (DataBase_Reader.Read())
            {
                try {
                    getCH = true;

                    string tempChapter = (string)DataBase_Reader["Chapter"];
                    int skills = (int)DataBase_Reader["SkillsUnit"];
                    string Reply = "X";
                    if (!DataBase_Reader.IsDBNull(DataBase_Reader.GetOrdinal("Reply")))
                    { Reply = (string)DataBase_Reader["Reply"]; }
                    for (int tc = 0; tc < chapterlist.Count; tc++)
                    {
                        if (tempChapter.Trim() == chapterlist[tc].chapter)
                        {
                            getCH = false;
                            while (skills != 0)
                            {
                                int ab = skills % 10;
                                if (Reply.Trim() == "A")//2
                                {
                                    chapterlist[tc].abilityYes[ab - 1]++;
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
                catch
                { }
                

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
                Chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1;
                //------
                Title title = new Title();
                title.Text = "[" + chapterlist[tc].chapter + "] 能力答題率";
                title.Font = new System.Drawing.Font("Trebuchet MS", 14F, FontStyle.Bold);
                Chart1.Titles.Add(title);
                
                for (int U = 0; U < chapterlist[tc].abilityTotal.Length; U++)
                {
                    if (chapterlist[tc].abilityTotal[U] != 0)
                    {
                        Chart1.Series["Series1"].Points.AddXY(U + 1, chapterlist[tc].abilityYes[U] * 100 / chapterlist[tc].abilityTotal[U]);

                    }

                }
                //Chart1.Series["Series1"].IsValueShownAsLabel = true;
                for (int i = 0; i < Chart1.Series["Series1"].Points.Count; i++)
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
            int[] Score = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0};
            int nottest=0;
            int totaltest = 0;
             AllData_Connection.Open();//開啟AllData資料庫
             DataBase_Language = "Select * from ExamsList where TestID= '" + TestID.Trim() + "'";//資料庫語法
            cmd = new SqlCommand(DataBase_Language, AllData_Connection);//使用SQL  
            DataBase_Reader = cmd.ExecuteReader();

            while (DataBase_Reader.Read())
            {
                totaltest++; 
                int Sc = (int)DataBase_Reader["Score"];
                if (Sc >= 0)
                    Score[(Sc / 10)]++;
                else
                    nottest++;

                
            }
            DataBase_Reader.Close();
            AllData_Connection.Close();//close AllData資料庫

            for (int U = 0; U < Score.Length; U++)
            {
                string name = "";
                if (U == 0)
                    name = "10以下";
                else if (U == (Score.Length - 1))
                    name = "滿分";
                else
                    name = (U * 10) + "~" + ((U + 1)*10-1);
                Chart_Score.Series["Series1"].Points.AddXY(name, Score[U]);             
            }
            Chart_Score.Series["Series1"].Points.AddXY("未考", nottest);

            #region 畫圖成績
            Chart_Score.Series["Series1"].ChartType = SeriesChartType.Bar; //橫條圖
            Chart_Score.ChartAreas["ChartArea1"].AxisX.Title = "分數";
            Chart_Score.ChartAreas["ChartArea1"].AxisY.Title = "人數";
            Chart_Score.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true; //3D效果
            Chart_Score.ChartAreas["ChartArea1"].Area3DStyle.IsClustered = true; //並排顯示
            Chart_Score.ChartAreas["ChartArea1"].Area3DStyle.Rotation = 40; //垂直角度
            Chart_Score.ChartAreas["ChartArea1"].Area3DStyle.Inclination = 50; //水平角度
            Chart_Score.ChartAreas["ChartArea1"].Area3DStyle.PointDepth = 30; //數據條深度
            Chart_Score.ChartAreas["ChartArea1"].Area3DStyle.WallWidth = 0; //外牆寬度
            Chart_Score.Series["Series1"].IsValueShownAsLabel = true;
            Chart_Score.ChartAreas["ChartArea1"].BackColor = Color.FromArgb(240, 240, 240); //背景色
            Chart_Score.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineColor = Color.FromArgb(150, 150, 150);
            //X 軸線顏色
            Chart_Score.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor = Color.FromArgb(150, 150, 150);
            //Y 軸線顏色
            Chart_Score.Series["Series1"].MarkerSize = 16;
            Title title1 = new Title();
            title1.Text = "成績分布圖";
            title1.Font = new System.Drawing.Font("Trebuchet MS", 18F, FontStyle.Bold);
            Chart_Score.Titles.Add(title1);
            for (int i = 0; i < Chart_Score.Series["Series1"].Points.Count; i++)
            {
                Chart_Score.Series["Series1"].Points[i].Label = "#VALX(#VALY人," + (Chart_Score.Series["Series1"].Points[i].YValues[0]*100/totaltest).ToString("0.00")+ "%)";
            }
            #endregion
            // this.GridView_Test.Columns[3].Visible = false;
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}