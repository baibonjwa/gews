using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using LibCommon;
using LibEntity;

namespace LibPanels
{
    public partial class ProbeInfoManagement : Form
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
                var probeId = line[1].Substring(3);
                var probe = Probe.TryFind(probeId);

                ProbeType probeType;
                if (line[3].Contains("CH4") || line[3].Contains("甲烷"))
                {
                    probeType = ProbeType.FindProbeTypeByProbeTypeName("甲烷");
                }
                else if (line[3].Contains("风速"))
                {
                    probeType = ProbeType.FindProbeTypeByProbeTypeName("风速");
                }
                else
                {
                    probeType = ProbeType.FindProbeTypeByProbeTypeName("其他");
                }
                Regex reg = new Regex(@"\(.*?\)");
                Match m = reg.Match(line[2]);
                string probeName = "";
                if (m.Value.Contains("T"))
                    probeName = m.Value.Trim('(', ')');


                if (probe == null)
                {
                    probe = new Probe
                    {
                        ProbeMeasureType = Convert.ToInt16(line[0]),
                        ProbeId = line[1].Substring(3),
                        ProbeDescription = line[2],
                        ProbeTypeDisplayName = line[3],
                        ProbeUseType = line[5],
                        Unit = line[6],
                        ProbeType = probeType,
                        ProbeName = probeName
                    };
                }
                else
                {
                    probe.ProbeMeasureType = Convert.ToInt16(line[0]);
                    probe.ProbeTypeDisplayName = line[3];
                    probe.ProbeUseType = line[5];
                    probe.Unit = line[6];
                    probe.ProbeType = probeType;
                    probe.ProbeName = probeName;
                    if (probe.ProbeDescription != line[2])
                    {
                        probe.ProbeDescription = line[2];
                        probe.Tunnel = null;
                    }
                }
                probe.Save();
            }
            RefreshData();
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
            var probeInfoEntering = new ProbeInfoEntering(probe.ProbeId);
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