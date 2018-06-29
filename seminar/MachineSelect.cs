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
    public partial class MachineSelect : Form
    {
        MySqlConnection conn;
        MySqlCommand command;
        MySqlDataAdapter adapter;
        DataSet myds; // 資料集 

        public MachineSelect()
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

        private void MachineSelect_Load(object sender, EventArgs e)
        {
            conn.Open();
            myds = new DataSet();
            adapter = new MySqlDataAdapter("SELECT * FROM 機台資料表;", conn);
            adapter.Fill(myds, "機台資料表");
            dataGridView1.DataSource = myds.Tables["機台資料表"];
            conn.Close();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //取得雙擊的行數
            Mould.irow = int.Parse(dataGridView1[0, e.RowIndex].Value.ToString());
            this.Close();
        }

    }
}
