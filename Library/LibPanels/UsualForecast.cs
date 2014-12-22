// ******************************************************************
// 概  述：井下数据日常预测信息添加修改
// 作  者：宋英杰
// 创建日期：2014/3/11
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LibPanels
{
    public partial class UsualForecast : Form
    {
        public UsualForecast()
        {
            InitializeComponent();
        }
        //是否有顶板下沉
        public int isRoofDown;
        //是否支架变形与折损
        public int isSupportBroken;
        //是否煤壁片帮
        public int isCoalWallDrop;
        //是否局部冒顶
        public int isPartRoolFall;
        //是否顶板沿工作面煤壁切落（大冒顶）
        public int isBigRoofFall;

        private void rbtnRoofDownY_CheckedChanged(object sender, EventArgs e)
        {
            if(rbtnRoofDownY.Checked)
            isRoofDown = 1;
        }

        private void rbtnRoofDownN_CheckedChanged(object sender, EventArgs e)
        {
            if(rbtnRoofDownN.Checked)
            isRoofDown = 0;
        }

        private void rbtnSupportBrokenY_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnSupportBrokenY.Checked)
            isSupportBroken = 1;
        }

        private void rbtnSupportBrokenN_CheckedChanged(object sender, EventArgs e)
        {
            if(rbtnSupportBrokenN.Checked)
            isSupportBroken = 0;
        }

        private void rbtnCoalWallDropY_CheckedChanged(object sender, EventArgs e)
        {
            if(rbtnCoalWallDropY.Checked)
            isCoalWallDrop = 1;
        }

        private void rbtnCoalWallDropN_CheckedChanged(object sender, EventArgs e)
        {
            if(rbtnCoalWallDropN.Checked)
            isCoalWallDrop = 0;
        }

        private void rbtnPartRoofFallY_CheckedChanged(object sender, EventArgs e)
        {
            if(rbtnPartRoofFallY.Checked)
            isPartRoolFall = 1;
        }

        private void rbtnPartRoofFallN_CheckedChanged(object sender, EventArgs e)
        {
            if(rbtnPartRoofFallN.Checked)
            isPartRoolFall = 0;
        }

        private void rbtnBigRoofFallY_CheckedChanged(object sender, EventArgs e)
        {
            if(rbtnBigRoofFallY.Checked)
            isBigRoofFall = 1;
        }

        private void rbtnBigRoofFallN_CheckedChanged(object sender, EventArgs e)
        {
            if(rbtnBigRoofFallN.Checked)
            isBigRoofFall = 0;
        }
        public void bindDefaultValue()
        {
            if (isRoofDown == 1)
            {
                rbtnRoofDownY.Checked = true;
            }
            else
            {
                rbtnRoofDownN.Checked = true;
            }
            if (isSupportBroken == 1)
            {
                rbtnSupportBrokenY.Checked = true;
            }
            else
            {
                rbtnSupportBrokenN.Checked = true;
            }
            if (isCoalWallDrop == 1)
            {
                rbtnCoalWallDropY.Checked = true;
            }
            else
            {
                rbtnCoalWallDropN.Checked = true;
            }
            if (isPartRoolFall == 1)
            {
                rbtnPartRoofFallY.Checked = true;
            }
            else
            {
                rbtnPartRoofFallN.Checked = true;
            }
            if (isBigRoofFall == 1)
            {
                rbtnBigRoofFallY.Checked = true;
            }
            else
            {
                rbtnBigRoofFallN.Checked = true;
            }
        }
    }
}
