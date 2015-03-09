namespace LibPanels
{
    partial class ProbeInfoEntering
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cboProbeType = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtProbeDescription = new System.Windows.Forms.TextBox();
            this.lblProbeDescribe = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.lbl = new System.Windows.Forms.Label();
            this.lblProbeName = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtProbeId = new System.Windows.Forms.TextBox();
            this.lblProbeId = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.gbMove = new System.Windows.Forms.GroupBox();
            this.lblM = new System.Windows.Forms.Label();
            this.txtM = new System.Windows.Forms.TextBox();
            this.lblFarFromFrontal = new System.Windows.Forms.Label();
            this.rbtnNo = new System.Windows.Forms.RadioButton();
            this.rbtnYes = new System.Windows.Forms.RadioButton();
            this.cmbProbeName = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.selectTunnelSimple1 = new LibCommonForm.SelectTunnelSimple();
            this.groupBox1.SuspendLayout();
            this.gbMove.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(227, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 14);
            this.label2.TabIndex = 9;
            this.label2.Text = "*";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(227, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 14);
            this.label1.TabIndex = 6;
            this.label1.Text = "*";
            // 
            // cboProbeType
            // 
            this.cboProbeType.BackColor = System.Drawing.SystemColors.Window;
            this.cboProbeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboProbeType.FormattingEnabled = true;
            this.cboProbeType.Location = new System.Drawing.Point(85, 92);
            this.cboProbeType.Name = "cboProbeType";
            this.cboProbeType.Size = new System.Drawing.Size(140, 22);
            this.cboProbeType.TabIndex = 8;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.txtProbeDescription);
            this.groupBox1.Controls.Add(this.lblProbeDescribe);
            this.groupBox1.Location = new System.Drawing.Point(14, 239);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(678, 212);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "位置描述";
            // 
            // txtProbeDescription
            // 
            this.txtProbeDescription.Location = new System.Drawing.Point(85, 26);
            this.txtProbeDescription.MaxLength = 200;
            this.txtProbeDescription.Multiline = true;
            this.txtProbeDescription.Name = "txtProbeDescription";
            this.txtProbeDescription.Size = new System.Drawing.Size(387, 132);
            this.txtProbeDescription.TabIndex = 8;
            // 
            // lblProbeDescribe
            // 
            this.lblProbeDescribe.AutoSize = true;
            this.lblProbeDescribe.Location = new System.Drawing.Point(14, 29);
            this.lblProbeDescribe.Name = "lblProbeDescribe";
            this.lblProbeDescribe.Size = new System.Drawing.Size(67, 14);
            this.lblProbeDescribe.TabIndex = 7;
            this.lblProbeDescribe.Text = "探头描述：";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(603, 470);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(87, 27);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSubmit.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSubmit.Location = new System.Drawing.Point(510, 470);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(87, 27);
            this.btnSubmit.TabIndex = 13;
            this.btnSubmit.Text = "提交";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // lbl
            // 
            this.lbl.AutoSize = true;
            this.lbl.Location = new System.Drawing.Point(14, 96);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(67, 14);
            this.lbl.TabIndex = 7;
            this.lbl.Text = "探头类型：";
            // 
            // lblProbeName
            // 
            this.lblProbeName.AutoSize = true;
            this.lblProbeName.Location = new System.Drawing.Point(14, 58);
            this.lblProbeName.Name = "lblProbeName";
            this.lblProbeName.Size = new System.Drawing.Size(67, 14);
            this.lblProbeName.TabIndex = 4;
            this.lblProbeName.Text = "探头名称：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(703, 254);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 14);
            this.label3.TabIndex = 11;
            this.label3.Text = "*";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(227, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(14, 14);
            this.label4.TabIndex = 2;
            this.label4.Text = "*";
            // 
            // txtProbeId
            // 
            this.txtProbeId.Location = new System.Drawing.Point(85, 20);
            this.txtProbeId.MaxLength = 15;
            this.txtProbeId.Name = "txtProbeId";
            this.txtProbeId.Size = new System.Drawing.Size(140, 22);
            this.txtProbeId.TabIndex = 1;
            // 
            // lblProbeId
            // 
            this.lblProbeId.AutoSize = true;
            this.lblProbeId.Location = new System.Drawing.Point(14, 23);
            this.lblProbeId.Name = "lblProbeId";
            this.lblProbeId.Size = new System.Drawing.Size(67, 14);
            this.lblProbeId.TabIndex = 0;
            this.lblProbeId.Text = "探头编号：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(247, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(223, 14);
            this.label5.TabIndex = 3;
            this.label5.Text = "探头编号录入后不可更改，请谨慎录入！";
            // 
            // gbMove
            // 
            this.gbMove.Controls.Add(this.lblM);
            this.gbMove.Controls.Add(this.txtM);
            this.gbMove.Controls.Add(this.lblFarFromFrontal);
            this.gbMove.Controls.Add(this.rbtnNo);
            this.gbMove.Controls.Add(this.rbtnYes);
            this.gbMove.Location = new System.Drawing.Point(14, 122);
            this.gbMove.Name = "gbMove";
            this.gbMove.Size = new System.Drawing.Size(357, 54);
            this.gbMove.TabIndex = 10;
            this.gbMove.TabStop = false;
            this.gbMove.Text = "是否自动位移";
            // 
            // lblM
            // 
            this.lblM.AutoSize = true;
            this.lblM.Location = new System.Drawing.Point(325, 23);
            this.lblM.Name = "lblM";
            this.lblM.Size = new System.Drawing.Size(16, 14);
            this.lblM.TabIndex = 4;
            this.lblM.Text = "M";
            // 
            // txtM
            // 
            this.txtM.Enabled = false;
            this.txtM.Location = new System.Drawing.Point(202, 20);
            this.txtM.Name = "txtM";
            this.txtM.Size = new System.Drawing.Size(116, 22);
            this.txtM.TabIndex = 3;
            // 
            // lblFarFromFrontal
            // 
            this.lblFarFromFrontal.AutoSize = true;
            this.lblFarFromFrontal.Location = new System.Drawing.Point(112, 23);
            this.lblFarFromFrontal.Name = "lblFarFromFrontal";
            this.lblFarFromFrontal.Size = new System.Drawing.Size(79, 14);
            this.lblFarFromFrontal.TabIndex = 2;
            this.lblFarFromFrontal.Text = "距迎头距离：";
            // 
            // rbtnNo
            // 
            this.rbtnNo.AutoSize = true;
            this.rbtnNo.Checked = true;
            this.rbtnNo.Location = new System.Drawing.Point(64, 21);
            this.rbtnNo.Name = "rbtnNo";
            this.rbtnNo.Size = new System.Drawing.Size(37, 18);
            this.rbtnNo.TabIndex = 1;
            this.rbtnNo.TabStop = true;
            this.rbtnNo.Text = "否";
            this.rbtnNo.UseVisualStyleBackColor = true;
            this.rbtnNo.Click += new System.EventHandler(this.rbtnNo_Click);
            // 
            // rbtnYes
            // 
            this.rbtnYes.AutoSize = true;
            this.rbtnYes.Location = new System.Drawing.Point(16, 21);
            this.rbtnYes.Name = "rbtnYes";
            this.rbtnYes.Size = new System.Drawing.Size(37, 18);
            this.rbtnYes.TabIndex = 0;
            this.rbtnYes.TabStop = true;
            this.rbtnYes.Text = "是";
            this.rbtnYes.UseVisualStyleBackColor = true;
            this.rbtnYes.Click += new System.EventHandler(this.rbtnYes_Click);
            // 
            // cmbProbeName
            // 
            this.cmbProbeName.FormattingEnabled = true;
            this.cmbProbeName.Items.AddRange(new object[] {
            "T0",
            "T1",
            "T2",
            "T3",
            "T4",
            "T5",
            "T6",
            "T7",
            "T8"});
            this.cmbProbeName.Location = new System.Drawing.Point(85, 55);
            this.cmbProbeName.Name = "cmbProbeName";
            this.cmbProbeName.Size = new System.Drawing.Size(140, 22);
            this.cmbProbeName.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 199);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 14);
            this.label6.TabIndex = 17;
            this.label6.Text = "所属巷道：";
            // 
            // selectTunnelSimple1
            // 
            this.selectTunnelSimple1.Location = new System.Drawing.Point(78, 188);
            this.selectTunnelSimple1.Name = "selectTunnelSimple1";
            this.selectTunnelSimple1.Size = new System.Drawing.Size(219, 38);
            this.selectTunnelSimple1.TabIndex = 16;
            this.selectTunnelSimple1.Load += new System.EventHandler(this.selectTunnelSimple1_Load);
            // 
            // ProbeInfoEntering
            // 
            this.AcceptButton = this.btnSubmit;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(728, 507);
            this.Controls.Add(this.selectTunnelSimple1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmbProbeName);
            this.Controls.Add(this.gbMove);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtProbeId);
            this.Controls.Add(this.lblProbeId);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboProbeType);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.lbl);
            this.Controls.Add(this.lblProbeName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProbeInfoEntering";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "探头数据录入";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbMove.ResumeLayout(false);
            this.gbMove.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboProbeType;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtProbeDescription;
        private System.Windows.Forms.Label lblProbeDescribe;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Label lbl;
        private System.Windows.Forms.Label lblProbeName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtProbeId;
        private System.Windows.Forms.Label lblProbeId;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox gbMove;
        private System.Windows.Forms.RadioButton rbtnYes;
        private System.Windows.Forms.RadioButton rbtnNo;
        private System.Windows.Forms.Label lblFarFromFrontal;
        private System.Windows.Forms.TextBox txtM;
        private System.Windows.Forms.Label lblM;
        private System.Windows.Forms.ComboBox cmbProbeName;
        private LibCommonForm.SelectTunnelSimple selectTunnelSimple1;
        private System.Windows.Forms.Label label6;


    }
}