namespace UnderTerminal
{
    partial class SetDefaultValue
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cboTeamName = new System.Windows.Forms.ComboBox();
            this.lblTeam = new System.Windows.Forms.Label();
            this.cboSubmitter = new System.Windows.Forms.ComboBox();
            this.lblSubmiter = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblWorkStyle = new System.Windows.Forms.Label();
            this.cboWorkTime = new System.Windows.Forms.ComboBox();
            this.rbtn38 = new System.Windows.Forms.RadioButton();
            this.lblWorkTime = new System.Windows.Forms.Label();
            this.rbtn46 = new System.Windows.Forms.RadioButton();
            this.btnOK = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cboTeamName);
            this.groupBox2.Controls.Add(this.lblTeam);
            this.groupBox2.Controls.Add(this.cboSubmitter);
            this.groupBox2.Controls.Add(this.lblSubmiter);
            this.groupBox2.Location = new System.Drawing.Point(214, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(175, 100);
            this.groupBox2.TabIndex = 45;
            this.groupBox2.TabStop = false;
            // 
            // cboTeamName
            // 
            this.cboTeamName.FormattingEnabled = true;
            this.cboTeamName.Location = new System.Drawing.Point(68, 20);
            this.cboTeamName.Name = "cboTeamName";
            this.cboTeamName.Size = new System.Drawing.Size(93, 20);
            this.cboTeamName.TabIndex = 40;
            this.cboTeamName.SelectedIndexChanged += new System.EventHandler(this.cboTeamName_SelectedIndexChanged);
            // 
            // lblTeam
            // 
            this.lblTeam.AutoSize = true;
            this.lblTeam.BackColor = System.Drawing.Color.Transparent;
            this.lblTeam.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblTeam.Location = new System.Drawing.Point(11, 23);
            this.lblTeam.Name = "lblTeam";
            this.lblTeam.Size = new System.Drawing.Size(29, 12);
            this.lblTeam.TabIndex = 39;
            this.lblTeam.Text = "队别";
            // 
            // cboSubmitter
            // 
            this.cboSubmitter.FormattingEnabled = true;
            this.cboSubmitter.Location = new System.Drawing.Point(68, 65);
            this.cboSubmitter.Name = "cboSubmitter";
            this.cboSubmitter.Size = new System.Drawing.Size(94, 20);
            this.cboSubmitter.TabIndex = 36;
            // 
            // lblSubmiter
            // 
            this.lblSubmiter.AutoSize = true;
            this.lblSubmiter.BackColor = System.Drawing.Color.Transparent;
            this.lblSubmiter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblSubmiter.Location = new System.Drawing.Point(7, 68);
            this.lblSubmiter.Name = "lblSubmiter";
            this.lblSubmiter.Size = new System.Drawing.Size(41, 12);
            this.lblSubmiter.TabIndex = 35;
            this.lblSubmiter.Text = "填报人";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblWorkStyle);
            this.groupBox1.Controls.Add(this.cboWorkTime);
            this.groupBox1.Controls.Add(this.rbtn38);
            this.groupBox1.Controls.Add(this.lblWorkTime);
            this.groupBox1.Controls.Add(this.rbtn46);
            this.groupBox1.Location = new System.Drawing.Point(25, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(175, 100);
            this.groupBox1.TabIndex = 44;
            this.groupBox1.TabStop = false;
            // 
            // lblWorkStyle
            // 
            this.lblWorkStyle.AutoSize = true;
            this.lblWorkStyle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblWorkStyle.Location = new System.Drawing.Point(6, 19);
            this.lblWorkStyle.Name = "lblWorkStyle";
            this.lblWorkStyle.Size = new System.Drawing.Size(53, 12);
            this.lblWorkStyle.TabIndex = 31;
            this.lblWorkStyle.Text = "工作制式";
            // 
            // cboWorkTime
            // 
            this.cboWorkTime.FormattingEnabled = true;
            this.cboWorkTime.Location = new System.Drawing.Point(66, 68);
            this.cboWorkTime.Name = "cboWorkTime";
            this.cboWorkTime.Size = new System.Drawing.Size(88, 20);
            this.cboWorkTime.TabIndex = 34;
            // 
            // rbtn38
            // 
            this.rbtn38.AutoSize = true;
            this.rbtn38.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbtn38.Location = new System.Drawing.Point(86, 17);
            this.rbtn38.Name = "rbtn38";
            this.rbtn38.Size = new System.Drawing.Size(58, 16);
            this.rbtn38.TabIndex = 32;
            this.rbtn38.TabStop = true;
            this.rbtn38.Text = "三八制";
            this.rbtn38.UseVisualStyleBackColor = true;
            this.rbtn38.CheckedChanged += new System.EventHandler(this.rbtn38_CheckedChanged);
            // 
            // lblWorkTime
            // 
            this.lblWorkTime.AutoSize = true;
            this.lblWorkTime.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblWorkTime.Location = new System.Drawing.Point(5, 71);
            this.lblWorkTime.Name = "lblWorkTime";
            this.lblWorkTime.Size = new System.Drawing.Size(53, 12);
            this.lblWorkTime.TabIndex = 0;
            this.lblWorkTime.Text = "班    次";
            // 
            // rbtn46
            // 
            this.rbtn46.AutoSize = true;
            this.rbtn46.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbtn46.Location = new System.Drawing.Point(86, 47);
            this.rbtn46.Name = "rbtn46";
            this.rbtn46.Size = new System.Drawing.Size(58, 16);
            this.rbtn46.TabIndex = 33;
            this.rbtn46.TabStop = true;
            this.rbtn46.Text = "四六制";
            this.rbtn46.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(314, 126);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 46;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // SetDefaultValue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(413, 161);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "SetDefaultValue";
            this.Text = "设置默认班次队别";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cboTeamName;
        private System.Windows.Forms.Label lblTeam;
        private System.Windows.Forms.ComboBox cboSubmitter;
        private System.Windows.Forms.Label lblSubmiter;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblWorkStyle;
        private System.Windows.Forms.ComboBox cboWorkTime;
        private System.Windows.Forms.RadioButton rbtn38;
        private System.Windows.Forms.Label lblWorkTime;
        private System.Windows.Forms.RadioButton rbtn46;
        private System.Windows.Forms.Button btnOK;
    }
}