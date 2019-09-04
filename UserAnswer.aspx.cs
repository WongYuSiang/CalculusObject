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
    public partial class UserAnswer : System.Web.UI.Page
    {
        //int WantPeople;
        string DataBase_Language = "";

        protected void datepicker1_Load(object sender, EventArgs e)
        {
            DateTime D = DateTime.Now;
            datepicker1.Text = D.ToString("yyyy/MM/d");
            datepicker2.Text = D.ToString("yyyy/MM/d");
        } 

        protected void Datepicker1(object sender, EventArgs e)
        {
            if (((Button)sender).ID == "ButtonDatepicker1")
            {
                Calendar1.Visible = true;
                Calendar1Label.Text = "選擇開始日期：";

            }
            else
            {
                Calendar2.Visible = true;
                Calendar1Labe2.Text = "選擇結束日期：";
            }
        }

        protected void CalendarChange(object sender, EventArgs e)
        {

            if (((Calendar)sender).ID == "Calendar1")
            {
                datepicker1.Text = Calendar1.SelectedDate.ToString("yyyy/MM/d");
                Calendar1.Visible = false;
                Calendar1Label.Text = "";
            }
            else
            {
                datepicker2.Text = Calendar2.SelectedDate.ToString("yyyy/MM/d");
                Calendar2.Visible = false;
                Calendar1Labe2.Text = "";
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
               
                //Label1.Text = "";
                //DataBase_Language = "SELECT [ID], [Difficulty], [DoneTimes], [CorrectTimes] FROM [ExerciseData] WHERE [DoneTimes] ";
                //if (DropDownList1.Text == ">=")
                //{
                //    DataBase_Language += ">=";
                //}
                //else
                //    DataBase_Language += "<=";
                //WantPeople = int.Parse(TextBox_ID.Text);
                //DataBase_Language +=  Convert.ToString(WantPeople);
                //SqlDataSource1.SelectCommand = DataBase_Language;//"SELECT [題目編號], [難易度], [TotalAns], [CorrectAns] FROM [題目資料] WHERE [TotalAns] >5";
            }
            catch
            {
                Label1.Text = "請重新輸入數字";
            }
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int DataNum=0;            
            try
            {
                SqlConnection AllData_Connection = null;
                SqlDataReader DataBase_Reader;//資料庫讀取
                AllData_Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConnection"].ConnectionString); //宣告一個資料庫連結
                AllData_Connection.Open();//開啟AllData資料庫

                DataBase_Language = "Select COUNT(ID) from ExerciseData where DoneTimes >0";//資料庫語法
                SqlCommand cmd = new SqlCommand(DataBase_Language, AllData_Connection);//使用SQL  
                DataBase_Reader = cmd.ExecuteReader();
                String s = "";
                while (DataBase_Reader.Read())
                {
                    DataNum= (int)DataBase_Reader[0];
                }
                AllData_Connection.Close();
            }
            catch
            {
                Console.Write("資料庫連接無法連線，請從新輸入" + "\n");
            }
            String [,]DataCatch=new String[DataNum,8];
            try
            {
                SqlConnection AllData_Connection = null;
                SqlDataReader DataBase_Reader;//資料庫讀取
                AllData_Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConnection"].ConnectionString); //宣告一個資料庫連結
                AllData_Connection.Open();//開啟AllData資料庫 
                DataBase_Language = "Select ID,Difficulty,User1,User2,User3,User1Total,User2Total,User3Total from ExerciseData where DoneTimes >0";//資料庫語法
                SqlCommand cmd = new SqlCommand(DataBase_Language, AllData_Connection);//使用SQL  
                DataBase_Reader = cmd.ExecuteReader();

                int i = 0;
                while (DataBase_Reader.Read())
                {
                    DataCatch[i, 0] = (string)DataBase_Reader["ID"];
                    DataCatch[i, 1] = "" + (int)DataBase_Reader["Difficulty"];
                    DataCatch[i, 2] = "" + (int)DataBase_Reader["User1"];
                    DataCatch[i, 3] = "" + (int)DataBase_Reader["User2"];
                    DataCatch[i, 4] = "" + (int)DataBase_Reader["User3"];
                    DataCatch[i, 5] = "" + (int)DataBase_Reader["User1Total"];
                    DataCatch[i, 6] = "" + (int)DataBase_Reader["User2Total"];
                    DataCatch[i, 7] = "" + (int)DataBase_Reader["User3Total"];
                    i++;
                }
                AllData_Connection.Close();
                //Label1.Text = DataCatch[0, 0] + DataCatch[0, 1] + DataCatch[0, 2] + DataCatch[0, 3] + DataCatch[0, 4] + DataCatch[0, 5] + DataCatch[0, 6] + DataCatch[0,7];
                //Label1.Text = "" + int.Parse(DataCatch[0, 6]);
                AnswersAbnormalRate(DataCatch, DataNum);
            }
            catch
            {
                Console.Write("資料庫連接無法連線，請從新輸入" + "\n");
            }
        }
        //演算法
        private int AnswersAbnormalRate(String[,] Data, int Num)
        {
            const int Data1 = 3,Data2=9,Data3=3,Data4=6;
            int DataNum=Num;
            int i = 0;
            int[, ,] mydata = new int[DataNum, Data1, Data2];
            double[, ,] myresult = new double[DataNum, Data3, Data4];
            double[,] AAR = new double[DataNum,3];

            #region 陣列初始為0
                while (i < DataNum)
                {
                    for (int j = 0; j < Data1; j++)
                    {
                        for (int k = 0; k < Data2; k++)
                        {
                            mydata[i, j, k] = 0;
                        }
                    }
                    for (int j = 0; j < Data3; j++)
                    {
                        for (int k = 0; k < Data4; k++)
                        {
                            myresult[i, j, k] = 0;
                        }
                    }
                    for (int l = 0; l < 3; l++) {
                        AAR[i, l] = 0;
                    }
                    i++;
                }
            #endregion
            
            #region 放入值
            i = 0;
            while (i < DataNum) {
                //E
                mydata[i, 1, 0] = int.Parse(Data[i, 2]);
                mydata[i, 1, 3] = int.Parse(Data[i, 3]);
                mydata[i, 1, 6] = int.Parse(Data[i, 4]);
                mydata[i, 1, 2] = int.Parse(Data[i, 5]);
                mydata[i, 1, 5] = int.Parse(Data[i, 6]);
                mydata[i, 1, 8] = int.Parse(Data[i, 7]);
                mydata[i, 1, 1] = mydata[i, 1, 2] - mydata[i, 1, 0];
                mydata[i, 1, 4] = mydata[i, 1, 5] - mydata[i, 1, 3];
                mydata[i, 1, 7] = mydata[i, 1, 8] - mydata[i, 1, 6];
                //H
                mydata[i, 0, 0] = int.Parse(Data[i, 3]) + int.Parse(Data[i, 4]);
                mydata[i, 0, 3] = int.Parse(Data[i, 4]);
                mydata[i, 0, 2] = int.Parse(Data[i, 6]) + int.Parse(Data[i, 7]);
                mydata[i, 0, 5] = int.Parse(Data[i, 7]);
                mydata[i, 0, 1] = mydata[i, 0, 2] - mydata[i, 0, 0];
                mydata[i, 0, 4] = mydata[i, 0, 5] - mydata[i, 0, 3];
                //L
                mydata[i, 2, 3] = int.Parse(Data[i, 2]);
                mydata[i, 2, 6] = int.Parse(Data[i, 2]) + int.Parse(Data[i, 3]);
                mydata[i, 2, 5] = int.Parse(Data[i, 5]);
                mydata[i, 2, 8] = int.Parse(Data[i, 5]) + int.Parse(Data[i, 6]);
                mydata[i, 2, 4] = mydata[i, 2, 5] - mydata[i, 2, 3];
                mydata[i, 2, 7] = mydata[i, 2, 8] - mydata[i, 2, 6];

                i++;
            }
            #endregion

            #region 計算
            i=0;
            while(i<DataNum){
                //H
                myresult[i, 0, 0] = abs(Nan((double)mydata[i, 0, 0] / (double)mydata[i, 0, 2]) - 1) * Nan((double)mydata[i, 0, 0] / (double)(mydata[i, 0, 2] + mydata[i, 1, 2] + mydata[i, 2, 2]));
                myresult[i, 0, 1] = abs(Nan((double)mydata[i, 0, 1] / (double)mydata[i, 0, 2]) - 0) * Nan((double)mydata[i, 0, 1] / (double)(mydata[i, 0, 2] + mydata[i, 1, 2] + mydata[i, 2, 2]));
                myresult[i, 0, 2] = abs(Nan((double)mydata[i, 0, 3] / (double)mydata[i, 0, 5]) - 1) * Nan((double)mydata[i, 0, 3] / (double)(mydata[i, 0, 5] + mydata[i, 1, 5] + mydata[i, 2, 5]));
                myresult[i, 0, 3] = abs(Nan((double)mydata[i, 0, 4] / (double)mydata[i, 0, 5]) - 0) * Nan((double)mydata[i, 0, 4] / (double)(mydata[i, 0, 5] + mydata[i, 1, 5] + mydata[i, 2, 5]));
                myresult[i, 0, 4] = abs(Nan((double)mydata[i, 0, 6] / (double)mydata[i, 0, 8]) - 1) * Nan((double)mydata[i, 0, 6] / (double)(mydata[i, 0, 8] + mydata[i, 1, 8] + mydata[i, 2, 8]));
                myresult[i, 0, 5] = abs(Nan((double)mydata[i, 0, 7] / (double)mydata[i, 0, 8]) - 0) * Nan((double)mydata[i, 0, 7] / (double)(mydata[i, 0, 8] + mydata[i, 1, 8] + mydata[i, 2, 8]));
                //E
                myresult[i, 1, 0] = abs(Nan((double)mydata[i, 1, 0] / (double)mydata[i, 1, 2]) - 0.5) * Nan((double)mydata[i, 1, 0] / (double)(mydata[i, 0, 2] + mydata[i, 1, 2] + mydata[i, 2, 2]));
                myresult[i, 1, 1] = abs(Nan((double)mydata[i, 1, 1] / (double)mydata[i, 1, 2]) - 0.5) * Nan((double)mydata[i, 1, 1] / (double)(mydata[i, 0, 2] + mydata[i, 1, 2] + mydata[i, 2, 2]));
                myresult[i, 1, 2] = abs(Nan((double)mydata[i, 1, 3] / (double)mydata[i, 1, 5]) - 0.5) * Nan((double)mydata[i, 1, 3] / (double)(mydata[i, 0, 5] + mydata[i, 1, 5] + mydata[i, 2, 5]));
                myresult[i, 1, 3] = abs(Nan((double)mydata[i, 1, 4] / (double)mydata[i, 1, 5]) - 0.5) * Nan((double)mydata[i, 1, 4] / (double)(mydata[i, 0, 5] + mydata[i, 1, 5] + mydata[i, 2, 5]));
                myresult[i, 1, 4] = abs(Nan((double)mydata[i, 1, 6] / (double)mydata[i, 1, 8]) - 0.5) * Nan((double)mydata[i, 1, 6] / (double)(mydata[i, 0, 8] + mydata[i, 1, 8] + mydata[i, 2, 8]));
                myresult[i, 1, 5] = abs(Nan((double)mydata[i, 1, 7] / (double)mydata[i, 1, 8]) - 0.5) * Nan((double)mydata[i, 1, 7] / (double)(mydata[i, 0, 8] + mydata[i, 1, 8] + mydata[i, 2, 8]));
                //L
                myresult[i, 2, 0] = abs(Nan((double)mydata[i, 2, 0] / (double)mydata[i, 2, 2]) - 0) * Nan((double)mydata[i, 2, 0] / (double)(mydata[i, 0, 2] + mydata[i, 1, 2] + mydata[i, 2, 2]));
                myresult[i, 2, 1] = abs(Nan((double)mydata[i, 2, 1] / (double)mydata[i, 2, 2]) - 1) * Nan((double)mydata[i, 2, 1] / (double)(mydata[i, 0, 2] + mydata[i, 1, 2] + mydata[i, 2, 2]));
                myresult[i, 2, 2] = abs(Nan((double)mydata[i, 2, 3] / (double)mydata[i, 2, 5]) - 0) * Nan((double)mydata[i, 2, 3] / (double)(mydata[i, 0, 5] + mydata[i, 1, 5] + mydata[i, 2, 5]));
                myresult[i, 2, 3] = abs(Nan((double)mydata[i, 2, 4] / (double)mydata[i, 2, 5]) - 1) * Nan((double)mydata[i, 2, 4] / (double)(mydata[i, 0, 5] + mydata[i, 1, 5] + mydata[i, 2, 5]));
                myresult[i, 2, 4] = abs(Nan((double)mydata[i, 2, 6] / (double)mydata[i, 2, 8]) - 0) * Nan((double)mydata[i, 2, 6] / (double)(mydata[i, 0, 8] + mydata[i, 1, 8] + mydata[i, 2, 8]));
                myresult[i, 2, 5] = abs(Nan((double)mydata[i, 2, 7] / (double)mydata[i, 2, 8]) - 1) * Nan((double)mydata[i, 2, 7] / (double)(mydata[i, 0, 8] + mydata[i, 1, 8] + mydata[i, 2, 8]));
                //-----H_E_L_AAR_ADD

                double[] re = new double[3];
                for (int j = 0; j < 3; j++){
                    AAR[i, j] = Nan(myresult[i, 0, 0 + j * 2]) + Nan(myresult[i, 0, 1 + j * 2]) + Nan(myresult[i, 1, 0 + j * 2]) + Nan(myresult[i, 1, 1 + j * 2]) + Nan(myresult[i, 2, 0 + j * 2]) + Nan(myresult[i, 2, 1 + j * 2]);
                    re[j] = AAR[i,j];
                }
                //加入歷史紀錄
                if(Lv(re)!=int.Parse(Data[i, 1])&&Lv(re)<4){
                    int Total=int.Parse(Data[i, 5])+int.Parse(Data[i, 6])+int.Parse(Data[i, 7]);
                    AddDataBase(Data[i, 0], int.Parse(Data[i, 1]), Lv(re),Total);
                }
                //Label1.Text=""+Lv(re);
                i++;
            }
            #endregion

            return 0;
        }

        //紀錄更改的資料
        private void AddDataBase(String name, int Diff, int ChangeDiff, int DoneTimes)
        {
            try
            {
                SqlConnection AllData_Connection = null;
                AllData_Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConnection"].ConnectionString); //宣告一個資料庫連結
                DateTime Date = DateTime.Now;
                //String AddNew = ;//資料庫語法
                SqlCommand cmd = new SqlCommand("Insert into HistoryAbilityChanges(ID,OriginDiff,NewDiff,Date,DoneTimes) Values(@Name,@Dif,@CDif,@date,@Total)", AllData_Connection);//使用SQL  
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Dif", Diff);
                cmd.Parameters.AddWithValue("@CDif", ChangeDiff);
                cmd.Parameters.AddWithValue("@date", Date);
                cmd.Parameters.AddWithValue("@Total", DoneTimes);

                AllData_Connection.Open();//開啟AllData資料庫
                cmd.ExecuteNonQuery();
                AllData_Connection.Close();//關閉AllData資料庫
                //UPDATE
                cmd = new SqlCommand("Update ExerciseData set Difficulty=@CDif where ID=@Name", AllData_Connection);//使用SQL
                cmd.Parameters.AddWithValue("@CDif", ChangeDiff);
                cmd.Parameters.AddWithValue("@Name", name);
                
                AllData_Connection.Open();//開啟AllData資料庫
                cmd.ExecuteNonQuery();
                AllData_Connection.Close();//關閉AllData資料庫

            }
            catch
            {
                Console.Write("資料庫連接無法連線，請從新輸入" + "\n");
            }
        }

        private double abs(double Num){
            if (Num >= 0)
                return Num;
            else
                return (-Num);
        }

        private double Nan(double Num){
            if (double.IsNaN(Num))
                return 0;
            else
                return Num;
        }

        private int Lv(double []Num) {
            double small=1;
            int N = 0,Zero=0;
            for (int i = 0; i < 3; i++) {
                if (Num[i] < small){
                    small = Num[i];
                    N = i + 1;
                }
                if (small == 0)
                    Zero++;
            }
            if (Zero >1)
                return 4;
            else
                return N;
        }

        protected void DateButtonClick(object sender, EventArgs e)
        {
            if (DateTime.Compare(Convert.ToDateTime(datepicker1.Text), Convert.ToDateTime(datepicker2.Text)) > 0)
            {
                //Response.Write("<Script language='JavaScript'>alert('Y2J測試！');</Script>");
                //Label1.Text=">";
                Response.Write("<script>alert('日期格式有誤！！')</script>");
            }
            else
            {
                try
                {
                    SqlConnection AllData_Connection = null;
                    AllData_Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConnection"].ConnectionString); //宣告一個資料庫連結
                    //String AddNew = ;//資料庫語法
                    DataBase_Language = "SELECT [ID], [OriginDiff], [NewDiff], [Date], [DoneTimes] FROM [HistoryAbilityChanges] WHERE ([Date] >=";
                    DataBase_Language += "'" + datepicker1.Text + " 00:00:00.000') AND ([Date] <= '" + datepicker2.Text + " 23:59:59.999')";

                    SqlDataSource1.SelectCommand = DataBase_Language;
                    //SqlCommand cmd = new SqlCommand("Select HistoryAbilityChanges(ID,OriginDiff,NewDiff,Date,DoneTimes) Values(@Name,@Dif,@CDif,@date,@Total)", AllData_Connection);//使用SQL  
                    //SqlCommand cmd = new SqlCommand(DataBase_Language, AllData_Connection);

                    //AllData_Connection.Open();//開啟AllData資料庫
                    //cmd.ExecuteNonQuery();
                    //AllData_Connection.Close();//關閉AllData資料庫

                }
                catch
                {
                    Console.Write("資料庫連接無法連線，請從新輸入" + "\n");
                }
            }
        }
    
    }
}