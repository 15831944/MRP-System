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
    public partial class Receipts : Form
    {
        MySqlConnection conn;
        MySqlCommand command;
        private static int _num;

        public static int num
        {
            set { _num = value; }
            get { return _num; }
        }

        //生產計劃資料表
        DataSet myds; //資料集 
        MySqlDataAdapter adapter; //變壓器

        //入庫單
        MySqlDataAdapter recepitAdapter;

        public Receipts()
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

        private void Receipts_Load(object sender, EventArgs e)
        {
            myds = new DataSet();

            conn.Open();
            command.CommandText = "ALTER TABLE 入庫單 AUTO_INCREMENT = 1;";
            command.ExecuteNonQuery();
            conn.Close();

            //先建立生產計劃的Binding Source
            adapter = new MySqlDataAdapter("SELECT * FROM 生產計劃資料表 WHERE 完成狀態=0;", conn);
            adapter.Fill(myds, "生產計劃資料表");
            dataGridView1.DataSource = myds.Tables["生產計劃資料表"];

            //入庫單
            recepitAdapter = new MySqlDataAdapter("SELECT * FROM 入庫單;", conn);
            recepitAdapter.Fill(myds, "入庫單");
            dataGridViewEx1.DataSource = myds.Tables["入庫單"];
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Storage s = new Storage();
            s.ShowDialog();

            DataRow dr = myds.Tables["入庫單"].NewRow();
            dr["日期"] = DateTime.Today.ToShortDateString();
            dr["品號"] = dataGridView1["品號", e.RowIndex].Value;
            dr["入庫數量"] = num;

            myds.Tables["入庫單"].Rows.Add(dr);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MySqlCommandBuilder mycb = new MySqlCommandBuilder(recepitAdapter);
            recepitAdapter.Update(myds, "入庫單");
            檢查入庫狀態();
            MessageBox.Show("已儲存");
            reload();
        }

        private void 檢查入庫狀態()
        {
            MySqlCommandBuilder mycb = new MySqlCommandBuilder(adapter);
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                DataGridViewCheckBoxCell check = dataGridView1.Rows[i].Cells["已完成"] as DataGridViewCheckBoxCell;

                if (Convert.ToBoolean(check.Value))
                {
                    dataGridView1.Rows[i].Cells["完成狀態"].Value = 1;
                }
            }
            adapter.Update(myds, "生產計劃資料表");
        }

        private void reload()
        {
            //先建立生產計劃的Binding Source
            myds.Tables["生產計劃資料表"].Clear();
            adapter = new MySqlDataAdapter("SELECT * FROM 生產計劃資料表 WHERE 完成狀態=0;", conn);
            adapter.Fill(myds, "生產計劃資料表");

            //入庫單
            myds.Tables["入庫單"].Clear();
            recepitAdapter = new MySqlDataAdapter("SELECT * FROM 入庫單;", conn);
            recepitAdapter.Fill(myds, "入庫單");
        }
    }
}
