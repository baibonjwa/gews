using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Castle.ActiveRecord;
using GIS;
using LibCommon;
using LibEntity;

namespace sys3
{
    public partial class BigFaultageInfoEntering : Form
    {
        /// <summary>
        ///     构造方法
        /// </summary>
        public BigFaultageInfoEntering()
        {
            InitializeComponent();
            // 设置窗体默认属性
            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_GM.INSERT_BIG_FAULTAGE_INFO);
        }

        public BigFaultageInfoEntering(BigFaultage bigFaultage)
        {
            InitializeComponent();
            // 主键
            using (new SessionScope())
            {
                // 设置窗体默认属性
                FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_GM.INSERT_BIG_FAULTAGE_INFO);

                tbFaultageName.Text = bigFaultage.BigFaultageName;
                tbGap.Text = bigFaultage.Gap;
                tbAngle.Text = bigFaultage.Angle;
                tbTrend.Text = bigFaultage.Trend;

                if (bigFaultage.Type == "正断层")
                {
                    rbtnFrontFaultage.Checked = true;
                    rbtnOppositeFaultage.Checked = false;
                }
                else
                {
                    rbtnFrontFaultage.Checked = false;
                    rbtnOppositeFaultage.Checked = true;
                }

                foreach (BigFaultagePoint i in bigFaultage.BigFaultagePoints)
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
        }

        private void btnReadTxt_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog
            {
                RestoreDirectory = true,
                Filter = @"文本文件(*.txt)|*.txt|所有文件(*.*)|*.*",
                Multiselect = true
            };
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
            var bigFaultage = BigFaultage.FindOneByBigFaultageName(tbFaultageName.Text);
            var bigFaultagePoingList = new List<BigFaultagePoint>();
            if (bigFaultage == null)
            {
                bigFaultage = new BigFaultage
                {
                    BigFaultageName = tbFaultageName.Text,
                    Gap = tbGap.Text,
                    Angle = tbAngle.Text,
                    Trend = tbTrend.Text,
                    Type = rbtnFrontFaultage.Checked ? "正断层" : "逆断层",
                    BindingId = IDGenerator.NewBindingID()
                };
                for (var i = 0; i < dgrdvUp.Rows.Count; i++)
                {
                    var point = new BigFaultagePoint { UpOrDown = "上盘" };
                    if (dgrdvUp.Rows[i].Cells[0].Value == null) continue;
                    point.CoordinateX = Convert.ToDouble(dgrdvUp.Rows[i].Cells[0].Value);
                    point.CoordinateY = Convert.ToDouble(dgrdvUp.Rows[i].Cells[1].Value);
                    point.CoordinateZ = Convert.ToDouble(dgrdvUp.Rows[i].Cells[2].Value);
                    point.Bid = IDGenerator.NewBindingID();
                    bigFaultagePoingList.Add(point);
                }
                for (var i = 0; i < dgrdvDown.Rows.Count; i++)
                {
                    var point = new BigFaultagePoint();
                    if (dgrdvDown.Rows[i].Cells[0].Value == null) continue;
                    point.UpOrDown = "下盘";
                    point.CoordinateX = Convert.ToDouble(dgrdvDown.Rows[i].Cells[0].Value);
                    point.CoordinateY = Convert.ToDouble(dgrdvDown.Rows[i].Cells[1].Value);
                    point.CoordinateZ = Convert.ToDouble(dgrdvDown.Rows[i].Cells[2].Value);
                    point.Bid = IDGenerator.NewBindingID();
                    bigFaultagePoingList.Add(point);
                }
                bigFaultage.Save();
                var title = bigFaultage.BigFaultageName + "  " + bigFaultage.Angle + "  " +
                               bigFaultage.Gap;
                DrawBigFaultageInfo.DrawTddc(title, bigFaultagePoingList, bigFaultage.BindingId);
            }
            else
            {
                bigFaultage.BigFaultageName = tbFaultageName.Text;
                bigFaultage.Gap = tbGap.Text;
                bigFaultage.Angle = tbAngle.Text;
                bigFaultage.Trend = tbTrend.Text;
                bigFaultage.Type = rbtnFrontFaultage.Checked ? "正断层" : "逆断层";
                foreach (var bigFaultagePoint in bigFaultagePoingList)
                {
                    bigFaultagePoint.Save();
                }
                bigFaultage.Save();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnReadMultTxt_Click(object sender, EventArgs e)
        {

        }
    }
}