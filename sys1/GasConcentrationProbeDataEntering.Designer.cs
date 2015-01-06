namespace sys1
{
    partial class GasConcentrationProbeDataEntering
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GasConcentrationProbeDataEntering));
            this.txtProbeValue = new System.Windows.Forms.TextBox();
            this.lblProbeValue = new System.Windows.Forms.Label();
            this.lblRecordTime = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.dtpRecordTime = new System.Windows.Forms.DateTimePicker();
            this.gbTunnel = new System.Windows.Forms.GroupBox();
            this.selectTunnelUserControl1 = new LibCommonForm.SelectTunnelUserControl();
            this._gbProbeSel = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this._lblProbeName = new System.Windows.Forms.Label();
            this._lblProbeStyle = new System.Windows.Forms.Label();
            this._lstProbeName = new System.Windows.Forms.ListBox();
            this._lstProbeStyle = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.gbTunnel.SuspendLayout();
            this._gbProbeSel.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtProbeValue
            // 
            this.txtProbeValue.Location = new System.Drawing.Point(84, 226);
            this.txtProbeValue.MaxLength = 15;
            this.txtProbeValue.Name = "txtProbeValue";
            this.txtProbeValue.Size = new System.Drawing.Size(89, 21);
            this.txtProbeValue.TabIndex = 1;
            // 
            // lblProbeValue
            // 
            this.lblProbeValue.AutoSize = true;
            this.lblProbeValue.Location = new System.Drawing.Point(13, 229);
            this.lblProbeValue.Name = "lblProbeValue";
            this.lblProbeValue.Size = new System.Drawing.Size(77, 12);
            this.lblProbeValue.TabIndex = 0;
            this.lblProbeValue.Text = "传感器数值：";
            // 
            // lblRecordTime
            // 
            this.lblRecordTime.AutoSize = true;
            this.lblRecordTime.Location = new System.Drawing.Point(25, 257);
            this.lblRecordTime.Name = "lblRecordTime";
            this.lblRecordTime.Size = new System.Drawing.Size(65, 12);
            this.lblRecordTime.TabIndex = 3;
            this.lblRecordTime.Text = "记录时间：";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(756, 309);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSubmit.Location = new System.Drawing.Point(675, 309);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 5;
            this.btnSubmit.Text = "提交";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // dtpRecordTime
            // 
            this.dtpRecordTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpRecordTime.Location = new System.Drawing.Point(84, 251);
            this.dtpRecordTime.Name = "dtpRecordTime";
            this.dtpRecordTime.Size = new System.Drawing.Size(160, 21);
            this.dtpRecordTime.TabIndex = 4;
            // 
            // gbTunnel
            // 
            this.gbTunnel.Controls.Add(this.selectTunnelUserControl1);
            this.gbTunnel.Location = new System.Drawing.Point(12, 12);
            this.gbTunnel.Name = "gbTunnel";
            this.gbTunnel.Size = new System.Drawing.Size(586, 200);
            this.gbTunnel.TabIndex = 7;
            this.gbTunnel.TabStop = false;
            this.gbTunnel.Text = "【选择传感器所在巷道】";
            // 
            // selectTunnelUserControl1
            // 
            this.selectTunnelUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selectTunnelUserControl1.ITunnelId = 0;
            this.selectTunnelUserControl1.Location = new System.Drawing.Point(3, 17);
            this.selectTunnelUserControl1.Name = "selectTunnelUserControl1";
            this.selectTunnelUserControl1.Size = new System.Drawing.Size(580, 180);
            this.selectTunnelUserControl1.STunnelName = null;
            this.selectTunnelUserControl1.TabIndex = 0;
            // 
            // _gbProbeSel
            // 
            this._gbProbeSel.Controls.Add(this.label7);
            this._gbProbeSel.Controls.Add(this._lblProbeName);
            this._gbProbeSel.Controls.Add(this._lblProbeStyle);
            this._gbProbeSel.Controls.Add(this._lstProbeName);
            this._gbProbeSel.Controls.Add(this._lstProbeStyle);
            this._gbProbeSel.Location = new System.Drawing.Point(604, 13);
            this._gbProbeSel.Name = "_gbProbeSel";
            this._gbProbeSel.Size = new System.Drawing.Size(227, 199);
            this._gbProbeSel.TabIndex = 8;
            this._gbProbeSel.TabStop = false;
            this._gbProbeSel.Text = "【传感器选择】";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(98, 85);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 12);
            this.label7.TabIndex = 2;
            this.label7.Text = ">>";
            // 
            // _lblProbeName
            // 
            this._lblProbeName.AutoSize = true;
            this._lblProbeName.Location = new System.Drawing.Point(128, 24);
            this._lblProbeName.Name = "_lblProbeName";
            this._lblProbeName.Size = new System.Drawing.Size(65, 12);
            this._lblProbeName.TabIndex = 3;
            this._lblProbeName.Text = "传感器名称";
            // 
            // _lblProbeStyle
            // 
            this._lblProbeStyle.AutoSize = true;
            this._lblProbeStyle.Location = new System.Drawing.Point(16, 24);
            this._lblProbeStyle.Name = "_lblProbeStyle";
            this._lblProbeStyle.Size = new System.Drawing.Size(65, 12);
            this._lblProbeStyle.TabIndex = 0;
            this._lblProbeStyle.Text = "传感器类型";
            // 
            // _lstProbeName
            // 
            this._lstProbeName.FormattingEnabled = true;
            this._lstProbeName.ItemHeight = 12;
            this._lstProbeName.Location = new System.Drawing.Point(121, 39);
            this._lstProbeName.Name = "_lstProbeName";
            this._lstProbeName.Size = new System.Drawing.Size(84, 124);
            this._lstProbeName.TabIndex = 4;
            this._lstProbeName.MouseUp += new System.Windows.Forms.MouseEventHandler(this._lstProbeName_MouseUp);
            // 
            // _lstProbeStyle
            // 
            this._lstProbeStyle.FormattingEnabled = true;
            this._lstProbeStyle.ItemHeight = 12;
            this._lstProbeStyle.Location = new System.Drawing.Point(8, 39);
            this._lstProbeStyle.Name = "_lstProbeStyle";
            this._lstProbeStyle.Size = new System.Drawing.Size(84, 124);
            this._lstProbeStyle.TabIndex = 1;
            this._lstProbeStyle.MouseUp += new System.Windows.Forms.MouseEventHandler(this._lstProbeStyle_MouseUp);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(179, 229);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(11, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "*";
            // 
            // GasConcentrationProbeDataEntering
            // 
            this.AcceptButton = this.btnSubmit;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(844, 344);
            this.Controls.Add(this.label3);
            this.Controls.Add(this._gbProbeSel);
            this.Controls.Add(this.gbTunnel);
            this.Controls.Add(this.dtpRecordTime);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.txtProbeValue);
            this.Controls.Add(this.lblRecordTime);
            this.Controls.Add(this.lblProbeValue);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GasConcentrationProbeDataEntering";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "传感器数据录入";
            this.gbTunnel.ResumeLayout(false);
            this._gbProbeSel.ResumeLayout(false);
            this._gbProbeSel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtProbeValue;
        private System.Windows.Forms.Label lblProbeValue;
        private System.Windows.Forms.Label lblRecordTime;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.DateTimePicker dtpRecordTime;
        private System.Windows.Forms.GroupBox gbTunnel;
        private LibCommonForm.SelectTunnelUserControl selectTunnelUserControl1;
        private System.Windows.Forms.GroupBox _gbProbeSel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label _lblProbeName;
        private System.Windows.Forms.Label _lblProbeStyle;
        private System.Windows.Forms.ListBox _lstProbeName;
        private System.Windows.Forms.ListBox _lstProbeStyle;
        private System.Windows.Forms.Label label3;
    }
}