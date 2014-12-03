namespace LibCommonForm
{
    partial class DatabaseManagement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DatabaseManagement));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnConn = new System.Windows.Forms.Button();
            this.txtLoginName = new System.Windows.Forms.TextBox();
            this.txtDataBaseName = new System.Windows.Forms.TextBox();
            this.txtServerNameOrIP = new System.Windows.Forms.TextBox();
            this.lblLoginName = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblDataBaseName = new System.Windows.Forms.Label();
            this.lblServerNameOrIP = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.btnConn);
            this.groupBox1.Controls.Add(this.txtLoginName);
            this.groupBox1.Controls.Add(this.txtDataBaseName);
            this.groupBox1.Controls.Add(this.txtServerNameOrIP);
            this.groupBox1.Controls.Add(this.lblLoginName);
            this.groupBox1.Controls.Add(this.lblPassword);
            this.groupBox1.Controls.Add(this.lblDataBaseName);
            this.groupBox1.Controls.Add(this.lblServerNameOrIP);
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.groupBox1.Location = new System.Drawing.Point(26, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(452, 233);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "数据库连接配置项";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(113, 172);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(171, 21);
            this.txtPassword.TabIndex = 3;
            // 
            // btnConn
            // 
            this.btnConn.ForeColor = System.Drawing.Color.Black;
            this.btnConn.Location = new System.Drawing.Point(306, 41);
            this.btnConn.Name = "btnConn";
            this.btnConn.Size = new System.Drawing.Size(130, 23);
            this.btnConn.TabIndex = 4;
            this.btnConn.Text = "测试连接";
            this.btnConn.UseVisualStyleBackColor = true;
            this.btnConn.Click += new System.EventHandler(this.btnConn_Click);
            // 
            // txtLoginName
            // 
            this.txtLoginName.Location = new System.Drawing.Point(113, 129);
            this.txtLoginName.Name = "txtLoginName";
            this.txtLoginName.Size = new System.Drawing.Size(171, 21);
            this.txtLoginName.TabIndex = 2;
            // 
            // txtDataBaseName
            // 
            this.txtDataBaseName.Location = new System.Drawing.Point(113, 86);
            this.txtDataBaseName.Name = "txtDataBaseName";
            this.txtDataBaseName.Size = new System.Drawing.Size(171, 21);
            this.txtDataBaseName.TabIndex = 1;
            // 
            // txtServerNameOrIP
            // 
            this.txtServerNameOrIP.Location = new System.Drawing.Point(113, 43);
            this.txtServerNameOrIP.Name = "txtServerNameOrIP";
            this.txtServerNameOrIP.Size = new System.Drawing.Size(171, 21);
            this.txtServerNameOrIP.TabIndex = 0;
            // 
            // lblLoginName
            // 
            this.lblLoginName.AutoSize = true;
            this.lblLoginName.ForeColor = System.Drawing.Color.Black;
            this.lblLoginName.Location = new System.Drawing.Point(65, 132);
            this.lblLoginName.Name = "lblLoginName";
            this.lblLoginName.Size = new System.Drawing.Size(53, 12);
            this.lblLoginName.TabIndex = 0;
            this.lblLoginName.Text = "登录名：";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.ForeColor = System.Drawing.Color.Black;
            this.lblPassword.Location = new System.Drawing.Point(53, 175);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(65, 12);
            this.lblPassword.TabIndex = 0;
            this.lblPassword.Text = "登录密码：";
            // 
            // lblDataBaseName
            // 
            this.lblDataBaseName.AutoSize = true;
            this.lblDataBaseName.ForeColor = System.Drawing.Color.Black;
            this.lblDataBaseName.Location = new System.Drawing.Point(53, 89);
            this.lblDataBaseName.Name = "lblDataBaseName";
            this.lblDataBaseName.Size = new System.Drawing.Size(65, 12);
            this.lblDataBaseName.TabIndex = 0;
            this.lblDataBaseName.Text = "数据库名：";
            // 
            // lblServerNameOrIP
            // 
            this.lblServerNameOrIP.AutoSize = true;
            this.lblServerNameOrIP.ForeColor = System.Drawing.Color.Black;
            this.lblServerNameOrIP.Location = new System.Drawing.Point(17, 46);
            this.lblServerNameOrIP.Name = "lblServerNameOrIP";
            this.lblServerNameOrIP.Size = new System.Drawing.Size(101, 12);
            this.lblServerNameOrIP.TabIndex = 0;
            this.lblServerNameOrIP.Text = "服务器名称或IP：";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(217, 283);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(403, 283);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnReset
            // 
            this.btnReset.ForeColor = System.Drawing.Color.Black;
            this.btnReset.Location = new System.Drawing.Point(26, 283);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 1;
            this.btnReset.Text = "重置";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // DatabaseManagement
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(500, 325);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DatabaseManagement";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据库管理";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnConn;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtLoginName;
        private System.Windows.Forms.TextBox txtDataBaseName;
        private System.Windows.Forms.TextBox txtServerNameOrIP;
        private System.Windows.Forms.Label lblLoginName;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblDataBaseName;
        private System.Windows.Forms.Label lblServerNameOrIP;
        private System.Windows.Forms.Button btnReset;
    }
}