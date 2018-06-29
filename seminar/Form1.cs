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
    public partial class Form1 : Form
    {

        MySqlConnection conn;
        MySqlCommand command;

        public Form1()
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

        private void Form1_Load(object sender, EventArgs e)
        {
            readFrom();
        }

        private string SelectCount()
        {
            try
            {
                return command.ExecuteScalar().ToString();
            }
            catch
            {
                return "0";
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OrderSlip os = new OrderSlip();
            os.FormClosed += new FormClosedEventHandler(FormClosed);
            os.Show();
            this.Hide();          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Customer c = new Customer();
            c.FormClosed += new FormClosedEventHandler(FormClosed);
            c.Show();
            this.Hide();

        }

        new void FormClosed(object sender, FormClosedEventArgs e)
        {
            readFrom();
            this.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Production p = new Production();
            p.FormClosed += new FormClosedEventHandler(FormClosed);
            p.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Mould m = new Mould();
            m.FormClosed += new FormClosedEventHandler(FormClosed);
            m.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Machine m = new Machine();
            m.FormClosed += new FormClosedEventHandler(FormClosed);
            m.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SaleSlip SS = new SaleSlip();
            SS.FormClosed += new FormClosedEventHandler(FormClosed);
            SS.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Receipts R = new Receipts();
            R.FormClosed += new FormClosedEventHandler(FormClosed);
            R.Show();
            this.Hide();
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            listBox1.ClearSelected();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            plan p = new plan();
            p.FormClosed += new FormClosedEventHandler(FormClosed);
            p.Show();
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            produce p = new produce();
            p.FormClosed += new FormClosedEventHandler(FormClosed);
            p.Show();
            this.Hide();
        }

        private void readFrom()
        {
            listBox1.Items.Clear();
            conn.Open();
            command.CommandText = "SELECT COUNT(*) FROM 訂單資料表 WHERE 訂單完成狀態 = 0"; ;
            listBox1.Items.Add("目前共有" + SelectCount() + "筆訂單未銷貨");
            listBox1.Items.Add("");
            command.CommandText = "SELECT COUNT(*) FROM 客戶資料表;";
            listBox1.Items.Add("共有" + SelectCount() + "筆客戶資料");
            listBox1.Items.Add("");
            command.CommandText = "SELECT COUNT(*) FROM 產品資料表;";
            listBox1.Items.Add("已建立" + SelectCount() + "項產品資料");
            listBox1.Items.Add("");
            command.CommandText = "SELECT COUNT(*) FROM 生產計劃資料表 WHERE 完成狀態 = 0;";
            listBox1.Items.Add("尚有" + SelectCount() + "筆生產計劃");
            listBox1.Items.Add("");
            command.CommandText = "SELECT COUNT(*) FROM 模具資料表;";
            listBox1.Items.Add("共有" + SelectCount() + "種模具");
            listBox1.Items.Add("");
            command.CommandText = "SELECT COUNT(*) FROM 機台資料表;";
            listBox1.Items.Add("共有" + SelectCount() + "台機台");
            conn.Close();
        }
    }
}
