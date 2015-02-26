using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using GIS;
using LibCommon;
using LibCommonControl;
using LibEntity;

namespace sys3
{
    public partial class BigFaultageInfoEntering : Form
    {
        /** 主键  **/
        /** 业务逻辑类型：添加/修改  **/
        private readonly string _bllType = "add";

        private BigFaultage bigFaultageEntity = new BigFaultage();

        /// <summary>
        ///     构造方法
        /// </summary>
        public BigFaultageInfoEntering()
        {
            InitializeComponent();
            // 设置窗体默认属性
            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_GM.INSERT_BIG_FAULTAGE_INFO);
        }

        /// <summary
        ///     带参数的构造方法
        /// </summary>
        /// <param name="strPrimaryKey">主键</param>
        public BigFaultageInfoEntering(string strPrimaryKey)
        {
            InitializeComponent();
            // 主键

            // 设置窗体默认属性
            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_GM.INSERT_BIG_FAULTAGE_INFO);

            // 设置业务类型
            _bllType = "update";

            bigFaultageEntity = BigFaultage.Find(strPrimaryKey);
            BigFaultagePoint[] pointList = BigFaultagePoint.FindAllByFaultageId(Convert.ToInt32(strPrimaryKey));

            tbFaultageName.Text = bigFaultageEntity.FaultageName;
            tbGap.Text = bigFaultageEntity.Gap;
            tbAngle.Text = bigFaultageEntity.Angle;
            tbTrend.Text = bigFaultageEntity.Trend;

            if (bigFaultageEntity.Type == "正断层")
            {
                rbtnFrontFaultage.Checked = true;
                rbtnOppositeFaultage.Checked = false;
            }
            else
            {
                rbtnFrontFaultage.Checked = false;
                rbtnOppositeFaultage.Checked = true;
            }

            foreach (BigFaultagePoint i in pointList)
            {
                if (i.UpOrDown == "上盘")
                {
                    dgrdvUp.Rows.Add(i.CoordinateX, i.CoordinateY, i.CoordinateZ);
                }
                else
                {
                    dgrdvDown.Rows.Add(i.CoordinateX, i.CoordinateY, i.CoordinateZ);
                }
            }
        }

        private void btnReadTxt_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.InitialDirectory = @"C:\Desktop\推断断层";
            ofd.RestoreDirectory = true;
            ofd.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
            ofd.Multiselect = true;
            //ofd.ShowDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string aa = ofd.FileName;
                string[] strs = File.ReadAllLines(aa, Encoding.GetEncoding("GB2312"));
                string type = "";
                for (int i = 0; i < strs.Length; i++)
                {
                    if (i == 0)
                    {
                        string[] line1 = strs[i].Split('|');
                        tbFaultageName.Text = line1[0];
                        tbGap.Text = line1[1];
                        if (line1[2] == "正断层")
                        {
                            rbtnFrontFaultage.Checked = true;
                            rbtnOppositeFaultage.Checked = false;
                        }
                        else if (line1[2] == "逆断层")
                        {
                            rbtnFrontFaultage.Checked = false;
                            rbtnOppositeFaultage.Checked = true;
                        }
                        tbAngle.Text = line1[3];
                    }
                    if (strs[i] == "上盘")
                    {
                        type = "上盘";
                        continue;
                    }
                    if (strs[i] == "下盘")
                    {
                        type = "下盘";
                        continue;
                    }
                    if (strs[i].Equals(""))
                    {
                        continue;
                    }
                    if (type == "上盘")
                    {
                        dgrdvUp.Rows.Add(strs[i].Split(',')[0], strs[i].Split(',')[1], "0");
                    }
                    if (type == "下盘")
                    {
                        dgrdvDown.Rows.Add(strs[i].Split(',')[0], strs[i].Split(',')[1], "0");
                    }
                }
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            var bigFaultage = new BigFaultage();
            var bigFaultagePoingList = new List<BigFaultagePoint>();
            switch (_bllType)
            {
                case "add":
                    bigFaultage.FaultageName = tbFaultageName.Text;
                    bigFaultage.Gap = tbGap.Text;
                    bigFaultage.Angle = tbAngle.Text;
                    bigFaultage.Trend = tbTrend.Text;
                    bigFaultage.Type = rbtnFrontFaultage.Checked ? "正断层" : "逆断层";
                    bigFaultage.BindingId =
                        IDGenerator.NewBindingID();
                    for (int i = 0; i < dgrdvUp.Rows.Count; i++)
                    {
                        var point = new BigFaultagePoint();
                        point.UpOrDown = "上盘";
                        if (dgrdvUp.Rows[i].Cells[0].Value != null)
                        {
                            point.CoordinateX = Convert.ToDouble(dgrdvUp.Rows[i].Cells[0].Value);
                            point.CoordinateY = Convert.ToDouble(dgrdvUp.Rows[i].Cells[1].Value);
                            point.CoordinateZ = Convert.ToDouble(dgrdvUp.Rows[i].Cells[2].Value);
                            point.Bid = IDGenerator.NewBindingID();
                            bigFaultagePoingList.Add(point);
                        }
                    }
                    for (int i = 0; i < dgrdvDown.Rows.Count; i++)
                    {
                        var point = new BigFaultagePoint();
                        if (dgrdvDown.Rows[i].Cells[0].Value != null)
                        {
                            point.UpOrDown = "下盘";
                            point.CoordinateX = Convert.ToDouble(dgrdvDown.Rows[i].Cells[0].Value);
                            point.CoordinateY = Convert.ToDouble(dgrdvDown.Rows[i].Cells[1].Value);
                            point.CoordinateZ = Convert.ToDouble(dgrdvDown.Rows[i].Cells[2].Value);
                            point.Bid = IDGenerator.NewBindingID();
                            bigFaultagePoingList.Add(point);
                        }
                    }
                    foreach (var bigFaultagePoint in bigFaultagePoingList)
                    {
                        bigFaultagePoint.Save();
                    }
                    bigFaultage.Save();

                    string title = bigFaultage.FaultageName + "  " + "∠=" + bigFaultage.Angle + "  H=" +
                                   bigFaultage.Gap;
                    DrawBigFaultageInfo.DrawTddc(title, bigFaultagePoingList, bigFaultage.BindingId);
                    break;
                case "update":
                    bigFaultage.FaultageId = bigFaultageEntity.FaultageId;
                    bigFaultage.BindingId = bigFaultageEntity.BindingId;
                    bigFaultage.FaultageName = tbFaultageName.Text;
                    bigFaultage.Gap = tbGap.Text;
                    bigFaultage.Angle = tbAngle.Text;
                    bigFaultage.Trend = tbTrend.Text;
                    bigFaultage.Type = rbtnFrontFaultage.Checked ? "正断层" : "逆断层";
                    foreach (var bigFaultagePoint in bigFaultagePoingList)
                    {
                        bigFaultagePoint.Save();
                    }
                    bigFaultage.Save();
                    break;
            }
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}