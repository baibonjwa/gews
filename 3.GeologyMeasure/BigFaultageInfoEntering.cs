// ******************************************************************
// 概  述：推断断层数据录入
// 作  者：伍鑫
// 创建日期：2013/11/30
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraTab.Buttons;
using GIS;
using LibBusiness;
using System.Security.Policy;
using System.Collections;
using LibEntity;
using LibCommon;
using GIS.Common;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using GIS.HdProc;

namespace _3.GeologyMeasure
{
    public partial class BigFaultageInfoEntering : Form
    {
        /** 主键  **/
        private int _iPK;
        /** 业务逻辑类型：添加/修改  **/
        private string _bllType = "add";

        private BigFaultage bigFaultageEntity = new BigFaultage();
        private List<BigFaultagePoint> pointList = new List<BigFaultagePoint>();
        private BigFaultageInfoManagement form;

        /// <summary>
        /// 构造方法
        /// </summary>
        public BigFaultageInfoEntering(BigFaultageInfoManagement form)
        {
            InitializeComponent();
            this.form = form;
            // 设置窗体默认属性
            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_GM.INSERT_BIG_FAULTAGE_INFO);
        }

        /// <summary
        /// 带参数的构造方法
        /// </summary>
        /// <param name="strPrimaryKey">主键</param>
        public BigFaultageInfoEntering(BigFaultageInfoManagement form, string strPrimaryKey)
        {
            InitializeComponent();
            this.form = form;
            // 主键
            int iPK = 0;
            if (int.TryParse(strPrimaryKey, out iPK))
            {
                // 主键
                this._iPK = iPK;

                // 设置窗体默认属性
                FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_GM.INSERT_BIG_FAULTAGE_INFO);

                // 设置业务类型
                this._bllType = "update";

                BigFaultageBLL.selectBigfaultageId(strPrimaryKey, bigFaultageEntity, pointList);

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

                foreach (var i in pointList)
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
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = @"C:\Desktop\推断断层";
            ofd.RestoreDirectory = true;
            ofd.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
            ofd.Multiselect = true;
            //ofd.ShowDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string aa = ofd.FileName;
                string[] strs = System.IO.File.ReadAllLines(aa, Encoding.GetEncoding("GB2312"));
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
            BigFaultage bigFaultage = new BigFaultage();
            List<BigFaultagePoint> bigFaultagePoingList = new List<BigFaultagePoint>();
            bool bResult = false;
            if (_bllType == "add")
            {
                bigFaultage.FaultageName = tbFaultageName.Text;
                bigFaultage.Gap = tbGap.Text;
                bigFaultage.Angle = tbAngle.Text;
                bigFaultage.Trend = tbTrend.Text;
                bigFaultage.Type = rbtnFrontFaultage.Checked == true ? "正断层" : "逆断层";
                bigFaultage.BindingId =
                    IDGenerator.NewBindingID();
                for (int i = 0; i < dgrdvUp.Rows.Count; i++)
                {
                    BigFaultagePoint point = new BigFaultagePoint();
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
                    BigFaultagePoint point = new BigFaultagePoint();
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

                bResult = BigFaultageBLL.insertBigFaultageInfo(bigFaultage, bigFaultagePoingList);
                if (bResult)
                {
                    try
                    {
                        string title = bigFaultage.FaultageName + "  " + "∠=" + bigFaultage.Angle + "  H=" + bigFaultage.Gap;
                        DrawBigFaultageInfo.DrawTddc(title, bigFaultagePoingList, bigFaultage.BindingId);
                    }
                    catch (Exception)
                    {
                        bResult = false;
                    }

                }
            }
            else if (_bllType == "update")
            {
                bigFaultage.FaultageId = bigFaultageEntity.FaultageId;
                bigFaultage.BindingId = bigFaultageEntity.BindingId;
                bigFaultage.FaultageName = tbFaultageName.Text;
                bigFaultage.Gap = tbGap.Text;
                bigFaultage.Angle = tbAngle.Text;
                bigFaultage.Trend = tbTrend.Text;
                bigFaultage.Type = rbtnFrontFaultage.Checked == true ? "正断层" : "逆断层";

                bResult = BigFaultageBLL.updateBigFaultageInfo(bigFaultage, pointList);
            }
            form.loadBigFaultageInfo();
            Alert.alert(bResult ? "提交成功" : "提交失败");
            if (bResult == true)
            {
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

