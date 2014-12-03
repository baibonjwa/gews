using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using LibBusiness;

namespace _8.EarlyWarningResult
{
    public partial class ImgPreview : Form
    {
        private String imgFileName;
        private String appPath = "";
        private EarlyWarningDetails _parent;

        public ImgPreview(string imgFileName, EarlyWarningDetails parent)
        {
            _parent = parent;
            this.imgFileName = imgFileName;
            InitializeComponent();
            appPath = Application.StartupPath + "\\";
            ReadImage();
        }

        /// <summary>
        /// 将实际位置中的照片转化为byte[]类型写入数据库中
        /// </summary>
        /// <param name="strFile">string图片地址</param>
        /// <returns>byte[]</returns>

        private void _btnPrevious_Click(object sender, EventArgs e)
        {
            imgFileName = _parent.PreviousImg();
            ReadImage();
            _lbFileName.Text = imgFileName;
        }

        private void _btnNext_Click(object sender, EventArgs e)
        {
            imgFileName = _parent.NextImg();
            ReadImage();
            _lbFileName.Text = imgFileName;
        }


        /// <summary>
        /// 读取byte[]并转化为图片
        /// </summary>
        /// <param name="bytes">byte[]</param>
        /// <returns>Image</returns>
        private Image GetImageByBytes(byte[] bytes)
        {
            Image photo = null;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                ms.Write(bytes, 0, bytes.Length);
                photo = Image.FromStream(ms, true);
            }

            return photo;
        }

        private void ReadImage()
        {
            if (!String.IsNullOrEmpty(imgFileName))
            {
                if (File.Exists(appPath + imgFileName))
                {
                    pictureBox1.ImageLocation = appPath + imgFileName;
                    pictureBox1.Show();
                    _lbFileName.Text = imgFileName;
                }
                else
                {
                    pictureBox1.Image = GetImageByBytes(WarningImgBLL.GetImageWithWarningIdAndFileName(_parent.warningId, imgFileName));
                    pictureBox1.Show();
                }
            }
        }
    }
}
