namespace seminar
{
    partial class Receipts
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
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridViewEx1 = new seminar.DataGridViewEx();
            this.入庫單編號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.日期2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.品號2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.入庫數量 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridView1 = new seminar.DataGridViewEx();
            this.序號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.已完成 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.日期 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.訂單 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.品號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.產品 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.預計數量 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.完成狀態 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEx1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(620, 285);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(130, 30);
            this.button1.TabIndex = 3;
            this.button1.Text = "儲存";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "生產計劃列表：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 300);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "入庫單列表：";
            // 
            // dataGridViewEx1
            // 
            this.dataGridViewEx1.AllowUserToAddRows = false;
            this.dataGridViewEx1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewEx1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.入庫單編號,
            this.日期2,
            this.品號2,
            this.入庫數量});
            this.dataGridViewEx1.Location = new System.Drawing.Point(12, 341);
            this.dataGridViewEx1.Name = "dataGridViewEx1";
            this.dataGridViewEx1.ReadOnly = true;
            this.dataGridViewEx1.RowTemplate.Height = 27;
            this.dataGridViewEx1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewEx1.Size = new System.Drawing.Size(1318, 400);
            this.dataGridViewEx1.TabIndex = 2;
            // 
            // 入庫單編號
            // 
            this.入庫單編號.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.入庫單編號.DataPropertyName = "入庫單編號";
            this.入庫單編號.HeaderText = "入庫單編號";
            this.入庫單編號.Name = "入庫單編號";
            this.入庫單編號.ReadOnly = true;
            // 
            // 日期2
            // 
            this.日期2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.日期2.DataPropertyName = "日期";
            this.日期2.HeaderText = "日期";
            this.日期2.Name = "日期2";
            this.日期2.ReadOnly = true;
            // 
            // 品號2
            // 
            this.品號2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.品號2.DataPropertyName = "品號";
            this.品號2.HeaderText = "品號";
            this.品號2.Name = "品號2";
            this.品號2.ReadOnly = true;
            // 
            // 入庫數量
            // 
            this.入庫數量.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.入庫數量.DataPropertyName = "入庫數量";
            this.入庫數量.HeaderText = "入庫數量";
            this.入庫數量.Name = "入庫數量";
            this.入庫數量.ReadOnly = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.序號,
            this.已完成,
            this.日期,
            this.訂單,
            this.品號,
            this.產品,
            this.預計數量,
            this.完成狀態});
            this.dataGridView1.Location = new System.Drawing.Point(12, 37);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 27;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1318, 225);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // 序號
            // 
            this.序號.DataPropertyName = "序號";
            this.序號.HeaderText = "序號";
            this.序號.Name = "序號";
            this.序號.ReadOnly = true;
            // 
            // 已完成
            // 
            this.已完成.FalseValue = "False";
            this.已完成.HeaderText = "已完成";
            this.已完成.Name = "已完成";
            this.已完成.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.已完成.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.已完成.TrueValue = "True";
            // 
            // 日期
            // 
            this.日期.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.日期.DataPropertyName = "日期";
            this.日期.HeaderText = "日期";
            this.日期.Name = "日期";
            this.日期.ReadOnly = true;
            // 
            // 訂單
            // 
            this.訂單.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.訂單.DataPropertyName = "訂單";
            this.訂單.HeaderText = "訂單";
            this.訂單.Name = "訂單";
            this.訂單.ReadOnly = true;
            // 
            // 品號
            // 
            this.品號.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.品號.DataPropertyName = "品號";
            this.品號.HeaderText = "品號";
            this.品號.Name = "品號";
            this.品號.ReadOnly = true;
            // 
            // 產品
            // 
            this.產品.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.產品.DataPropertyName = "產品";
            this.產品.HeaderText = "產品";
            this.產品.Name = "產品";
            this.產品.ReadOnly = true;
            // 
            // 預計數量
            // 
            this.預計數量.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.預計數量.DataPropertyName = "預計數量";
            this.預計數量.HeaderText = "預計數量";
            this.預計數量.Name = "預計數量";
            this.預計數量.ReadOnly = true;
            // 
            // 完成狀態
            // 
            this.完成狀態.DataPropertyName = "完成狀態";
            this.完成狀態.HeaderText = "完成狀態";
            this.完成狀態.Name = "完成狀態";
            this.完成狀態.ReadOnly = true;
            this.完成狀態.Visible = false;
            // 
            // Receipts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1342, 753);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridViewEx1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Receipts";
            this.Text = "入庫單";
            this.Load += new System.EventHandler(this.Receipts_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEx1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DataGridViewEx dataGridView1;
        private DataGridViewEx dataGridViewEx1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn 入庫單編號;
        private System.Windows.Forms.DataGridViewTextBoxColumn 日期2;
        private System.Windows.Forms.DataGridViewTextBoxColumn 品號2;
        private System.Windows.Forms.DataGridViewTextBoxColumn 入庫數量;
        private System.Windows.Forms.DataGridViewTextBoxColumn 序號;
        private System.Windows.Forms.DataGridViewCheckBoxColumn 已完成;
        private System.Windows.Forms.DataGridViewTextBoxColumn 日期;
        private System.Windows.Forms.DataGridViewTextBoxColumn 訂單;
        private System.Windows.Forms.DataGridViewTextBoxColumn 品號;
        private System.Windows.Forms.DataGridViewTextBoxColumn 產品;
        private System.Windows.Forms.DataGridViewTextBoxColumn 預計數量;
        private System.Windows.Forms.DataGridViewTextBoxColumn 完成狀態;
    }
}