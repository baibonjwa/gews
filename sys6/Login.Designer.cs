namespace UnderTerminal
{
    partial class Login
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.btnSure = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tbPeople = new System.Windows.Forms.TextBox();
            this.tbCode = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnSure
            // 
            this.btnSure.Font = new System.Drawing.Font("SimSun", 12F);
            this.btnSure.Location = new System.Drawing.Point(280, 351);
            this.btnSure.Name = "btnSure";
            this.btnSure.Size = new System.Drawing.Size(92, 30);
            this.btnSure.TabIndex = 2;
            this.btnSure.Text = "登录";
            this.btnSure.UseVisualStyleBackColor = true;
            this.btnSure.Click += new System.EventHandler(this.btnSure_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("SimSun", 12F);
            this.btnCancel.Location = new System.Drawing.Point(420, 351);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(92, 30);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "清除";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tbPeople
            // 
            this.tbPeople.Font = new System.Drawing.Font("SimSun", 12F);
            this.tbPeople.Location = new System.Drawing.Point(340, 261);
            this.tbPeople.Name = "tbPeople";
            this.tbPeople.Size = new System.Drawing.Size(146, 26);
            this.tbPeople.TabIndex = 0;
            // 
            // tbCode
            // 
            this.tbCode.Font = new System.Drawing.Font("SimSun", 12F);
            this.tbCode.Location = new System.Drawing.Point(340, 306);
            this.tbCode.Name = "tbCode";
            this.tbCode.PasswordChar = '*';
            this.tbCode.Size = new System.Drawing.Size(146, 26);
            this.tbCode.TabIndex = 1;
            // 
            // Login
            // 
            this.AcceptButton = this.btnSure;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.tbCode);
            this.Controls.Add(this.tbPeople);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSure);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "欢    迎";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Login_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSure;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox tbPeople;
        private System.Windows.Forms.TextBox tbCode;
    }
}

