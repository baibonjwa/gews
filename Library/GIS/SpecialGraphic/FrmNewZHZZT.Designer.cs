namespace GIS.SpecialGraphic
{
    partial class FrmNewZHZZT
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.dgrdvZhzzt = new System.Windows.Forms.DataGridView();
            this.MYName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.houdu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DropDownZZ = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.miaoshu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnDelete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.txtname = new System.Windows.Forms.TextBox();
            this.lblCollapsePillarsName = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBlc = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.labelSC = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.插入ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.复制ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.剪切ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.粘贴ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.上移ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.下移ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgrdvZhzzt)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(112, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 12);
            this.label1.TabIndex = 34;
            this.label1.Text = "*";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(172, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 12);
            this.label2.TabIndex = 35;
            this.label2.Text = "*";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(224, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(11, 12);
            this.label3.TabIndex = 36;
            this.label3.Text = "*";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(330, 11);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(11, 12);
            this.label6.TabIndex = 33;
            this.label6.Text = "*";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(673, 371);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 32;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // dgrdvZhzzt
            // 
            this.dgrdvZhzzt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgrdvZhzzt.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrdvZhzzt.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MYName,
            this.houdu,
            this.DropDownZZ,
            this.miaoshu,
            this.btnDelete});
            this.dgrdvZhzzt.ContextMenuStrip = this.contextMenuStrip1;
            this.dgrdvZhzzt.Location = new System.Drawing.Point(12, 48);
            this.dgrdvZhzzt.MultiSelect = false;
            this.dgrdvZhzzt.Name = "dgrdvZhzzt";
            this.dgrdvZhzzt.RowTemplate.Height = 23;
            this.dgrdvZhzzt.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgrdvZhzzt.Size = new System.Drawing.Size(736, 318);
            this.dgrdvZhzzt.TabIndex = 28;
            this.dgrdvZhzzt.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgrdvZhzzt_CellContentClick);
            this.dgrdvZhzzt.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgrdvZhzzt_CellEndEdit);
            this.dgrdvZhzzt.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgrdvZhzzt_CellMouseDown);
            this.dgrdvZhzzt.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgrdvZhzzt_RowPostPaint);
            this.dgrdvZhzzt.RowValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgrdvZhzzt_RowValidated);
            // 
            // MYName
            // 
            this.MYName.HeaderText = "煤岩名称";
            this.MYName.MaxInputLength = 18;
            this.MYName.Name = "MYName";
            this.MYName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.MYName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.MYName.Width = 85;
            // 
            // houdu
            // 
            this.houdu.HeaderText = "厚度";
            this.houdu.MaxInputLength = 18;
            this.houdu.Name = "houdu";
            this.houdu.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.houdu.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.houdu.Width = 50;
            // 
            // DropDownZZ
            // 
            this.DropDownZZ.FillWeight = 162.963F;
            this.DropDownZZ.HeaderText = "柱状";
            this.DropDownZZ.Name = "DropDownZZ";
            // 
            // miaoshu
            // 
            this.miaoshu.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.miaoshu.HeaderText = "岩性描述";
            this.miaoshu.Name = "miaoshu";
            // 
            // btnDelete
            // 
            this.btnDelete.HeaderText = "";
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.ReadOnly = true;
            this.btnDelete.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseColumnTextForButtonValue = true;
            this.btnDelete.Width = 60;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSubmit.Location = new System.Drawing.Point(579, 371);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 31;
            this.btnSubmit.Text = "提交";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // txtname
            // 
            this.txtname.Location = new System.Drawing.Point(79, 8);
            this.txtname.Name = "txtname";
            this.txtname.Size = new System.Drawing.Size(245, 21);
            this.txtname.TabIndex = 26;
            // 
            // lblCollapsePillarsName
            // 
            this.lblCollapsePillarsName.AutoSize = true;
            this.lblCollapsePillarsName.Location = new System.Drawing.Point(8, 11);
            this.lblCollapsePillarsName.Name = "lblCollapsePillarsName";
            this.lblCollapsePillarsName.Size = new System.Drawing.Size(77, 12);
            this.lblCollapsePillarsName.TabIndex = 25;
            this.lblCollapsePillarsName.Text = "柱状图名称：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(344, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 37;
            this.label4.Text = "比例尺：";
            // 
            // txtBlc
            // 
            this.txtBlc.Location = new System.Drawing.Point(403, 8);
            this.txtBlc.Name = "txtBlc";
            this.txtBlc.Size = new System.Drawing.Size(100, 21);
            this.txtBlc.TabIndex = 38;
            this.txtBlc.Text = "50";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(567, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 39;
            this.label5.Text = "单位：米";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(509, 12);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(11, 12);
            this.label7.TabIndex = 40;
            this.label7.Text = "*";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(174, 371);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(384, 23);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 41;
            // 
            // labelSC
            // 
            this.labelSC.AutoSize = true;
            this.labelSC.Location = new System.Drawing.Point(13, 376);
            this.labelSC.Name = "labelSC";
            this.labelSC.Size = new System.Drawing.Size(131, 12);
            this.labelSC.TabIndex = 42;
            this.labelSC.Text = "图形生成中，请稍后...";
            this.labelSC.Visible = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.插入ToolStripMenuItem,
            this.toolStripSeparator1,
            this.复制ToolStripMenuItem,
            this.剪切ToolStripMenuItem,
            this.粘贴ToolStripMenuItem,
            this.toolStripSeparator2,
            this.上移ToolStripMenuItem,
            this.下移ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 148);
            // 
            // 插入ToolStripMenuItem
            // 
            this.插入ToolStripMenuItem.Name = "插入ToolStripMenuItem";
            this.插入ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.插入ToolStripMenuItem.Text = "插入";
            this.插入ToolStripMenuItem.Click += new System.EventHandler(this.插入ToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(97, 6);
            // 
            // 复制ToolStripMenuItem
            // 
            this.复制ToolStripMenuItem.Name = "复制ToolStripMenuItem";
            this.复制ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.复制ToolStripMenuItem.Text = "复制";
            this.复制ToolStripMenuItem.Click += new System.EventHandler(this.复制ToolStripMenuItem_Click);
            // 
            // 剪切ToolStripMenuItem
            // 
            this.剪切ToolStripMenuItem.Name = "剪切ToolStripMenuItem";
            this.剪切ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.剪切ToolStripMenuItem.Text = "剪切";
            this.剪切ToolStripMenuItem.Click += new System.EventHandler(this.剪切ToolStripMenuItem_Click);
            // 
            // 粘贴ToolStripMenuItem
            // 
            this.粘贴ToolStripMenuItem.Name = "粘贴ToolStripMenuItem";
            this.粘贴ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.粘贴ToolStripMenuItem.Text = "粘贴";
            this.粘贴ToolStripMenuItem.Visible = false;
            this.粘贴ToolStripMenuItem.Click += new System.EventHandler(this.粘贴ToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(97, 6);
            // 
            // 上移ToolStripMenuItem
            // 
            this.上移ToolStripMenuItem.Name = "上移ToolStripMenuItem";
            this.上移ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.上移ToolStripMenuItem.Text = "上移";
            this.上移ToolStripMenuItem.Click += new System.EventHandler(this.上移ToolStripMenuItem_Click);
            // 
            // 下移ToolStripMenuItem
            // 
            this.下移ToolStripMenuItem.Name = "下移ToolStripMenuItem";
            this.下移ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.下移ToolStripMenuItem.Text = "下移";
            this.下移ToolStripMenuItem.Click += new System.EventHandler(this.下移ToolStripMenuItem_Click);
            // 
            // FrmNewZHZZT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(752, 397);
            this.Controls.Add(this.labelSC);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtBlc);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.dgrdvZhzzt);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.txtname);
            this.Controls.Add(this.lblCollapsePillarsName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmNewZHZZT";
            this.Text = "新建综合柱状图";
            ((System.ComponentModel.ISupportInitialize)(this.dgrdvZhzzt)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DataGridView dgrdvZhzzt;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.TextBox txtname;
        private System.Windows.Forms.Label lblCollapsePillarsName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBlc;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label labelSC;
        private System.Windows.Forms.DataGridViewTextBoxColumn MYName;
        private System.Windows.Forms.DataGridViewTextBoxColumn houdu;
        private System.Windows.Forms.DataGridViewComboBoxColumn DropDownZZ;
        private System.Windows.Forms.DataGridViewTextBoxColumn miaoshu;
        private System.Windows.Forms.DataGridViewButtonColumn btnDelete;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 插入ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem 复制ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 剪切ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 粘贴ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem 上移ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 下移ToolStripMenuItem;
    }
}