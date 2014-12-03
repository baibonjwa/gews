// ******************************************************************
// 概  述：人员详细信息录入
// 作  者：秦凯
// 创建日期：2014/03/07
// 版本号：V1.0
// 版本信息:
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
using LibEntity;
using LibBusiness;
using LibCommon;

namespace LibCommonForm
{
    public partial class UserInformationDetailsInput : Form
    {
        //定义字符串，表达添加或修改
        string _strIsAddOrModify = "add";

        /// <summary>
        /// 传入实体，用于实例化时判断为添加、修改。若实体为空，则认为是添加。
        /// </summary>
        /// <param name="ent">用户详细信息实体</param>
        public UserInformationDetailsInput(UserInformationDetailsEnt ent)
        {
            InitializeComponent();

            //添加部门名称
            SetComboxDepartment();

            if (ent == null)
            {
                //标记字符，用于告知”确定“执行何种操作
                _strIsAddOrModify = "add";
                //设置窗体格式
                LibCommon.FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, LibCommon.LibFormTitles.USER_INFO_DETAILS_ADD);
            }
            else
            {
                //姓名
                _txtName.Text = ent.Name;
                //手机号码
                _txtPhoneNumber.Text = ent.PhoneNumber;
                //电话
                _txtTel.Text = ent.TelePhoneNumber;
                //邮箱
                _txtEmail.Text = ent.Email;
                //部门
                if (_cboDepartment.Items.Count != 0)//解决部门中无值时的报错问题
                {
                    _cboDepartment.SelectedIndex = GetDepartmentIndex(ent.Depratment);
                }
                //职位
                _txtPosition.Text = ent.Position;
                //备注
                _rtxtRemark.Text = ent.Remarks;

                if (ent.IsInform == 0)
                {
                    rbtnInformNo.Checked = true;
                    rbtnInformYes.Checked = false;
                }
                else
                {
                    rbtnInformNo.Checked = false;
                    rbtnInformYes.Checked = true;
                }

                //标记字符，用于告知”确定“执行何种操作
                _strIsAddOrModify = "modify";
                //设置窗体格式
                LibCommon.FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, LibCommon.LibFormTitles.USER_INFO_DETAILS_MOD);
            }
        }

        /// <summary>
        /// 添加部门名称中的项
        /// </summary>
        private void SetComboxDepartment()
        {
            //清空_cbxDepartment中部门名称的值
            _cboDepartment.Items.Clear();

            string[] depts = UserInformationDetailsManagementBLL.GetDepartmentNames();
            //需判断是否为空
            if (depts != null)
            {
                foreach (string dept in depts)
                {
                    //逐行添加部门名称
                    _cboDepartment.Items.Add(dept);
                }
            }
        }

        /// <summary>
        /// 获取部门名称在部门名称组中的索引，用于“修改”命令中，旧值的导入
        /// </summary>
        /// <param name="value"></param>
        /// <returns>部门名称的索引</returns>
        private int GetDepartmentIndex(string value)
        {
            int returnNumber = 0;
            foreach (object str in _cboDepartment.Items)
            {
                if (str.ToString() == value)
                {
                    return returnNumber;
                }
                returnNumber++;
            }
            //解决若不存在该用户名时报错的问题
            if (_cboDepartment.Items.Count == returnNumber)
            {
                return -1;
            }
            return returnNumber;
        }

        /// <summary>
        /// 点击确定按钮出发的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            //定义接收界面信息的实体
            UserInformationDetailsEnt ent = new UserInformationDetailsEnt();
            //姓名
            ent.Name = _txtName.Text;
            //手机号码
            ent.PhoneNumber = _txtPhoneNumber.Text;
            //电话号码
            ent.TelePhoneNumber = _txtTel.Text;
            //邮箱
            ent.Email = _txtEmail.Text;

            ent.IsInform = rbtnInformNo.Checked == true ? 0 : 1;
            //部门
            if (_cboDepartment.SelectedItem != null)
            {
                ent.Depratment = _cboDepartment.SelectedItem.ToString();
            }
            //职称
            ent.Position = _txtPosition.Text;
            //备注
            ent.Remarks = _rtxtRemark.Text;

            //姓名不能存在特殊字符，且不能为空
            if (LibCommon.Validator.checkSpecialCharacters(ent.Name) || LibCommon.Validator.IsEmptyOrBlank(ent.Name))
            {
                Alert.alert(LibCommon.Const.NAME_IS_WRONG, LibCommon.Const.NOTES, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _txtName.Focus();

                //解决return后，窗体消失的bug
                DialogResult = DialogResult.None;
                return;
            }

            //检查手机号码，可以为空但不能格式错误
            //if (_txtPhoneNumber.Text != "")
            //{
            //    if (!LibCommon.Validator.checkIsPhoneNumber(_txtPhoneNumber.Text))
            //    {
            //        Alert.alert(LibCommon.Const.PHONE_IS_WRONG, LibCommon.Const.NOTES, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        this._txtPhoneNumber.Focus();
            //        return;
            //    }
            //}

            //检查电话号码，可以为空但不能格式错误
            //if (_txtTel.Text != "")
            //{
            //    if (!LibCommon.Validator.checkIsIsTelePhone(_txtTel.Text))
            //    {
            //        Alert.alert(LibCommon.Const.TEL_IS_WRONG, LibCommon.Const.NOTES, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        this._txtTel.Focus();
            //        return;
            //    }
            //}

            //检查邮箱，可以为空但不能格式错误
            if (_txtEmail.Text != "")
            {
                if (!LibCommon.Validator.checkIsEmailAddress(_txtEmail.Text))
                {
                    Alert.alert(LibCommon.Const.EMAIL_IS_WRONG, LibCommon.Const.NOTES, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this._txtEmail.Focus();
                    return;
                }
            }

            //添加
            if (_strIsAddOrModify == "add")
            {
                //添加用户详细信息
                UserInformationDetailsManagementBLL.InsertUserInformationDetailsIntoTable(ent);
            }
            //修改。操作是仍需记录管理界面赋值的Id，（_userSel[0])中存放
            else if (_strIsAddOrModify == "modify")
            {
                //修改用户详细信息
                UserInformationDetailsManagementBLL.UpdataUserInformationDetails(ent, UserInformationDetailsManagement._userSel[0]);
            }
            this.Close();
        }

    }
}
