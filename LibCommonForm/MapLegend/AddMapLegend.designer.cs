namespace LibCommonForm
{
    partial class AddMapLegend
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvLegendList = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.选择 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.图例名称 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.样式 = new System.Windows.Forms.DataGridViewImageColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
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
            this.Column1});
            this.dgvLegendList.Location = new System.Drawing.Point(7, 37);
            this.dgvLegendList.Name = "dgvLegendList";
            this.dgvLegendList.RowTemplate.Height = 23;
            this.dgvLegendList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLegendList.Size = new System.Drawing.Size(751, 453);
            this.dgvLegendList.TabIndex = 38;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(183, 8);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 39;
            this.button1.Text = "打开图片";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(264, 8);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 40;
            this.button2.Text = "保存数据";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // 选择
            // 
            this.选择.FillWeight = 11.07868F;
            this.选择.HeaderText = "选择";
            this.选择.Name = "选择";
            // 
            // 图例名称
            // 
            this.图例名称.FillWeight = 22.15736F;
            this.图例名称.HeaderText = "图例名称";
            this.图例名称.Name = "图例名称";
            this.图例名称.ReadOnly = true;
            // 
            // 样式
            // 
            this.样式.FillWeight = 55.3934F;
            this.样式.HeaderText = "样式";
            this.样式.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.样式.Name = "样式";
            // 
            // Column1
            // 
            this.Column1.FillWeight = 40F;
            this.Column1.HeaderText = "路径";
            this.Column1.Name = "Column1";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(12, 8);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 41;
            this.button3.Text = "全选";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(93, 8);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 42;
            this.button4.Text = "取消全选";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(770, 502);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dgvLegendList);
            this.Name = "Form1";
            this.Text = "添加图例";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLegendList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvLegendList;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn 选择;
        private System.Windows.Forms.DataGridViewTextBoxColumn 图例名称;
        private System.Windows.Forms.DataGridViewImageColumn 样式;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
    }
}

