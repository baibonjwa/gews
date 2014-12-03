// ******************************************************************
// 概  述：揭露断层数据录入
// 作  者：伍鑫
// 创建日期：2013/11/27
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LibCommonControl;
using LibEntity;
using LibBusiness;
using LibCommon;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Display;
using GIS;
using GIS.Common;
using LibSocket;
using stdole;

namespace _3.GeologyMeasure
{
    public partial class FaultageInfoEntering : MainFrm
    {
        /** 主键  **/
        private int _iPK;
        /** 业务逻辑类型：添加/修改  **/
        private string _bllType = "add";
        double dLength = 0;//长度
        FaultageInfoManagement form;
        /// <summary>
        /// 构造方法
        /// </summary>
        public FaultageInfoEntering(FaultageInfoManagement frm)
        {
            InitializeComponent();

            // 设置窗体默认属性
            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_GM.INSERT_FAULTAGE_INFO);
            form = frm;
        }

        /// <summary>
        /// 带参数的构造方法
        /// </summary>
        /// <param name="strPrimaryKey">主键</param>
        public FaultageInfoEntering(string strPrimaryKey, FaultageInfoManagement frm)
        {
            InitializeComponent();
            form = frm;
            // 主键
            int iPK = 0;
            if (int.TryParse(strPrimaryKey, out iPK))
            {
                this._iPK = iPK;

                // 设置窗体默认属性
                FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_GM.UPDATE_FAULTAGE_INFO);

                // 设置业务类型
                this._bllType = "update";

                // 设置断层信息
                this.setFaultageInfo();
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

            // 创建断层实体
            FaultageEntity faultageEntity = new FaultageEntity();

            // 断层名称
            faultageEntity.FaultageName = this.txtFaultageName.Text.Trim();

            // 落差
            faultageEntity.Gap = txtGap.Text.Trim();

            // 倾角
            double dAngle = 0;
            if (double.TryParse(this.txtAngle.Text.Trim(), out dAngle))
            {
                faultageEntity.Angle = dAngle;
            }

            // 类型
            Control[] arrControl2 = this.pnlType.Controls.Find("rbtnFrontFaultage", true);
            RadioButton _rbtnFrontFaultage = arrControl2[0] as RadioButton;
            if (_rbtnFrontFaultage.Checked)
            {
                faultageEntity.Type = Const_GM.FRONT_FAULTAGE; // 正断层 
            }
            else
            {
                faultageEntity.Type = Const_GM.OPPOSITE_FAULTAGE; // 逆断层
            }

            // 走向
            faultageEntity.Trend = txtTrend.Text.Trim();

            // 断距
            faultageEntity.Separation = txtSeparation.Text.Trim();

            // 坐标X
            double dCoordinateX = 0;
            if (double.TryParse(this.txtCoordinateX.Text.Trim(), out dCoordinateX))
            {
                faultageEntity.CoordinateX = dCoordinateX;
            }

            // 坐标Y
            double dCoordinateY = 0;
            if (double.TryParse(this.txtCoordinateY.Text.Trim(), out dCoordinateY))
            {
                faultageEntity.CoordinateY = dCoordinateY;
            }

            // 坐标Z
            double dCoordinateZ = 0;
            if (double.TryParse(this.txtCoordinateZ.Text.Trim(), out dCoordinateZ))
            {
                faultageEntity.CoordinateZ = dCoordinateZ;
            }
            dLength = 0;
            if (double.TryParse(txtLength.Text.Trim(), out dLength))
            {

            }
            bool bResult = false;
            if (this._bllType == "add")
            {
                // BID
                faultageEntity.BindingId = IDGenerator.NewBindingID();

                // 断层信息登录
                bResult = FaultageBLL.insertFaultageInfo(faultageEntity);
                // 工作面坐标已经改变，需要更新工作面信息
                SendMessengToServer();

                //20140429 lyf
                DrawJLDC(faultageEntity);
                if (bResult)
                {
                    form.refreshadd();
                }
            }
            else
            {
                // 主键
                faultageEntity.FaultageId = this._iPK;
                // 断层信息修改
                bResult = FaultageBLL.updateFaultageInfo(faultageEntity);
                SendMessengToServer();

                //20140428 lyf 
                //获取揭露断层的BID
                string sBID = "";
                if (faultageEntity.FaultageId != null)
                {
                    sBID = FaultageBLL.selectFaultageBIDByFaultageId(faultageEntity.FaultageId);
                    faultageEntity.BindingId = sBID;
                    ModifyJLDC(faultageEntity);//修改图元
                }
                if (bResult)
                {
                    form.refreshUpdate();
                }
            }

            // 添加/修改成功的场合
            if (bResult)
            {
                this.Close();
            }
        }

        #region 绘制图元

        /// <summary>
        /// 修改揭露断层图元
        /// </summary>
        /// <param name="faultageEntity"></param>
        private void ModifyJLDC(FaultageEntity faultageEntity)
        {
            //1.获得当前编辑图层
            DrawSpecialCommon drawspecial = new DrawSpecialCommon();
            string sLayerAliasName = GIS.LayerNames.DEFALUT_EXPOSE_FAULTAGE;//“默认_揭露断层”图层
            IFeatureLayer featureLayer = drawspecial.GetFeatureLayerByName(sLayerAliasName);
            if (featureLayer == null)
            {
                MessageBox.Show("未找到" + sLayerAliasName + "图层,无法修改揭露断层图元。");
                return;
            }

            //2.删除原来图元，重新绘制新图元
            bool bIsDeleteOldFeature = DataEditCommon.DeleteFeatureByBId(featureLayer, faultageEntity.BindingId);
            //if (bIsDeleteOldFeature)
            {
                //绘制图元
                DrawJLDC(faultageEntity);
            }

        }

        /// <summary>
        /// 绘制揭露断层图元
        /// </summary>
        /// <param name="faultageEntity"></param>
        private void DrawJLDC(FaultageEntity faultageEntity)
        {
            ////1.获得当前编辑图层
            //DrawSpecialCommon drawspecial = new DrawSpecialCommon();
            //string sLayerAliasName = LibCommon.LibLayerNames.DEFALUT_EXPOSE_FAULTAGE;//“默认_揭露断层”图层
            //IFeatureLayer featureLayer = drawspecial.GetFeatureLayerByName(sLayerAliasName);
            //if (featureLayer == null)
            //{
            //    MessageBox.Show("未找到" + sLayerAliasName + "图层,无法绘制揭露断层图元。");
            //    return;
            //}

            ////2.生成要素（要根据中心点获取起止点）
            ////中心点
            //double centrePtX = Convert.ToDouble(this.txtCoordinateX.Text.ToString());
            //double centrePtY = Convert.ToDouble(this.txtCoordinateY.Text.ToString());
            //IPoint centrePt = new PointClass();
            //centrePt.X = centrePtX;
            //centrePt.Y = centrePtY;

            //// 图形坐标Z  //zwy 20140526 add
            //double dCoordinateZ = 0;
            //if (!double.TryParse(this.txtCoordinateZ.Text.Trim(), out dCoordinateZ))
            //{
            //    MessageBox.Show("输入的Z坐标不是有效数值，请检查！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            //centrePt.Z = dCoordinateZ;

            //double angle = Convert.ToDouble(this.txtAngle.Text.ToString());//倾角
            //double length = 10;//默认长度为20，左右各10

            ////计算起止点
            //IPoint fromPt = new PointClass();
            //IPoint toPt = new PointClass();
            //CalculateEndpoints(centrePt, angle, length, ref fromPt, ref toPt);
            //DataEditCommon.g_CurWorkspaceEdit.StartEditing(false);
            //DataEditCommon.g_CurWorkspaceEdit.StartEditOperation();
            //IFeature pFeature = featureLayer.FeatureClass.CreateFeature();

            //ILine line = new LineClass();
            //line.PutCoords(fromPt, toPt);
            //object Missing = Type.Missing;
            //ISegment segment = line as ISegment;
            //ISegmentCollection newLine = new PolylineClass();
            //newLine.AddSegment(segment, ref Missing, ref Missing);
            //IPolyline polyline = newLine as IPolyline;

            //DataEditCommon.ZMValue(pFeature, polyline);  //zwy 20140526 add
            //pFeature.Shape = polyline;

            ////2.1断层标注(DCBZ)
            //string strMC = this.txtFaultageName.Text;//断层名称
            //string strLC = this.txtGap.Text;//落差
            //string strQJ = this.txtAngle.Text;//倾角
            //string strDCBZ = strMC + " " + "H=" + strLC + "m" + " " + "<" + strQJ + "°";

            ////断层标注字段赋值（该字段值保持在图层属性中）
            //int index = featureLayer.FeatureClass.Fields.FindField("FAULTAGE_NAME");
            //pFeature.set_Value(index, strDCBZ);

            ////要素ID字段赋值（对应属性表中BindingID）
            //int iFieldID = pFeature.Fields.FindField(GIS_Const.FIELD_BID);
            //pFeature.Value[iFieldID] = faultageEntity.BindingId.ToString();

            //pFeature.Store();
            //DataEditCommon.g_CurWorkspaceEdit.StopEditOperation();
            //DataEditCommon.g_CurWorkspaceEdit.StopEditing(true);
            ////2.2给生成的断层赋符号
            //int ID = pFeature.OID;
            //string path = Application.StartupPath + @"\symbol.ServerStyle";//【这里用到了自己定义的符号库】
            ////默认为正断层（符号）
            //string sGalleryClassName = "123";
            //string symbolName = "123"; ;
            //if (this.rbtnFrontFaultage.Checked)//正断层
            //{

            //    sGalleryClassName = "123";
            //    symbolName = "123";
            //}
            //else if (this.rbtnOppositeFaultage.Checked)//逆断层
            //{

            //    sGalleryClassName = "1234";
            //    symbolName = "1234";
            //}

            //ILineSymbol lineSymbol = GetSymbol(path, sGalleryClassName, symbolName);
            //ILayer layer = featureLayer as ILayer;
            //SpecialLineRenderer(layer, ID, lineSymbol);
            //AddAnnotate(layer, GIS_Const.FILE_DCBZ);

            ////缩放到新增的线要素，并高亮该要素
            //GIS.Common.DataEditCommon.g_pMyMapCtrl.ActiveView.Extent = pFeature.Shape.Envelope;
            //GIS.Common.DataEditCommon.g_pMyMapCtrl.ActiveView.Extent.Expand(1.5, 1.5, true);
            //GIS.Common.DataEditCommon.g_pMyMapCtrl.Map.SelectFeature(featureLayer, pFeature);
            //GIS.Common.DataEditCommon.g_pMyMapCtrl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewAll, null, null);


            ILayer pLayer = DataEditCommon.GetLayerByName(DataEditCommon.g_pMap, GIS.LayerNames.DEFALUT_EXPOSE_FAULTAGE);
            IFeatureLayer featureLayer = (IFeatureLayer)pLayer;
            if (pLayer == null)
            {
                MessageBox.Show("未找到揭露断层图层,无法绘制揭露断层图元。");
                return;
            }
            //2.生成要素（要根据中心点获取起止点）
            //中心点
            double centrePtX = Convert.ToDouble(this.txtCoordinateX.Text.ToString());
            double centrePtY = Convert.ToDouble(this.txtCoordinateY.Text.ToString());
            IPoint centrePt = new PointClass();
            centrePt.X = centrePtX;
            centrePt.Y = centrePtY;

            // 图形坐标Z  //zwy 20140526 add
            double dCoordinateZ = 0;
            if (!double.TryParse(this.txtCoordinateZ.Text.Trim(), out dCoordinateZ))
            {
                MessageBox.Show("输入的Z坐标不是有效数值，请检查！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            centrePt.Z = dCoordinateZ;

            double angle = Convert.ToDouble(this.txtAngle.Text.ToString());//倾角
            double length = dLength / 2;//默认长度为20，左右各10

            //计算起止点
            IPoint fromPt = new PointClass();
            IPoint toPt = new PointClass();
            CalculateEndpoints(centrePt, angle, length, ref fromPt, ref toPt);

            ILine line = new LineClass();
            line.PutCoords(fromPt, toPt);
            object Missing = Type.Missing;
            ISegment segment = line as ISegment;
            ISegmentCollection newLine = new PolylineClass();
            newLine.AddSegment(segment, ref Missing, ref Missing);
            IPolyline polyline = newLine as IPolyline;

            List<ziduan> list = new List<ziduan>();
            list.Add(new ziduan("bid", faultageEntity.BindingId.ToString()));
            list.Add(new ziduan("FAULTAGE_NAME", faultageEntity.FaultageName.ToString()));
            list.Add(new ziduan("addtime", DateTime.Now.ToString()));
            list.Add(new ziduan("GAP", faultageEntity.Gap.ToString()));
            list.Add(new ziduan("ANGLE", faultageEntity.Angle.ToString()));
            list.Add(new ziduan("TREND", faultageEntity.Trend.ToString()));
            list.Add(new ziduan("SEPARATION", faultageEntity.Separation.ToString()));
            list.Add(new ziduan("type", faultageEntity.Type));

            IFeature pfeature = DataEditCommon.CreateNewFeature(featureLayer, polyline, list);
            if (pfeature != null)
            {
                GIS.MyMapHelp.Jump(polyline);
                DataEditCommon.g_pMyMapCtrl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography | esriViewDrawPhase.esriViewForeground, null, null);
            }
        }

        /// <summary>
        /// 根据中心点、倾角和长度计算起止点
        /// </summary>
        /// <param name="centrePt">中心点</param>
        /// <param name="angle">倾角</param>
        /// <param name="length">1/2长度</param>
        /// <param name="fromPt">起点</param>
        /// <param name="toPt">终点</param>
        private void CalculateEndpoints(IPoint centrePt, double angle, double length, ref IPoint fromPt, ref IPoint toPt)
        {
            double radian = (Math.PI / 180) * angle;//角度转为弧度

            fromPt.X = centrePt.X + length * Math.Cos(radian);
            fromPt.Y = centrePt.Y + length * Math.Sin(radian);

            toPt.X = centrePt.X - length * Math.Cos(radian);
            toPt.Y = centrePt.Y - length * Math.Sin(radian);

            //给Z坐标赋值
            if (centrePt.Z != double.NaN)
            {
                fromPt.Z = centrePt.Z;
                toPt.Z = centrePt.Z;
            }
            else
            {
                fromPt.Z = 0;
                toPt.Z = 0;
            }
        }

        /// <summary>
        /// 获得符号
        /// </summary>
        /// <param name="sServerStylePath"></param>
        /// <param name="sGalleryClassName"></param>
        /// <param name="symbolName"></param>
        /// <returns></returns>
        public static ILineSymbol GetSymbol(string sServerStylePath, string sGalleryClassName, string symbolName)
        {
            IStyleGallery pStyleGallery;
            IStyleGalleryStorage pStyleGalleryStorage;
            IEnumStyleGalleryItem pEnumStyleGalleryItem;
            IStyleGalleryItem pStyleGalleryItem;

            pStyleGallery = new ServerStyleGalleryClass();
            pStyleGalleryStorage = (IStyleGalleryStorage)pStyleGallery;
            ILineSymbol lineSymbol = new SimpleLineSymbolClass();
            pEnumStyleGalleryItem = new EnumServerStyleGalleryItemClass();

            //查找到符号
            pStyleGalleryStorage.TargetFile = sServerStylePath;//输入符号的地址
            pEnumStyleGalleryItem = pStyleGallery.get_Items("Line Symbols", sServerStylePath, sGalleryClassName);//sGalleryClassName为输入符号库的名称

            pEnumStyleGalleryItem.Reset();
            pStyleGalleryItem = pEnumStyleGalleryItem.Next();
            while (pStyleGalleryItem != null)
            {
                if (pStyleGalleryItem.Name == symbolName)
                {
                    lineSymbol = (ILineSymbol)pStyleGalleryItem.Item;
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(pEnumStyleGalleryItem);
                    break;
                }
                pStyleGalleryItem = pEnumStyleGalleryItem.Next();
            }
            //定义符号的大小,已经定义好了是size=30
            //lineSymbol.Width = 2;

            return lineSymbol;
        }
        public static void SpecialLineRenderer2(ILayer layer, string field, string value, ILineSymbol lineSymbol)
        {
            IGeoFeatureLayer geoFeaLayer = layer as IGeoFeatureLayer;
            IFeatureRenderer featureRenderer = geoFeaLayer.Renderer;
            IUniqueValueRenderer uniValueRender = new UniqueValueRenderer();

            ///选择某个字段作为渲染符号值
            IQueryFilter2 queryFilter = new QueryFilterClass();
            int fieldIndex;
            uniValueRender.FieldCount = 1;
            uniValueRender.Field[0] = field;
            queryFilter.AddField(field);
            fieldIndex = geoFeaLayer.FeatureClass.Fields.FindField(field);//获得字段的index  

            ISymbol customSymbol = (ISymbol)lineSymbol;
            ISymbol defaultSymbol;

            ///设置渲染符号进行渲染
            string sValue;
            IFeature feature = null;
            IFeatureCursor featureCursor;
            featureCursor = geoFeaLayer.FeatureClass.Search(queryFilter, true);
            feature = featureCursor.NextFeature();
            while (feature != null)
            {
                sValue = Convert.ToString(feature.get_Value(fieldIndex));
                if (sValue == value)
                {
                    uniValueRender.AddValue(sValue, "", customSymbol);
                }
                else
                {
                    ///非当前所选要素，其符号保持不变
                    defaultSymbol = geoFeaLayer.Renderer.get_SymbolByFeature(feature);
                    uniValueRender.AddValue(sValue, "", defaultSymbol);
                }

                feature = featureCursor.NextFeature();
            }

            if (featureCursor != null)
            {
                featureCursor = null;
                ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(featureCursor);
                //System.Runtime.InteropServices.Marshal.ReleaseComObject(featureCursor);  //释放指针                
            }
            geoFeaLayer.Renderer = uniValueRender as IFeatureRenderer;

        }

        public static void SpecialLineRenderer(ILayer layer, int ID, ILineSymbol lineSymbol)
        {
            IGeoFeatureLayer geoFeaLayer = layer as IGeoFeatureLayer;
            IFeatureRenderer featureRenderer = geoFeaLayer.Renderer;
            IUniqueValueRenderer uniValueRender = new UniqueValueRenderer();

            uniValueRender.FieldCount = 1;
            uniValueRender.Field[0] = "OBJECTID";
            ISymbol customSymbol = (ISymbol)lineSymbol;


            ///设置渲染符号进行渲染
            string sValue = ID.ToString();

            //选择某个字段作为渲染符号值            
            IFeatureCursor featureCursor = geoFeaLayer.FeatureClass.Search(null, true);
            IFeature feature = featureCursor.NextFeature();
            ISymbol defaultSymbol;
            while (feature != null)
            {
                int nowID = feature.OID;

                if (nowID == ID)
                {
                    uniValueRender.AddValue(feature.OID.ToString(), "", customSymbol);
                }
                else
                {
                    ///非当前所选要素，其符号保持不变
                    defaultSymbol = geoFeaLayer.Renderer.get_SymbolByFeature(feature);
                    uniValueRender.AddValue(feature.OID.ToString(), "", defaultSymbol);
                }

                feature = featureCursor.NextFeature();
            }

            geoFeaLayer.Renderer = uniValueRender as IFeatureRenderer;
        }

        public static void AddAnnotate(ILayer layer, string fieldName)
        {
            IGeoFeatureLayer pGeoLayer = layer as IGeoFeatureLayer;
            IAnnotateLayerPropertiesCollection IPALPColl = pGeoLayer.AnnotationProperties;
            IPALPColl.Clear();
            IColor fontColor = new RgbColor();
            fontColor.RGB = 255;//字体颜色      
            stdole.IFontDisp pFont = new StdFont()
            {
                Name = "宋体",
                Bold = true
            } as IFontDisp;

            ITextSymbol pTextSymbol = new TextSymbolClass()
            {
                Color = fontColor,
                Font = pFont,
                Size = 12
            };
            ////用来控制标注和要素的相对位置关系  

            ILineLabelPosition pLineLpos = new LineLabelPositionClass()
            {
                Parallel = true,  //修改标注的属性     
                //Perpendicular = false,  
                Below = true,
                InLine = false,
                Above = false
            };

            //用来控制标注冲突      
            ILineLabelPlacementPriorities pLinePlace = new LineLabelPlacementPrioritiesClass()
            {
                AboveStart = 5, //让above 和start的优先级为5 
                BelowAfter = 4
            };

            //用来实现对ILineLabelPosition 和 ILineLabelPlacementPriorities以及更高级属性的控制

            IBasicOverposterLayerProperties pBOLP = new BasicOverposterLayerPropertiesClass()
            {
                FeatureType = esriBasicOverposterFeatureType.esriOverposterPolygon,
                LineLabelPlacementPriorities = pLinePlace,
                LineLabelPosition = pLineLpos
            };
            //创建标注对象          
            ILabelEngineLayerProperties pLableEngine = new LabelEngineLayerPropertiesClass()
            {
                Symbol = pTextSymbol,
                BasicOverposterLayerProperties = pBOLP,
                IsExpressionSimple = true,
                Expression = "[" + fieldName + "]"
            };
            //设置标注的参考比例尺  
            IAnnotateLayerTransformationProperties pAnnoLyrPros = pLableEngine as IAnnotateLayerTransformationProperties; pAnnoLyrPros.ReferenceScale = 2500000;
            //设置标注可见的最大最小比例尺       
            IAnnotateLayerProperties pAnnoPros = pLableEngine as IAnnotateLayerProperties;
            //pAnnoPros.AnnotationMaximumScale = 2500000;       
            //pAnnoPros.AnnotationMinimumScale = 25000000;  
            //pAnnoPros.WhereClause属性  设置过滤条件   
            IPALPColl.Add(pAnnoPros);
            pGeoLayer.DisplayAnnotation = true;
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
            // 判断断层名称是否录入
            if (!Check.isEmpty(this.txtFaultageName, Const_GM.FAULTAGE_NAME))
            {
                return false;
            }

            // 断层名称特殊字符判断
            if (!Check.checkSpecialCharacters(this.txtFaultageName, Const_GM.FAULTAGE_NAME))
            {
                return false;
            }

            // 只有当添加新断层信息的时候才去判断断层名称是否重复
            if (this._bllType == "add")
            {
                // 判断探头名称是否存在
                if (!Check.isExist(this.txtFaultageName, Const_GM.FAULTAGE_NAME,
                    FaultageBLL.isFaultageNameExist(this.txtFaultageName.Text.Trim())))
                {
                    return false;
                }
            }
            else
            {
                /* 修改的时候，首先要获取UI输入的断层名称到DB中去检索，
                如果检索件数 > 0 并且该断层ID还不是传过来的主键，那么视为输入了已存在的断层名称 */
                DataSet ds = FaultageBLL.selectFaultageInfoByFaultageName(this.txtFaultageName.Text.Trim());
                if (ds.Tables[0].Rows.Count > 0 && !ds.Tables[0].Rows[0][FaultageDbConstNames.FAULTAGE_ID].ToString().Equals(_iPK.ToString()))
                {
                    this.txtFaultageName.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const_GM.FAULTAGE_EXIST_MSG); // 断层名称已存在，请重新录入！
                    this.txtFaultageName.Focus();
                    return false;
                }
            }

            // 判断落差是否录入
            if (!Check.isEmpty(this.txtGap, Const_GM.GAP))
            {
                return false;
            }
            // 判断长度是否录入
            if (!Check.isEmpty(this.txtLength, "长度"))
            {
                return false;
            }

            // 判断落差是否为数字
            if (!Check.IsNumeric(this.txtGap, Const_GM.GAP))
            {
                return false;
            }

            // 判断倾角是否录入
            if (!Check.isEmpty(this.txtAngle, Const_GM.ANGLE))
            {
                return false;
            }

            // 判断倾角是否为数字
            if (!Check.IsNumeric(this.txtAngle, Const_GM.ANGLE))
            {
                return false;
            }
            // 判断长度是否为数字
            if (!Check.IsNumeric(this.txtLength, "长度"))
            {
                return false;
            }
            // 判断走向是否录入
            if (!Check.isEmpty(this.txtTrend, Const_GM.TREND))
            {
                return false;
            }

            // 判断走向是否为数字
            if (!Check.IsNumeric(this.txtTrend, Const_GM.TREND))
            {
                return false;
            }

            // 判断断距是否录入
            if (!Check.isEmpty(this.txtSeparation, Const_GM.SEPARATION))
            {
                return false;
            }

            // 判断断距是否为数字
            if (!Check.IsNumeric(this.txtSeparation, Const_GM.SEPARATION))
            {
                return false;
            }

            //****************************************************
            // 判断坐标X是否录入
            if (!Check.isEmpty(this.txtCoordinateX, Const_GM.COORDINATE_X))
            {
                return false;
            }

            // 判断坐标X是否为数字
            if (!Check.IsNumeric(this.txtCoordinateX, Const_GM.COORDINATE_X))
            {
                return false;
            }

            // 判断坐标Y是否录入
            if (!Check.isEmpty(this.txtCoordinateY, Const_GM.COORDINATE_Y))
            {
                return false;
            }

            // 判断坐标Y是否为数字
            if (!Check.IsNumeric(this.txtCoordinateY, Const_GM.COORDINATE_Y))
            {
                return false;
            }

            // 判断坐标Z是否录入
            if (!Check.isEmpty(this.txtCoordinateZ, Const_GM.COORDINATE_Z))
            {
                return false;
            }

            // 判断坐标Z是否为数字
            if (!Check.IsNumeric(this.txtCoordinateZ, Const_GM.COORDINATE_Z))
            {
                return false;
            }
            //****************************************************

            // 验证通过
            return true;
        }

        /// <summary>
        /// 设置断层信息
        /// </summary>
        private void setFaultageInfo()
        {
            // 通过主键获取断层信息
            DataSet ds = FaultageBLL.selectFaultageInfoByFaultageId(this._iPK);

            if (ds.Tables[0].Rows.Count > 0)
            {
                // 断层名称
                this.txtFaultageName.Text = ds.Tables[0].Rows[0][FaultageDbConstNames.FAULTAGE_NAME].ToString();
                // 落差
                this.txtGap.Text = ds.Tables[0].Rows[0][FaultageDbConstNames.GAP].ToString();
                // 倾角
                this.txtAngle.Text = ds.Tables[0].Rows[0][FaultageDbConstNames.ANGLE].ToString();
                // 类型
                if (Const_GM.FRONT_FAULTAGE.Equals(ds.Tables[0].Rows[0][FaultageDbConstNames.TYPE].ToString()))
                {
                    this.rbtnFrontFaultage.Checked = true;
                }
                else
                {
                    this.rbtnOppositeFaultage.Checked = true;
                }
                // 走向
                this.txtTrend.Text = ds.Tables[0].Rows[0][FaultageDbConstNames.TREND].ToString();
                // 断距
                this.txtSeparation.Text = ds.Tables[0].Rows[0][FaultageDbConstNames.SEPARATION].ToString();
                // 坐标X
                this.txtCoordinateX.Text = ds.Tables[0].Rows[0][FaultageDbConstNames.X].ToString();
                // 坐标Y
                this.txtCoordinateY.Text = ds.Tables[0].Rows[0][FaultageDbConstNames.Y].ToString();
                // 坐标Z
                this.txtCoordinateZ.Text = ds.Tables[0].Rows[0][FaultageDbConstNames.Z].ToString();
                //长度
                string bid = ds.Tables[0].Rows[0][FaultageDbConstNames.BID].ToString();
                ILayer pLayer = DataEditCommon.GetLayerByName(DataEditCommon.g_pMap, GIS.LayerNames.DEFALUT_EXPOSE_FAULTAGE);
                IFeatureLayer featureLayer = (IFeatureLayer)pLayer;
                if (pLayer == null)
                {
                    txtLength.Text = "0";
                    return;
                }
                IFeature pFeature = MyMapHelp.FindFeatureByWhereClause(featureLayer, "BID='" + bid + "'");
                if (pFeature != null)
                {
                    IPolyline pline = (IPolyline)pFeature.Shape;
                    if (pline == null) return;
                    txtLength.Text = Math.Round(pline.Length).ToString();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GIS.Common.DataEditCommon.PickUpPoint(txtCoordinateX, txtCoordinateY);
        }

        private void SendMessengToServer()
        {
            Log.Debug("更新服务端断层Map------开始");
            // 通知服务端回采进尺已经添加
            GeologyMsg msg = new GeologyMsg(0, 0, "", DateTime.Now, COMMAND_ID.UPDATE_GEOLOG_DATA);
            this.SendMsg2Server(msg);
            Log.Debug("服务端断层Map------完成" + msg.ToString());
        }

        private void btnReadTxt_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = @"C:\";
            ofd.RestoreDirectory = true;
            ofd.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
            //ofd.ShowDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string aa = ofd.FileName;
                StreamReader sr = new StreamReader(@aa);
                string duqu;
                while ((duqu = sr.ReadLine()) != null)
                {
                    String[] str = duqu.Split('|');
                    txtFaultageName.Text = str[0];
                    txtCoordinateX.Text = str[1].Split(',')[0];
                    txtCoordinateY.Text = str[1].Split(',')[1];
                    txtCoordinateZ.Text = "0";
                    txtSeparation.Text = str[2];
                    txtGap.Text = str[2];
                    txtTrend.Text = str[4];
                    txtAngle.Text = str[5];
                    txtLength.Text = str[6];
                }
            }
        }
    }
}
