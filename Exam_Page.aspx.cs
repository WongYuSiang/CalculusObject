using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace CalculusObject.webpage
{
    public partial class Exam_Page : System.Web.UI.Page
    {
       // int Qnow =1;//現在答的題目(1~17)
        int totalQ = 0;//總題署
        //HttpCookie mycookie;
        public class Qset
        {
            public string ID;
            public int sation=0;//0沒答
            public Qset(string QuestionID)
            {
                this.ID = QuestionID;
                this.sation = 0;
                // 0沒答
                // 1以達
            }
        }//題目設定
        Random rand = new Random(DateTime.Now.Millisecond);
        //string[] option_set = { "D", "A", "C", "B" };//選項暫存

        protected void Page_Load(object sender, EventArgs e)
        {
            Button_Finish.Attributes.Add("onclick", "return confirm('確定要離開考試嗎?');");
            if (!IsPostBack)
            {
                Session["CCMATHReplynum"] = 1;
                setQuestion();
                //  createButton();
                //   ChangeView();
               
                HttpCookie mycookie = Request.Cookies["CCMATHTEST"];
                if (mycookie != null&& mycookie.Values["textID"] != string.Empty)
                {
                    Label_CSID.Text = mycookie.Values["SID"];
                    Label_CTID.Text = mycookie.Values["textID"];
                    //Label_Count.Text= mycookie.Values["totalNumber"];
                }
                else
                {
                    if (mycookie.Values["SID"]==string.Empty)
                    {
                        Response.Write("問題:讀不到學號");
                        Response.Redirect("~/WebPage/TextOption_Page.aspx");
                    }
                    if (mycookie.Values["textID"] == string.Empty)
                    {
                        Response.Write("問題:讀不到考試內容");
                        Response.Redirect("~/WebPage/TextOption_Page.aspx");

                    }
                    else
                    { Response.Redirect("~/WebPage/TextOption_Page.aspx"); }
                    
                }
                HttpCookie TNcookie = Request.Cookies["CCMATHTESTTN"];
                if (TNcookie != null)
                {
                    Label_Count.Text = TNcookie.Values["TN"];
                }
                else
                {
                    Label_Count.Text = "15";
                    SqlConnection AllData_Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConnection"].ConnectionString); //宣告一個資料庫連結
                    SqlCommand cmd;
                    SqlDataReader DataBase_Reader;
                    AllData_Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConnection"].ConnectionString); //宣告一個資料庫連結
                    AllData_Connection.Open();//開啟AllData資料庫 
                    string DataBase_Language = "Select * from TestTimeData where TestID='"+ Label_CTID.Text.Trim() + "'";//資料庫語法
                    cmd = new SqlCommand(DataBase_Language, AllData_Connection);//使用SQL  
                    DataBase_Reader = cmd.ExecuteReader();
                    int TotalCount = 0;
                    while (DataBase_Reader.Read())
                    {
                       TotalCount =(int)DataBase_Reader["TotalCount"];
                    }
                    AllData_Connection.Close();//關閉AllData資料庫
                    Label_Count.Text = TotalCount.ToString();
                }
            }
            createButton();
            //    setQuestion();
            // createButton();
        }
        void createButton()
        {
            HttpCookie mycookie = Request.Cookies["CCMATHTEST"];
            int Qnow = Convert.ToInt32(Session["CCMATHReplynum"]);
            if (mycookie != null)
            {
                totalQ = Convert.ToInt32(Label_Count.Text);
            }
                this.Panel_button.Controls.Clear();
            for (int bt = 1; bt <= totalQ; bt++)
            {

                Button btn = new Button();
                btn.Text = bt.ToString();
                btn.Font.Size = 20;
                btn.ID = ("I"+bt).ToString().Trim();
                btn.Click += new EventHandler(Button1_Click);
                btn.CausesValidation = false;
                
                this.Panel_button.Controls.Add(btn);
            }
        }

       
        protected void Button_Next_Click(object sender, EventArgs e)
        {
            saveReply();
            //最後設定
            Session["CCMATHReplynum"] = Convert.ToInt32(Session["CCMATHReplynum"]) + 1;
            
            setQuestion();
          //  ChangeView();
            Response.Write("按鈕事件觸發：下一題");
            
        }

        

        public void createView()
        {
            int Qnow = 1;
            HttpCookie mycookie = Request.Cookies["CCMATHTEST"];
            Menu mm = (Menu)Master.FindControl("NavigationMenu");
            
            mm.Visible = false;
            
            //確認有沒有COOKIE
            if (mycookie != null)
            {
                totalQ = Convert.ToInt32(Label_Count.Text);
                //  totalQ = Convert.ToInt32(mycookie.Values["totalNumber"]);
                Qnow = Convert.ToInt32(Session["CCMATHReplynum"]);
            //    studentID = mycookie.Values["SID"];

            }
            else
            {
                Response.Redirect("~/WebPage/AdaptiveOption.aspx");
            }
            //
            

            if (Qnow >= totalQ)
                Button_Next.Visible = false;
            else
                Button_Next.Visible = true;
            if (Qnow <= 1)
                Button_before.Visible = false;
            else
                Button_before.Visible = true;

            
            int a = Convert.ToInt32(Session["CCMATHReplynum"]);
            Label_number.Text = "第" + a + "題";
            Response.Write("按鈕事件觸發");
        }//

        protected void RadioButton_Option1_CheckedChanged(object sender, EventArgs e)
        {
         
        }

        protected void Panel_button_PreRender(object sender, EventArgs e)
        {
          //  Panel_button.Controls.Clear();
           
        }

        protected void Button_before_Click(object sender, EventArgs e)
        {
            saveReply();
            
            Session["CCMATHReplynum"] = (Convert.ToInt32(Session["CCMATHReplynum"]) - 1).ToString();
     //       ChangeView();
            setQuestion();
            Response.Write("按鈕事件觸發：上一題" );
        }

        protected void Button_Finish_Click(object sender, EventArgs e)
        {
            saveReply();
            //Exam_Result_Page.aspx
            //  setQuestion();
            Response.Redirect("~/WebPage/Exam_Result_Page.aspx");
        }
        protected void Button1_Click(object sender, EventArgs e)//按上面
        {
            saveReply();
            
            //下方改題目
            // HttpCookie mycookie = Request.Cookies["CCMATHTEST"];
            int set_qn;
            Button ButtonNo = (Button)sender;
            set_qn = Convert.ToInt32(ButtonNo.Text.ToString());
            Session["CCMATHReplynum"] = (set_qn).ToString();
            
            setQuestion();
      //      ChangeView();
            Response.Write("按鈕事件觸發：" + set_qn);
        }
        public void setQuestion()//設定題目
        {
            
            
                SqlConnection AllData_Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConnection"].ConnectionString); //宣告一個資料庫連結
                SqlCommand cmd;
                SqlDataReader DataBase_Reader;
                AllData_Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConnection"].ConnectionString); //宣告一個資料庫連結
                AllData_Connection.Open();//開啟AllData資料庫
                int NQ = Convert.ToInt32(Session["CCMATHReplynum"]);

                string studentID = "";
                string TestID = "";
                string OptionSet="";//存下選項
                string TestDataNumber = "";
                string Reply="X";//存下選項
                string SaveOption = "CABD";
                HttpCookie mycookie = Request.Cookies["CCMATHTEST"];
               if (mycookie != null&& mycookie.Values["textID"]!=string.Empty)
                {

                    studentID = HttpUtility.UrlDecode(mycookie.Values["SID"]);
                    TestID = mycookie.Values["textID"];

                }
                else
                {
                    TestID = Label_CTID.Text;
                    studentID= Label_CSID.Text;
                }
                //string update = string.Format("UPDATE FloatData set Reply='{0}' where StudentID='{1}' AND NumberQ ='{2}' AND TestType='{3}'  ", OptionSet, studentID, PersonReply, 1);
                string DataBase_Language = "Select * from FloatData where NumberQ  = '" +NQ + "' AND StudentID = '" + studentID.Trim() + "'AND TestID='"+ TestID.Trim() + "'";//資料庫語法
                cmd = new SqlCommand(DataBase_Language, AllData_Connection);//使用SQL  
                DataBase_Reader = cmd.ExecuteReader();

                while (DataBase_Reader.Read())
                {
               
                        OptionSet = (string)DataBase_Reader["SaveOption"];
                        TestDataNumber = (string)DataBase_Reader["QuestionID"];
                        Reply = (string)DataBase_Reader["Reply"];
                        SaveOption = (string)DataBase_Reader["SaveOption"];
                   

                }
                AllData_Connection.Close();
                //11/9還沒取題目.選項設定好
                //string TestDataNumber = "";
                
                string[] option_set = { "D", "A", "C", "B" };
                char[] charArr = OptionSet.ToCharArray();
                for (int U = 0; U < 4; U++)//選項
                {
                    option_set[U] = charArr[U].ToString();
                }
                    ViewState["CCMathsaveOptionA"] = option_set[0];
                ViewState["CCMathsaveOptionB"] = option_set[1];
                ViewState["CCMathsaveOptionC"] = option_set[2];
                ViewState["CCMathsaveOptionD"] = option_set[3];
                RadioButton_Option1.Checked = false;
                RadioButton_Option2.Checked = false;
                RadioButton_Option3.Checked = false;
                RadioButton_Option4.Checked = false;
                Reply = Reply.Trim();
                if (Reply== option_set[0])
                    RadioButton_Option1.Checked = true;
                else if (Reply == option_set[1])
                    RadioButton_Option2.Checked = true;
                else if (Reply == option_set[2])
                    RadioButton_Option3.Checked = true;
                else if(Reply == option_set[3])
                    RadioButton_Option4.Checked = true;
               

                System.Drawing.Image image_set = System.Drawing.Image.FromFile(Server.MapPath("~/Images/Adaptive/" + TestDataNumber.Trim() + "Q.png"));
                Image_Ques.ImageUrl = "~/Images/Adaptive/" + TestDataNumber.Trim() + "Q.png";
                Image_Ques.Width = image_set.Width;
                Image_Ques.Height = image_set.Height;
                Image_Option1.ImageUrl = "~/Images/Adaptive/" + TestDataNumber.Trim() + option_set[0].Trim() + ".png";
                Image_Option2.ImageUrl = "~/Images/Adaptive/" + TestDataNumber.Trim() + option_set[1].Trim() + ".png";
                Image_Option3.ImageUrl = "~/Images/Adaptive/" + TestDataNumber.Trim() + option_set[2].Trim() + ".png";
                Image_Option4.ImageUrl = "~/Images/Adaptive/" + TestDataNumber.Trim() + option_set[3].Trim() + ".png";
            
            
           
        }
        public void saveReply()//把題目回傳
        {
            string Reply = "X";
            string studentID = "";
            string TestID = "";
            int PersonReply = 0;
            string[] option_set = { "D", "A", "C", "B" };
            option_set[0] = (String)ViewState["CCMathsaveOptionA"].ToString().Trim();
            option_set[1] = (String)ViewState["CCMathsaveOptionB"].ToString().Trim();
            option_set[2] = (String)ViewState["CCMathsaveOptionC"].ToString().Trim();
             option_set[3] = (String)ViewState["CCMathsaveOptionD"].ToString().Trim();
            PersonReply= Convert.ToInt32(Session["CCMATHReplynum"]);
            HttpCookie mycookie = Request.Cookies["CCMATHTEST"];
            //------------cookie資料
            if(RadioButton_Option1.Checked == true)
                Reply = option_set[0];
            else if (RadioButton_Option2.Checked == true)
                Reply = option_set[1];
            else if (RadioButton_Option3.Checked == true)
                Reply = option_set[2];
            else if (RadioButton_Option4.Checked == true)
                Reply = option_set[3];
            if (mycookie != null)
            {
                studentID = HttpUtility.UrlDecode(mycookie.Values["SID"]);
                TestID = Label_CTID.Text.Trim();
                studentID = Label_CSID.Text.Trim();
                //11/9還沒回傳作答以及設定saveReply/setQuestion
                SqlConnection AllData_Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConnection"].ConnectionString); //宣告一個資料庫連結
                SqlCommand cmd;
                AllData_Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConnection"].ConnectionString); //宣告一個資料庫連結
                AllData_Connection.Open();//開啟AllData資料庫



                //string update = string.Format("UPDATE FloatData set Reply='{0}' where StudentID='{1}' AND NumberQ ='{2}' AND TestType='{3}'  ", OptionSet, studentID, PersonReply, 1);
                string update = string.Format("UPDATE FloatData set Reply='{0}' where StudentID='{1}' AND NumberQ='{2}' AND TestID='{3}'", Reply, studentID, PersonReply, TestID);
                cmd = new SqlCommand(update, AllData_Connection);
                cmd.Connection = AllData_Connection;
                //執行更新
                cmd.ExecuteNonQuery();
                AllData_Connection.Close();
            }
            else
            {
                Response.Write("錯誤觸發：系統出現狀況請離開此網頁");
            }
                
        }

        protected void Image_Ques_PreRender(object sender, EventArgs e)
        {
            string studentID = "";
            string TestID = "";
            SqlConnection AllData_Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConnection"].ConnectionString); //宣告一個資料庫連結
            SqlCommand cmd;
            SqlDataReader DataBase_Reader;
            //AllData_Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConnection"].ConnectionString); //宣告一個資料庫連結
            
            createView();
            int Qnow = Convert.ToInt32(Session["CCMATHReplynum"]);
            Response.Write(this.Panel_button.Controls.Count);
            HttpCookie mycookie = Request.Cookies["CCMATHTEST"];
            /*if (mycookie != null)
            {*/

                TestID = Label_CTID.Text.Trim();
                studentID = Label_CSID.Text.Trim();

           /* }*/
            string DataBase_Language = "Select * from FloatData where StudentID = '" + studentID + "'AND TestID='" + TestID + "'";//資料庫語法
            AllData_Connection.Open();//開啟AllData資料庫
            cmd = new SqlCommand(DataBase_Language, AllData_Connection);//使用SQL  
            DataBase_Reader = cmd.ExecuteReader();

            while (DataBase_Reader.Read())
            {
                int numberQ= (int)DataBase_Reader["NumberQ"];
                string reply = (string)DataBase_Reader["Reply"];
                foreach (Button control in this.Panel_button.Controls)
                {
                    if (control.ID == ("I" + numberQ).ToString().Trim())
                    {
                        if (reply.Trim() != "X")
                        {
                            control.ForeColor = System.Drawing.Color.Black;
                            control.BackColor = System.Drawing.Color.YellowGreen;


                        }
                        else
                        {
                            control.ForeColor = System.Drawing.Color.Black;
                            control.BackColor = System.Drawing.Color.White;
                        }

                    }

                    


                }
            }
            AllData_Connection.Close();//關閉AllData資料庫
            foreach (Button control in this.Panel_button.Controls)
            {
                if (control.ID == ("I" + Qnow).ToString().Trim())
                {
                    control.ForeColor = System.Drawing.Color.White;
                    control.BackColor = System.Drawing.Color.Black;

                }



            }

        }

        protected void Panel_button_Init(object sender, EventArgs e)
        {
           
        }
    }
}