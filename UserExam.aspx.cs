using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Text.RegularExpressions;

namespace CalculusObject.WebPage
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        //Array.Resize(ref myArr,myArr.Length +5);
        //設定遞迴最多10次
        int MAX = 15;
        SqlConnection AllData_Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBaseConnection"].ConnectionString); //宣告一個資料庫連結
        SqlDataReader DataBase_Reader;//資料庫讀取
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //計算
        protected void Button1Click(object sender, EventArgs e)
        {
            int i,j;
            int ChaptNum=0;  //章節數量

            AllData_Connection.Open();//開啟AllData資料庫

            //ID,SkillsUnit,Difficulty,Chapter,Correct,StudentID,HaveDo
            #region 抓取不同章節
            SqlCommand cmd = new SqlCommand("Select Count(Distinct Chapter) From ExtraExamHistory Where HaveDo='false'", AllData_Connection);//使用SQL  
            try
            {
                DataBase_Reader = cmd.ExecuteReader();//執行
                DataBase_Reader.Read();//讀取
                ChaptNum = (int)DataBase_Reader[0];
                DataBase_Reader.Close();//關閉
            }
            catch { Console.Write("資料庫連接無法連線，請從新輸入" + "\n"); }
            #endregion
            String[] Chapt = new String[ChaptNum];//存放章節

            #region 抓取各章節
            cmd = new SqlCommand("Select Distinct Chapter From ExtraExamHistory Where HaveDo='false'", AllData_Connection);//使用SQL  
            try
            {
                DataBase_Reader = cmd.ExecuteReader();//執行
                i=0;
                while(DataBase_Reader.Read()){
                    Chapt[i]=""+(String)DataBase_Reader["Chapter"];
                    i++;
                }
                DataBase_Reader.Close();
            }
            catch { Console.Write("資料庫連接無法連線，請從新輸入" + "\n"); }
            #endregion

            #region 各章節計算

            for (i = 0; i < Chapt.Length; i++)
            {
                ActionChaptCalculation(Chapt[i]);
            }

            #endregion

            AllData_Connection.Close();//關閉AllData資料庫
        }

        //針對各章節的計算
        private void ActionChaptCalculation(String Chapt)
        {
            int IDNum = 0,StudentNum=0,i,j;            
            
            #region 抓取題目數量
            
                SqlCommand cmd = new SqlCommand("Select Count(Distinct ID) From ExtraExamHistory Where HaveDo='false'", AllData_Connection);//使用SQL  
                try
                {
                    DataBase_Reader = cmd.ExecuteReader();//執行
                    DataBase_Reader.Read();//讀取
                    IDNum = (int)DataBase_Reader[0];
                    DataBase_Reader.Close();//關閉
                }
                catch { Console.Write("資料庫連接無法連線，請從新輸入" + "\n"); }
                
            #endregion

            #region 抓取學生數量

                cmd = new SqlCommand("Select Count(Distinct StudentID) From ExtraExamHistory Where Chapter='"+Chapt+"'AND HaveDo='false'", AllData_Connection);//使用SQL  
                try
                {
                    DataBase_Reader = cmd.ExecuteReader();//執行
                    DataBase_Reader.Read();//讀取
                    StudentNum = (int)DataBase_Reader[0];
                    DataBase_Reader.Close();//關閉
                }
                catch { Console.Write("資料庫連接無法連線，請從新輸入" + "\n"); }
    
            #endregion

            #region 創造計算表格
                //要遞迴的題目資料,原本8,多一格放技能
                String[,] IDData = new String[IDNum, 9];
                //要遞迴的學生資料_學生學號,技能等級(共10格,其餘為題目答對錯)
                String[,] StudentData=new String[StudentNum,IDNum+10];
    
                //放入題目資料
                cmd = new SqlCommand("Select Distinct ID,SkillsUnit,Difficulty From ExtraExamHistory Where Chapter='" + Chapt + "'AND HaveDo='false' ORDER BY ID", AllData_Connection);//使用SQL  
                try
                {
                    DataBase_Reader = cmd.ExecuteReader();//執行
                    i = 0;
                    while(DataBase_Reader.Read())//讀取
                    {
                        IDData[i, 0] = (string)DataBase_Reader["ID"];
                        IDData[i, 1] = "" + (int)DataBase_Reader["Difficulty"];
                        IDData[i, 8] = "" + (int)DataBase_Reader["SkillsUnit"];
                        i++;
                    }
                    DataBase_Reader.Close();//關閉
                }
                catch { Console.Write("資料庫連接無法連線，請從新輸入" + "\n"); }
                
                //放入學生資料
                cmd = new SqlCommand("Select Distinct StudentID, Correct, ID From ExtraExamHistory Where Chapter='" + Chapt + "'AND HaveDo='false' ORDER BY StudentID, ID", AllData_Connection);//使用SQL  
                try
                {
                    DataBase_Reader = cmd.ExecuteReader();//執行
                    i = -1;
                    j = 0;
                    String text = "";
                    while (DataBase_Reader.Read())//讀取
                    {
                        if (text != (string)DataBase_Reader["StudentID"])
                        {
                            i++;
                            StudentData[i, 0] = (string)DataBase_Reader["StudentID"];
                            text = StudentData[i, 0];
                            j=0;
                        }
                        StudentData[i, j + 10] = ""+(int)DataBase_Reader["Correct"];
                        j++;
                    }
                    DataBase_Reader.Close();//關閉
                }
                catch { Console.Write("資料庫連接無法連線，請從新輸入" + "\n"); }
                
            #endregion

            #region 計算初始學生能力值
                CalculationStudentsAbility(ref IDData, ref StudentData);
            #endregion

                #region 開始遞迴
                bool a = AnswersAbnormalRate(IDData, StudentData, 0);
                #endregion

        }

        //計算學生能力值及題目不同難易度的人數
        private void CalculationStudentsAbility(ref String[,] IDData, ref String[,] StudentData)
        {
            int i, j, k;
            //存放學生10個能力值的3個等級數量
            double[,] Calculation = new double[10, 3];
            //存放題目10個能力值的3個等級數量
            double[,] CalculationNum = new double[10, 3];

            #region 初始存放題目陣列
                for (j = 0; j < 10; j++)
                {
                    for (k = 0; k < 3; k++)
                        CalculationNum[j, k] = 0;
                }
            #endregion

            #region 計算題目能力值得總數量
                for (i = 0; i < IDData.GetLength(0); i++)
                {
                    String a = IDData[i, IDData.GetLength(1)-1];
                    Char[] aa = a.ToCharArray();
                    for (j = 0; j < aa.Length; j++)
                    {
                        CalculationNum[Convert.ToInt32(aa[j].ToString()), Convert.ToInt32(IDData[i, 1].ToString())-1] += 1;
                    }
                }
            #endregion
            //每位學生
            for (i = 0; i < StudentData.GetLength(0); i++)
            {
                #region 初始存放學生的陣列
                    for (j = 0; j < 10; j++)
                    {
                        for (k = 0; k < 3; k++)
                        {
                            Calculation[j, k] = 0;
                        }
                    }
                #endregion
                #region 計算各個學生的10個能力值的數量
                //--計算學生1-9的數量 陣列[0]不用
                    for (j = 0; j < IDData.GetLength(0); j++)
                    {
                        if (StudentData[i, j + 10] == "1")
                        {
                            String a = IDData[j, IDData.GetLength(1) - 1];
                            Char[] aa = a.ToCharArray();
                            for (k = 0; k < aa.Length; k++)
                            {
                                Calculation[Convert.ToInt32(aa[k].ToString()), Convert.ToInt32(IDData[j, 1].ToString()) - 1] += 1;
                            }
                        }
                    }
                #endregion                
                #region 判斷學生是否有答對3題或以上且機率超過0.5，有給予適當等級沒有給1等
                    for (j = 1; j < 10; j++)
                    {
                        bool NoHaveSave=true;
                        for (k = 2; k >=0; k--)
                        {
                            if (NoHaveSave&&Calculation[j, k] >= 3 && (Calculation[j, k] / CalculationNum[j, k]) >= 0.5)
                            {
                                StudentData[i, j] =""+ (k + 1);
                                NoHaveSave = false;
                            }
                            if (NoHaveSave)
                                StudentData[i, j] = "1";
                        }
                    }
                #endregion
            }
            #region 計算答對ID題目的人數(6格分別[123等級答對人數及123等級做過總人數])
                //先歸0
                for (i = 0; i < IDData.GetLength(0); i++)
                {
                    for (j = 2; j < 8; j++)
                    {
                        IDData[i, j] = "0";
                    }
                }
                //每位學生
                for (i = 0; i < StudentData.GetLength(0); i++)
                {                    
                    //每一題目
                    for (j = 0; j < IDData.GetLength(0); j++)
                    {
                        double avg = 0;
                        String a = IDData[j, IDData.GetLength(1) - 1];
                        Char[] aa = a.ToCharArray();
                        for (k = 0; k < aa.Length; k++)
                        {
                            avg += Convert.ToInt32(StudentData[i, Convert.ToInt32(aa[k].ToString())]);
                        }
                        if ((avg / aa.Length) >= 2.5)
                        {
                            IDData[j, 7] = "" + (Convert.ToInt32(IDData[j, 7].ToString()) + 1);
                            if (StudentData[i, j + 10] == "1")
                            {
                                IDData[j, 4] = "" + (Convert.ToInt32(IDData[j, 4].ToString()) + 1);
                            }
                        }
                        else if ((avg / aa.Length) >= 1.5)
                        {
                            IDData[j, 6] = "" + (Convert.ToInt32(IDData[j, 6].ToString()) + 1);
                            if (StudentData[i, j + 10] == "1")
                            {
                                IDData[j, 3] = "" + (Convert.ToInt32(IDData[j, 3].ToString()) + 1);
                            }
                        }
                        else
                        {
                            IDData[j, 5] = "" + (Convert.ToInt32(IDData[j, 5].ToString()) + 1);
                            if (StudentData[i, j + 10] == "1")
                            {
                                IDData[j, 2] = "" + (Convert.ToInt32(IDData[j, 2].ToString()) + 1);
                            }
                        }
                    }
                }
            #endregion
        }

        //資料整批匯入
        protected void Button2Click(object sender, EventArgs e)
        {
            TCheck.Text = "";
            String[] ID = Regex.Split(TID.Text, ",");
            String Chapter = TChapter.Text;
            //int skill = Convert.ToInt16(TSkillsUnit.Text);
            //int Difficult = Convert.ToInt16(TDifficult.Text);
            int[] skill = ArrayTextToInt(TSkillsUnit.Text);
            int[] Difficult = ArrayTextToInt(TDifficult.Text);
            String[] StudentData = Regex.Split(TStudentData.Text, "\r\n");

            AllData_Connection.Open();//開啟AllData資料庫
            for (int i = 0; i < StudentData.Length; i++)
            {
                String[] StuData = Regex.Split(StudentData[i], ",");
                if (ID.Length != skill.Length || skill.Length != Difficult.Length || Difficult.Length != (StuData.Length - 1))
                {
                    Label1.Text = "匯入失敗";
                    Response.Write("<script language=JavaScript>alert('輸入資料比數不正確\\r請重新確認')</script>");
                    break;
                }
                else
                {
                    for (int j = 0; j < ID.Length; j++)
                    {
                        //輸出資料ID
                        if (i == 0 && j == 0)
                        {
                            TCheck.Text += StuData[0] + "-";
                        }
                        //判斷是否有重複資料
                        SqlCommand cmd = new SqlCommand("Select COUNT(ID) From ExtraExamHistory Where ID=@ID AND SkillsUnit=@Skill AND Difficulty=@Dif AND Chapter=@Chapter AND Correct=@Correct AND StudentID=@StuID", AllData_Connection);//使用SQL  
                        cmd.Parameters.AddWithValue("@ID", ID[j]);
                        cmd.Parameters.AddWithValue("@Skill", skill[j]);
                        cmd.Parameters.AddWithValue("@Dif", Difficult[j]);
                        cmd.Parameters.AddWithValue("@Chapter", Chapter);
                        cmd.Parameters.AddWithValue("@Correct", StuData[j + 1]);
                        cmd.Parameters.AddWithValue("@StuID", StuData[0]);
                        //--是否有重複
                        bool NoHave = true;
                        try
                        {
                            DataBase_Reader = cmd.ExecuteReader();
                            DataBase_Reader.Read();
                            if ((int)DataBase_Reader[0] > 0)
                            {
                                NoHave = false;
                                TCheck.Text += "?-";
                                Label1.Text = "資料重複";
                            }
                            DataBase_Reader.Close();
                        }
                        catch
                        {
                            NoHave = false;
                            TCheck.Text += "X-";
                            Label1.Text = "匯入失敗";
                            Console.Write("資料庫連接無法連線，請從新輸入" + "\n");
                        }
                        if (NoHave)
                        {
                            cmd = new SqlCommand("Insert into ExtraExamHistory(ID,SkillsUnit,Difficulty,Chapter,Correct,StudentID,HaveDo) Values(@ID,@Skill,@Dif,@Chapter,@Correct,@StuID,@HD)", AllData_Connection);//使用SQL  
                            //@ID,@Skill,@Dif,@Chapter,@Correct,@StuID,@HD
                            cmd.Parameters.AddWithValue("@ID", ID[j]);
                            cmd.Parameters.AddWithValue("@Skill", skill[j]);
                            cmd.Parameters.AddWithValue("@Dif", Difficult[j]);
                            cmd.Parameters.AddWithValue("@Chapter", Chapter);
                            cmd.Parameters.AddWithValue("@Correct", StuData[j + 1]);
                            cmd.Parameters.AddWithValue("@StuID", StuData[0]);
                            cmd.Parameters.AddWithValue("@HD", "false");

                            try
                            {
                                cmd.ExecuteNonQuery();
                                if (i != 0 && j == 0)
                                    TCheck.Text += "\r\n" + StuData[0] + "-";
                                TCheck.Text += StuData[j + 1] + "-";
                                Label1.Text = "匯入成功";
                            }
                            catch
                            {
                                TCheck.Text += "X-";
                                Label1.Text = "匯入失敗";
                                Console.Write("資料庫連接無法連線，請從新輸入" + "\n");
                            }
                        }
                    }
                }

            }
            AllData_Connection.Close();//關閉AllData資料庫
        }

        //演算法
        private bool AnswersAbnormalRate(String[,] Data, String[,] StudentData, int Num)
        {
            int N = Num+1;
            int i, j, k;
            #region 複製傳入的陣列
            String[,] CpData = new String[Data.GetLength(0), Data.GetLength(1)];
            String[,] CpStudentData = new String[StudentData.GetLength(0), StudentData.GetLength(1)];
            for (i = 0; i < Data.GetLength(0); i++)
            {
                for (j = 0; j < Data.GetLength(1); j++)
                {
                    CpData[i, j] = Data[i, j];
                }
            }
            for (i = 0; i < StudentData.GetLength(0); i++)
            {
                for (j = 0; j < StudentData.GetLength(1); j++)
                {
                    CpStudentData[i, j] = StudentData[i, j];
                }
            }
            #endregion
            const int Data1 = 3, Data2 = 9, Data3 = 3, Data4 = 6;
            int DataNum = Data.GetLength(0);
            i = 0;
            int[, ,] mydata = new int[DataNum, Data1, Data2];
            double[, ,] myresult = new double[DataNum, Data3, Data4];
            double[,] AAR = new double[DataNum, 3];

            #region 陣列初始為0
            while (i < DataNum)
            {
                for (j = 0; j < Data1; j++)
                {
                    for (k = 0; k < Data2; k++)
                    {
                        mydata[i, j, k] = 0;
                    }
                }
                for (j = 0; j < Data3; j++)
                {
                    for (k = 0; k < Data4; k++)
                    {
                        myresult[i, j, k] = 0;
                    }
                }
                for (int l = 0; l < 3; l++)
                {
                    AAR[i, l] = 0;
                }
                i++;
            }
            #endregion

            #region 放入值
            i = 0;
            while (i < DataNum)
            {
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
            i = 0;
            while (i < DataNum)
            {
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
                for (j = 0; j < 3; j++)
                {
                    AAR[i, j] = Nan(myresult[i, 0, 0 + j * 2]) + Nan(myresult[i, 0, 1 + j * 2]) + Nan(myresult[i, 1, 0 + j * 2]) + Nan(myresult[i, 1, 1 + j * 2]) + Nan(myresult[i, 2, 0 + j * 2]) + Nan(myresult[i, 2, 1 + j * 2]);
                    re[j] = AAR[i, j];
                }
                //改變複製陣列CpData
                if (Lv(re) != int.Parse(Data[i, 1]) && Lv(re) < 4)//難易度不同且有人做過(沒人做過會有[等級1~3算出來都是]0的問題)
                {
                    CpData[i, 1] = "" + Lv(re);
                    /*int Total = int.Parse(Data[i, 5]) + int.Parse(Data[i, 6]) + int.Parse(Data[i, 7]);
                    AddDataBase(Data[i, 0], int.Parse(Data[i, 1]), Lv(re), Total);*/
                }
                //Label1.Text=""+Lv(re);
                i++;
            }
            #endregion

            #region 難易度沒有收斂
            bool NoReturnSame = true;
            for (i = 0; i < Data.GetLength(0); i++)
            {
                if (NoReturnSame && Data[i, 1] == CpData[i, 1]) ;
                else
                {
                    NoReturnSame = false;
                    break;
                }
            }
            //難易度沒有收斂
            if (!NoReturnSame&&N<=MAX)
            {
                CalculationStudentsAbility(ref CpData, ref CpStudentData);
                NoReturnSame = AnswersAbnormalRate(CpData,CpStudentData, N);
            }
            #endregion
            //難易度收斂了且遞迴<=10次 紀錄
            if (NoReturnSame && N <= MAX)
            {
                int Total=0;
                for (i = 0; i < CpData.GetLength(0); i++)
                {
                    Total=int.Parse(Data[i, 5])+int.Parse(Data[i, 6])+int.Parse(Data[i, 7]);
                    AddIDDataBase(Data[i, 0], int.Parse(Data[i, 1]), Total,N);
                }
                AddStudentDataBase(CpStudentData, N);
                
            }
            return NoReturnSame;
        }

        //紀錄ID資料
        private void AddIDDataBase(String name, int Diff, int DoneTimes, int Num)
        {
            SqlCommand cmd = new SqlCommand("Select * From ExtraHistoryAbilityChanges Where ID='" + name + "'AND No='" + Num + "'AND Diff='" + Diff + "'", AllData_Connection);//使用SQL
            bool NoHaveID = true;
            try
            {
                DataBase_Reader = cmd.ExecuteReader();

                if (DataBase_Reader.Read())
                {
                    NoHaveID = false;
                }
                DataBase_Reader.Close();
            }
            catch
            {
                Console.Write("資料庫連接無法連線，請從新輸入" + "\n");
            }
            if (NoHaveID)
            {
                try
                {

                    DateTime Date = DateTime.Now;
                    //String AddNew = ;//資料庫語法
                    //插入歷史資料表                
                    cmd = new SqlCommand("Insert into ExtraHistoryAbilityChanges(ID,No,Diff,Date,DoneTimes) Values(@Name,@No,@Dif,@date,@Total)", AllData_Connection);//使用SQL  
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@No", Num);
                    cmd.Parameters.AddWithValue("@Dif", Diff);
                    cmd.Parameters.AddWithValue("@date", Date);
                    cmd.Parameters.AddWithValue("@Total", DoneTimes);
                    cmd.ExecuteNonQuery();

                    //難易度更新
                    /*cmd = new SqlCommand("Update ExerciseData set Difficulty=@CDif where ID=@Name", AllData_Connection);//使用SQL
                    cmd.Parameters.AddWithValue("@CDif", ChangeDiff);
                    cmd.Parameters.AddWithValue("@Name", name);

                    cmd.ExecuteNonQuery();*/

                }
                catch
                {
                    Console.Write("資料庫連接無法連線，請從新輸入" + "\n");
                }
            }
        }
        //紀錄學生資料
        private void AddStudentDataBase(String[,] StudentData, int Num)
        {
            int i,j;
            String Cheapter;
            for (i = 0; i < StudentData.GetLength(0); i++)
            {
                Cheapter="";
                for (j = 1; j < 10; j++)
                {
                    Cheapter += StudentData[i, j];
                }
                SqlCommand cmd = new SqlCommand("Select * From ExtraStudentAbilityLog Where StudentID='" + StudentData[i, 0] + "'AND No='" + Num + "'AND Cheapter='" + Cheapter + "'", AllData_Connection);//使用SQL
                bool NoHave = true;
                try
                {
                    DataBase_Reader = cmd.ExecuteReader();

                    if (DataBase_Reader.Read())
                    {
                        NoHave = false;
                    }
                    DataBase_Reader.Close();
                }
                catch
                {
                    Console.Write("資料庫連接無法連線，請從新輸入" + "\n");
                }
                if (NoHave)
                {
                    try
                    {

                        DateTime Date = DateTime.Now;
                        //String AddNew = ;//資料庫語法
                        //插入歷史資料表                
                        cmd = new SqlCommand("Insert into ExtraStudentAbilityLog(StudentID,No,Cheapter,Date) Values(@StudentID,@No,@Cheapter,@date)", AllData_Connection);//使用SQL  
                        cmd.Parameters.AddWithValue("@StudentID", StudentData[i, 0]);
                        cmd.Parameters.AddWithValue("@No", Num);
                        cmd.Parameters.AddWithValue("@Cheapter", Cheapter);
                        cmd.Parameters.AddWithValue("@date", Date);
                        cmd.ExecuteNonQuery();

                        //難易度更新
                        /*cmd = new SqlCommand("Update ExerciseData set Difficulty=@CDif where ID=@Name", AllData_Connection);//使用SQL
                        cmd.Parameters.AddWithValue("@CDif", ChangeDiff);
                        cmd.Parameters.AddWithValue("@Name", name);

                        cmd.ExecuteNonQuery();*/

                    }
                    catch
                    {
                        Console.Write("資料庫連接無法連線，請從新輸入" + "\n");
                    }
                }
            }
        }

        //取正整數
        private double abs(double Num)
        {
            if (Num >= 0)
                return Num;
            else
                return (-Num);
        }

        //判斷是否為不存在的數值(1/0的狀況)
        private double Nan(double Num)
        {
            if (double.IsNaN(Num))
                return 0;
            else
                return Num;
        }

        //傳回最低的數值(等級1~3)
        private int Lv(double[] Num)
        {
            double small = 1;
            int N = 0, Zero = 0;
            for (int i = 0; i < 3; i++)
            {
                if (Num[i] < small)
                {
                    small = Num[i];
                    N = i + 1;
                }
                if (small == 0)
                    Zero++;
            }
            if (Zero > 1)
                return 4;
            else
                return N;
        }

        //字串轉整數陣列
        public int[] ArrayTextToInt(String a) {
            if (a != "")
            {
                String[] str = Regex.Split(a, ",");
                int i;
                int[] reNum = { };
                Array.Resize(ref reNum, str.Length);
                for (i = 0; i < str.Length; i++)
                {
                    try
                    {
                        reNum[i] = Convert.ToInt16(str[i]);
                    }
                    catch { }
                }
                return reNum;
            }
            else
            {
                int[] Nu = { };
                    return Nu;
            }
        }
    }
}