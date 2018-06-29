namespace seminar
{
    partial class ViewOrder
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new seminar.DataGridViewEx();
            this.銷貨單號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.客戶名稱 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.幣別 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.日期 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.匯率 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.客戶代號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewEx1 = new seminar.DataGridViewEx();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.已交數量 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEx1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.銷貨單號,
            this.客戶名稱,
            this.幣別,
            this.日期,
            this.匯率,
            this.客戶代號});
            this.dataGridView1.Enabled = false;
            this.dataGridView1.Location = new System.Drawing.Point(12, 61);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 27;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1318, 250);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_RowEnter);
            // 
            // 銷貨單號
            // 
            this.銷貨單號.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.銷貨單號.DataPropertyName = "銷貨單號";
            this.銷貨單號.HeaderText = "訂單編號";
            this.銷貨單號.Name = "銷貨單號";
            this.銷貨單號.ReadOnly = true;
            // 
            // 客戶名稱
            // 
            this.客戶名稱.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.客戶名稱.DataPropertyName = "客戶名稱";
            this.客戶名稱.HeaderText = "客戶名稱";
            this.客戶名稱.Name = "客戶名稱";
            this.客戶名稱.ReadOnly = true;
            // 
            // 幣別
            // 
            this.幣別.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.幣別.DataPropertyName = "幣別";
            this.幣別.HeaderText = "幣別";
            this.幣別.Name = "幣別";
            this.幣別.ReadOnly = true;
            // 
            // 日期
            // 
            this.日期.DataPropertyName = "日期";
            this.日期.HeaderText = "日期";
            this.日期.Name = "日期";
            this.日期.ReadOnly = true;
            // 
            // 匯率
            // 
            this.匯率.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.匯率.DataPropertyName = "匯率";
            this.匯率.HeaderText = "匯率";
            this.匯率.Name = "匯率";
            this.匯率.ReadOnly = true;
            // 
            // 客戶代號
            // 
            this.客戶代號.DataPropertyName = "客戶代號";
            this.客戶代號.HeaderText = "客戶代號";
            this.客戶代號.Name = "客戶代號";
            this.客戶代號.ReadOnly = true;
            this.客戶代號.Visible = false;
            // 
            // dataGridViewEx1
            // 
            this.dataGridViewEx1.AllowUserToAddRows = false;
            this.dataGridViewEx1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewEx1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.已交數量});
            this.dataGridViewEx1.Enabled = false;
            this.dataGridViewEx1.Location = new System.Drawing.Point(12, 338);
            this.dataGridViewEx1.Name = "dataGridViewEx1";
            this.dataGridViewEx1.ReadOnly = true;
            this.dataGridViewEx1.RowTemplate.Height = 27;
            this.dataGridViewEx1.Size = new System.Drawing.Size(1318, 403);
            this.dataGridViewEx1.TabIndex = 2;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "訂單編號";
            this.Column1.HeaderText = "訂單編號";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Visible = false;
            this.Column1.Width = 319;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column2.DataPropertyName = "品號";
            this.Column2.HeaderText = "品號";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column3.DataPropertyName = "數量";
            this.Column3.HeaderText = "數量";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column4.DataPropertyName = "單價";
            this.Column4.HeaderText = "單價";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // 已交數量
            // 
            this.已交數量.DataPropertyName = "已交數量";
            this.已交數量.HeaderText = "已交數量";
            this.已交數量.Name = "已交數量";
            this.已交數量.ReadOnly = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(884, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(220, 43);
            this.button1.TabIndex = 3;
            this.button1.Text = "刪除";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1110, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(220, 43);
            this.button2.TabIndex = 4;
            this.button2.Text = "修改";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "未完成",
            "已完成",
            "全部"});
            this.comboBox1.Location = new System.Drawing.Point(70, 20);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(224, 23);
            this.comboBox1.TabIndex = 5;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "篩選：";
            // 
            // ViewOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1342, 753);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridViewEx1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "ViewOrder";
            this.Text = "訂單查詢";
            this.Load += new System.EventHandler(this.ViewOrder_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEx1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DataGridViewEx dataGridView1;
        private DataGridViewEx dataGridViewEx1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridViewTextBoxColumn 銷貨單號;
        private System.Windows.Forms.DataGridViewTextBoxColumn 客戶名稱;
        private System.Windows.Forms.DataGridViewTextBoxColumn 幣別;
        private System.Windows.Forms.DataGridViewTextBoxColumn 日期;
        private System.Windows.Forms.DataGridViewTextBoxColumn 匯率;
        private System.Windows.Forms.DataGridViewTextBoxColumn 客戶代號;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn 已交數量;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
    }
}