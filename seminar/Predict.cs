using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;


namespace seminar
{
    public partial class Predict : Form
    {
        MySqlConnection conn;
        MySqlCommand command;
        private int irow;//品號


        public Predict()
        {
            InitializeComponent();
        }

        public void setI(int i)
        {
            irow = i;
        }


        private void Predict_Load(object sender, EventArgs e)
        {

            double r = ROP();

            label2.Text = "ROP:" + r.ToString();

            double eoq = EOQ();
            label1.Text = "EOQ:" + eoq.ToString();

            //chart
            CreateChartSpline();
        }

        public double search()
        {
            string dbHost = "localhost";//資料庫位址
            string dbPort = "3306";//資料庫的port
            string dbUser = "root";//資料庫使用者帳號
            string dbPass = "5423";//資料庫使用者密碼
            string dbName = "Seminar";//資料庫名稱

            string connStr = "server=" + dbHost + ";port=" + dbPort + ";uid=" + dbUser + ";pwd=" + dbPass + ";database=" + dbName;
            conn = new MySqlConnection(connStr);
            command = conn.CreateCommand();

            conn.Open();

            //查詢

            String cmdText = "SELECT 月預測值 FROM 產品月銷量及月預測表 WHERE 品號="+irow+" ORDER BY 日期 desc limit 1 ";
            MySqlCommand cmd = new MySqlCommand(cmdText, conn);
            double a;
            try { a = double.Parse(cmd.ExecuteScalar().ToString()); }
            catch { a = 0; }

            return a;
        }

        public double EOQ()
        {
            double d = search();
            double eoq = Math.Round(Math.Sqrt((2 * d * 56.8) / 0.2));
            return eoq;
        }
        public double ROP()
        {
            string dbHost = "localhost";//資料庫位址
            string dbPort = "3306";//資料庫的port
            string dbUser = "root";//資料庫使用者帳號
            string dbPass = "5423";//資料庫使用者密碼
            string dbName = "Seminar";//資料庫名稱


            string cs = "server=" + dbHost + ";port=" + dbPort + ";uid=" + dbUser + ";pwd=" + dbPass + ";database=" + dbName;

            //string cs = "Data Source=127.0.0.1;port = 3306;username = root;password=123456;\\mySQLEXPRESS;Initial Catalog=Northwind;Integrated Security=True";
            double[] avg_order = new double[12];


            //找出去年年份
            DateTime d1 = DateTime.Now;
            int a = int.Parse(d1.Year.ToString()) - 1;

            string qs1 = "SELECT 銷貨單資料表.銷貨單號,日期,品號,數量 FROM 銷貨單資料表 JOIN 匯率資料表 JOIN 銷貨單明細資料表 WHERE 銷貨單資料表.匯率表id=匯率資料表.匯率表id and 銷貨單資料表.銷貨單號 = 銷貨單明細資料表.銷貨單號 and 日期>='" + a + "-01-01' and 日期<'" + a + "-02-01';";
            string qs2 = "SELECT 銷貨單資料表.銷貨單號,日期,品號,數量 FROM 銷貨單資料表 JOIN 匯率資料表 JOIN 銷貨單明細資料表 WHERE 銷貨單資料表.匯率表id=匯率資料表.匯率表id and 銷貨單資料表.銷貨單號 = 銷貨單明細資料表.銷貨單號 and 日期>='" + a + "-02-01' and 日期<'" + a + "-03-01';";
            string qs3 = "SELECT 銷貨單資料表.銷貨單號,日期,品號,數量 FROM 銷貨單資料表 JOIN 匯率資料表 JOIN 銷貨單明細資料表 WHERE 銷貨單資料表.匯率表id=匯率資料表.匯率表id and 銷貨單資料表.銷貨單號 = 銷貨單明細資料表.銷貨單號 and 日期>='" + a + "-03-01' and 日期<'" + a + "-04-01';";
            string qs4 = "SELECT 銷貨單資料表.銷貨單號,日期,品號,數量 FROM 銷貨單資料表 JOIN 匯率資料表 JOIN 銷貨單明細資料表 WHERE 銷貨單資料表.匯率表id=匯率資料表.匯率表id and 銷貨單資料表.銷貨單號 = 銷貨單明細資料表.銷貨單號 and 日期>='" + a + "-04-01' and 日期<'" + a + "-05-01';";
            string qs5 = "SELECT 銷貨單資料表.銷貨單號,日期,品號,數量 FROM 銷貨單資料表 JOIN 匯率資料表 JOIN 銷貨單明細資料表 WHERE 銷貨單資料表.匯率表id=匯率資料表.匯率表id and 銷貨單資料表.銷貨單號 = 銷貨單明細資料表.銷貨單號 and 日期>='" + a + "-05-01' and 日期<'" + a + "-06-01';";
            string qs6 = "SELECT 銷貨單資料表.銷貨單號,日期,品號,數量 FROM 銷貨單資料表 JOIN 匯率資料表 JOIN 銷貨單明細資料表 WHERE 銷貨單資料表.匯率表id=匯率資料表.匯率表id and 銷貨單資料表.銷貨單號 = 銷貨單明細資料表.銷貨單號 and 日期>='" + a + "-06-01' and 日期<'" + a + "-07-01';";
            string qs7 = "SELECT 銷貨單資料表.銷貨單號,日期,品號,數量 FROM 銷貨單資料表 JOIN 匯率資料表 JOIN 銷貨單明細資料表 WHERE 銷貨單資料表.匯率表id=匯率資料表.匯率表id and 銷貨單資料表.銷貨單號 = 銷貨單明細資料表.銷貨單號 and 日期>='" + a + "-07-01' and 日期<'" + a + "-08-01';";
            string qs8 = "SELECT 銷貨單資料表.銷貨單號,日期,品號,數量 FROM 銷貨單資料表 JOIN 匯率資料表 JOIN 銷貨單明細資料表 WHERE 銷貨單資料表.匯率表id=匯率資料表.匯率表id and 銷貨單資料表.銷貨單號 = 銷貨單明細資料表.銷貨單號 and 日期>='" + a + "-08-01' and 日期<'" + a + "-09-01';";
            string qs9 = "SELECT 銷貨單資料表.銷貨單號,日期,品號,數量 FROM 銷貨單資料表 JOIN 匯率資料表 JOIN 銷貨單明細資料表 WHERE 銷貨單資料表.匯率表id=匯率資料表.匯率表id and 銷貨單資料表.銷貨單號 = 銷貨單明細資料表.銷貨單號 and 日期>='" + a + "-09-01' and 日期<'" + a + "-10-01';";
            string qs10 = "SELECT 銷貨單資料表.銷貨單號,日期,品號,數量 FROM 銷貨單資料表 JOIN 匯率資料表 JOIN 銷貨單明細資料表 WHERE 銷貨單資料表.匯率表id=匯率資料表.匯率表id and 銷貨單資料表.銷貨單號 = 銷貨單明細資料表.銷貨單號 and 日期>='" + a + "-10-01' and 日期<'" + a + "-11-01';";
            string qs11 = "SELECT 銷貨單資料表.銷貨單號,日期,品號,數量 FROM 銷貨單資料表 JOIN 匯率資料表 JOIN 銷貨單明細資料表 WHERE 銷貨單資料表.匯率表id=匯率資料表.匯率表id and 銷貨單資料表.銷貨單號 = 銷貨單明細資料表.銷貨單號 and 日期>='" + a + "-11-01' and 日期<'" + a + "-12-01';";
            string qs12 = "SELECT 銷貨單資料表.銷貨單號,日期,品號,數量 FROM 銷貨單資料表 JOIN 匯率資料表 JOIN 銷貨單明細資料表 WHERE 銷貨單資料表.匯率表id=匯率資料表.匯率表id and 銷貨單資料表.銷貨單號 = 銷貨單明細資料表.銷貨單號 and 日期>='" + a + "-12-01' and 日期<'" + (a + 1) + "-1';";


            //1.SqlConnection
            using (MySqlConnection cn = new MySqlConnection(cs))
            {
                //1
                using (MySqlCommand cmd = new MySqlCommand(qs1, cn))
                {
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt1 = new DataTable();
                    da.Fill(dt1);
                    // this.dataGridView2.DataSource = dt1;
                    double o1 = dt1.Rows.Count;
                    //table數量加總
                    double sum1 = dt1.AsEnumerable().Sum(
                            row => double.Parse(row["數量"].ToString()));
                    avg_order[0] = sum1 / o1;

                }
                //2
                using (MySqlCommand cmd = new MySqlCommand(qs2, cn))
                {
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt2 = new DataTable();
                    da.Fill(dt2);
                    //this.dataGridView2.DataSource = dt2;
                    double o2 = dt2.Rows.Count;

                    double sum2 = dt2.AsEnumerable().Sum(
                            row => double.Parse(row["數量"].ToString()));

                    avg_order[1] = sum2 / o2;
                }
                //3
                using (MySqlCommand cmd = new MySqlCommand(qs3, cn))
                {
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt3 = new DataTable();
                    da.Fill(dt3);
                    //this.dataGridView2.DataSource = dt3;
                    double o3 = dt3.Rows.Count;

                    double sum3 = dt3.AsEnumerable().Sum(
                            row => double.Parse(row["數量"].ToString()));

                    avg_order[2] = sum3 / o3;
                }
                //4
                using (MySqlCommand cmd = new MySqlCommand(qs4, cn))
                {
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt4 = new DataTable();
                    da.Fill(dt4);
                    //this.dataGridView2.DataSource = dt4;
                    double o4 = dt4.Rows.Count;

                    double sum4 = dt4.AsEnumerable().Sum(
                            row => double.Parse(row["數量"].ToString()));

                    avg_order[3] = sum4 / o4;
                }
                //5
                using (MySqlCommand cmd = new MySqlCommand(qs5, cn))
                {
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt5 = new DataTable();
                    da.Fill(dt5);
                    // this.dataGridView2.DataSource = dt5;
                    double o5 = dt5.Rows.Count;

                    double sum5 = dt5.AsEnumerable().Sum(
                            row => double.Parse(row["數量"].ToString()));

                    avg_order[4] = sum5 / o5;
                }
                //2.SqlCommand
                //6
                using (MySqlCommand cmd = new MySqlCommand(qs6, cn))
                {
                    //3.SqlDataAdapter
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                    //4.建立DataSet類別或DataTable類別
                    //使用Fill方法
                    //===========================================

                    DataTable dt6 = new DataTable();
                    da.Fill(dt6);

                    //  this.dataGridView1.DataSource = dt6;
                    double o6 = dt6.Rows.Count;


                    double sum6 = dt6.AsEnumerable().Sum(
                        row => double.Parse(row["數量"].ToString()));


                    avg_order[5] = sum6 / o6;



                }
                //7
                using (MySqlCommand cmd = new MySqlCommand(qs7, cn))
                {
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt7 = new DataTable();
                    da.Fill(dt7);
                    //  this.dataGridView2.DataSource = dt7;
                    double o7 = dt7.Rows.Count;

                    double sum7 = dt7.AsEnumerable().Sum(
                            row => double.Parse(row["數量"].ToString()));

                    avg_order[6] = sum7 / o7;

                }
                //8
                using (MySqlCommand cmd = new MySqlCommand(qs8, cn))
                {
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt8 = new DataTable();
                    da.Fill(dt8);
                    //this.dataGridView2.DataSource = dt8;
                    double o8 = dt8.Rows.Count;

                    double sum8 = dt8.AsEnumerable().Sum(
                            row => double.Parse(row["數量"].ToString()));

                    avg_order[7] = sum8 / o8;
                }
                //9
                using (MySqlCommand cmd = new MySqlCommand(qs9, cn))
                {
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt9 = new DataTable();
                    da.Fill(dt9);
                    //this.dataGridView2.DataSource = dt9;
                    double o9 = dt9.Rows.Count;

                    double sum9 = dt9.AsEnumerable().Sum(
                            row => double.Parse(row["數量"].ToString()));

                    avg_order[8] = sum9 / o9;
                }
                //10
                using (MySqlCommand cmd = new MySqlCommand(qs10, cn))
                {
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt10 = new DataTable();
                    da.Fill(dt10);
                    //this.dataGridView2.DataSource = dt10;
                    double o10 = dt10.Rows.Count;

                    double sum10 = dt10.AsEnumerable().Sum(
                            row => double.Parse(row["數量"].ToString()));

                    avg_order[9] = sum10 / o10;
                }
                //11
                using (MySqlCommand cmd = new MySqlCommand(qs11, cn))
                {
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt11 = new DataTable();
                    da.Fill(dt11);
                    //this.dataGridView2.DataSource = dt11;
                    double o11 = dt11.Rows.Count;

                    double sum11 = dt11.AsEnumerable().Sum(
                            row => double.Parse(row["數量"].ToString()));

                    avg_order[10] = sum11 / o11;
                }
                //12
                using (MySqlCommand cmd = new MySqlCommand(qs12, cn))
                {
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt12 = new DataTable();
                    da.Fill(dt12);
                    //.dataGridView2.DataSource = dt12;
                    double o12 = dt12.Rows.Count;

                    double sum12 = dt12.AsEnumerable().Sum(
                            row => double.Parse(row["數量"].ToString()));

                    avg_order[11] = sum12 / o12;

                }

            }

            double rop = Math.Round(Average(avg_order) + StandardDeviation(avg_order) * 1.64);

            return rop;
        }


        public double StandardDeviation(double[] num)
        {
            double avg = Average(num);

            double SumOfSqrs = 0.0;
            double row = num.Length;

            foreach (double d in num)
            {
                string s = "非數值";

                if (d.ToString() == s)
                {
                    row--;
                }
                else
                {
                    SumOfSqrs += Math.Pow(d - avg, 2);
                }

            }


            return Math.Sqrt((SumOfSqrs / (row - 1)));
        }


        public double Average(double[] num)
        {
            double sum = 0.0;
            double row = Convert.ToDouble(num.Length);

            foreach (double d in num)
            {
                string s = "非數值";

                if (d.ToString() == s)
                {
                    row--;
                }
                else
                {
                    sum += d;
                }


            }

            return sum / row;
        }


        //chart
        #region 折线图
        private void CreateChartSpline()
        {
            string dbHost = "localhost";//資料庫位址
            string dbPort = "3306";//資料庫的port
            string dbUser = "root";//資料庫使用者帳號
            string dbPass = "5423";//資料庫使用者密碼
            string dbName = "Seminar";//資料庫名稱


            string cs = "server=" + dbHost + ";port=" + dbPort + ";uid=" + dbUser + ";pwd=" + dbPass + ";database=" + dbName;

            string str = irow.ToString(); //品號
            string qs = "SELECT * FROM 產品月銷量及月預測表 WHERE 品號= '" + str + "' ORDER BY 日期 desc limit 10";

            //table
            using (MySqlConnection cn = new MySqlConnection(cs))
            {
                //1
                using (MySqlCommand cmd = new MySqlCommand(qs, cn))
                {
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);


                    //创建一个图标
                    //Chart chart = new Chart();

                    chart1.Titles.Add("實際與預測表");

                    //表格值帶入
                    chart1.DataSource = dt;
                    chart1.Series[0].XValueMember = "日期";
                    chart1.Series[0].YValueMembers = "月總銷量";
                    chart1.Series[0].LegendText = "實際月銷量";
                    chart1.Series[0].ChartType = SeriesChartType.Line;
                    chart1.Series[0].IsValueShownAsLabel = true;

                    chart1.Series[1].XValueMember = "日期";
                    chart1.Series[1].YValueMembers = "月預測值";
                    chart1.Series[1].LegendText = "預測月銷量";
                    chart1.Series[1].ChartType = SeriesChartType.Line;
                    chart1.Series[1].IsValueShownAsLabel = true;



                    chart1.DataBind();



                }

            }
        }
        #endregion

        private void button1_Click_1(object sender, EventArgs e)
        {
            double k = 725;
            k = EOQ();

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
