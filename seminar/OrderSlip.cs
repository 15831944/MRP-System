using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using ComboxExtended;
using System.Data.SqlClient;

namespace seminar
{
    public partial class OrderSlip : Form
    {
        MySqlConnection conn;
        MySqlCommand command;
        private static int _irow; //選擇的品號
        private static string _order;//欲修改的單號
        private static string _cus;//欲修改的客戶代號
        private static int _category;//欲修改的單別

        //訂單資料表
        DataSet myds; //資料集 
        MySqlDataAdapter adapter; //變壓器
        BindingSource bs = new BindingSource(); //數據訪問層

        //訂單明細資料表
        MySqlDataAdapter scheduleAdapter;
        BindingSource scheduleBs = new BindingSource();

        //产品资料表
        MySqlDataAdapter productAdapter;

        //匯率資料表
        MySqlDataAdapter rateAdapter;

        public OrderSlip()
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

        public static string cus
        {
            set { _cus = value; }
            get { return _cus; }
        }

        public static int category
        {
            set { _category = value; }
            get { return _category; }
        }

        private void 新增ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (checkResult())
            {
                conn.Open();
                command.CommandText = "SELECT * FROM 訂單資料表 ORDER BY 訂單編號 DESC LIMIT 1;";
                MySqlDataReader rd = command.ExecuteReader();
                textBox1.Text = rd.Read() ? (int.Parse(rd[0].ToString()) + 1).ToString() : 1.ToString(); //目前最新訂單的單號
                conn.Close();

                scheduleAdapter = new MySqlDataAdapter("SELECT * FROM 訂單明細資料表 WHERE 訂單編號 = " + textBox1.Text + ";", conn);
                commandSet();
                myds.Tables["訂單明細資料表"].Clear();
                scheduleAdapter.Fill(myds, "訂單明細資料表");

                新增與刪除切換(false);
                comboBox1.SelectedIndex = -1;
                imagedComboBox1.SelectedIndex = 0;
            }

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                imagedComboBox1.SelectedIndex = 0;
            }
        }

        private void OrderSlip_Load(object sender, EventArgs e)
        {
            imagedComboBox1.Items.Add(new ComboBoxItem("台灣", Image.FromFile("../../flag/Taiwan.gif")));
            imagedComboBox1.Items.Add(new ComboBoxItem("美國", Image.FromFile("../../flag/America.gif")));
            imagedComboBox1.Items.Add(new ComboBoxItem("香港", Image.FromFile("../../flag/HongKong.gif")));
            imagedComboBox1.Items.Add(new ComboBoxItem("英國", Image.FromFile("../../flag/England.gif")));
            imagedComboBox1.Items.Add(new ComboBoxItem("澳洲", Image.FromFile("../../flag/Australia.gif")));
            imagedComboBox1.Items.Add(new ComboBoxItem("加拿大", Image.FromFile("../../flag/Canada.gif")));
            imagedComboBox1.Items.Add(new ComboBoxItem("新加坡", Image.FromFile("../../flag/Singapore.gif")));
            imagedComboBox1.Items.Add(new ComboBoxItem("瑞士", Image.FromFile("../../flag/Swiss.gif")));
            imagedComboBox1.Items.Add(new ComboBoxItem("日本", Image.FromFile("../../flag/Japan.gif")));
            imagedComboBox1.Items.Add(new ComboBoxItem("南非", Image.FromFile("../../flag/SouthAfrica.gif")));
            imagedComboBox1.Items.Add(new ComboBoxItem("瑞典", Image.FromFile("../../flag/Sweden.gif")));
            imagedComboBox1.Items.Add(new ComboBoxItem("紐西蘭", Image.FromFile("../../flag/NewZealand.gif")));
            imagedComboBox1.Items.Add(new ComboBoxItem("泰國", Image.FromFile("../../flag/Thailand.gif")));
            imagedComboBox1.Items.Add(new ComboBoxItem("菲律賓", Image.FromFile("../../flag/Philippine.gif")));
            imagedComboBox1.Items.Add(new ComboBoxItem("印尼", Image.FromFile("../../flag/Indonesia.gif")));
            imagedComboBox1.Items.Add(new ComboBoxItem("歐洲", Image.FromFile("../../flag/Euro.gif")));
            imagedComboBox1.Items.Add(new ComboBoxItem("韓國", Image.FromFile("../../flag/Korea.gif")));
            imagedComboBox1.Items.Add(new ComboBoxItem("越南", Image.FromFile("../../flag/Vietnam.gif")));
            imagedComboBox1.Items.Add(new ComboBoxItem("馬來西亞", Image.FromFile("../../flag/Malaysia.gif")));
            imagedComboBox1.Items.Add(new ComboBoxItem("中國大陸", Image.FromFile("../../flag/China.gif")));
            radioButton1.Checked = true;

            //以下整理資料庫

            //先建立訂單的Binding Source
            conn.Open();
            command.CommandText = "ALTER TABLE 訂單資料表 AUTO_INCREMENT = 1;";
            command.ExecuteNonQuery();
            conn.Close();
            myds = new DataSet();
            adapter = new MySqlDataAdapter("SELECT * FROM 訂單資料表;", conn);
            adapter.Fill(myds, "訂單資料表");
            bs.DataSource = myds.Tables["訂單資料表"];

            myds.Tables.Add("匯率資料表");

            //以下整理訂單單號
            conn.Open();
            command.CommandText = "SELECT * FROM 訂單資料表 ORDER BY 訂單編號 DESC LIMIT 1;";
            MySqlDataReader rd = command.ExecuteReader();
            textBox1.Text = rd.Read() ? (int.Parse(rd[0].ToString()) + 1).ToString() : 1.ToString(); //目前最新訂單的單號
            conn.Close();

            //以下建立訂單明細
            myds.Tables.Add("訂單明細資料表");
            //conn.Open();
            scheduleAdapter = new MySqlDataAdapter("SELECT * FROM 訂單明細資料表 WHERE 訂單編號 = " + textBox1.Text + ";", conn);
            scheduleAdapter.Fill(myds, "訂單明細資料表");
            scheduleBs.DataSource = myds.Tables["訂單明細資料表"];
            dataGridView1.DataSource = scheduleBs;
            //conn.Close();

            //以下整理客戶代號
            MySqlDataAdapter customerAdapter = new MySqlDataAdapter("SELECT * FROM 客戶資料表; ", conn); //客戶資料用臨時變壓器
            customerAdapter.Fill(myds, "客戶資料表");
            comboBox1.DataSource = myds.Tables["客戶資料表"];
            comboBox1.DisplayMember = "客戶名稱";
            comboBox1.ValueMember = "客戶代號";
            comboBox1.SelectedIndex = -1;

            //以下整理產品資料表
            productAdapter = new MySqlDataAdapter("SELECT * FROM 產品資料表;", conn);
            productAdapter.Fill(myds,"產品資料表");
            myds.Tables["產品資料表"].PrimaryKey = new DataColumn[] { myds.Tables["產品資料表"].Columns["品號"] };


            //開啟控制項
            imagedComboBox1.Enabled = true;
            dateTimePicker1.Enabled = true;
            dateTimePicker2.Enabled = true;
            changeRate();
            commandSet();

        }

        private void imagedComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (imagedComboBox1.SelectedIndex != 0)
            {
                radioButton2.Checked = true;
            }
            else
            {
                radioButton1.Checked = true;
            }
            changeRate();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            changeRate();
        }

        private void changeRate()
        {
            if (imagedComboBox1.Enabled)
            {
                try
                {
                    //再建立匯率表的DataTable
                    myds.Tables["匯率資料表"].Clear();
                    rateAdapter = new MySqlDataAdapter("SELECT * FROM 匯率資料表 WHERE 日期 = '" + dateTimePicker1.Value.ToString("yyyy/MM/dd") + "';", conn);
                    rateAdapter.Fill(myds, "匯率資料表");
                    textBox3.Text = myds.Tables["匯率資料表"].Rows[imagedComboBox1.SelectedIndex][3].ToString();
                    
                }
                catch
                {
                    textBox3.Text = "";
                }
            }

        }

        private void OrderSlip_FormClosing(object sender, FormClosingEventArgs e)
        {
            int added = 0;
            int deleted = 0;
            int modified = 0;

            foreach (DataRow dr in myds.Tables["訂單明細資料表"].Rows)

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
                {
                    if (checkAll())
                    {
                        try
                        {
                            scheduleAdapter.Update(myds, "訂單明細資料表");
                        }
                        catch
                        {
                            DataRow dr = myds.Tables["訂單資料表"].NewRow();
                            dr["客戶代號"] = comboBox1.SelectedValue.ToString();
                            dr["單別"] = radioButton1.Checked ? 1 : 0; //國內為true（資料庫記1），國外為false
                            dr["匯率表id"] = myds.Tables["匯率資料表"].Rows[imagedComboBox1.SelectedIndex][0].ToString();

                            myds.Tables["訂單資料表"].Rows.Add(dr);
                            adapter.Update(myds, "訂單資料表");

                            scheduleAdapter.Update(myds, "訂單明細資料表");

                        }
                    }
                    else
                    {
                        //MessageBox.Show("請檢查是否有未填欄位\n或是庫存量不足的情況！");
                        e.Cancel = true;
                    }
                }
                else if (dialogResult == DialogResult.No)
                {
                    //do something
                }
                else if (dialogResult == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }

            }
            檢視訂單完成度();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkAll())
            {
                try
                {
                    if (button1.Text.Equals("新增"))
                    {
                        DataRow dr = myds.Tables["訂單資料表"].NewRow();
                        dr["客戶代號"] = comboBox1.SelectedValue.ToString();
                        dr["單別"] = radioButton1.Checked ? 1 : 0; //國內為true（資料庫記1），國外為false
                        dr["交貨日期"] = dateTimePicker2.Value.ToString("yyyy/MM/dd");
                        dr["匯率表id"] = myds.Tables["匯率資料表"].Rows[imagedComboBox1.SelectedIndex][0].ToString();
                        dr["訂單完成狀態"] = 0;

                        myds.Tables["訂單資料表"].Rows.Add(dr);
                        adapter.Update(myds, "訂單資料表");

                        scheduleAdapter.Update(myds, "訂單明細資料表");

                        order = textBox1.Text;
                        cus = comboBox1.SelectedValue.ToString();
                        revise();
                        MessageBox.Show("已新增");
                    }
                    else if (button1.Text.Equals("修改"))
                    {
                        scheduleAdapter.Update(myds, "訂單明細資料表");

                        MessageBox.Show("已修改");
                    }
                }
                catch
                {
                    //以下檢查有無重複
                    foreach (DataGridViewRow v in dataGridView1.Rows)
                    {
                        if (v.Cells["品號"].Value != null)
                        {
                            var count = 0;
                            foreach (DataGridViewRow v2 in dataGridView1.Rows)
                            {
                                if (v2.Cells["品號"].Value != null)
                                {
                                    if (v.Cells["品號"].Value.ToString().Equals(v2.Cells["品號"].Value.ToString()))
                                        count++;
                                }
                            }
                            if (count > 1)
                            {
                                MessageBox.Show("有重複的品號:【" + v.Cells["品號"].Value + "】請檢查！");
                                return;
                            }
                        }
                    }

                }

            }
            
        }

        private bool checkAll()
        {
            bool pass = true;

            //以下檢查客戶有無選擇

            if (comboBox1.SelectedIndex == -1 && !comboBox1.Text.Trim().Equals(""))
            {
                DialogResult dialogResult;
                dialogResult = MessageBox.Show("找不到這個客戶，新增嗎？", "新增客戶", MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    string s = comboBox1.Text;
                    Customer c = new Customer(s);
                    c.ShowDialog();

                    myds.Tables["客戶資料表"].Clear();
                    MySqlDataAdapter customerAdapter = new MySqlDataAdapter("SELECT * FROM 客戶資料表; ", conn); //客戶資料用臨時變壓器
                    customerAdapter.Fill(myds, "客戶資料表");

                    comboBox1.SelectedIndex = comboBox1.FindStringExact(s);

                }

                pass = false;
            }
            else if (comboBox1.SelectedIndex == -1)
            {
                pass = false;
                MessageBox.Show("請檢查是否有未填欄位！");
            }

            //以下檢查數量是否都有填入，出貨後庫存量是否有負數
                foreach (DataGridViewRow v in dataGridView1.Rows)
            {
                if (v.Cells["數量"].Value == null && !v.IsNewRow)
                {
                    pass = false;
                    MessageBox.Show("請檢查是否有未填欄位！");
                }

                if (v.Cells["出貨後數量"].Style.ForeColor == Color.Red)
                {
                    DialogResult dialogResult;
                    dialogResult = MessageBox.Show("注意：有庫存量不足的情況！\n是否排定生產計畫？","生產單建立",MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        plan pp = new plan();
                        pp.ShowDialog();
                    }
                }
            }

                return pass;
        }

        private void dataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            dataGridView1["訂單編號", e.Row.Index-1].Value = textBox1.Text;
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["品號"].Index && dataGridView1.CurrentRow.IsNewRow)
            {
                ProductSelect fm = new ProductSelect();
                DataRow dr;
                fm.ShowDialog();
                try {
                    dataGridView1.AllowUserToAddRows = false;
                    scheduleBs.AddNew();
                    dataGridView1[e.ColumnIndex, e.RowIndex].Value = irow;
                    dr = myds.Tables["產品資料表"].Rows.Find(dataGridView1["品號", e.RowIndex].Value.ToString());
                    dataGridView1["品名", e.RowIndex].Value = dr["品名"];
                    dataGridView1["規格", e.RowIndex].Value = dr["規格"];
                    dataGridView1["單價", e.RowIndex].Value = dr["單價"];
                    dataGridView1["目前庫存數量", e.RowIndex].Value = dr["庫存量"].ToString();
                    dataGridView1.AllowUserToAddRows = true;
                    dataGridView1.CurrentCell = dataGridView1[dataGridView1.Columns["數量"].Index, e.RowIndex];
                    _irow = new int();
                }
                catch
                {
                    dataGridView1.Rows.RemoveAt(e.RowIndex);
                    dataGridView1.AllowUserToAddRows = true;
                }
            }
            else if (e.ColumnIndex == dataGridView1.Columns["品號"].Index)
            {
                ProductSelect fm = new ProductSelect();
                fm.ShowDialog();
                //dataGridView1.AllowUserToAddRows = false;
                if (!dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString().Equals(_irow.ToString()))
                {
                    DataRow dr = myds.Tables["訂單明細資料表"].NewRow();
                    int old;
                    int old2;
                    try
                    {
                        old = int.Parse(myds.Tables["訂單明細資料表"].Rows[e.RowIndex]["品號"].ToString());
                    }
                    catch
                    {
                        old = 0;
                    }

                    try
                    {
                        old2 = int.Parse(myds.Tables["訂單明細資料表"].Rows[e.RowIndex]["數量"].ToString());
                    }
                    catch
                    {
                        old2 = 0;
                    }
                    dr["品號"] = _irow;
                    myds.Tables["訂單明細資料表"].Rows.Add(dr);
                    try
                    {
                        dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
                        dr = myds.Tables["產品資料表"].Rows.Find(dataGridView1["品號", e.RowIndex].Value.ToString());
                        dataGridView1["品名", e.RowIndex].Value = dr["品名"];
                        dataGridView1["規格", e.RowIndex].Value = dr["規格"];
                        dataGridView1["單價", e.RowIndex].Value = dr["單價"];
                        dataGridView1["目前庫存數量", e.RowIndex].Value = dr["庫存量"].ToString();
                        _irow = new int();
                    }
                    catch
                    {
                        dr = myds.Tables["訂單明細資料表"].NewRow();
                        dr["品號"] = old;
                        myds.Tables["訂單明細資料表"].Rows.Add(dr);
                        dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
                        dr = myds.Tables["產品資料表"].Rows.Find(dataGridView1["品號", e.RowIndex].Value.ToString());
                        if (old2 != 0)
                        {
                            dataGridView1["數量", e.RowIndex].Value = old2;
                        }
                        try
                        {
                            dataGridView1["品名", e.RowIndex].Value = dr["品名"];
                            dataGridView1["規格", e.RowIndex].Value = dr["規格"];
                            dataGridView1["單價", e.RowIndex].Value = dr["單價"];
                            dataGridView1["目前庫存數量", e.RowIndex].Value = dr["庫存量"].ToString();
                        }
                        catch
                        {

                        }
                        _irow = new int();

                    }
                }
            }

        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                dataGridView1["金額", e.RowIndex].Value = double.Parse(dataGridView1["單價", e.RowIndex].Value.ToString()) * double.Parse(dataGridView1["數量", e.RowIndex].Value.ToString());
                dataGridView1["訂單編號", e.RowIndex].Value = textBox1.Text;
                calStorage();
            }
            catch { }

        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try {        
                double sumPrice = 0;
                double sumQuantity = 0;
                for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                {
                    sumPrice += double.Parse(dataGridView1["金額", i].Value.ToString());
                    sumQuantity += double.Parse(dataGridView1["數量", i].Value.ToString());
                }
                textBox4.Text = sumPrice.ToString();
                textBox5.Text = sumQuantity.ToString();

            }
            catch { }


        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            textBox6.Text = (double.Parse(textBox4.Text) * double.Parse(textBox3.Text)).ToString();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            textBox6.Text = (double.Parse(textBox4.Text) * double.Parse(textBox3.Text)).ToString();
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("資料有誤請檢查！\n（非數字嗎？）");
        }

        private void initialP()
        {
            for (int i = 0; i < dataGridView1.RowCount - 1; i++)
            {
                conn.Open();
                command.CommandText = "SELECT 品名,規格,單價 FROM 產品資料表 WHERE 品號 = " + dataGridView1["品號", i].Value.ToString() + ";";
                MySqlDataReader rd = command.ExecuteReader();
                rd.Read();
                dataGridView1["品名", i].Value = rd[0].ToString();
                dataGridView1["規格", i].Value = rd[1].ToString();
                dataGridView1["單價", i].Value = rd[2].ToString();
                conn.Close();
            }
        }

        private bool checkResult()
        {
            int added = 0;
            int deleted = 0;
            int modified = 0;

            foreach (DataRow dr in myds.Tables["訂單明細資料表"].Rows)

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
                {
                    if (checkAll())
                    {

                        try
                        {
                            scheduleAdapter.Update(myds, "訂單明細資料表");
                        }
                        catch
                        {
                            DataRow dr = myds.Tables["訂單資料表"].NewRow();
                            dr["客戶代號"] = comboBox1.SelectedValue.ToString();
                            dr["單別"] = radioButton1.Checked ? 1 : 0; //國內為true（資料庫記1），國外為false
                            dr["匯率表id"] = myds.Tables["匯率資料表"].Rows[imagedComboBox1.SelectedIndex][0].ToString();

                            myds.Tables["訂單資料表"].Rows.Add(dr);
                            adapter.Update(myds, "訂單資料表");

                            scheduleAdapter.Update(myds, "訂單明細資料表");
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (dialogResult == DialogResult.No)
                {
                    //do something
                }
                else if (dialogResult == DialogResult.Cancel)
                {
                    return false;
                }

            }

            return true;
        }

        private void commandSet()
        {
            adapter.InsertCommand = new MySqlCommand("INSERT INTO 訂單資料表(客戶代號,單別,交貨日期,匯率表id,訂單完成狀態) VALUES(@客戶代號,@單別,@交貨日期,@匯率表id,@訂單完成狀態);", conn);
            adapter.InsertCommand.Parameters.Add("@客戶代號", MySqlDbType.Int32, 100, "客戶代號");
            adapter.InsertCommand.Parameters.Add("@交貨日期", MySqlDbType.Date, 10, "交貨日期");
            adapter.InsertCommand.Parameters.Add("@單別", MySqlDbType.Bit, 1, "單別");
            adapter.InsertCommand.Parameters.Add("@匯率表id", MySqlDbType.Int32, 100, "匯率表id");
            adapter.InsertCommand.Parameters.Add("@訂單完成狀態", MySqlDbType.Bit, 1, "訂單完成狀態");

            scheduleAdapter.InsertCommand = new MySqlCommand("INSERT INTO 訂單明細資料表(訂單編號, 品號, 數量, 單價, 已交數量) VALUES(@訂單編號, @品號, @數量, @單價, 0);", conn);
            scheduleAdapter.InsertCommand.Parameters.Add("@訂單編號", MySqlDbType.Int32, 11, "訂單編號");
            scheduleAdapter.InsertCommand.Parameters.Add("@品號", MySqlDbType.Int32, 11, "品號");
            scheduleAdapter.InsertCommand.Parameters.Add("@數量", MySqlDbType.Int32, 11, "數量");
            scheduleAdapter.InsertCommand.Parameters.Add("@單價", MySqlDbType.Double, 100, "單價");

            scheduleAdapter.UpdateCommand = new MySqlCommand("UPDATE 訂單明細資料表 SET 品號=@品號, 數量=@數量, 單價=@單價 WHERE 訂單編號=@訂單編號 and 品號=@品號; ", conn);
            scheduleAdapter.UpdateCommand.Parameters.Add("@訂單編號", MySqlDbType.Int32, 11, "訂單編號");
            scheduleAdapter.UpdateCommand.Parameters.Add("@品號", MySqlDbType.Int32, 11, "品號");
            scheduleAdapter.UpdateCommand.Parameters.Add("@數量", MySqlDbType.Int32, 11, "數量");
            scheduleAdapter.UpdateCommand.Parameters.Add("@單價", MySqlDbType.Double, 100, "單價");

            scheduleAdapter.DeleteCommand = new MySqlCommand("DELETE FROM 訂單明細資料表 WHERE 訂單編號=@訂單編號 and 品號=@品號;", conn);
            scheduleAdapter.DeleteCommand.Parameters.Add("@訂單編號", MySqlDbType.Int32, 11, "訂單編號");
            scheduleAdapter.DeleteCommand.Parameters.Add("@品號", MySqlDbType.Int32, 11, "品號");

            productAdapter.UpdateCommand = new MySqlCommand("UPDATE 產品資料表 SET 庫存量=@庫存量 WHERE 品號=@品號;",conn);
            productAdapter.UpdateCommand.Parameters.Add("@庫存量", MySqlDbType.Int32, 11, "庫存量");
            productAdapter.UpdateCommand.Parameters.Add("@品號", MySqlDbType.Int32, 11, "品號");
        }

        public void revise()
        {
            myds.Tables["訂單資料表"].Clear();
            adapter.Fill(myds, "訂單資料表");

            textBox1.Text = _order;
            comboBox1.SelectedValue = _cus;
            imagedComboBox1.SelectedIndex = category;

            myds.Tables["訂單明細資料表"].Clear();
            scheduleAdapter = new MySqlDataAdapter("SELECT * FROM 訂單明細資料表 WHERE 訂單編號 = " + textBox1.Text + ";", conn);
            scheduleAdapter.Fill(myds, "訂單明細資料表");

            新增與刪除切換(true);

            commandSet();
            reSelect();
        }

        private void 查詢ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (checkResult())
            {
                ViewOrder del = new ViewOrder(this);
                del.ShowDialog();
            }
        }

        private void reSelect()
        {

            double sumPrice = 0;
            double sumQuantity = 0;
            DataRow dr;

            for (int i = 0; i < myds.Tables["訂單明細資料表"].Rows.Count; i++)
            {
                dr = myds.Tables["產品資料表"].Rows.Find(dataGridView1["品號", i].Value.ToString());
                dataGridView1["品名", i].Value = dr["品名"].ToString();
                dataGridView1["規格", i].Value = dr["規格"].ToString();
                dataGridView1["單價", i].Value = dr["單價"].ToString();
                dataGridView1["目前庫存數量", i].Value = dr["庫存量"].ToString();
                dataGridView1["金額", i].Value = double.Parse(dataGridView1["單價", i].Value.ToString()) * double.Parse(dataGridView1["數量", i].Value.ToString());
                sumPrice += double.Parse(dataGridView1["金額", i].Value.ToString());
                sumQuantity += double.Parse(dataGridView1["數量", i].Value.ToString());
            }
            textBox4.Text = sumPrice.ToString();
            textBox5.Text = sumQuantity.ToString();
            calStorage();
        }

        private void calStorage()
        {
                for (int i = 0; i < myds.Tables["訂單明細資料表"].Rows.Count; i++)
                {
                    dataGridView1["出貨後數量", i].Value = int.Parse(dataGridView1["目前庫存數量", i].Value.ToString()) - int.Parse(dataGridView1["數量", i].Value.ToString());
                    dataGridView1["出貨後數量", i].Style.ForeColor = int.Parse(dataGridView1["出貨後數量", i].Value.ToString()) < 0 ? Color.Red : Color.Black;
                }
        }
        
        private void 新增與刪除切換(Boolean i)
        {
            if (i)
            {
                comboBox1.Enabled = false;
                dateTimePicker1.Enabled = false;
                button1.Text = "修改";
            }
            else
            {
                comboBox1.Enabled = true;
                dateTimePicker1.Enabled = true;
                button1.Text = "新增";
            }
        }

        private void 檢視訂單完成度()
        {
            myds.Tables["訂單資料表"].Clear();
            adapter = new MySqlDataAdapter("SELECT * FROM 訂單資料表 WHERE 訂單完成狀態 = 1;", conn);
            adapter.Fill(myds, "訂單資料表");
            MySqlDataReader rd;

            foreach (DataRow v in myds.Tables["訂單資料表"].Rows)
            {
                conn.Open();
                command.CommandText = "SELECT * FROM 訂單明細資料表 WHERE 訂單編號=" + v["訂單編號"].ToString() + " AND 已交數量<數量;";
                rd = command.ExecuteReader();
                if (rd.Read())
                {
                    v["訂單完成狀態"] = 0;
                }
                conn.Close();
            }
            MySqlCommandBuilder mycb = new MySqlCommandBuilder(adapter);
            adapter.Update(myds.Tables["訂單資料表"]);
        }

    }
}
