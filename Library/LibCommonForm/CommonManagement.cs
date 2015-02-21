﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Castle.ActiveRecord;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting.Native;
using LibBusiness;
using LibCommon;
using LibCommonControl;
using LibEntity;
using NHibernate.Linq;

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
        public CommonManagement()
        {
            InitializeComponent();
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
                        gridView1.Columns.Add(new GridColumn
                        {
                            Caption = @"编号",
                            FieldName = "MineId",
                            MaxWidth = 60,
                            VisibleIndex = gridView1.Columns.Count
                        });
                        // 矿井名称
                        gridView1.Columns.Add(new GridColumn
                        {
                            Caption = @"矿井名称",
                            FieldName = "MineName",
                            VisibleIndex = gridView1.Columns.Count
                        });

                        AddDeleteButton();

                        gridControl1.DataSource = Mine.FindAll();
                    }
                    break;
                case FlagManangingHorizontal:
                    {
                        gridView1.Columns.Add(new GridColumn
                        {
                            Caption = @"编号",
                            FieldName = "HorizontalId",
                            MaxWidth = 60,
                            VisibleIndex = gridView1.Columns.Count
                        });
                        // 矿井名称
                        gridView1.Columns.Add(new GridColumn
                        {
                            Caption = @"水平名称",
                            FieldName = "HorizontalName",
                            VisibleIndex = gridView1.Columns.Count
                        });

                        // 所属矿井
                        var mineComboBox = new RepositoryItemLookUpEdit();

                        mineComboBox.Columns.Add(new LookUpColumnInfo("MineId", "编号"));
                        mineComboBox.Columns.Add(new LookUpColumnInfo("MineName", "矿井名称"));
                        mineComboBox.DataSource = Mine.FindAll();
                        mineComboBox.DisplayMember = "MineName";
                        mineComboBox.ValueMember = "MineId";

                        gridView1.Columns.Add(new GridColumn
                        {
                            Caption = @"所在矿区",
                            FieldName = "Mine.MineId",
                            ColumnEdit = mineComboBox,
                            VisibleIndex = gridView1.Columns.Count
                        });

                        AddDeleteButton();

                        gridControl1.DataSource = Horizontal.FindAll();
                    }
                    break;
                case FlagManangingMiningArea:
                    {
                        gridView1.Columns.Add(new GridColumn
                        {
                            Caption = @"编号",
                            FieldName = "MiningAreaId",
                            MaxWidth = 60,
                            VisibleIndex = gridView1.Columns.Count
                        });
                        // 矿井名称
                        gridView1.Columns.Add(new GridColumn
                        {
                            Caption = @"采区名称",
                            FieldName = "MiningAreaName",
                            VisibleIndex = gridView1.Columns.Count
                        });


                        // 所属水平
                        var horizontalComboBox = new RepositoryItemLookUpEdit();

                        horizontalComboBox.Columns.Add(new LookUpColumnInfo("HorizontalId", "编号"));
                        horizontalComboBox.Columns.Add(new LookUpColumnInfo("HorizontalName", "水平名称"));
                        horizontalComboBox.DataSource = Horizontal.FindAll();
                        horizontalComboBox.DisplayMember = "HorizontalName";
                        horizontalComboBox.ValueMember = "HorizontalId";

                        gridView1.Columns.Add(new GridColumn
                        {
                            Caption = @"所在水平",
                            FieldName = "Horizontal.HorizontalId",
                            ColumnEdit = horizontalComboBox,
                            VisibleIndex = gridView1.Columns.Count
                        });

                        AddDeleteButton();
                        gridControl1.DataSource = MiningArea.FindAll();

                    }
                    break;
                case FlagManangingWorkingFace:
                    {
                        Text = @"工作面名称管理";
                        gridView1.Columns.Add(new GridColumn
                        {
                            Caption = @"编号",
                            FieldName = "WorkingFaceId",
                            MaxWidth = 60,
                            VisibleIndex = gridView1.Columns.Count
                        });
                        // 矿井名称
                        gridView1.Columns.Add(new GridColumn
                        {
                            Caption = @"工作面名称",
                            FieldName = "WorkingFaceName",
                            VisibleIndex = gridView1.Columns.Count
                        });


                        gridView1.Columns.Add(new GridColumn
                        {
                            Caption = @"所在采区",
                            FieldName = "MiningArea",
                            ColumnEdit = lueMiningArea,
                            VisibleIndex = gridView1.Columns.Count
                        });

                        AddDeleteButton();
                        lueMiningArea.DataSource = MiningArea.FindAll();
                        gridControl1.DataSource = WorkingFace.FindAll();
                    }
                    break;

                case FlagManangingCoalSeam:
                    {
                        //// 窗口标题
                        Text = @"煤层名称管理";
                        gridView1.Columns.Add(new GridColumn
                        {
                            Caption = @"编号",
                            FieldName = "CoalSeamId",
                            MaxWidth = 60,
                            VisibleIndex = gridView1.Columns.Count
                        });
                        // 矿井名称
                        gridView1.Columns.Add(new GridColumn
                        {
                            Caption = @"煤层名称",
                            FieldName = "CoalSeamName",
                            VisibleIndex = gridView1.Columns.Count
                        });
                        AddDeleteButton();
                        gridControl1.DataSource = CoalSeams.FindAll();
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
        ///     提交
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            // 验证
            if (!Check())
            {
                DialogResult = DialogResult.None;
                return;
            }
            DialogResult = DialogResult.OK;

            switch (_typeFlag)
            {
                case FlagManangingMineName:
                    // 矿井名称管理
                    UpdateInfo<Mine>();
                    break;
                case FlagManangingHorizontal:
                    // 水平名称管理
                    UpdateInfo<Horizontal>();
                    break;
                case FlagManangingMiningArea:
                    // 采区名称管理
                    UpdateInfo<MiningArea>();
                    break;
                case FlagManangingWorkingFace:
                    // 工作面名称管理
                    UpdateInfo<WorkingFace>();
                    break;
                case FlagManangingCoalSeam:
                    // 煤层名称管理
                    UpdateInfo<CoalSeams>();
                    break;
            }
        }

        /// <summary>
        ///     更新矿井信息
        /// </summary>
        private void UpdateInfo<T>() where T : ActiveRecordBase
        {
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                int rowHandle = gridView1.GetVisibleRowHandle(i);
                var obj = (T)gridView1.GetRow(rowHandle);
                if (obj != null) obj.Save();
            }
            Alert.alert(Const.SUCCESS_MSG);
        }

        /// <summary>
        ///     更新水平信息
        /// </summary>
        private void UpdateHorizontalInfo()
        {
            //for (int i = 0; i < gridView1.Rows.Count - 1; i++)
            //{
            //    var horizontalEntity = new Horizontal();
            //    // 水平编号
            //    if (gridView1.Rows[i].Cells[0].Value != DBNull.Value && gridView1.Rows[i].Cells[0].Value != null)
            //    {
            //        horizontalEntity.HorizontalId = Convert.ToInt32(gridView1.Rows[i].Cells[0].Value);
            //    }
            //    // 水平名称
            //    if (gridView1.Rows[i].Cells[1].Value != DBNull.Value && gridView1.Rows[i].Cells[1].Value != null)
            //    {
            //        horizontalEntity.HorizontalName = Convert.ToString(gridView1.Rows[i].Cells[1].Value);
            //    }

            //    //***************************************
            //    // 所属矿井
            //    if (gridView1.Rows[i].Cells[2].Value != DBNull.Value && gridView1.Rows[i].Cells[2].Value != null)
            //    {
            //        horizontalEntity.Mine.MineId = Convert.ToInt32(gridView1.Rows[i].Cells[2].Value);
            //    }
            //    //***************************************

            //    if (gridView1.Rows[i].Cells[0].Value != DBNull.Value && gridView1.Rows[i].Cells[0].Value != null)
            //    {
            //        horizontalEntity.Save();
            //    }
            //    else
            //    {
            //        horizontalEntity.Save();
            //    }
            //}
            //DataBindUtil.LoadHorizontalName(gridView1, _id);
            //Alert.alert(Const.SUCCESS_MSG);
        }

        /// <summary>
        ///     更新采区信息
        /// </summary>
        private void UpdateMiningAreaInfo()
        {

            //for (int i = 0; i < gridView1.Rows.Count - 1; i++)
            //{
            //    int miningAreaId = Const.INVALID_ID;
            //    string miningAreaName = string.Empty;
            //    int horizontalId = Const.INVALID_ID;

            //    // 采区编号
            //    if (gridView1.Rows[i].Cells[0].Value != DBNull.Value && gridView1.Rows[i].Cells[0].Value != null)
            //    {
            //        miningAreaId = Convert.ToInt32(gridView1.Rows[i].Cells[0].Value);
            //    }
            //    // 采区名称
            //    if (gridView1.Rows[i].Cells[1].Value != DBNull.Value && gridView1.Rows[i].Cells[1].Value != null)
            //    {
            //        miningAreaName = Convert.ToString(gridView1.Rows[i].Cells[1].Value);
            //    }

            //    //***************************************
            //    // 所属水平
            //    if (gridView1.Rows[i].Cells[2].Value != DBNull.Value && gridView1.Rows[i].Cells[2].Value != null)
            //    {
            //        horizontalId = Convert.ToInt32(gridView1.Rows[i].Cells[2].Value);
            //    }

            //    //***************************************

            //    if (gridView1.Rows[i].Cells[0].Value != DBNull.Value && gridView1.Rows[i].Cells[0].Value != null)
            //    {
            //        var miningArea = MiningArea.Find(miningAreaId);
            //        miningArea.MiningAreaName = miningAreaName;
            //        miningArea.Horizontal = Horizontal.Find(horizontalId);
            //        miningArea.Save();
            //    }
            //    else
            //    {
            //        var miningArea = new MiningArea
            //        {
            //            MiningAreaName = miningAreaName,
            //            Horizontal = Horizontal.Find(horizontalId)
            //        };
            //        miningArea.Save();
            //    }
            //}
            //DataBindUtil.LoadMiningAreaName(gridView1, _id);
            //Alert.alert(Const.SUCCESS_MSG);
            //// 绑定采区信息

        }

        private void UpdateWorkingFaceInfo()
        {
            //for (int i = 0; i < gridView1.Rows.Count - 1; i++)
            //{
            //    DataGridViewCellCollection cells = gridView1.Rows[i].Cells;
            //    int tmpWorkingFaceId = Const.INVALID_ID;
            //    string tmpWorkingFaceName = string.Empty;
            //    int miningAreaId = Const.INVALID_ID;
            //    int workingfaceType = 0;
            //    // 工作面编号
            //    if (cells[0].Value != DBNull.Value && cells[0].Value != null)
            //    {
            //        tmpWorkingFaceId = Convert.ToInt32(cells[0].Value);
            //    }
            //    // 工作面名称
            //    if (cells[1].Value != DBNull.Value && cells[1].Value != null)
            //    {
            //        tmpWorkingFaceName = Convert.ToString(cells[1].Value);
            //    }

            //    //***************************************
            //    // 所属采区
            //    if (cells[2].Value != DBNull.Value && cells[2].Value != null)
            //    {
            //        miningAreaId = Convert.ToInt32(cells[2].Value);
            //    }

            //    if (cells[3].Value != DBNull.Value &&
            //        cells[3].Value != null)
            //    {
            //        workingfaceType = Convert.ToInt32(cells[3].Value);
            //    }

            //    //***************************************

            //    if (cells[0].Value != DBNull.Value && cells[0].Value != null)
            //    {
            //        var workingFace = WorkingFace.Find(tmpWorkingFaceId);
            //        workingFace.WorkingFaceName = tmpWorkingFaceName;
            //        workingFace.MiningArea = MiningArea.Find(miningAreaId);
            //        workingFace.WorkingfaceTypeEnum = (WorkingfaceTypeEnum)workingfaceType;

            //        workingFace.Save();
            //    }
            //    else
            //    {
            //        var workingFace = new WorkingFace
            //        {
            //            WorkingFaceName = tmpWorkingFaceName,
            //            MiningArea = MiningArea.Find(miningAreaId),
            //            WorkingfaceTypeEnum = (WorkingfaceTypeEnum)workingfaceType
            //        };
            //        workingFace.Save();
            //    }
            //}

            //DataBindUtil.LoadWorkingFaceName(gridView1, _id);
            //Alert.alert(Const.SUCCESS_MSG);
        }

        /// <summary>
        ///     更新煤层信息
        /// </summary>
        private void UpdateCoalSeamsInfo()
        {
            //bool bResultFlag = true;

            //for (int i = 0; i < gridView1.Rows.Count - 1; i++)
            //{
            //    var coalSeamsEntity = new CoalSeams();
            //    // 煤层编号
            //    if (gridView1.Rows[i].Cells[0].Value != DBNull.Value && gridView1.Rows[i].Cells[0].Value != null)
            //    {
            //        coalSeamsEntity.CoalSeamsId = Convert.ToInt32(gridView1.Rows[i].Cells[0].Value);
            //    }
            //    // 煤层名称
            //    if (gridView1.Rows[i].Cells[1].Value != DBNull.Value && gridView1.Rows[i].Cells[1].Value != null)
            //    {
            //        coalSeamsEntity.CoalSeamsName = Convert.ToString(gridView1.Rows[i].Cells[1].Value);
            //    }

            //    if (gridView1.Rows[i].Cells[0].Value != DBNull.Value && gridView1.Rows[i].Cells[0].Value != null)
            //    {
            //        bResultFlag = CoalSeamsBLL.updateCoalSeamsInfo(coalSeamsEntity);
            //    }
            //    else
            //    {
            //        bResultFlag = CoalSeamsBLL.insertCoalSeamsInfo(coalSeamsEntity);
            //    }

            //    if (!bResultFlag)
            //    {
            //        break;
            //    }
            //}

            //// 执行结果判断
            //if (bResultFlag)
            //{
            //    Alert.alert(Const.SUCCESS_MSG);
            //    // 绑定煤层名称
            //    DataBindUtil.LoadCoalSeamsName(gridView1);
            //}
            //else
            //{
            //    Alert.alert(Const.FAILURE_MSG);
            //}
        }

        private bool Check()
        {
            //for (int i = 0; i < gridView1.Rows.Count - 1; i++)
            //{
            //    int count = 0;
            //    for (int j = 0; j < gridView1.Rows.Count - 1; j++)
            //    {
            //        if (gridView1.Rows[i].Cells[1].Value.ToString() ==
            //            gridView1.Rows[j].Cells[1].Value.ToString())
            //        {
            //            count = count + 1;
            //        }
            //    }

            //    if (count >= 2)
            //    {
            //        Alert.alert(Const.WORKINGFACENAMESAME_MSG);
            //        return false;
            //    }
            //}
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
        private void gridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // 矿井名称管理
            //if (_typeFlag == FlagManangingMineName)
            //{
            //    // 判断列索引是不是删除按钮
            //    if (e.ColumnIndex == 2)
            //    {
            //        // 最后一行删除按钮设为不可
            //        if (gridView1.CurrentRow != null && gridView1.RowCount - 1 != gridView1.CurrentRow.Index)
            //        {
            //            if (Alert.confirm(Const.DEL_CONFIRM_MSG))
            //            {
            //                if (gridView1.Rows[gridView1.CurrentRow.Index].Cells[0].Value != DBNull.Value
            //                    && gridView1.Rows[gridView1.CurrentRow.Index].Cells[0].Value != null)
            //                {
            //                    int iMineId =
            //                        Convert.ToInt32(gridView1.Rows[gridView1.CurrentRow.Index].Cells[0].Value);

            //                    Mine.Find(iMineId).Delete();
            //                    // 绑定矿井名称
            //                    DataBindUtil.LoadMineName(gridView1);

            //                    Alert.alert(Const.DEL_FAILURE_MSG);
            //                }
            //                else
            //                {
            //                    gridView1.Rows.Remove(gridView1.CurrentRow);
            //                }
            //            }
            //        }
            //    }
            //}

            //// 水平名称管理
            //if (_typeFlag == FlagManangingHorizontal)
            //{
            //    // 判断列索引是不是删除按钮
            //    if (e.ColumnIndex == 3)
            //    {
            //        // 最后一行删除按钮设为不可
            //        if (gridView1.CurrentRow != null && gridView1.RowCount - 1 != gridView1.CurrentRow.Index)
            //        {
            //            if (Alert.confirm(Const.DEL_CONFIRM_MSG))
            //            {
            //                if (gridView1.Rows[gridView1.CurrentRow.Index].Cells[0].Value != DBNull.Value
            //                    && gridView1.Rows[gridView1.CurrentRow.Index].Cells[0].Value != null)
            //                {
            //                    int iHorizontalId =
            //                        Convert.ToInt32(gridView1.Rows[gridView1.CurrentRow.Index].Cells[0].Value);
            //                    Horizontal.Find(iHorizontalId).Delete();
            //                    // 绑定水平信息
            //                    DataBindUtil.LoadHorizontalName(gridView1, _id);

            //                }
            //                else
            //                {
            //                    gridView1.Rows.Remove(gridView1.CurrentRow);
            //                }
            //            }
            //        }
            //    }
            //}

            //if (_typeFlag == FlagManangingMiningArea)
            //{
            //    // 判断列索引是不是删除按钮
            //    if (e.ColumnIndex == 3)
            //    {
            //        // 最后一行删除按钮设为不可
            //        if (gridView1.CurrentRow != null && gridView1.RowCount - 1 != gridView1.CurrentRow.Index)
            //        {
            //            if (Alert.confirm(Const.DEL_CONFIRM_MSG))
            //            {
            //                if (gridView1.Rows[gridView1.CurrentRow.Index].Cells[0].Value != DBNull.Value
            //                    && gridView1.Rows[gridView1.CurrentRow.Index].Cells[0].Value != null)
            //                {
            //                    int iMiningAreaId =
            //                        Convert.ToInt32(gridView1.Rows[gridView1.CurrentRow.Index].Cells[0].Value);
            //                    if (WorkingFace.ExistsByMiningAreaId(iMiningAreaId))
            //                    {
            //                        Alert.alert("采区有关联的工作面，请首先解除关联");
            //                    }
            //                    else
            //                    {
            //                        var miningArea = MiningArea.Find(iMiningAreaId);
            //                        miningArea.Delete();

            //                        // 绑定采区信息
            //                        DataBindUtil.LoadMiningAreaName(gridView1, _id);
            //                    }
            //                }
            //                else
            //                {
            //                    gridView1.Rows.Remove(gridView1.CurrentRow);
            //                }
            //            }
            //        }
            //    }
            //}

            //if (_typeFlag == FlagManangingWorkingFace)
            //{
            //    // 判断列索引是不是删除按钮
            //    if (e.ColumnIndex == 4)
            //    {
            //        // 最后一行删除按钮设为不可
            //        if (gridView1.CurrentRow != null && gridView1.RowCount - 1 != gridView1.CurrentRow.Index)
            //        {
            //            if (Alert.confirm(Const.DEL_CONFIRM_MSG))
            //            {
            //                if (gridView1.Rows[gridView1.CurrentRow.Index].Cells[0].Value != DBNull.Value
            //                    && gridView1.Rows[gridView1.CurrentRow.Index].Cells[0].Value != null)
            //                {
            //                    int iWorkingFaceId =
            //                        Convert.ToInt32(gridView1.Rows[gridView1.CurrentRow.Index].Cells[0].Value);


            //                    IList<Tunnel> list = WorkingFace.Find(iWorkingFaceId).Tunnels;
            //                    if (list != null && list.Count > 0)
            //                    {
            //                        Alert.alert("工作面有关联的巷道，请首先解除关联");
            //                    }
            //                    else
            //                    {
            //                        var workingFace = WorkingFace.Find(iWorkingFaceId);
            //                        workingFace.Delete();
            //                        // 绑定工作面信息
            //                        DataBindUtil.LoadWorkingFaceName(gridView1, _id);
            //                    }
            //                }
            //                else
            //                {
            //                    gridView1.Rows.Remove(gridView1.CurrentRow);
            //                }
            //            }
            //        }
            //    }
            //}

            //// 煤层名称管理
            //if (_typeFlag != FlagManangingCoalSeam) return;
            //// 判断列索引是不是删除按钮
            //if (e.ColumnIndex != 2) return;
            //// 最后一行删除按钮设为不可
            //if (gridView1.CurrentRow == null || gridView1.RowCount - 1 == gridView1.CurrentRow.Index)
            //    return;
            //if (!Alert.confirm(Const.DEL_CONFIRM_MSG)) return;
            //if (gridView1.Rows[gridView1.CurrentRow.Index].Cells[0].Value != DBNull.Value
            //    && gridView1.Rows[gridView1.CurrentRow.Index].Cells[0].Value != null)
            //{
            //    int iCoalSeamsId =
            //        Convert.ToInt32(gridView1.Rows[gridView1.CurrentRow.Index].Cells[0].Value);
            //    bool bResult = CoalSeamsBLL.deleteCoalSeamsInfo(iCoalSeamsId);

            //    if (bResult)
            //    {
            //        // 绑定矿煤层信息
            //        DataBindUtil.LoadCoalSeamsName(gridView1);
            //    }
            //    else
            //    {
            //        Alert.alert(Const.DEL_FAILURE_MSG);
            //    }
            //}
            //else
            //{
            //    gridView1.Rows.Remove(gridView1.CurrentRow);
            //}
        }

        /// <summary>
        ///     编号自动排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            //if (_typeFlag == 2 || _typeFlag == 3 || _typeFlag == 4)
            //{
            //    gridView1.Rows[e.Row.Index - 1].Cells[2].Value = _id;
            //}
        }

        private void AddDeleteButton()
        {
            var delButton = new RepositoryItemButtonEdit();
            delButton.Buttons.Clear();
            delButton.TextEditStyle = TextEditStyles.HideTextEditor;
            var button = new EditorButton { Kind = ButtonPredefines.Glyph, Caption = "删除" };
            delButton.Buttons.Add(button);
            //delButton.Buttons.Add(new EditorButton(ButtonPredefines.Delete));
            gridView1.Columns.Add(new GridColumn
            {
                Caption = @"删除",
                MaxWidth = 60,
                VisibleIndex = gridView1.Columns.Count,
                ColumnEdit = delButton
            });
        }
    }
}