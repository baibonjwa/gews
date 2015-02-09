// ******************************************************************
// 概  述：停采线数据添加修改
// 作  者：宋英杰
// 创建日期：2014/3/11
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************

using System;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using GIS;
using GIS.Common;
using LibBusiness;
using LibCommon;
using LibEntity;

namespace sys2
{
    public partial class StopLineEntering : Form
    {
        #region ******变量声明******

        private readonly StopLine stopLineEntity = new StopLine();

        #endregion
        private StopLine _oldStopLineEntity; //更新前的停采线实体

        /// <summary>
        ///     构造方法
        /// </summary>
        public StopLineEntering()
        {
            InitializeComponent();
            //窗体属性设置
            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_MS.STOP_LINE_ADD);
        }

        /// <summary>
        ///     构造方法
        /// </summary>
        /// <param name="stopLineEntity">停采线实体</param>
        public StopLineEntering(StopLine stopLineEntity)
        {
            this.stopLineEntity = stopLineEntity;

            InitializeComponent();
            //绑定修改初始信息
            updateInfo();

            //窗体属性设置
            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_MS.STOP_LINE_CHANGE);
        }

        /// <summary>
        ///     修改初始信息绑定
        /// </summary>
        private void updateInfo()
        {
            //停采线名称
            txtStopLineName.Text = stopLineEntity.StopLineName;

            //起点坐标X
            txtSCoordinateX.Text = stopLineEntity.SCoordinateX.ToString();

            //起点坐标Y
            txtSCoordinateY.Text = stopLineEntity.SCoordinateY.ToString();

            //起点坐标Z
            txtSCoordinateZ.Text = stopLineEntity.SCoordinateZ.ToString();

            //终点坐标X
            txtFCoordinateX.Text = stopLineEntity.FCoordinateX.ToString();

            //终点坐标Y
            txtFCoordinateY.Text = stopLineEntity.FCoordinateY.ToString();

            //终点坐标Z
            txtFCoordinateZ.Text = stopLineEntity.FCoordinateZ.ToString();

            _oldStopLineEntity = stopLineEntity; //记录修改前的实体
        }

        private void btnSubmint_Click(object sender, EventArgs e)
        {
            // 验证
            if (!check())
            {
                //DialogResult = DialogResult.None;
                return;
            }
            //DialogResult = DialogResult.OK;

            //停采线名称
            stopLineEntity.StopLineName = txtStopLineName.Text;
            //起点坐标X
            stopLineEntity.SCoordinateX = Convert.ToDouble(txtSCoordinateX.Text);
            //起点坐标Y
            stopLineEntity.SCoordinateY = Convert.ToDouble(txtSCoordinateY.Text);
            //起点坐标Z
            stopLineEntity.SCoordinateZ = Convert.ToDouble(txtSCoordinateZ.Text);
            //终点坐标X
            stopLineEntity.FCoordinateX = Convert.ToDouble(txtFCoordinateX.Text);
            //终点坐标Y
            stopLineEntity.FCoordinateY = Convert.ToDouble(txtFCoordinateY.Text);
            //终点Z
            stopLineEntity.FCoordinateZ = Convert.ToDouble(txtFCoordinateZ.Text);

            //添加
            if (Text == Const_MS.STOP_LINE_ADD)
            {
                //BID
                stopLineEntity.BindingId = IDGenerator.NewBindingID();
                //添加
                addStopLineInfo();
            }
            //修改
            if (Text == Const_MS.STOP_LINE_CHANGE)
            {
                //修改
                UpdateStopLineInfo();
            }
        }

        /// <summary>
        ///     添加
        /// </summary>
        private void addStopLineInfo()
        {
            //添加操作
            stopLineEntity.Save();
            DrawStopLine(stopLineEntity); //在地图上绘制停采线
            Close();
        }

        /// <summary>
        ///     修改
        /// </summary>
        private void UpdateStopLineInfo()
        {
            //修改操作
            stopLineEntity.Save();
            UpdateStopLineOnMap(_oldStopLineEntity, stopLineEntity); //更新地图上的停采线 
            Close();
        }

        /// <summary>
        ///     取消按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            //窗体关闭
            Close();
        }

        /// <summary>
        ///     验证
        /// </summary>
        /// <returns></returns>
        private bool check()
        {
            //停采线名称非空验证
            if (!Check.isEmpty(txtStopLineName, Const_MS.STOP_LINE_NAME))
            {
                return false;
            }
            //停采线名称特殊字符验证
            if (!Check.checkSpecialCharacters(txtStopLineName, Const_MS.STOP_LINE_NAME))
            {
                return false;
            }
            //停采线名称重复
            if (txtStopLineName.Text != stopLineEntity.StopLineName)
            {
                if (StopLine.ExistsByStopLineName(txtStopLineName.Text))
                {
                    Alert.alert(Const_MS.STOP_LINE + Const.MSG_ALREADY_HAVE + Const.SIGN_EXCLAMATION_MARK);
                    return false;
                }
            }
            //起点坐标X数字验证
            if (!Check.IsNumeric(txtSCoordinateX, Const_MS.SCOORDINATE + Const_MS.X))
            {
                return false;
            }
            //起点坐标X非空验证
            if (!Check.isEmpty(txtSCoordinateX, Const_MS.SCOORDINATE + Const_MS.X))
            {
                return false;
            }
            //起点坐标Y数字验证
            if (!Check.IsNumeric(txtSCoordinateY, Const_MS.SCOORDINATE + Const_MS.Y))
            {
                return false;
            }
            //起点坐标Y非空验证
            if (!Check.isEmpty(txtSCoordinateY, Const_MS.SCOORDINATE + Const_MS.Y))
            {
                return false;
            }
            //起点坐标Z数字验证
            if (!Check.IsNumeric(txtSCoordinateZ, Const_MS.SCOORDINATE + Const_MS.Z))
            {
                return false;
            }
            //起点坐标Z非空验证
            if (!Check.isEmpty(txtSCoordinateZ, Const_MS.SCOORDINATE + Const_MS.Z))
            {
                return false;
            }
            //终点坐标X数字验证
            if (!Check.IsNumeric(txtFCoordinateX, Const_MS.FCOORDINATE + Const_MS.X))
            {
                return false;
            }
            //终点坐标X非空验证
            if (!Check.isEmpty(txtFCoordinateX, Const_MS.FCOORDINATE + Const_MS.X))
            {
                return false;
            }
            //终点坐标Y数字验证
            if (!Check.IsNumeric(txtFCoordinateY, Const_MS.FCOORDINATE + Const_MS.Y))
            {
                return false;
            }
            //终点坐标Y非空验证
            if (!Check.isEmpty(txtFCoordinateY, Const_MS.FCOORDINATE + Const_MS.Y))
            {
                return false;
            }
            //终点坐标Z数字验证
            if (!Check.IsNumeric(txtFCoordinateZ, Const_MS.FCOORDINATE + Const_MS.Z))
            {
                return false;
            }
            //终点坐标Z非空验证
            if (!Check.isEmpty(txtFCoordinateZ, Const_MS.FCOORDINATE + Const_MS.Z))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        ///     获取停采线图层
        /// </summary>
        /// <returns>矢量图层</returns>
        private IFeatureLayer GetStopLineFeatureLayer()
        {
            //找到图层
            IMap map = DataEditCommon.g_pMap;
            string layerName = LayerNames.STOP_LINE; //“停采线”图层
            var drawSpecialCom = new DrawSpecialCommon();
            IFeatureLayer featureLayer = drawSpecialCom.GetFeatureLayerByName(layerName);

            //ILayer layer = GIS.Common.DataEditCommon.GetLayerByName(map, layerName);///获得图层IFeatureLayer featureLayer 
            if (featureLayer == null)
            {
                MessageBox.Show("没有找到" + layerName + "图层，将不能绘制停采线。", "提示", MessageBoxButtons.OK);
                return null;
            }

            //IFeatureLayer featureLayer = layer as IFeatureLayer;
            return featureLayer;
        }

        /// <summary>
        ///     在地图上绘制一条停采线
        /// </summary>
        /// <param name="stopLineEntity">掘进的起点和终点</param>
        private bool DrawStopLine(StopLine stopLineEntity)
        {
            if (stopLineEntity == null)
                return false;

            IFeatureLayer featureLayer = GetStopLineFeatureLayer();
            if (featureLayer == null)
                return false;

            IPoint startPoint = new PointClass();
            startPoint.PutCoords(stopLineEntity.SCoordinateX, stopLineEntity.SCoordinateY);
            startPoint.Z = stopLineEntity.SCoordinateZ;

            IPoint endPoint = new PointClass();
            endPoint.PutCoords(stopLineEntity.FCoordinateX, stopLineEntity.FCoordinateY);
            endPoint.Z = stopLineEntity.FCoordinateZ;

            IPolyline polyline = new PolylineClass();
            polyline.FromPoint = startPoint;
            polyline.ToPoint = endPoint;

            bool bSuccess = GIS.SpecialGraphic.DrawStopLine.CreateLineFeature(featureLayer, polyline, stopLineEntity);
            return bSuccess;
        }

        /// <summary>
        ///     更新地图上的停采线
        /// </summary>
        /// <param name="oldEntity">修改前的停采线实体</param>
        /// <param name="newEntity">修改后的停采线实体</param>
        /// <returns>成功返回true</returns>
        private bool UpdateStopLineOnMap(StopLine oldEntity, StopLine newEntity)
        {
            IFeatureLayer featureLayer = GetStopLineFeatureLayer();
            if (featureLayer == null)
                return false;

            //先删除原有的线要素
            GIS.SpecialGraphic.DrawStopLine.DeleteLineFeature(featureLayer, oldEntity.BindingId);

            //重新绘制一条新的停采线
            bool bSuccess = DrawStopLine(newEntity);
            return bSuccess;
        }

        private void btnQD_Click(object sender, EventArgs e)
        {
            DataEditCommon.PickUpPoint(txtSCoordinateX, txtSCoordinateY);
        }

        private void btnZD_Click(object sender, EventArgs e)
        {
            DataEditCommon.PickUpPoint(txtFCoordinateX, txtFCoordinateY);
        }
    }
}