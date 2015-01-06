// ******************************************************************
// 概  述：探头数据管理
// 作  者：伍鑫
// 创建日期：2013/12/01
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************

using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Windows.Forms;
using FarPoint.Win;
using FarPoint.Win.Spread;
using FarPoint.Win.Spread.CellType;
using LibCommon;
using LibCommonControl;
using LibEntity;

namespace LibPanels
{
    public partial class ProbeInfoManagement : BaseForm
    {
        /** 明细部开始index位置 **/
        private const int _iRowDetailStartIndex = 4;
        private readonly int[] _filterColunmIdxs;
        /** 保存所有用户选中的行的索引 **/
        private readonly Hashtable _htSelIdxs = new Hashtable();
        /** 检索件数**/
        private int _iRowCount;
        /** 主键index **/
        private int _primaryKeyIndex = 1;
        /** 需要过滤的列索引 **/

        /// <summary>
        ///     构造方法
        /// </summary>
        public ProbeInfoManagement(MainFrm mainFrm)
        {
            MainForm = mainFrm;
            InitializeComponent();

            //分配用户权限
            if (CurrentUser.CurLoginUserInfo.Permission != Permission.管理员.ToString())
            {
                btnAdd.Visible = false;
                btnUpdate.Visible = false;
                btnDelete.Visible = false;
            }

            // 设置窗体默认属性
            FormDefaultPropertiesSetter.SetManagementFormDefaultProperties(this, Const_GE.MANAGE_PROBE_INFO);

            // 设置Farpoint默认属性
            FarpointDefaultPropertiesSetter.SetFpDefaultProperties(fpProbeInfo, Const_GE.MANAGE_PROBE_INFO,
                _iRowDetailStartIndex);

            // 调用委托方法 （必须实装）
            dataPager1.FrmChild_EventHandler += FrmParent_EventHandler;

            // 加载探头信息
            loadProbeInfo();

            #region Farpoint自动过滤功能

            //初始化需要过滤功能的列
            _filterColunmIdxs = new[]
            {
                3,
                8,
                9,
                10,
                11,
                12
            };
            //设置自动隐藏过滤条件
            FarpointDefaultPropertiesSetter.SetFpFilterHideProperties(fpProbeInfo, _filterColunmIdxs);

            #endregion
        }

        /// <summary>
        ///     调用委托方法 （必须实装）
        /// </summary>
        /// <param name="sender"></param>
        private void FrmParent_EventHandler(object sender)
        {
            // 分页控件与Farpoint过滤绑定问题
            FarpointFilterSetter.ClearFpFilter(fpProbeInfo);

            // 加载探头信息
            loadProbeInfo();
        }

        /// <summary>
        ///     加载探头信息
        /// </summary>
        private void loadProbeInfo()
        {
            // 修改按钮设为不可用（必须实装）
            btnUpdate.Enabled = false;
            // 删除按钮设为不可用（必须实装）
            btnDelete.Enabled = false;
            // 全选/全不选checkbox设为未选中（必须实装）
            chkSelAll.Checked = false;

            // 清空HashTabl（必须实装）
            _htSelIdxs.Clear();

            // 删除farpoint明细部（必须实装）
            // 解决修改、删除某条数据后，重新load的时候，选择列checkbox不恢复成默认（不选择）的BUG
            // 解决删除全部数据后，再添加一行，报错的BUG
            if (fpProbeInfo.Sheets[0].Rows.Count != _iRowDetailStartIndex)
            {
                fpProbeInfo.Sheets[0].Rows.Remove(_iRowDetailStartIndex, _iRowCount);
            }
            else
            {
                _iRowCount = 0;
            }

            // 获取全部数据件数（必须实装）
            int iRecordCount = Probe.FindAll().Length;

            // 调用分页控件初始化方法（必须实装）
            dataPager1.PageControlInit(iRecordCount);

            // 获取要检索数据的开始位置和结束位置 （必须实装）
            int iStartIndex = dataPager1.getStartIndex();
            int iEndIndex = dataPager1.getEndIndex();

            // 获取开始位置和结束位置之间的数据（必须实装）
            // 说明：如果画面当前显示的件数是10，那么init时开始位置为1，结束位置为10，点击下一页后，开始位置变为11，结束位置变为20
            Probe[] probes = Probe.SlicedFindAll(iStartIndex, iEndIndex);

            // 当前检索件数（必须实装）
            int iSelCnt = probes.Length;

            // 重新设定farpoint显示行数 （必须实装）
            fpProbeInfo.Sheets[0].Rows.Count = _iRowDetailStartIndex + iSelCnt;

            // 检索件数 > 0 的场合
            if (iSelCnt > 0)
            {
                // 当前检索件数（必须实装）
                _iRowCount = iSelCnt;

                // 循环结果集
                for (int i = 0; i < iSelCnt; i++)
                {
                    int index = 0;
                    // 选择
                    var objCheckCell = new CheckBoxCellType();
                    fpProbeInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, index].CellType = objCheckCell;

                    // 探头编号
                    fpProbeInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, ++index].Text = probes[i].ProbeId;
                    // 探头名称
                    fpProbeInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, ++index].Text = probes[i].ProbeName;

                    // 探头显示名称
                    fpProbeInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, ++index].Text =
                        probes[i].ProbeTypeDisplayName;

                    // 探头位置坐标X
                    fpProbeInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, ++index].Text =
                        probes[i].ProbeLocationX.ToString();

                    // 探头位置坐标Y
                    fpProbeInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, ++index].Text =
                        probes[i].ProbeLocationY.ToString();

                    // 探头位置坐标Z
                    fpProbeInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, ++index].Text =
                        probes[i].ProbeLocationZ.ToString();

                    // 探头描述
                    fpProbeInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, ++index].Text =
                        probes[i].ProbeDescription;

                    // 矿井名称
                    fpProbeInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, ++index].Text = "";
                    // 水平
                    fpProbeInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, ++index].Text = "";
                    // 采区
                    fpProbeInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, ++index].Text = "";
                    // 工作面
                    fpProbeInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, ++index].Text = "";
                    // 巷道名称
                    fpProbeInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, ++index].Text = "";

                    if (probes[i].Tunnel != null)
                    {
                        // 矿井名称
                        fpProbeInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, index - 4].Text =
                            probes[i].Tunnel.WorkingFace.MiningArea.Horizontal.Mine.MineName; //.MineName;
                        // 水平
                        fpProbeInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, index - 3].Text =
                            probes[i].Tunnel.WorkingFace.MiningArea.Horizontal.HorizontalName; //.HorizontalName;
                        // 采区
                        fpProbeInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, index - 2].Text =
                            probes[i].Tunnel.WorkingFace.MiningArea.MiningAreaName; //MiningAreaName;
                        // 工作面
                        fpProbeInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, index - 1].Text =
                            probes[i].Tunnel.WorkingFace.WorkingFaceName; //WorkingFaceName;
                        // 巷道名称
                        fpProbeInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, index].Text =
                            probes[i].Tunnel.TunnelName;
                    }


                    // 是否自动位移
                    if (probes[i].IsMove == 1)
                    {
                        fpProbeInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, ++index].Text = "是";
                        // 距迎头距离
                        fpProbeInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, ++index].Text =
                            probes[i].FarFromFrontal.ToString();
                    }
                    else
                    {
                        fpProbeInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, ++index].Text = "否";
                        // 距迎头距离
                        fpProbeInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, ++index].Text = "";
                    }
                }
            }
        }

        /// <summary>
        ///     添加（必须实装）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            var probeInfoEnteringForm = new ProbeInfoEntering(MainForm);
            if (DialogResult.OK == probeInfoEnteringForm.ShowDialog())
            {
                // 加载探头信息
                loadProbeInfo();
                // 跳转到尾页（必须实装）
                dataPager1.btnLastPage_Click(sender, e);
                // #TODO:暂时添加成功后不设计焦点，因为探头编号是可以输入的，无法根据其进行排序定位。
                //// 设置farpoint焦点（必须实装）
                //this.fpProbeInfo.Sheets[0].SetActiveCell(this.fpProbeInfo.Sheets[0].Rows.Count, 0);
            }
        }

        /// <summary>
        ///     修改（必须实装）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // 获取已选择明细行的索引
            int[] iSelIdxsArr = GetSelIdxs();
            // 获取编号（主键）
            string strPrimaryKey = fpProbeInfo.Sheets[0].Cells[iSelIdxsArr[0], _primaryKeyIndex].Text;

            var probeInfoEnteringForm = new ProbeInfoEntering(strPrimaryKey, MainForm);
            if (DialogResult.OK == probeInfoEnteringForm.ShowDialog())
            {
                // 加载探头信息
                loadProbeInfo();

                // 设置farpoint焦点（必须实装）
                fpProbeInfo.Sheets[0].SetActiveCell(iSelIdxsArr[0], 0);
            }
        }

        /// <summary>
        ///     删除（必须实装）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (Alert.confirm(Const_GE.DEL_CONFIRM_MSG))
            {
                // 获取已选择明细行的索引
                int[] iSelIdxsArr = GetSelIdxs();

                // 存放主键的数组
                var iPkIdxsArr = new string[iSelIdxsArr.Length];

                for (int i = 0; i < iSelIdxsArr.Length; i++)
                {
                    // 获取主键
                    string iPk = fpProbeInfo.Sheets[0].Cells[iSelIdxsArr[i], _primaryKeyIndex].Text;
                    iPkIdxsArr[i] = iPk;
                }

                // 探头数据删除
                Probe.DeleteAll(iPkIdxsArr);

                // 加载探头信息
                loadProbeInfo();

                // 设置farpoint焦点（必须实装）
                fpProbeInfo.Sheets[0].SetActiveCell(iSelIdxsArr[0], 0);
            }
        }

        /// <summary>
        ///     获取farpoint中选中的所有行（必须实装）
        /// </summary>
        /// <returns>注意，返回值可能是null，null则代表一个也没选中</returns>
        private int[] GetSelIdxs()
        {
            if (_htSelIdxs.Count == 0)
            {
                return null;
            }
            var retArr = new int[_htSelIdxs.Count];
            _htSelIdxs.Keys.CopyTo(retArr, 0);
            return retArr;
        }

        /// <summary>
        ///     farpoint的ButtonClicked事件（必须实装）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpProbeInfo_ButtonClicked(object sender, EditorNotifyEventArgs e)
        {
            // 判断点击的空间类型是否是.FpCheckBox)
            if (e.EditingControl is FpCheckBox)
            {
                var fpChk = (FpCheckBox)e.EditingControl;
                // 判断是否被选中
                if (fpChk.Checked)
                {
                    // 保存索引号
                    if (!_htSelIdxs.Contains(e.Row))
                    {
                        _htSelIdxs.Add(e.Row, true);

                        // 点击每条记录知道全部选中的情况下，全选/全不选checkbox设为选中
                        if (_htSelIdxs.Count == _iRowCount)
                        {
                            // 全选/全不选checkbox设为选中
                            chkSelAll.Checked = true;
                        }
                    }
                }
                else
                {
                    // 移除索引号
                    _htSelIdxs.Remove(e.Row);

                    // 全选/全不选checkbox设为未选中
                    chkSelAll.Checked = false;
                }

                // 如果保存索引号的Hashtable中保存的索引件数是1，则修改按钮设为可用，否则设为不可用
                btnUpdate.Enabled = (_htSelIdxs.Count == 1) ? true : false;
                // 删除按钮
                btnDelete.Enabled = (_htSelIdxs.Count >= 1) ? true : false;
            }
        }

        /// <summary>
        ///     全选/全不选checkbox的click事件（必须实装）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkSelAll_Click(object sender, EventArgs e)
        {
            // 全不选的情况下
            if (_htSelIdxs.Count == _iRowCount)
            {
                // 循环明细
                for (int i = 0; i < _iRowCount; i++)
                {
                    // 将所有明细的checkbox设为未选中
                    fpProbeInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, 0].Value = ((CheckBox)sender).Checked;
                    // 将存有选中项目的数组清空
                    _htSelIdxs.Remove(_iRowDetailStartIndex + i);
                }
                // 删除按钮设为不可用
                btnDelete.Enabled = false;
            }
            // 全选的情况下
            else
            {
                // 循环明细
                for (int i = 0; i < _iRowCount; i++)
                {
                    // 将所有明细设为全选中
                    fpProbeInfo.Sheets[0].Cells[_iRowDetailStartIndex + i, 0].Value = ((CheckBox)sender).Checked;
                    // 将选中明细的索引添加到数组中，如果已经存在不要二次添加
                    if (!_htSelIdxs.Contains(_iRowDetailStartIndex + i))
                    {
                        _htSelIdxs.Add(_iRowDetailStartIndex + i, true);
                    }
                }
                // 删除按钮设为可用
                btnDelete.Enabled = true;
            }

            // 如果保存索引号的Hashtable中保存的索引件数是1，则修改按钮设为可用，否则设为不可用
            btnUpdate.Enabled = (_htSelIdxs.Count == 1) ? true : false;
            // 删除按钮
            btnDelete.Enabled = (_htSelIdxs.Count >= 1) ? true : false;
        }

        /// <summary>
        ///     导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnExport_Click(object sender, EventArgs e)
        {
            if (FileExport.fileExport(fpProbeInfo, true))
            {
                Alert.alert(Const.EXPORT_SUCCESS_MSG);
            }
        }

        /// <summary>
        ///     打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnPrint_Click(object sender, EventArgs e)
        {
            FilePrint.CommonPrint(fpProbeInfo, 0);
        }

        /// <summary>
        ///     退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            // 关闭窗口
            Close();
        }

        /// <summary>
        ///     刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            // 加载探头信息
            loadProbeInfo();
        }

        private void btnProbeImport_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.InitialDirectory = @"C:\Desktop";
            ofd.RestoreDirectory = true;
            ofd.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
            ofd.Multiselect = true;
            //ofd.ShowDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string aa = ofd.FileName;
                string[] strs = File.ReadAllLines(aa, Encoding.GetEncoding("GB2312"));
                string type = "";
                for (int i = 1; i < strs.Length; i++)
                {
                    string[] line = strs[i].Split(',');
                    var probe = new Probe();
                    probe.ProbeMeasureType = Convert.ToInt16(line[0]);
                    probe.ProbeId = line[1].Substring(3);
                    probe.ProbeDescription = line[2];
                    probe.ProbeTypeDisplayName = line[3];
                    probe.ProbeMeasureType = Convert.ToInt16(line[4]);
                    probe.ProbeUseType = line[5];
                    probe.Unit = line[6];

                    probe.SaveAndFlush();
                }
            }
        }
    }
}