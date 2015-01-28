// ******************************************************************
// 概  述：勘探线数据录入界面
// 作  者：伍鑫
// 创建日期：2014/03/05
// 版本号：V1.0
// 版本信息：
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
using LibBusiness;
using System.Security.Policy;
using System.Collections;
using LibEntity;
using LibCommon;
using GIS.Common;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;

namespace _3.GeologyMeasure
{
    public partial class ProspectingLineInfoEntering : Form
    {
        /** 主键  **/
        private int _iPK;
        /** 业务逻辑类型：添加/修改  **/
        private string _bllType = "add";

        //public event EventHandler<ItemClickEventArgs> ListBoxItemClick;

        //public class ItemClickEventArgs : EventArgs
        //{
        //    public int Index { get; set; }
        //}

        /// <summary>
        /// 构造方法
        /// </summary>
        public ProspectingLineInfoEntering()
        {
            InitializeComponent();

            // 设置窗体默认属性
            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_GM.INSERT_PROSPECTING_LINE_INFO);

            //this.listBox1.Click += (ss, ee) =>
            //{
            //    ListBox lb = ss as ListBox;
            //    Point p = lb.PointToClient(Control.MousePosition);
            //    for (int i = 0; i < lb.Items.Count; i++)
            //    {
            //        if (ListBoxItemClick != null && lb.GetItemRectangle(i).Contains(p))
            //        {
            //            ListBoxItemClick.Invoke(lb, new ItemClickEventArgs() { Index = i });
            //            break;
            //        }
            //    }
            //};

            //this.ListBoxItemClick += (s, e) =>
            //{
            //    MessageBox.Show(e.Index.ToString());
            //};

            // 绑定钻孔信息
        }

        /// <summary>
        /// 带参数的构造方法
        /// </summary>
        /// <param name="strPrimaryKey">主键</param>
        public ProspectingLineInfoEntering(string strPrimaryKey)
        {
            InitializeComponent();
            // 主键
            int iPK = 0;
            if (int.TryParse(strPrimaryKey, out iPK))
            {
                // 主键
                this._iPK = iPK;

                // 设置窗体默认属性
                FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_GM.UPDATE_PROSPECTING_LINE_INFO);

                // 设置业务类型
                this._bllType = "update";

            }
        }

        /// <summary>
        /// 选择（小）断层 【→】
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.lstProspectingBoreholeAll.SelectedItems.Count; )
            {
                // 将左侧ListBox中选择的数据添加到右侧ListBox中
                this.lstProspectingBoreholeSelected.Items.Add(this.lstProspectingBoreholeAll.SelectedItems[i].ToString());
                // 移除左侧ListBox中选择添加的数据
                this.lstProspectingBoreholeAll.Items.RemoveAt(this.lstProspectingBoreholeAll.SelectedIndices[0]);
            }

        }

        /// <summary>
        /// 将已经选择的（小）断层移除 【←】
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeltete_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.lstProspectingBoreholeSelected.SelectedItems.Count; )
            {
                // 将右侧ListBox中选择移除的数据恢复到左侧ListBox中
                this.lstProspectingBoreholeAll.Items.Add(this.lstProspectingBoreholeSelected.SelectedItems[i].ToString());
                // 移除右侧ListBox中选择的数据
                this.lstProspectingBoreholeSelected.Items.RemoveAt(this.lstProspectingBoreholeSelected.SelectedIndices[0]);
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

            // 创建勘探线实体
            ProspectingLine prospectingLineEntity = new ProspectingLine();
            // 勘探线名称
            prospectingLineEntity.ProspectingLineName = this.txtProspectingLineName.Text.Trim();
            // 勘探线钻孔
            int cnt = this.lstProspectingBoreholeSelected.Items.Count;
            List<IPoint> lstProspectingBoreholePts = new List<IPoint>();//20140505 lyf 存储选择的钻孔点要素
            for (int i = 0; i < cnt; i++)
            {
                String strDisplayName = this.lstProspectingBoreholeSelected.Items[i].ToString();
                if (Validator.IsEmpty(prospectingLineEntity.ProspectingBorehole))
                {
                    prospectingLineEntity.ProspectingBorehole = strDisplayName;
                }
                else
                {
                    prospectingLineEntity.ProspectingBorehole = prospectingLineEntity.ProspectingBorehole + "," + strDisplayName;
                }

                ///20140505 lyf
                ///根据钻孔点名查找钻孔点信息  
                IPoint pt = new PointClass();
                pt = GetProspectingBoreholePointSelected(strDisplayName);
                if (pt != null && !lstProspectingBoreholePts.Contains(pt))
                {
                    lstProspectingBoreholePts.Add(pt);
                }
            }

            bool bResult = false;
            if (this._bllType == "add")
            {
                // BIDID
                prospectingLineEntity.BindingId = IDGenerator.NewBindingID();

                // 勘探线信息登录
                bResult = ProspectingLineBLL.insertProspectingLineInfo(prospectingLineEntity);

                ///20140505 lyf
                ///绘制勘探线图形
                DrawProspectingLine(prospectingLineEntity, lstProspectingBoreholePts);
            }
            else
            {
                // 主键
                prospectingLineEntity.ProspectingLineId = this._iPK;
                // 勘探线信息修改
                bResult = ProspectingLineBLL.updateProspectingLineInfo(prospectingLineEntity);

                //20140506 lyf 
                //获取勘探线的BID
                string sBID = "";
                ProspectingLineBLL.selectProspectingLineBIDByProspectingLineID(prospectingLineEntity.ProspectingLineId, out sBID);
                if (sBID != "")
                {
                    prospectingLineEntity.BindingId = sBID;
                    ModifyProspectingLine(prospectingLineEntity, lstProspectingBoreholePts);//修改图元
                }
            }

            // 添加/修改成功的场合
            if (bResult)
            {

            }

        }

        #region 绘制勘探线

        /// <summary>
        /// 修改勘探线图元
        /// </summary>
        /// <param name="prospectingLineEntity"></param>
        /// <param name="lstProspectingBoreholePts"></param>
        private void ModifyProspectingLine(ProspectingLine prospectingLineEntity, List<IPoint> lstProspectingBoreholePts)
        {
            //1.获得当前编辑图层
            DrawSpecialCommon drawspecial = new DrawSpecialCommon();
            string sLayerAliasName = GIS.LayerNames.DEFALUT_KANTANXIAN;//“默认_勘探线”图层
            IFeatureLayer featureLayer = drawspecial.GetFeatureLayerByName(sLayerAliasName);
            if (featureLayer == null)
            {
                MessageBox.Show("未找到" + sLayerAliasName + "图层,无法修改勘探线图元。");
                return;
            }

            //2.删除原来图元，重新绘制新图元
            bool bIsDeleteOldFeature = DataEditCommon.DeleteFeatureByBId(featureLayer, prospectingLineEntity.BindingId);
            if (bIsDeleteOldFeature)
            {
                //绘制图元
                DrawProspectingLine(prospectingLineEntity, lstProspectingBoreholePts);
            }

        }

        /// <summary>
        /// 根据钻孔点名查找钻孔点信息  
        /// </summary>
        /// <param name="strDisplayName"></param>
        /// <returns></returns>
        private IPoint GetProspectingBoreholePointSelected(String strDisplayName)
        {
            try
            {
                Borehole brehole = Borehole.FindOneByBoreholeNum(strDisplayName);

                IPoint pt = new PointClass();
                if (brehole != null)
                {
                    pt.X = brehole.CoordinateX;
                    pt.Y = brehole.CoordinateX;
                    pt.Z = brehole.CoordinateZ;
                }

                return pt;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 根据所选钻孔点绘制勘探线
        /// </summary>
        /// <param name="prospectingLineEntity"></param>
        /// <param name="lstProspectingBoreholePts"></param>
        private void DrawProspectingLine(ProspectingLine prospectingLineEntity, List<IPoint> lstProspectingBoreholePts)
        {
            //1.获得当前编辑图层
            DrawSpecialCommon drawspecial = new DrawSpecialCommon();
            string sLayerAliasName = GIS.LayerNames.DEFALUT_KANTANXIAN;//“默认_勘探线”图层
            IFeatureLayer featureLayer = drawspecial.GetFeatureLayerByName(sLayerAliasName);
            if (featureLayer == null)
            {
                MessageBox.Show("未找到" + sLayerAliasName + "图层,无法绘制勘探线图元。");
                return;
            }

            //2.绘制图元
            if (lstProspectingBoreholePts.Count == 0) return;

            string prospectingLineID = prospectingLineEntity.BindingId;
            //绘制推断断层
            PointsFit2Polyline.CreateLine(featureLayer, lstProspectingBoreholePts, prospectingLineID);
        }

        #endregion

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
        /// 验证画面入力数据
        /// </summary>
        /// <returns>验证结果：true 通过验证, false未通过验证</returns>
        private bool check()
        {
            // 判断勘探线名称是否录入
            if (!Check.isEmpty(this.txtProspectingLineName, Const_GM.PROSPECTING_LINE_NAME))
            {
                return false;
            }

            // 勘探线名称特殊字符判断
            if (!Check.checkSpecialCharacters(this.txtProspectingLineName, Const_GM.PROSPECTING_LINE_NAME))
            {
                return false;
            }

            //// 判断勘探线名称是否存在
            //if (!Check.isExist(this.txtProspectingLineName, Const_GM.PROSPECTING_LINE_NAME, 
            //    ProspectingLineBLL.isProspectingLineNameExist(this.txtProspectingLineName.Text.Trim())))
            //{
            //    return false;
            //}

            // 只有当添加新勘探线信息的时候才去判断勘探线名称是否重复
            if (this._bllType == "add")
            {
                // 判断孔号是否存在
                if (!Check.isExist(this.txtProspectingLineName, Const_GM.PROSPECTING_LINE_NAME,
                    ProspectingLineBLL.isProspectingLineNameExist(this.txtProspectingLineName.Text.Trim())))
                {
                    return false;
                }
            }
            else
            {
                /* 修改的时候，首先要获取UI输入的钻孔名称到DB中去检索，
                如果检索件数 > 0 并且该断层ID还不是传过来的主键，那么视为输入了已存在的钻孔名称 */
                int boreholeId = -1;
                ProspectingLineBLL.selectProspectingLineIdByProspectingLineName(this.txtProspectingLineName.Text.ToString().Trim(), out boreholeId);
                if (boreholeId != -1 && !Convert.ToString(boreholeId).Equals(_iPK.ToString()))
                {
                    this.txtProspectingLineName.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const_GM.PROSPECTING_LINE_EXIST_MSG); // 勘探线名称已存在，请重新录入！
                    this.txtProspectingLineName.Focus();
                    return false;
                }

            }

            // 勘探线钻孔必须选择
            if (this.lstProspectingBoreholeSelected.Items.Count == 0)
            {
                Alert.alert(Const_GM.PROSPECTING_BOREHOLE_MUST_CHOOSE_MSG);
                this.lstProspectingBoreholeAll.Focus();
                return false;
            }

            // 验证通过
            return true;
        }

        /// <summary>
        /// 实现点击鼠标右键，将点击处的Item设为选中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstProspectingBoreholeSelected_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)//判断是否右键点击
            {
                System.Drawing.Point p = e.Location;//获取点击的位置

                int index = this.lstProspectingBoreholeSelected.IndexFromPoint(p);//根据位置获取右键点击项的索引

                this.lstProspectingBoreholeSelected.ClearSelected();

                this.lstProspectingBoreholeSelected.SelectedIndex = index;//设置该索引值对应的项为选定状态
            }
        }

        /// <summary>
        /// 上移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 上移ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 当前下标
            int iNowIndex = this.lstProspectingBoreholeSelected.SelectedIndex;

            if (iNowIndex == 0)
            {
                Alert.alert("无法上移");
                return;
            }

            string strTemp = this.lstProspectingBoreholeSelected.SelectedItem.ToString();

            this.lstProspectingBoreholeSelected.Items[iNowIndex] = this.lstProspectingBoreholeSelected.Items[iNowIndex - 1];

            this.lstProspectingBoreholeSelected.Items[iNowIndex - 1] = strTemp;

            this.lstProspectingBoreholeSelected.ClearSelected();

            this.lstProspectingBoreholeSelected.SelectedIndex = iNowIndex - 1; // 设置该索引值对应的项为选定状态
        }

        /// <summary>
        /// 下移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 下移ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 当前下标
            int iNowIndex = this.lstProspectingBoreholeSelected.SelectedIndex;

            if (iNowIndex == this.lstProspectingBoreholeSelected.Items.Count - 1)
            {
                Alert.alert("无法下移");
                return;
            }

            string strTemp = this.lstProspectingBoreholeSelected.SelectedItem.ToString();

            this.lstProspectingBoreholeSelected.Items[iNowIndex] = this.lstProspectingBoreholeSelected.Items[iNowIndex + 1];

            this.lstProspectingBoreholeSelected.Items[iNowIndex + 1] = strTemp;

            this.lstProspectingBoreholeSelected.ClearSelected();

            this.lstProspectingBoreholeSelected.SelectedIndex = iNowIndex + 1; // 设置该索引值对应的项为选定状态
        }
    }
}
