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
    public partial class MouldSelect : Form
    {
        MySqlConnection conn;
        MySqlCommand command;
        MySqlDataAdapter adapter;
        DataSet myds; // 資料集 
        BindingSource bs = new BindingSource();
        produce toAdded;

        public MouldSelect(produce value)
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

            toAdded = value;

        }

        private void MouldSelect_Load(object sender, EventArgs e)
        {
            myds = new DataSet();
            adapter = new MySqlDataAdapter("SELECT * FROM 模具資料表;", conn);
            adapter.Fill(myds, "模具資料表");
            bs.DataSource = myds.Tables["模具資料表"];
            dataGridView1.DataSource = bs;

            dataGridView1.ClearSelection();
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int irow = int.Parse(dataGridView1["模具編號", e.RowIndex].Value.ToString());
            toAdded.irow = irow;
            this.Close();
        }
    }
}
