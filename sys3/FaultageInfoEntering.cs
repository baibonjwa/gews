using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using GIS;
using GIS.Common;
using LibBusiness;
using LibCommon;
using LibCommonControl;
using LibEntity;
using LibSocket;
using stdole;

namespace sys3
{
    public partial class FaultageInfoEntering : MainFrm
    {
        /** 主键  **/
        /** 业务逻辑类型：添加/修改  **/
        private readonly string _bllType = "add";
        private int _iPK;
        private double dLength; //长度

        /// <summary>
        ///     构造方法
        /// </summary>
        public FaultageInfoEntering()
        {
            InitializeComponent();

            // 设置窗体默认属性
            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_GM.INSERT_FAULTAGE_INFO);
        }

        /// <summary>
        ///     带参数的构造方法
        /// </summary>
        /// <param name="strPrimaryKey">主键</param>
        public FaultageInfoEntering(Faultage faultage)
        {
            InitializeComponent();

            // 设置窗体默认属性
            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_GM.UPDATE_FAULTAGE_INFO);

            // 设置业务类型
            _bllType = "update";

            // 设置断层信息
            // 断层名称
            txtFaultageName.Text = faultage.FaultageName;
            // 落差
            txtGap.Text = faultage.Gap;
            // 倾角
            txtAngle.Text = faultage.Angle.ToString(CultureInfo.InvariantCulture);
            // 类型
            if (Const_GM.FRONT_FAULTAGE.Equals(faultage.Type))
            {
                rbtnFrontFaultage.Checked = true;
            }
            else
            {
                rbtnOppositeFaultage.Checked = true;
            }
            // 走向
            txtTrend.Text = faultage.Trend;
            // 断距
            txtSeparation.Text = faultage.Separation;
            // 坐标X
            txtCoordinateX.Text = faultage.CoordinateX.ToString(CultureInfo.InvariantCulture);
            // 坐标Y
            txtCoordinateY.Text = faultage.CoordinateY.ToString(CultureInfo.InvariantCulture);
            // 坐标Z
            txtCoordinateZ.Text = faultage.CoordinateZ.ToString(CultureInfo.InvariantCulture);
            //长度
            string bid = faultage.BindingId;
            ILayer pLayer = DataEditCommon.GetLayerByName(DataEditCommon.g_pMap, LayerNames.DEFALUT_EXPOSE_FAULTAGE);
            var featureLayer = (IFeatureLayer)pLayer;
            if (pLayer == null)
            {
                txtLength.Text = "0";
                return;
            }
            IFeature pFeature = MyMapHelp.FindFeatureByWhereClause(featureLayer, "BID='" + bid + "'");
            if (pFeature != null)
            {
                var pline = (IPolyline)pFeature.Shape;
                if (pline == null) return;
                txtLength.Text = Math.Round(pline.Length).ToString();
            }
        }

        /// <summary>
        ///     提  交
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

            // 创建断层实体
            var faultageEntity = new Faultage();

            // 断层名称
            faultageEntity.FaultageName = txtFaultageName.Text.Trim();

            // 落差
            faultageEntity.Gap = txtGap.Text.Trim();

            // 倾角
            double dAngle = 0;
            if (double.TryParse(txtAngle.Text.Trim(), out dAngle))
            {
                faultageEntity.Angle = dAngle;
            }

            // 类型
            Control[] arrControl2 = pnlType.Controls.Find("rbtnFrontFaultage", true);
            var _rbtnFrontFaultage = arrControl2[0] as RadioButton;
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
            if (double.TryParse(txtCoordinateX.Text.Trim(), out dCoordinateX))
            {
                faultageEntity.CoordinateX = dCoordinateX;
            }

            // 坐标Y
            double dCoordinateY = 0;
            if (double.TryParse(txtCoordinateY.Text.Trim(), out dCoordinateY))
            {
                faultageEntity.CoordinateY = dCoordinateY;
            }

            // 坐标Z
            double dCoordinateZ = 0;
            if (double.TryParse(txtCoordinateZ.Text.Trim(), out dCoordinateZ))
            {
                faultageEntity.CoordinateZ = dCoordinateZ;
            }
            dLength = 0;
            if (double.TryParse(txtLength.Text.Trim(), out dLength))
            {
            }
            bool bResult = false;
            if (_bllType == "add")
            {
                // BID
                faultageEntity.BindingId = IDGenerator.NewBindingID();

                // 断层信息登录
                faultageEntity.Save();
                // 工作面坐标已经改变，需要更新工作面信息
                SendMessengToServer();

                //20140429 lyf
                DrawJLDC(faultageEntity);
            }
            else
            {
                // 主键
                faultageEntity.FaultageId = _iPK;
                faultageEntity.Save();
                // 断层信息修改
                SendMessengToServer();

                //20140428 lyf 
                //获取揭露断层的BID
                string sBID = "";
                if (faultageEntity.FaultageId != null)
                {
                    sBID = Faultage.Find(_iPK).BindingId;
                    faultageEntity.BindingId = sBID;
                    ModifyJLDC(faultageEntity); //修改图元
                }
                if (bResult)
                {
                }
            }

            // 添加/修改成功的场合
            if (bResult)
            {
                Close();
            }
        }

        /// <summary>
        ///     取  消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            // 关闭窗口
            Close();
        }

        /// <summary>
        ///     验证画面入力数据
        /// </summary>
        /// <returns>验证结果：true 通过验证, false未通过验证</returns>
        private bool check()
        {
            // 判断断层名称是否录入
            if (!Check.isEmpty(txtFaultageName, Const_GM.FAULTAGE_NAME))
            {
                return false;
            }

            // 断层名称特殊字符判断
            if (!Check.checkSpecialCharacters(txtFaultageName, Const_GM.FAULTAGE_NAME))
            {
                return false;
            }

            // 只有当添加新断层信息的时候才去判断断层名称是否重复
            if (_bllType == "add")
            {
                // 判断探头名称是否存在
                if (Faultage.ExistsByFaultageName(txtFaultageName.Text))
                {
                    return false;
                }
            }
            else
            {
                /* 修改的时候，首先要获取UI输入的断层名称到DB中去检索，
                如果检索件数 > 0 并且该断层ID还不是传过来的主键，那么视为输入了已存在的断层名称 */
                if (Faultage.ExistsByFaultageName(txtFaultageName.Text))
                {
                    txtFaultageName.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const_GM.FAULTAGE_EXIST_MSG); // 断层名称已存在，请重新录入！
                    txtFaultageName.Focus();
                    return false;
                }
            }

            // 判断落差是否录入
            if (!Check.isEmpty(txtGap, Const_GM.GAP))
            {
                return false;
            }
            // 判断长度是否录入
            if (!Check.isEmpty(txtLength, "长度"))
            {
                return false;
            }

            // 判断落差是否为数字
            if (!Check.IsNumeric(txtGap, Const_GM.GAP))
            {
                return false;
            }

            // 判断倾角是否录入
            if (!Check.isEmpty(txtAngle, Const_GM.ANGLE))
            {
                return false;
            }

            // 判断倾角是否为数字
            if (!Check.IsNumeric(txtAngle, Const_GM.ANGLE))
            {
                return false;
            }
            // 判断长度是否为数字
            if (!Check.IsNumeric(txtLength, "长度"))
            {
                return false;
            }
            // 判断走向是否录入
            if (!Check.isEmpty(txtTrend, Const_GM.TREND))
            {
                return false;
            }

            // 判断走向是否为数字
            if (!Check.IsNumeric(txtTrend, Const_GM.TREND))
            {
                return false;
            }

            // 判断断距是否录入
            if (!Check.isEmpty(txtSeparation, Const_GM.SEPARATION))
            {
                return false;
            }

            // 判断断距是否为数字
            if (!Check.IsNumeric(txtSeparation, Const_GM.SEPARATION))
            {
                return false;
            }

            //****************************************************
            // 判断坐标X是否录入
            if (!Check.isEmpty(txtCoordinateX, Const_GM.COORDINATE_X))
            {
                return false;
            }

            // 判断坐标X是否为数字
            if (!Check.IsNumeric(txtCoordinateX, Const_GM.COORDINATE_X))
            {
                return false;
            }

            // 判断坐标Y是否录入
            if (!Check.isEmpty(txtCoordinateY, Const_GM.COORDINATE_Y))
            {
                return false;
            }

            // 判断坐标Y是否为数字
            if (!Check.IsNumeric(txtCoordinateY, Const_GM.COORDINATE_Y))
            {
                return false;
            }

            // 判断坐标Z是否录入
            if (!Check.isEmpty(txtCoordinateZ, Const_GM.COORDINATE_Z))
            {
                return false;
            }

            // 判断坐标Z是否为数字
            if (!Check.IsNumeric(txtCoordinateZ, Const_GM.COORDINATE_Z))
            {
                return false;
            }
            //****************************************************

            // 验证通过
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataEditCommon.PickUpPoint(txtCoordinateX, txtCoordinateY);
        }

        private void SendMessengToServer()
        {
            Log.Debug("更新服务端断层Map------开始");
            // 通知服务端回采进尺已经添加
            var msg = new GeologyMsg(0, 0, "", DateTime.Now, COMMAND_ID.UPDATE_GEOLOG_DATA);
            SendMsg2Server(msg);
            Log.Debug("服务端断层Map------完成" + msg);
        }

        private void btnReadTxt_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.InitialDirectory = @"C:\";
            ofd.RestoreDirectory = true;
            ofd.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
            //ofd.ShowDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string aa = ofd.FileName;
                var sr = new StreamReader(@aa);
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

        #region 绘制图元

        /// <summary>
        ///     修改揭露断层图元
        /// </summary>
        /// <param name="faultageEntity"></param>
        private void ModifyJLDC(Faultage faultageEntity)
        {
            //1.获得当前编辑图层
            var drawspecial = new DrawSpecialCommon();
            string sLayerAliasName = LayerNames.DEFALUT_EXPOSE_FAULTAGE; //“默认_揭露断层”图层
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
        ///     绘制揭露断层图元
        /// </summary>
        /// <param name="faultageEntity"></param>
        private void DrawJLDC(Faultage faultageEntity)
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


            ILayer pLayer = DataEditCommon.GetLayerByName(DataEditCommon.g_pMap, LayerNames.DEFALUT_EXPOSE_FAULTAGE);
            var featureLayer = (IFeatureLayer)pLayer;
            if (pLayer == null)
            {
                MessageBox.Show("未找到揭露断层图层,无法绘制揭露断层图元。");
                return;
            }
            //2.生成要素（要根据中心点获取起止点）
            //中心点
            double centrePtX = Convert.ToDouble(txtCoordinateX.Text);
            double centrePtY = Convert.ToDouble(txtCoordinateY.Text);
            IPoint centrePt = new PointClass();
            centrePt.X = centrePtX;
            centrePt.Y = centrePtY;

            // 图形坐标Z  //zwy 20140526 add
            double dCoordinateZ = 0;
            if (!double.TryParse(txtCoordinateZ.Text.Trim(), out dCoordinateZ))
            {
                MessageBox.Show(@"输入的Z坐标不是有效数值，请检查！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            centrePt.Z = dCoordinateZ;

            double angle = Convert.ToDouble(txtAngle.Text); //倾角
            double length = dLength / 2; //默认长度为20，左右各10

            //计算起止点
            IPoint fromPt = new PointClass();
            IPoint toPt = new PointClass();
            CalculateEndpoints(centrePt, angle, length, ref fromPt, ref toPt);

            ILine line = new LineClass();
            line.PutCoords(fromPt, toPt);
            object Missing = Type.Missing;
            var segment = line as ISegment;
            ISegmentCollection newLine = new PolylineClass();
            newLine.AddSegment(segment, ref Missing, ref Missing);
            var polyline = newLine as IPolyline;

            var list = new List<ziduan>();
            list.Add(new ziduan("bid", faultageEntity.BindingId));
            list.Add(new ziduan("FAULTAGE_NAME", faultageEntity.FaultageName));
            list.Add(new ziduan("addtime", DateTime.Now.ToString()));
            list.Add(new ziduan("GAP", faultageEntity.Gap));
            list.Add(new ziduan("ANGLE", faultageEntity.Angle.ToString()));
            list.Add(new ziduan("TREND", faultageEntity.Trend));
            list.Add(new ziduan("SEPARATION", faultageEntity.Separation));
            list.Add(new ziduan("type", faultageEntity.Type));

            IFeature pfeature = DataEditCommon.CreateNewFeature(featureLayer, polyline, list);
            if (pfeature != null)
            {
                MyMapHelp.Jump(polyline);
                DataEditCommon.g_pMyMapCtrl.ActiveView.PartialRefresh(
                    esriViewDrawPhase.esriViewGeography | esriViewDrawPhase.esriViewForeground, null, null);
            }
        }

        /// <summary>
        ///     根据中心点、倾角和长度计算起止点
        /// </summary>
        /// <param name="centrePt">中心点</param>
        /// <param name="angle">倾角</param>
        /// <param name="length">1/2长度</param>
        /// <param name="fromPt">起点</param>
        /// <param name="toPt">终点</param>
        private void CalculateEndpoints(IPoint centrePt, double angle, double length, ref IPoint fromPt, ref IPoint toPt)
        {
            double radian = (Math.PI / 180) * angle; //角度转为弧度

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
        ///     获得符号
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
            pStyleGalleryStorage.TargetFile = sServerStylePath; //输入符号的地址
            pEnumStyleGalleryItem = pStyleGallery.get_Items("Line Symbols", sServerStylePath, sGalleryClassName);
            //sGalleryClassName为输入符号库的名称

            pEnumStyleGalleryItem.Reset();
            pStyleGalleryItem = pEnumStyleGalleryItem.Next();
            while (pStyleGalleryItem != null)
            {
                if (pStyleGalleryItem.Name == symbolName)
                {
                    lineSymbol = (ILineSymbol)pStyleGalleryItem.Item;
                    Marshal.ReleaseComObject(pEnumStyleGalleryItem);
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
            var geoFeaLayer = layer as IGeoFeatureLayer;
            IFeatureRenderer featureRenderer = geoFeaLayer.Renderer;
            IUniqueValueRenderer uniValueRender = new UniqueValueRenderer();

            ///选择某个字段作为渲染符号值
            IQueryFilter2 queryFilter = new QueryFilterClass();
            int fieldIndex;
            uniValueRender.FieldCount = 1;
            uniValueRender.Field[0] = field;
            queryFilter.AddField(field);
            fieldIndex = geoFeaLayer.FeatureClass.Fields.FindField(field); //获得字段的index  

            var customSymbol = (ISymbol)lineSymbol;
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
                ComReleaser.ReleaseCOMObject(featureCursor);
                //System.Runtime.InteropServices.Marshal.ReleaseComObject(featureCursor);  //释放指针                
            }
            geoFeaLayer.Renderer = uniValueRender as IFeatureRenderer;
        }

        public static void SpecialLineRenderer(ILayer layer, int ID, ILineSymbol lineSymbol)
        {
            var geoFeaLayer = layer as IGeoFeatureLayer;
            IFeatureRenderer featureRenderer = geoFeaLayer.Renderer;
            IUniqueValueRenderer uniValueRender = new UniqueValueRenderer();

            uniValueRender.FieldCount = 1;
            uniValueRender.Field[0] = "OBJECTID";
            var customSymbol = (ISymbol)lineSymbol;


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
            var pGeoLayer = layer as IGeoFeatureLayer;
            IAnnotateLayerPropertiesCollection IPALPColl = pGeoLayer.AnnotationProperties;
            IPALPColl.Clear();
            IColor fontColor = new RgbColor();
            fontColor.RGB = 255; //字体颜色      
            var pFont = new StdFont
            {
                Name = "宋体",
                Bold = true
            } as IFontDisp;

            ITextSymbol pTextSymbol = new TextSymbolClass
            {
                Color = fontColor,
                Font = pFont,
                Size = 12
            };
            ////用来控制标注和要素的相对位置关系  

            ILineLabelPosition pLineLpos = new LineLabelPositionClass
            {
                Parallel = true, //修改标注的属性     
                //Perpendicular = false,  
                Below = true,
                InLine = false,
                Above = false
            };

            //用来控制标注冲突      
            ILineLabelPlacementPriorities pLinePlace = new LineLabelPlacementPrioritiesClass
            {
                AboveStart = 5, //让above 和start的优先级为5 
                BelowAfter = 4
            };

            //用来实现对ILineLabelPosition 和 ILineLabelPlacementPriorities以及更高级属性的控制

            IBasicOverposterLayerProperties pBOLP = new BasicOverposterLayerPropertiesClass
            {
                FeatureType = esriBasicOverposterFeatureType.esriOverposterPolygon,
                LineLabelPlacementPriorities = pLinePlace,
                LineLabelPosition = pLineLpos
            };
            //创建标注对象          
            ILabelEngineLayerProperties pLableEngine = new LabelEngineLayerPropertiesClass
            {
                Symbol = pTextSymbol,
                BasicOverposterLayerProperties = pBOLP,
                IsExpressionSimple = true,
                Expression = "[" + fieldName + "]"
            };
            //设置标注的参考比例尺  
            var pAnnoLyrPros = pLableEngine as IAnnotateLayerTransformationProperties;
            pAnnoLyrPros.ReferenceScale = 2500000;
            //设置标注可见的最大最小比例尺       
            var pAnnoPros = pLableEngine as IAnnotateLayerProperties;
            //pAnnoPros.AnnotationMaximumScale = 2500000;       
            //pAnnoPros.AnnotationMinimumScale = 25000000;  
            //pAnnoPros.WhereClause属性  设置过滤条件   
            IPALPColl.Add(pAnnoPros);
            pGeoLayer.DisplayAnnotation = true;
        }

        #endregion
    }
}