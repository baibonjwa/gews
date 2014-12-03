namespace _5.WarningManagement
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
            this.lblProbeLocation = new System.Windows.Forms.Label();
            this.lblProbeLocationX = new System.Windows.Forms.Label();
            this.lblProbeLocationY = new System.Windows.Forms.Label();
            this.lblProbeLocationZ = new System.Windows.Forms.Label();
            this.txtProbeLocationX = new System.Windows.Forms.TextBox();
            this.txtProbeLocationY = new System.Windows.Forms.TextBox();
            this.txtProbeDescription = new System.Windows.Forms.TextBox();
            this.txtProbeLocationZ = new System.Windows.Forms.TextBox();
            this.lblProbeDescribe = new System.Windows.Forms.Label();
            this.gbTunnel = new System.Windows.Forms.GroupBox();
            this.selectTunnelUserControl1 = new LibCommonForm.SelectTunnelUserControl();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.txtProbeName = new System.Windows.Forms.TextBox();
            this.lbl = new System.Windows.Forms.Label();
            this.lblProbeName = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtProbeId = new System.Windows.Forms.TextBox();
            this.lblProbeId = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.gbTunnel.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(195, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "*";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(195, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "*";
            // 
            // cboProbeType
            // 
            this.cboProbeType.BackColor = System.Drawing.SystemColors.Window;
            this.cboProbeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboProbeType.FormattingEnabled = true;
            this.cboProbeType.Location = new System.Drawing.Point(73, 79);
            this.cboProbeType.Name = "cboProbeType";
            this.cboProbeType.Size = new System.Drawing.Size(121, 20);
            this.cboProbeType.TabIndex = 8;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblProbeLocation);
            this.groupBox1.Controls.Add(this.lblProbeLocationX);
            this.groupBox1.Controls.Add(this.lblProbeLocationY);
            this.groupBox1.Controls.Add(this.lblProbeLocationZ);
            this.groupBox1.Controls.Add(this.txtProbeLocationX);
            this.groupBox1.Controls.Add(this.txtProbeLocationY);
            this.groupBox1.Controls.Add(this.txtProbeDescription);
            this.groupBox1.Controls.Add(this.txtProbeLocationZ);
            this.groupBox1.Controls.Add(this.lblProbeDescribe);
            this.groupBox1.Location = new System.Drawing.Point(14, 337);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(581, 182);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "位置描述";
            // 
            // lblProbeLocation
            // 
            this.lblProbeLocation.AutoSize = true;
            this.lblProbeLocation.Location = new System.Drawing.Point(12, 23);
            this.lblProbeLocation.Name = "lblProbeLocation";
            this.lblProbeLocation.Size = new System.Drawing.Size(65, 12);
            this.lblProbeLocation.TabIndex = 0;
            this.lblProbeLocation.Text = "探头位置：";
            // 
            // lblProbeLocationX
            // 
            this.lblProbeLocationX.AutoSize = true;
            this.lblProbeLocationX.Location = new System.Drawing.Point(74, 23);
            this.lblProbeLocationX.Name = "lblProbeLocationX";
            this.lblProbeLocationX.Size = new System.Drawing.Size(35, 12);
            this.lblProbeLocationX.TabIndex = 1;
            this.lblProbeLocationX.Text = "坐标X";
            // 
            // lblProbeLocationY
            // 
            this.lblProbeLocationY.AutoSize = true;
            this.lblProbeLocationY.Location = new System.Drawing.Point(183, 23);
            this.lblProbeLocationY.Name = "lblProbeLocationY";
            this.lblProbeLocationY.Size = new System.Drawing.Size(35, 12);
            this.lblProbeLocationY.TabIndex = 3;
            this.lblProbeLocationY.Text = "坐标Y";
            // 
            // lblProbeLocationZ
            // 
            this.lblProbeLocationZ.AutoSize = true;
            this.lblProbeLocationZ.Location = new System.Drawing.Point(298, 23);
            this.lblProbeLocationZ.Name = "lblProbeLocationZ";
            this.lblProbeLocationZ.Size = new System.Drawing.Size(35, 12);
            this.lblProbeLocationZ.TabIndex = 5;
            this.lblProbeLocationZ.Text = "坐标Z";
            // 
            // txtProbeLocationX
            // 
            this.txtProbeLocationX.Location = new System.Drawing.Point(110, 20);
            this.txtProbeLocationX.MaxLength = 8;
            this.txtProbeLocationX.Name = "txtProbeLocationX";
            this.txtProbeLocationX.Size = new System.Drawing.Size(55, 21);
            this.txtProbeLocationX.TabIndex = 2;
            // 
            // txtProbeLocationY
            // 
            this.txtProbeLocationY.Location = new System.Drawing.Point(218, 20);
            this.txtProbeLocationY.MaxLength = 8;
            this.txtProbeLocationY.Name = "txtProbeLocationY";
            this.txtProbeLocationY.Size = new System.Drawing.Size(55, 21);
            this.txtProbeLocationY.TabIndex = 4;
            // 
            // txtProbeDescription
            // 
            this.txtProbeDescription.Location = new System.Drawing.Point(73, 53);
            this.txtProbeDescription.MaxLength = 200;
            this.txtProbeDescription.Multiline = true;
            this.txtProbeDescription.Name = "txtProbeDescription";
            this.txtProbeDescription.Size = new System.Drawing.Size(332, 114);
            this.txtProbeDescription.TabIndex = 8;
            // 
            // txtProbeLocationZ
            // 
            this.txtProbeLocationZ.Location = new System.Drawing.Point(333, 20);
            this.txtProbeLocationZ.MaxLength = 8;
            this.txtProbeLocationZ.Name = "txtProbeLocationZ";
            this.txtProbeLocationZ.Size = new System.Drawing.Size(55, 21);
            this.txtProbeLocationZ.TabIndex = 6;
            // 
            // lblProbeDescribe
            // 
            this.lblProbeDescribe.AutoSize = true;
            this.lblProbeDescribe.Location = new System.Drawing.Point(12, 56);
            this.lblProbeDescribe.Name = "lblProbeDescribe";
            this.lblProbeDescribe.Size = new System.Drawing.Size(65, 12);
            this.lblProbeDescribe.TabIndex = 7;
            this.lblProbeDescribe.Text = "探头描述：";
            // 
            // gbTunnel
            // 
            this.gbTunnel.Controls.Add(this.selectTunnelUserControl1);
            this.gbTunnel.Location = new System.Drawing.Point(14, 117);
            this.gbTunnel.Name = "gbTunnel";
            this.gbTunnel.Size = new System.Drawing.Size(583, 204);
            this.gbTunnel.TabIndex = 10;
            this.gbTunnel.TabStop = false;
            this.gbTunnel.Text = "所在巷道";
            // 
            // selectTunnelUserControl1
            // 
            this.selectTunnelUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selectTunnelUserControl1.ITunnelId = 0;
            this.selectTunnelUserControl1.Location = new System.Drawing.Point(3, 17);
            this.selectTunnelUserControl1.Name = "selectTunnelUserControl1";
            this.selectTunnelUserControl1.Size = new System.Drawing.Size(577, 184);
            this.selectTunnelUserControl1.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(519, 550);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSubmit.Location = new System.Drawing.Point(439, 550);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 13;
            this.btnSubmit.Text = "提交";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // txtProbeName
            // 
            this.txtProbeName.Location = new System.Drawing.Point(73, 47);
            this.txtProbeName.MaxLength = 15;
            this.txtProbeName.Name = "txtProbeName";
            this.txtProbeName.Size = new System.Drawing.Size(121, 21);
            this.txtProbeName.TabIndex = 5;
            // 
            // lbl
            // 
            this.lbl.AutoSize = true;
            this.lbl.Location = new System.Drawing.Point(12, 82);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(65, 12);
            this.lbl.TabIndex = 7;
            this.lbl.Text = "探头类型：";
            // 
            // lblProbeName
            // 
            this.lblProbeName.AutoSize = true;
            this.lblProbeName.Location = new System.Drawing.Point(12, 50);
            this.lblProbeName.Name = "lblProbeName";
            this.lblProbeName.Size = new System.Drawing.Size(65, 12);
            this.lblProbeName.TabIndex = 4;
            this.lblProbeName.Text = "探头名称：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(603, 208);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(11, 12);
            this.label3.TabIndex = 11;
            this.label3.Text = "*";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(195, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(11, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "*";
            // 
            // txtProbeId
            // 
            this.txtProbeId.Location = new System.Drawing.Point(73, 17);
            this.txtProbeId.MaxLength = 15;
            this.txtProbeId.Name = "txtProbeId";
            this.txtProbeId.Size = new System.Drawing.Size(121, 21);
            this.txtProbeId.TabIndex = 1;
            // 
            // lblProbeId
            // 
            this.lblProbeId.AutoSize = true;
            this.lblProbeId.Location = new System.Drawing.Point(12, 20);
            this.lblProbeId.Name = "lblProbeId";
            this.lblProbeId.Size = new System.Drawing.Size(65, 12);
            this.lblProbeId.TabIndex = 0;
            this.lblProbeId.Text = "探头编号：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(212, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(221, 12);
            this.label5.TabIndex = 3;
            this.label5.Text = "探头编号录入后不可更改，请谨慎录入！";
            // 
            // ProbeInfoEntering
            // 
            this.AcceptButton = this.btnSubmit;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(624, 589);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtProbeId);
            this.Controls.Add(this.lblProbeId);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboProbeType);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbTunnel);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.txtProbeName);
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
            this.gbTunnel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboProbeType;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblProbeLocation;
        private System.Windows.Forms.Label lblProbeLocationX;
        private System.Windows.Forms.Label lblProbeLocationY;
        private System.Windows.Forms.Label lblProbeLocationZ;
        private System.Windows.Forms.TextBox txtProbeLocationX;
        private System.Windows.Forms.TextBox txtProbeLocationY;
        private System.Windows.Forms.TextBox txtProbeDescription;
        private System.Windows.Forms.TextBox txtProbeLocationZ;
        private System.Windows.Forms.Label lblProbeDescribe;
        private System.Windows.Forms.GroupBox gbTunnel;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.TextBox txtProbeName;
        private System.Windows.Forms.Label lbl;
        private System.Windows.Forms.Label lblProbeName;
        private LibCommonForm.SelectTunnelUserControl selectTunnelUserControl1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtProbeId;
        private System.Windows.Forms.Label lblProbeId;
        private System.Windows.Forms.Label label5;


    }
}