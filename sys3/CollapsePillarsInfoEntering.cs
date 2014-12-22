// ******************************************************************
// 概  述：陷落柱数据录入
// 作  者：宋英杰
// 创建日期：2013/11/28
// 版本号：1.0
//         废弃
// ******************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LibCommon;
using LibCommonControl;
using LibEntity;
using LibBusiness;
using LibSocket;

namespace _3.GeologyMeasure
{
    public partial class CollapsePillarsInfoEntering : MainFrm
    {
        public CollapsePillarsInfoEntering()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            //关闭窗体
            this.Close();
        }

        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            // 验证
            if (!this.check())
            {
                return;
            }
            // 创建陷落柱实体
            CollapsePillarsEntity collapsePillarsEntity = new CollapsePillarsEntity();
            //陷落柱名称
            collapsePillarsEntity.CollapsePillarsName = this.txtCollapsePillarName.Text;
            //位置X
            collapsePillarsEntity.CollapsePillarsLocationX = this.textLocationX.Text;
            //位置Y
            collapsePillarsEntity.CollapsePillarsLocationY = this.txtLocationY.Text;
            //位置Z
            collapsePillarsEntity.CollapsePillarsLocationZ = this.txtLocationZ.Text;
            //陷落柱长轴长
            collapsePillarsEntity.CollapsePillarsLongAxisLength = this.txtLongAxisLength.Text;
            //陷落柱短轴长
            collapsePillarsEntity.CollapsePillarsShortAxialLength = this.txtShortAxialLength.Text;
            //描述
            collapsePillarsEntity.CollapsePillarsDescribe = this.txtDescribe.Text;

            //陷落柱信息登录
            //bool bResult = CollapsePillarsInfoEnteringBLL.insertCollapsePillarsInfo(collapsePillarsEntity);
        }

        /// <summary>
        /// 验证画面入力数据
        /// </summary>
        /// <returns>验证结果：true 通过验证, false未通过验证</returns>
        private bool check()
        {
            // 判断陷落柱名称是否入力
            if (Validator.IsEmpty(this.txtCollapsePillarName.Text))
            {
                this.txtCollapsePillarName.BackColor = Const.ERROR_FIELD_COLOR;
                Alert.alert("陷落柱名称不能为空！");
                this.txtCollapsePillarName.Focus();
                return false;
            }
            else
            {
                this.txtCollapsePillarName.BackColor = Const.NO_ERROR_FIELD_COLOR;
            }
            //判断陷落柱位置X坐标是否入力
            if (Validator.IsEmpty(this.textLocationX.Text))
            {
                this.txtCollapsePillarName.BackColor = Const.ERROR_FIELD_COLOR;

                Alert.alert("陷落柱位置X坐标不能为空！");
                this.txtCollapsePillarName.Focus();
                return false;
            }
            else
            {
                this.txtCollapsePillarName.BackColor = Const.NO_ERROR_FIELD_COLOR;
            }
            //判断位置X是否为数字
            if (!Validator.IsNumeric(this.textLocationX.Text))
            {
                this.textLocationX.BackColor = Const.ERROR_FIELD_COLOR;
                Alert.alert("位置X为数字！");
                this.textLocationX.Focus();
                return false;
            }
            else
            {
                this.textLocationX.BackColor = Const.NO_ERROR_FIELD_COLOR;
            }
            //判断陷落柱位置Y坐标是否入力
            if (Validator.IsEmpty(this.txtLocationY.Text))
            {
                this.txtCollapsePillarName.BackColor = Const.ERROR_FIELD_COLOR;
                Alert.alert("陷落柱位置Y坐标不能为空！");
                this.txtCollapsePillarName.Focus();
                return false;
            }
            else
            {
                this.txtCollapsePillarName.BackColor = Const.NO_ERROR_FIELD_COLOR;
            }
            //判断位置Y是否为数字
            if (!Validator.IsNumeric(this.txtLocationY.Text))
            {
                this.txtLocationY.BackColor = Const.ERROR_FIELD_COLOR;
                Alert.alert("位置Y为数字！");
                this.txtLocationY.Focus();
                return false;
            }
            else
            {
                this.txtLocationY.BackColor = Const.NO_ERROR_FIELD_COLOR;
            }
            //判断陷落柱位置Z坐标是否入力
            if (Validator.IsEmpty(this.txtLocationZ.Text))
            {
                this.txtCollapsePillarName.BackColor = Const.ERROR_FIELD_COLOR;
                Alert.alert("陷落柱位置Z坐标不能为空！");
                this.txtCollapsePillarName.Focus();
                return false;
            }
            else
            {
                this.txtCollapsePillarName.BackColor = Const.NO_ERROR_FIELD_COLOR;
            }
            //判断位置Z是否为数字
            if (!Validator.IsNumeric(this.txtLocationZ.Text))
            {
                this.txtLocationZ.BackColor = Const.ERROR_FIELD_COLOR;
                Alert.alert("位置Y为数字！");
                this.txtLocationZ.Focus();
                return false;
            }
            else
            {
                this.txtLocationZ.BackColor = Const.NO_ERROR_FIELD_COLOR;
            }
            //判断陷落柱长轴长是否入力
            if (Validator.IsEmpty(this.txtLongAxisLength.Text))
            {
                this.txtCollapsePillarName.BackColor = Const.ERROR_FIELD_COLOR;
                Alert.alert("陷落柱长轴长不能为空！");
                this.txtCollapsePillarName.Focus();
                return false;
            }
            else
            {
                this.txtCollapsePillarName.BackColor = Const.NO_ERROR_FIELD_COLOR;
            }
            //判断陷落柱长轴长是否为数字
            if (!Validator.IsNumeric(this.txtLongAxisLength.Text))
            {
                this.txtLongAxisLength.BackColor = Const.ERROR_FIELD_COLOR;
                Alert.alert("位置Y为数字！");
                this.txtLongAxisLength.Focus();
                return false;
            }
            else
            {
                this.txtLongAxisLength.BackColor = Const.NO_ERROR_FIELD_COLOR;
            }
            //判断陷落柱短轴长是否入力
            if (Validator.IsEmpty(this.txtShortAxialLength.Text))
            {
                this.txtCollapsePillarName.BackColor = Const.ERROR_FIELD_COLOR;
                Alert.alert("陷落柱短轴长不能为空！");
                this.txtCollapsePillarName.Focus();
                return false;
            }
            else
            {
                this.txtCollapsePillarName.BackColor = Const.NO_ERROR_FIELD_COLOR;
            }
            //判断陷落柱短轴长是否为数字
            if (!Validator.IsNumeric(this.txtShortAxialLength.Text))
            {
                this.txtShortAxialLength.BackColor = Const.ERROR_FIELD_COLOR;
                Alert.alert("位置Y为数字！");
                this.txtShortAxialLength.Focus();
                return false;
            }
            else
            {
                this.txtShortAxialLength.BackColor = Const.NO_ERROR_FIELD_COLOR;
            }
            //验证通过
            return true;
        }
        private void SendMessengToServer()
        {
            Log.Debug("更新服务端断层Map------开始");
            // 通知服务端回采进尺已经添加
            GeologyMsg msg = new GeologyMsg(0, 0, "", DateTime.Now, COMMAND_ID.UPDATE_GEOLOG_DATA);
            this.SendMsg2Server(msg);
            Log.Debug("服务端断层Map------完成" + msg.ToString());
        }
    }
}
