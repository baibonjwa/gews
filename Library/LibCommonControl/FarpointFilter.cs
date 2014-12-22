// ******************************************************************
// 概  述：
// 作  者：
// 创建日期：
// 版本号：V1.0
// 版本信息:
// V1.0 新建
// ******************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LibCommon;

namespace LibCommonControl
{
    public partial class FarpointFilter : UserControl
    {
        #region 事件
        public delegate void CheckHideEventHandler(object sender, EventArgs arg);
        //Checkbox值更改事件
        public event CheckHideEventHandler OnCheckFilterChanged;

        //符合条件规则颜色按钮事件
        public delegate void ClickFitColorBtnEventHandler(object sender, EventArgs arg);
        //正确获取符合条件颜色事件
        public event ClickFitColorBtnEventHandler OnClickFitColorBtnOK;

        //不符合条件规则颜色按钮事件
        public delegate void ClickNotFitColorBtnEventHandler(object sender, EventArgs arg);
        //正确获取不符合条件颜色事件
        public event ClickFitColorBtnEventHandler OnClickNotFitColorBtnOK;

        //清空过滤选项按钮事件
        public delegate void ClickClearFilterBtnEventHandler(object sender, EventArgs arg);
        public event ClickClearFilterBtnEventHandler OnClickClearFilterBtn;
        #endregion


        //筛选/过滤符合条件单元格颜色
        private Color _fitColor = Const.FIT_COLOR;

        /// <summary>
        /// 获取用户选择的符合过滤条件的颜色
        /// </summary>
        /// <returns></returns>
        public Color GetSelectedFitColor()
        {
            return _fitColor;
        }
        //筛选/过滤不符合条件单元格颜色
        private Color _notFitColor = Const.NOT_FIT_COLOR;
        /// <summary>
        /// 获取用户选择的不符合条件的颜色
        /// </summary>
        /// <returns></returns>
        public Color GetSelectedNotFitColor()
        {
            return _notFitColor;
        }

        public FarpointFilter()
        {
            InitializeComponent();
            //设置界面中Lable初始颜色
            this.btnFitColor.BackColor = Const.FIT_COLOR;
            this.btnNotFitColor.BackColor = Const.NOT_FIT_COLOR;
        }

        private void chkHideUnfiltered_CheckedChanged(object sender, EventArgs e)
        {
            OnCheckFilterChanged(sender, e);
        }

        private void btnFitColor_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == clrDlg.ShowDialog())
            {
                this._fitColor = clrDlg.Color;
                this.btnFitColor.BackColor = clrDlg.Color;
                OnClickFitColorBtnOK(sender, e);
            }
        }

        private void btnNotFitColor_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == clrDlg.ShowDialog())
            {
                this._notFitColor = clrDlg.Color;
                this.btnNotFitColor.BackColor = clrDlg.Color;
                OnClickNotFitColorBtnOK(sender, e);
            }
        }

        private void btnClearFilter_Click(object sender, EventArgs e)
        {
            OnClickClearFilterBtn(sender, e);
        }

        public void EnableChooseColorCtrls(bool enable)
        {
            this.lblFitColor.Enabled = enable;
            this.lblNotFitColor.Enabled = enable;
            this.btnFitColor.Enabled = enable;
            this.btnNotFitColor.Enabled = enable;
        }
    }
}
