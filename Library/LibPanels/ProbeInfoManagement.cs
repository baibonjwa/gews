// ******************************************************************
// 概  述：探头数据管理
// 作  者：伍鑫
// 创建日期：2013/12/01
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************

using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.Data.PLinq.Helpers;
using DevExpress.XtraGrid.Views.Grid;
using FarPoint.Win;
using FarPoint.Win.Spread;
using FarPoint.Win.Spread.CellType;
using LibCommon;
using LibCommonControl;
using LibEntity;

namespace LibPanels
{
    public partial class ProbeInfoManagement 
    {

        public ProbeInfoManagement()
        {
            InitializeComponent();

            gcProbe.DataSource = Probe.FindAll();
        }

        private void ProbeInfoManagement_Load(object sender, EventArgs e)
        {

        }


        /// <summary>
        ///     导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnExport_Click(object sender, EventArgs e)
        {
            //if (FileExport.fileExport(fpProbeInfo, true))
            //{
            //    Alert.alert(Const.EXPORT_SUCCESS_MSG);
            //}
        }

        /// <summary>
        ///     打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnPrint_Click(object sender, EventArgs e)
        {
            //FilePrint.CommonPrint(fpProbeInfo, 0);
        }

        private void btnProbeImport_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.InitialDirectory = @"C:\Desktop";
            ofd.RestoreDirectory = true;
            ofd.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
            ofd.Multiselect = true;
            //ofd.ShowDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string aa = ofd.FileName;
                string[] strs = File.ReadAllLines(aa, Encoding.GetEncoding("GB2312"));
                string type = "";
                for (int i = 1; i < strs.Length; i++)
                {
                    string[] line = strs[i].Split(',');
                    var probe = new Probe();
                    probe.ProbeMeasureType = Convert.ToInt16(line[0]);
                    probe.ProbeId = line[1].Substring(3);
                    probe.ProbeDescription = line[2];
                    probe.ProbeTypeDisplayName = line[3];
                    probe.ProbeMeasureType = Convert.ToInt16(line[4]);
                    probe.ProbeUseType = line[5];
                    probe.Unit = line[6];

                    probe.SaveAndFlush();
                }
            }
        }

        private void bgvProbe_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "IsMove")
            {
                switch (e.DisplayText)
                {
                    case "0":
                        e.DisplayText = "否";
                        break;
                    case "1":
                        e.DisplayText = "是";
                        break;
                }
            }
        }


    }
}