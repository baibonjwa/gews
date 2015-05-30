namespace sys3
{
    partial class TunnelJjEntering
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
            this.lblTeamName = new System.Windows.Forms.Label();
            this.cboTeamName = new System.Windows.Forms.ComboBox();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.gbxIsFinish = new System.Windows.Forms.GroupBox();
            this.dtpStopDate = new System.Windows.Forms.DateTimePicker();
            this.lblStopDate = new System.Windows.Forms.Label();
            this.rbtnJJN = new System.Windows.Forms.RadioButton();
            this.rbtnJJY = new System.Windows.Forms.RadioButton();
            this.lblWorkTime = new System.Windows.Forms.Label();
            this.cboWorkTime = new System.Windows.Forms.ComboBox();
            this.gbxWorkStyle = new System.Windows.Forms.GroupBox();
            this.rbtn46 = new System.Windows.Forms.RadioButton();
            this.rbtn38 = new System.Windows.Forms.RadioButton();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblTunnelChoose1 = new System.Windows.Forms.Label();
            this.btnChooseWorkFace = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnChooseTunnel = new System.Windows.Forms.Button();
            this.gbxIsFinish.SuspendLayout();
            this.gbxWorkStyle.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTeamName
            // 
            this.lblTeamName.AutoSize = true;
            this.lblTeamName.Location = new System.Drawing.Point(18, 103);
            this.lblTeamName.Name = "lblTeamName";
            this.lblTeamName.Size = new System.Drawing.Size(65, 12);
            this.lblTeamName.TabIndex = 2;
            this.lblTeamName.Text = "队    别：";
            // 
            // cboTeamName
            // 
            this.cboTeamName.FormattingEnabled = true;
            this.cboTeamName.Location = new System.Drawing.Point(89, 100);
            this.cboTeamName.Name = "cboTeamName";
            this.cboTeamName.Size = new System.Drawing.Size(208, 20);
            this.cboTeamName.TabIndex = 3;
            // 
            // lblStartDate
            // 
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Location = new System.Drawing.Point(17, 145);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(65, 12);
            this.lblStartDate.TabIndex = 4;
            this.lblStartDate.Text = "开工日期：";
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Location = new System.Drawing.Point(89, 139);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(208, 21);
            this.dtpStartDate.TabIndex = 5;
            // 
            // gbxIsFinish
            // 
            this.gbxIsFinish.Controls.Add(this.dtpStopDate);
            this.gbxIsFinish.Controls.Add(this.lblStopDate);
            this.gbxIsFinish.Controls.Add(this.rbtnJJN);
            this.gbxIsFinish.Controls.Add(this.rbtnJJY);
            this.gbxIsFinish.Location = new System.Drawing.Point(14, 176);
            this.gbxIsFinish.Name = "gbxIsFinish";
            this.gbxIsFinish.Size = new System.Drawing.Size(283, 48);
            this.gbxIsFinish.TabIndex = 6;
            this.gbxIsFinish.TabStop = false;
            this.gbxIsFinish.Text = "是否掘进完毕";
            // 
            // dtpStopDate
            // 
            this.dtpStopDate.Location = new System.Drawing.Point(154, 20);
            this.dtpStopDate.Name = "dtpStopDate";
            this.dtpStopDate.Size = new System.Drawing.Size(123, 21);
            this.dtpStopDate.TabIndex = 3;
            // 
            // lblStopDate
            // 
            this.lblStopDate.AutoSize = true;
            this.lblStopDate.Location = new System.Drawing.Point(91, 25);
            this.lblStopDate.Name = "lblStopDate";
            this.lblStopDate.Size = new System.Drawing.Size(65, 12);
            this.lblStopDate.TabIndex = 2;
            this.lblStopDate.Text = "停工日期：";
            // 
            // rbtnJJN
            // 
            this.rbtnJJN.AutoSize = true;
            this.rbtnJJN.Location = new System.Drawing.Point(47, 23);
            this.rbtnJJN.Name = "rbtnJJN";
            this.rbtnJJN.Size = new System.Drawing.Size(35, 16);
            this.rbtnJJN.TabIndex = 1;
            this.rbtnJJN.TabStop = true;
            this.rbtnJJN.Text = "否";
            this.rbtnJJN.UseVisualStyleBackColor = true;
            // 
            // rbtnJJY
            // 
            this.rbtnJJY.AutoSize = true;
            this.rbtnJJY.Location = new System.Drawing.Point(6, 23);
            this.rbtnJJY.Name = "rbtnJJY";
            this.rbtnJJY.Size = new System.Drawing.Size(35, 16);
            this.rbtnJJY.TabIndex = 0;
            this.rbtnJJY.TabStop = true;
            this.rbtnJJY.Text = "是";
            this.rbtnJJY.UseVisualStyleBackColor = true;
            this.rbtnJJY.CheckedChanged += new System.EventHandler(this.rbtnJJY_CheckedChanged);
            // 
            // lblWorkTime
            // 
            this.lblWorkTime.AutoSize = true;
            this.lblWorkTime.Location = new System.Drawing.Point(152, 20);
            this.lblWorkTime.Name = "lblWorkTime";
            this.lblWorkTime.Size = new System.Drawing.Size(41, 12);
            this.lblWorkTime.TabIndex = 8;
            this.lblWorkTime.Text = "班次：";
            // 
            // cboWorkTime
            // 
            this.cboWorkTime.FormattingEnabled = true;
            this.cboWorkTime.Location = new System.Drawing.Point(190, 17);
            this.cboWorkTime.Name = "cboWorkTime";
            this.cboWorkTime.Size = new System.Drawing.Size(87, 20);
            this.cboWorkTime.TabIndex = 9;
            // 
            // gbxWorkStyle
            // 
            this.gbxWorkStyle.Controls.Add(this.rbtn46);
            this.gbxWorkStyle.Controls.Add(this.rbtn38);
            this.gbxWorkStyle.Controls.Add(this.cboWorkTime);
            this.gbxWorkStyle.Controls.Add(this.lblWorkTime);
            this.gbxWorkStyle.Location = new System.Drawing.Point(14, 235);
            this.gbxWorkStyle.Name = "gbxWorkStyle";
            this.gbxWorkStyle.Size = new System.Drawing.Size(283, 45);
            this.gbxWorkStyle.TabIndex = 7;
            this.gbxWorkStyle.TabStop = false;
            this.gbxWorkStyle.Text = "工作制式";
            // 
            // rbtn46
            // 
            this.rbtn46.AutoSize = true;
            this.rbtn46.Location = new System.Drawing.Point(80, 18);
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
            this.rbtn38.Location = new System.Drawing.Point(9, 18);
            this.rbtn38.Name = "rbtn38";
            this.rbtn38.Size = new System.Drawing.Size(59, 16);
            this.rbtn38.TabIndex = 0;
            this.rbtn38.TabStop = true;
            this.rbtn38.Text = "三八制";
            this.rbtn38.UseVisualStyleBackColor = true;
            this.rbtn38.CheckedChanged += new System.EventHandler(this.rbtn38_CheckedChanged);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSubmit.Location = new System.Drawing.Point(145, 294);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 10;
            this.btnSubmit.Text = "提交";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(226, 294);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblTunnelChoose1
            // 
            this.lblTunnelChoose1.AutoSize = true;
            this.lblTunnelChoose1.Location = new System.Drawing.Point(10, 25);
            this.lblTunnelChoose1.Name = "lblTunnelChoose1";
            this.lblTunnelChoose1.Size = new System.Drawing.Size(77, 12);
            this.lblTunnelChoose1.TabIndex = 0;
            this.lblTunnelChoose1.Text = "选择掘进面：";
            // 
            // btnChooseWorkFace
            // 
            this.btnChooseWorkFace.Location = new System.Drawing.Point(89, 12);
            this.btnChooseWorkFace.Name = "btnChooseWorkFace";
            this.btnChooseWorkFace.Size = new System.Drawing.Size(208, 38);
            this.btnChooseWorkFace.TabIndex = 1;
            this.btnChooseWorkFace.Text = "点此选择";
            this.btnChooseWorkFace.UseVisualStyleBackColor = true;
            this.btnChooseWorkFace.Click += new System.EventHandler(this.btnChooseWorkFace_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(298, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(11, 12);
            this.label6.TabIndex = 12;
            this.label6.Text = "*";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "选择巷道：";
            // 
            // btnChooseTunnel
            // 
            this.btnChooseTunnel.Location = new System.Drawing.Point(89, 54);
            this.btnChooseTunnel.Name = "btnChooseTunnel";
            this.btnChooseTunnel.Size = new System.Drawing.Size(208, 38);
            this.btnChooseTunnel.TabIndex = 1;
            this.btnChooseTunnel.Text = "点此选择";
            this.btnChooseTunnel.UseVisualStyleBackColor = true;
            this.btnChooseTunnel.Click += new System.EventHandler(this.btnChooseTunnel_Click);
            // 
            // TunnelJJEntering
            // 
            this.AcceptButton = this.btnSubmit;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(313, 329);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnChooseTunnel);
            this.Controls.Add(this.btnChooseWorkFace);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblTunnelChoose1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.gbxWorkStyle);
            this.Controls.Add(this.gbxIsFinish);
            this.Controls.Add(this.dtpStartDate);
            this.Controls.Add(this.lblStartDate);
            this.Controls.Add(this.cboTeamName);
            this.Controls.Add(this.lblTeamName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TunnelJjEntering";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "设置掘进面";
            this.Load += new System.EventHandler(this.TunnelJJHC_Load);
            this.gbxIsFinish.ResumeLayout(false);
            this.gbxIsFinish.PerformLayout();
            this.gbxWorkStyle.ResumeLayout(false);
            this.gbxWorkStyle.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTeamName;
        private System.Windows.Forms.ComboBox cboTeamName;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.GroupBox gbxIsFinish;
        private System.Windows.Forms.DateTimePicker dtpStopDate;
        private System.Windows.Forms.Label lblStopDate;
        private System.Windows.Forms.RadioButton rbtnJJN;
        private System.Windows.Forms.RadioButton rbtnJJY;
        private System.Windows.Forms.Label lblWorkTime;
        private System.Windows.Forms.ComboBox cboWorkTime;
        private System.Windows.Forms.GroupBox gbxWorkStyle;
        private System.Windows.Forms.RadioButton rbtn46;
        private System.Windows.Forms.RadioButton rbtn38;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblTunnelChoose1;
        private System.Windows.Forms.Button btnChooseWorkFace;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnChooseTunnel;
    }
}