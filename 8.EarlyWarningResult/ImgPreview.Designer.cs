namespace _8.EarlyWarningResult
{
    partial class ImgPreview
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this._btnPrevious = new System.Windows.Forms.Button();
            this._btnNext = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this._lbFileName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(640, 480);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // _btnPrevious
            // 
            this._btnPrevious.Location = new System.Drawing.Point(455, 504);
            this._btnPrevious.Name = "_btnPrevious";
            this._btnPrevious.Size = new System.Drawing.Size(75, 23);
            this._btnPrevious.TabIndex = 1;
            this._btnPrevious.Text = "上一张";
            this._btnPrevious.UseVisualStyleBackColor = true;
            this._btnPrevious.Click += new System.EventHandler(this._btnPrevious_Click);
            // 
            // _btnNext
            // 
            this._btnNext.Location = new System.Drawing.Point(555, 504);
            this._btnNext.Name = "_btnNext";
            this._btnNext.Size = new System.Drawing.Size(75, 23);
            this._btnNext.TabIndex = 1;
            this._btnNext.Text = "下一张";
            this._btnNext.UseVisualStyleBackColor = true;
            this._btnNext.Click += new System.EventHandler(this._btnNext_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 509);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "文件名称：";
            // 
            // _lbFileName
            // 
            this._lbFileName.AutoSize = true;
            this._lbFileName.Location = new System.Drawing.Point(70, 509);
            this._lbFileName.Name = "_lbFileName";
            this._lbFileName.Size = new System.Drawing.Size(0, 12);
            this._lbFileName.TabIndex = 3;
            // 
            // ImgPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(667, 539);
            this.Controls.Add(this._lbFileName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._btnNext);
            this.Controls.Add(this._btnPrevious);
            this.Controls.Add(this.pictureBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ImgPreview";
            this.Text = "预览";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button _btnPrevious;
        private System.Windows.Forms.Button _btnNext;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label _lbFileName;
    }
}