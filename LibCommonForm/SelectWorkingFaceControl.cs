using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LibCommonControl;
using LibBusiness;
using LibCommon;
using LibEntity;

namespace LibCommonForm
{
    public partial class SelectWorkingFaceControl : BaseControl
    {
        // 矿井编号
        private static int _iMineId;
        // 水平编号
        private static int _iHorizontalId;
        // 采区编号
        private static int _iMiningAreaId;
        // 工作面编号
        private static int _iWorkingFaceId;

        private static string _sWorkingFaceName;

        //是否启用过滤
        private bool _isFilterOn;

        private WorkingfaceTypeEnum[] workingfaceTypes { get; set; }
        public WorkingfaceTypeEnum workingfaceType { get; set; }
        public int IWorkingFaceId
        {
            get
            {
                return _iWorkingFaceId;
            }
            set
            {
                _iWorkingFaceId = value;
            }
        }

        public string SWorkingFaceName
        {
            get
            {
                return _sWorkingFaceName;
            }
            set
            {
                _sWorkingFaceName = value;
            }
        }

        //声明巷道名称更改委托
        public delegate void WorkingFaceNameChangedEventHandler(object sender, WorkingFaceEventArgs e);
        //巷道名称更改事件
        public event WorkingFaceNameChangedEventHandler WorkingFaceNameChanged;

        public SelectWorkingFaceControl()
        {
            InitializeComponent();
        }

        public SelectWorkingFaceControl(MainFrm mainFrm)
        {
            InitializeComponent();
            // 加载矿井信息
            //loadMineName();
        }

        /// <summary>
        /// 返回全部ID
        /// </summary>
        /// <returns></returns>
        public int[] getSelectedValueArr()
        {
            int[] intArr = new int[4];
            intArr[0] = _iMineId;
            intArr[1] = _iHorizontalId;
            intArr[2] = _iMiningAreaId;
            intArr[3] = _iWorkingFaceId;

            return intArr;
        }

        /// <summary>
        /// 设置控件中选中内容，数组大小为4,元素内容为对应的ID
        /// </summary>
        /// <param name="intArr">存储所选矿井编号，水平编号，采区编号，工作面编号的数组</param>
        public void setCurSelectedID(int[] intArr)
        {
            // 加载矿井信息
            loadMineName();
            // 设置默认
            this.lstMineName.SelectedValue = intArr[0];
            _iMineId = intArr[0];

            // 加载水平信息
            loadHorizontalName();
            // 设置默认
            this.lstHorizontalName.SelectedValue = intArr[1];
            _iHorizontalId = intArr[1];

            // 加载采区信息
            loadMiningAreaName();
            // 设置默认
            this.lstMiningAreaName.SelectedValue = intArr[2];
            _iMiningAreaId = intArr[2];

            // 加载工作面信息
            loadWorkingFaceName();
            // 设置默认
            this.lstWorkingFaceName.SelectedValue = intArr[3];
            _iWorkingFaceId = intArr[3];
        }

        #region 加载矿井信息
        /// <summary>
        /// 加载矿井信息
        /// </summary>
        public void loadMineName()
        {
            this.lstMineName.DataSource = null;
            this.lstHorizontalName.DataSource = null;
            this.lstMiningAreaName.DataSource = null;
            this.lstWorkingFaceName.DataSource = null;

            // 获取矿井信息
            DataSet ds = MineBLL.selectAllMineInfo();
            // 检索件数
            int iSelCnt = ds.Tables[0].Rows.Count;
            // 检索件数 > 0 的场合

            if (iSelCnt > 0)
            {
                // 绑定矿井信息
                this.lstMineName.DataSource = ds.Tables[0];
                this.lstMineName.DisplayMember = MineDbConstNames.MINE_NAME;
                this.lstMineName.ValueMember = MineDbConstNames.MINE_ID;

                // 2014/05/29 upd by wuxin Start
                //this.lstMineName.SelectedIndex = -1;
                this.lstMineName.SelectedIndex = 0;
                // 2014/05/29 upd by wuxin End

                // 2014/05/29 add by wuxin Start
                // 加载水平信息
                loadHorizontalName();
                // 2014/05/29 add by wuxin End
            }
        }
        #endregion

        #region 矿井名称选择事件
        /// <summary>
        /// 矿井名称选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstMineName_MouseUp(object sender, MouseEventArgs e)
        {
            this.lstHorizontalName.DataSource = null;
            this.lstMiningAreaName.DataSource = null;
            this.lstWorkingFaceName.DataSource = null;

            if (this.lstMineName.SelectedItems.Count > 0)
            {
                //MessageBox.Show("矿井编号：" + this.lstMineName.SelectedValue.ToString());

                // 矿井编号
                int iMineId = Convert.ToInt32(this.lstMineName.SelectedValue);
                _iMineId = iMineId;

                // 获取水平信息
                DataSet ds = HorizontalBLL.selectHorizontalInfoByMineId(iMineId);

                // 检索件数
                int iSelCnt = ds.Tables[0].Rows.Count;
                // 检索件数 > 0 的场合
                if (iSelCnt > 0)
                {
                    // 绑定水平信息
                    this.lstHorizontalName.DataSource = ds.Tables[0];
                    this.lstHorizontalName.DisplayMember = HorizontalDbConstNames.HORIZONTAL_NAME;
                    this.lstHorizontalName.ValueMember = HorizontalDbConstNames.HORIZONTAL_ID;

                    this.lstHorizontalName.SelectedIndex = -1;
                }
            }
        }
        #endregion

        #region 加载水平信息
        /// <summary>
        /// 加载水平信息
        /// </summary>
        private void loadHorizontalName()
        {
            this.lstHorizontalName.DataSource = null;
            this.lstMiningAreaName.DataSource = null;
            this.lstWorkingFaceName.DataSource = null;

            if (this.lstMineName.SelectedItems.Count > 0)
            {
                //MessageBox.Show("矿井编号：" + this.lstMineName.SelectedValue.ToString());

                // 矿井编号
                int iMineId = Convert.ToInt32(this.lstMineName.SelectedValue);
                _iMineId = iMineId;

                // 获取水平信息
                DataSet ds = HorizontalBLL.selectHorizontalInfoByMineId(iMineId);

                // 检索件数
                int iSelCnt = ds.Tables[0].Rows.Count;
                // 检索件数 > 0 的场合
                if (iSelCnt > 0)
                {
                    // 绑定水平信息
                    this.lstHorizontalName.DataSource = ds.Tables[0];
                    this.lstHorizontalName.DisplayMember = HorizontalDbConstNames.HORIZONTAL_NAME;
                    this.lstHorizontalName.ValueMember = HorizontalDbConstNames.HORIZONTAL_ID;

                    this.lstHorizontalName.SelectedIndex = 0;

                    // 加载采区信息
                    loadMiningAreaName();
                }
            }
        }
        #endregion

        #region 水平名称选择事件
        /// <summary>
        /// 水平名称选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstHorizontalName_MouseUp(object sender, MouseEventArgs e)
        {
            this.lstMiningAreaName.DataSource = null;

            if (this.lstHorizontalName.SelectedItems.Count > 0)
            {
                //MessageBox.Show("水平编号：" + this.lstHorizontalName.SelectedValue.ToString());

                // 水平编号
                int iHorizontalId = Convert.ToInt32(this.lstHorizontalName.SelectedValue);
                _iHorizontalId = iHorizontalId;

                // 获取采区信息
                DataSet ds = MiningAreaBLL.selectMiningAreaInfoByHorizontalId(iHorizontalId);

                // 检索件数
                int iSelCnt = ds.Tables[0].Rows.Count;
                // 检索件数 > 0 的场合
                if (iSelCnt > 0)
                {
                    // 绑定采区信息
                    this.lstMiningAreaName.DataSource = ds.Tables[0];
                    this.lstMiningAreaName.DisplayMember = MiningAreaDbConstNames.MININGAREA_NAME;
                    this.lstMiningAreaName.ValueMember = MiningAreaDbConstNames.MININGAREA_ID;

                    this.lstMiningAreaName.SelectedIndex = -1;
                }
            }
        }
        #endregion

        #region 加载采区信息
        /// <summary>
        /// 加载采区信息
        /// </summary>
        private void loadMiningAreaName()
        {
            this.lstMiningAreaName.DataSource = null;
            this.lstWorkingFaceName.DataSource = null;

            if (this.lstHorizontalName.SelectedItems.Count > 0)
            {
                //MessageBox.Show("水平编号：" + this.lstHorizontalName.SelectedValue.ToString());

                // 水平编号
                int iHorizontalId = Convert.ToInt32(this.lstHorizontalName.SelectedValue);
                _iHorizontalId = iHorizontalId;

                // 获取采区信息
                DataSet ds = MiningAreaBLL.selectMiningAreaInfoByHorizontalId(iHorizontalId);

                // 检索件数
                int iSelCnt = ds.Tables[0].Rows.Count;
                // 检索件数 > 0 的场合
                if (iSelCnt > 0)
                {
                    // 绑定采区信息
                    this.lstMiningAreaName.DataSource = ds.Tables[0];
                    this.lstMiningAreaName.DisplayMember = MiningAreaDbConstNames.MININGAREA_NAME;
                    this.lstMiningAreaName.ValueMember = MiningAreaDbConstNames.MININGAREA_ID;

                    this.lstMiningAreaName.SelectedIndex = 0;

                    // 加载工作面信息
                    loadWorkingFaceName();
                }
            }
        }
        #endregion

        #region 采区名称选择事件
        /// <summary>
        /// 采区名称选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstMiningAreaName_MouseUp(object sender, MouseEventArgs e)
        {
            this.lstWorkingFaceName.DataSource = null;

            if (this.lstMiningAreaName.SelectedItems.Count > 0)
            {
                //MessageBox.Show("采区编号：" + this.lstMiningAreaName.SelectedValue.ToString());

                // 采区编号
                int iMiningAreaId = Convert.ToInt32(this.lstMiningAreaName.SelectedValue);
                _iMiningAreaId = iMiningAreaId;

                // 获取工作面信息
                DataSet ds;
                if (_isFilterOn)
                {
                    ds = WorkingFaceBLL.selectWorkingFaceInfoByMiningAreaIdAndWorkingfaceType(iMiningAreaId, workingfaceTypes);
                }
                else
                {
                    ds = WorkingFaceBLL.selectWorkingFaceInfoByMiningAreaId(iMiningAreaId);
                }
                int iSelCnt = 0;
                if (ds.Tables.Count > 0)
                {
                    // 检索件数
                    iSelCnt = ds.Tables[0].Rows.Count;
                }
                // 检索件数 > 0 的场合
                if (iSelCnt > 0)
                {
                    // 绑定工作面信息
                    this.lstWorkingFaceName.DataSource = ds.Tables[0];
                    this.lstWorkingFaceName.DisplayMember = WorkingFaceDbConstNames.WORKINGFACE_NAME;
                    this.lstWorkingFaceName.ValueMember = WorkingFaceDbConstNames.WORKINGFACE_ID;

                    this.lstWorkingFaceName.SelectedIndex = -1;
                }
            }
        }
        #endregion

        #region 加载工作面信息
        /// <summary>
        /// 加载工作面信息
        /// </summary>
        private void loadWorkingFaceName()
        {
            this.lstWorkingFaceName.DataSource = null;

            if (this.lstMiningAreaName.SelectedItems.Count > 0)
            {
                // MessageBox.Show("采区编号：" + this.lstMiningAreaName.SelectedValue.ToString());

                // 采区编号
                int iMiningAreaId = Convert.ToInt32(this.lstMiningAreaName.SelectedValue);
                _iMiningAreaId = iMiningAreaId;

                // 获取工作面信息
                DataSet ds;
                if (_isFilterOn)
                {
                    ds = WorkingFaceBLL.selectWorkingFaceInfoByMiningAreaIdAndWorkingfaceType(iMiningAreaId, workingfaceTypes);
                }
                else
                {
                    ds = WorkingFaceBLL.selectWorkingFaceInfoByMiningAreaId(iMiningAreaId);
                }
                int iSelCnt = 0;
                if (ds.Tables.Count > 0)
                {
                    // 检索件数
                    iSelCnt = ds.Tables[0].Rows.Count;
                }
                // 检索件数 > 0 的场合
                if (iSelCnt > 0)
                {
                    // 绑定工作面信息
                    this.lstWorkingFaceName.DataSource = ds.Tables[0];
                    this.lstWorkingFaceName.DisplayMember = WorkingFaceDbConstNames.WORKINGFACE_NAME;
                    this.lstWorkingFaceName.ValueMember = WorkingFaceDbConstNames.WORKINGFACE_ID;

                    this.lstWorkingFaceName.SelectedIndex = -1;
                }
            }
        }
        #endregion

        #region 工作面名称选择事件
        /// <summary>
        /// 工作面名称选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstWorkingFaceName_MouseUp(object sender, MouseEventArgs e)
        {
            //MessageBox.Show("工作面编号：" + this.lstWorkingFaceName.SelectedValue.ToString());
            if (this.lstWorkingFaceName.SelectedItems.Count > 0)
            {
                //MessageBox.Show("工作面编号：" + this.lstWorkingFaceName.SelectedValue.ToString());

                // 工作面编号
                int iWorkingFaceId = Convert.ToInt32(this.lstWorkingFaceName.SelectedValue);
                _iWorkingFaceId = iWorkingFaceId;


                //_sWorkingFaceName = ((WorkingfaceSimple)this.lstWorkingFaceName.SelectedItem).Name;
                foreach (System.Data.DataRowView item in this.lstWorkingFaceName.SelectedItems)
                {
                    _sWorkingFaceName = item.Row.ItemArray[1].ToString();
                    workingfaceType = (WorkingfaceTypeEnum)Enum.Parse(typeof(WorkingfaceTypeEnum), item.Row.ItemArray[2].ToString());
                }

                //调用事件方法，以便外部能够响应巷道名称改变事件。
                try
                {
                    if (WorkingFaceNameChanged != null)
                    {
                        WorkingFaceEventArgs arg = new WorkingFaceEventArgs(_iWorkingFaceId);
                        WorkingFaceNameChanged(this, arg);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("未正常注册WorkingFaceNameChanged事件: " + ex.Message);
                }
            }
        }
        #endregion


        /// <summary>
        /// 矿井名称Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMineName_Click(object sender, EventArgs e)
        {
            CommonManagement commonManagement = new CommonManagement(1, 999, this.MainForm);
            if (DialogResult.OK == commonManagement.ShowDialog())
            {
                // 绑定矿井信息
                loadMineName();
            }
        }

        /// <summary>
        /// 水平名称Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHorizontalName_Click(object sender, EventArgs e)
        {
            if (this.lstMineName.SelectedItems.Count > 0)
            {
                CommonManagement commonManagement = new CommonManagement(2, _iMineId, this.MainForm);
                if (DialogResult.OK == commonManagement.ShowDialog())
                {
                    // 绑定水平信息
                    loadHorizontalName();
                }
            }
            else
            {
                Alert.alert("请先选择所在矿井名称！");
            }
        }

        /// <summary>
        /// 采区名称Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMiningAreaName_Click(object sender, EventArgs e)
        {
            if (this.lstHorizontalName.SelectedItems.Count > 0)
            {
                CommonManagement commonManagement = new CommonManagement(3, _iHorizontalId, this.MainForm);
                if (DialogResult.OK == commonManagement.ShowDialog())
                {
                    // 绑定采区信息
                    loadMiningAreaName();
                }
            }
            else
            {
                Alert.alert("请先选择所在水平名称！");
            }
        }

        /// <summary>
        /// 工作面名称Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWorkingFaceName_Click(object sender, EventArgs e)
        {
            if (this.lstMiningAreaName.SelectedItems.Count > 0)
            {
                CommonManagement commonManagement = new CommonManagement(4, _iMiningAreaId, this.MainForm);
                if (DialogResult.OK == commonManagement.ShowDialog())
                {
                    // 绑定工作面信息
                    loadWorkingFaceName();
                }
            }
            else
            {
                Alert.alert("请先选择所在采区名称！");
            }
        }

        /// <summary>
        /// 工作面过滤设置（规则过滤）
        /// </summary>
        /// <param name="tunnelFilterRules"></param>
        public void SetFilterOn(WorkingfaceTypeEnum workingfaceType)
        {
            _isFilterOn = true;
            this.workingfaceTypes[0] = workingfaceType;
        }

        public void SetFilterOn(params WorkingfaceTypeEnum[] workingfaceTypes)
        {
            _isFilterOn = true;
            this.workingfaceTypes = workingfaceTypes;
        }
    }

    public class WorkingFaceEventArgs : EventArgs
    {
        //工作面ID
        private int _workingFaceID;
        //构造函数
        public WorkingFaceEventArgs(int workingFaceID)
        {
            _workingFaceID = workingFaceID;
        }
    }
}
