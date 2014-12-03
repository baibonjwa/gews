// ******************************************************************
// 概   述：部门信息管理
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
using LibCommon;
using LibDatabase;
using LibXPorperty;
using LibEntity;
using System.Reflection;
using FarPoint.Win.Spread.CellType;
using LibBusiness;

namespace LibCommonForm
{
    public partial class DepartmentInformation : Form
    {
        //定义接收选择值的变量
        List<string> _departmentSel = new List<string>();

        //定义Farpoint中的行列数
        const int _ifpRowTitleCount = 2;
        const int _ifpColumnsCount = 6;

        //定义Farpoint中部门名称的列Lable
        //部门名称
        const string _strDeptNameLable = "部门名称";
        //部门电话
        const string _strDeptTelLable = "部门电话";
        //部门邮箱
        const string _strDeptEmailLable = "部门Email";
        //部门人数
        const string _strDeptStuffCount = "员工人数";

        //定义个列值的最大字符串长度
        //部门名称
        const int _iDeptNameLength = 50;
        //部门电话
        const int _iDeptTelLength = 50;
        //部门邮箱
        const int _iDeptEmailLength = 50;

        #region 定义属性窗口
        private DepartmentParameter _srcEnt = null;
        private XProps _props = new XProps();
        //部门名称
        XProp _departmentName = new XProp();
        //部门电话
        XProp _departmentTel = new XProp();
        //部门邮箱
        XProp _departmentEmail = new XProp();
        //部门人数
        XProp _departmentStaff = new XProp();
        XProp _remarks = new XProp();
        List<XProp> _paramProp = new List<XProp>();

        const string CATEGORY_NAME_TITLE = "1.部门名称";
        const string CATEGORY_TEL_TITLE = "2.部门电话";
        const string CATEGORY_EMAIL_TYPE_TITLE = "3.部门邮箱";
        const string CATEGORY_STAFF_TYPE_TITLE = "4.员工人数";
        const string CATEGORY_REMARKS_TITLE = "5.备注";
        #endregion

        /// <summary>
        /// 初始化属性窗口
        /// </summary>
        void InitXPorps()
        {
            #region 基本信息
            //部门名称
            _departmentName.Category = CATEGORY_NAME_TITLE;
            _departmentName.Name = "部门名称";
            _departmentName.Description = "部门唯一标识，部门名称不能重复且wu";
            _departmentName.ReadOnly = true;
            _departmentName.ProType = typeof(string);
            _departmentName.Visible = true;
            _departmentName.Converter = null;
            _props.Add(_departmentName);

            //部门电话
            _departmentTel.Category = CATEGORY_TEL_TITLE;
            _departmentTel.Name = "部门电话";
            _departmentTel.Description = "部门电话若无，则无需添加";
            _departmentTel.ReadOnly = true;
            _departmentTel.ProType = typeof(string);
            _departmentTel.Visible = true;
            _departmentTel.Converter = null;
            _props.Add(_departmentTel);

            //部门邮箱
            _departmentEmail.Category = CATEGORY_EMAIL_TYPE_TITLE;
            _departmentEmail.Name = "部门Email";
            _departmentEmail.Description = "部门邮箱若无，则无需添加";
            _departmentEmail.ReadOnly = true;
            _departmentEmail.ProType = typeof(string);
            _departmentEmail.Visible = true;
            _departmentEmail.Converter = null;
            _props.Add(_departmentEmail);

            //员工人数
            _departmentStaff.Category = CATEGORY_STAFF_TYPE_TITLE;
            _departmentStaff.Name = "员工人数";
            _departmentStaff.Description = "显示该部门员工数目，详细信息请点击后方  \"···\"按钮";
            _departmentStaff.ReadOnly = true;
            _departmentStaff.ProType = typeof(string);
            _departmentStaff.Visible = true;
            _departmentStaff.Converter = null;
            _props.Add(_departmentStaff);

            //备注
            _remarks.Category = CATEGORY_REMARKS_TITLE;
            _remarks.Name = "备注";
            _remarks.ReadOnly = true;
            _remarks.ProType = typeof(string);
            _remarks.Visible = true;
            _remarks.Converter = null;
            _props.Add(_remarks);
            #endregion
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public DepartmentInformation()
        {
            InitializeComponent();

            //初始化属性窗口
            InitXPorps();

            //设置窗体和farpoint格式
            LibCommon.FarpointDefaultPropertiesSetter.SetFpDefaultProperties(_fpDepartmentInfo, LibCommon.LibFormTitles.DEPARTMENT_MANMAGEMENT, 2);
            LibCommon.FormDefaultPropertiesSetter.SetManagementFormDefaultProperties(this, LibCommon.LibFormTitles.DEPARTMENT_MANMAGEMENT);

            //判断用户权限
            if (CurrentUserEnt._curLoginUserInfo.Permission != Permission.管理员.ToString())
            {
                tsBtnAdd.Visible = false;
                tsBtnDel.Visible = false;
                propertyGrid.Enabled = false;
            }

            //设置Farpoint的默认行列数
            _fpDepartmentInfo.ActiveSheet.Rows.Count = _ifpRowTitleCount;
            _fpDepartmentInfo.ActiveSheet.Columns.Count = _ifpColumnsCount;
        }

        /// <summary>
        /// 设置属性框的值
        /// </summary>
        /// <param name="ent"></param>
        void SetPropertyGridEnt(DepartmentParameter ent)
        {
            if (ent == null)
            {
                return;
            }
            _srcEnt = ent.DeepClone();

            //设置属性窗口的显示值
            _departmentName.Value = ent.Name;
            _departmentName.ReadOnly = false;
            _departmentEmail.ReadOnly = false;
            _departmentTel.ReadOnly = false;
            _departmentStaff.ReadOnly = false;
            _remarks.ReadOnly = false;
            _departmentTel.Value = ent.Tel;
            _departmentEmail.Value = ent.Email;
            _departmentStaff.Value = ent.Staff;
            _remarks.Value = ent.Remark;

            int preCnt = _paramProp.Count;
            for (int i = 0; i < preCnt; i++)
            {
                _props.Remove(_paramProp[i]);
            }
            _paramProp.Clear();

            propertyGrid.SelectedObject = _props;
        }

        /// <summary>
        /// 清空属性框的值
        /// </summary>
        private void ClearPropertyGridSelEnt()
        {
            propertyGrid.SelectedObject = null;
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnPrint_Click(object sender, EventArgs e)
        {
            FilePrint.CommonPrint(_fpDepartmentInfo, 0);
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnExport_Click(object sender, EventArgs e)
        {
            if (FileExport.fileExport(_fpDepartmentInfo, true))
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
            DepartmentInformationInput ba = new DepartmentInformationInput();
            ba.ShowDialog();

            //加载信息
            GetDepartmentInfo();
        }

        /// <summary>
        /// 窗体登陆时加载数据,设置按钮是否可用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DepartmentInformation_Load(object sender, EventArgs e)
        {
            //加载信息
            GetDepartmentInfo();

            //清空选择
            _departmentSel.Clear();

            //设置按钮不启用
            tsBtnDel.Enabled = false;
            this.删除ToolStripMenuItem.Enabled = false;
        }

        /// <summary>
        /// 获取所有部门信息
        /// </summary>
        private void GetDepartmentInfo()
        {
            #region 删除垃圾数据
            while (_fpDepartmentInfo.ActiveSheet.Rows.Count > _ifpRowTitleCount)
            {
                _fpDepartmentInfo.ActiveSheet.Rows.Remove(_ifpRowTitleCount, 1);
            }
            #endregion

            //数据库中读取数据
            DataTable dt = DepartmentInformationManagementBLL.GetDeptInformation();
            if (dt != null)
            {
                //获取行列数
                int n = dt.Rows.Count;
                int k = dt.Columns.Count;
                for (int i = 0; i < n; i++)
                {
                    this._fpDepartmentInfo.ActiveSheet.Rows.Add(_ifpRowTitleCount + i, 1);
                    for (int j = 0; j < k; j++)
                    {
                        //设置单元格类型
                        this._fpDepartmentInfo.Sheets[0].Cells[i + 2, j + 1].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                        //锁定单元格
                        this._fpDepartmentInfo.Sheets[0].Cells[i + 2, j + 1].Locked = true;
                        //设置单元格显示文本
                        this._fpDepartmentInfo.Sheets[0].Cells[i + 2, j + 1].Text = dt.Rows[i][j].ToString();
                        //调整单元格对其方式
                        this._fpDepartmentInfo.Sheets[0].Cells[i + 2, j + 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                        this._fpDepartmentInfo.Sheets[0].Cells[i + 2, j + 1].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                    }

                    //设置第一列的值为checkbox,并设置格式
                    this._fpDepartmentInfo.Sheets[0].Cells[i + 2, 0].CellType = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
                    this._fpDepartmentInfo.Sheets[0].Cells[i + 2, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    this._fpDepartmentInfo.Sheets[0].Cells[i + 2, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                }
            }
            //设置单元格焦点
            this._fpDepartmentInfo.ActiveSheet.SetActiveCell(1, 0);
        }

        /// <summary>
        /// farpoint选择单元格变化时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpDepartmentInfo_SelectionChanged(object sender, FarPoint.Win.Spread.SelectionChangedEventArgs e)
        {
            //获取当前行索引
            int n = this._fpDepartmentInfo.ActiveSheet.ActiveCell.Row.Index;

            //当选择空行时,属性框为空
            if (CurrentUserEnt._curLoginUserInfo.Permission != Permission.普通用户.ToString())
            {
                propertyGrid.Enabled = this._fpDepartmentInfo.ActiveSheet.Cells[n, 1].Value == null ? false : true;
            }

            //若无数据则返回
            if (n < 2)
            {
                return;
            }

            //部门名称
            string name = "";
            //部门电话
            string tel = "";
            //部门邮箱
            string email = "";
            //部门人数
            string staff = "";
            //备注
            string remark = "";

            //从FarPoint上取值,检查值是否为空
            if (this._fpDepartmentInfo.ActiveSheet.Cells[n, 1].Value != null)
            {
                name = this._fpDepartmentInfo.ActiveSheet.Cells[n, 1].Value.ToString();
            }
            if (this._fpDepartmentInfo.ActiveSheet.Cells[n, 2].Value != null)
            {
                tel = this._fpDepartmentInfo.ActiveSheet.Cells[n, 2].Value.ToString();
            }
            if (this._fpDepartmentInfo.ActiveSheet.Cells[n, 3].Value != null)
            {
                email = this._fpDepartmentInfo.ActiveSheet.Cells[n, 3].Value.ToString();
            }
            if (this._fpDepartmentInfo.ActiveSheet.Cells[n, 4].Value != null)
            {
                staff = this._fpDepartmentInfo.ActiveSheet.Cells[n, 4].Value.ToString();
            }
            if (this._fpDepartmentInfo.ActiveSheet.Cells[n, 5].Value != null)
            {
                remark = this._fpDepartmentInfo.ActiveSheet.Cells[n, 5].Value.ToString();
            }

            //设置属性框的显示值
            DepartmentParameter dp = new DepartmentParameter(name, tel, email, staff, remark);
            //将值显示在属性窗口中
            SetPropertyGridEnt(dp);
        }

        /// <summary>
        /// 属性窗口的值发生变化
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void propertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            //获取fp中的当前行
            int currentRow = this._fpDepartmentInfo.ActiveSheet.ActiveCell.Row.Index;
            //定义变量,接收修改的值
            string changeValue = "";

            //存在修改值时
            if (e.ChangedItem.Value != null)
            {
                //修改值的标题
                string ss = e.ChangedItem.Label.ToString();
                //获取fp中修改值的列索引
                int columnIndex = this._fpDepartmentInfo.ActiveSheet.Columns[ss].Index;
                //获取修改的值
                changeValue = e.ChangedItem.Value.ToString();               

                if (ss == _strDeptNameLable)
                {
                    //检测特殊字符、是否为空、是否重复
                    if (LibCommon.Validator.checkSpecialCharacters(changeValue) ||
                        LibCommon.Validator.IsEmpty(changeValue) ||
                        DepartmentInformationManagementBLL.FindDeptInformationByDeptName(changeValue))
                    {
                        Alert.alert(LibCommon.Const.DEPT_NAME_IS_WRONG, LibCommon.Const.NOTES, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        fpDepartmentInfo_SelectionChanged(null, null);
                        return;
                    }

                    //检查字符串的长度
                    if (changeValue.Length > _iDeptNameLength)
                    {
                        Alert.alert(LibCommon.Const.TXT_IS_WRONG1 + _iDeptNameLength + LibCommon.Const.TXT_IS_WRONG2, LibCommon.Const.NOTES, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        fpDepartmentInfo_SelectionChanged(null, null);
                        return;
                    }
                }

                //验证邮箱
                if (ss==_strDeptEmailLable)
                {
                    //邮箱格式
                    if (!LibCommon.Validator.checkIsEmailAddress(changeValue))
                    {
                        Alert.alert(LibCommon.Const.DEPT_EMAIL_IS_WRONG, LibCommon.Const.NOTES, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        fpDepartmentInfo_SelectionChanged(null, null);
                        return;
                    }
                    //检查字符串的长度
                    if (changeValue.Length > _iDeptEmailLength)
                    {
                        Alert.alert(LibCommon.Const.TXT_IS_WRONG1 + _iDeptEmailLength + LibCommon.Const.TXT_IS_WRONG2, LibCommon.Const.NOTES, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        fpDepartmentInfo_SelectionChanged(null, null);
                        return;
                    }
                }

                //验证电话
                if (ss == _strDeptTelLable)
                {
                    //验证电话格式
                    if (!LibCommon.Validator.checkIsIsTelePhone(changeValue))
                    {
                        Alert.alert(LibCommon.Const.DEPT_TEL_IS_WRONG, LibCommon.Const.NOTES, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        fpDepartmentInfo_SelectionChanged(null, null);
                        return;
                    }

                    //检查字符串的长度
                    if (changeValue.Length > _iDeptTelLength)
                    {
                        Alert.alert(LibCommon.Const.TXT_IS_WRONG1 + _iDeptTelLength + LibCommon.Const.TXT_IS_WRONG2, LibCommon.Const.NOTES, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        fpDepartmentInfo_SelectionChanged(null, null);
                        return;
                    }
                }

                //验证人数必须是数字
                if (ss == _strDeptStuffCount)
                {
                    if (!LibCommon.Validator.IsNumeric(changeValue))
                    {
                        Alert.alert(LibCommon.Const.DEPT_STAFF_COUNT_IS_WRONG, LibCommon.Const.NOTES, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        fpDepartmentInfo_SelectionChanged(null, null);
                        return;
                    }
                }

                //Farpoint显示修改值
                this._fpDepartmentInfo.ActiveSheet.Cells[currentRow, columnIndex].Text = changeValue;

                //部门名称
                string name = "";
                //部门电话
                string tel = "";
                //部门邮箱
                string email = "";
                //部门人数
                string staff = "";
                //备注
                string remark = "";

                //从FarPoint上取值,检查值是否为空
                if (this._fpDepartmentInfo.ActiveSheet.Cells[currentRow, 1].Value != null)
                {
                    name = this._fpDepartmentInfo.ActiveSheet.Cells[currentRow, 1].Value.ToString();
                }
                if (this._fpDepartmentInfo.ActiveSheet.Cells[currentRow, 2].Value != null)
                {
                    tel = this._fpDepartmentInfo.ActiveSheet.Cells[currentRow, 2].Value.ToString();
                }
                if (this._fpDepartmentInfo.ActiveSheet.Cells[currentRow, 3].Value != null)
                {
                    email = this._fpDepartmentInfo.ActiveSheet.Cells[currentRow, 3].Value.ToString();
                }
                if (this._fpDepartmentInfo.ActiveSheet.Cells[currentRow, 4].Value != null)
                {
                    staff = this._fpDepartmentInfo.ActiveSheet.Cells[currentRow, 4].Value.ToString();
                }
                if (this._fpDepartmentInfo.ActiveSheet.Cells[currentRow, 5].Value != null)
                {
                    remark = this._fpDepartmentInfo.ActiveSheet.Cells[currentRow, 5].Value.ToString();
                }

                //旧的 部门名称
                string oldName = name;
                if (ss==_strDeptNameLable)
                {
                    oldName = e.OldValue.ToString();
                }

                 //定义 部门信息实体 接收新修改的数据
                DepartmentInformationEntity userDeptInfo = new DepartmentInformationEntity();
                //部门名称
                userDeptInfo.Name = name;
                //人数
                userDeptInfo.Staff = staff;
                //邮箱
                userDeptInfo.Email = email;
                //电话
                userDeptInfo.Tel = tel;
                //备注
                userDeptInfo.Remark = remark;

                //更新数据库
                DepartmentInformationManagementBLL.UpdateDepartmentInfomationDatabase(userDeptInfo, oldName);
            }
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
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnRefresh_Click(object sender, EventArgs e)
        {                        
            //加载数据         
            GetDepartmentInfo();
        }

        /// <summary>
        /// 从数据库中获取记录条数
        /// </summary>
        /// <returns></returns>
        private int GetTableRecordCount()
        {
            return DepartmentInformationManagementBLL.GetRecordCountFromTable();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnDel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(@"确认要删除部门信息吗？", "提示：", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int n = GetTableRecordCount();//获取表中记录条数
                for (int i = n - 1; i >= 0; i--)
                {
                    if (this._fpDepartmentInfo.ActiveSheet.Cells[2 + i, 0].CellType is CheckBoxCellType)
                    {
                        if (this._fpDepartmentInfo.ActiveSheet.Cells[2 + i, 0].Value != null)
                        {
                            if ((bool)this._fpDepartmentInfo.ActiveSheet.Cells[2 + i, 0].Value)
                            {
                                this._fpDepartmentInfo.ActiveSheet.Rows.Remove(2 + i, 1);
                            }
                        }
                    }
                }
                foreach (string str in _departmentSel)
                {
                    //删除数据库中部门信息
                    DepartmentInformationManagementBLL.DeleteDeptInformationByDeptName(str);
                    //更新用户详细信息表中部门信息
                    DepartmentInformationManagementBLL.UpdateUserInformationWhenDeleteDept(str);
                }
                //刷新
                _fpDepartmentInfo.Refresh();

                //设置焦点
                this._fpDepartmentInfo.ActiveSheet.SetActiveCell(DepartmentInformationManagementBLL.GetRecordCountFromTable() + 1, 0);

                //设置按钮不可用
                this.tsBtnDel.Enabled = false;
            }
        }

        /// <summary>
        /// farPoint单击 触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpDepartmentInfo_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (this._fpDepartmentInfo.ActiveSheet.Cells[e.Row, 1].Value != null)
            {
                // 判断点击的空间类型是否是.FpCheckBox)
                if (e.EditingControl is FarPoint.Win.FpCheckBox)
                {
                    FarPoint.Win.FpCheckBox fpChk = (FarPoint.Win.FpCheckBox)e.EditingControl;
                    // 判断是否被选中
                    if (fpChk.Checked)
                    {
                        // 保存索引号
                        if (!_departmentSel.Contains(this._fpDepartmentInfo.ActiveSheet.Cells[e.Row, 1].Value.ToString()))
                        {
                            _departmentSel.Add(this._fpDepartmentInfo.ActiveSheet.Cells[e.Row, 1].Value.ToString());
                            this._chkSelAll.CheckedChanged -= this.chkSelAll_CheckedChanged;
                            this._chkSelAll.Checked = _departmentSel.Count == DepartmentInformationManagementBLL.GetRecordCountFromTable() ? true : false;
                            this._chkSelAll.CheckedChanged += this.chkSelAll_CheckedChanged;
                        }
                    }
                    else
                    {
                        // 移除索引号
                        _departmentSel.Remove(this._fpDepartmentInfo.ActiveSheet.Cells[e.Row, 1].Value.ToString());
                        // 全选/全不选checkbox设为未选中
                        this._chkSelAll.CheckedChanged -= this.chkSelAll_CheckedChanged;
                        this._chkSelAll.Checked = false;
                        this._chkSelAll.CheckedChanged += this.chkSelAll_CheckedChanged;
                    }
                }
                // 删除按钮
                this.tsBtnDel.Enabled = (_departmentSel.Count >= 1) ? true : false;
                this.删除ToolStripMenuItem.Enabled = (_departmentSel.Count >= 1) ? true : false;
            }
        }

        /// <summary>
        /// 全选/全不选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkSelAll_CheckedChanged(object sender, EventArgs e)
        {
            //清除记录数据
            _departmentSel.Clear();
            int n = GetTableRecordCount();
            if (_chkSelAll.Checked)
            {
                for (int i = 0; i < n; i++)
                {
                    //设置farpoint选择列显示
                    this._fpDepartmentInfo.Sheets[0].Cells[2 + i, 0].Value = ((CheckBox)sender).Checked;
                    if (this._fpDepartmentInfo.Sheets[0].Cells[2 + i, 1].Value != null)
                    {
                        //添加已选择的记录
                        _departmentSel.Add(this._fpDepartmentInfo.Sheets[0].Cells[2 + i, 1].Value.ToString());
                    }
                }
            }
            else
            {
                for (int i = 0; i < n; i++)
                {
                    //设置farpoint选择列显示
                    this._fpDepartmentInfo.Sheets[0].Cells[2 + i, 0].Value = false;
                }
            }
            //设置删除按钮的显示
            this.tsBtnDel.Enabled = (_departmentSel.Count >= 1) ? true : false;
            this.删除ToolStripMenuItem.Enabled = (_departmentSel.Count >= 1) ? true : false;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 添加ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DepartmentInformationInput ba = new DepartmentInformationInput();
            ba.ShowDialog();

            //加载数据
            GetDepartmentInfo();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tsBtnDel_Click(sender, e);
        }

        /// <summary>
        /// 全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 全选ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _chkSelAll.Checked = true;
        }

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 刷新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tsBtnRefresh_Click(sender, e);
        }               

        /// <summary>
        /// 设置右键菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpDepartmentInfo_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                //右键
                if (e.Row >= 0)
                {
                    contextMenuStripRightButton.Show(MousePosition.X, MousePosition.Y);
                }
            }
        }

        /// <summary>
        /// 部门信息的参数,用于属性窗口的显示
        /// </summary>
        class DepartmentParameter
        {
            //部门名称
            private string _name;
            /// <summary>
            /// 获取设置部门名称
            /// </summary>
            public string Name
            {
                get { return _name; }
                set { _name = value; }
            }

            //部门电话
            private string _tel;
            /// <summary>
            /// 获取设置电话号码
            /// </summary>
            public string Tel
            {
                get { return _tel; }
                set { _tel = value; }
            }

            //部门邮箱
            private string _email;
            /// <summary>
            /// 获取设置部门邮箱
            /// </summary>
            public string Email
            {
                get { return _email; }
                set { _email = value; }
            }

            //部门员工数
            private string _staff;
            /// <summary>
            /// 获取设置部门员工数
            /// </summary>
            public string Staff
            {
                get { return _staff; }
                set { _staff = value; }
            }

            //备注
            private string _remark;
            /// <summary>
            /// 获取设置备注
            /// </summary>
            public string Remark
            {
                get { return _remark; }
                set { _remark = value; }
            }

            /// <summary>
            /// 构造函数
            /// </summary>
            /// <param name="name"></param>
            /// <param name="tel"></param>
            /// <param name="email"></param>
            /// <param name="staff"></param>
            /// <param name="remark"></param>
            public DepartmentParameter(string name, string tel, string email, string staff, string remark)
            {
                _name = name;
                _tel = tel;
                _email = email;
                _staff = staff;
                _remark = remark;
            }

            /// <summary>
            /// 默认构造函数
            /// </summary>
            public DepartmentParameter()
            {

            }

            /// <summary>
            /// 深度克隆
            /// </summary>
            /// <returns></returns>
            public DepartmentParameter DeepClone()
            {
                DepartmentParameter cpyEnt = new DepartmentParameter();
                cpyEnt.Name = Name;
                cpyEnt.Tel = Tel;
                cpyEnt.Email = Email;
                cpyEnt.Staff = Staff;
                cpyEnt.Remark = Remark;
                return cpyEnt;
            }
        }
    }
}
