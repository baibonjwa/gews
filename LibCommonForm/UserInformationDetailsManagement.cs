// ******************************************************************
// 概  述：用户详细信息管理
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
using LibCommon;
using LibEntity;
using LibBusiness;

namespace LibCommonForm
{
    public partial class UserInformationDetailsManagement : Form
    {
        #region 定义事件和委托
        //是否通过外部事件关闭窗体，不响应自身内部的OK、Cancel、Exit事件
        private bool _closeExternal = false;
        public void CloseExternal(bool vl)
        {
            _closeExternal = vl;
        }
        //定义确定Button单击委托
        public delegate void OnOKButtonClick(object sender, EventArgs arg);
        //定义确定Button单击事件
        public event OnOKButtonClick OnButtonClickHandle;

        //定义取消Button单击事件
        public delegate void OnCancleButtonClick(object sender, EventArgs arg);
        //定义取消Button单击事件
        public event OnCancleButtonClick OnCancleButtonClickHandle;

        //定义退出按钮委托
        public delegate void OnExiteClick(object sender, EventArgs arg);
        //定义取消Button单击事件
        public event OnExiteClick OnExiteClickHandle;
        #endregion

        //存放已选择UserId
        public static List<int> _userSel = new List<int>();
        //定义存放数据记录的值
        int _fpRowsCount = 0;
        //定义farpoint数据标题头
        const int _fpRowsTitleCount = 2;
        const int _fpColumnsTitleCount = 10;
        //需要过滤的列索引
        private int[] _filterColunmIdxs = null;

        public UserInformationDetailsManagement()
        {
            InitializeComponent();
            //设置窗体、farpoint格式
            LibCommon.FormDefaultPropertiesSetter.SetMdiChildrenManagementFormDefaultProperties(this, LibCommon.LibFormTitles.USER_INFO_DETAILS_MANMAGEMENT);
            LibCommon.FarpointDefaultPropertiesSetter.SetFpDefaultProperties(fpUserInformationDetails, LibCommon.LibFarpintTiltes.USER_INFO_DETAILS, 2);
            //判断用户权限
            if (CurrentUserEnt.CurLoginUserInfo.Permission != Permission.管理员.ToString())
            {
                tsBtnAdd.Visible = false;
                tsBtnDel.Visible = false;
                tsBtnModify.Visible = false;
            }

            #region Farpoint自动过滤功能
            //初始化需要过滤功能的列
            _filterColunmIdxs = new int[]
            {
                6,
            };
            //禁用选择颜色相关控件
            farpointFilter1.EnableChooseColorCtrls(false);
            //设置自动隐藏过滤条件
            FarpointDefaultPropertiesSetter.SetFpFilterHideProperties(this.fpUserInformationDetails, _filterColunmIdxs);
            #endregion
        }

        #region Farpoint自动过滤功能
        private void farpointFilter1_OnCheckFilterChanged(object sender, EventArgs arg)
        {
            CheckBox chk = (CheckBox)sender;
            //当Checkbox选中时，筛选过程中则将不符合条件的数据隐藏
            if (chk.Checked == true)
            {
                //禁用选择颜色相关控件
                farpointFilter1.EnableChooseColorCtrls(false);
                //设置自动隐藏过滤条件
                FarpointDefaultPropertiesSetter.SetFpFilterHideProperties(this.fpUserInformationDetails, _filterColunmIdxs);

            }
            else//未选中时，根据用户自定义的颜色进行分类显示
            {
                //启用选择颜色相关控件
                farpointFilter1.EnableChooseColorCtrls(true);
                //设置自定义过滤条件
                FarpointDefaultPropertiesSetter.SetFpCustomFilterProperties(this.fpUserInformationDetails, farpointFilter1.GetSelectedFitColor(), farpointFilter1.GetSelectedNotFitColor(), _filterColunmIdxs);
            }
        }

        private void farpointFilter1_OnClickClearFilterBtn(object sender, EventArgs arg)
        {
            //清空过滤条件
            this.fpUserInformationDetails.ActiveSheet.RowFilter.ResetFilter();
        }

        private void farpointFilter1_OnClickFitColorBtnOK(object sender, EventArgs arg)
        {
            //根据新的颜色值设置自动隐藏过滤条件
            FarpointDefaultPropertiesSetter.SetFpCustomFilterProperties(this.fpUserInformationDetails, farpointFilter1.GetSelectedFitColor(), farpointFilter1.GetSelectedNotFitColor(), _filterColunmIdxs);
        }

        private void farpointFilter1_OnClickNotFitColorBtnOK(object sender, EventArgs arg)
        {
            //根据新的颜色值设置自动隐藏过滤条件
            FarpointDefaultPropertiesSetter.SetFpCustomFilterProperties(this.fpUserInformationDetails, farpointFilter1.GetSelectedFitColor(), farpointFilter1.GetSelectedNotFitColor(), _filterColunmIdxs);
        }
        #endregion

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnPrint_Click(object sender, EventArgs e)
        {
            FilePrint.CommonPrint(this.fpUserInformationDetails, 0);
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnExport_Click(object sender, EventArgs e)
        {
            if (FileExport.fileExport(fpUserInformationDetails, true))
            {
                Alert.alert(Const.EXPORT_SUCCESS_MSG);
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnAdd_Click(object sender, EventArgs e)
        {
            //添加,实例化方法中，null则认为是添加
            UserInformationDetailsInput uidi = new UserInformationDetailsInput(null);
            uidi.ShowDialog();
            //重新获取用户信息到显示界面
            GetUsersInfoDetails();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnModify_Click(object sender, EventArgs e)
        {
            if (_userSel.Count == 0)
            {
                //管理界面没有选择时，返回
                return;
            }

            //定义用户登录信息实体，接收旧值，添加到窗体中。旧值来源于管理界面的选择值。可以直接取值，也可从数据库取值，暂不考虑效率，以下代码从数据库取值。
            UserInformationDetailsEnt ent = UserInformationDetailsManagementBLL.GetUserLoginInformationByID(_userSel[0]);

            //修改
            UserInformationDetailsInput uidi = new UserInformationDetailsInput(ent);
            uidi.ShowDialog();
            //刷新显示的用户信息
            GetUsersInfoDetails();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnDel_Click(object sender, EventArgs e)
        {
            if (Alert.confirm(LibCommon.Const.DELETE_STUFF_INFO, LibCommon.Const.NOTES, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                //获取登录用户记录条数
                int n = UserInformationDetailsManagementBLL.GetRecordCountFromTable();
                //从数据末尾删除数据
                for (int i = n - 1; i >= 0; i--)
                {
                    if (this.fpUserInformationDetails.ActiveSheet.Cells[2 + i, 0].CellType is FarPoint.Win.Spread.CellType.CheckBoxCellType)
                    {
                        if (this.fpUserInformationDetails.ActiveSheet.Cells[2 + i, 0].Value != null)
                        {
                            if ((bool)this.fpUserInformationDetails.ActiveSheet.Cells[2 + i, 0].Value)
                            {
                                this.fpUserInformationDetails.ActiveSheet.Rows.Remove(2 + i, 1);
                                //删除一行数据后,在数据末尾加一行
                                //this.fpUserInformationDetails.ActiveSheet.Rows.Add(n - 1 + 2, 1);
                                //this.fpUserLoginInformation.ActiveSheet.Rows[n - 1 + 2].BackColor = Color.Red;//显示颜色直观显示追加行位置，方便调试观察，代码在后期删除
                            }
                        }
                    }
                }
                //遍历 记录用户信息的数组，删除数据库中的数据
                foreach (int id in _userSel)
                {
                    UserInformationDetailsManagementBLL.DeleteUserLoginInformationByID(id);
                }
                _userSel.Clear();

                //设置删除按钮是否启用
                this.tsBtnDel.Enabled = (_userSel.Count >= 1) ? true : false;
                //设置修改按钮是否启用
                this.tsBtnModify.Enabled = (_userSel.Count == 1) ? true : false;

                //刷新数据
                fpUserInformationDetails.Refresh();
                //设置焦点
                this.fpUserInformationDetails.ActiveSheet.SetActiveCell(UserInformationDetailsManagementBLL.GetRecordCountFromTable() + 1, 0);
                //记录当前farpoint的记录条数
                _fpRowsCount = this.fpUserInformationDetails.ActiveSheet.Rows.Count;
            }
        }

        /// <summary>
        /// 刷新。从数据库中重新读取数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnRefresh_Click(object sender, EventArgs e)
        {
            GetUsersInfoDetails();
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnExit_Click(object sender, EventArgs e)
        {
            if (_closeExternal)
            {
                //自定义事件获取按钮事件
                OnExiteClickHandle(sender, e);
            }
            else
            {   //关闭自定义控件
                this.Close();
            }

        }

        /// <summary>
        /// 全选/全不选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkSelAll_CheckedChanged(object sender, EventArgs e)
        {
            //清除  记录用户信息的数组，以便于重新赋值
            _userSel.Clear();
            //获取用户登录信息记录条数
            int n = this.fpUserInformationDetails.ActiveSheet.Rows.Count - _fpRowsTitleCount;
            //设置全选/全不选
            if (chkSelAll.Checked)
            {
                for (int i = 0; i < n; i++)
                {
                    //设置farPoint中checkbox显示为全选
                    this.fpUserInformationDetails.Sheets[0].Cells[2 + i, 0].Value = ((CheckBox)sender).Checked;
                    //记录用户信息
                    _userSel.Add((int)this.fpUserInformationDetails.Sheets[0].Cells[2 + i, 1].Value);
                }
            }
            //设置farPoint中checkbox显示为全不选
            else
            {
                for (int i = 0; i < n; i++)
                {
                    //设置全不选时的，farpoint的选择单元格值
                    this.fpUserInformationDetails.Sheets[0].Cells[2 + i, 0].Value = false;
                }
            }
            //设置删除按钮是否启用
            this.tsBtnDel.Enabled = (_userSel.Count >= 1) ? true : false;
            //设置修改按钮是否启用
            this.tsBtnModify.Enabled = (_userSel.Count == 1) ? true : false;
        }

        /// <summary>
        /// farpoint单元格单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpUserInformationDetails_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            // 判断点击的空间类型是否是.FpCheckBox)
            if (e.EditingControl is FarPoint.Win.FpCheckBox)
            {
                FarPoint.Win.FpCheckBox fpChk = (FarPoint.Win.FpCheckBox)e.EditingControl;
                // 判断是否被选中
                if (fpChk.Checked)
                {
                    if (!_userSel.Contains((int)this.fpUserInformationDetails.ActiveSheet.Cells[e.Row, 1].Value))
                    {
                        //添加  记录用户信息
                        _userSel.Add((int)this.fpUserInformationDetails.ActiveSheet.Cells[e.Row, 1].Value);
                        //checkbox赋值前，先移除CheckedChanged事件，然后加上
                        this.chkSelAll.CheckedChanged -= this.chkSelAll_CheckedChanged;
                        chkSelAll.Checked = (_userSel.Count == _fpRowsCount - _fpRowsTitleCount) ? true : false;
                        this.chkSelAll.CheckedChanged += this.chkSelAll_CheckedChanged;
                    }
                }
                else
                {
                    // 移除索引号
                    _userSel.Remove((int)this.fpUserInformationDetails.ActiveSheet.Cells[e.Row, 1].Value);
                    // 全选/全不选checkbox设为未选中
                    this.chkSelAll.CheckedChanged -= this.chkSelAll_CheckedChanged;
                    this.chkSelAll.Checked = false;
                    this.chkSelAll.CheckedChanged += this.chkSelAll_CheckedChanged;
                }
                // 如果保存索引号件数是1，则修改按钮设为可用，否则设为不可用
                this.tsBtnModify.Enabled = (_userSel.Count == 1) ? true : false;
                // 删除按钮
                this.tsBtnDel.Enabled = (_userSel.Count >= 1) ? true : false;
            }
        }

        /// <summary>
        /// 窗体登陆
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserInformationDetailsManagement_Load(object sender, EventArgs e)
        {
            //保证登陆窗体的时候，其中不存在值
            _userSel.Clear();
            //给farpoint赋值
            GetUsersInfoDetails();
            //设置修改按钮是否启用
            this.tsBtnModify.Enabled = (_userSel.Count == 1) ? true : false;
            //设置修改按钮是否启用
            this.tsBtnDel.Enabled = (_userSel.Count >= 1) ? true : false;
        }

        /// <summary>
        /// 获取用户详细信息
        /// </summary>
        private void GetUsersInfoDetails()
        {
            //定义接受用户详细信息实体的数组
            UserInformationDetailsEnt[] ents = UserInformationDetailsManagementBLL.GetUserInformationDetails();
            //若为空，则返回
            if (ents == null)
            {
                return;
            }
            //记录条数
            int rowsCount = ents.Length;

            this.fpUserInformationDetails.ActiveSheet.Rows.Count = rowsCount + _fpRowsTitleCount;
            this.fpUserInformationDetails.ActiveSheet.Columns.Count = _fpColumnsTitleCount;
            this.fpUserInformationDetails.ActiveSheet.Columns[1].Visible = false;
            for (int i = 0; i < rowsCount; i++)
            {
                //选择
                this.fpUserInformationDetails.ActiveSheet.Cells[i + 2, 0].CellType = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
                this.fpUserInformationDetails.ActiveSheet.Cells[i + 2, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.fpUserInformationDetails.ActiveSheet.Cells[i + 2, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                //ID
                this.fpUserInformationDetails.ActiveSheet.Cells[i + 2, 1].Text = ents[i].ID.ToString();
                //姓名
                this.fpUserInformationDetails.ActiveSheet.Cells[i + 2, 2].Text = ents[i].Name;
                //手机号码
                this.fpUserInformationDetails.ActiveSheet.Cells[i + 2, 3].Text = DisPlayPhoneNumber(ents[i].PhoneNumber);
                //电话号码
                this.fpUserInformationDetails.ActiveSheet.Cells[i + 2, 4].Text = ents[i].TelePhoneNumber;
                //Email
                this.fpUserInformationDetails.ActiveSheet.Cells[i + 2, 5].Text = ents[i].Email;
                //所属部门
                this.fpUserInformationDetails.ActiveSheet.Cells[i + 2, 6].Text = ents[i].Depratment;
                //职位
                this.fpUserInformationDetails.ActiveSheet.Cells[i + 2, 7].Text = ents[i].Position;

                this.fpUserInformationDetails.ActiveSheet.Cells[i + 2, 8].Text = ents[i].IsInform == 0 ? "否" : "是";
                //备注
                this.fpUserInformationDetails.ActiveSheet.Cells[i + 2, 9].Text = ents[i].Remarks;
            }
            //设置焦点
            this.fpUserInformationDetails.ActiveSheet.SetActiveCell(1, 0);

            //记录当前farpoint的记录条数
            _fpRowsCount = this.fpUserInformationDetails.ActiveSheet.Rows.Count;
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void buttonOK_Click(object sender, EventArgs e)
        {
            if (_closeExternal)
            {
                OnButtonClickHandle(sender, e);
            }
            else
            {
                this.Close();
            }
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (_closeExternal)
            {
                OnCancleButtonClickHandle(sender, e);
            }
            else
            {
                this.Close();
            }
        }

        /// <summary>
        /// 将手机号码显示为138-0418-2343
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private string DisPlayPhoneNumber(string str)
        {
            StringBuilder strReturn = new StringBuilder();
            int i = 0;
            foreach (char letter in str)
            {
                i++;
                if (i == 4)
                {
                    strReturn.Append(LibCommon.Const.CHAR_IN_PHONENUMBER);
                }
                if (i == 8)
                {
                    strReturn.Append(LibCommon.Const.CHAR_IN_PHONENUMBER);
                }
                strReturn.Append(letter);
            }
            return strReturn.ToString();
        }
    }
}
