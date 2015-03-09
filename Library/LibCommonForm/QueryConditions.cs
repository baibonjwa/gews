using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LibCommon;
using LibCommonControl;

namespace LibCommonForm
{
    public partial class QueryConditions : Control
    {
        public String DefaultStartTime { get; set; }
        public String DefaultEndTime { get; set; }
        public delegate void BindDataGrid();
        public new BindDataGrid Show { get; set; }
        public int TunnelId { get; set; }
        public QueryConditions()
        {
            InitializeComponent();
            if (!String.IsNullOrEmpty(DefaultStartTime))
            {
                _dateTimeStart.Text = DefaultStartTime;
            }
            else
            {
                DefaultStartTime = DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00";
                this._dateTimeStart.Text = DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00";
            }

            if (!String.IsNullOrEmpty(DefaultEndTime))
            {
                _dateTimeEnd.Text = DefaultEndTime;
            }
            else
            {
                DefaultEndTime = DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59";
                this._dateTimeEnd.Text = DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59";
            }
        }

        private void _btnSearch_Click(object sender, EventArgs e)
        {
            if (selectTunnelSimple1.SelectedTunnel == null)
            {
                Alert.alert("请选择巷道");
            }
            else
            {
                TunnelId = selectTunnelSimple1.SelectedTunnel.TunnelId;
                if (String.IsNullOrEmpty(DefaultStartTime))
                {
                    DefaultStartTime = DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00";
                    this._dateTimeStart.Text = DefaultStartTime;
                }
                if (String.IsNullOrEmpty(DefaultEndTime))
                {
                    DefaultEndTime = DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59";
                    this._dateTimeEnd.Text = DefaultEndTime;
                }
                Show();
            }

        }

        private void _dateTimeStart_ValueChanged(object sender, EventArgs e)
        {
            DefaultStartTime = this._dateTimeStart.Text;
        }

        private void _dateTimeEnd_ValueChanged(object sender, EventArgs e)
        {
            DefaultEndTime = this._dateTimeEnd.Text;
        }


    }
}
