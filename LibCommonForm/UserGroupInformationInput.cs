// ******************************************************************
// 概  述：用户组信息录入
// 作  者：秦凯
// 创建日期：2014/03/06
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
using LibDatabase;
using LibBusiness;
using LibEntity;
using LibCommon;

namespace LibCommonForm
{
    public partial class UserGroupInformationInput : Form
    {
        //已选择的用户
        List<string> _strSelItem = new List<string>();

        /// <summary>
        /// 构造函数
        /// </summary>
        public UserGroupInformationInput()
        {
            InitializeComponent();

            //设置窗体格式
            LibCommon.FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, LibCommon.LibFormTitles.USER_GROUP_INFO_MANMAGEMENT_ADD);
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            //解决确定后窗体关闭问题
            DialogResult = DialogResult.None;
            //用户组名称
            string name = _txtGroupName.Text.ToString().Trim();
            //检查用户组名称不能为空,不能重复
            if (LibCommon.Validator.checkSpecialCharacters(name) || LibCommon.Validator.IsEmpty(name)||UserGroupInformationManagementBLL.FindTheSameGroupName(name))
            {
                Alert.alert(LibCommon.Const.USER_GROUP_NAME_IS_WRONG, LibCommon.Const.NOTES, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //设置焦点
                _txtGroupName.Focus();
                return;
            }
            //选择用户的数量
            int userCount = _lstSelUserName.Items.Count;
            
            //定义用户组信息实体
            UserGroupInformationManagementEntity ent = new UserGroupInformationManagementEntity();
            //组名
            ent.GroupName = _txtGroupName.Text.ToString().Trim();
            //人数
            ent.UserCount = userCount.ToString();
            //备注
            ent.Remark = _rtxtRemarks.Text.ToString().Trim();
            //插入数据库
            UserGroupInformationManagementBLL.InsertRecordIntoTableUserGroupInformation(ent);

            //修改用户登录信息表中记录的组名                      
            for (int i = 0; i < userCount; i++)
            {
                UserGroupInformationManagementBLL.ChangeUserGroup(ent.GroupName, _lstSelUserName.Items[i].ToString());
            }

            DataTable dt = UserGroupInformationManagementBLL.GetUserGroupInformation();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                UserGroupInformationManagementBLL.UpdateUserCountFromUserGroup(dt.Rows[i][UserGroupInformationMangementDbConstNames.USER_GROUP_NAME].ToString());
            }
            this.Close(); 
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 窗体load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserGroupInformationInput_Load(object sender, EventArgs e)
        {
            GetUserInfo();//加载数据           
            _strSelItem.Clear(); //清除垃圾数据
            //设置按钮是否启用
            _btnSelAdding.Enabled = false;
            _btnSelReduce.Enabled = false;
        }

        /// <summary>
        /// 加载用户信息数据
        /// </summary>
        private void GetUserInfo()
        {            
            //从数据库中加载数据
            DataTable dt = UserGroupInformationManagementBLL.GetUserInformationForInputWindow();
            if (dt != null)
            {
                //datagridview显示用户信息
                _dgrvdUserInfo.DataSource = DecryptDataTable(dt);
                
                for (int i = 0; i < _dgrvdUserInfo.ColumnCount; i++)
                {
                    //"USER_LOGIN_NAME"
                    if (_dgrvdUserInfo.Columns[i].HeaderText == LibBusiness.UserLoginInformationManagementDbConstNames.USER_LOGIN_NAME)
                    {
                        _dgrvdUserInfo.Columns[i].HeaderText = LibCommon.Const.USER_GROUP_NAME_LOGIN_NAME;
                    }
                    //"USER_GROUP_NAME"
                    if (_dgrvdUserInfo.Columns[i].HeaderText == LibBusiness.UserLoginInformationManagementDbConstNames.USER_GROUP_NAME)
                    {
                        _dgrvdUserInfo.Columns[i].HeaderText = LibCommon.Const.USER_GROUP_NAME_LOGIN_NAME_USER_GROUP;
                    }
                    //"USER_PERMISSION"
                    if (_dgrvdUserInfo.Columns[i].HeaderText == LibBusiness.UserLoginInformationManagementDbConstNames.USER_PERMISSION)
                    {
                        _dgrvdUserInfo.Columns[i].HeaderText = LibCommon.Const.USER_GROUP_NAME_PERMISSION;
                    }
                    
                    //设置datagridview列的尺寸
                    _dgrvdUserInfo.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            //清除选择
            _dgrvdUserInfo.ClearSelection();
        }

        /// <summary>
        /// 数据表整体解密
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private DataTable DecryptDataTable(DataTable dt)
        {
            DataTable returndt = dt;
            //获取行、列数
            int dtRowCount=dt.Rows.Count;
            int dtColumnCount=dt.Columns.Count;

            for (int rowIndex = 0; rowIndex < dtRowCount;rowIndex++ )
            {
                for (int columnIndex = 0; columnIndex < dtColumnCount; columnIndex++)
                {
                    returndt.Rows[rowIndex][columnIndex] = LibEncryptDecrypt.DWEncryptDecryptClass.DecryptString(dt.Rows[rowIndex][columnIndex].ToString());
                }
            }
            return returndt;
        }

        /// <summary>
        /// 选择的用户信息改变,用以检测是否已选择用户,设置按钮启用与否
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _dgrvdUserInfo_SelectionChanged(object sender, EventArgs e)
        {
           if (_dgrvdUserInfo.SelectedRows.Count>0)
           {
               //添加按钮启用
               _btnSelAdding.Enabled = true;
           }
           else
           {
               //添加按钮不启用
               _btnSelAdding.Enabled = false;
           }
        }

        /// <summary>
        /// 添加用户到已选择列表中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _btnSelAdding_Click(object sender, EventArgs e)
        {
            //遍历选择的所有项
            foreach (DataGridViewRow dgrvr in _dgrvdUserInfo.SelectedRows)
            {
                if (!_strSelItem.Contains(dgrvr.Cells[0].Value.ToString()))
                {
                    _strSelItem.Add(dgrvr.Cells[0].Value.ToString());
                }               
            }

            //清除_lstSelUserName中的垃圾数据
            _lstSelUserName.Items.Clear();

            //重新加入新数据
            foreach (string str in _strSelItem)
            {
                _lstSelUserName.Items.Add(str);
            }

            //datagridview清除选择
            _dgrvdUserInfo.ClearSelection();
            _btnSelAdding.Enabled = false;

            //触发此事件,利用_lstSelUserName中已无值，设置按钮Enabled
            _lstSelUserName_SelectedIndexChanged(sender, e);
        }

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _btnSelReduce_Click(object sender, EventArgs e)
        {
            //移除选择的用户信息
            while (_lstSelUserName.SelectedIndex != -1)
            {                
                _strSelItem.Remove(_lstSelUserName.SelectedItem.ToString());
                _lstSelUserName.Items.Remove(_lstSelUserName.SelectedItem);
            }

            //清空选择的用户信息,便于下面重新加载
            _lstSelUserName.Items.Clear();

            foreach (string str in _strSelItem)
            {
                _lstSelUserName.Items.Add(str);
            }

            //清除选择项
            _lstSelUserName.ClearSelected();

            //设置按钮不启用
            _btnSelReduce.Enabled = false;

            //触发此事件,利用_lstSelUserName中已无值，设置按钮Enabled
            _lstSelUserName_SelectedIndexChanged(sender, e);
        }

        /// <summary>
        /// 选择的用户信息改变,用以检测是否已选择用户,设置按钮启用与否
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _lstSelUserName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_lstSelUserName.SelectedItems.Count > 0)
            {
                _btnSelReduce.Enabled = true;
            }
            else
            {
                _btnSelReduce.Enabled = false;
            }
        }
    }
}
