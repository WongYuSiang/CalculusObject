using System;
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
    public partial class StudentSituation : System.Web.UI.Page
    {
        static string studentID = "";
        SqlConnection AllData_Connection = null;
        SqlCommand cmd;
        SqlDataReader DataBase_Reader;//資料庫讀取
        string DataBase_Language;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button_GetID_Click(object sender, EventArgs e)
        {
            /*
              SqlDataSource_stust.SelectCommand = "SELECT StudentID,Name,Class,Groups FROM PersonalInformation  WHERE (Purview = 'Student')AND(Groups='" + SelectGroup.Trim() + "') ORDER BY StudentID DESC";
            SqlDataSource_stust.DataBind();
             */
            SqlDataSource_student.SelectCommand = "SELECT StudentID, Name, Class, Groups FROM PersonalInformation WHERE StudentID like '%" + TextBox_stID.Text.Trim()+ "%'";
            SqlDataSource_student.DataBind();
           
        }

        protected void TextBox_stID_TextChanged(object sender, EventArgs e)
        {

        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlDataSource_studentDoing.SelectCommand = "SELECT  * FROM FloatData WHERE StudentID ='" + GridView1.SelectedRow.Cells[1].Text + "'";
            SqlDataSource_studentDoing.DataBind();
        }

        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectnumber = GridView2.SelectedRow.Cells[1].Text.Trim();
            Label_C.Text = "題目";
            try
            {
                //擷取Q.png資料+調整適當大小
                System.Drawing.Image image_set = System.Drawing.Image.FromFile(Server.MapPath("~/Images/Adaptive/" + selectnumber + "Q.png"));
                Image_Q.ImageUrl = "~/Images/Adaptive/" + (selectnumber + "Q.png").Trim();
                Image_Q.Width = (int)300;
                Image_Q.Height = (int)300 * image_set.Height / image_set.Width;
                //擷取A.png資料+調整適當大小
                image_set = System.Drawing.Image.FromFile(Server.MapPath("~/Images/Adaptive/" + selectnumber + "A.png"));
                Image_A.ImageUrl = "~/Images/Adaptive/" + (selectnumber + "A.png").Trim();
                Image_A.Width = (int)300;
                Image_A.Height = (int)300 * image_set.Height / image_set.Width;
                //B
                image_set = System.Drawing.Image.FromFile(Server.MapPath("~/Images/Adaptive/" + selectnumber + "B.png"));
                Image_B.ImageUrl = "~/Images/Adaptive/" + (selectnumber + "B.png").Trim();
                Image_B.Width = (int)300;
                Image_B.Height = (int)300 * image_set.Height / image_set.Width;
                //C
                image_set = System.Drawing.Image.FromFile(Server.MapPath("~/Images/Adaptive/" + selectnumber + "C.png"));
                Image_C.ImageUrl = "~/Images/Adaptive/" + (selectnumber + "C.png").Trim();
                Image_C.Width = (int)300;
                Image_C.Height = (int)300 * image_set.Height / image_set.Width;
                //D
                image_set = System.Drawing.Image.FromFile(Server.MapPath("~/Images/Adaptive/" + selectnumber + "D.png"));
                Image_D.ImageUrl = "~/Images/Adaptive/" + (selectnumber + "D.png").Trim();
                Image_D.Width = (int)300;
                Image_D.Height = (int)300 * image_set.Height / image_set.Width;

                /*取得答對率*/
                AllData_Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConnection"].ConnectionString); //宣告一個資料庫連結
                AllData_Connection.Open();//開啟AllData資料庫
                DataBase_Language = "Select * from FloatData Where QuestionID=@QuestionID and StudentID=@StudentID";//資料庫語法
                cmd = new SqlCommand(DataBase_Language, AllData_Connection);//使用SQL  
                cmd.Parameters.Add("QuestionID", SqlDbType.Char).Value = selectnumber;
                cmd.Parameters.Add("StudentID", SqlDbType.Char).Value = GridView1.SelectedRow.Cells[1].Text;
                DataBase_Reader = cmd.ExecuteReader();
                int[] reply = { 0, 0, 0, 0 };
                String re;
                while (DataBase_Reader.Read())
                {

                    re = (string)DataBase_Reader["Reply"];
                    if (re.Trim() == "A")
                        reply[0]++;
                    else if (re.Trim() == "B")
                        reply[1]++;
                    else if (re.Trim() == "C")
                        reply[2]++;
                    else
                        reply[3]++;

                    //    Purview = (string)DataBase_Reader["Purview"];
                }
                AllData_Connection.Close();//開啟AllData資料庫
                int countre = reply[0] + reply[1] + reply[2] + reply[3];

                Label_C0.Text = reply[0] + "次(" + ((reply[0] * 100) / countre).ToString() + "%)";
                Label_C1.Text = reply[1] + "次(" + ((reply[1] * 100) / countre).ToString() + "%)";
                Label_C2.Text = reply[2] + "次(" + ((reply[2] * 100) / countre).ToString() + "%)";
                Label_C3.Text = reply[3] + "次(" + ((reply[3] * 100) / countre).ToString() + "%)";
            }
            catch
            {
                Label_C.Text = "顯示錯誤請重新選擇";
            }
           
        }
    }
}