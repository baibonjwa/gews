// ******************************************************************
// 概  述：矿井名称，水平名称，采区名称，工作面名称共通管理界面
// 作成者：伍鑫
// 作成日：2014/02/26
// 版本号：1.0
// ******************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LibBusiness;
using LibEntity;
using LibCommon;
using LibCommonControl;

namespace LibCommonForm
{
    public partial class CommonManagement : BaseForm
    {
        // 功能识别位
        private static int _typeFlag;
        // id
        private static int _id;

        private const int FLAG_MANANGING_MINE_NAME = 1;
        private const int FLAG_MANANGING_HORIZONTAL = 2;
        private const int FLAG_MANANGING_MINING_AREA = 3;
        private const int FLAG_MANANGING_WORKING_FACE = 4;
        private const int FLAG_MANANGING_COAL_SEAM = 5;

        /// <summary>
        /// 构造方法
        /// </summary>
        public CommonManagement(MainFrm mainFrm)
        {
            InitializeComponent();
            this.MainForm = mainFrm;
        }

        /// <summary>
        /// 带参数的构造方法
        /// </summary>
        /// <param name="typeFlag"></param>
        /// <param name="id"></param>
        public CommonManagement(int typeFlag, int id, MainFrm mainFrm)
        {
            InitializeComponent();

            this.MainForm = mainFrm;

            _id = id;
            _typeFlag = typeFlag;

            switch (typeFlag)
            {
                case FLAG_MANANGING_MINE_NAME:
                    {
                        // 窗口标题
                        this.Text = "矿井名称管理";
                        // 编号
                        DataGridViewTextBoxColumn textBoxColumn0 = new DataGridViewTextBoxColumn();
                        textBoxColumn0.HeaderText = "编号";
                        textBoxColumn0.Width = 60;
                        textBoxColumn0.ReadOnly = true;
                        this.dataGridView1.Columns.Add(textBoxColumn0);
                        // 矿井名称
                        DataGridViewTextBoxColumn textBoxColumn1 = new DataGridViewTextBoxColumn();
                        textBoxColumn1.HeaderText = "矿井名称";
                        textBoxColumn1.Width = 100;
                        this.dataGridView1.Columns.Add(textBoxColumn1);

                        // 删除按钮
                        DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
                        buttonColumn.HeaderText = "删除";
                        buttonColumn.Width = 60;
                        buttonColumn.Text = "删除";
                        buttonColumn.UseColumnTextForButtonValue = true;
                        this.dataGridView1.Columns.Add(buttonColumn);

                        // 绑定矿井信息
                        loadMineInfo();

                    }
                    break;
                case FLAG_MANANGING_HORIZONTAL:
                    {
                        this.Text = "水平名称管理";
                        // 编号
                        DataGridViewTextBoxColumn textBoxColumn0 = new DataGridViewTextBoxColumn();
                        textBoxColumn0.HeaderText = "编号";
                        textBoxColumn0.Width = 60;
                        textBoxColumn0.ReadOnly = true;
                        this.dataGridView1.Columns.Add(textBoxColumn0);
                        // 水平名称
                        DataGridViewTextBoxColumn textBoxColumn1 = new DataGridViewTextBoxColumn();
                        textBoxColumn1.HeaderText = "水平名称";
                        textBoxColumn1.Width = 100;
                        this.dataGridView1.Columns.Add(textBoxColumn1);
                        // 所属矿井
                        DataGridViewComboBoxColumn comboBoxColumn = new DataGridViewComboBoxColumn();
                        comboBoxColumn.HeaderText = "所属矿井";
                        comboBoxColumn.Width = 100;
                        this.dataGridView1.Columns.Add(comboBoxColumn);

                        //*******************************************************
                        // 获取矿井信息
                        DataSet ds = MineBLL.selectMineInfoByMineId(id);
                        // 设置数据源
                        comboBoxColumn.DataSource = ds.Tables[0];
                        // 设置显示字段
                        comboBoxColumn.DisplayMember = MineDbConstNames.MINE_NAME;
                        // 设置隐藏字段
                        comboBoxColumn.ValueMember = MineDbConstNames.MINE_ID;
                        //*******************************************************

                        // 删除按钮
                        DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
                        buttonColumn.HeaderText = "删除";
                        buttonColumn.Width = 60;
                        buttonColumn.Text = "删除";
                        buttonColumn.UseColumnTextForButtonValue = true;
                        this.dataGridView1.Columns.Add(buttonColumn);

                        // 绑定水平信息
                        loadHorizontalInfo(id);
                    }
                    break;
                case FLAG_MANANGING_MINING_AREA:
                    {
                        this.Text = "采区名称管理";
                        // 编号
                        DataGridViewTextBoxColumn textBoxColumn0 = new DataGridViewTextBoxColumn();
                        textBoxColumn0.HeaderText = "编号";
                        textBoxColumn0.Width = 60;
                        textBoxColumn0.ReadOnly = true;
                        this.dataGridView1.Columns.Add(textBoxColumn0);
                        // 采区名称
                        DataGridViewTextBoxColumn textBoxColumn1 = new DataGridViewTextBoxColumn();
                        textBoxColumn1.HeaderText = "采区名称";
                        textBoxColumn1.Width = 100;
                        this.dataGridView1.Columns.Add(textBoxColumn1);
                        // 所属水平
                        DataGridViewComboBoxColumn comboBoxColumn = new DataGridViewComboBoxColumn();
                        comboBoxColumn.HeaderText = "所属水平";
                        comboBoxColumn.Width = 100;
                        this.dataGridView1.Columns.Add(comboBoxColumn);

                        //*******************************************************
                        // 获取水平信息
                        DataSet ds = HorizontalBLL.selectHorizontalInfoByHorizontalId(id);
                        // 设置数据源
                        comboBoxColumn.DataSource = ds.Tables[0];
                        // 设置显示字段
                        comboBoxColumn.DisplayMember = HorizontalDbConstNames.HORIZONTAL_NAME;
                        // 设置隐藏字段
                        comboBoxColumn.ValueMember = HorizontalDbConstNames.HORIZONTAL_ID;
                        //*******************************************************

                        // 删除按钮
                        DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
                        buttonColumn.HeaderText = "删除";
                        buttonColumn.Width = 60;
                        buttonColumn.Text = "删除";
                        buttonColumn.UseColumnTextForButtonValue = true;
                        this.dataGridView1.Columns.Add(buttonColumn);

                        // 绑定采区信息
                        loadMiningAreaInfo(id);
                    }
                    break;
                case FLAG_MANANGING_WORKING_FACE:
                    {
                        this.Text = "工作面名称管理";
                        // 编号
                        DataGridViewTextBoxColumn textBoxColumn0 = new DataGridViewTextBoxColumn();
                        textBoxColumn0.HeaderText = "编号";
                        textBoxColumn0.Width = 60;
                        textBoxColumn0.ReadOnly = true;
                        this.dataGridView1.Columns.Add(textBoxColumn0);
                        // 工作面名称
                        DataGridViewTextBoxColumn textBoxColumn1 = new DataGridViewTextBoxColumn();
                        textBoxColumn1.HeaderText = "工作面名称";
                        textBoxColumn1.Width = 100;
                        this.dataGridView1.Columns.Add(textBoxColumn1);
                        // 所属采区
                        DataGridViewComboBoxColumn comboBoxColumn = new DataGridViewComboBoxColumn();
                        comboBoxColumn.HeaderText = "所属采区";
                        comboBoxColumn.Width = 100;
                        this.dataGridView1.Columns.Add(comboBoxColumn);

                        DataGridViewComboBoxColumn comboBoxWorkingfaceType = new DataGridViewComboBoxColumn();
                        comboBoxWorkingfaceType.HeaderText = "工作面类型";
                        comboBoxWorkingfaceType.Width = 100;
                        List<TunnelSimple> list = new List<TunnelSimple>
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
                        comboBoxWorkingfaceType.ValueMember = "Id";


                        this.dataGridView1.Columns.Add(comboBoxWorkingfaceType);

                        //*******************************************************
                        // 获取采区信息
                        DataSet ds = MiningAreaBLL.selectMiningAreaInfoByMiningAreaId(id);
                        // 设置数据源
                        comboBoxColumn.DataSource = ds.Tables[0];
                        // 设置显示字段
                        comboBoxColumn.DisplayMember = MiningAreaDbConstNames.MININGAREA_NAME;
                        // 设置隐藏字段
                        comboBoxColumn.ValueMember = MiningAreaDbConstNames.MININGAREA_ID;

                        //*******************************************************

                        // 删除按钮
                        DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
                        buttonColumn.HeaderText = "删除";
                        buttonColumn.Width = 60;
                        buttonColumn.Text = "删除";
                        buttonColumn.UseColumnTextForButtonValue = true;
                        this.dataGridView1.Columns.Add(buttonColumn);

                        // 绑定工作面信息
                        loadWorkingFaceInfo(id);
                    }
                    break;

                case FLAG_MANANGING_COAL_SEAM:
                    {
                        // 窗口标题
                        this.Text = "煤层名称管理";
                        // 编号
                        DataGridViewTextBoxColumn textBoxColumn0 = new DataGridViewTextBoxColumn();
                        textBoxColumn0.HeaderText = "编号";
                        textBoxColumn0.Width = 60;
                        textBoxColumn0.ReadOnly = true;
                        this.dataGridView1.Columns.Add(textBoxColumn0);
                        // 矿井名称
                        DataGridViewTextBoxColumn textBoxColumn1 = new DataGridViewTextBoxColumn();
                        textBoxColumn1.HeaderText = "煤层名称";
                        textBoxColumn1.Width = 100;
                        this.dataGridView1.Columns.Add(textBoxColumn1);

                        // 删除按钮
                        DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
                        buttonColumn.HeaderText = "删除";
                        buttonColumn.Width = 60;
                        buttonColumn.Text = "删除";
                        buttonColumn.UseColumnTextForButtonValue = true;
                        this.dataGridView1.Columns.Add(buttonColumn);

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

        /// <summary>
        /// 绑定矿井信息
        /// </summary>
        private void loadMineInfo()
        {
            this.dataGridView1.DataSource = null;

            // 获取矿井信息
            DataSet ds = MineBLL.selectAllMineInfo();
            int iSelCnt = ds.Tables[0].Rows.Count;
            if (iSelCnt > 0)
            {
                // 禁止自动生成列(※位置不可变)
                this.dataGridView1.AutoGenerateColumns = false;
                this.dataGridView1.DataSource = ds.Tables[0];
                this.dataGridView1.Columns[0].DataPropertyName = MineDbConstNames.MINE_ID;
                this.dataGridView1.Columns[1].DataPropertyName = MineDbConstNames.MINE_NAME;
            }
        }

        /// <summary>
        /// 绑定水平信息
        /// </summary>
        private void loadHorizontalInfo(int id)
        {
            this.dataGridView1.DataSource = null;

            // 获取水平信息
            DataSet ds = HorizontalBLL.selectHorizontalInfoByMineId(id);
            int iSelCnt = ds.Tables[0].Rows.Count;
            if (iSelCnt > 0)
            {
                // 禁止自动生成列(※位置不可变)
                this.dataGridView1.AutoGenerateColumns = false;
                this.dataGridView1.DataSource = ds.Tables[0];
                this.dataGridView1.Columns[0].DataPropertyName = HorizontalDbConstNames.HORIZONTAL_ID;
                this.dataGridView1.Columns[1].DataPropertyName = HorizontalDbConstNames.HORIZONTAL_NAME;
                this.dataGridView1.Columns[2].DataPropertyName = HorizontalDbConstNames.MINE_ID;
            }
        }

        /// <summary>
        /// 绑定采区信息
        /// </summary>
        private void loadMiningAreaInfo(int id)
        {
            this.dataGridView1.DataSource = null;

            // 获取水平信息
            DataSet ds = MiningAreaBLL.selectMiningAreaInfoByHorizontalId(id);
            int iSelCnt = ds.Tables[0].Rows.Count;
            if (iSelCnt > 0)
            {
                // 禁止自动生成列(※位置不可变)
                this.dataGridView1.AutoGenerateColumns = false;
                this.dataGridView1.DataSource = ds.Tables[0];
                this.dataGridView1.Columns[0].DataPropertyName = MiningAreaDbConstNames.MININGAREA_ID;
                this.dataGridView1.Columns[1].DataPropertyName = MiningAreaDbConstNames.MININGAREA_NAME;
                this.dataGridView1.Columns[2].DataPropertyName = MiningAreaDbConstNames.HORIZONTAL_ID;
            }
        }

        /// <summary>
        /// 绑定工作面信息
        /// </summary>
        private void loadWorkingFaceInfo(int id)
        {
            this.dataGridView1.DataSource = null;

            // 获取水平信息
            DataSet ds = WorkingFaceBLL.selectWorkingFaceInfoByMiningAreaId(id);
            int iSelCnt = ds.Tables[0].Rows.Count;
            if (iSelCnt > 0)
            {
                // 禁止自动生成列(※位置不可变)
                this.dataGridView1.AutoGenerateColumns = false;
                this.dataGridView1.DataSource = ds.Tables[0];
                this.dataGridView1.Columns[0].DataPropertyName = WorkingFaceDbConstNames.WORKINGFACE_ID;
                this.dataGridView1.Columns[1].DataPropertyName = WorkingFaceDbConstNames.WORKINGFACE_NAME;
                this.dataGridView1.Columns[2].DataPropertyName = WorkingFaceDbConstNames.MININGAREA_ID;
                this.dataGridView1.Columns[3].DataPropertyName = WorkingFaceDbConstNames.WORKINGFACE_TYPE;
            }
        }

        /// <summary>
        /// 绑定煤层信息
        /// </summary>
        private void loadCoalSeamsInfo()
        {
            this.dataGridView1.DataSource = null;

            // 获取矿井信息
            DataSet ds = CoalSeamsBLL.selectAllCoalSeamsInfo();
            int iSelCnt = ds.Tables[0].Rows.Count;
            if (iSelCnt > 0)
            {
                // 禁止自动生成列(※位置不可变)
                this.dataGridView1.AutoGenerateColumns = false;
                this.dataGridView1.DataSource = ds.Tables[0];
                this.dataGridView1.Columns[0].DataPropertyName = CoalSeamsDbConstNames.COAL_SEAMS_ID;
                this.dataGridView1.Columns[1].DataPropertyName = CoalSeamsDbConstNames.COAL_SEAMS_NAME;
            }
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
                DialogResult = DialogResult.None;
                return;
            }
            DialogResult = DialogResult.OK;

            switch (_typeFlag)
            {
                case FLAG_MANANGING_MINE_NAME:
                    // 矿井名称管理
                    updateMineInfo();
                    break;
                case FLAG_MANANGING_HORIZONTAL:
                    // 水平名称管理
                    updateHorizontalInfo();
                    break;
                case FLAG_MANANGING_MINING_AREA:
                    // 采区名称管理
                    updateMiningAreaInfo();
                    break;
                case FLAG_MANANGING_WORKING_FACE:
                    // 工作面名称管理
                    updateWorkingFaceInfo();
                    break;
                case FLAG_MANANGING_COAL_SEAM:
                    // 煤层名称管理
                    updateCoalSeamsInfo();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 更新矿井信息
        /// </summary>
        private void updateMineInfo()
        {
            bool bResultFlag = true;

            for (int i = 0; i < this.dataGridView1.Rows.Count - 1; i++)
            {
                Mine mineEntity = new Mine();
                // 矿井编号
                if (this.dataGridView1.Rows[i].Cells[0].Value != System.DBNull.Value && this.dataGridView1.Rows[i].Cells[0].Value != null)
                {
                    mineEntity.MineId = Convert.ToInt32(this.dataGridView1.Rows[i].Cells[0].Value);
                }
                // 矿井名称
                if (this.dataGridView1.Rows[i].Cells[1].Value != System.DBNull.Value && this.dataGridView1.Rows[i].Cells[1].Value != null)
                {
                    mineEntity.MineName = Convert.ToString(this.dataGridView1.Rows[i].Cells[1].Value);
                }

                if (this.dataGridView1.Rows[i].Cells[0].Value != System.DBNull.Value && this.dataGridView1.Rows[i].Cells[0].Value != null)
                {
                    bResultFlag = MineBLL.updateMineInfo(mineEntity);
                }
                else
                {
                    bResultFlag = MineBLL.insertMineInfo(mineEntity);
                }

                if (!bResultFlag)
                {
                    break;
                }
            }

            BasicInfoManager.getInstance().refreshMineInfo();

            // 执行结果判断
            if (bResultFlag)
            {
                Alert.alert(Const.SUCCESS_MSG);
                // 绑定矿井名称
                loadMineInfo();
            }
            else
            {
                Alert.alert(Const.FAILURE_MSG);
            }
        }

        /// <summary>
        /// 更新水平信息
        /// </summary>
        private void updateHorizontalInfo()
        {
            bool bResultFlag = true;

            for (int i = 0; i < this.dataGridView1.Rows.Count - 1; i++)
            {
                Horizontal horizontalEntity = new Horizontal();
                // 水平编号
                if (this.dataGridView1.Rows[i].Cells[0].Value != System.DBNull.Value && this.dataGridView1.Rows[i].Cells[0].Value != null)
                {
                    horizontalEntity.HorizontalId = Convert.ToInt32(this.dataGridView1.Rows[i].Cells[0].Value);
                }
                // 水平名称
                if (this.dataGridView1.Rows[i].Cells[1].Value != System.DBNull.Value && this.dataGridView1.Rows[i].Cells[1].Value != null)
                {
                    horizontalEntity.HorizontalName = Convert.ToString(this.dataGridView1.Rows[i].Cells[1].Value);
                }

                //***************************************
                // 所属矿井
                if (this.dataGridView1.Rows[i].Cells[2].Value != System.DBNull.Value && this.dataGridView1.Rows[i].Cells[2].Value != null)
                {
                    horizontalEntity.Mine.MineId = Convert.ToInt32(this.dataGridView1.Rows[i].Cells[2].Value);
                }
                //***************************************

                if (this.dataGridView1.Rows[i].Cells[0].Value != System.DBNull.Value && this.dataGridView1.Rows[i].Cells[0].Value != null)
                {
                    bResultFlag = HorizontalBLL.updateHorizontalInfo(horizontalEntity);
                }
                else
                {
                    bResultFlag = HorizontalBLL.insertHorizontalInfo(horizontalEntity);
                }

                if (!bResultFlag)
                {
                    break;
                }
            }

            BasicInfoManager.getInstance().refreshHorizontalInfo();

            // 执行结果判断
            if (bResultFlag)
            {
                Alert.alert(Const.SUCCESS_MSG);
                // 绑定水平信息
                loadHorizontalInfo(_id);
            }
            else
            {
                Alert.alert(Const.FAILURE_MSG);
            }
        }

        /// <summary>
        /// 更新采区信息
        /// </summary>
        private void updateMiningAreaInfo()
        {
            bool bResultFlag = true;

            for (int i = 0; i < this.dataGridView1.Rows.Count - 1; i++)
            {
                int miningAreaId = Const.INVALID_ID;
                string miningAreaName = string.Empty;
                int horizontalId = Const.INVALID_ID;

                // 采区编号
                if (this.dataGridView1.Rows[i].Cells[0].Value != System.DBNull.Value && this.dataGridView1.Rows[i].Cells[0].Value != null)
                {
                    miningAreaId = Convert.ToInt32(this.dataGridView1.Rows[i].Cells[0].Value);
                }
                // 采区名称
                if (this.dataGridView1.Rows[i].Cells[1].Value != System.DBNull.Value && this.dataGridView1.Rows[i].Cells[1].Value != null)
                {
                    miningAreaName = Convert.ToString(this.dataGridView1.Rows[i].Cells[1].Value);
                }

                //***************************************
                // 所属水平
                if (this.dataGridView1.Rows[i].Cells[2].Value != System.DBNull.Value && this.dataGridView1.Rows[i].Cells[2].Value != null)
                {
                    horizontalId = Convert.ToInt32(this.dataGridView1.Rows[i].Cells[2].Value);
                }

                //***************************************

                if (this.dataGridView1.Rows[i].Cells[0].Value != System.DBNull.Value && this.dataGridView1.Rows[i].Cells[0].Value != null)
                {
                    bResultFlag = MiningAreaBLL.updateMiningAreaInfo(miningAreaId, miningAreaName, horizontalId);
                }
                else
                {
                    bResultFlag = MiningAreaBLL.insertMiningAreaInfo(miningAreaName, horizontalId);
                }

                if (!bResultFlag)
                {
                    break;
                }
            }

            BasicInfoManager.getInstance().refreshMiningAreaInfo();

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

            for (int i = 0; i < this.dataGridView1.Rows.Count - 1; i++)
            {
                DataGridViewCellCollection cells = this.dataGridView1.Rows[i].Cells;
                int tmpWorkingFaceId = Const.INVALID_ID;
                string tmpWorkingFaceName = string.Empty;
                int miningAreaId = Const.INVALID_ID;
                int workingfaceType = 0;
                // 工作面编号
                if (cells[0].Value != System.DBNull.Value && cells[0].Value != null)
                {
                    tmpWorkingFaceId = Convert.ToInt32(cells[0].Value);
                }
                // 工作面名称
                if (cells[1].Value != System.DBNull.Value && cells[1].Value != null)
                {
                    tmpWorkingFaceName = Convert.ToString(cells[1].Value);
                }

                //***************************************
                // 所属采区
                if (cells[2].Value != System.DBNull.Value && cells[2].Value != null)
                {
                    miningAreaId = Convert.ToInt32(cells[2].Value);
                }

                if (cells[3].Value != System.DBNull.Value &&
                    cells[3].Value != null)
                {
                    workingfaceType = Convert.ToInt32(cells[3].Value);
                }

                //***************************************

                if (cells[0].Value != System.DBNull.Value && cells[0].Value != null)
                {
                    bResultFlag = WorkingFaceBLL.updateWorkingFaceBasicInfo(tmpWorkingFaceId, tmpWorkingFaceName, miningAreaId, workingfaceType);
                }
                else
                {
                    bResultFlag = WorkingFaceBLL.insertWorkingFaceBasicInfo(tmpWorkingFaceName, miningAreaId, workingfaceType);
                }

                if (!bResultFlag)
                {
                    break;
                }
            }

            // 工作面信息已经变更，需要刷新工作面信息
            BasicInfoManager.getInstance().refreshWorkingFaceInfo();

            // 执行结果判断
            if (bResultFlag)
            {
                Alert.alert(Const.SUCCESS_MSG);
                // 绑定工作面信息
                loadWorkingFaceInfo(_id);
            }
            else
            {
                Alert.alert(Const.FAILURE_MSG);
            }
        }

        /// <summary>
        /// 更新煤层信息
        /// </summary>
        private void updateCoalSeamsInfo()
        {
            bool bResultFlag = true;

            for (int i = 0; i < this.dataGridView1.Rows.Count - 1; i++)
            {
                CoalSeams coalSeamsEntity = new CoalSeams();
                // 煤层编号
                if (this.dataGridView1.Rows[i].Cells[0].Value != System.DBNull.Value && this.dataGridView1.Rows[i].Cells[0].Value != null)
                {
                    coalSeamsEntity.CoalSeamsId = Convert.ToInt32(this.dataGridView1.Rows[i].Cells[0].Value);
                }
                // 煤层名称
                if (this.dataGridView1.Rows[i].Cells[1].Value != System.DBNull.Value && this.dataGridView1.Rows[i].Cells[1].Value != null)
                {
                    coalSeamsEntity.CoalSeamsName = Convert.ToString(this.dataGridView1.Rows[i].Cells[1].Value);
                }

                if (this.dataGridView1.Rows[i].Cells[0].Value != System.DBNull.Value && this.dataGridView1.Rows[i].Cells[0].Value != null)
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
            for (int i = 0; i < this.dataGridView1.Rows.Count - 1; i++)
            {
                int count = 0;
                for (int j = 0; j < this.dataGridView1.Rows.Count - 1; j++)
                {
                    if (this.dataGridView1.Rows[i].Cells[1].Value.ToString() == this.dataGridView1.Rows[j].Cells[1].Value.ToString())
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
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            // 窗口关闭
            this.Close();
        }

        /// <summary>
        /// Cell Click事件
        /// 主要用于删除操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // 矿井名称管理
            if (_typeFlag == FLAG_MANANGING_MINE_NAME)
            {
                // 判断列索引是不是删除按钮
                if (e.ColumnIndex == 2)
                {
                    // 最后一行删除按钮设为不可
                    if (this.dataGridView1.RowCount - 1 != this.dataGridView1.CurrentRow.Index)
                    {
                        if (Alert.confirm(Const.DEL_CONFIRM_MSG))
                        {

                            if (this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells[0].Value != System.DBNull.Value
                                && this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells[0].Value != null)
                            {
                                int iMineId = Convert.ToInt32(this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells[0].Value);
                                bool bResult = MineBLL.deleteMineInfo(iMineId);

                                if (bResult)
                                {
                                    // 绑定矿井名称
                                    loadMineInfo();
                                }
                                else
                                {
                                    Alert.alert(Const.DEL_FAILURE_MSG);
                                }
                            }
                            else
                            {
                                this.dataGridView1.Rows.Remove(this.dataGridView1.CurrentRow);
                            }

                        }
                    }

                }
            }

            // 水平名称管理
            if (_typeFlag == FLAG_MANANGING_HORIZONTAL)
            {
                // 判断列索引是不是删除按钮
                if (e.ColumnIndex == 3)
                {
                    // 最后一行删除按钮设为不可
                    if (this.dataGridView1.RowCount - 1 != this.dataGridView1.CurrentRow.Index)
                    {
                        if (Alert.confirm(Const.DEL_CONFIRM_MSG))
                        {

                            if (this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells[0].Value != System.DBNull.Value
                                && this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells[0].Value != null)
                            {
                                int iHorizontalId = Convert.ToInt32(this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells[0].Value);
                                bool bResult = HorizontalBLL.deleteHorizontalInfo(iHorizontalId);

                                if (bResult)
                                {
                                    // 绑定水平信息
                                    loadHorizontalInfo(_id);
                                }
                                else
                                {
                                    Alert.alert(Const.DEL_FAILURE_MSG);
                                }
                            }
                            else
                            {
                                this.dataGridView1.Rows.Remove(this.dataGridView1.CurrentRow);
                            }

                        }
                    }

                }
            }

            if (_typeFlag == FLAG_MANANGING_MINING_AREA)
            {
                // 判断列索引是不是删除按钮
                if (e.ColumnIndex == 3)
                {
                    // 最后一行删除按钮设为不可
                    if (this.dataGridView1.RowCount - 1 != this.dataGridView1.CurrentRow.Index)
                    {
                        if (Alert.confirm(Const.DEL_CONFIRM_MSG))
                        {

                            if (this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells[0].Value != System.DBNull.Value
                                && this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells[0].Value != null)
                            {
                                int iMiningAreaId = Convert.ToInt32(this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells[0].Value);
                                int count = BasicInfoManager.getInstance().getWorkingFaceCountByMiningAreaId(iMiningAreaId);
                                if (count > 0)
                                {
                                    Alert.alert("采区有关联的工作面，请首先解除关联");
                                }
                                else
                                {
                                    bool bResult = MiningAreaBLL.deleteMiningAreaInfo(iMiningAreaId);

                                    if (bResult)
                                    {
                                        // 绑定采区信息
                                        loadMiningAreaInfo(_id);
                                    }
                                    else
                                    {
                                        Alert.alert(Const.DEL_FAILURE_MSG);
                                    }
                                }
                            }
                            else
                            {
                                this.dataGridView1.Rows.Remove(this.dataGridView1.CurrentRow);
                            }

                        }
                    }

                }
            }

            if (_typeFlag == FLAG_MANANGING_WORKING_FACE)
            {
                // 判断列索引是不是删除按钮
                if (e.ColumnIndex == 4)
                {
                    // 最后一行删除按钮设为不可
                    if (this.dataGridView1.RowCount - 1 != this.dataGridView1.CurrentRow.Index)
                    {
                        if (Alert.confirm(Const.DEL_CONFIRM_MSG))
                        {

                            if (this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells[0].Value != System.DBNull.Value
                                && this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells[0].Value != null)
                            {
                                int iWorkingFaceId = Convert.ToInt32(this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells[0].Value);
                                List<Tunnel> list = BasicInfoManager.getInstance().getTunnelListByWorkingFaceId(iWorkingFaceId);
                                if (list != null && list.Count > 0)
                                {
                                    Alert.alert("工作面有关联的巷道，请首先解除关联");
                                }
                                else
                                {
                                    bool bResult = WorkingFaceBLL.deleteWorkingFaceInfo(iWorkingFaceId);

                                    if (bResult)
                                    {
                                        BasicInfoManager.getInstance().refreshWorkingFaceInfo();
                                        // 绑定工作面信息
                                        loadWorkingFaceInfo(_id);
                                    }
                                    else
                                    {
                                        Alert.alert(Const.DEL_FAILURE_MSG);
                                    }
                                }
                            }
                            else
                            {
                                this.dataGridView1.Rows.Remove(this.dataGridView1.CurrentRow);
                            }

                        }
                    }

                }
            }

            // 煤层名称管理
            if (_typeFlag == FLAG_MANANGING_COAL_SEAM)
            {
                // 判断列索引是不是删除按钮
                if (e.ColumnIndex == 2)
                {
                    // 最后一行删除按钮设为不可
                    if (this.dataGridView1.RowCount - 1 != this.dataGridView1.CurrentRow.Index)
                    {
                        if (Alert.confirm(Const.DEL_CONFIRM_MSG))
                        {

                            if (this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells[0].Value != System.DBNull.Value
                                && this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells[0].Value != null)
                            {
                                int iCoalSeamsId = Convert.ToInt32(this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells[0].Value);
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
                                this.dataGridView1.Rows.Remove(this.dataGridView1.CurrentRow);
                            }

                        }
                    }

                }
            }

        }

        /// <summary>
        /// 编号自动排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            if (_typeFlag == 2 || _typeFlag == 3 || _typeFlag == 4)
            {
                this.dataGridView1.Rows[e.Row.Index - 1].Cells[2].Value = _id;
            }
        }
    }
}
