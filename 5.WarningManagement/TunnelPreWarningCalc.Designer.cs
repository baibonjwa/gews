namespace _5.WarningManagement
{
    partial class TunnelPreWarningCalc
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
            this._splitContainer = new System.Windows.Forms.SplitContainer();
            this._btnCalc = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this._txtInterval = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this._dateTimePickerStart = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this._dateTimePickerEnd = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnAddTunnel = new System.Windows.Forms.Button();
            this.lblMineName = new System.Windows.Forms.Label();
            this.cboMineName = new System.Windows.Forms.ComboBox();
            this.lblTunnelName = new System.Windows.Forms.Label();
            this.cboTunnelName = new System.Windows.Forms.ComboBox();
            this.cboHorizontal = new System.Windows.Forms.ComboBox();
            this.lblHorizontal = new System.Windows.Forms.Label();
            this.cboMiningArea = new System.Windows.Forms.ComboBox();
            this.lblWorkingFace = new System.Windows.Forms.Label();
            this.lblMiningArea = new System.Windows.Forms.Label();
            this.cboWorkingFace = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this._splitContainer)).BeginInit();
            this._splitContainer.Panel1.SuspendLayout();
            this._splitContainer.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _splitContainer
            // 
            this._splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this._splitContainer.IsSplitterFixed = true;
            this._splitContainer.Location = new System.Drawing.Point(0, 0);
            this._splitContainer.Name = "_splitContainer";
            // 
            // _splitContainer.Panel1
            // 
            this._splitContainer.Panel1.Controls.Add(this._btnCalc);
            this._splitContainer.Panel1.Controls.Add(this.groupBox2);
            this._splitContainer.Panel1.Controls.Add(this.groupBox1);
            this._splitContainer.Size = new System.Drawing.Size(792, 536);
            this._splitContainer.SplitterDistance = 325;
            this._splitContainer.TabIndex = 0;
            // 
            // _btnCalc
            // 
            this._btnCalc.Location = new System.Drawing.Point(240, 388);
            this._btnCalc.Name = "_btnCalc";
            this._btnCalc.Size = new System.Drawing.Size(75, 23);
            this._btnCalc.TabIndex = 2;
            this._btnCalc.Text = "计算 >>";
            this._btnCalc.UseVisualStyleBackColor = true;
            this._btnCalc.Click += new System.EventHandler(this._btnCalc_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this._txtInterval);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this._dateTimePickerStart);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this._dateTimePickerEnd);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(10, 238);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(305, 144);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "预警数据时段";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(22, 113);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(257, 12);
            this.label4.TabIndex = 29;
            this.label4.Text = "此处日期为方便测试，日后改为选择某天的数据";
            // 
            // _txtInterval
            // 
            this._txtInterval.Location = new System.Drawing.Point(87, 89);
            this._txtInterval.Name = "_txtInterval";
            this._txtInterval.ReadOnly = true;
            this._txtInterval.Size = new System.Drawing.Size(198, 21);
            this._txtInterval.TabIndex = 26;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 25;
            this.label3.Text = "时间间隔:";
            // 
            // _dateTimePickerStart
            // 
            this._dateTimePickerStart.CustomFormat = "yyyy/MM/dd hh:mm:ss";
            this._dateTimePickerStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this._dateTimePickerStart.Location = new System.Drawing.Point(85, 26);
            this._dateTimePickerStart.Name = "_dateTimePickerStart";
            this._dateTimePickerStart.Size = new System.Drawing.Size(200, 21);
            this._dateTimePickerStart.TabIndex = 23;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 24;
            this.label2.Text = "终止时间:";
            // 
            // _dateTimePickerEnd
            // 
            this._dateTimePickerEnd.CustomFormat = "yyyy/MM/dd hh:mm:ss";
            this._dateTimePickerEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this._dateTimePickerEnd.Location = new System.Drawing.Point(85, 54);
            this._dateTimePickerEnd.Name = "_dateTimePickerEnd";
            this._dateTimePickerEnd.Size = new System.Drawing.Size(200, 21);
            this._dateTimePickerEnd.TabIndex = 23;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 24;
            this.label1.Text = "起始时间:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnAddTunnel);
            this.groupBox1.Controls.Add(this.lblMineName);
            this.groupBox1.Controls.Add(this.cboMineName);
            this.groupBox1.Controls.Add(this.lblTunnelName);
            this.groupBox1.Controls.Add(this.cboTunnelName);
            this.groupBox1.Controls.Add(this.cboHorizontal);
            this.groupBox1.Controls.Add(this.lblHorizontal);
            this.groupBox1.Controls.Add(this.cboMiningArea);
            this.groupBox1.Controls.Add(this.lblWorkingFace);
            this.groupBox1.Controls.Add(this.lblMiningArea);
            this.groupBox1.Controls.Add(this.cboWorkingFace);
            this.groupBox1.Location = new System.Drawing.Point(10, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(305, 229);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "选择巷道";
            // 
            // btnAddTunnel
            // 
            this.btnAddTunnel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddTunnel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnAddTunnel.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnAddTunnel.FlatAppearance.BorderSize = 0;
            this.btnAddTunnel.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAddTunnel.Location = new System.Drawing.Point(272, 9);
            this.btnAddTunnel.Name = "btnAddTunnel";
            this.btnAddTunnel.Size = new System.Drawing.Size(31, 24);
            this.btnAddTunnel.TabIndex = 0;
            this.btnAddTunnel.Text = "＋";
            this.btnAddTunnel.UseVisualStyleBackColor = true;
            // 
            // lblMineName
            // 
            this.lblMineName.AutoSize = true;
            this.lblMineName.Location = new System.Drawing.Point(28, 41);
            this.lblMineName.Name = "lblMineName";
            this.lblMineName.Size = new System.Drawing.Size(53, 12);
            this.lblMineName.TabIndex = 21;
            this.lblMineName.Text = "矿井名称";
            // 
            // cboMineName
            // 
            this.cboMineName.BackColor = System.Drawing.SystemColors.Window;
            this.cboMineName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMineName.FormattingEnabled = true;
            this.cboMineName.Location = new System.Drawing.Point(85, 38);
            this.cboMineName.Name = "cboMineName";
            this.cboMineName.Size = new System.Drawing.Size(192, 20);
            this.cboMineName.TabIndex = 13;
            this.cboMineName.SelectedIndexChanged += new System.EventHandler(this.cboMineName_SelectedIndexChanged);
            // 
            // lblTunnelName
            // 
            this.lblTunnelName.AutoSize = true;
            this.lblTunnelName.Location = new System.Drawing.Point(28, 173);
            this.lblTunnelName.Name = "lblTunnelName";
            this.lblTunnelName.Size = new System.Drawing.Size(53, 12);
            this.lblTunnelName.TabIndex = 12;
            this.lblTunnelName.Text = "巷道名称";
            // 
            // cboTunnelName
            // 
            this.cboTunnelName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTunnelName.FormattingEnabled = true;
            this.cboTunnelName.Location = new System.Drawing.Point(85, 170);
            this.cboTunnelName.Name = "cboTunnelName";
            this.cboTunnelName.Size = new System.Drawing.Size(192, 20);
            this.cboTunnelName.TabIndex = 17;
            this.cboTunnelName.SelectedIndexChanged += new System.EventHandler(this.cboTunnelName_SelectedIndexChanged);
            // 
            // cboHorizontal
            // 
            this.cboHorizontal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHorizontal.FormattingEnabled = true;
            this.cboHorizontal.Location = new System.Drawing.Point(85, 71);
            this.cboHorizontal.Name = "cboHorizontal";
            this.cboHorizontal.Size = new System.Drawing.Size(192, 20);
            this.cboHorizontal.TabIndex = 14;
            this.cboHorizontal.SelectedIndexChanged += new System.EventHandler(this.cboHorizontal_SelectedIndexChanged);
            // 
            // lblHorizontal
            // 
            this.lblHorizontal.AutoSize = true;
            this.lblHorizontal.Location = new System.Drawing.Point(28, 74);
            this.lblHorizontal.Name = "lblHorizontal";
            this.lblHorizontal.Size = new System.Drawing.Size(53, 12);
            this.lblHorizontal.TabIndex = 18;
            this.lblHorizontal.Text = "水    平";
            // 
            // cboMiningArea
            // 
            this.cboMiningArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMiningArea.FormattingEnabled = true;
            this.cboMiningArea.Location = new System.Drawing.Point(85, 103);
            this.cboMiningArea.Name = "cboMiningArea";
            this.cboMiningArea.Size = new System.Drawing.Size(192, 20);
            this.cboMiningArea.TabIndex = 15;
            this.cboMiningArea.SelectedIndexChanged += new System.EventHandler(this.cboMiningArea_SelectedIndexChanged);
            // 
            // lblWorkingFace
            // 
            this.lblWorkingFace.AutoSize = true;
            this.lblWorkingFace.Location = new System.Drawing.Point(28, 139);
            this.lblWorkingFace.Name = "lblWorkingFace";
            this.lblWorkingFace.Size = new System.Drawing.Size(53, 12);
            this.lblWorkingFace.TabIndex = 19;
            this.lblWorkingFace.Text = "工 作 面";
            // 
            // lblMiningArea
            // 
            this.lblMiningArea.AutoSize = true;
            this.lblMiningArea.Location = new System.Drawing.Point(28, 106);
            this.lblMiningArea.Name = "lblMiningArea";
            this.lblMiningArea.Size = new System.Drawing.Size(53, 12);
            this.lblMiningArea.TabIndex = 20;
            this.lblMiningArea.Text = "采    区";
            // 
            // cboWorkingFace
            // 
            this.cboWorkingFace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboWorkingFace.FormattingEnabled = true;
            this.cboWorkingFace.Location = new System.Drawing.Point(85, 136);
            this.cboWorkingFace.Name = "cboWorkingFace";
            this.cboWorkingFace.Size = new System.Drawing.Size(192, 20);
            this.cboWorkingFace.TabIndex = 16;
            this.cboWorkingFace.SelectedIndexChanged += new System.EventHandler(this.cboWorkingFace_SelectedIndexChanged);
            // 
            // TunnelPreWarningCalc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 536);
            this.Controls.Add(this._splitContainer);
            this.IsMdiContainer = true;
            this.Name = "TunnelPreWarningCalc";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "巷道预警结果计算";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.TunnelPreWarningCalc_Load);
            this._splitContainer.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._splitContainer)).EndInit();
            this._splitContainer.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer _splitContainer;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DateTimePicker _dateTimePickerStart;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker _dateTimePickerEnd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblMineName;
        private System.Windows.Forms.ComboBox cboMineName;
        private System.Windows.Forms.Label lblTunnelName;
        private System.Windows.Forms.ComboBox cboTunnelName;
        private System.Windows.Forms.ComboBox cboHorizontal;
        private System.Windows.Forms.Label lblHorizontal;
        private System.Windows.Forms.ComboBox cboMiningArea;
        private System.Windows.Forms.Label lblWorkingFace;
        private System.Windows.Forms.Label lblMiningArea;
        private System.Windows.Forms.ComboBox cboWorkingFace;
        private System.Windows.Forms.Button _btnCalc;
        private System.Windows.Forms.TextBox _txtInterval;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnAddTunnel;

    }
}