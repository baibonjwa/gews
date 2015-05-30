namespace LibPanels
{
    partial class WorkTime
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WorkTime));
            this.gbxWorkTime = new System.Windows.Forms.GroupBox();
            this.rbtn46 = new System.Windows.Forms.RadioButton();
            this.rbtn38 = new System.Windows.Forms.RadioButton();
            this.btnSetAsDefault = new System.Windows.Forms.Button();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnCancle = new System.Windows.Forms.Button();
            this.dgrdvWorkTime = new System.Windows.Forms.DataGridView();
            this.workTimeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.workTimeFrom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.workTimeTo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnReset = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.gbxWorkTime.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgrdvWorkTime)).BeginInit();
            this.SuspendLayout();
            // 
            // gbxWorkTime
            // 
            this.gbxWorkTime.Controls.Add(this.rbtn46);
            this.gbxWorkTime.Controls.Add(this.rbtn38);
            this.gbxWorkTime.Location = new System.Drawing.Point(12, 12);
            this.gbxWorkTime.Name = "gbxWorkTime";
            this.gbxWorkTime.Size = new System.Drawing.Size(91, 83);
            this.gbxWorkTime.TabIndex = 0;
            this.gbxWorkTime.TabStop = false;
            this.gbxWorkTime.Text = "工作制式";
            // 
            // rbtn46
            // 
            this.rbtn46.AutoSize = true;
            this.rbtn46.Location = new System.Drawing.Point(16, 53);
            this.rbtn46.Name = "rbtn46";
            this.rbtn46.Size = new System.Drawing.Size(59, 16);
            this.rbtn46.TabIndex = 1;
            this.rbtn46.TabStop = true;
            this.rbtn46.Text = "四六制";
            this.rbtn46.UseVisualStyleBackColor = true;
            this.rbtn46.CheckedChanged += new System.EventHandler(this.rbtn46_CheckedChanged);
            // 
            // rbtn38
            // 
            this.rbtn38.AutoSize = true;
            this.rbtn38.Location = new System.Drawing.Point(16, 22);
            this.rbtn38.Name = "rbtn38";
            this.rbtn38.Size = new System.Drawing.Size(59, 16);
            this.rbtn38.TabIndex = 0;
            this.rbtn38.TabStop = true;
            this.rbtn38.Text = "三八制";
            this.rbtn38.UseVisualStyleBackColor = true;
            this.rbtn38.CheckedChanged += new System.EventHandler(this.rbtn38_CheckedChanged);
            // 
            // btnSetAsDefault
            // 
            this.btnSetAsDefault.Location = new System.Drawing.Point(12, 101);
            this.btnSetAsDefault.Name = "btnSetAsDefault";
            this.btnSetAsDefault.Size = new System.Drawing.Size(91, 23);
            this.btnSetAsDefault.TabIndex = 2;
            this.btnSetAsDefault.Text = "设置为默认";
            this.btnSetAsDefault.UseVisualStyleBackColor = true;
            this.btnSetAsDefault.Click += new System.EventHandler(this.btnSetAsDefault_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSubmit.Location = new System.Drawing.Point(279, 141);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 3;
            this.btnSubmit.Text = "提交";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnCancle
            // 
            this.btnCancle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancle.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancle.Location = new System.Drawing.Point(360, 141);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(75, 23);
            this.btnCancle.TabIndex = 4;
            this.btnCancle.Text = "取消";
            this.btnCancle.UseVisualStyleBackColor = true;
            this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // dgrdvWorkTime
            // 
            this.dgrdvWorkTime.AllowUserToAddRows = false;
            this.dgrdvWorkTime.AllowUserToResizeColumns = false;
            this.dgrdvWorkTime.AllowUserToResizeRows = false;
            this.dgrdvWorkTime.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgrdvWorkTime.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgrdvWorkTime.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgrdvWorkTime.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgrdvWorkTime.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgrdvWorkTime.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.workTimeName,
            this.workTimeFrom,
            this.workTimeTo});
            this.dgrdvWorkTime.Location = new System.Drawing.Point(109, 20);
            this.dgrdvWorkTime.Name = "dgrdvWorkTime";
            this.dgrdvWorkTime.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgrdvWorkTime.RowTemplate.Height = 23;
            this.dgrdvWorkTime.Size = new System.Drawing.Size(326, 112);
            this.dgrdvWorkTime.TabIndex = 1;
            this.dgrdvWorkTime.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgrdvWorkTime_CellEnter);
            this.dgrdvWorkTime.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgrdvWorkTime_CellLeave);
            // 
            // workTimeName
            // 
            this.workTimeName.HeaderText = "班次名";
            this.workTimeName.MaxInputLength = 15;
            this.workTimeName.Name = "workTimeName";
            this.workTimeName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.workTimeName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // workTimeFrom
            // 
            this.workTimeFrom.HeaderText = "开始时间";
            this.workTimeFrom.MaxInputLength = 10;
            this.workTimeFrom.Name = "workTimeFrom";
            this.workTimeFrom.ReadOnly = true;
            this.workTimeFrom.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.workTimeFrom.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // workTimeTo
            // 
            this.workTimeTo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.workTimeTo.HeaderText = "结束时间";
            this.workTimeTo.MaxInputLength = 10;
            this.workTimeTo.Name = "workTimeTo";
            this.workTimeTo.ReadOnly = true;
            this.workTimeTo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(185, 141);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(88, 23);
            this.btnReset.TabIndex = 2;
            this.btnReset.Text = "恢复初始设置";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.SystemColors.Control;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(196, 27);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(11, 12);
            this.label6.TabIndex = 17;
            this.label6.Text = "*";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(300, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 12);
            this.label1.TabIndex = 18;
            this.label1.Text = "*";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(396, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 12);
            this.label2.TabIndex = 19;
            this.label2.Text = "*";
            // 
            // WorkTime
            // 
            this.AcceptButton = this.btnSubmit;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancle;
            this.ClientSize = new System.Drawing.Size(444, 171);
            this.Controls.Add(this.btnSetAsDefault);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.dgrdvWorkTime);
            this.Controls.Add(this.btnCancle);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.gbxWorkTime);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WorkTime";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "班次";
            this.Load += new System.EventHandler(this.WorkTime_Load);
            this.gbxWorkTime.ResumeLayout(false);
            this.gbxWorkTime.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgrdvWorkTime)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxWorkTime;
        private System.Windows.Forms.RadioButton rbtn46;
        private System.Windows.Forms.RadioButton rbtn38;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnCancle;
        private System.Windows.Forms.DataGridView dgrdvWorkTime;
        private System.Windows.Forms.Button btnSetAsDefault;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.DataGridViewTextBoxColumn workTimeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn workTimeFrom;
        private System.Windows.Forms.DataGridViewTextBoxColumn workTimeTo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}