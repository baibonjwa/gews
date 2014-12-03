namespace LibCommonForm
{
    partial class UserLoginInformationInput
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
            this._cboPermission = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this._rtxtRemark = new System.Windows.Forms.RichTextBox();
            this._cboGroup = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this._txtPassWord = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this._txtLoginName = new System.Windows.Forms.TextBox();
            this.labelLoginName = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this._lblLoginName = new System.Windows.Forms.Label();
            this._lblConfirmPassword = new System.Windows.Forms.Label();
            this._lblPassword = new System.Windows.Forms.Label();
            this._txtConfirmPassword = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this._lblPermission = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _cboPermission
            // 
            this._cboPermission.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cboPermission.FormattingEnabled = true;
            this._cboPermission.Location = new System.Drawing.Point(83, 86);
            this._cboPermission.Name = "_cboPermission";
            this._cboPermission.Size = new System.Drawing.Size(136, 20);
            this._cboPermission.TabIndex = 3;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 89);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 29;
            this.label8.Text = "权    限：";
            // 
            // _rtxtRemark
            // 
            this._rtxtRemark.Location = new System.Drawing.Point(83, 138);
            this._rtxtRemark.Name = "_rtxtRemark";
            this._rtxtRemark.Size = new System.Drawing.Size(136, 73);
            this._rtxtRemark.TabIndex = 5;
            this._rtxtRemark.Text = "";
            // 
            // _cboGroup
            // 
            this._cboGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cboGroup.FormattingEnabled = true;
            this._cboGroup.Location = new System.Drawing.Point(83, 112);
            this._cboGroup.Name = "_cboGroup";
            this._cboGroup.Size = new System.Drawing.Size(136, 20);
            this._cboGroup.TabIndex = 4;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 141);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 30;
            this.label7.Text = "备    注：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 115);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 25;
            this.label2.Text = "用 户 组：";
            // 
            // _txtPassWord
            // 
            this._txtPassWord.Location = new System.Drawing.Point(83, 33);
            this._txtPassWord.Name = "_txtPassWord";
            this._txtPassWord.PasswordChar = '*';
            this._txtPassWord.Size = new System.Drawing.Size(136, 21);
            this._txtPassWord.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 23;
            this.label1.Text = "密    码：";
            // 
            // _txtLoginName
            // 
            this._txtLoginName.Location = new System.Drawing.Point(83, 6);
            this._txtLoginName.Name = "_txtLoginName";
            this._txtLoginName.Size = new System.Drawing.Size(136, 21);
            this._txtLoginName.TabIndex = 0;
            // 
            // labelLoginName
            // 
            this.labelLoginName.AutoSize = true;
            this.labelLoginName.Location = new System.Drawing.Point(12, 9);
            this.labelLoginName.Name = "labelLoginName";
            this.labelLoginName.Size = new System.Drawing.Size(65, 12);
            this.labelLoginName.TabIndex = 19;
            this.labelLoginName.Text = "登 录 名：";
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(63, 220);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 6;
            this.buttonOK.Text = "确定";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(144, 220);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 7;
            this.buttonCancel.Text = "取消";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // _lblLoginName
            // 
            this._lblLoginName.AutoSize = true;
            this._lblLoginName.ForeColor = System.Drawing.Color.Red;
            this._lblLoginName.Location = new System.Drawing.Point(224, 9);
            this._lblLoginName.Name = "_lblLoginName";
            this._lblLoginName.Size = new System.Drawing.Size(11, 12);
            this._lblLoginName.TabIndex = 31;
            this._lblLoginName.Text = "*";
            // 
            // _lblConfirmPassword
            // 
            this._lblConfirmPassword.AutoSize = true;
            this._lblConfirmPassword.ForeColor = System.Drawing.Color.Red;
            this._lblConfirmPassword.Location = new System.Drawing.Point(224, 62);
            this._lblConfirmPassword.Name = "_lblConfirmPassword";
            this._lblConfirmPassword.Size = new System.Drawing.Size(11, 12);
            this._lblConfirmPassword.TabIndex = 32;
            this._lblConfirmPassword.Text = "*";
            // 
            // _lblPassword
            // 
            this._lblPassword.AutoSize = true;
            this._lblPassword.ForeColor = System.Drawing.Color.Red;
            this._lblPassword.Location = new System.Drawing.Point(224, 36);
            this._lblPassword.Name = "_lblPassword";
            this._lblPassword.Size = new System.Drawing.Size(11, 12);
            this._lblPassword.TabIndex = 33;
            this._lblPassword.Text = "*";
            // 
            // _txtConfirmPassword
            // 
            this._txtConfirmPassword.Location = new System.Drawing.Point(83, 59);
            this._txtConfirmPassword.Name = "_txtConfirmPassword";
            this._txtConfirmPassword.PasswordChar = '*';
            this._txtConfirmPassword.Size = new System.Drawing.Size(136, 21);
            this._txtConfirmPassword.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 62);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 35;
            this.label6.Text = "确认密码：";
            // 
            // _lblPermission
            // 
            this._lblPermission.AutoSize = true;
            this._lblPermission.ForeColor = System.Drawing.Color.Red;
            this._lblPermission.Location = new System.Drawing.Point(224, 89);
            this._lblPermission.Name = "_lblPermission";
            this._lblPermission.Size = new System.Drawing.Size(11, 12);
            this._lblPermission.TabIndex = 36;
            this._lblPermission.Text = "*";
            // 
            // UserLoginInformationInput
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(240, 252);
            this.Controls.Add(this._lblPermission);
            this.Controls.Add(this._txtConfirmPassword);
            this.Controls.Add(this.label6);
            this.Controls.Add(this._lblPassword);
            this.Controls.Add(this._lblConfirmPassword);
            this.Controls.Add(this._lblLoginName);
            this.Controls.Add(this._cboPermission);
            this.Controls.Add(this.label8);
            this.Controls.Add(this._rtxtRemark);
            this.Controls.Add(this._cboGroup);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._txtPassWord);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._txtLoginName);
            this.Controls.Add(this.labelLoginName);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Name = "UserLoginInformationInput";
            this.Text = "UserLoginInformationInput";
            this.Load += new System.EventHandler(this.UserLoginInformationInput_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox _cboPermission;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.RichTextBox _rtxtRemark;
        private System.Windows.Forms.ComboBox _cboGroup;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox _txtPassWord;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox _txtLoginName;
        private System.Windows.Forms.Label labelLoginName;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label _lblLoginName;
        private System.Windows.Forms.Label _lblConfirmPassword;
        private System.Windows.Forms.Label _lblPassword;
        private System.Windows.Forms.TextBox _txtConfirmPassword;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label _lblPermission;
    }
}