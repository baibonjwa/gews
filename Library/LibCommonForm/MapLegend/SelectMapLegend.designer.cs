namespace LibCommonForm
{
    partial class SelectMapLegend
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
            this.dgvLegendList = new System.Windows.Forms.DataGridView();
            this.选择 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.图例名称 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.样式 = new System.Windows.Forms.DataGridViewImageColumn();
            this.BID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLegendList)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvLegendList
            // 
            this.dgvLegendList.AllowUserToAddRows = false;
            this.dgvLegendList.AllowUserToDeleteRows = false;
            this.dgvLegendList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLegendList.BackgroundColor = System.Drawing.Color.White;
            this.dgvLegendList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLegendList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.选择,
            this.图例名称,
            this.样式,
            this.BID,
            this.Column1});
            this.dgvLegendList.Location = new System.Drawing.Point(23, 55);
            this.dgvLegendList.Name = "dgvLegendList";
            this.dgvLegendList.RowTemplate.Height = 23;
            this.dgvLegendList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLegendList.Size = new System.Drawing.Size(830, 439);
            this.dgvLegendList.TabIndex = 39;
            // 
            // 选择
            // 
            this.选择.FillWeight = 10F;
            this.选择.HeaderText = "选择";
            this.选择.Name = "选择";
            // 
            // 图例名称
            // 
            this.图例名称.FillWeight = 20F;
            this.图例名称.HeaderText = "图例名称";
            this.图例名称.Name = "图例名称";
            this.图例名称.ReadOnly = true;
            // 
            // 样式
            // 
            this.样式.FillWeight = 50F;
            this.样式.HeaderText = "样式";
            this.样式.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.样式.Name = "样式";
            // 
            // BID
            // 
            this.BID.FillWeight = 10F;
            this.BID.HeaderText = "BID";
            this.BID.Name = "BID";
            this.BID.Visible = false;
            // 
            // Column1
            // 
            this.Column1.FillWeight = 5F;
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            this.Column1.Visible = false;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(104, 12);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 44;
            this.button4.Text = "取消全选";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(23, 12);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 43;
            this.button3.Text = "全选";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(221, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 45;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // SelectMapLegend
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(901, 506);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.dgvLegendList);
            this.Name = "SelectMapLegend";
            this.Text = "选择图例";
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLegendList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvLegendList;
        private System.Windows.Forms.DataGridViewCheckBoxColumn 选择;
        private System.Windows.Forms.DataGridViewTextBoxColumn 图例名称;
        private System.Windows.Forms.DataGridViewImageColumn 样式;
        private System.Windows.Forms.DataGridViewTextBoxColumn BID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button1;
    }
}