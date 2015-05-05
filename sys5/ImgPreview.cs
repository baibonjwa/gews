using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using LibBusiness;
using sys5;

namespace _5.WarningManagement
{
    public partial class ImgPreview : Form
    {
        private String imgFileName;
        private readonly PreWarningResultDetailsQuery _parent;
        private readonly String appPath = "";

        public ImgPreview(string imgFileName, PreWarningResultDetailsQuery parent)
        {
            _parent = parent;
            this.imgFileName = imgFileName;
            InitializeComponent();
            appPath = Application.StartupPath + "\\";
            ReadImage();
        }

        /// <summary>
        ///     将实际位置中的照片转化为byte[]类型写入数据库中
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
        ///     读取byte[]并转化为图片
        /// </summary>
        /// <param name="bytes">byte[]</param>
        /// <returns>Image</returns>
        private Image GetImageByBytes(byte[] bytes)
        {
            Image photo = null;
            using (var ms = new MemoryStream(bytes))
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
                    pictureBox1.Image =
                        GetImageByBytes(WarningImgBLL.GetImageWithWarningIdAndFileName(_parent.warningId, imgFileName));
                    pictureBox1.Show();
                }
            }
        }
    }
}