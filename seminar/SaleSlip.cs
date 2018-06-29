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
    public partial class SaleSlip : Form
    {
        MySqlConnection conn;
        MySqlCommand command;

        //銷貨單資料表
        DataSet myds; //資料集 
        MySqlDataAdapter adapter; //變壓器
        BindingSource bs = new BindingSource(); //數據訪問層

        //銷貨單明細資料表
        MySqlDataAdapter scheduleAdapter;
        BindingSource scheduleBs = new BindingSource();

        //产品资料表
        MySqlDataAdapter productAdapter;

        //匯率資料表
        MySqlDataAdapter rateAdapter;

        //訂單資料表
        MySqlDataAdapter orderAdapter;

        //訂單明細資料表
        MySqlDataAdapter orderAdapter2;


        private static string _order;//欲銷貨的單號
        private static int _irow; //選擇的品號
        private static string _sale; //欲修改的單號
        private static int _category;//欲修改的單別
        private static int _needed;//欲銷貨的需求數量

        public static string order
        {
            set { _order = value; }
            get { return _order; }
        }

        public static int needed
        {
            set { _needed = value; }
            get { return _needed; }
        }

        public static int irow
        {
            set { _irow = value; }
            get { return _irow; }
        }

        public static string sale
        {
            set { _sale = value; }
            get { return _sale; }
        }

        public static int category
        {
            set { _category = value; }
            get { return _category; }
        }

        public SaleSlip()
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


        private void SaleSlip_Load(object sender, EventArgs e)
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
            myds = new DataSet();
            myds.Tables.Add("匯率資料表");

            //先建立銷貨單的Binding Source
            conn.Open();
            command.CommandText = "ALTER TABLE 銷貨單資料表 AUTO_INCREMENT = 1;";
            command.ExecuteNonQuery();
            conn.Close();
            adapter = new MySqlDataAdapter("SELECT * FROM 銷貨單資料表;", conn);
            adapter.Fill(myds, "銷貨單資料表");
            bs.DataSource = myds.Tables["銷貨單資料表"];

            //以下整理銷貨單號
            conn.Open();
            command.CommandText = "SELECT * FROM 銷貨單資料表 ORDER BY 銷貨單號 DESC LIMIT 1;";
            MySqlDataReader rd = command.ExecuteReader();
            textBox1.Text = rd.Read() ? (int.Parse(rd[0].ToString()) + 1).ToString() : 1.ToString(); //目前最新訂單的單號
            conn.Close();

            //以下建立銷貨單明細
            myds.Tables.Add("銷貨單明細資料表");
            scheduleAdapter = new MySqlDataAdapter("SELECT * FROM 銷貨單明細資料表 WHERE 銷貨單號 = " + textBox1.Text + ";", conn);
            scheduleAdapter.Fill(myds, "銷貨單明細資料表");
            scheduleBs.DataSource = myds.Tables["銷貨單明細資料表"];
            dataGridView1.DataSource = scheduleBs;

            //以下整理產品資料表
            productAdapter = new MySqlDataAdapter("SELECT * FROM 產品資料表;", conn);
            productAdapter.Fill(myds, "產品資料表");
            myds.Tables["產品資料表"].PrimaryKey = new DataColumn[] { myds.Tables["產品資料表"].Columns["品號"] };

            //以下整理訂單資料表
            orderAdapter = new MySqlDataAdapter("SELECT * FROM 訂單資料表;", conn);
            orderAdapter.Fill(myds, "訂單資料表");
            myds.Tables["訂單資料表"].PrimaryKey = new DataColumn[] { myds.Tables["訂單資料表"].Columns["訂單編號"] };
            
            //以下整理訂單明細資料表
            orderAdapter2 = new MySqlDataAdapter("SELECT * FROM 訂單明細資料表;", conn);
            orderAdapter2.Fill(myds, "訂單明細資料表");
            myds.Tables["訂單明細資料表"].PrimaryKey = new DataColumn[] { myds.Tables["訂單明細資料表"].Columns["訂單編號"], myds.Tables["訂單明細資料表"].Columns["品號"]};

            //開啟控制項
            imagedComboBox1.Enabled = true;
            dateTimePicker1.Enabled = true;
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

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

            if (radioButton1.Checked)
            {
                imagedComboBox1.SelectedIndex = 0;
            }

        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["訂單編號"].Index && dataGridView1.CurrentRow.IsNewRow)
            {
                OrderSelect os = new OrderSelect();
                os.ShowDialog();
                if (order == null)
                    return;
                try
                {
                    dataGridView1.AllowUserToAddRows = false;
                    scheduleBs.AddNew();
                    dataGridView1["訂單編號", e.RowIndex].Value = order;
                    dataGridView1.AllowUserToAddRows = true;
                    dataGridView1.CurrentCell = dataGridView1[dataGridView1.Columns["數量"].Index, e.RowIndex];
                    order = null;
                }
                catch
                {
                    dataGridView1.Rows.RemoveAt(e.RowIndex);
                    dataGridView1.AllowUserToAddRows = true;
                }
                if (irow != new int())
                {
                    DataRow dr;
                    dataGridView1["品號", e.RowIndex].Value = irow;
                    dr = myds.Tables["產品資料表"].Rows.Find(dataGridView1["品號", e.RowIndex].Value.ToString());
                    dataGridView1["品名", e.RowIndex].Value = dr["品名"];
                    dataGridView1["規格", e.RowIndex].Value = dr["規格"];
                    dataGridView1["單價", e.RowIndex].Value = dr["單價"];
                    dataGridView1["目前庫存數量", e.RowIndex].Value = dr["庫存量"].ToString();
                    dataGridView1["需求數量", e.RowIndex].Value = needed;
                    dataGridView1.AllowUserToAddRows = true;
                    dataGridView1.CurrentCell = dataGridView1[dataGridView1.Columns["數量"].Index, e.RowIndex];
                    _irow = new int();
                }

            }

            else if (e.ColumnIndex == dataGridView1.Columns["訂單編號"].Index)
            {
                OrderSelect os = new OrderSelect();
                os.ShowDialog();

                if (order == null)
                    return;
                dataGridView1["訂單編號", e.RowIndex].Value = order;
                if (irow != new int())
                {
                    DataRow dr;
                    dataGridView1["品號", e.RowIndex].Value = irow;
                    dr = myds.Tables["產品資料表"].Rows.Find(dataGridView1["品號", e.RowIndex].Value.ToString());
                    dataGridView1["品名", e.RowIndex].Value = dr["品名"];
                    dataGridView1["規格", e.RowIndex].Value = dr["規格"];
                    dataGridView1["單價", e.RowIndex].Value = dr["單價"];
                    dataGridView1["目前庫存數量", e.RowIndex].Value = dr["庫存量"].ToString();
                    dataGridView1["需求數量", e.RowIndex].Value = needed;
                    dataGridView1.AllowUserToAddRows = true;
                    dataGridView1.CurrentCell = dataGridView1[dataGridView1.Columns["數量"].Index, e.RowIndex];
                    _irow = new int();
                }

            }
            else if (e.ColumnIndex == dataGridView1.Columns["品號"].Index && dataGridView1.CurrentRow.IsNewRow)
            {
                ProductSelect fm = new ProductSelect();
                DataRow dr;
                fm.ShowDialog();
                try
                {
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
                if (!dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString().Equals(_irow.ToString()))
                {
                    DataRow dr = myds.Tables["銷貨單明細資料表"].NewRow();
                    int old;
                    int old2;
                    try
                    {
                        old = int.Parse(myds.Tables["銷貨單明細資料表"].Rows[e.RowIndex]["品號"].ToString());
                    }
                    catch
                    {
                        old = 0;
                    }
                    try
                    {
                        old2 = int.Parse(myds.Tables["銷貨單明細資料表"].Rows[e.RowIndex]["數量"].ToString());
                    }
                    catch
                    {
                        old2 = 0;
                    }
                    dr["品號"] = _irow;
                    myds.Tables["銷貨單明細資料表"].Rows.Add(dr);
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
                        dr = myds.Tables["銷貨單明細資料表"].NewRow();
                        dr["品號"] = old;
                        myds.Tables["銷貨單明細資料表"].Rows.Add(dr);
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

        private void upStorage()
        {
            for (int i = 0; i < myds.Tables["銷貨單明細資料表"].Rows.Count; i++)
            {
                myds.Tables["產品資料表"].Rows.Find(dataGridView1["品號", i].Value.ToString())["庫存量"] = dataGridView1["出貨後數量", i].Value.ToString();
            }
            productAdapter.Update(myds.Tables["產品資料表"]);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkAll())
            {
                try
                {
                    if (button1.Text.Equals("新增"))
                    {
                        DataRow dr = myds.Tables["銷貨單資料表"].NewRow();
                        dr["銷貨單號"] = textBox1.Text;
                        dr["匯率表id"] = myds.Tables["匯率資料表"].Rows[imagedComboBox1.SelectedIndex][0].ToString();

                        myds.Tables["銷貨單資料表"].Rows.Add(dr);
                        adapter.Update(myds, "銷貨單資料表");

                        scheduleAdapter.Update(myds, "銷貨單明細資料表");

                        庫存量更新();
                        已交數量更新();
                        productAdapter.Update(myds, "產品資料表");

                        sale = textBox1.Text;
                        revise();

                        MessageBox.Show("已新增");
                        低於ROP();
                    }
                    else if (button1.Text.Equals("修改"))
                    {
                        scheduleAdapter.Update(myds, "銷貨單明細資料表");

                        庫存量更新();
                        已交數量更新();
                        productAdapter.Update(myds, "產品資料表");

                        MessageBox.Show("已修改");
                        低於ROP();
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

        public void revise()
        {
            myds.Tables["銷貨單資料表"].Clear();
            adapter.Fill(myds, "銷貨單資料表");

            textBox1.Text = _sale;
            imagedComboBox1.SelectedIndex = category;

            myds.Tables["銷貨單明細資料表"].Clear();
            scheduleAdapter = new MySqlDataAdapter("SELECT * FROM 銷貨單明細資料表 WHERE 銷貨單號 = " + textBox1.Text + ";", conn);
            scheduleAdapter.Fill(myds, "銷貨單明細資料表");

            新增與刪除切換(true);

            commandSet();
            reSelect();
        }

        private bool checkAll()
        {
            bool pass = true;

            //以下檢查數量是否都有填入，出貨後庫存量是否有負數
            foreach (DataGridViewRow v in dataGridView1.Rows)
            {
                if (v.Cells["數量"].Value == null && !v.IsNewRow)
                {
                    pass = false;
                    MessageBox.Show("請檢查是否有未填欄位！");
                }

                if (v.Cells["訂單編號"].Value == null && !v.IsNewRow)
                {
                    pass = false;
                    MessageBox.Show("請檢查是否有未填欄位！");
                }

                if (v.Cells["出貨後數量"].Style.ForeColor == Color.Red)
                {
                    pass = false;
                    DialogResult dialogResult;
                    dialogResult = MessageBox.Show("注意：有庫存量不足的情況！\n是否排定生產單？", "生產單建立", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        produce p = new produce();
                        p.ShowDialog();
                    }
                }
            }

            return pass;
        }

        private void calStorage()
        {
            for (int i = 0; i < myds.Tables["銷貨單明細資料表"].Rows.Count; i++)
            {
                dataGridView1["出貨後數量", i].Value = int.Parse(dataGridView1["目前庫存數量", i].Value.ToString()) - int.Parse(dataGridView1["數量", i].Value.ToString());
                dataGridView1["出貨後數量", i].Style.ForeColor = int.Parse(dataGridView1["出貨後數量", i].Value.ToString()) < 0 ? Color.Red : Color.Black;
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dataGridView1["金額", e.RowIndex].Value = double.Parse(dataGridView1["單價", e.RowIndex].Value.ToString()) * double.Parse(dataGridView1["數量", e.RowIndex].Value.ToString());
                dataGridView1["銷貨單號", e.RowIndex].Value = textBox1.Text;
                calStorage();
            }
            catch { }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
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

        private void 新增與刪除切換(Boolean i)
        {
            if (i)
            {
                dateTimePicker1.Enabled = false;
                button1.Text = "修改";
            }
            else
            {
                dateTimePicker1.Enabled = true;
                button1.Text = "新增";
            }
        }

        private void commandSet()
        {
            adapter.InsertCommand = new MySqlCommand("INSERT INTO 銷貨單資料表(匯率表id) VALUES(@匯率表id);", conn);
            adapter.InsertCommand.Parameters.Add("@匯率表id", MySqlDbType.Int32, 100, "匯率表id");

            scheduleAdapter.InsertCommand = new MySqlCommand("INSERT INTO 銷貨單明細資料表(銷貨單號, 品號, 訂單編號, 數量, 單價) VALUES(@銷貨單號, @品號, @訂單編號, @數量, @單價);", conn);
            scheduleAdapter.InsertCommand.Parameters.Add("@銷貨單號", MySqlDbType.Int32, 11, "銷貨單號");
            scheduleAdapter.InsertCommand.Parameters.Add("@品號", MySqlDbType.Int32, 11, "品號");
            scheduleAdapter.InsertCommand.Parameters.Add("@訂單編號", MySqlDbType.Int32, 11, "訂單編號");
            scheduleAdapter.InsertCommand.Parameters.Add("@數量", MySqlDbType.Int32, 11, "數量");
            scheduleAdapter.InsertCommand.Parameters.Add("@單價", MySqlDbType.Double, 100, "單價");

            scheduleAdapter.UpdateCommand = new MySqlCommand("UPDATE 銷貨單明細資料表 SET 品號=@品號, 訂單編號=@訂單編號, 數量=@數量, 單價=@單價 WHERE 銷貨單號=@銷貨單編號 and 品號=@品號; ", conn);
            scheduleAdapter.UpdateCommand.Parameters.Add("@銷貨單號", MySqlDbType.Int32, 11, "銷貨單號");
            scheduleAdapter.UpdateCommand.Parameters.Add("@訂單編號", MySqlDbType.Int32, 11, "訂單編號");
            scheduleAdapter.UpdateCommand.Parameters.Add("@品號", MySqlDbType.Int32, 11, "品號");
            scheduleAdapter.UpdateCommand.Parameters.Add("@數量", MySqlDbType.Int32, 11, "數量");
            scheduleAdapter.UpdateCommand.Parameters.Add("@單價", MySqlDbType.Double, 100, "單價");

            scheduleAdapter.DeleteCommand = new MySqlCommand("DELETE FROM 銷貨單明細資料表 WHERE 銷貨單號=@銷貨單號 and 品號=@品號;", conn);
            scheduleAdapter.DeleteCommand.Parameters.Add("@銷貨單號", MySqlDbType.Int32, 11, "銷貨單號");
            scheduleAdapter.DeleteCommand.Parameters.Add("@品號", MySqlDbType.Int32, 11, "品號");

            productAdapter.UpdateCommand = new MySqlCommand("UPDATE 產品資料表 SET 庫存量=@庫存量 WHERE 品號=@品號;", conn);
            productAdapter.UpdateCommand.Parameters.Add("@庫存量", MySqlDbType.Int32, 11, "庫存量");
            productAdapter.UpdateCommand.Parameters.Add("@品號", MySqlDbType.Int32, 11, "品號");

            orderAdapter2.UpdateCommand = new MySqlCommand("UPDATE 訂單明細資料表 SET 已交數量=@已交數量 WHERE 訂單編號=@訂單編號 and 品號=@品號;",conn);
            orderAdapter2.UpdateCommand.Parameters.Add("@已交數量", MySqlDbType.Int32, 11, "已交數量");
            orderAdapter2.UpdateCommand.Parameters.Add("@訂單編號", MySqlDbType.Int32, 11, "訂單編號");
            orderAdapter2.UpdateCommand.Parameters.Add("@品號", MySqlDbType.Int32, 11, "品號");
        }

        private void reSelect()
        {

            double sumPrice = 0;
            double sumQuantity = 0;
            DataRow dr;

            for (int i = 0; i < myds.Tables["銷貨單明細資料表"].Rows.Count; i++)
            {
                dr = myds.Tables["產品資料表"].Rows.Find(dataGridView1["品號", i].Value.ToString());
                dataGridView1["品名", i].Value = dr["品名"].ToString();
                dataGridView1["規格", i].Value = dr["規格"].ToString();
                dataGridView1["單價", i].Value = dr["單價"].ToString();
                dataGridView1["目前庫存數量", i].Value = int.Parse(dr["庫存量"].ToString()) + int.Parse(dataGridView1["數量", i].Value.ToString());
                dataGridView1["金額", i].Value = double.Parse(dataGridView1["單價", i].Value.ToString()) * double.Parse(dataGridView1["數量", i].Value.ToString());
                sumPrice += double.Parse(dataGridView1["金額", i].Value.ToString());
                sumQuantity += double.Parse(dataGridView1["數量", i].Value.ToString());
            }
            textBox4.Text = sumPrice.ToString();
            textBox5.Text = sumQuantity.ToString();
            calStorage();
        }

        private void 庫存量更新()
        {
            DataRow dr;
            foreach (DataGridViewRow v in dataGridView1.Rows)
            {
                try {
                    dr = myds.Tables["產品資料表"].Rows.Find(v.Cells["品號"].Value.ToString());
                    dr["庫存量"] = v.Cells["出貨後數量"].Value.ToString();
                }
                catch
                {

                }
            }
                    
        }

        private void 已交數量更新()
        {
            DataRow dr;
            foreach (DataGridViewRow v in dataGridView1.Rows)
            {
                try
                {
                    object[] search = new object[] { v.Cells["訂單編號"].Value.ToString(), v.Cells["品號"].Value.ToString() };
                    dr = myds.Tables["訂單明細資料表"].Rows.Find(search);
                    dr["已交數量"] = v.Cells["數量"].Value.ToString();
                    orderAdapter2.Update(myds.Tables["訂單明細資料表"]);
                }
                catch
                {

                }
            }

        }

        private void SaleSlip_FormClosing(object sender, FormClosingEventArgs e)
        {
            int added = 0;
            int deleted = 0;
            int modified = 0;

            foreach (DataRow dr in myds.Tables["銷貨單明細資料表"].Rows)

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
                            scheduleAdapter.Update(myds, "銷貨單明細資料表");
                        }
                        catch
                        {
                            DataRow dr = myds.Tables["銷貨單資料表"].NewRow();
                            dr["銷貨單號"] = textBox1.Text;
                            dr["匯率表id"] = myds.Tables["匯率資料表"].Rows[imagedComboBox1.SelectedIndex][0].ToString();

                            myds.Tables["銷貨單資料表"].Rows.Add(dr);
                            adapter.Update(myds, "銷貨單資料表");

                            scheduleAdapter.Update(myds, "銷貨單明細資料表");

                            庫存量更新();
                            已交數量更新();
                            productAdapter.Update(myds, "產品資料表");

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

        private void 低於ROP()
        {
            Predict p = new Predict();
            foreach (DataGridViewRow v in dataGridView1.Rows)
            {
                p.setI(int.Parse(v.Cells["品號"].Value.ToString()));
                double a = int.Parse(v.Cells["出貨後數量"].Value.ToString());
                double b = p.ROP();
                if (a < b)
                {
                    DialogResult dialogResult = MessageBox.Show("品號："+ v.Cells["品號"].Value.ToString() + "剩餘數量低於ROP點", "要安排生產計畫嗎？", MessageBoxButtons.YesNoCancel);
                    if (dialogResult == DialogResult.Yes)
                    {
                        plan pp = new plan(v.Cells["品號"].Value.ToString(), p.EOQ().ToString());
                        pp.ShowDialog();
                    }
                    p.Show();
                }
            }
        }

        private void 檢視訂單完成度()
        {
            myds.Tables["訂單資料表"].Clear();
            orderAdapter = new MySqlDataAdapter("SELECT * FROM 訂單資料表 WHERE 訂單完成狀態=0;", conn);
            orderAdapter.Fill(myds, "訂單資料表");
            myds.Tables["訂單資料表"].PrimaryKey = new DataColumn[] { myds.Tables["訂單資料表"].Columns["訂單編號"] };
            MySqlDataReader rd;

            foreach (DataRow v in myds.Tables["訂單資料表"].Rows)
            {
                conn.Open();
                command.CommandText = "SELECT * FROM 訂單明細資料表 WHERE 訂單編號=" + v["訂單編號"].ToString() + " AND 已交數量<數量;";
                rd = command.ExecuteReader();
                if (!rd.Read())
                {
                    v["訂單完成狀態"] = 1;
                }
                conn.Close();
            }


            MySqlCommandBuilder mycb = new MySqlCommandBuilder(orderAdapter);
            orderAdapter.Update(myds.Tables["訂單資料表"]);
        }

    }


}
