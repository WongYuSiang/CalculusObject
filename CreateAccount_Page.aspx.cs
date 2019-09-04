using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Security;
using System.Data;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
namespace CalculusObject.WebPage
{
    public partial class CreateAccount_Page : System.Web.UI.Page
    {
        string DataBase_Language;
        SqlCommand cmd;
        SqlConnection AllData_Connection = null;
        static DateTime Begin_times = DateTime.Now;
        static DateTime End_times = DateTime.Now;

        
        protected void Page_Load(object sender, EventArgs e)
        {
           

        }

        protected void AddButton_Click(object sender, EventArgs e)
        {
            Label_error.Text = "";
           //try
           {
                string input = TextBox_AccountData.Text;
                String[] ss = Regex.Split(input, "\r\n");
                for (int i = 0; i < ss.Length; i++)
                {
                    String[] ss2 = Regex.Split(ss[i], ",");
                    if (ss2.Length ==5)
                    {
                        
                        string id = ss2[0].Trim();
                        string Name = ss2[1].Trim();
                        string Class = ss2[2].Trim();
                        string Groups = ss2[3].Trim();
                        string MD5ID = "";
                        string ClassID = ss2[4].Trim();
                        MD5ID = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(id, "MD5").ToLower();
                        AllData_Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConnection"].ConnectionString); //宣告一個資料庫連結
                        AllData_Connection.Open();//開啟AllData資料庫
                         try
                        {
                            
                            /**/
                            
                            DataBase_Language = string.Format("INSERT INTO PersonalInformation(StudentID,Name,Purview,Groups,Class,Account,Passsword,ClassID,CreateTime,Subject)VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')", id, Name, RadioButtonList_purview.SelectedValue.Trim(), Groups, Class, MD5ID, MD5ID, ClassID,DateTime.Now,"工");
                            cmd = new SqlCommand(DataBase_Language, AllData_Connection);//使用SQL  
                            cmd.ExecuteNonQuery();
                            AllData_Connection.Close();
                            AllData_Connection.Open();//開啟AllData資料庫
                            DataBase_Language = string.Format("INSERT INTO PersonalAbility(ID)VALUES('{0}')", id);
                            cmd = new SqlCommand(DataBase_Language, AllData_Connection);//使用SQL  
                            cmd.ExecuteNonQuery();
                            AllData_Connection.Close();
                            Label_error.Text = "完成匯入手續";
                            //Label_error.ForeColor = System.Drawing.Color.Green;
                           // GridView1.DataSourceID = "SqlDataSource_ACCOUNT";

                        }
                        catch
                        {
                         Label_error.Text += Name + "可能有\"重複的人\"";
                       
                        }
                        AllData_Connection.Close();
                    }

                }
                
                
           }
           
           /* catch (Exception ex)
            {
                Label_error.ForeColor = System.Drawing.Color.Red;
                Label_error.Text += Name+"("++")" + "匯入失敗，可能有\"重複的人\";
            }
            */
    
                
        }

        protected void RadioButtonList_purview_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void CheckBoxList_C_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GridView3_Load(object sender, EventArgs e)
        {

        }

        protected void GridView3_DataBound(object sender, EventArgs e)
        {
            for (int i = 0; i < GridView3.Rows.Count; i++)
            {

                if (GridView3.Rows[i].Cells[3].Text.Trim() == "assistant")
                {
                    GridView3.Rows[i].Cells[3].Text = "助教";
                }
                else if (GridView3.Rows[i].Cells[3].Text.Trim() == "teacher")
                {
                    GridView3.Rows[i].Cells[3].Text = "老師";
                }
                else
                {
                    GridView3.Rows[i].Cells[3].Text = "";


                }


            }
        }

        protected void CheckBoxList_C_SelectedIndexChanged1(object sender, EventArgs e)
        {


        }
    }
}