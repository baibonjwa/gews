using System;
using System.Windows.Forms;
using LibEntity;

namespace _5.WarningManagement
{
    public partial class TunnelPreWarningCalc : Form
    {
        private readonly PreWarningResultTable _table = new PreWarningResultTable();
        private readonly Tunnel tunnelEntity = new Tunnel();

        public TunnelPreWarningCalc()
        {
            InitializeComponent();
        }

        private void InitControls()
        {
            _dateTimePickerEnd.Value = DateTime.Now;
            _dateTimePickerStart.Value = _dateTimePickerEnd.Value.Subtract(new TimeSpan(24, 0, 0));
            var ts = _dateTimePickerEnd.Value.Subtract(_dateTimePickerStart.Value);
            _txtInterval.Text = ts.Days + "天 " + ts.Hours + "小时 " + ts.Minutes + "分 " + ts.Seconds + "秒";

            _table.MdiParent = this;
            _splitContainer.Panel2.Controls.Add(_table);
            _table.Dock = DockStyle.Fill;
            _table.Show();
            _table.Activate();
        }

        private void TunnelPreWarningCalc_Load(object sender, EventArgs e)
        {
            InitControls();
            bindMineName();
        }

        private void bindMineName()
        {
            //DataSet ds = TunnelInfoBLL.cboAddMineName();

            //for (int recordIdx = 0; recordIdx < ds.Tables[0].Rows.Count; recordIdx++)
            //{
            //    cboMineName.Items.Add(ds.Tables[0].Rows[recordIdx]["MINE_NAME"].ToString());
            //}
        }

        private void _btnCalc_Click(object sender, EventArgs e)
        {
            //PreWarningCalculationBLL calc = new PreWarningCalculationBLL();
            //int tunnelID = TunnelInfoBLL.getTunnelID(tunnelEntity);
            //DateTime dtMin = _dateTimePickerStart.Value;
            //DateTime dtMax = _dateTimePickerEnd.Value;
            //PreWarningResultEntity[] result = calc.CalcPreWarningResult(tunnelID, dtMin, dtMax);
            //if (null == result||result.Length < 1)
            //{
            //    MessageBox.Show("无预警数据！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            //PreWarningResultTableEntity tblEnt = PreWarningResultTableEntity.Convert2WarningResultTableEntity(result);
            //_table.UpdateTableContents(tblEnt, tunnelID);
        }

        private void cboMineName_SelectedIndexChanged(object sender, EventArgs e)
        {
            //cboHorizontal.Items.Clear();
            //tunnelEntity.MineName = cboMineName.Text;
            //DataSet ds = TunnelInfoBLL.cboAddHorizontal(tunnelEntity);
            //for (int recordIdx = 0; recordIdx < ds.Tables[0].Rows.Count; recordIdx++)
            //{
            //    cboHorizontal.Items.Add(ds.Tables[0].Rows[recordIdx]["HORIZONTAL"].ToString());
            //}
        }

        private void cboHorizontal_SelectedIndexChanged(object sender, EventArgs e)
        {
            //cboMiningArea.Items.Clear();
            //tunnelEntity.HorizontalName = cboHorizontal.Text;
            //DataSet ds = TunnelInfoBLL.cboAddMiningArea(tunnelEntity);
            //for (int recordIdx = 0; recordIdx < ds.Tables[0].Rows.Count; recordIdx++)
            //{
            //    cboMiningArea.Items.Add(ds.Tables[0].Rows[recordIdx]["MINING_AREA"].ToString());
            //}
        }

        private void cboMiningArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            //cboWorkingFace.Items.Clear();
            //tunnelEntity.MiningAreaName = cboMiningArea.Text;
            //DataSet ds = TunnelInfoBLL.cboAddWorkingFace(tunnelEntity);
            //for (int recordIdx = 0; recordIdx < ds.Tables[0].Rows.Count; recordIdx++)
            //{
            //    cboWorkingFace.Items.Add(ds.Tables[0].Rows[recordIdx]["WORKING_FACE"].ToString());
            //}
        }

        private void cboWorkingFace_SelectedIndexChanged(object sender, EventArgs e)
        {
            //cboTunnelName.Items.Clear();
            //tunnelEntity.WorkingFaceName = cboWorkingFace.Text;
            //DataSet ds = TunnelInfoBLL.cboAddTunnelName(tunnelEntity);
            //for (int recordIdx = 0; recordIdx < ds.Tables[0].Rows.Count; recordIdx++)
            //{
            //    cboTunnelName.Items.Add(ds.Tables[0].Rows[recordIdx]["TUNNEL_NAME"].ToString());
            //}
        }

        private void cboTunnelName_SelectedIndexChanged(object sender, EventArgs e)
        {
            tunnelEntity.TunnelName = cboTunnelName.Text;
        }
    }
}