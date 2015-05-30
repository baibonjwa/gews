namespace GIS.SpecialGraphic
{
    partial class FrmManageZhzzt
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
            this.dgrdvZhzzt = new System.Windows.Forms.DataGridView();
            this.checkBoxAll = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.mycheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.MYName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.blc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnDelete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btnopen = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btnupdate = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgrdvZhzzt)).BeginInit();
            this.SuspendLayout();
            // 
            // dgrdvZhzzt
            // 
            this.dgrdvZhzzt.AllowUserToAddRows = false;
            this.dgrdvZhzzt.ColumnHeadersHeight = 30;
            this.dgrdvZhzzt.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgrdvZhzzt.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.mycheck,
            this.MYName,
            this.BID,
            this.blc,
            this.btnDelete,
            this.btnopen,
            this.btnupdate});
            this.dgrdvZhzzt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgrdvZhzzt.Location = new System.Drawing.Point(0, 0);
            this.dgrdvZhzzt.Name = "dgrdvZhzzt";
            this.dgrdvZhzzt.ReadOnly = true;
            this.dgrdvZhzzt.RowHeadersVisible = false;
            this.dgrdvZhzzt.RowTemplate.Height = 23;
            this.dgrdvZhzzt.ShowEditingIcon = false;
            this.dgrdvZhzzt.Size = new System.Drawing.Size(454, 531);
            this.dgrdvZhzzt.TabIndex = 29;
            this.dgrdvZhzzt.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgrdvZhzzt_CellContentClick);
            this.dgrdvZhzzt.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgrdvZhzzt_RowsAdded);
            // 
            // checkBoxAll
            // 
            this.checkBoxAll.AutoSize = true;
            this.checkBoxAll.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxAll.Location = new System.Drawing.Point(10, 9);
            this.checkBoxAll.Name = "checkBoxAll";
            this.checkBoxAll.Size = new System.Drawing.Size(48, 16);
            this.checkBoxAll.TabIndex = 30;
            this.checkBoxAll.Text = "全选";
            this.checkBoxAll.UseVisualStyleBackColor = false;
            this.checkBoxAll.CheckedChanged += new System.EventHandler(this.checkBoxAll_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(276, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(107, 23);
            this.button1.TabIndex = 31;
            this.button1.Text = "删除选中";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // mycheck
            // 
            this.mycheck.HeaderText = "";
            this.mycheck.IndeterminateValue = "false";
            this.mycheck.MinimumWidth = 60;
            this.mycheck.Name = "mycheck";
            this.mycheck.ReadOnly = true;
            this.mycheck.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.mycheck.Width = 60;
            // 
            // MYName
            // 
            this.MYName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.MYName.DataPropertyName = "HistogramEntName";
            this.MYName.HeaderText = "柱状图名称";
            this.MYName.MaxInputLength = 18;
            this.MYName.MinimumWidth = 70;
            this.MYName.Name = "MYName";
            this.MYName.ReadOnly = true;
            this.MYName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.MYName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // BID
            // 
            this.BID.DataPropertyName = "ID";
            this.BID.HeaderText = "id";
            this.BID.Name = "BID";
            this.BID.ReadOnly = true;
            this.BID.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.BID.Visible = false;
            this.BID.Width = 5;
            // 
            // blc
            // 
            this.blc.DataPropertyName = "BLC";
            this.blc.HeaderText = "比例尺";
            this.blc.Name = "blc";
            this.blc.ReadOnly = true;
            this.blc.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.blc.Visible = false;
            // 
            // btnDelete
            // 
            this.btnDelete.HeaderText = "";
            this.btnDelete.MinimumWidth = 60;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.ReadOnly = true;
            this.btnDelete.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseColumnTextForButtonValue = true;
            this.btnDelete.Width = 60;
            // 
            // btnopen
            // 
            this.btnopen.HeaderText = "";
            this.btnopen.MinimumWidth = 60;
            this.btnopen.Name = "btnopen";
            this.btnopen.ReadOnly = true;
            this.btnopen.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.btnopen.Text = "打开";
            this.btnopen.UseColumnTextForButtonValue = true;
            this.btnopen.Width = 60;
            // 
            // btnupdate
            // 
            this.btnupdate.HeaderText = "";
            this.btnupdate.MinimumWidth = 60;
            this.btnupdate.Name = "btnupdate";
            this.btnupdate.ReadOnly = true;
            this.btnupdate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.btnupdate.Text = "修改";
            this.btnupdate.UseColumnTextForButtonValue = true;
            this.btnupdate.Width = 60;
            // 
            // FrmManageZhzzt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 531);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.checkBoxAll);
            this.Controls.Add(this.dgrdvZhzzt);
            this.MinimumSize = new System.Drawing.Size(282, 100);
            this.Name = "FrmManageZhzzt";
            this.Text = "综合柱状图";
            this.Load += new System.EventHandler(this.FrmManageZhzzt_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgrdvZhzzt)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgrdvZhzzt;
        private System.Windows.Forms.CheckBox checkBoxAll;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn mycheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn MYName;
        private System.Windows.Forms.DataGridViewTextBoxColumn BID;
        private System.Windows.Forms.DataGridViewTextBoxColumn blc;
        private System.Windows.Forms.DataGridViewButtonColumn btnDelete;
        private System.Windows.Forms.DataGridViewButtonColumn btnopen;
        private System.Windows.Forms.DataGridViewButtonColumn btnupdate;
    }
}