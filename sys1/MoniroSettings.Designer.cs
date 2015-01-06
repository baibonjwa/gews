namespace sys1
{
    partial class MoniroSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MoniroSettings));
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this._cmbFrequency = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.gb02 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tbDataCountPerFrame = new System.Windows.Forms.TextBox();
            this.tbBadDataThreshold = new System.Windows.Forms.TextBox();
            this.tbYellowThreshold = new System.Windows.Forms.TextBox();
            this.tbRedThreshold = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.gb02.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(125, 238);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(252, 238);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // _cmbFrequency
            // 
            this._cmbFrequency.FormattingEnabled = true;
            this._cmbFrequency.Items.AddRange(new object[] {
            "1",
            "10",
            "20",
            "30",
            "60"});
            this._cmbFrequency.Location = new System.Drawing.Point(178, 15);
            this._cmbFrequency.Name = "_cmbFrequency";
            this._cmbFrequency.Size = new System.Drawing.Size(100, 20);
            this._cmbFrequency.TabIndex = 7;
            this._cmbFrequency.Text = "10";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "实时监控数据更新频率：";
            // 
            // gb02
            // 
            this.gb02.Controls.Add(this.label12);
            this.gb02.Controls.Add(this.label11);
            this.gb02.Controls.Add(this.label10);
            this.gb02.Controls.Add(this.label9);
            this.gb02.Controls.Add(this.label8);
            this.gb02.Controls.Add(this.label7);
            this.gb02.Controls.Add(this.tbDataCountPerFrame);
            this.gb02.Controls.Add(this.tbBadDataThreshold);
            this.gb02.Controls.Add(this.tbYellowThreshold);
            this.gb02.Controls.Add(this.tbRedThreshold);
            this.gb02.Controls.Add(this.label5);
            this.gb02.Controls.Add(this._cmbFrequency);
            this.gb02.Controls.Add(this.label4);
            this.gb02.Controls.Add(this.label3);
            this.gb02.Controls.Add(this.label1);
            this.gb02.Controls.Add(this.label6);
            this.gb02.Controls.Add(this.label2);
            this.gb02.Location = new System.Drawing.Point(21, 12);
            this.gb02.Name = "gb02";
            this.gb02.Size = new System.Drawing.Size(397, 209);
            this.gb02.TabIndex = 23;
            this.gb02.TabStop = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(128, 185);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(131, 12);
            this.label12.TabIndex = 10;
            this.label12.Text = "*括号中的数值为参考值";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(284, 156);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(23, 12);
            this.label11.TabIndex = 9;
            this.label11.Text = "(5)";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(284, 123);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 9;
            this.label10.Text = "(0.75)";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(284, 91);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(23, 12);
            this.label9.TabIndex = 9;
            this.label9.Text = "(1)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(284, 51);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 12);
            this.label8.TabIndex = 9;
            this.label8.Text = "(120)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(297, 19);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 9;
            this.label7.Text = "(10)";
            // 
            // tbDataCountPerFrame
            // 
            this.tbDataCountPerFrame.Location = new System.Drawing.Point(178, 48);
            this.tbDataCountPerFrame.Name = "tbDataCountPerFrame";
            this.tbDataCountPerFrame.Size = new System.Drawing.Size(100, 21);
            this.tbDataCountPerFrame.TabIndex = 8;
            this.tbDataCountPerFrame.Text = "120";
            // 
            // tbBadDataThreshold
            // 
            this.tbBadDataThreshold.Location = new System.Drawing.Point(178, 152);
            this.tbBadDataThreshold.Name = "tbBadDataThreshold";
            this.tbBadDataThreshold.Size = new System.Drawing.Size(100, 21);
            this.tbBadDataThreshold.TabIndex = 8;
            this.tbBadDataThreshold.Text = "10";
            // 
            // tbYellowThreshold
            // 
            this.tbYellowThreshold.Location = new System.Drawing.Point(178, 119);
            this.tbYellowThreshold.Name = "tbYellowThreshold";
            this.tbYellowThreshold.Size = new System.Drawing.Size(100, 21);
            this.tbYellowThreshold.TabIndex = 8;
            this.tbYellowThreshold.Text = "4";
            // 
            // tbRedThreshold
            // 
            this.tbRedThreshold.Location = new System.Drawing.Point(178, 86);
            this.tbRedThreshold.Name = "tbRedThreshold";
            this.tbRedThreshold.Size = new System.Drawing.Size(100, 21);
            this.tbRedThreshold.TabIndex = 8;
            this.tbRedThreshold.Text = "0.75";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(44, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(113, 12);
            this.label5.TabIndex = 6;
            this.label5.Text = "每屏显示数据个数：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(56, 156);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(113, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "智能识别剔除阈值：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(68, 123);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "黄色警告阈值：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(68, 91);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "红色警告阈值：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(283, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 12);
            this.label6.TabIndex = 6;
            this.label6.Text = "秒";
            // 
            // MoniroSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(443, 274);
            this.Controls.Add(this.gb02);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MoniroSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "监控参数设置";
            this.gb02.ResumeLayout(false);
            this.gb02.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox _cmbFrequency;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox gb02;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbBadDataThreshold;
        private System.Windows.Forms.TextBox tbYellowThreshold;
        private System.Windows.Forms.TextBox tbRedThreshold;
        private System.Windows.Forms.TextBox tbDataCountPerFrame;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label12;
    }
}