namespace seminar
{
    partial class OrderSelect
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
            this.訂單編號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.客戶名稱 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.幣別 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.日期 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.匯率 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.客戶代號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewEx1 = new seminar.DataGridViewEx();
            this.銷貨單號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.品號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.數量 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.單價 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.已交數量 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEx1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.訂單編號,
            this.客戶名稱,
            this.幣別,
            this.日期,
            this.匯率,
            this.客戶代號});
            this.dataGridView1.Enabled = false;
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 27;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1318, 300);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dataGridView1.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_RowEnter);
            // 
            // 訂單編號
            // 
            this.訂單編號.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.訂單編號.DataPropertyName = "訂單編號";
            this.訂單編號.HeaderText = "訂單編號";
            this.訂單編號.Name = "訂單編號";
            this.訂單編號.ReadOnly = true;
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
            this.銷貨單號,
            this.品號,
            this.數量,
            this.單價,
            this.已交數量});
            this.dataGridViewEx1.Location = new System.Drawing.Point(12, 338);
            this.dataGridViewEx1.Name = "dataGridViewEx1";
            this.dataGridViewEx1.ReadOnly = true;
            this.dataGridViewEx1.RowTemplate.Height = 27;
            this.dataGridViewEx1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewEx1.Size = new System.Drawing.Size(1318, 403);
            this.dataGridViewEx1.TabIndex = 2;
            this.dataGridViewEx1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewEx1_CellDoubleClick);
            // 
            // 銷貨單號
            // 
            this.銷貨單號.DataPropertyName = "銷貨單號";
            this.銷貨單號.HeaderText = "銷貨單號";
            this.銷貨單號.Name = "銷貨單號";
            this.銷貨單號.ReadOnly = true;
            this.銷貨單號.Visible = false;
            this.銷貨單號.Width = 319;
            // 
            // 品號
            // 
            this.品號.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.品號.DataPropertyName = "品號";
            this.品號.HeaderText = "品號";
            this.品號.Name = "品號";
            this.品號.ReadOnly = true;
            // 
            // 數量
            // 
            this.數量.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.數量.DataPropertyName = "數量";
            this.數量.HeaderText = "數量";
            this.數量.Name = "數量";
            this.數量.ReadOnly = true;
            // 
            // 單價
            // 
            this.單價.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.單價.DataPropertyName = "單價";
            this.單價.HeaderText = "單價";
            this.單價.Name = "單價";
            this.單價.ReadOnly = true;
            // 
            // 已交數量
            // 
            this.已交數量.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.已交數量.DataPropertyName = "已交數量";
            this.已交數量.HeaderText = "已交數量";
            this.已交數量.Name = "已交數量";
            this.已交數量.ReadOnly = true;
            // 
            // OrderSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1342, 753);
            this.Controls.Add(this.dataGridViewEx1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "OrderSelect";
            this.Text = "訂單查詢";
            this.Load += new System.EventHandler(this.OrderSelect_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEx1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DataGridViewEx dataGridView1;
        private DataGridViewEx dataGridViewEx1;
        private System.Windows.Forms.DataGridViewTextBoxColumn 訂單編號;
        private System.Windows.Forms.DataGridViewTextBoxColumn 客戶名稱;
        private System.Windows.Forms.DataGridViewTextBoxColumn 幣別;
        private System.Windows.Forms.DataGridViewTextBoxColumn 日期;
        private System.Windows.Forms.DataGridViewTextBoxColumn 匯率;
        private System.Windows.Forms.DataGridViewTextBoxColumn 客戶代號;
        private System.Windows.Forms.DataGridViewTextBoxColumn 銷貨單號;
        private System.Windows.Forms.DataGridViewTextBoxColumn 品號;
        private System.Windows.Forms.DataGridViewTextBoxColumn 數量;
        private System.Windows.Forms.DataGridViewTextBoxColumn 單價;
        private System.Windows.Forms.DataGridViewTextBoxColumn 已交數量;
    }
}