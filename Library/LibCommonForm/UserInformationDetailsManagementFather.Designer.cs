namespace LibCommonForm
{
    partial class UserInformationDetailsManagementFather
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
            this.panelFather = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // panelFather
            // 
            this.panelFather.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFather.Location = new System.Drawing.Point(0, 0);
            this.panelFather.Name = "panelFather";
            this.panelFather.Size = new System.Drawing.Size(840, 437);
            this.panelFather.TabIndex = 0;
            // 
            // UserInformationDetailsManagementFather
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(840, 437);
            this.Controls.Add(this.panelFather);
            this.KeyPreview = true;
            this.Name = "UserInformationDetailsManagementFather";
            this.Text = "UserInformationDetailsManagementFather";
            this.Load += new System.EventHandler(this.UserInformationDetailsManagementFather_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.UserInformationDetailsManagementFather_KeyPress);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelFather;
    }
}