namespace LibCommonControl
{
    partial class DataPager
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnLastPage = new System.Windows.Forms.Button();
            this.btnNextPage = new System.Windows.Forms.Button();
            this.lblPage2 = new System.Windows.Forms.Label();
            this.btnPrePage = new System.Windows.Forms.Button();
            this.lblGoto = new System.Windows.Forms.Label();
            this.btnFirstPage = new System.Windows.Forms.Button();
            this.lblPage = new System.Windows.Forms.Label();
            this.txtCurrentPage = new System.Windows.Forms.TextBox();
            this.btnGotoPage = new System.Windows.Forms.Button();
            this.cboPageSize = new System.Windows.Forms.ComboBox();
            this.lblDisplay = new System.Windows.Forms.Label();
            this.lblCountOfPage = new System.Windows.Forms.Label();
            this.lblTotalPage = new System.Windows.Forms.Label();
            this.lblTotalPages = new System.Windows.Forms.Label();
            this.txtGoto = new System.Windows.Forms.TextBox();
            this.lblTotalData = new System.Windows.Forms.Label();
            this.txtTotalCount = new System.Windows.Forms.TextBox();
            this.lblData = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnLastPage
            // 
            this.btnLastPage.Location = new System.Drawing.Point(450, 7);
            this.btnLastPage.Name = "btnLastPage";
            this.btnLastPage.Size = new System.Drawing.Size(75, 23);
            this.btnLastPage.TabIndex = 7;
            this.btnLastPage.Text = "尾页";
            this.btnLastPage.UseVisualStyleBackColor = true;
            this.btnLastPage.Click += new System.EventHandler(this.btnLastPage_Click);
            // 
            // btnNextPage
            // 
            this.btnNextPage.Location = new System.Drawing.Point(369, 7);
            this.btnNextPage.Name = "btnNextPage";
            this.btnNextPage.Size = new System.Drawing.Size(75, 23);
            this.btnNextPage.TabIndex = 6;
            this.btnNextPage.Text = "下一页";
            this.btnNextPage.UseVisualStyleBackColor = true;
            this.btnNextPage.Click += new System.EventHandler(this.btnNextPage_Click);
            // 
            // lblPage2
            // 
            this.lblPage2.AutoSize = true;
            this.lblPage2.Location = new System.Drawing.Point(709, 13);
            this.lblPage2.Name = "lblPage2";
            this.lblPage2.Size = new System.Drawing.Size(17, 12);
            this.lblPage2.TabIndex = 13;
            this.lblPage2.Text = "页";
            // 
            // btnPrePage
            // 
            this.btnPrePage.Location = new System.Drawing.Point(239, 6);
            this.btnPrePage.Name = "btnPrePage";
            this.btnPrePage.Size = new System.Drawing.Size(75, 23);
            this.btnPrePage.TabIndex = 4;
            this.btnPrePage.Text = "上一页";
            this.btnPrePage.UseVisualStyleBackColor = true;
            this.btnPrePage.Click += new System.EventHandler(this.btnPrePage_Click);
            // 
            // lblGoto
            // 
            this.lblGoto.AutoSize = true;
            this.lblGoto.Location = new System.Drawing.Point(626, 12);
            this.lblGoto.Name = "lblGoto";
            this.lblGoto.Size = new System.Drawing.Size(29, 12);
            this.lblGoto.TabIndex = 11;
            this.lblGoto.Text = "到第";
            // 
            // btnFirstPage
            // 
            this.btnFirstPage.Location = new System.Drawing.Point(158, 6);
            this.btnFirstPage.Name = "btnFirstPage";
            this.btnFirstPage.Size = new System.Drawing.Size(75, 23);
            this.btnFirstPage.TabIndex = 3;
            this.btnFirstPage.Text = "首页";
            this.btnFirstPage.UseVisualStyleBackColor = true;
            this.btnFirstPage.Click += new System.EventHandler(this.btnFirstPage_Click);
            // 
            // lblPage
            // 
            this.lblPage.AutoSize = true;
            this.lblPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblPage.Location = new System.Drawing.Point(591, 13);
            this.lblPage.Name = "lblPage";
            this.lblPage.Size = new System.Drawing.Size(17, 12);
            this.lblPage.TabIndex = 10;
            this.lblPage.Text = "页";
            // 
            // txtCurrentPage
            // 
            this.txtCurrentPage.BackColor = System.Drawing.SystemColors.Control;
            this.txtCurrentPage.Enabled = false;
            this.txtCurrentPage.Location = new System.Drawing.Point(320, 8);
            this.txtCurrentPage.Name = "txtCurrentPage";
            this.txtCurrentPage.Size = new System.Drawing.Size(42, 21);
            this.txtCurrentPage.TabIndex = 5;
            this.txtCurrentPage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnGotoPage
            // 
            this.btnGotoPage.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnGotoPage.Font = new System.Drawing.Font("SimSun", 5F);
            this.btnGotoPage.Location = new System.Drawing.Point(747, 12);
            this.btnGotoPage.Name = "btnGotoPage";
            this.btnGotoPage.Size = new System.Drawing.Size(26, 15);
            this.btnGotoPage.TabIndex = 14;
            this.btnGotoPage.Text = ">>";
            this.btnGotoPage.UseVisualStyleBackColor = true;
            this.btnGotoPage.Click += new System.EventHandler(this.btnGotoPage_Click);
            // 
            // cboPageSize
            // 
            this.cboPageSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPageSize.FormattingEnabled = true;
            this.cboPageSize.Items.AddRange(new object[] {
            "10",
            "20",
            "30",
            "50",
            "100"});
            this.cboPageSize.Location = new System.Drawing.Point(43, 8);
            this.cboPageSize.Name = "cboPageSize";
            this.cboPageSize.Size = new System.Drawing.Size(68, 20);
            this.cboPageSize.TabIndex = 1;
            this.cboPageSize.SelectedValueChanged += new System.EventHandler(this.cboPageSize_SelectedValueChanged);
            // 
            // lblDisplay
            // 
            this.lblDisplay.AutoSize = true;
            this.lblDisplay.Location = new System.Drawing.Point(8, 11);
            this.lblDisplay.Name = "lblDisplay";
            this.lblDisplay.Size = new System.Drawing.Size(29, 12);
            this.lblDisplay.TabIndex = 0;
            this.lblDisplay.Text = "显示";
            // 
            // lblCountOfPage
            // 
            this.lblCountOfPage.AutoSize = true;
            this.lblCountOfPage.Location = new System.Drawing.Point(117, 11);
            this.lblCountOfPage.Name = "lblCountOfPage";
            this.lblCountOfPage.Size = new System.Drawing.Size(35, 12);
            this.lblCountOfPage.TabIndex = 2;
            this.lblCountOfPage.Text = "条/页";
            // 
            // lblTotalPage
            // 
            this.lblTotalPage.AutoSize = true;
            this.lblTotalPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblTotalPage.Location = new System.Drawing.Point(541, 13);
            this.lblTotalPage.Name = "lblTotalPage";
            this.lblTotalPage.Size = new System.Drawing.Size(17, 12);
            this.lblTotalPage.TabIndex = 8;
            this.lblTotalPage.Text = "共";
            // 
            // lblTotalPages
            // 
            this.lblTotalPages.AutoSize = true;
            this.lblTotalPages.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblTotalPages.Location = new System.Drawing.Point(564, 12);
            this.lblTotalPages.Name = "lblTotalPages";
            this.lblTotalPages.Size = new System.Drawing.Size(0, 12);
            this.lblTotalPages.TabIndex = 9;
            // 
            // txtGoto
            // 
            this.txtGoto.BackColor = System.Drawing.Color.White;
            this.txtGoto.Location = new System.Drawing.Point(661, 9);
            this.txtGoto.MaxLength = 5;
            this.txtGoto.Name = "txtGoto";
            this.txtGoto.Size = new System.Drawing.Size(42, 21);
            this.txtGoto.TabIndex = 12;
            this.txtGoto.Text = "1";
            this.txtGoto.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtGoto.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtGoto_KeyDown);
            // 
            // lblTotalData
            // 
            this.lblTotalData.AutoSize = true;
            this.lblTotalData.Location = new System.Drawing.Point(792, 13);
            this.lblTotalData.Name = "lblTotalData";
            this.lblTotalData.Size = new System.Drawing.Size(29, 12);
            this.lblTotalData.TabIndex = 15;
            this.lblTotalData.Text = "总共";
            // 
            // txtTotalCount
            // 
            this.txtTotalCount.BackColor = System.Drawing.Color.White;
            this.txtTotalCount.ForeColor = System.Drawing.Color.Red;
            this.txtTotalCount.Location = new System.Drawing.Point(827, 9);
            this.txtTotalCount.MaxLength = 5;
            this.txtTotalCount.Name = "txtTotalCount";
            this.txtTotalCount.ReadOnly = true;
            this.txtTotalCount.Size = new System.Drawing.Size(42, 21);
            this.txtTotalCount.TabIndex = 16;
            this.txtTotalCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblData
            // 
            this.lblData.AutoSize = true;
            this.lblData.Location = new System.Drawing.Point(875, 13);
            this.lblData.Name = "lblData";
            this.lblData.Size = new System.Drawing.Size(41, 12);
            this.lblData.TabIndex = 17;
            this.lblData.Text = "条记录";
            // 
            // DataPager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblData);
            this.Controls.Add(this.txtTotalCount);
            this.Controls.Add(this.lblTotalData);
            this.Controls.Add(this.txtGoto);
            this.Controls.Add(this.lblTotalPage);
            this.Controls.Add(this.btnLastPage);
            this.Controls.Add(this.btnNextPage);
            this.Controls.Add(this.lblPage2);
            this.Controls.Add(this.btnPrePage);
            this.Controls.Add(this.lblGoto);
            this.Controls.Add(this.btnFirstPage);
            this.Controls.Add(this.lblTotalPages);
            this.Controls.Add(this.lblPage);
            this.Controls.Add(this.txtCurrentPage);
            this.Controls.Add(this.btnGotoPage);
            this.Controls.Add(this.cboPageSize);
            this.Controls.Add(this.lblDisplay);
            this.Controls.Add(this.lblCountOfPage);
            this.Name = "DataPager";
            this.Size = new System.Drawing.Size(925, 37);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLastPage;
        private System.Windows.Forms.Button btnNextPage;
        private System.Windows.Forms.Label lblPage2;
        private System.Windows.Forms.Button btnPrePage;
        private System.Windows.Forms.Label lblGoto;
        private System.Windows.Forms.Button btnFirstPage;
        private System.Windows.Forms.Label lblPage;
        private System.Windows.Forms.TextBox txtCurrentPage;
        private System.Windows.Forms.Button btnGotoPage;
        private System.Windows.Forms.ComboBox cboPageSize;
        private System.Windows.Forms.Label lblDisplay;
        private System.Windows.Forms.Label lblCountOfPage;
        private System.Windows.Forms.Label lblTotalPage;
        private System.Windows.Forms.Label lblTotalPages;
        private System.Windows.Forms.TextBox txtGoto;
        private System.Windows.Forms.Label lblTotalData;
        private System.Windows.Forms.TextBox txtTotalCount;
        private System.Windows.Forms.Label lblData;
    }
}
