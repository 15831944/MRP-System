namespace seminar
{
    partial class Mould
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.模具編號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.機台編號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.品號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.模具數量 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.模具編號,
            this.機台編號,
            this.品號,
            this.模具數量});
            this.dataGridView1.Location = new System.Drawing.Point(12, 81);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 27;
            this.dataGridView1.Size = new System.Drawing.Size(1318, 460);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGridView1_CellBeginEdit);
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dataGridView1.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dataGridView1_UserAddedRow_1);
            // 
            // 模具編號
            // 
            this.模具編號.DataPropertyName = "模具編號";
            this.模具編號.HeaderText = "模具編號";
            this.模具編號.Name = "模具編號";
            this.模具編號.ReadOnly = true;
            // 
            // 機台編號
            // 
            this.機台編號.DataPropertyName = "機台編號";
            this.機台編號.HeaderText = "機台編號";
            this.機台編號.Name = "機台編號";
            this.機台編號.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // 品號
            // 
            this.品號.DataPropertyName = "品號";
            this.品號.HeaderText = "品號";
            this.品號.Name = "品號";
            // 
            // 模具數量
            // 
            this.模具數量.DataPropertyName = "模具數量";
            this.模具數量.HeaderText = "模具數量";
            this.模具數量.Name = "模具數量";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1210, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 50);
            this.button1.TabIndex = 1;
            this.button1.Text = "儲存";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Mould
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1342, 553);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Mould";
            this.Text = "模具資料";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Mould_FormClosing);
            this.Load += new System.EventHandler(this.Mould_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn 模具編號;
        private System.Windows.Forms.DataGridViewTextBoxColumn 機台編號;
        private System.Windows.Forms.DataGridViewTextBoxColumn 品號;
        private System.Windows.Forms.DataGridViewTextBoxColumn 模具數量;
    }
}