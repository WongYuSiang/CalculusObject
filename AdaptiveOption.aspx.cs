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
    public partial class AdaptiveOption : System.Web.UI.Page
    {
        
        string SendChapter = "";
        /****/
        SqlConnection AllData_Connection = null;
        SqlCommand cmd;
       static List<string> testID = new List<string>();
        SqlDataReader DataBase_Reader;//資料庫讀取
        /***/
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
              //  StudentID = (string)DataBase_Reader["StudentID"];
            }
            AllData_Connection.Close();//開啟AllData資料庫
            if (!check)
            {
                Server.Transfer("~/Account/Login.aspx");
            }
            #endregion
            Label_TEXT.Text = "想要測驗的章節：";
            SendChapter = "";
            //更新頁面
            CheckBoxList_AllAbility_SelectedIndexChanged(sender, e);
            if (!IsPostBack)
            {             
            }
        }

        protected void CheckBoxList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckBoxList_AllAbility.Items.Clear();
            foreach (ListItem temp in CheckBoxList1.Items)
            {
                if (temp.Selected == true)
                { 
                    #region
               // String selectchapter = DropDownList1.SelectedValue.ToString();
                String selectchapter = temp.Value.ToString();
                //資料庫
                AllData_Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConnection"].ConnectionString); //宣告一個資料庫連結
                AllData_Connection.Open();//開啟AllData資料庫
                string DataBase_Language = "Select * from Ability";//資料庫語法
                cmd = new SqlCommand(DataBase_Language, AllData_Connection);//使用SQL  
                DataBase_Reader = cmd.ExecuteReader();

                while (DataBase_Reader.Read())
                {
                    
                    try
                    {
                        int AbilityValue = (int)DataBase_Reader["AbilityValue"];
                        string TestDataNumber = (string)DataBase_Reader[selectchapter.Trim()];
                        CheckBoxList_AllAbility.Items.Add(new ListItem("(" + selectchapter.Trim() + "-" + AbilityValue + ")" + TestDataNumber, selectchapter.Trim() + "." + AbilityValue.ToString()));
                    }
                    catch 
                    { }
                   

                }
                AllData_Connection.Close();
                #endregion
                }
                
            }
            CheckBoxList_AllAbility_SelectedIndexChanged(sender, e);
        }


        static Boolean selectAllChapter = true; 
        protected void Button_all_Click(object sender, EventArgs e)
        {
           // Server.Transfer("~/WebPage/ExamSelectedDatabase.aspx");
           
            SendChapter = "";
            for (int i = 0; i < CheckBoxList1.Items.Count; i++)
            {
                CheckBoxList1.Items[i].Selected = selectAllChapter;
               // SendChapter += CheckBoxList1.Items[i].Text;
            }
            if (selectAllChapter)
            {
                selectAllChapter = false;
                Button_all.Text = "全部取消";
            }
            else
            {
                selectAllChapter = true;
                Button_all.Text = "全部選取";
            }
            CheckBoxList1_SelectedIndexChanged(sender, e);
        }

        protected void Button_Enter_Click(object sender, EventArgs e)
        {
            SendChapter = "";
            Boolean getData = false;
            for (int i = 0; i < CheckBoxList_AllAbility.Items.Count; i++)
            {
                if (CheckBoxList_AllAbility.Items[i].Selected == true)
                {
                  //  Label_TEXT.Text += CheckBoxList_AllAbility.Items[i].Value + ",";
                    SendChapter += CheckBoxList_AllAbility.Items[i].Value + ",";
                    getData=true;
                }
            }
            if (getData)
            {
                HttpContext Context = HttpContext.Current;//宣告這個可以傳資料
                Context.Items.Add("SendChapter", SendChapter);//內建一個子資料傳值
                Server.Transfer("~/WebPage/AdaptiveTest.aspx");//換一個網站
            }
            else
            {
                Label_TEXT.Text = "沒有選屬性，請重新選擇";
            }
            
        }
        static Boolean Allselected = true;
        protected void Button_all_AllAbility_Click(object sender, EventArgs e)
        {
            

            for (int i = 0; i < CheckBoxList_AllAbility.Items.Count; i++)
            {
                CheckBoxList_AllAbility.Items[i].Selected = Allselected;

            }
            if (Allselected)
            {
                Allselected = false;
                Button_all_AllAbility.Text = "全部取消";
            
            }
            else
            {
                Allselected = true;
                Button_all_AllAbility.Text = "全部選取";
            }
            CheckBoxList_AllAbility_SelectedIndexChanged(sender, e);
        }

        protected void CheckBoxList_AllAbility_SelectedIndexChanged(object sender, EventArgs e)
        {
            Label_TEXT.Text = "想要測驗的章節：";
            SendChapter = "";
            for (int i = 0; i < CheckBoxList_AllAbility.Items.Count; i++)
            {
                if (CheckBoxList_AllAbility.Items[i].Selected == true)
                {
                    Label_TEXT.Text += CheckBoxList_AllAbility.Items[i].Value+",";
                    SendChapter += CheckBoxList_AllAbility.Items[i].Value+",";
                }
            }
            if (CheckBoxList_AllAbility.Items.Count == 0)
                Label_Nodata.Visible = true;
            else
                Label_Nodata.Visible = false;
        }

        
        

       
    }
}