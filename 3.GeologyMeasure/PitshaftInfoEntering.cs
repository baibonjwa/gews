// ******************************************************************
// 概  述：井筒数据录入界面
// 作  者：伍鑫
// 创建日期：2013/03/05
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
using LibEntity;
using LibBusiness;
using LibCommon;
using GIS;
using GIS.Common;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;

namespace _3.GeologyMeasure
{
    public partial class PitshaftInfoEntering : Form
    {
        private Rectangle _PropertyName;
        /** 主键  **/
        private int _iPK;
        /** 业务逻辑类型：添加/修改  **/
        private string _bllType = "add";
        PitshaftInfoManagement frm;
        /// <summary>
        /// 构造方法
        /// </summary>
        public PitshaftInfoEntering(PitshaftInfoManagement form)
        {
            frm = form;
            InitializeComponent();
            // 设置窗体默认属性
            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_GM.INSERT_PITSHAFT_INFO);
            // 加载井筒类型信息
            loadPitshaftTypeInfo();
        }

        /// <summary>
        /// 带参数的构造方法
        /// </summary>
        /// <param name="strPrimaryKey">主键</param>
        public PitshaftInfoEntering(string strPrimaryKey, string strTitle,PitshaftInfoManagement form)
        {
            frm = form;
            InitializeComponent();
            // 设置业务类型
            this._bllType = "update";
            // 主键
            this._iPK = Convert.ToInt32(strPrimaryKey);
            // 设置窗体默认属性
            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, strTitle);
            // 加载井筒类型信息
            loadPitshaftTypeInfo();
            // 设置井筒信息
            this.setPitshaftTypeInfo();
        }

        /// <summary>
        /// 加载井筒类型信息
        /// </summary>
        private void loadPitshaftTypeInfo()
        {
            DataSet ds = PitshaftTypeBLL.selectAllPitshaftTypeInfo();

            if (ds.Tables[0].Rows.Count > 0)
            {
                this.cobPitshaftType.DataSource    = ds.Tables[0];
                this.cobPitshaftType.DisplayMember = PitshaftTypeDbConstNames.PITSHAFT_TYPE_NAME;
                this.cobPitshaftType.ValueMember   = PitshaftTypeDbConstNames.PITSHAFT_TYPE_ID;
                this.cobPitshaftType.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// 提  交
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

            // 创建井筒实体
            Pitshaft pitshaftEntity = new Pitshaft();
            // 井筒名称
            pitshaftEntity.PitshaftName = this.txtPitshaftName.Text.Trim();
            // 井筒类型
            int iPitshaftTypeId = 0;
            if (int.TryParse(Convert.ToString(this.cobPitshaftType.SelectedValue), out iPitshaftTypeId))
            {
                pitshaftEntity.PitshaftTypeId = iPitshaftTypeId;
            }
            // 井口标高
            double dWellheadElevation = 0;
            if (double.TryParse(this.txtWellheadElevation.Text.Trim(), out dWellheadElevation))
            {
                pitshaftEntity.WellheadElevation = dWellheadElevation;
            }
            // 井底标高
            double dWellbottomElevation = 0;
            if (double.TryParse(this.txtWellbottomElevation.Text.Trim(), out dWellbottomElevation))
            {
                pitshaftEntity.WellbottomElevation = dWellbottomElevation;
            }
            // 井筒坐标X
            double dPitshaftCoordinateX = 0;
            if (double.TryParse(this.txtPitshaftCoordinateX.Text.Trim(), out dPitshaftCoordinateX))
            {
                pitshaftEntity.PitshaftCoordinateX = Math.Round(dPitshaftCoordinateX,3);
            }
            // 井筒坐标Y
            double dPitshaftCoordinateY = 0;
            if (double.TryParse(this.txtPitshaftCoordinateY.Text.Trim(), out dPitshaftCoordinateY))
            {
                pitshaftEntity.PitshaftCoordinateY = Math.Round(dPitshaftCoordinateY,3);
            }
            // 图形坐标X
            double dFigureCoordinateX = 0;
            if (double.TryParse(this.txtFigureCoordinateX.Text.Trim(), out dFigureCoordinateX))
            {
                pitshaftEntity.FigureCoordinateX = Math.Round(dFigureCoordinateX,3);
            }
            // 图形坐标Y
            double dFigureCoordinateY = 0;
            if (double.TryParse(this.txtFigureCoordinateY.Text.Trim(), out dFigureCoordinateY))
            {
                pitshaftEntity.FigureCoordinateY = Math.Round(dFigureCoordinateY,3);
            }
            // 图形坐标Z
            double dFigureCoordinateZ = 0;
            if (double.TryParse(this.txtFigureCoordinateZ.Text.Trim(), out dFigureCoordinateZ))
            {
                pitshaftEntity.FigureCoordinateZ = dFigureCoordinateZ;
            }
  
            bool bResult = false;
            if (this._bllType == "add")
            {
                // BID
                pitshaftEntity.BindingId = IDGenerator.NewBindingID();

                //井筒信息插入
                bResult = PitshaftBLL.insertPitshaftInfo(pitshaftEntity);
                
                //绘制井筒
                if (bResult)
                {
                    frm.refreshAdd();
                    DrawJingTong(pitshaftEntity);
                }
            }
            else
            {
                // 主键
                pitshaftEntity.PitshaftId = this._iPK;
                // 井筒信息修改
                bResult = PitshaftBLL.updatePitshaftInfo(pitshaftEntity);


                //20140428 lyf 
                //获取井筒BID，为后面修改绘制井筒赋值所用
                if (bResult)
                {
                    frm.refreshUpdate();
                    string sBID = "";
                    sBID = PitshaftBLL.selectPitshaftInfoBIDByPitshaftName(pitshaftEntity.PitshaftName);
                    pitshaftEntity.BindingId = sBID;
                    //修改图元
                    ModifyJingTong(pitshaftEntity);
                }
            }

            // 添加/修改成功的场合
            if (bResult)
            {
                this.Close();
            }
        }

        #region 绘制井筒图元

        /// <summary>
        /// 绘制井筒图元
        /// </summary>
        /// <param name="pitshaftEntity"></param>
        private void DrawJingTong(Pitshaft pitshaftEntity)
        {
            ////1.获得当前编辑图层
            //DrawSpecialCommon drawspecial = new DrawSpecialCommon();
            //string sLayerAliasName = LibCommon.LibLayerNames.DEFALUT_JINGTONG;//“默认_井筒”图层
            //IFeatureLayer featureLayer = drawspecial.GetFeatureLayerByName(sLayerAliasName);
            //if (featureLayer == null)
            //{
            //    MessageBox.Show("未找到" + sLayerAliasName + "图层,无法绘制井筒图元。");
            //    return;
            //}

            ////2.绘制井筒
            //double X = Convert.ToDouble(this.txtFigureCoordinateX.Text.ToString());
            //double Y = Convert.ToDouble(this.txtFigureCoordinateY.Text.ToString());
            //IPoint pt = new PointClass();
            //pt.X = X;
            //pt.Y = Y;

            //double dZ = 0;
            //if (!string.IsNullOrEmpty(this.txtFigureCoordinateZ.Text))
            //{
            //    double.TryParse(this.txtFigureCoordinateZ.Text, out dZ);
            //}
            //pt.Z = dZ;

            ////标注内容
            //string strH =(Convert.ToDouble(this.txtWellheadElevation.Text.ToString())+
            //    Convert.ToDouble(this.txtWellbottomElevation.Text.ToString())).ToString();//井口标高+井底标高
            //string strX = SplitStr(this.txtPitshaftCoordinateX.Text.ToString());
            //string strY = SplitStr(this.txtPitshaftCoordinateY.Text.ToString());
            ////string strName =this.cobPitshaftType.SelectedValue.ToString()+"："+
            ////    this.txtPitshaftName.Text.ToString();
            //string strName = this.cobPitshaftType.SelectedValue.ToString() + "：" +
            //   this.txtPitshaftName.Text.ToString();

            //GIS.SpecialGraphic.DrawJT drawJT = new GIS.SpecialGraphic.DrawJT(strX, strY, strH, strName);
            ////dfs
            //DataEditCommon.InitEditEnvironment();
            //DataEditCommon.CheckEditState();
            //DataEditCommon.g_CurWorkspaceEdit.StartEditOperation();
            ////dfe
            //IFeature feature = featureLayer.FeatureClass.CreateFeature();         
            //IGeometry geometry = pt;
            //DataEditCommon.ZMValue(feature, geometry);   //几何图形Z值处理
            ////drawspecial.ZMValue(feature, geometry);//几何图形Z值处理
            //feature.Shape = pt;
            ////要素ID字段赋值（对应属性表中BindingID）
            //int iFieldID = feature.Fields.FindField(GIS_Const.FIELD_BID);
            //feature.Value[iFieldID] = pitshaftEntity.BindingId.ToString();
            //feature.Store();
            //DataEditCommon.g_CurWorkspaceEdit.StopEditOperation();
            //string strValue = feature.get_Value(feature.Fields.FindField(GIS_Const.FIELD_OBJECTID)).ToString();
            //DataEditCommon.SpecialPointRenderer(featureLayer, GIS_Const.FIELD_OBJECTID, strValue, drawJT.m_Bitmap);
            
            /////3.显示井筒图层
            //if (featureLayer.Visible == false)
            //    featureLayer.Visible = true;

            ////缩放到新增的线要素，并高亮该要素
            //GIS.Common.DataEditCommon.g_pMyMapCtrl.ActiveView.Extent = feature.Shape.Envelope;
            //GIS.Common.DataEditCommon.g_pMyMapCtrl.ActiveView.Extent.Expand(2.5, 2.5, true);
            //GIS.Common.DataEditCommon.g_pMyMapCtrl.Map.SelectFeature(featureLayer, feature);
            //GIS.Common.DataEditCommon.g_pMyMapCtrl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewAll, null, null);

            ////DataEditCommon.g_pMyMapCtrl.ActiveView.Refresh();

            double X = Convert.ToDouble(this.txtFigureCoordinateX.Text.ToString());
            double Y = Convert.ToDouble(this.txtFigureCoordinateY.Text.ToString());
            IPoint pt = new PointClass();
            pt.X = X;
            pt.Y = Y;

            double dZ = 0;
            if (!string.IsNullOrEmpty(this.txtFigureCoordinateZ.Text))
            {
                double.TryParse(this.txtFigureCoordinateZ.Text, out dZ);
            }
            pt.Z = dZ;
            ILayer pLayer = DataEditCommon.GetLayerByName(DataEditCommon.g_pMap, GIS.LayerNames.DEFALUT_JINGTONG);
            if (pLayer == null)
            {
                MessageBox.Show("未找到井筒图层,无法绘制井筒图元。");
                return;
            }
            IFeatureLayer pFeatureLayer = (IFeatureLayer)pLayer;
            IGeometry geometry = pt;
            List<ziduan> list = new List<ziduan>();
            list.Add(new ziduan("bid", pitshaftEntity.BindingId));
            list.Add(new ziduan("mc", pitshaftEntity.PitshaftName));
            list.Add(new ziduan("addtime", DateTime.Now.ToString()));
            list.Add(new ziduan("jkgc", pitshaftEntity.WellheadElevation.ToString()));
            list.Add(new ziduan("jdgc", pitshaftEntity.WellbottomElevation.ToString()));
            list.Add(new ziduan("yt", cobPitshaftType.Text));
            list.Add(new ziduan("x", pitshaftEntity.PitshaftCoordinateX.ToString()));
            list.Add(new ziduan("y", pitshaftEntity.PitshaftCoordinateY.ToString()));
            list.Add(new ziduan("h", (pitshaftEntity.WellbottomElevation + pitshaftEntity.WellheadElevation).ToString()));

            IFeature pfeature = DataEditCommon.CreateNewFeature(pFeatureLayer, geometry, list);
            if (pfeature != null)
            {
                GIS.MyMapHelp.Jump(pt);
                DataEditCommon.g_pMyMapCtrl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography | esriViewDrawPhase.esriViewForeground, null, null);
            }
        }

        /// <summary>
        /// 修改井筒
        /// </summary>
        /// <param name="pitshaftEntity"></param>
        private void ModifyJingTong(Pitshaft pitshaftEntity)
        {
            //1.获得当前编辑图层
            DrawSpecialCommon drawspecial = new DrawSpecialCommon();
            string sLayerAliasName = GIS.LayerNames.DEFALUT_JINGTONG;//“默认_井筒”图层
            IFeatureLayer featureLayer = drawspecial.GetFeatureLayerByName(sLayerAliasName);
            if (featureLayer == null)
            {
                MessageBox.Show("未找到" + sLayerAliasName + "图层,无法修改井筒图元。");
                return;
            }

            //2.删除原来图元，重新绘制新图元
            bool bIsDeleteOldFeature = DataEditCommon.DeleteFeatureByWhereClause(featureLayer, "BID='"+pitshaftEntity.BindingId+"'");
            //if (bIsDeleteOldFeature)
            {
                //绘制井筒
                DrawJingTong(pitshaftEntity);
            }

        }
        /// <summary>
        /// 坐标长度处理
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private string SplitStr(string str)
        {
            string resultStr;

            if (!str.Contains('.')) return str;

            string[] strArr = str.Split('.');
            if (strArr[1].Length > 3)
            {
                resultStr = strArr[0] + "." + strArr[1].Substring(0, 3);
            }
            else
            {
                resultStr = str;
            }
            return resultStr;
        }

        #endregion

        /// <summary>
        /// 取  消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            // 关闭窗口
            this.Close();
        }

        /// <summary>
        /// 验证画面入力数据
        /// </summary>
        /// <returns>验证结果：true 通过验证, false未通过验证</returns>
        private bool check()
        {
            // 判断<井筒名称>是否录入
            if (!Check.isEmpty(this.txtPitshaftName, Const_GM.PITSHAFT_NAME))
            {
                return false;
            }

            // 判断<井筒名称>是否包含特殊字符
            if (!Check.checkSpecialCharacters(this.txtPitshaftName, Const_GM.PITSHAFT_NAME))
            {
                return false;
            }

            // 判断井筒名称是否重复
            if (this._bllType == "add")
            {
                // 判断井筒名称是否存在
                if (!Check.isExist(this.txtPitshaftName, Const_GM.PITSHAFT_NAME,
                    PitshaftBLL.isPitshaftNameExist(this.txtPitshaftName.Text.Trim())))
                {
                    return false;
                }
            }
            else
            {
                /* 修改的时候，首先要获取UI输入的名称到DB中去检索，
                如果检索件数 > 0 并且该断层ID还不是传过来的主键，那么视为输入了已存在的名称 */
                DataSet ds = PitshaftBLL.selectPitshaftInfoByPitshaftName(this.txtPitshaftName.Text.Trim());
                if (ds.Tables[0].Rows.Count > 0 && !ds.Tables[0].Rows[0][PitshaftDbConstNames.PITSHAFT_ID].ToString().Equals(_iPK.ToString()))
                {
                    this.txtPitshaftName.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const_GM.PITSHAFT_NAME_EXIST_MSG); // 井筒名称已存在，请重新录入！
                    this.txtPitshaftName.Focus();
                    return false;
                }

            }

            // 判断<井筒类型>是否选择
            if (!Check.isEmpty(this.cobPitshaftType, Const_GM.PITSHAFT_TYPE))
            {
                return false;
            }

            // 判断<井口标高>是否录入
            if (!Check.isEmpty(this.txtWellheadElevation, Const_GM.WELLHEAD_ELEVATION))
            {
                return false;
            }

            // 判断<井口标高>是否为数字
            if (!Check.IsNumeric(this.txtWellheadElevation, Const_GM.WELLHEAD_ELEVATION))
            {
                return false;
            }

            // 判断<井底标高>是否录入
            if (!Check.isEmpty(this.txtWellbottomElevation, Const_GM.WELLBOTTOM_ELEVATION))
            {
                return false;
            }

            // 判断<井底标高>是否为数字
            if (!Check.IsNumeric(this.txtWellbottomElevation, Const_GM.WELLBOTTOM_ELEVATION))
            {
                return false;
            }

            // 判断<井筒坐标X>是否录入
            if (!Check.isEmpty(this.txtPitshaftCoordinateX, Const_GM.PITSHAFT_COORDINATE_X))
            {
                return false;
            }

            // 判断<井筒坐标X>是否为数字
            if (!Check.IsNumeric(this.txtPitshaftCoordinateX, Const_GM.PITSHAFT_COORDINATE_X))
            {
                return false;
            }

            // 判断<井筒坐标Y>是否录入
            if (!Check.isEmpty(this.txtPitshaftCoordinateY, Const_GM.PITSHAFT_COORDINATE_Y))
            {
                return false;
            }

            // 判断<井筒坐标Y>是否为数字
            if (!Check.IsNumeric(this.txtPitshaftCoordinateY, Const_GM.PITSHAFT_COORDINATE_Y))
            {
                return false;
            }

            // 判断<图形坐标X>是否录入
            if (!Check.isEmpty(this.txtFigureCoordinateX, Const_GM.FIGURE_COORDINATE_X))
            {
                return false;
            }

            // 判断<图形坐标X>是否为数字
            if (!Check.IsNumeric(this.txtFigureCoordinateX, Const_GM.FIGURE_COORDINATE_X))
            {
                return false;
            }

            // 判断<图形坐标Y>是否录入
            if (!Check.isEmpty(this.txtFigureCoordinateY, Const_GM.FIGURE_COORDINATE_Y))
            {
                return false;
            }

            // 判断<图形坐标Y>是否为数字
            if (!Check.IsNumeric(this.txtFigureCoordinateY, Const_GM.FIGURE_COORDINATE_Y))
            {
                return false;
            }

            // TODO:图形坐标Z暂时设为非必须录入，暂时保留
            //// 判断<图形坐标Z>是否录入
            //if (!Check.isEmpty(this.txtFigureCoordinateZ, Const_GM.FIGURE_COORDINATE_Z))
            //{
            //    return false;
            //}

            // 判断<图形坐标Z>是否为数字
            if (!Check.IsNumeric(this.txtFigureCoordinateZ, Const_GM.FIGURE_COORDINATE_Z))
            {
                return false;
            }

            //****************************************************

            // 验证通过
            return true;
        }

        /// <summary>
        /// 设置井筒信息
        /// </summary>
        private void setPitshaftTypeInfo()
        {
            // 通过主键获取断层信息
            DataSet ds = PitshaftBLL.selectPitshaftInfoByPitshaftId(this._iPK);

            if (ds.Tables[0].Rows.Count > 0)
            {
                // 井筒名称
                this.txtPitshaftName.Text = ds.Tables[0].Rows[0][PitshaftDbConstNames.PITSHAFT_NAME].ToString();
                // 井筒类型
                this.cobPitshaftType.SelectedValue = ds.Tables[0].Rows[0][PitshaftDbConstNames.PITSHAFT_TYPE_ID].ToString();

                // 井口标高
                this.txtWellheadElevation.Text = ds.Tables[0].Rows[0][PitshaftDbConstNames.WELLHEAD_ELEVATION].ToString();
                // 井底标高
                this.txtWellbottomElevation.Text = ds.Tables[0].Rows[0][PitshaftDbConstNames.WELLBOTTOM_ELEVATION].ToString();
                // 井筒坐标X
                this.txtPitshaftCoordinateX.Text = ds.Tables[0].Rows[0][PitshaftDbConstNames.PITSHAFT_COORDINATE_X].ToString();
                // 井筒坐标Y
                this.txtPitshaftCoordinateY.Text = ds.Tables[0].Rows[0][PitshaftDbConstNames.PITSHAFT_COORDINATE_Y].ToString();
                // 图形坐标X
                this.txtFigureCoordinateX.Text = ds.Tables[0].Rows[0][PitshaftDbConstNames.FIGURE_COORDINATE_X].ToString();
                // 图形坐标Y
                this.txtFigureCoordinateY.Text = ds.Tables[0].Rows[0][PitshaftDbConstNames.FIGURE_COORDINATE_Y].ToString();
                // 图形坐标Z
                string strCoordinate_Z = ds.Tables[0].Rows[0][PitshaftDbConstNames.FIGURE_COORDINATE_Z].ToString();
                this.txtFigureCoordinateZ.Text = (strCoordinate_Z == Const.DOUBLE_DEFAULT_VALUE ? "" : strCoordinate_Z);
            }
        }

        private void btnQD_Click(object sender, EventArgs e)
        {
            GIS.Common.DataEditCommon.PickUpPoint(txtPitshaftCoordinateX, txtPitshaftCoordinateY);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GIS.Common.DataEditCommon.PickUpPoint(txtFigureCoordinateX, txtFigureCoordinateY);
        }
    }
}
