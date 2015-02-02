// ******************************************************************
// 概  述：队别信息管理
// 作  者：宋英杰
// 创建日期：2014/3/11
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************

using System;
using System.Data;
using System.Windows.Forms;
using LibBusiness;
using LibCommon;
using LibEntity;

namespace LibPanels
{
    public partial class TeamManagement : Form
    {
        #region ******变量声明******
        private int _iRecordCount = 0;
        /**数据行数**/
        int _rowsCount = 0;
        /**选择行数**/
        int _checkCount = 0;
        /**表头冻结行数**/
        int _rowDetailStartIndex = 3;
        /**修改行号（修改时重新设置焦点用）**/
        int _tmpRowIndex = 0;
        /**队别实体**/
        Team teamEntity = new Team();
        /**接分页查询数据**/
        DataSet _ds = new DataSet();
        #endregion

        /// <summary>
        /// 构造方法
        /// </summary>
        public TeamManagement()
        {
            InitializeComponent();

            //※分页必须
            dataPager1.FrmChild_EventHandler += FrmParent_EventHandler;

            //窗体属性设置
            LibCommon.FormDefaultPropertiesSetter.SetManagementFormDefaultProperties(this, Const_MS.TEAM_INFO_MANAGEMENT);

            //Farpoint属性设置
            LibCommon.FarpointDefaultPropertiesSetter.SetFpDefaultProperties(fpTeam, LibCommon.Const_MS.TEAM_INFO_FARPOINT_TITLE, _rowDetailStartIndex);
        }

        /// <summary>
        /// 调用委托方法
        /// </summary>
        /// <param name="sender"></param>
        private void FrmParent_EventHandler(object sender)
        {
            //绑定数据
            bindFpTeam();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TeamManagement_Load(object sender, EventArgs e)
        {
            //绑定数据
            bindFpTeam();
        }

        /// <summary>
        /// farpoint数据绑定
        /// </summary>
        private void bindFpTeam()
        {
            //清空Farpoint
            FarPointOperate.farPointClear(fpTeam, _rowDetailStartIndex, _rowsCount);

            //选择数清空
            _checkCount = 0;

            //全选取消选择
            chkSelAll.Checked = false;

            //※分页必须
            _iRecordCount = Team.GetTotalCount();

            // ※分页必须
            dataPager1.PageControlInit(_iRecordCount);

            int iStartIndex = dataPager1.getStartIndex();
            int iEndIndex = dataPager1.getEndIndex();

            _ds = TeamBll.selectAllTeamInfo(iStartIndex, iEndIndex);
            _rowsCount = _ds.Tables[0].Rows.Count;

            //Farpoint重新绘制
            FarPointOperate.farPointReAdd(fpTeam, _rowDetailStartIndex, _rowsCount);
            if (_rowsCount > 0)
            {
                FarPoint.Win.Spread.CellType.CheckBoxCellType ckbxcell = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
                //checkbox取消三选
                ckbxcell.ThreeState = false;
                for (int i = 0; i < _ds.Tables[0].Rows.Count; i++)
                {
                    int index = 0;
                    //选择
                    this.fpTeam.Sheets[0].Cells[_rowDetailStartIndex + i, index].CellType = ckbxcell;
                    //队别名称
                    this.fpTeam.Sheets[0].Cells[_rowDetailStartIndex + i, ++index].Text = _ds.Tables[0].Rows[i][TeamDbConstNames.TEAM_NAME].ToString();
                    //队长姓名
                    this.fpTeam.Sheets[0].Cells[_rowDetailStartIndex + i, ++index].Text = _ds.Tables[0].Rows[i][TeamDbConstNames.TEAM_LEADER].ToString();

                    this.fpTeam.ActiveSheet.AddSpanCell(_rowDetailStartIndex + i, 3, 1, 5);
                    //队员姓名
                    this.fpTeam.Sheets[0].Cells[_rowDetailStartIndex + i, ++index].Text = _ds.Tables[0].Rows[i][TeamDbConstNames.TEAM_MEMBER].ToString();
                }
                //设置按钮可操作性
                setButtenEnable();
            }
        }

        /// <summary>
        /// farpoint中checkbox选中对全选反选的影响
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpTeam_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.EditingControl is FarPoint.Win.FpCheckBox)
            {
                FarPoint.Win.FpCheckBox fpChk = (FarPoint.Win.FpCheckBox)e.EditingControl;
                //选择数改变
                if (fpChk.Checked)
                {
                    _checkCount++;
                }
                else
                {
                    _checkCount--;
                }
            }
            //全选选择框变化
            if (_checkCount == _rowsCount)
            {
                chkSelAll.Checked = true;
            }
            else
            {
                chkSelAll.Checked = false;
            }
            //设置按钮可操作性
            setButtenEnable();
        }

        /// <summary>
        /// 设置按钮可操作性
        /// </summary>
        private void setButtenEnable()
        {
            //修改在只选择一条数据时可用
            if (_checkCount == 1)
            {
                tsBtnModify.Enabled = true;
            }
            else
            {
                tsBtnModify.Enabled = false;
            }

            //删除在选择1条以上数据时可用
            if (_checkCount > 0)
            {
                tsBtnDel.Enabled = true;
            }
            else
            {
                tsBtnDel.Enabled = false;
            }
        }

        /// <summary>
        /// 为teamInfoEntity赋值
        /// </summary>
        private void setTeamInfoEntityValue()
        {
            for (int i = 0; i < _rowsCount; i++)
            {
                if (fpTeam.Sheets[0].Cells[_rowDetailStartIndex + i, 0].Value != null && (bool)fpTeam.Sheets[0].Cells[_rowDetailStartIndex + i, 0].Value == true)
                {
                    //获取当然
                    _tmpRowIndex = _rowDetailStartIndex + i;
                    int index = 0;
                    //队别ID
                    int id = 0;
                    int.TryParse(_ds.Tables[0].Rows[i][TeamDbConstNames.ID].ToString(), out id);
                    teamEntity.TeamId = id;
                    //队别名称
                    teamEntity.TeamName = this.fpTeam.Sheets[0].Cells[_rowDetailStartIndex + i, ++index].Text;
                    //队长姓名
                    teamEntity.TeamLeader = this.fpTeam.Sheets[0].Cells[_rowDetailStartIndex + i, ++index].Text;
                    //队员姓名
                    teamEntity.TeamMember = this.fpTeam.Sheets[0].Cells[_rowDetailStartIndex + i, ++index].Text;
                }
            }
        }

        /// <summary>
        /// 添加按钮响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnAdd_Click(object sender, EventArgs e)
        {
            TeamInfoEntering teamInfoForm = new TeamInfoEntering();
            if (DialogResult.OK == teamInfoForm.ShowDialog())
            {
                //绑定数据
                bindFpTeam();
                //跳转到最后一页
                this.dataPager1.btnLastPage_Click(sender, e);
                //添加后重置farpoint焦点
                FarPointOperate.farPointFocusSetAdd(fpTeam, _rowDetailStartIndex, _rowsCount);
            }
        }

        /// <summary>
        /// 修改按钮响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnModify_Click(object sender, EventArgs e)
        {
            //为队别实体赋值
            setTeamInfoEntityValue();

            TeamInfoEntering teamInfoForm = new TeamInfoEntering(teamEntity);
            if (DialogResult.OK == teamInfoForm.ShowDialog())
            {
                //绑定数据
                bindFpTeam();
                //修改后重置farpoint焦点
                FarPointOperate.farPointFocusSetChange(fpTeam, _tmpRowIndex);
            }
        }

        /// <summary>
        /// 删除按钮响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnDel_Click(object sender, EventArgs e)
        {
            //确定删除
            bool result = Alert.confirm(Const.DEL_CONFIRM_MSG);

            //确定
            if (result == true)
            {
                bool bResult = false;
                _tmpRowIndex = fpTeam.Sheets[0].ActiveRowIndex;
                for (int i = 0; i < _rowsCount; i++)
                {
                    //遍历“选择”是否选中
                    if (fpTeam.Sheets[0].Cells[_rowDetailStartIndex + i, 0].Value != null && (bool)fpTeam.Sheets[0].Cells[_rowDetailStartIndex + i, 0].Value == true)
                    {
                        //掘进ID
                        teamEntity.TeamId = (int)_ds.Tables[0].Rows[i][TeamDbConstNames.ID];
                        //删除操作
                        bResult = TeamBll.deleteTeamInfo(teamEntity);
                    }
                }
                if (bResult)
                {
                    //TODO:删除成功后事件
                }
                //绑定数据
                bindFpTeam();

                //删除后重设Farpoint焦点
                FarPointOperate.farPointFocusSetDel(fpTeam, _tmpRowIndex);

                /*******************根据杨小颖意见，删除操作后提示信息*************/
                ////删除成功
                //if (bResult)
                //{
                //    //绑定数据
                //    bindFpTeam();
                //}
                ////删除失败
                //else
                //{
                //    Alert.alert(Const_MS.MSG_DELETE_FAILURE);
                //}
                /******************************************************************/
            }
            return;
        }

        /// <summary>
        /// 刷新按钮响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnRefresh_Click(object sender, EventArgs e)
        {
            //绑定数据
            bindFpTeam();
        }

        /// <summary>
        /// 退出按钮响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnExit_Click(object sender, EventArgs e)
        {
            //关闭窗体
            this.Close();
        }

        /// <summary>
        /// 全选反选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkSelAll_Click(object sender, EventArgs e)
        {
            //farpoint有数据时
            if (_rowsCount > 0)
            {
                //遍历数据
                for (int i = 0; i < _rowsCount; i++)
                {
                    //checkbox选中
                    if (chkSelAll.Checked)
                    {
                        fpTeam.Sheets[0].Cells[_rowDetailStartIndex + i, 0].Value = ((CheckBox)sender).Checked;
                        _checkCount = _ds.Tables[0].Rows.Count;
                    }
                    //checkbox未选中
                    else
                    {
                        fpTeam.Sheets[0].Cells[_rowDetailStartIndex + i, 0].Value = ((CheckBox)sender).Checked;
                        _checkCount = 0;
                    }
                }
            }
            //设置按钮可操作性
            setButtenEnable();
        }

        /// <summary>
        /// 确定按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            //关闭窗体
            this.Close();
        }

        /// <summary>
        /// 取消按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            //关闭窗体
            this.Close();
        }

        /// <summary>
        /// 导出按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnExport_Click(object sender, EventArgs e)
        {
            if (FileExport.fileExport(fpTeam, true))
            {
                Alert.alert(Const.EXPORT_SUCCESS_MSG);
            }
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnPrint_Click(object sender, EventArgs e)
        {
            //打印
            FilePrint.CommonPrint(fpTeam, 0);
        }
    }
}
