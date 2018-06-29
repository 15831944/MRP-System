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
    public partial class produce : Form
    {
        MySqlConnection conn;
        MySqlCommand command;
        private int _irow;

        //生產單資料表
        DataSet myds; //資料集 
        MySqlDataAdapter adapter; //變壓器
        BindingSource bs = new BindingSource(); //數據訪問層

        public int irow
        {
            set { _irow = value; }
            get { return _irow; }
        }

        public produce()
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

        private void produce_Load(object sender, EventArgs e)
        {
            conn.Open();
            command.CommandText = "ALTER TABLE 生產單資料表 AUTO_INCREMENT = 1;";
            command.ExecuteNonQuery();
            conn.Close();

            myds = new DataSet();
            adapter = new MySqlDataAdapter("SELECT * FROM 生產單資料表;", conn);
            adapter.Fill(myds, "生產單資料表");
            bs.DataSource = myds.Tables["生產單資料表"];
            dataGridViewEx1.DataSource = bs;

            commandSet();

            DataView dv = myds.Tables["生產單資料表"].DefaultView;
            DataTable newdata = dv.ToTable(true, "製程條件參數");

            List<string> content = new List<string>(); //字串的動態陣列

            foreach(DataRowView drv in dv)
            {
                content.Add(drv["製程條件參數"].ToString());
            }

            AutoCompleteStringCollection newAdd = new AutoCompleteStringCollection();
            newAdd.AddRange(content.ToArray<string>());

            textBox3.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox3.AutoCompleteCustomSource = newAdd;


        }

        private void commandSet()
        {
            adapter.InsertCommand = new MySqlCommand("INSERT INTO 生產單資料表(生產編號, 日期, 模具編號, 製程條件參數, 開始, 結束, 不良總數) VALUES(@生產編號, @日期, @模具編號, @製程條件參數, @開始, @結束, @不良總數);", conn);
            adapter.InsertCommand.Parameters.Add("@生產編號", MySqlDbType.Int32, 100, "生產編號");
            adapter.InsertCommand.Parameters.Add("@日期", MySqlDbType.Date, 20, "日期");
            adapter.InsertCommand.Parameters.Add("@模具編號", MySqlDbType.Int32, 100, "模具編號");
            adapter.InsertCommand.Parameters.Add("@製程條件參數", MySqlDbType.VarChar, 45, "製程條件參數");
            adapter.InsertCommand.Parameters.Add("@開始", MySqlDbType.Int32, 100, "開始");
            adapter.InsertCommand.Parameters.Add("@結束", MySqlDbType.Int32, 100, "結束");
            adapter.InsertCommand.Parameters.Add("@不良總數", MySqlDbType.Int32, 100, "不良總數");

            adapter.DeleteCommand = new MySqlCommand("DELETE FROM 生產單資料表 WHERE SN=@SN;", conn);
            adapter.DeleteCommand.Parameters.Add("@SN", MySqlDbType.Int32, 100, "SN");

            adapter.UpdateCommand = new MySqlCommand("UPDATE 生產單資料表 SET 生產編號=@生產編號, 日期=@日期, 模具編號=@模具編號, 製程條件參數=@製程條件參數, 開始=@開始, 結束=@結束, 不良總數=@不良總數 WHERE SN=@SN;",conn);
            adapter.UpdateCommand.Parameters.Add("@生產編號", MySqlDbType.Int32, 100, "生產編號");
            adapter.UpdateCommand.Parameters.Add("@日期", MySqlDbType.Date, 20, "日期");
            adapter.UpdateCommand.Parameters.Add("@模具編號", MySqlDbType.Int32, 100, "模具編號");
            adapter.UpdateCommand.Parameters.Add("@製程條件參數", MySqlDbType.VarChar, 45, "製程條件參數");
            adapter.UpdateCommand.Parameters.Add("@開始", MySqlDbType.Int32, 100, "開始");
            adapter.UpdateCommand.Parameters.Add("@結束", MySqlDbType.Int32, 100, "結束");
            adapter.UpdateCommand.Parameters.Add("@不良總數", MySqlDbType.Int32, 100, "不良總數");
            adapter.UpdateCommand.Parameters.Add("@SN", MySqlDbType.Int32, 100, "SN");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int added = 0;
            int deleted = 0;
            int modified = 0;

            foreach (DataRow dr in myds.Tables["生產單資料表"].Rows)

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

            if (added != 0 || deleted != 0 || modified != 0)
            {
                String result = "";

               result += "共\n"
                    + (added == 0 ? "" : "【新增】 " + added + "筆新資料\n")
                    + (deleted == 0 ? "" : "【刪除】 " + deleted + "筆資料\n")
                    + (modified == 0 ? "" : "【變更】 " + modified + "筆資料\n");

                MessageBox.Show(result);

                adapter.Update(myds.Tables["生產單資料表"]);
            }
        }

        private void produce_FormClosing(object sender, FormClosingEventArgs e)
        {
            int added = 0;
            int deleted = 0;
            int modified = 0;

            foreach (DataRow dr in myds.Tables["生產單資料表"].Rows)

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

            if (added != 0 || deleted != 0 || modified != 0)
            {
                String result = "";

                result += (added == 0 ? "" : "【新增】 " + added + "筆新資料\n")
                    + (deleted == 0 ? "" : "【刪除】 " + deleted + "筆資料\n")
                    + (modified == 0 ? "" : "【變更】 " + modified + "筆資料\n")
                    + "要儲存嗎？";

                DialogResult dialogResult = MessageBox.Show(result, "要儲存嗎？", MessageBoxButtons.YesNoCancel);
                if (dialogResult == DialogResult.Yes)
                    adapter.Update(myds.Tables["生產單資料表"]);
                else if (dialogResult == DialogResult.Cancel)
                    e.Cancel = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkall())
            {
                DataRow dr = myds.Tables["生產單資料表"].NewRow();
                dr["生產編號"] = int.Parse(textBox1.Text);
                dr["日期"] = dateTimePicker1.Value.ToString("yyyy/MM/dd");
                dr["模具編號"] = int.Parse(textBox2.Text);
                dr["製程條件參數"] = textBox3.Text;
                dr["開始"] = int.Parse(textBox4.Text);
                dr["結束"] = int.Parse(textBox5.Text);
                dr["不良總數"] = int.Parse(textBox6.Text);

                myds.Tables["生產單資料表"].Rows.Add(dr);

            }
            else
            {
                MessageBox.Show("請檢查欄位格式是否有誤！");
            }
        }

        private bool checkall()
        {
            int i;
            bool b = true;

            if (!int.TryParse(textBox1.Text, out i) || textBox1.Text.Trim() == string.Empty) //檢查生產編號是否數字且不為空
                b = false;
            if (!int.TryParse(textBox2.Text, out i) || textBox2.Text.Trim() == string.Empty) //檢查模具編號是否數字且不為空
                b = false;
            if (!int.TryParse(textBox4.Text, out i)) //檢查開始欄位是否為數字
                b = false;
            if (!int.TryParse(textBox5.Text, out i)) //檢查結束欄位是否為數字
                b = false;
            if (!int.TryParse(textBox6.Text, out i)) //檢查不良總數欄位是否為數字
                b = false;

            return b;
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            MouldSelect ms = new MouldSelect(this);
            ms.ShowDialog();

            textBox2.Text = irow.ToString();

        }
    }
}
