// ******************************************************************
// 概   述：用户组信息管理
// 作  者：秦  凯
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
using LibXPorperty;
using FarPoint.Win.Spread.CellType;
using LibBusiness;
using LibEntity;
using LibCommon;

namespace LibCommonForm
{
    public partial class UserGroupInformationManagement : Form
    {
        //定义Farpoint的初始行列数
        const int _fpColumnsCount = 4;
        const int _fpRowsTitleCount = 2;

        //定义用户组名称的长度
        public const int _iUserGroupNameLength = 50;

        //定义用户组列的列Lable，用于标记该列，该值与Farpoint中的列Lable值相同
        const string _strUserGroupNameLable = "用户组名称";

        //定义存放选择用户组名称  的变量
        List<string> _strUserGroupSel = new List<string>();

        #region 属性窗口使用的变量
        //定义用户组属性实体
        private UserGroupParameter _srcEnt = null;
        private XProps _props = new XProps();

        //用户组名称
        XProp _userGroupName = new XProp();
        //用户组人数
        XProp _userCount = new XProp();
        //用户组备注
        XProp _remarks = new XProp();

        List<XProp> _paramProp = new List<XProp>();

        //属性窗口使用的常量
        const string CATEGORY_GROUPNAME_TITLE = "1.用户组名称";
        const string CATEGORY_USERCOUNT_TITLE = "2.用户组成员数";        
        const string CATEGORY_REMARKS_TITLE = "3.备注";//MAKR FIELD

        //用户组信息参数
        class UserGroupParameter
        {
            //用户组名称
            private string _groupname;
            /// <summary>
            /// 用户组名称
            /// </summary>
            public string GroupName
            {
                get { return _groupname; }
                set { _groupname = value; }
            }

            //用户组人数
            private string _usercount;
            /// <summary>
            /// 用户组人数
            /// </summary>
            public string UserCount
            {
                get { return _usercount; }
                set { _usercount = value; }
            } 

            //备注
            private string _remark;
            /// <summary>
            /// 备注
            /// </summary>
            public string Remark
            {
                get { return _remark; }
                set { _remark = value; }
            }  

            /// <summary>
            /// 构造函数
            /// </summary>
            /// <param name="groupname"></param>
            /// <param name="count"></param>
            /// <param name="remark"></param>
            public UserGroupParameter(string groupname, string count, string remark)
            {
                _usercount=count;
                _groupname=groupname;
                _remark = remark;
            }

            /// <summary>
            /// 默认构造函数
            /// </summary>
            public UserGroupParameter()
            {

            }

            /// <summary>
            /// 深度克隆
            /// </summary>
            /// <returns></returns>
            public UserGroupParameter DeepClone()
            {
                UserGroupParameter cpyEnt = new UserGroupParameter();
                cpyEnt.GroupName = GroupName;
                cpyEnt.UserCount=UserCount;
                cpyEnt.Remark = Remark;                
                return cpyEnt;
            }
        }
        #endregion

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public UserGroupInformationManagement()
        {
            InitializeComponent();

            //设置窗体和farpoint格式
            LibCommon.FarpointDefaultPropertiesSetter.SetFpDefaultProperties(_fpUserGroupInfo, LibCommon.LibFormTitles.USER_GROUP_INFO_MANMAGEMENT, 2);
            LibCommon.FormDefaultPropertiesSetter.SetManagementFormDefaultProperties(this, LibCommon.LibFormTitles.USER_GROUP_INFO_MANMAGEMENT);
            
            //判断用户权限
            if (CurrentUserEnt.CurLoginUserInfo.Permission != Permission.管理员.ToString())
            {
                //不具备管理员权限是，隐藏按钮
                tsBtnAdd.Visible = false;
                tsBtnDel.Visible = false;
                _propInfo.Enabled = false;
            }

            //设置Farpoint的初始行列数
            _fpUserGroupInfo.ActiveSheet.Columns.Count = _fpColumnsCount;
            _fpUserGroupInfo.ActiveSheet.Rows.Count = _fpRowsTitleCount;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnAdd_Click(object sender, EventArgs e)
        {
            UserGroupInformationInput ugi = new UserGroupInformationInput();
            ugi.ShowDialog();

            //重新load数据
            GetUserGrouptInfo();
        }

        /// <summary>
        /// 加载用户组信息数据
        /// </summary>
        private void GetUserGrouptInfo()
        {
            #region 删除垃圾数据
            while (_fpUserGroupInfo.ActiveSheet.Rows.Count > _fpRowsTitleCount)
            {
                _fpUserGroupInfo.ActiveSheet.Rows.Remove(_fpRowsTitleCount, 1);
            }
            #endregion

            //从数据库中获取用户组信息值
            DataTable dt = UserGroupInformationManagementBLL.GetUserGroupInformation();

            if (dt != null)
            {
                //记录条数
                int n = dt.Rows.Count;
                //记录列数
                int k = dt.Columns.Count;

                for (int i = 0; i < n; i++)
                {
                    this._fpUserGroupInfo.ActiveSheet.Rows.Add(i + _fpRowsTitleCount, 1);
                    for (int j = 0; j < k; j++)
                    {
                        //设置单元格类型
                        this._fpUserGroupInfo.Sheets[0].Cells[i + 2, j + 1].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                        //锁定单元格
                        this._fpUserGroupInfo.Sheets[0].Cells[i + 2, j + 1].Locked = true;
                        //设置单元格的值
                        this._fpUserGroupInfo.Sheets[0].Cells[i + 2, j + 1].Text = dt.Rows[i][j].ToString();
                        //设置单元格对其方式
                        this._fpUserGroupInfo.Sheets[0].Cells[i + 2, j + 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                        this._fpUserGroupInfo.Sheets[0].Cells[i + 2, j + 1].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                    }

                    //设置选择单元格的类型与对其方式
                    this._fpUserGroupInfo.Sheets[0].Cells[i + 2, 0].CellType = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
                    this._fpUserGroupInfo.Sheets[0].Cells[i + 2, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    this._fpUserGroupInfo.Sheets[0].Cells[i + 2, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                }
            }

            //设置焦点
            this._fpUserGroupInfo.ActiveSheet.SetActiveCell(1, 0);
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnPrint_Click(object sender, EventArgs e)
        {
            FilePrint.CommonPrint(_fpUserGroupInfo, 0);
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnExport_Click(object sender, EventArgs e)
        {
            if (FileExport.fileExport(_fpUserGroupInfo, true))
            {
                Alert.alert(Const.EXPORT_SUCCESS_MSG);
            }           
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnDel_Click(object sender, EventArgs e)
        {
            if (Alert.confirm(Const.DEL_CONFIRM_MSG, Const.NOTES, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                //获取记录条数
                int n = GetTableRecordCount();
                for (int i = n - 1; i >= 0; i--)
                {
                    if (this._fpUserGroupInfo.ActiveSheet.Cells[2 + i, 0].CellType is CheckBoxCellType)
                    {
                        if (this._fpUserGroupInfo.ActiveSheet.Cells[2 + i, 0].Value != null)
                        {
                            if ((bool)this._fpUserGroupInfo.ActiveSheet.Cells[2 + i, 0].Value)
                            {
                                this._fpUserGroupInfo.ActiveSheet.Rows.Remove(2 + i, 1);                           
                            }
                        }
                    }
                }

                //更新数据库中用户组、用户信息
                foreach (string str in _strUserGroupSel)
                {
                    //删除用户组
                    UserGroupInformationManagementBLL.DeleteUserGroupInformationByGroupName(str);
                    //删除登录用户的用户组信息
                    UserGroupInformationManagementBLL.UpdateUserInformationWhenDeleteGroup(str);
                }

                //farPoint刷新
                _fpUserGroupInfo.Refresh();

                //设置焦点
                this._fpUserGroupInfo.ActiveSheet.SetActiveCell(UserGroupInformationManagementBLL.GetRecordCountFromTable() + 1, 0);

                //设置删除按钮不可用
                tsBtnDel.Enabled = false;
            }
        }

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnRefresh_Click(object sender, EventArgs e)
        {
            //重新加载数据
            GetUserGrouptInfo();
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 窗体登陆
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserGroupInformationManagement_Load(object sender, EventArgs e)
        {
            //加载数据
            GetUserGrouptInfo();

            //实例化属性窗口
            InitXPorps();

            //清除垃圾数据
            _strUserGroupSel.Clear();
            tsBtnDel.Enabled = false;
        }

        /// <summary>
        /// 属性窗口初始化
        /// </summary>
        void InitXPorps()
        {
            #region 基本信息
            //用户组名称
            _userGroupName.Category = CATEGORY_GROUPNAME_TITLE;
            _userGroupName.Name = "用户组名称";
            _userGroupName.Description = "用户组唯一身份标识，不能重复";
            _userGroupName.ReadOnly = true;
            _userGroupName.ProType = typeof(string);
            _userGroupName.Visible = true;
            _userGroupName.Converter = null;
            _props.Add(_userGroupName);

            //用户组成员数
            _userCount.Category = CATEGORY_USERCOUNT_TITLE;
            _userCount.Name = "用户组成员数";
            _userCount.Description = "用户组成员数不可编辑";
            _userCount.ReadOnly = true;
            _userCount.ProType= typeof(string);
            _userCount.Visible = true;
            _userCount.Converter = null;
            _props.Add(_userCount);            

            //备注
            _remarks.Category = CATEGORY_REMARKS_TITLE;
            _remarks.Name = "备注";
            _remarks.Description = "备注若无，则无需添加";
            _remarks.ReadOnly = true;
            _remarks.ProType = typeof(string);
            _remarks.Visible = true;
            _remarks.Converter = null;
            _props.Add(_remarks);
            #endregion
        }

        /// <summary>
        /// farpoint选择变化时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpUserInfo_SelectionChanged(object sender, FarPoint.Win.Spread.SelectionChangedEventArgs e)
        {
            //获取选择行的行索引
            int n = this._fpUserGroupInfo.ActiveSheet.ActiveCell.Row.Index;

            //设定属性窗口是否启用,当选择的单元格所在行,无用户组名称时,不启用
            if (CurrentUserEnt.CurLoginUserInfo.Permission != Permission.普通用户.ToString())
            {
                _propInfo.Enabled = this._fpUserGroupInfo.ActiveSheet.Cells[n, 1].Value == null ? false : true;
            }
           
            //点击标题时,返回
            if (n < 2)
            {
                return;
            }

            //组名
            string groupname = "";
            //人数
            string usercount = "";
            //备注
            string remark = "";
            if (this._fpUserGroupInfo.ActiveSheet.Cells[n, 1].Value != null)
            {
                groupname = this._fpUserGroupInfo.ActiveSheet.Cells[n, 1].Value.ToString();
            }
            if (this._fpUserGroupInfo.ActiveSheet.Cells[n, 2].Value != null)
            {
                usercount = this._fpUserGroupInfo.ActiveSheet.Cells[n, 2].Value.ToString();
            }          
            if (this._fpUserGroupInfo.ActiveSheet.Cells[n, 3].Value != null)
            {
                remark = this._fpUserGroupInfo.ActiveSheet.Cells[n, 3].Value.ToString();
            }

            //属性窗口显示的值
            UserGroupParameter ugp = new UserGroupParameter(groupname,usercount,remark);
            //设置属性值
            SetPropertyGridEnt(ugp);
        }

        /// <summary>
        /// 设定显示窗口的实体
        /// </summary>
        /// <param name="ent"></param>
        void SetPropertyGridEnt(UserGroupParameter ent)
        {
            if (ent == null)
            {
                return;
            }
            //深度克隆
            _srcEnt = ent.DeepClone();

            //设置只读属性为否，可以修改属性值
            _userGroupName.ReadOnly = false;
            _remarks.ReadOnly = false;

            //设置个属性值
            _userGroupName.Value = ent.GroupName;
            _userCount.Value= ent.UserCount;
            _remarks.Value = ent.Remark;

            //删除旧信息
            int preCnt = _paramProp.Count;
            for (int i = 0; i < preCnt; i++)
            {
                _props.Remove(_paramProp[i]);
            }
            _paramProp.Clear();

            //添加新属性实体
            _propInfo.SelectedObject = _props;
        }

        /// <summary>
        /// farpoint单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpUserInfo_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (this._fpUserGroupInfo.ActiveSheet.Cells[e.Row, 1] != null)
            {
                // 判断点击的空间类型是否是.FpCheckBox)
                if (e.EditingControl is FarPoint.Win.FpCheckBox)
                {
                    FarPoint.Win.FpCheckBox fpChk = (FarPoint.Win.FpCheckBox)e.EditingControl;
                    // 判断是否被选中
                    if (fpChk.Checked)
                    {
                        if (!_strUserGroupSel.Contains(this._fpUserGroupInfo.ActiveSheet.Cells[e.Row, 1].Value.ToString()))
                        {
                            //记录选中的用户组名称
                            _strUserGroupSel.Add(this._fpUserGroupInfo.ActiveSheet.Cells[e.Row, 1].Value.ToString());
                            //判断是否已全选
                            this._chkSelAll.CheckedChanged -= this.chkSelAll_CheckedChanged;
                            this._chkSelAll.Checked = _strUserGroupSel.Count == UserGroupInformationManagementBLL.GetRecordCountFromTable() ? true : false;
                            this._chkSelAll.CheckedChanged += this.chkSelAll_CheckedChanged;
                        }
                    }
                    else
                    {
                        // 移除索引号
                        _strUserGroupSel.Remove(this._fpUserGroupInfo.ActiveSheet.Cells[e.Row, 1].Value.ToString());
                        // 全选/全不选checkbox设为未选中
                        this._chkSelAll.CheckedChanged -= this.chkSelAll_CheckedChanged;
                        this._chkSelAll.Checked = false;
                        this._chkSelAll.CheckedChanged += this.chkSelAll_CheckedChanged;
                    }
                    // 删除按钮
                    this.tsBtnDel.Enabled = (_strUserGroupSel.Count >= 1) ? true : false;
                }
            }
        }

        /// <summary>
        /// 全选/全不选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkSelAll_CheckedChanged(object sender, EventArgs e)
        {
            //清除记录的数据
            _strUserGroupSel.Clear();

            //获得表中记录条数
            int n = GetTableRecordCount();
            if (_chkSelAll.Checked)
            {
                for (int i = 0; i < n; i++)
                {
                    //设置farpoint显示
                    this._fpUserGroupInfo.Sheets[0].Cells[2 + i, 0].Value = ((CheckBox)sender).Checked;
                    if (this._fpUserGroupInfo.Sheets[0].Cells[2 + i, 1].Value!=null)
                    {
                        //记录选择的值
                        _strUserGroupSel.Add(this._fpUserGroupInfo.Sheets[0].Cells[2 + i, 1].Value.ToString());
                    }
                }
            }
            else
            {
                for (int i = 0; i < n; i++)
                {
                    //设置farpoint显示
                    this._fpUserGroupInfo.Sheets[0].Cells[2 + i, 0].Value = false;
                }
            }
            this.tsBtnDel.Enabled = (_strUserGroupSel.Count >= 1) ? true : false;
        }

        /// <summary>
        /// 从表中获取数据
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        private int GetTableRecordCount()
        {
            return UserGroupInformationManagementBLL.GetRecordCountFromTable();
        }

        
        /// <summary>
        /// 属性值更改
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void _propInfo_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            //获取当前行的行索引
            int n = this._fpUserGroupInfo.ActiveSheet.ActiveCell.Row.Index;

            //选择表题时,返回
            if (n<2)
            {
                return;
            }

            //定义接收变化的值
            string changeValue = "";
             if (e.ChangedItem.Value != null)
            {
                //若易报错，则首先检查fp中columns的tag是否与ss相同
                 //获取更改值的Label
                string ss = e.ChangedItem.Label.ToString();
                //获取列索引
                int columnIndex = this._fpUserGroupInfo.ActiveSheet.Columns[ss].Index;
                //获取修改的值
                changeValue = e.ChangedItem.Value.ToString();

                if (ss == _strUserGroupNameLable)
                {
                    //检测特殊字符、是否为空、是否重复
                    if (LibCommon.Validator.checkSpecialCharacters(changeValue) ||
                        LibCommon.Validator.IsEmpty(changeValue) ||
                        UserGroupInformationManagementBLL.FindTheSameGroupName(changeValue))
                    {
                        Alert.alert(LibCommon.Const.USER_GROUP_NAME_IS_WRONG, LibCommon.Const.NOTES, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        fpUserInfo_SelectionChanged(null, null);
                        return;
                    }

                    //检查字符串的长度
                    if (changeValue.Length > _iUserGroupNameLength)
                    {
                        Alert.alert(LibCommon.Const.TXT_IS_WRONG1 + _iUserGroupNameLength + LibCommon.Const.TXT_IS_WRONG2, LibCommon.Const.NOTES, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        fpUserInfo_SelectionChanged(null, null);
                        return;
                    }
                }

                #region 获取旧值
                //定义组名称
                string groupName = "";
                //人数
                string userCount = "";
                //备注
                string remark = "";

                //从farpoint取值时,检查是否为空
                if (this._fpUserGroupInfo.ActiveSheet.Cells[n, 1].Value != null)
                {
                    groupName = this._fpUserGroupInfo.ActiveSheet.Cells[n, 1].Value.ToString();
                }
                if (this._fpUserGroupInfo.ActiveSheet.Cells[n, 2].Value != null)
                {
                    userCount = this._fpUserGroupInfo.ActiveSheet.Cells[n, 2].Value.ToString();
                }
                if (this._fpUserGroupInfo.ActiveSheet.Cells[n, 3].Value != null)
                {
                    remark = this._fpUserGroupInfo.ActiveSheet.Cells[n, 3].Value.ToString();
                }     
           
                //用户组名称旧值
                string oldName = groupName;

                if (ss == _strUserGroupNameLable)
                {                   
                    oldName = e.OldValue.ToString();
                }
                #endregion

                //修改farpoint的显示值
                this._fpUserGroupInfo.ActiveSheet.Cells[n, columnIndex].Text = changeValue;          

                 //定义用户组信息实体,接收新值
                UserGroupInformationManagementEntity userGroupInfo = new UserGroupInformationManagementEntity();
                userGroupInfo.GroupName = groupName;
                userGroupInfo.UserCount = userCount;
                userGroupInfo.Remark = remark;     
      
                //更新数据库
                UserGroupInformationManagementBLL.UpdateUserGroupInfomationDatabase(userGroupInfo, oldName);
            }
        }
    }
}
