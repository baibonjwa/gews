using System;
using System.Windows.Forms;
using LibCommonControl;
using LibBusiness;
using LibCommon;
using LibEntity;

namespace LibCommonForm
{
    public partial class SelectWorkingFaceControl : BaseControl
    {

        public WorkingFace SelectedWorkingFace { get; set; }

        public SelectWorkingFaceControl()
        {
            InitializeComponent();
        }

        public SelectWorkingFaceControl(WorkingFace workingFace)
        {
            LoadMineData();
            lstMineName.SelectedItem = workingFace.MiningArea.Horizontal.Mine;
            lstHorizontalName.SelectedItem = workingFace.MiningArea.Horizontal;
            lstMiningAreaName.SelectedItem = workingFace.MiningArea;
            lstWorkingFaceName.SelectedItem = workingFace;
        }

        public SelectWorkingFaceControl(MiningArea miningArea)
        {
            LoadMineData();
            lstMineName.SelectedItem = miningArea.Horizontal.Mine;
            lstHorizontalName.SelectedItem = miningArea.Horizontal;
            lstMiningAreaName.SelectedItem = miningArea;
        }


        #region 加载矿井信息
        /// <summary>
        /// 加载矿井信息
        /// </summary>
        public void LoadMineData()
        {
            DataBindUtil.LoadMineName(lstMineName);
        }
        #endregion

        /// <summary>
        /// 矿井名称Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMineName_Click(object sender, EventArgs e)
        {
            var commonManagement = new CommonManagement(1, 999);
            if (DialogResult.OK == commonManagement.ShowDialog())
            {
                DataBindUtil.LoadMineName(lstMineName);
            }
        }

        /// <summary>
        /// 水平名称Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHorizontalName_Click(object sender, EventArgs e)
        {
            if (lstMineName.SelectedItems.Count > 0)
            {
                var commonManagement = new CommonManagement(2, Convert.ToInt32(lstMineName.SelectedValue));
                if (DialogResult.OK == commonManagement.ShowDialog())
                {
                    if (lstMineName.SelectedItems.Count <= 0) return;
                    var mine = (Mine)lstMineName.SelectedItem;
                    DataBindUtil.LoadHorizontalName(lstHorizontalName, mine.MineId);
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
            if (lstHorizontalName.SelectedItems.Count > 0)
            {
                var commonManagement = new CommonManagement(3, Convert.ToInt32(lstHorizontalName.SelectedValue));
                if (DialogResult.OK == commonManagement.ShowDialog())
                {
                    if (lstHorizontalName.SelectedItems.Count <= 0) return;
                    var horizontal = (Horizontal)lstHorizontalName.SelectedItem;
                    DataBindUtil.LoadMiningAreaName(lstMiningAreaName, horizontal.HorizontalId);
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
            if (lstMiningAreaName.SelectedItems.Count > 0)
            {
                var commonManagement = new CommonManagement(4, Convert.ToInt32(lstMiningAreaName.SelectedValue));
                if (DialogResult.OK == commonManagement.ShowDialog())
                {
                    if (lstMiningAreaName.SelectedItems.Count <= 0) return;
                    var miningArea = (MiningArea)lstMiningAreaName.SelectedItem;
                    DataBindUtil.LoadWorkingFaceName(lstWorkingFaceName, miningArea.MiningAreaId);
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
        //public void SetFilterOn(WorkingfaceTypeEnum workingfaceType)
        //{
        //    _isFilterOn = true;
        //    this.workingfaceTypes[0] = workingfaceType;
        //}

        //public void SetFilterOn(params WorkingfaceTypeEnum[] workingfaceTypes)
        //{
        //    _isFilterOn = true;
        //    this.workingfaceTypes = workingfaceTypes;
        //}

        private void lstMineName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstMineName.SelectedItems.Count <= 0) return;
            var mine = (Mine)lstMineName.SelectedItem;
            DataBindUtil.LoadHorizontalName(lstHorizontalName, mine.MineId);
        }

        private void lstHorizontalName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstHorizontalName.SelectedItems.Count <= 0) return;
            var horizontal = (Horizontal)lstHorizontalName.SelectedItem;
            DataBindUtil.LoadMiningAreaName(lstMiningAreaName, horizontal.HorizontalId);
        }

        private void lstMiningAreaName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstMiningAreaName.SelectedItems.Count <= 0) return;
            var miningArea = (MiningArea)lstMiningAreaName.SelectedItem;
            DataBindUtil.LoadWorkingFaceName(lstWorkingFaceName, miningArea.MiningAreaId);
        }

        private void lstWorkingFaceName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstWorkingFaceName.SelectedItems.Count <= 0)
                SelectedWorkingFace = null;
            else
                SelectedWorkingFace = (WorkingFace)lstWorkingFaceName.SelectedItem;
        }
    }
}
