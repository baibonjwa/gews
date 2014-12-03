using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using FarPoint.Win.Spread.CellType.BarCode;
using LibBusiness;
using LibCommon;
using LibEntity;
using ImageFormat = System.Drawing.Imaging.ImageFormat;

namespace _5.WarningManagement
{
    public partial class UploadImg : Form
    {

        /// <summary>
        /// 获取系统安装的摄像头列表
        /// </summary>
        /// <returns>返回格式：0|USB视频设备|1|USB摄像头，其中0或1是摄像头ID</returns>
        [DllImport("VedioCapture.dll")]
        private extern static string GetCameraList();
        /// <summary>
        /// 设置打开的摄像头ID
        /// </summary>
        /// <param name="value">GetCameraList中列举的摄像头ID</param>
        /// <returns>返回0，设置成功</returns>
        [DllImport("VedioCapture.dll")]
        private extern static int SetCameraID(int value);
        /// <summary>
        /// 打开SetCameraID中设置的摄像头
        /// </summary>
        /// <param name="Handle">要显示视频的控件</param>
        /// <returns>摄像头句柄</returns>
        [DllImport("VedioCapture.dll")]
        private extern static IntPtr StartCamera(IntPtr Handle);
        /// <summary>
        /// 关闭摄像头
        /// </summary>
        /// <param name="hWndC">摄像头句柄</param>
        /// <returns>返回0，设置成功</returns>
        [DllImport("VedioCapture.dll")]
        private extern static int CloseCamera(IntPtr hWndC);
        /// <summary>
        /// 截取BMP格式的视频图像
        /// </summary>
        /// <param name="hWndC">摄像头句柄</param>
        /// <param name="cFileName">截取的bmp文件名，或则-1（代表内存图片）</param>
        /// <returns>如果cFileName传入文件名，返回0，截取成功；如果cFileName传入-1，返回bmp图片的句柄（请使用后注意释放）</returns>
        [DllImport("VedioCapture.dll")]
        private extern static int SaveBmp(IntPtr hWndC, string cFileName);
        /// <summary>
        /// 开始录制视频
        /// </summary>
        /// <param name="hWndC">摄像头句柄</param>
        /// <param name="cFileName">视频文件名</param>
        /// <returns>返回0，开始录制</returns>
        [DllImport("VedioCapture.dll")]
        private extern static int RecordVideo(IntPtr hWndC, string cFileName);
        /// <summary>
        /// 停止录制视频
        /// </summary>
        /// <param name="hWndC">摄像头句柄</param>
        /// <returns>返回0，停止成功</returns>
        [DllImport("VedioCapture.dll")]
        private extern static int StopRecord(IntPtr hWndC);
        /// <summary>
        /// 将现有的图片文件制作成视频，仅支持bmp和jpg格式的图片
        /// </summary>
        /// <param name="cPath">图片文件所在路径，以/结尾</param>
        /// <param name="cFileName">制作后的视频文件名</param>
        /// <param name="cFileType">图片类型：jpg或bmp</param>
        /// <param name="FrameRate">视频写入帧率，摄像头常用15帧，</param>
        /// <returns>返回0，制作成功</returns>
        [DllImport("VedioCapture.dll")]
        private extern static int CreateVideoByFiles(string cPath, string cFileName, string cFileType, int FrameRate);

        string AppPath = "";
        string CameraID = "";
        string CameraName = "";
        private PreWarningResultDetailsQuery _parent;

        public UploadImg(PreWarningResultDetailsQuery parent)
        {
            _parent = parent;
            InitializeComponent();
            AppPath = Application.StartupPath + "\\";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dgvList.Rows.Clear();
            string[] strdev = GetCameraList().Split('|');
            if (strdev.Length < 2) return;
            //0|USB视频设备|1|USB摄像头，其中0或1是摄像头ID
            int rowindex = 0;
            for (int i = 0; i < strdev.Length && strdev[i].ToString() != ""; i++)
            {
                if (i % 2 == 0)
                {
                    //id
                    dgvList.Rows.Add();
                    dgvList[0, rowindex].Value = strdev[i].ToString();
                }
                else
                {
                    dgvList[1, rowindex].Value = strdev[i].ToString();
                    rowindex++;
                }
            }
        }
        IntPtr camerah = IntPtr.Zero;
        private void button2_Click(object sender, EventArgs e)
        {
            if (dgvList.Rows.Count == 0) return;
            int selectindex = Convert.ToInt32(dgvList[0, dgvList.CurrentCell.RowIndex].Value.ToString());


            int Result = SetCameraID(selectindex);
            if (Result != 0)
            {
                MessageBox.Show("设备打开失败");
                return;
            }
            camerah = StartCamera(panel1.Handle);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //int imgCount = PreWarningResultDetailsQuery.ImgList.Count;
            try
            {
                String fileName = SaveBmp();
                if (!String.IsNullOrEmpty(fileName))
                {
                    PreWarningResultDetailsQuery.ImgList.Add(fileName + ".jpg");
                    WarningImgEnt ent = new WarningImgEnt();
                    ent.FileName = fileName + ".jpg";
                    ent.Remarks = "";
                    ent.WarningId = _parent.warningId;
                    ent.Img = _parent.GetBytesByImagePath(fileName + ".jpg");
                    if (!WarningImgBLL.IsRepeatWithWarningIdAndFileName(_parent.warningId, ent.FileName))
                    {
                        WarningImgBLL.InsertWarningImg(ent);
                    }
                }
            }
            catch (Exception)
            {
                Alert.alert("请先打开高拍仪，或检查高拍仪连接是否正确");
            }     
        }

        public string SaveBmp()
        {
            if (camerah != IntPtr.Zero)
            {
                String fileName = DateTime.Now.ToString("yyyyMMddHHmmss");
                SaveBmp(camerah, AppPath + fileName + ".bmp");
                //传入-1，返回内存图片
                IntPtr iptr = (IntPtr)SaveBmp(camerah, "-1");
                Bitmap bmp = Bitmap.FromHbitmap(iptr);
                bmp.Save(AppPath + fileName + ".jpg", ImageFormat.Jpeg);
                bmp.Dispose();
                return fileName;
            }
            else
            {
                return "";
            }
        }

        private void UploadImg_FormClosed(object sender, FormClosedEventArgs e)
        {
            CloseCamera(camerah);
            _parent.RefreshImgListFromDb();
        }
    }
}
