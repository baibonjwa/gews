// ******************************************************************
// 概  述：探头数据管理
// 作  者：伍鑫
// 创建日期：2013/12/01
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************

using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraPrinting;
using LibCommon;
using LibCommonControl;
using LibEntity;

namespace LibPanels
{
    public partial class ProbeInfoManagement : BaseForm
    {

        public ProbeInfoManagement()
        {
            InitializeComponent();

        }

        private void RefreshData()
        {
            if (cbtnAll.Checked)
            {
                var probes = Probe.FindAll();
                gcProbe.DataSource = probes;
            }
            else
            {
                var probes = Probe.FindAllWithGasOrVentilation();
                gcProbe.DataSource = probes;
            }
        }

        private void ProbeInfoManagement_Load(object sender, EventArgs e)
        {
            RefreshData();
        }


        /// <summary>
        ///     打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnPrint_Click(object sender, EventArgs e)
        {
            DevUtil.DevPrint(gcProbe, "传感器数据报表");
        }

        private void bandedGridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
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

        private void sbtnUpdateProbe_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog
            {
                InitialDirectory = @"C:\Desktop",
                RestoreDirectory = true,
                Filter = @"文本文件(*.txt)|*.txt|所有文件(*.*)|*.*",
                Multiselect = true
            };
            //ofd.ShowDialog();
            if (ofd.ShowDialog() != DialogResult.OK) return;
            var aa = ofd.FileName;
            var strs = File.ReadAllLines(aa, Encoding.GetEncoding("GB2312"));
            for (var i = 1; i < strs.Length; i++)
            {
                var line = strs[i].Split(',');
                var probe = new Probe
                {
                    ProbeMeasureType = Convert.ToInt16(line[0]),
                    ProbeId = line[1].Substring(3),
                    ProbeDescription = line[2],
                    ProbeTypeDisplayName = line[3]
                };
                probe.ProbeMeasureType = Convert.ToInt16(line[4]);
                probe.ProbeUseType = line[5];
                probe.Unit = line[6];

                probe.SaveAndFlush();
            }
        }

        private void tsBtnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tsExport_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                gcProbe.ExportToXls(saveFileDialog1.FileName);
            }
        }

        private void tsBtnRefresh_Click(object sender, EventArgs e)
        {
            RefreshData();
            //gcProbe.Views[0].RefreshData();
        }

        private void tsBtnModify_Click(object sender, EventArgs e)
        {
            var probe = (Probe)bandedGridView1.GetFocusedRow();
            var probeInfoEntering = new ProbeInfoEntering(probe.ProbeId, this);
            if (probeInfoEntering.ShowDialog() == DialogResult.OK)
            {
                RefreshData();
            }
        }

        private void tsBtnDel_Click(object sender, EventArgs e)
        {
            if (!Alert.confirm("确认清除绑定吗？")) return;
            var probe = (Probe)bandedGridView1.GetFocusedRow();
            probe.Tunnel = null;
            probe.Save();
            bandedGridView1.RefreshData();
        }

        private void cbtnAll_CheckedChanged(object sender, EventArgs e)
        {
            RefreshData();
        }
    }
}