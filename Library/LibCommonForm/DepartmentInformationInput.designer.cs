namespace LibCommonForm
{
    partial class DepartmentInformationInput
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
            this.labelName = new System.Windows.Forms.Label();
            this._txtName = new System.Windows.Forms.TextBox();
            this._txtTel = new System.Windows.Forms.TextBox();
            this.labelTel = new System.Windows.Forms.Label();
            this._txtStaffCount = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this._txtEmail = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this._rtxtRemarks = new System.Windows.Forms.RichTextBox();
            this._buttonCancel = new System.Windows.Forms.Button();
            this._buttonOK = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(13, 15);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(65, 12);
            this.labelName.TabIndex = 0;
            this.labelName.Text = "部门名称：";
            // 
            // _txtName
            // 
            this._txtName.Location = new System.Drawing.Point(84, 12);
            this._txtName.Name = "_txtName";
            this._txtName.Size = new System.Drawing.Size(118, 21);
            this._txtName.TabIndex = 1;
            // 
            // _txtTel
            // 
            this._txtTel.Location = new System.Drawing.Point(84, 42);
            this._txtTel.Name = "_txtTel";
            this._txtTel.Size = new System.Drawing.Size(118, 21);
            this._txtTel.TabIndex = 3;
            // 
            // labelTel
            // 
            this.labelTel.AutoSize = true;
            this.labelTel.Location = new System.Drawing.Point(13, 45);
            this.labelTel.Name = "labelTel";
            this.labelTel.Size = new System.Drawing.Size(65, 12);
            this.labelTel.TabIndex = 2;
            this.labelTel.Text = "部门电话：";
            // 
            // _txtStaffCount
            // 
            this._txtStaffCount.Location = new System.Drawing.Point(84, 102);
            this._txtStaffCount.Name = "_txtStaffCount";
            this._txtStaffCount.Size = new System.Drawing.Size(118, 21);
            this._txtStaffCount.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "部门Email：";
            // 
            // _txtEmail
            // 
            this._txtEmail.Location = new System.Drawing.Point(84, 72);
            this._txtEmail.Name = "_txtEmail";
            this._txtEmail.Size = new System.Drawing.Size(118, 21);
            this._txtEmail.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "人员数量：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 135);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "备注：";
            // 
            // _rtxtRemarks
            // 
            this._rtxtRemarks.Location = new System.Drawing.Point(60, 135);
            this._rtxtRemarks.Name = "_rtxtRemarks";
            this._rtxtRemarks.Size = new System.Drawing.Size(142, 72);
            this._rtxtRemarks.TabIndex = 9;
            this._rtxtRemarks.Text = "";
            // 
            // _buttonCancel
            // 
            this._buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._buttonCancel.Location = new System.Drawing.Point(127, 217);
            this._buttonCancel.Name = "_buttonCancel";
            this._buttonCancel.Size = new System.Drawing.Size(75, 23);
            this._buttonCancel.TabIndex = 11;
            this._buttonCancel.Text = "取消";
            this._buttonCancel.UseVisualStyleBackColor = true;
            this._buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // _buttonOK
            // 
            this._buttonOK.Location = new System.Drawing.Point(46, 217);
            this._buttonOK.Name = "_buttonOK";
            this._buttonOK.Size = new System.Drawing.Size(75, 23);
            this._buttonOK.TabIndex = 10;
            this._buttonOK.Text = "确定";
            this._buttonOK.UseVisualStyleBackColor = true;
            this._buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(203, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 12);
            this.label1.TabIndex = 12;
            this.label1.Text = "*";
            // 
            // DepartmentInformationInput
            // 
            this.AcceptButton = this._buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._buttonCancel;
            this.ClientSize = new System.Drawing.Size(225, 247);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._buttonOK);
            this.Controls.Add(this._buttonCancel);
            this.Controls.Add(this._rtxtRemarks);
            this.Controls.Add(this.label4);
            this.Controls.Add(this._txtEmail);
            this.Controls.Add(this.label3);
            this.Controls.Add(this._txtStaffCount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._txtTel);
            this.Controls.Add(this.labelTel);
            this.Controls.Add(this._txtName);
            this.Controls.Add(this.labelName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DepartmentInformationInput";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "部门信息录入";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TextBox _txtName;
        private System.Windows.Forms.TextBox _txtTel;
        private System.Windows.Forms.Label labelTel;
        private System.Windows.Forms.TextBox _txtStaffCount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox _txtEmail;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox _rtxtRemarks;
        private System.Windows.Forms.Button _buttonCancel;
        private System.Windows.Forms.Button _buttonOK;
        private System.Windows.Forms.Label label1;
    }
}