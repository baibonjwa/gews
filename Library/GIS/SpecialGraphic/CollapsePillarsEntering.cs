using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using GIS.Common;
using LibBusiness;
using LibCommon;
using LibCommonControl;
using LibEntity;
using LibSocket;
using Path = System.IO.Path;

namespace GIS
{
    public partial class CollapsePillarsEntering : Form
    {
        //陷落柱实体
        public static ClientSocket _clientSocket = null;
        private readonly CollapsePillarsEnt collapsePillarsEnt = new CollapsePillarsEnt();
        private readonly MainFrm mainFrm;
        private CollapsePillarsKeyPointEnt[] _dsCollapsePillarsPoint;
        private int _itemCount;
        private int _rowIndex = -1;
        private int _rowsCount;
        private string collapsePillarsName = "";
        //客户端

        /// <summary>
        ///     构造方法
        /// </summary>
        public CollapsePillarsEntering()
        {
            InitializeComponent();
            mainFrm = mainFrm;
            //设置窗体属性
            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_GM.COLLAPSEPILLARE_ADD);
        }

        /// <summary>
        ///     构造方法
        /// </summary>
        public CollapsePillarsEntering(IPointCollection pointCollection)
        {
            InitializeComponent();

            dgrdvCoordinate.RowCount = pointCollection.PointCount;
            for (int i = 0; i < pointCollection.PointCount - 1; i++)
            {
                dgrdvCoordinate[0, i].Value = pointCollection.get_Point(i).X;
                dgrdvCoordinate[1, i].Value = pointCollection.get_Point(i).Y;
                if (pointCollection.get_Point(i).Z.ToString() == "非数字" ||
                    pointCollection.get_Point(i).Z.ToString() == "NaN")
                    dgrdvCoordinate[2, i].Value = 0;
                else
                    dgrdvCoordinate[2, i].Value = pointCollection.get_Point(i).Z;
            }
            //设置窗体属性
            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_GM.COLLAPSEPILLARE_ADD);
        }

        /// <summary>
        ///     构造方法
        /// </summary>
        /// <param name="collapsePillarsEnt">陷落柱实体</param>
        public CollapsePillarsEntering(CollapsePillarsEnt collapsePillarsEnt)
        {
            this.collapsePillarsEnt = collapsePillarsEnt;

            collapsePillarsName = collapsePillarsEnt.CollapsePillarsName;

            InitializeComponent();

            //绑定修改数据
            changeInfo();

            //设置窗体属性
            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_GM.COLLAPSEPILLARE_CHANGE);
        }

        /// <summary>
        ///     绑定修改数据
        /// </summary>
        private void changeInfo()
        {
            _itemCount = 0;

            txtCollapsePillarsName.Text = collapsePillarsEnt.CollapsePillarsName;
            if (collapsePillarsEnt.Xtype == "1")
                radioBtnS.Checked = true;
            _dsCollapsePillarsPoint = CollapsePillarsKeyPointEnt.FindAllByCollapsePillarsId(collapsePillarsEnt.Id);

            _rowsCount = _dsCollapsePillarsPoint.Length;
            dgrdvCoordinate.RowCount = _rowsCount + 1;
            for (int i = 0; i < _rowsCount; i++)
            {
                dgrdvCoordinate[0, i].Value = _dsCollapsePillarsPoint[i].CoordinateX;
                dgrdvCoordinate[1, i].Value = _dsCollapsePillarsPoint[i].CoordinateY;
                dgrdvCoordinate[2, i].Value = _dsCollapsePillarsPoint[i].CoordinateZ;
                _itemCount++;
            }
            txtDescribe.Text = collapsePillarsEnt.Discribe;
        }

        /// <summary>
        ///     取消按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            //关闭窗体
            Close();
        }

        /// <summary>
        ///     提交按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            //去除无用空行
            for (int i = 0; i < dgrdvCoordinate.RowCount - 1; i++)
            {
                if (dgrdvCoordinate.Rows[i].Cells[0].Value == null &&
                    dgrdvCoordinate.Rows[i].Cells[1].Value == null &&
                    dgrdvCoordinate.Rows[i].Cells[2].Value == null)
                {
                    dgrdvCoordinate.Rows.RemoveAt(i);
                }
            }
            //实体赋值
            collapsePillarsEnt.CollapsePillarsName = txtCollapsePillarsName.Text;
            collapsePillarsEnt.Discribe = txtDescribe.Text;
            if (radioBtnX.Checked)
                collapsePillarsEnt.Xtype = "0";
            else
                collapsePillarsEnt.Xtype = "1";
            //验证
            if (!check())
            {
                DialogResult = DialogResult.None;
                return;
            }

            DialogResult = DialogResult.OK;

            bool bResult = false;


            if (Text == Const_GM.COLLAPSEPILLARE_ADD)
            {
                //添加陷落柱
                collapsePillarsEnt.Save();
            }

            //20140509 lyf
            //存储陷落柱关键点实体
            //CollapsePillarsEnt collapsePillarsEnt
            var lstCollapsePillarsEntKeyPts = new List<CollapsePillarsEnt>();
            var collapse = new CollapsePillarsEnt();
            //根据陷落柱名称获得陷落柱ID（作为陷落柱绑定ID）
            string sCollapseID = collapsePillarsEnt.Id.ToString(CultureInfo.InvariantCulture);

            //添加关键点
            for (int i = 0; i < dgrdvCoordinate.RowCount - 1; i++)
            {
                collapse = new CollapsePillarsEnt();
                if (Text == Const_GM.COLLAPSEPILLARE_CHANGE)
                {
                    if (i < _dsCollapsePillarsPoint.Length)
                    {
                        //关键点ID
                        collapse.PointId = _dsCollapsePillarsPoint[i].PointId;
                    }
                }
                //X
                collapse.CoordinateX = Convert.ToDouble(dgrdvCoordinate[0, i].Value);
                //Y
                collapse.CoordinateY = Convert.ToDouble(dgrdvCoordinate[1, i].Value);
                //Z
                collapse.CoordinateZ = Convert.ToDouble(dgrdvCoordinate[2, i].Value);

                if (Text == Const_GM.COLLAPSEPILLARE_ADD)
                {
                    collapse.BindingId = IDGenerator.NewBindingID();

                    //collapse.WirePointName =
                    //    Convert.ToInt32(
                    //        CollapsePillarsBLL.selectMaxCollapsePillars().Tables[0].Rows[0][
                    //            CollapsePillarsInfoDbConstNames.ID].ToString());
                    collapse.Save();
                }
                //如果添加未成功，删除添加的关键点与陷落柱
                if (Text == Const_GM.COLLAPSEPILLARE_ADD)
                {
                    collapse.Delete();
                }
                //修改
                if (Text == Const_GM.COLLAPSEPILLARE_CHANGE)
                {
                    if (i < CollapsePillarsKeyPointEnt.FindAllByCollapsePillarsId(collapse.Id).Length)
                    {
                        collapse.Save();
                    }
                    else
                    {
                        collapse.BindingId = IDGenerator.NewBindingID();
                        collapse.Save();
                        collapse.BindingId = null;
                    }
                }

                ///20140509 lyf
                if (!lstCollapsePillarsEntKeyPts.Contains(collapse))
                {
                    lstCollapsePillarsEntKeyPts.Add(collapse);
                }
            }
            if (dgrdvCoordinate.Rows.Count <= _itemCount)
            {
                for (int i = dgrdvCoordinate.Rows.Count - 1; i < _itemCount; i++)
                {
                    collapse.PointId = _dsCollapsePillarsPoint[i].PointId;
                    if (dgrdvCoordinate.Rows.Count == 1)
                    {
                        collapse.Delete();
                    }
                    else
                    {
                        CollapsePillarsKeyPointEnt.DeleteAllByCollapsePillarsId(collapse.Id);
                    }
                }
            }
            //TODO:提交成功后事件

            ///20140510 lyf
            //若为新增
            if (Text == Const_GM.COLLAPSEPILLARE_ADD)
            {
                DrawXLZ(lstCollapsePillarsEntKeyPts, sCollapseID);
            }
            //若为修改
            if (Text == Const_GM.COLLAPSEPILLARE_CHANGE)
            {
                ModifyXLZ(lstCollapsePillarsEntKeyPts, sCollapseID);
            }

            SendMessengToServer();
        }

        /// <summary>
        ///     显示行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgrdvCoordinate_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var rectangle = new Rectangle(e.RowBounds.Location.X,
                e.RowBounds.Location.Y, dgrdvCoordinate.RowHeadersWidth - 4, e.RowBounds.Height);

            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                dgrdvCoordinate.RowHeadersDefaultCellStyle.Font, rectangle,
                dgrdvCoordinate.RowHeadersDefaultCellStyle.ForeColor,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        /// <summary>
        ///     验证
        /// </summary>
        /// <returns></returns>
        private bool check()
        {
            //陷落柱非空
            if (!Check.isEmpty(txtCollapsePillarsName, Const_GM.COLLAPSEPILLARE_NAME))
            {
                return false;
            }
            //陷落柱名称特殊字符
            if (!Check.checkSpecialCharacters(txtCollapsePillarsName, Const_GM.COLLAPSEPILLARE_NAME))
            {
                return false;
            }

            //datagridview内部
            for (int i = 0; i < dgrdvCoordinate.RowCount - 1; i++)
            {
                //坐标X
                var cell = dgrdvCoordinate.Rows[i].Cells[0] as DataGridViewTextBoxCell;
                //非空
                if (cell.Value == null)
                {
                    Alert.alert(Const_GM.COORDINATE_X + Const.MSG_NOT_NULL + Const.SIGN_EXCLAMATION_MARK);
                    return false;
                }
                //数字
                if (!Validator.IsNumeric(cell.Value.ToString()))
                {
                    Alert.alert(Const_GM.COORDINATE_X + Const.MSG_MUST_NUMBER + Const.SIGN_EXCLAMATION_MARK);
                    return false;
                }
                //坐标Y
                cell = dgrdvCoordinate.Rows[i].Cells[1] as DataGridViewTextBoxCell;
                //非空
                if (cell.Value == null)
                {
                    Alert.alert(Const_GM.COORDINATE_Y + Const.MSG_NOT_NULL + Const.SIGN_EXCLAMATION_MARK);
                    return false;
                }
                //数字
                if (!Validator.IsNumeric(cell.Value.ToString()))
                {
                    Alert.alert(Const_GM.COORDINATE_Y + Const.MSG_MUST_NUMBER + Const.SIGN_EXCLAMATION_MARK);
                    return false;
                }
                //坐标Z
                cell = dgrdvCoordinate.Rows[i].Cells[2] as DataGridViewTextBoxCell;
                //非空
                if (cell.Value == null)
                {
                    Alert.alert(Const_GM.COORDINATE_Z + Const.MSG_NOT_NULL + Const.SIGN_EXCLAMATION_MARK);
                    return false;
                }
                //数字
                if (!Validator.IsNumeric(cell.Value.ToString()))
                {
                    Alert.alert(Const_GM.COORDINATE_Z + Const.MSG_MUST_NUMBER + Const.SIGN_EXCLAMATION_MARK);
                    return false;
                }
            }
            //关键点数>3
            if ((dgrdvCoordinate.RowCount <= 3))
            {
                Alert.alert(Const_GM.COLLAPSEPILLARE_MSG_MUST_MORE_THAN_THREE);
                return false;
            }
            //描述特殊字符
            if (!Check.checkSpecialCharacters(txtDescribe, Const_GM.COLLAPSEPILLARE_DISCRIBE))
            {
                return false;
            }
            ILayer m_pCurrentLayer = DataEditCommon.GetLayerByName(DataEditCommon.g_pMap,
                LayerNames.LAYER_ALIAS_MR_XianLuoZhu1);
            var featureLayer = m_pCurrentLayer as IFeatureLayer;
            if (featureLayer == null)
            {
                MessageBox.Show(@"陷落柱图层丢失！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DataEditCommon.g_pMyMapCtrl.CurrentTool = null;
                return false;
            }
            if (featureLayer.FeatureClass.ShapeType != esriGeometryType.esriGeometryPolygon)
            {
                MessageBox.Show(@"陷落柱图层丢失！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DataEditCommon.g_pMyMapCtrl.CurrentTool = null;
                return false;
            }
            //成功
            return true;
        }

        private void dgrdvCoordinate_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != dgrdvCoordinate.Rows.Count - 1 && e.ColumnIndex == 3 &&
                Alert.confirm(Const.DEL_CONFIRM_MSG))
            {
                if (e.ColumnIndex == 3)
                {
                    dgrdvCoordinate.Rows.RemoveAt(e.RowIndex);
                }
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                var open = new OpenFileDialog();
                open.Filter = "关键点文本(*.txt)|*.txt";
                if (open.ShowDialog(this) == DialogResult.Cancel)
                    return;
                string filename = open.FileName;
                string[] file = File.ReadAllLines(filename);
                dgrdvCoordinate.RowCount = file.Length;
                for (int i = 0; i < file.Length; i++)
                {
                    dgrdvCoordinate[0, i].Value = file[i].Split(',')[0];
                    dgrdvCoordinate[1, i].Value = file[i].Split(',')[1];
                    //dgrdvCoordinate[2, i].Value = file[i].Split(',')[2];
                    dgrdvCoordinate[2, i].Value = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SendMessengToServer()
        {
            Log.Debug("更新服务端断层Map------开始");
            // 通知服务端回采进尺已经添加
            var msg = new GeologyMsg(0, 0, CollapsePillarsEnt.TableName, DateTime.Now,
                COMMAND_ID.UPDATE_GEOLOG_DATA);
            var socket = new SocketHelper4gis();
            socket.GetClientSocketInstance().SendSocketMsg2Server(msg);
            Log.Debug("服务端断层Map------完成" + msg);
        }

        private void btnMirror_Click(object sender, EventArgs e)
        {
        }

        #region 根据关键点绘制陷落柱

        /// <summary>
        ///     修改陷落柱图元
        /// </summary>
        /// <param name="lstCollapsePillarsEntKeyPts"></param>
        /// <param name="sCollapseID"></param>
        private void ModifyXLZ(List<CollapsePillarsEnt> lstCollapsePillarsEntKeyPts, string sCollapseID)
        {
            //1.获得当前编辑图层
            var drawspecial = new DrawSpecialCommon();
            string sLayerAliasName = LayerNames.DEFALUT_COLLAPSE_PILLAR_1; //“默认_陷落柱_1”图层
            IFeatureLayer featureLayer = drawspecial.GetFeatureLayerByName(sLayerAliasName);
            if (featureLayer == null)
            {
                MessageBox.Show("未找到" + sLayerAliasName + "图层,无法修改陷落柱图元。");
                return;
            }

            //2.删除原来图元，重新绘制新图元
            bool bIsDeleteOldFeature = DataEditCommon.DeleteFeatureByBId(featureLayer, sCollapseID);
            if (bIsDeleteOldFeature)
            {
                //绘制图元
                DrawXLZ(lstCollapsePillarsEntKeyPts, sCollapseID);
            }
        }

        /// <summary>
        ///     将关键点坐标写入txt文件中
        /// </summary>
        /// <param name="lstCollapsePillarsEntKeyPts"></param>
        /// <param name="txtPath"></param>
        /// <returns></returns>
        private bool WritePtsInfo2Txt(List<CollapsePillarsEnt> lstCollapsePillarsEntKeyPts, string txtPath)
        {
            if (lstCollapsePillarsEntKeyPts.Count == 0) return false;

            try
            {
                //zwy 20140527 add  先判断子目录是否存在，不存在需要创建
                string folder = Path.GetDirectoryName(txtPath);
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                ////创建关键点坐标临时文件
                //if (!File.Exists(txtPath))
                //{
                //    File.CreateText(txtPath);
                //}

                var fs = new FileStream(txtPath, FileMode.OpenOrCreate, FileAccess.Write); //zwy modify 0527
                var sw = new StreamWriter(fs);

                var collapsePillarsKeyPt = new CollapsePillarsEnt();
                for (int i = 0; i < lstCollapsePillarsEntKeyPts.Count; i++)
                {
                    collapsePillarsKeyPt = lstCollapsePillarsEntKeyPts[i];

                    //将关键点坐标写入到临时文件中
                    string strContent = "";
                    strContent = collapsePillarsKeyPt.CoordinateX + ","
                                 + collapsePillarsKeyPt.CoordinateY + ","
                                 + collapsePillarsKeyPt.CoordinateZ;

                    //"txtwriter.txt"
                    //StreamWriter sw = new StreamWriter(txtPath, false);//"txtwriter.txt"
                    sw.WriteLine(strContent);
                    //sw.Close();
                }

                sw.Flush();
                sw.Close();

                return true;
            }
            catch (Exception ex)
            {
                Trace.WriteLine("陷落柱关键点写入txt报错：" + ex.Message);
                return false;
            }
        }

        private void DrawXLZ(List<CollapsePillarsEnt> lstCollapsePillarsEntKeyPts, string sCollapseID)
        {
            ILayer m_pCurrentLayer = DataEditCommon.GetLayerByName(DataEditCommon.g_pMap,
                LayerNames.LAYER_ALIAS_MR_XianLuoZhu1);
            var pFeatureLayer = m_pCurrentLayer as IFeatureLayer;
            INewBezierCurveFeedback pBezier = new NewBezierCurveFeedbackClass();
            IGeometry geo = null;
            IPoint pt = new PointClass();
            IPolyline polyline = new PolylineClass();
            for (int i = 0; i < lstCollapsePillarsEntKeyPts.Count; i++)
            {
                pt = new PointClass();
                var mZAware = (IZAware)pt;
                mZAware.ZAware = true;

                pt.X = lstCollapsePillarsEntKeyPts[i].CoordinateX;
                pt.Y = lstCollapsePillarsEntKeyPts[i].CoordinateY;
                pt.Z = lstCollapsePillarsEntKeyPts[i].CoordinateZ;
                if (i == 0)
                {
                    pBezier.Start(pt);
                }
                else if (i == lstCollapsePillarsEntKeyPts.Count - 1)
                {
                    pBezier.AddPoint(pt);
                    pt = new PointClass();
                    var zZAware = (IZAware)pt;
                    zZAware.ZAware = true;
                    pt.X = lstCollapsePillarsEntKeyPts[0].CoordinateX;
                    pt.Y = lstCollapsePillarsEntKeyPts[0].CoordinateY;
                    pt.Z = lstCollapsePillarsEntKeyPts[0].CoordinateZ;
                    pBezier.AddPoint(pt);
                    polyline = pBezier.Stop();
                }
                else
                    pBezier.AddPoint(pt);
            }
            //polyline = (IPolyline)geo;
            var pSegmentCollection = polyline as ISegmentCollection;
            for (int i = 0; i < pSegmentCollection.SegmentCount; i++)
            {
                pt = new PointClass();
                var mZAware = (IZAware)pt;
                mZAware.ZAware = true;

                pt.X = lstCollapsePillarsEntKeyPts[i].CoordinateX;
                pt.Y = lstCollapsePillarsEntKeyPts[i].CoordinateY;
                pt.Z = lstCollapsePillarsEntKeyPts[i].CoordinateZ;


                IPoint pt1 = new PointClass();
                mZAware = (IZAware)pt1;
                mZAware.ZAware = true;
                if (i == pSegmentCollection.SegmentCount - 1)
                {
                    pt1.X = lstCollapsePillarsEntKeyPts[0].CoordinateX;
                    pt1.Y = lstCollapsePillarsEntKeyPts[0].CoordinateY;
                    pt1.Z = lstCollapsePillarsEntKeyPts[0].CoordinateZ;

                    pSegmentCollection.get_Segment(i).FromPoint = pt;
                    pSegmentCollection.get_Segment(i).ToPoint = pt1;
                }
                else
                {
                    pt1.X = lstCollapsePillarsEntKeyPts[i + 1].CoordinateX;
                    pt1.Y = lstCollapsePillarsEntKeyPts[i + 1].CoordinateY;
                    pt1.Z = lstCollapsePillarsEntKeyPts[i + 1].CoordinateZ;

                    pSegmentCollection.get_Segment(i).FromPoint = pt;
                    pSegmentCollection.get_Segment(i).ToPoint = pt1;
                }
            }
            polyline = pSegmentCollection as IPolyline;
            //polyline = DataEditCommon.PDFX(polyline, "Bezier");

            IPolygon pPolygon = DataEditCommon.PolylineToPolygon(polyline);
            var list = new List<ziduan>();
            list.Add(new ziduan("COLLAPSE_PILLAR_NAME", txtCollapsePillarsName.Text));
            list.Add(new ziduan("BID", sCollapseID));
            if (radioBtnX.Checked)
                list.Add(new ziduan("XTYPE", "0"));
            else
                list.Add(new ziduan("BID", "1"));
            IFeature pFeature = DataEditCommon.CreateNewFeature(pFeatureLayer, pPolygon, list);
            if (pFeature != null)
            {
                MyMapHelp.Jump(pFeature.Shape);
                DataEditCommon.g_pMyMapCtrl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewBackground, null, null);
            }

            #region 暂时无用

            //string sTempFolderPath = System.Windows.Forms.Application.StartupPath + "\\TempFolder";

            /////1.将关键点坐标存储到临时文件中
            //string sPtsCoordinateTxtPath = sTempFolderPath + "\\PtsCoordinate.txt";
            //bool bIsWrite = WritePtsInfo2Txt(lstCollapsePillarsEntKeyPts, sPtsCoordinateTxtPath);
            //if (!bIsWrite) return;

            /////2.读取点坐标文件拟合生成陷落柱，仿照等值线
            /////步骤：点文件生成点要素层→转为Raster→提取等值线
            //Geoprocessor GP = new Geoprocessor();
            //string featureOut = sTempFolderPath + "\\KeyPts.shp";
            //DrawContours.ConvertASCIIDescretePoint2FeatureClass(GP, sPtsCoordinateTxtPath, featureOut);//点文件生成点要素层

            //string sRasterOut = sTempFolderPath + "\\Raster";
            //DrawContours.ConvertFeatureCls2Raster(GP, featureOut, sRasterOut);//要素层→Raster

            //string sR2Contour = sTempFolderPath + "\\Contour.shp";
            //double douElevation = 0.5;//等高距0.5
            //DrawContours.SplineRasterToContour(GP, sRasterOut, sR2Contour, douElevation);//提取等值线（即为拟合的陷落柱）

            /////3.复制生成的等值线（即为拟合的陷落柱）要素到陷落柱图层
            /////3.1 获得源图层
            //IFeatureLayer sourceFeaLayer = new FeatureLayerClass();
            //string sourcefeatureClassName = "Contour.shp";
            //IFeatureClass featureClass =PointsFit2Polyline.GetFeatureClassFromShapefileOnDisk(sTempFolderPath, sourcefeatureClassName);//获得等值线（即为拟合的陷落柱）图层

            //if (featureClass == null) return;
            //sourceFeaLayer.FeatureClass = featureClass;


            /////3.2 获得当前编辑图层(目标图层)
            //DrawSpecialCommon drawspecial = new DrawSpecialCommon();
            //string sLayerAliasName = LibCommon.LibLayerNames.DEFALUT_COLLAPSE_PILLAR;//“默认_陷落柱_1”图层
            //IFeatureLayer featureLayer = drawspecial.GetFeatureLayerByName(sLayerAliasName);
            //if (featureLayer == null)
            //{
            //    MessageBox.Show("未找到" + sLayerAliasName + "图层,无法绘制陷落柱图元。");
            //    return;
            //}

            /////3.3 复制要素
            //PointsFit2Polyline.CopyFeature(sourceFeaLayer, featureLayer, sCollapseID);

            #endregion
        }

        #endregion
    }
}