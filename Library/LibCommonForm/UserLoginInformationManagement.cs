// ******************************************************************
// 概  述：用户登录信息管理
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
    public partial class UserLoginInformationManagement : Form
    {
        //存放已选择LoginName
        public static List<string> _userSel = new List<string>();
        //定义变量，存放fp的行数
        int _fpRowsCount = 0;
        //定义标题占用行数
        int _fpTitleRowCount = 2;
        //定义列数量
        int _columnsCount = 8;
        //需要过滤的列索引
        private int[] _filterColunmIdxs = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        public UserLoginInformationManagement()
        {
            InitializeComponent();
            //设置窗体和farpoint格式
            LibCommon.FormDefaultPropertiesSetter.SetManagementFormDefaultProperties(this, LibCommon.LibFormTitles.USER_LOGIN_INFO_MANMAGEMENT);
            LibCommon.FarpointDefaultPropertiesSetter.SetFpDefaultProperties(fpUserLoginInformation, LibCommon.LibFarpintTiltes.USER_LOGIN_INFO, 2);

            #region Farpoint自动过滤功能
            //初始化需要过滤功能的列
            _filterColunmIdxs = new int[]
            {
                4,
            };
            //禁用选择颜色相关控件
            farpointFilter1.EnableChooseColorCtrls(false);
            //设置自动隐藏过滤条件
            FarpointDefaultPropertiesSetter.SetFpFilterHideProperties(this.fpUserLoginInformation, _filterColunmIdxs);
            #endregion
        }

        /// <summary>
        /// 窗体登陆时触发，清除_userSel中的值，加载数据，设置按钮是否启用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserLoginInformationManagement_Load(object sender, EventArgs e)
        {
            //保证登陆窗体的时候，其中不存在值
            _userSel.Clear();
            //给farpoint赋值
            GetUserLoginInfo();

            //判断用户权限
            if (CurrentUser.CurLoginUserInfo.Permission != Permission.管理员.ToString())
            {
                tsBtnAdd.Visible = false;
                tsBtnDel.Visible = false;
                tsBtnModify.Visible = false;
            }
            //设置修改按钮是否启用
            this.tsBtnModify.Enabled = (_userSel.Count == 1) ? true : false;
            //设置修改按钮是否启用
            this.tsBtnDel.Enabled = (_userSel.Count >= 1) ? true : false;

        }

        /// <summary>
        /// 获取所有登录用户信息
        /// </summary>
        private void GetUserLoginInfo()
        {
            //sql语句,从数据库中获取数据
            UserLogin[] ents = UserLogin.FindAll();
            if (ents == null)
            {
                return;
            }
            //获取记录条数
            int rowsCount = ents.Length;

            fpUserLoginInformation.ActiveSheet.Rows.Count = rowsCount + _fpTitleRowCount;
            fpUserLoginInformation.ActiveSheet.Columns.Count = _columnsCount;

            for (int i = 0; i < rowsCount; i++)
            {
                //选择
                this.fpUserLoginInformation.ActiveSheet.Cells[i + 2, 0].CellType = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
                this.fpUserLoginInformation.ActiveSheet.Cells[i + 2, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.fpUserLoginInformation.ActiveSheet.Cells[i + 2, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                //登录名
                this.fpUserLoginInformation.ActiveSheet.Cells[i + 2, 1].Text = ents[i].LoginName;
                //密码，让密码显示为“******”
                //this.fpUserLoginInformation.ActiveSheet.Cells[i + 2, 2].Text = ents[i].PassWord;
                this.fpUserLoginInformation.ActiveSheet.Cells[i + 2, 2].Text = new string(LibCommon.Const.PASSWORD_CHAR, 6);
                this.fpUserLoginInformation.ActiveSheet.Cells[i + 2, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.fpUserLoginInformation.ActiveSheet.Cells[i + 2, 2].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                //权限
                this.fpUserLoginInformation.ActiveSheet.Cells[i + 2, 3].Text = ents[i].Permission;
                //所属用户组
                this.fpUserLoginInformation.ActiveSheet.Cells[i + 2, 4].Text = ents[i].GroupName;
                //记住密码，“√”为True
                this.fpUserLoginInformation.ActiveSheet.Cells[i + 2, 5].Text = ents[i].IsSavePassWord.ToString();
                this.fpUserLoginInformation.ActiveSheet.Cells[i + 2, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.fpUserLoginInformation.ActiveSheet.Cells[i + 2, 5].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

                this.fpUserLoginInformation.ActiveSheet.Cells[i + 2, 6].Text = ents[i].IsLogined.ToString();
                this.fpUserLoginInformation.ActiveSheet.Cells[i + 2, 6].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.fpUserLoginInformation.ActiveSheet.Cells[i + 2, 6].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                //备注
                this.fpUserLoginInformation.ActiveSheet.Cells[i + 2, 7].Text = ents[i].Remarks;
            }
            //设置焦点
            this.fpUserLoginInformation.ActiveSheet.SetActiveCell(1, 0);
            //记录当前farpoint的记录条数
            _fpRowsCount = this.fpUserLoginInformation.ActiveSheet.Rows.Count;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnAdd_Click(object sender, EventArgs e)
        {
            //添加,实例化方法中，null则认为是添加
            UserLoginInformationInput ulii = new UserLoginInformationInput(null);
            ulii.ShowDialog();
            //重新获取用户信息到显示界面
            GetUserLoginInfo();
            //设置焦点
            //this.fpUserLoginInformation.ActiveSheet.SetActiveCell(this.fpUserLoginInformation.ActiveSheet.Rows.Count, 0);
        }

        /// <summary>
        /// fp的单元格单击事件,设置CheckBoxCell的值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpUserLoginInformation_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            // 判断点击的空间类型是否是.FpCheckBox)
            if (e.EditingControl is FarPoint.Win.FpCheckBox)
            {
                FarPoint.Win.FpCheckBox fpChk = (FarPoint.Win.FpCheckBox)e.EditingControl;
                // 判断是否被选中
                if (fpChk.Checked)
                {
                    if (!_userSel.Contains(this.fpUserLoginInformation.ActiveSheet.Cells[e.Row, 1].Value.ToString()))
                    {
                        //添加  记录用户信息
                        _userSel.Add(this.fpUserLoginInformation.ActiveSheet.Cells[e.Row, 1].Value.ToString());
                        //checkbox赋值前，先移除CheckedChanged事件，然后加上
                        this.chkSelAll.CheckedChanged -= this.chkSelAll_CheckedChanged;
                        chkSelAll.Checked = (_userSel.Count == _fpRowsCount - _fpTitleRowCount) ? true : false;
                        this.chkSelAll.CheckedChanged += this.chkSelAll_CheckedChanged;
                    }
                }
                else
                {
                    // 移除索引号
                    _userSel.Remove(this.fpUserLoginInformation.ActiveSheet.Cells[e.Row, 1].Value.ToString());
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
        /// 全选/全不选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkSelAll_CheckedChanged(object sender, EventArgs e)
        {
            //清除  记录用户信息的数组，以便于重新赋值
            _userSel.Clear();
            //获取用户登录信息记录条数
            //int rowCount = LoginFormBLL.GetRecordCountFromTable();//带有有改动，测试
            int rowCount = this.fpUserLoginInformation.ActiveSheet.Rows.Count - _fpTitleRowCount;
            //设置全选/全不选
            if (chkSelAll.Checked)
            {
                for (int i = 0; i < rowCount; i++)
                {
                    //设置farPoint中checkbox显示为全选
                    this.fpUserLoginInformation.Sheets[0].Cells[2 + i, 0].Value = ((CheckBox)sender).Checked;
                    //记录用户信息
                    _userSel.Add(this.fpUserLoginInformation.Sheets[0].Cells[2 + i, 1].Value.ToString());
                }
            }
            else
            {//设置farPoint中checkbox显示为全不选
                for (int i = 0; i < rowCount; i++)
                {
                    this.fpUserLoginInformation.Sheets[0].Cells[2 + i, 0].Value = false;
                }
            }
            //设置删除按钮是否启用
            this.tsBtnDel.Enabled = (_userSel.Count >= 1) ? true : false;
            //设置修改按钮是否启用
            this.tsBtnModify.Enabled = (_userSel.Count == 1) ? true : false;
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnPrint_Click(object sender, EventArgs e)
        {
            FilePrint.CommonPrint(this.fpUserLoginInformation, 0);
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnExport_Click(object sender, EventArgs e)
        {
            if (FileExport.fileExport(fpUserLoginInformation, true))
            {
                Alert.alert(Const.EXPORT_SUCCESS_MSG);
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnModify_Click(object sender, EventArgs e)
        {
            //管理界面没有选择时，返回
            if (UserLoginInformationManagement._userSel.Count == 0)
            {
                return;
            }

            //定义  用户登录信息实体，接受旧值，添加到窗体中。旧值来源于管理界面的选择值。可以直接取值，也可从数据库取值，暂不考虑效率。
            UserLogin ent = UserLogin.FindOneByLoginName(UserLoginInformationManagement._userSel[0]);

            //修改
            UserLoginInformationInput ulii = new UserLoginInformationInput(ent);
            ulii.ShowDialog();
            //刷新显示的用户信息
            GetUserLoginInfo();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnDel_Click(object sender, EventArgs e)
        {
            if (Alert.confirm(LibCommon.Const.DELETE_USER_INFO, LibCommon.Const.NOTES, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                //获取登录用户记录条数
                //int n = LoginFormBLL.GetRecordCountFromTable();
                int n = this.fpUserLoginInformation.ActiveSheet.Rows.Count - _fpTitleRowCount;
                //从数据末尾删除数据
                for (int i = n - 1; i >= 0; i--)
                {
                    if (this.fpUserLoginInformation.ActiveSheet.Cells[2 + i, 0].CellType is FarPoint.Win.Spread.CellType.CheckBoxCellType)
                    {
                        if (this.fpUserLoginInformation.ActiveSheet.Cells[2 + i, 0].Value != null)
                        {
                            if ((bool)this.fpUserLoginInformation.ActiveSheet.Cells[2 + i, 0].Value)
                            {
                                this.fpUserLoginInformation.ActiveSheet.Rows.Remove(2 + i, 1);
                                //删除一行数据后,在数据末尾加一行     
                                //this.fpUserLoginInformation.ActiveSheet.Rows.Add(n - 1 + 2, 1);                           
                                //this.fpUserLoginInformation.ActiveSheet.Rows[n - 1 + 2].BackColor = Color.Red;//显示颜色直观显示追加行位置
                            }
                        }
                    }
                }
                //遍历 记录用户信息的数组，删除数据库中的数据
                foreach (string str in _userSel)
                {
                    var userLogin = UserLogin.FindOneByLoginName(str);
                    userLogin.Delete();
                }
                fpUserLoginInformation.Refresh();
                this.fpUserLoginInformation.ActiveSheet.SetActiveCell(UserLogin.Count() + 1, 0);

                //清除所有选择数据
                _userSel.Clear();

                //设置删除按钮是否启用
                this.tsBtnDel.Enabled = false;
                //设置修改按钮是否启用
                this.tsBtnModify.Enabled = false;

                //记录当前farpoint的记录条数
                _fpRowsCount = this.fpUserLoginInformation.ActiveSheet.Rows.Count;
            }
        }

        /// <summary>
        /// 刷新后，重新load数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnRefresh_Click(object sender, EventArgs e)
        {
            GetUserLoginInfo();
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
        private void buttonOK_Click(object sender, EventArgs e)
        {
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
                FarpointDefaultPropertiesSetter.SetFpFilterHideProperties(this.fpUserLoginInformation, _filterColunmIdxs);

            }
            else//未选中时，根据用户自定义的颜色进行分类显示
            {
                //启用选择颜色相关控件
                farpointFilter1.EnableChooseColorCtrls(true);
                //设置自定义过滤条件
                FarpointDefaultPropertiesSetter.SetFpCustomFilterProperties(this.fpUserLoginInformation, farpointFilter1.GetSelectedFitColor(), farpointFilter1.GetSelectedNotFitColor(), _filterColunmIdxs);
            }
        }

        private void farpointFilter1_OnClickClearFilterBtn(object sender, EventArgs arg)
        {
            //清空过滤条件
            this.fpUserLoginInformation.ActiveSheet.RowFilter.ResetFilter();
        }

        private void farpointFilter1_OnClickFitColorBtnOK(object sender, EventArgs arg)
        {
            //根据新的颜色值设置自动隐藏过滤条件
            FarpointDefaultPropertiesSetter.SetFpCustomFilterProperties(this.fpUserLoginInformation, farpointFilter1.GetSelectedFitColor(), farpointFilter1.GetSelectedNotFitColor(), _filterColunmIdxs);
        }

        private void farpointFilter1_OnClickNotFitColorBtnOK(object sender, EventArgs arg)
        {
            //根据新的颜色值设置自动隐藏过滤条件
            FarpointDefaultPropertiesSetter.SetFpCustomFilterProperties(this.fpUserLoginInformation, farpointFilter1.GetSelectedFitColor(), farpointFilter1.GetSelectedNotFitColor(), _filterColunmIdxs);
        }
        #endregion
    }
}
