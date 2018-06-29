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
    public partial class plan : Form
    {
        MySqlConnection conn;
        MySqlCommand command;
        private static int _irow;
        private static string _order;

        //生產單資料表
        DataSet myds; //資料集 
        MySqlDataAdapter adapter; //變壓器
        BindingSource bs = new BindingSource(); //數據訪問層

        //产品资料表
        MySqlDataAdapter productAdapter;

        public static int irow
        {
            set { _irow = value; }
            get { return _irow; }
        }

        public static string order
        {
            set { _order = value; }
            get { return _order; }
        }

        public plan()
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

        public plan(string i,string e)
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

            textBox3.Text = i; //品號
            textBox4.Text = e; //預計產量

        }

        private void plan_Load(object sender, EventArgs e)
        {
            myds = new DataSet();

            //先建立生產計劃的Binding Source
            conn.Open();
            command.CommandText = "ALTER TABLE 生產計劃資料表 AUTO_INCREMENT = 1;";
            command.ExecuteNonQuery();
            conn.Close();
            adapter = new MySqlDataAdapter("SELECT * FROM 生產計劃資料表;", conn);
            adapter.Fill(myds, "生產計劃資料表");
            bs.DataSource = myds.Tables["生產計劃資料表"];
            dataGridViewEx1.DataSource = bs;
            comboBox1.SelectedIndex = 0;

            //以下整理產品資料表
            productAdapter = new MySqlDataAdapter("SELECT * FROM 產品資料表;", conn);
            productAdapter.Fill(myds, "產品資料表");
            myds.Tables["產品資料表"].PrimaryKey = new DataColumn[] { myds.Tables["產品資料表"].Columns["品號"] };

            productName();//品名整理
            commandSet();

        }

        private void commandSet()
        {
            adapter.InsertCommand = new MySqlCommand("INSERT INTO 生產計劃資料表(訂單編號,品號,日期,預計數量,完成狀態) VALUES(@訂單編號,@品號,@日期,@預計數量,@完成狀態);", conn);
            adapter.InsertCommand.Parameters.Add("@訂單編號", MySqlDbType.Int32, 100, "訂單編號");
            adapter.InsertCommand.Parameters.Add("@品號", MySqlDbType.Int32, 100, "品號");
            adapter.InsertCommand.Parameters.Add("@日期", MySqlDbType.Date, 10, "日期");
            adapter.InsertCommand.Parameters.Add("@預計數量", MySqlDbType.Int32, 100, "預計數量");
            adapter.InsertCommand.Parameters.Add("@完成狀態", MySqlDbType.Bit, 1, "完成狀態");

            adapter.DeleteCommand = new MySqlCommand("DELETE FROM 生產計劃資料表 WHERE 序號=@序號;", conn);
            adapter.DeleteCommand.Parameters.Add("@序號", MySqlDbType.Int32, 100, "序號");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int dri = bs.IndexOf(bs.AddNew());
            dataGridViewEx1.Rows[dri].Cells["日期"].Value = dateTimePicker1.Value.ToString("yyyy/MM/dd");
            dataGridViewEx1.Rows[dri].Cells["訂單"].Value = textBox2.Text;
            dataGridViewEx1.Rows[dri].Cells["品號"].Value = textBox3.Text;
            DataRow dr = myds.Tables["產品資料表"].Rows.Find(dataGridViewEx1.Rows[dri].Cells["品號"].Value);
            dataGridViewEx1.Rows[dri].Cells["品名"].Value = dr["品名"].ToString();
            dataGridViewEx1.Rows[dri].Cells["預計數量"].Value = textBox4.Text;
            dataGridViewEx1.Rows[dri].Cells["完成狀態"].Value = 0;
            bs.EndEdit();

            adapter.Update(myds.Tables["生產計劃資料表"]);

            reFill();
            dataGridViewEx1.ClearSelection();
            dataGridViewEx1.Rows[dri].Selected = true;
        }

        private void textBox2_DoubleClick(object sender, EventArgs e)
        {
            OrderSelect OS = new OrderSelect();
            OS.ShowDialog();
            textBox2.Text = order;
        }

        private void textBox3_DoubleClick(object sender, EventArgs e)
        {
            ProductSelect PS = new ProductSelect();
            PS.ShowDialog();
            textBox3.Text = irow.ToString();
        }

        private void productName()
        {
            DataRow dr;
            for (int i=0;i< dataGridViewEx1.Rows.Count; i++)
            {
                dr = myds.Tables["產品資料表"].Rows.Find(dataGridViewEx1.Rows[i].Cells["品號"].Value);
                dataGridViewEx1.Rows[i].Cells["品名"].Value = dr["品名"].ToString();
            }
        }

        private void plan_FormClosing(object sender, FormClosingEventArgs e)
        {
            int added = 0;
            int deleted = 0;
            int modified = 0;

            foreach (DataRow dr in myds.Tables["生產計劃資料表"].Rows)

            {

                switch (dr.RowState)

                {

                    case DataRowState.Added:

                        added++;

                        break;

                    case DataRowState.Deleted:

                        deleted++;

                        break;

                    case DataRowState.Modified:

                        modified++;

                        break;

                    case DataRowState.Unchanged:

                        break;

                    default:

                        break;

                }

            }


            if (deleted != 0)
                adapter.Update(myds.Tables["生產計劃資料表"]);
        }

        private void reFill()
        {
            myds.Tables["生產計劃資料表"].Clear();
            adapter.Fill(myds, "生產計劃資料表");
            productName();//品名整理
            commandSet();
            comboBox1.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            adapter.Update(myds.Tables["生產計劃資料表"]);
            MessageBox.Show("已儲存");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                myds.Tables["生產計劃資料表"].Clear();
                adapter = new MySqlDataAdapter("SELECT * FROM 生產計劃資料表 WHERE 完成狀態=0;", conn);
                adapter.Fill(myds, "生產計劃資料表");
            }
            else if(comboBox1.SelectedIndex == 1)
            {
                myds.Tables["生產計劃資料表"].Clear();
                adapter = new MySqlDataAdapter("SELECT * FROM 生產計劃資料表 WHERE 完成狀態=1;", conn);
                adapter.Fill(myds, "生產計劃資料表");
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                myds.Tables["生產計劃資料表"].Clear();
                adapter = new MySqlDataAdapter("SELECT * FROM 生產計劃資料表;", conn);
                adapter.Fill(myds, "生產計劃資料表");
            }

            commandSet();
        }
    }
}
