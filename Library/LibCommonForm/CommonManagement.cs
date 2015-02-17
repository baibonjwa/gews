using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using LibBusiness;
using LibCommon;
using LibCommonControl;
using LibEntity;
using NHibernate.Criterion;

namespace LibCommonForm
{
    public partial class CommonManagement : BaseForm
    {
        // 功能识别位

        private const int FlagManangingMineName = 1;
        private const int FlagManangingHorizontal = 2;
        private const int FlagManangingMiningArea = 3;
        private const int FlagManangingWorkingFace = 4;
        private const int FlagManangingCoalSeam = 5;
        private static int _typeFlag;
        // id
        private static int _id;

        /// <summary>
        ///     构造方法
        /// </summary>
        public CommonManagement(MainFrm mainFrm)
        {
            InitializeComponent();
            MainForm = mainFrm;
        }

        /// <summary>
        ///     带参数的构造方法
        /// </summary>
        /// <param name="typeFlag"></param>
        /// <param name="id"></param>
        public CommonManagement(int typeFlag, int id)
        {
            InitializeComponent();

            _id = id;
            _typeFlag = typeFlag;

            switch (typeFlag)
            {
                case FlagManangingMineName:
                    {
                        // 窗口标题
                        Text = @"矿井名称管理";
                        // 编号
                        var textBoxColumn0 = new DataGridViewTextBoxColumn { HeaderText = @"编号", Width = 60, ReadOnly = true };
                        dataGridView1.Columns.Add(textBoxColumn0);
                        // 矿井名称
                        var textBoxColumn1 = new DataGridViewTextBoxColumn { HeaderText = @"矿井名称", Width = 100 };
                        dataGridView1.Columns.Add(textBoxColumn1);

                        // 删除按钮
                        var buttonColumn = new DataGridViewButtonColumn
                        {
                            HeaderText = @"删除",
                            Width = 60,
                            Text = "删除",
                            UseColumnTextForButtonValue = true
                        };
                        dataGridView1.Columns.Add(buttonColumn);

                        // 绑定矿井信息
                        LoadMineInfo();
                    }
                    break;
                case FlagManangingHorizontal:
                    {
                        Text = @"水平名称管理";
                        // 编号
                        var textBoxColumn0 = new DataGridViewTextBoxColumn
                        {
                            HeaderText = @"编号",
                            Width = 60,
                            ReadOnly = true
                        };
                        dataGridView1.Columns.Add(textBoxColumn0);
                        // 水平名称
                        var textBoxColumn1 = new DataGridViewTextBoxColumn { HeaderText = @"水平名称", Width = 100 };
                        dataGridView1.Columns.Add(textBoxColumn1);
                        // 所属矿井
                        var comboBoxColumn = new DataGridViewComboBoxColumn { HeaderText = @"所属矿井", Width = 100 };
                        dataGridView1.Columns.Add(comboBoxColumn);

                        //*******************************************************
                        // 获取矿井信息
                        Mine mine = Mine.Find(id);
                        // 设置数据源
                        comboBoxColumn.DataSource = mine;
                        // 设置显示字段
                        comboBoxColumn.DisplayMember = "MineName";
                        // 设置隐藏字段
                        comboBoxColumn.ValueMember = "MineId";
                        //*******************************************************

                        // 删除按钮
                        var buttonColumn = new DataGridViewButtonColumn
                        {
                            HeaderText = @"删除",
                            Width = 60,
                            Text = "删除",
                            UseColumnTextForButtonValue = true
                        };
                        dataGridView1.Columns.Add(buttonColumn);

                        // 绑定水平信息
                        loadHorizontalInfo(id);
                    }
                    break;
                case FlagManangingMiningArea:
                    {
                        Text = @"采区名称管理";
                        // 编号
                        var textBoxColumn0 = new DataGridViewTextBoxColumn
                        {
                            HeaderText = @"编号",
                            Width = 60,
                            ReadOnly = true
                        };
                        dataGridView1.Columns.Add(textBoxColumn0);
                        // 采区名称
                        var textBoxColumn1 = new DataGridViewTextBoxColumn { HeaderText = @"采区名称", Width = 100 };
                        dataGridView1.Columns.Add(textBoxColumn1);
                        // 所属水平
                        var comboBoxColumn = new DataGridViewComboBoxColumn { HeaderText = @"所属水平", Width = 100 };
                        dataGridView1.Columns.Add(comboBoxColumn);

                        //*******************************************************
                        // 获取水平信息

                        Horizontal horizontals = Horizontal.Find(id);
                        // 设置数据源
                        comboBoxColumn.DataSource = horizontals;
                        // 设置显示字段
                        comboBoxColumn.DisplayMember = "HorizontalName";
                        // 设置隐藏字段
                        comboBoxColumn.ValueMember = "HorizontalId";
                        //*******************************************************

                        // 删除按钮
                        var buttonColumn = new DataGridViewButtonColumn
                        {
                            HeaderText = @"删除",
                            Width = 60,
                            Text = @"删除",
                            UseColumnTextForButtonValue = true
                        };
                        dataGridView1.Columns.Add(buttonColumn);

                        // 绑定采区信息
                        loadMiningAreaInfo(id);
                    }
                    break;
                case FlagManangingWorkingFace:
                    {
                        Text = "工作面名称管理";
                        // 编号
                        var textBoxColumn0 = new DataGridViewTextBoxColumn();
                        textBoxColumn0.HeaderText = "编号";
                        textBoxColumn0.Width = 60;
                        textBoxColumn0.ReadOnly = true;
                        dataGridView1.Columns.Add(textBoxColumn0);
                        // 工作面名称
                        var textBoxColumn1 = new DataGridViewTextBoxColumn();
                        textBoxColumn1.HeaderText = "工作面名称";
                        textBoxColumn1.Width = 100;
                        dataGridView1.Columns.Add(textBoxColumn1);
                        // 所属采区
                        var comboBoxColumn = new DataGridViewComboBoxColumn();
                        comboBoxColumn.HeaderText = "所属采区";
                        comboBoxColumn.Width = 100;
                        dataGridView1.Columns.Add(comboBoxColumn);

                        var comboBoxWorkingfaceType = new DataGridViewComboBoxColumn();
                        comboBoxWorkingfaceType.HeaderText = "工作面类型";
                        comboBoxWorkingfaceType.Width = 100;
                        var list = new List<TunnelSimple>
                    {
                        new TunnelSimple((int) WorkingfaceTypeEnum.OTHER, "其他"),
                        new TunnelSimple((int) WorkingfaceTypeEnum.JJ, "掘进"),
                        new TunnelSimple((int) WorkingfaceTypeEnum.HC, "回采")
                    };
                        //foreach (var i in list)
                        //{
                        //    comboBoxWorkingfaceType.Items.Add(i);
                        //}
                        comboBoxWorkingfaceType.DataSource = list;
                        //comboBoxWorkingfaceType.Items.AddRange(list);
                        comboBoxWorkingfaceType.DisplayMember = "Name";
                        comboBoxWorkingfaceType.ValueMember = "WirePointName";


                        dataGridView1.Columns.Add(comboBoxWorkingfaceType);

                        //*******************************************************
                        // 获取采区信息
                        var miningArea = MiningArea.Find(id);
                        // 设置数据源
                        comboBoxColumn.DataSource = miningArea;
                        // 设置显示字段
                        comboBoxColumn.DisplayMember = "MiningAreaName";
                        // 设置隐藏字段
                        comboBoxColumn.ValueMember = "MiningAreaId";

                        //*******************************************************

                        // 删除按钮
                        var buttonColumn = new DataGridViewButtonColumn();
                        buttonColumn.HeaderText = "删除";
                        buttonColumn.Width = 60;
                        buttonColumn.Text = "删除";
                        buttonColumn.UseColumnTextForButtonValue = true;
                        dataGridView1.Columns.Add(buttonColumn);

                        // 绑定工作面信息
                        loadWorkingFaceInfo(id);
                    }
                    break;

                case FlagManangingCoalSeam:
                    {
                        // 窗口标题
                        Text = "煤层名称管理";
                        // 编号
                        var textBoxColumn0 = new DataGridViewTextBoxColumn();
                        textBoxColumn0.HeaderText = "编号";
                        textBoxColumn0.Width = 60;
                        textBoxColumn0.ReadOnly = true;
                        dataGridView1.Columns.Add(textBoxColumn0);
                        // 矿井名称
                        var textBoxColumn1 = new DataGridViewTextBoxColumn();
                        textBoxColumn1.HeaderText = "煤层名称";
                        textBoxColumn1.Width = 100;
                        dataGridView1.Columns.Add(textBoxColumn1);

                        // 删除按钮
                        var buttonColumn = new DataGridViewButtonColumn();
                        buttonColumn.HeaderText = "删除";
                        buttonColumn.Width = 60;
                        buttonColumn.Text = "删除";
                        buttonColumn.UseColumnTextForButtonValue = true;
                        dataGridView1.Columns.Add(buttonColumn);

                        // 绑定矿煤层信息
                        loadCoalSeamsInfo();
                    }
                    break;
                default:
                    {
                    }
                    break;
            }
        }

        public override sealed string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        /// <summary>
        ///     绑定矿井信息
        /// </summary>
        private void LoadMineInfo()
        {
            DataBindUtil.LoadMineName(dataGridView1);
        }

        /// <summary>
        ///     绑定水平信息
        /// </summary>
        private void loadHorizontalInfo(int id)
        {
            dataGridView1.DataSource = null;

            DataBindUtil.LoadHorizontalName(dataGridView1, id);
        }

        /// <summary>
        ///     绑定采区信息
        /// </summary>
        private void loadMiningAreaInfo(int id)
        {
            dataGridView1.DataSource = null;

            // 获取水平信息
            DataBindUtil.LoadMiningAreaName(dataGridView1, id);
        }

        /// <summary>
        ///     绑定工作面信息
        /// </summary>
        private void loadWorkingFaceInfo(int id)
        {
            dataGridView1.DataSource = null;
            // 获取水平信息
            DataBindUtil.LoadWorkingFaceName(dataGridView1, id);
        }

        /// <summary>
        ///     绑定煤层信息
        /// </summary>
        private void loadCoalSeamsInfo()
        {
            dataGridView1.DataSource = null;

            // 获取矿井信息
            DataSet ds = CoalSeamsBLL.selectAllCoalSeamsInfo();
            int iSelCnt = ds.Tables[0].Rows.Count;
            if (iSelCnt > 0)
            {
                // 禁止自动生成列(※位置不可变)
                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns[0].DataPropertyName = CoalSeamsDbConstNames.COAL_SEAMS_ID;
                dataGridView1.Columns[1].DataPropertyName = CoalSeamsDbConstNames.COAL_SEAMS_NAME;
            }
        }

        /// <summary>
        ///     提交
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            // 验证
            if (!check())
            {
                DialogResult = DialogResult.None;
                return;
            }
            DialogResult = DialogResult.OK;

            switch (_typeFlag)
            {
                case FlagManangingMineName:
                    // 矿井名称管理
                    updateMineInfo();
                    break;
                case FlagManangingHorizontal:
                    // 水平名称管理
                    updateHorizontalInfo();
                    break;
                case FlagManangingMiningArea:
                    // 采区名称管理
                    updateMiningAreaInfo();
                    break;
                case FlagManangingWorkingFace:
                    // 工作面名称管理
                    updateWorkingFaceInfo();
                    break;
                case FlagManangingCoalSeam:
                    // 煤层名称管理
                    updateCoalSeamsInfo();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        ///     更新矿井信息
        /// </summary>
        private void updateMineInfo()
        {
            bool bResultFlag = true;

            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                var mineEntity = new Mine();
                // 矿井编号
                if (dataGridView1.Rows[i].Cells[0].Value != DBNull.Value && dataGridView1.Rows[i].Cells[0].Value != null)
                {
                    mineEntity.MineId = Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value);
                }
                // 矿井名称
                if (dataGridView1.Rows[i].Cells[1].Value != DBNull.Value && dataGridView1.Rows[i].Cells[1].Value != null)
                {
                    mineEntity.MineName = Convert.ToString(dataGridView1.Rows[i].Cells[1].Value);
                }

                if (dataGridView1.Rows[i].Cells[0].Value != DBNull.Value && dataGridView1.Rows[i].Cells[0].Value != null)
                {
                    mineEntity.Save();
                }
                else
                {
                    mineEntity.Save();
                }
            }
            BasicInfoManager.getInstance().RefreshMineInfo();
            Alert.alert(Const.SUCCESS_MSG);
            // 绑定矿井名称
            LoadMineInfo();
            Alert.alert(Const.FAILURE_MSG);
        }

        /// <summary>
        ///     更新水平信息
        /// </summary>
        private void updateHorizontalInfo()
        {
            bool bResultFlag = true;

            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                var horizontalEntity = new Horizontal();
                // 水平编号
                if (dataGridView1.Rows[i].Cells[0].Value != DBNull.Value && dataGridView1.Rows[i].Cells[0].Value != null)
                {
                    horizontalEntity.HorizontalId = Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value);
                }
                // 水平名称
                if (dataGridView1.Rows[i].Cells[1].Value != DBNull.Value && dataGridView1.Rows[i].Cells[1].Value != null)
                {
                    horizontalEntity.HorizontalName = Convert.ToString(dataGridView1.Rows[i].Cells[1].Value);
                }

                //***************************************
                // 所属矿井
                if (dataGridView1.Rows[i].Cells[2].Value != DBNull.Value && dataGridView1.Rows[i].Cells[2].Value != null)
                {
                    horizontalEntity.Mine.MineId = Convert.ToInt32(dataGridView1.Rows[i].Cells[2].Value);
                }
                //***************************************

                if (dataGridView1.Rows[i].Cells[0].Value != DBNull.Value && dataGridView1.Rows[i].Cells[0].Value != null)
                {
                    horizontalEntity.Save();
                }
                else
                {
                    horizontalEntity.Save();
                }
            }

            //BasicInfoManager.getInstance().refreshHorizontalInfo();

            // 执行结果判断

            Alert.alert(Const.SUCCESS_MSG);
            // 绑定水平信息
            loadHorizontalInfo(_id);
        }

        /// <summary>
        ///     更新采区信息
        /// </summary>
        private void updateMiningAreaInfo()
        {
            bool bResultFlag = true;

            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                int miningAreaId = Const.INVALID_ID;
                string miningAreaName = string.Empty;
                int horizontalId = Const.INVALID_ID;

                // 采区编号
                if (dataGridView1.Rows[i].Cells[0].Value != DBNull.Value && dataGridView1.Rows[i].Cells[0].Value != null)
                {
                    miningAreaId = Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value);
                }
                // 采区名称
                if (dataGridView1.Rows[i].Cells[1].Value != DBNull.Value && dataGridView1.Rows[i].Cells[1].Value != null)
                {
                    miningAreaName = Convert.ToString(dataGridView1.Rows[i].Cells[1].Value);
                }

                //***************************************
                // 所属水平
                if (dataGridView1.Rows[i].Cells[2].Value != DBNull.Value && dataGridView1.Rows[i].Cells[2].Value != null)
                {
                    horizontalId = Convert.ToInt32(dataGridView1.Rows[i].Cells[2].Value);
                }

                //***************************************

                if (dataGridView1.Rows[i].Cells[0].Value != DBNull.Value && dataGridView1.Rows[i].Cells[0].Value != null)
                {
                    var miningArea = MiningArea.Find(miningAreaId);
                    miningArea.MiningAreaName = miningAreaName;
                    miningArea.Horizontal = Horizontal.Find(horizontalId);
                    miningArea.Save();
                }
                else
                {
                    var miningArea = new MiningArea
                    {
                        MiningAreaName = miningAreaName,
                        Horizontal = Horizontal.Find(horizontalId)
                    };
                    miningArea.Save();
                }

                if (!bResultFlag)
                {
                    break;
                }
            }

            // 执行结果判断
            if (bResultFlag)
            {
                Alert.alert(Const.SUCCESS_MSG);
                // 绑定采区信息
                loadMiningAreaInfo(_id);
            }
            else
            {
                Alert.alert(Const.FAILURE_MSG);
            }
        }

        private void updateWorkingFaceInfo()
        {
            bool bResultFlag = true;

            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                DataGridViewCellCollection cells = dataGridView1.Rows[i].Cells;
                int tmpWorkingFaceId = Const.INVALID_ID;
                string tmpWorkingFaceName = string.Empty;
                int miningAreaId = Const.INVALID_ID;
                int workingfaceType = 0;
                // 工作面编号
                if (cells[0].Value != DBNull.Value && cells[0].Value != null)
                {
                    tmpWorkingFaceId = Convert.ToInt32(cells[0].Value);
                }
                // 工作面名称
                if (cells[1].Value != DBNull.Value && cells[1].Value != null)
                {
                    tmpWorkingFaceName = Convert.ToString(cells[1].Value);
                }

                //***************************************
                // 所属采区
                if (cells[2].Value != DBNull.Value && cells[2].Value != null)
                {
                    miningAreaId = Convert.ToInt32(cells[2].Value);
                }

                if (cells[3].Value != DBNull.Value &&
                    cells[3].Value != null)
                {
                    workingfaceType = Convert.ToInt32(cells[3].Value);
                }

                //***************************************

                if (cells[0].Value != DBNull.Value && cells[0].Value != null)
                {
                    var workingFace = WorkingFace.Find(tmpWorkingFaceId);
                    workingFace.WorkingFaceName = tmpWorkingFaceName;
                    workingFace.MiningArea = MiningArea.Find(miningAreaId);
                    workingFace.WorkingfaceTypeEnum = (WorkingfaceTypeEnum)workingfaceType;

                    workingFace.Save();
                }
                else
                {
                    var workingFace = new WorkingFace()
                    {
                        WorkingFaceName = tmpWorkingFaceName,
                        MiningArea = MiningArea.Find(miningAreaId),
                        WorkingfaceTypeEnum = (WorkingfaceTypeEnum)workingfaceType
                    };
                    workingFace.Save();
                }
            }


            Alert.alert(Const.SUCCESS_MSG);
            // 绑定工作面信息
            loadWorkingFaceInfo(_id);
        }

        /// <summary>
        ///     更新煤层信息
        /// </summary>
        private void updateCoalSeamsInfo()
        {
            bool bResultFlag = true;

            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                var coalSeamsEntity = new CoalSeams();
                // 煤层编号
                if (dataGridView1.Rows[i].Cells[0].Value != DBNull.Value && dataGridView1.Rows[i].Cells[0].Value != null)
                {
                    coalSeamsEntity.CoalSeamsId = Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value);
                }
                // 煤层名称
                if (dataGridView1.Rows[i].Cells[1].Value != DBNull.Value && dataGridView1.Rows[i].Cells[1].Value != null)
                {
                    coalSeamsEntity.CoalSeamsName = Convert.ToString(dataGridView1.Rows[i].Cells[1].Value);
                }

                if (dataGridView1.Rows[i].Cells[0].Value != DBNull.Value && dataGridView1.Rows[i].Cells[0].Value != null)
                {
                    bResultFlag = CoalSeamsBLL.updateCoalSeamsInfo(coalSeamsEntity);
                }
                else
                {
                    bResultFlag = CoalSeamsBLL.insertCoalSeamsInfo(coalSeamsEntity);
                }

                if (!bResultFlag)
                {
                    break;
                }
            }

            BasicInfoManager.getInstance().refreshCoalSeamsInfo();

            // 执行结果判断
            if (bResultFlag)
            {
                Alert.alert(Const.SUCCESS_MSG);
                // 绑定煤层名称
                loadCoalSeamsInfo();
            }
            else
            {
                Alert.alert(Const.FAILURE_MSG);
            }
        }

        private bool check()
        {
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                int count = 0;
                for (int j = 0; j < dataGridView1.Rows.Count - 1; j++)
                {
                    if (dataGridView1.Rows[i].Cells[1].Value.ToString() ==
                        dataGridView1.Rows[j].Cells[1].Value.ToString())
                    {
                        count = count + 1;
                    }
                }

                if (count >= 2)
                {
                    Alert.alert(Const.WORKINGFACENAMESAME_MSG);
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        ///     取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            // 窗口关闭
            Close();
        }

        /// <summary>
        ///     Cell Click事件
        ///     主要用于删除操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // 矿井名称管理
            if (_typeFlag == FlagManangingMineName)
            {
                // 判断列索引是不是删除按钮
                if (e.ColumnIndex == 2)
                {
                    // 最后一行删除按钮设为不可
                    if (dataGridView1.RowCount - 1 != dataGridView1.CurrentRow.Index)
                    {
                        if (Alert.confirm(Const.DEL_CONFIRM_MSG))
                        {
                            if (dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value != DBNull.Value
                                && dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value != null)
                            {
                                int iMineId =
                                    Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value);

                                Mine.Find(iMineId).Delete();
                                // 绑定矿井名称
                                LoadMineInfo();

                                Alert.alert(Const.DEL_FAILURE_MSG);
                            }
                            else
                            {
                                dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
                            }
                        }
                    }
                }
            }

            // 水平名称管理
            if (_typeFlag == FlagManangingHorizontal)
            {
                // 判断列索引是不是删除按钮
                if (e.ColumnIndex == 3)
                {
                    // 最后一行删除按钮设为不可
                    if (dataGridView1.RowCount - 1 != dataGridView1.CurrentRow.Index)
                    {
                        if (Alert.confirm(Const.DEL_CONFIRM_MSG))
                        {
                            if (dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value != DBNull.Value
                                && dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value != null)
                            {
                                int iHorizontalId =
                                    Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value);
                                Horizontal.Find(iHorizontalId).Delete();
                                // 绑定水平信息
                                loadHorizontalInfo(_id);

                            }
                            else
                            {
                                dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
                            }
                        }
                    }
                }
            }

            if (_typeFlag == FlagManangingMiningArea)
            {
                // 判断列索引是不是删除按钮
                if (e.ColumnIndex == 3)
                {
                    // 最后一行删除按钮设为不可
                    if (dataGridView1.RowCount - 1 != dataGridView1.CurrentRow.Index)
                    {
                        if (Alert.confirm(Const.DEL_CONFIRM_MSG))
                        {
                            if (dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value != DBNull.Value
                                && dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value != null)
                            {
                                int iMiningAreaId =
                                    Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value);
                                if (WorkingFace.ExistsByMiningAreaId(iMiningAreaId))
                                {
                                    Alert.alert("采区有关联的工作面，请首先解除关联");
                                }
                                else
                                {
                                    var miningArea = MiningArea.Find(iMiningAreaId);
                                    miningArea.Delete();

                                    // 绑定采区信息
                                    loadMiningAreaInfo(_id);
                                }
                            }
                            else
                            {
                                dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
                            }
                        }
                    }
                }
            }

            if (_typeFlag == FlagManangingWorkingFace)
            {
                // 判断列索引是不是删除按钮
                if (e.ColumnIndex == 4)
                {
                    // 最后一行删除按钮设为不可
                    if (dataGridView1.RowCount - 1 != dataGridView1.CurrentRow.Index)
                    {
                        if (Alert.confirm(Const.DEL_CONFIRM_MSG))
                        {
                            if (dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value != DBNull.Value
                                && dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value != null)
                            {
                                int iWorkingFaceId =
                                    Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value);


                                IList<Tunnel> list = WorkingFace.Find(iWorkingFaceId).Tunnels;
                                if (list != null && list.Count > 0)
                                {
                                    Alert.alert("工作面有关联的巷道，请首先解除关联");
                                }
                                else
                                {
                                    var workingFace = WorkingFace.Find(iWorkingFaceId);
                                    workingFace.Delete();
                                    // 绑定工作面信息
                                    loadWorkingFaceInfo(_id);
                                }
                            }
                            else
                            {
                                dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
                            }
                        }
                    }
                }
            }

            // 煤层名称管理
            if (_typeFlag == FlagManangingCoalSeam)
            {
                // 判断列索引是不是删除按钮
                if (e.ColumnIndex == 2)
                {
                    // 最后一行删除按钮设为不可
                    if (dataGridView1.RowCount - 1 != dataGridView1.CurrentRow.Index)
                    {
                        if (Alert.confirm(Const.DEL_CONFIRM_MSG))
                        {
                            if (dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value != DBNull.Value
                                && dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value != null)
                            {
                                int iCoalSeamsId =
                                    Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value);
                                bool bResult = CoalSeamsBLL.deleteCoalSeamsInfo(iCoalSeamsId);

                                if (bResult)
                                {
                                    // 绑定矿煤层信息
                                    loadCoalSeamsInfo();
                                }
                                else
                                {
                                    Alert.alert(Const.DEL_FAILURE_MSG);
                                }
                            }
                            else
                            {
                                dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        ///     编号自动排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            if (_typeFlag == 2 || _typeFlag == 3 || _typeFlag == 4)
            {
                dataGridView1.Rows[e.Row.Index - 1].Cells[2].Value = _id;
            }
        }
    }
}