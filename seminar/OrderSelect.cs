using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace seminar
{
    public partial class OrderSelect : Form
    {

        //訂單資料表
        MySqlConnection conn;
        MySqlCommand command;
        DataSet myds; //資料集 
        MySqlDataAdapter adapter; //變壓器
        BindingSource bs = new BindingSource(); //數據訪問層

        //匯率資料表
        MySqlDataAdapter rateAdapter;
        BindingSource rateBs = new BindingSource();

        //訂單明細資料表
        MySqlDataAdapter scheduleAdapter;
        BindingSource scheduleBs = new BindingSource();

        public OrderSelect()
        {
            InitializeComponent();


            string dbHost = "localhost";//資料庫位址
            string dbPort = "3306";//資料庫的port
            string dbUser = "root";//資料庫使用者帳號
            string dbPass = "5423";//資料庫使用者密碼
            string dbName = "Seminar";//資料庫名稱

            string connStr = "server=" + dbHost + ";port=" + dbPort + ";uid=" + dbUser + ";pwd=" + dbPass + ";database=" + dbName;
            conn = new MySqlConnection(connStr);
            command = conn.CreateCommand();
        }

        private void OrderSelect_Load(object sender, EventArgs e)
        {
            myds = new DataSet();

            rateAdapter = new MySqlDataAdapter("SELECT * FROM 匯率資料表;", conn);
            rateAdapter.Fill(myds, "匯率資料表");
            rateBs.DataSource = myds.Tables["匯率資料表"];

            adapter = new MySqlDataAdapter("SELECT 訂單編號,客戶名稱,幣別,日期,匯率,訂單資料表.客戶代號 FROM 訂單資料表 INNER JOIN 匯率資料表 INNER JOIN 客戶資料表 ON 訂單資料表.匯率表id = 匯率資料表.匯率表id AND 訂單資料表.客戶代號 = 客戶資料表.客戶代號 AND 訂單資料表.訂單完成狀態=0 ;", conn);
            adapter.Fill(myds, "訂單資料表");
            bs.DataSource = myds.Tables["訂單資料表"];
            dataGridView1.DataSource = bs;

            myds.Tables.Add("訂單明細資料表");
            scheduleBs.DataSource = myds.Tables["訂單明細資料表"];
            dataGridViewEx1.DataSource = scheduleBs;

            dataGridView1.Enabled = true;
        }

        private DataTable AutoDate(DataTable original)
        {
            DataColumn co = new DataColumn("日期");
            DataTable imitation = new DataTable();
            imitation.Columns.Add(co);
;
            DataView dvRate = new DataView(myds.Tables["匯率資料表"]);


            for (int i = 0;i<original.Rows.Count;i++)
            {
                DataRow nr = imitation.NewRow();

                dvRate.RowFilter = "匯率表id = " + original.Rows[i]["匯率表id"].ToString();
                nr["日期"] = dvRate[0]["日期"].ToString();
                imitation.Rows.Add(nr);
            }

            imitation.Merge(original);
            return imitation;

        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Enabled)
            {
                myds.Tables["訂單明細資料表"].Clear();
                scheduleAdapter = new MySqlDataAdapter("SELECT * FROM 訂單明細資料表 WHERE 訂單編號 = " + dataGridView1["訂單編號", e.RowIndex].Value.ToString() + ";", conn);
                scheduleAdapter.Fill(myds, "訂單明細資料表");
                dataGridViewEx1.ClearSelection();
            }
        }

        private int category()
        {
            switch (dataGridView1.CurrentRow.Cells["幣別"].Value.ToString())
            {
                case "臺幣 (TWD)":
                    return 0;
                case "美金 (USD)":
                    return 1;

                case "港幣 (HKD)":
                    return 2;
                case "英鎊 (GBP)":
                    return 3;
                case "澳幣 (AUD)":
                    return 4;
                case "加拿大幣 (CAD)":
                    return 5;
                case "新加坡幣 (SGD)":
                    return 6;
                case "瑞士法郎 (CHF)":
                    return 7;
                case "日圓 (JPY)":
                    return 8;
                case "南非幣 (ZAR)":
                    return 9;
                case "瑞典幣 (SEK)":
                    return 10;
                case "紐元 (NZD)":
                    return 11;
                case "泰幣 (THB)":
                    return 12;
                case "菲國比索 (PHP)":
                    return 13;
                case "印尼幣 (IDR)":
                    return 14;
                case "歐元 (EUR)":
                    return 15;
                case "韓元 (KRW)":
                    return 16;
                case "越南盾 (VND)":
                    return 17;
                case "馬來幣 (MYR)":
                    return 18;
                case "人民幣 (CNY)":
                    return 19;
                default:
                    return 0;
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            SaleSlip.order = dataGridView1["訂單編號", e.RowIndex].Value.ToString();
            plan.order = dataGridView1["訂單編號", e.RowIndex].Value.ToString();
            this.Close();
        }

        private void dataGridViewEx1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            SaleSlip.order = dataGridView1.CurrentRow.Cells["訂單編號"].Value.ToString();
            plan.order = dataGridView1.CurrentRow.Index.ToString();
            SaleSlip.irow= int.Parse(dataGridViewEx1["品號", e.RowIndex].Value.ToString());
            int needed = int.Parse(dataGridViewEx1["數量", e.RowIndex].Value.ToString()) - int.Parse(dataGridViewEx1["已交數量", e.RowIndex].Value.ToString());
            SaleSlip.needed = needed < 0 ? 0 : needed;
            this.Close();
        }
    }
}
