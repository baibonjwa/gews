namespace LibCommonForm
{
    partial class UserInformationInput
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
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelLoginName = new System.Windows.Forms.Label();
            this._txtLoginName = new System.Windows.Forms.TextBox();
            this._txtPassWord = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this._txtEmail = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this._txtTel = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this._txtPhoneNumber = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this._cboDepartment = new System.Windows.Forms.ComboBox();
            this._cboGroup = new System.Windows.Forms.ComboBox();
            this._rtxtRemark = new System.Windows.Forms.RichTextBox();
            this._cboPromission = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this._txtName = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonOK.Location = new System.Drawing.Point(302, 168);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 10;
            this.buttonOK.Text = "确定";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonCancel.Location = new System.Drawing.Point(383, 168);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 12;
            this.buttonCancel.Text = "取消";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // labelLoginName
            // 
            this.labelLoginName.AutoSize = true;
            this.labelLoginName.Location = new System.Drawing.Point(12, 12);
            this.labelLoginName.Name = "labelLoginName";
            this.labelLoginName.Size = new System.Drawing.Size(65, 12);
            this.labelLoginName.TabIndex = 0;
            this.labelLoginName.Text = "登 录 名：";
            // 
            // _txtLoginName
            // 
            this._txtLoginName.Location = new System.Drawing.Point(83, 9);
            this._txtLoginName.Name = "_txtLoginName";
            this._txtLoginName.Size = new System.Drawing.Size(136, 21);
            this._txtLoginName.TabIndex = 0;
            // 
            // _txtPassWord
            // 
            this._txtPassWord.Location = new System.Drawing.Point(83, 36);
            this._txtPassWord.Name = "_txtPassWord";
            this._txtPassWord.Size = new System.Drawing.Size(136, 21);
            this._txtPassWord.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "密    码：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "用 户 组：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "部    门：";
            // 
            // _txtEmail
            // 
            this._txtEmail.Location = new System.Drawing.Point(322, 36);
            this._txtEmail.Name = "_txtEmail";
            this._txtEmail.Size = new System.Drawing.Size(136, 21);
            this._txtEmail.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(251, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "电子邮箱：";
            // 
            // _txtTel
            // 
            this._txtTel.Location = new System.Drawing.Point(322, 9);
            this._txtTel.Name = "_txtTel";
            this._txtTel.Size = new System.Drawing.Size(136, 21);
            this._txtTel.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(251, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 12;
            this.label5.Text = "电话号码：";
            // 
            // _txtPhoneNumber
            // 
            this._txtPhoneNumber.Location = new System.Drawing.Point(83, 168);
            this._txtPhoneNumber.Name = "_txtPhoneNumber";
            this._txtPhoneNumber.Size = new System.Drawing.Size(136, 21);
            this._txtPhoneNumber.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 171);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 14;
            this.label6.Text = "手机号码：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(251, 66);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 18;
            this.label7.Text = "备    注：";
            // 
            // _cboDepartment
            // 
            this._cboDepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cboDepartment.FormattingEnabled = true;
            this._cboDepartment.Location = new System.Drawing.Point(83, 115);
            this._cboDepartment.Name = "_cboDepartment";
            this._cboDepartment.Size = new System.Drawing.Size(136, 20);
            this._cboDepartment.TabIndex = 4;
            // 
            // _cboGroup
            // 
            this._cboGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cboGroup.FormattingEnabled = true;
            this._cboGroup.Location = new System.Drawing.Point(83, 88);
            this._cboGroup.Name = "_cboGroup";
            this._cboGroup.Size = new System.Drawing.Size(136, 20);
            this._cboGroup.TabIndex = 3;
            // 
            // _rtxtRemark
            // 
            this._rtxtRemark.Location = new System.Drawing.Point(322, 63);
            this._rtxtRemark.Name = "_rtxtRemark";
            this._rtxtRemark.Size = new System.Drawing.Size(136, 91);
            this._rtxtRemark.TabIndex = 9;
            this._rtxtRemark.Text = "";
            // 
            // _cboPromission
            // 
            this._cboPromission.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cboPromission.FormattingEnabled = true;
            this._cboPromission.Items.AddRange(new object[] {
            "普通用户",
            "管理员"});
            this._cboPromission.Location = new System.Drawing.Point(83, 62);
            this._cboPromission.Name = "_cboPromission";
            this._cboPromission.Size = new System.Drawing.Size(136, 20);
            this._cboPromission.TabIndex = 2;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 65);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 16;
            this.label8.Text = "权    限：";
            // 
            // _txtName
            // 
            this._txtName.Location = new System.Drawing.Point(83, 141);
            this._txtName.Name = "_txtName";
            this._txtName.Size = new System.Drawing.Size(136, 21);
            this._txtName.TabIndex = 5;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 144);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 8;
            this.label9.Text = "姓    名：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(225, 12);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(11, 12);
            this.label10.TabIndex = 19;
            this.label10.Text = "*";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(225, 39);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(11, 12);
            this.label11.TabIndex = 20;
            this.label11.Text = "*";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(225, 66);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(11, 12);
            this.label12.TabIndex = 21;
            this.label12.Text = "*";
            // 
            // UserInformationInput
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(470, 200);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this._txtName);
            this.Controls.Add(this.label9);
            this.Controls.Add(this._cboPromission);
            this.Controls.Add(this.label8);
            this.Controls.Add(this._rtxtRemark);
            this.Controls.Add(this._cboGroup);
            this.Controls.Add(this._cboDepartment);
            this.Controls.Add(this.label7);
            this.Controls.Add(this._txtPhoneNumber);
            this.Controls.Add(this.label6);
            this.Controls.Add(this._txtTel);
            this.Controls.Add(this.label5);
            this.Controls.Add(this._txtEmail);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._txtPassWord);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._txtLoginName);
            this.Controls.Add(this.labelLoginName);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UserInformationInput";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "用户信息录入";
            this.Load += new System.EventHandler(this.UserInformationInput_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelLoginName;
        private System.Windows.Forms.TextBox _txtLoginName;
        private System.Windows.Forms.TextBox _txtPassWord;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox _txtEmail;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox _txtTel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox _txtPhoneNumber;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox _cboDepartment;
        private System.Windows.Forms.ComboBox _cboGroup;
        private System.Windows.Forms.RichTextBox _rtxtRemark;
        private System.Windows.Forms.ComboBox _cboPromission;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox _txtName;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
    }
}