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
    public partial class ProductSelect : Form
    {
        MySqlConnection conn;
        MySqlCommand command;
        MySqlDataAdapter adapter;
        DataSet myds; // 資料集 
        BindingSource bs = new BindingSource();

        public ProductSelect()
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

        private void Production_Load(object sender, EventArgs e)
        {
            conn.Open();
            command.CommandText = "ALTER TABLE 產品資料表 AUTO_INCREMENT = 1;";
            command.ExecuteNonQuery();
            myds = new DataSet();
            adapter = new MySqlDataAdapter("SELECT * FROM 產品資料表;", conn);
            adapter.Fill(myds, "產品資料表");
            bs.DataSource = myds.Tables["產品資料表"];
            dataGridView1.DataSource = bs;
            conn.Close();

            DataView dv = myds.Tables["產品資料表"].DefaultView;
            DataTable newdata = dv.ToTable(true, "品名");
            comboBox1.DataSource = newdata;
            comboBox1.DisplayMember = comboBox1.ValueMember = "品名";
            comboBox1.SelectedIndex = -1;
            comboBox1.Enabled = true;

        }


        private void dataGridView1_UserAddedRow_1(object sender, DataGridViewRowEventArgs e)
        {
            //每新增一行資料時，把上一行的行號自動加入
            dataGridView1[0, e.Row.Index - 1].Value = e.Row.Index;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;

            //取得雙擊的行數
            OrderSlip.irow = int.Parse(dataGridView1[0, e.RowIndex].Value.ToString());
            Mould.irow = int.Parse(dataGridView1[0, e.RowIndex].Value.ToString());
            SaleSlip.irow = int.Parse(dataGridView1[0, e.RowIndex].Value.ToString());
            plan.irow = int.Parse(dataGridView1[0, e.RowIndex].Value.ToString());
            this.Close();
        }



        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Enabled)
            {
                myds.Clear();
                adapter = new MySqlDataAdapter("SELECT * FROM 產品資料表 WHERE 品名 = '" + comboBox1.Text + "';", conn);
                adapter.Fill(myds, "產品資料表");
            }
        }

        private void comboBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && comboBox1.Text != "")
            {
                myds.Clear();
                adapter = new MySqlDataAdapter("SELECT * FROM 產品資料表 WHERE 品名 = '" + comboBox1.Text + "';", conn);
                adapter.Fill(myds, "產品資料表");
            }
            else if (e.KeyCode == Keys.Enter)
            {
                myds.Clear();
                adapter = new MySqlDataAdapter("SELECT * FROM 產品資料表;", conn);
                adapter.Fill(myds, "產品資料表");
            }
        }

    }
}
