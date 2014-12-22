// ******************************************************************
// 概  述：巷道预警结果表
// 作  者：伍鑫
// 创建日期：2013/12/01
// 版本号：1.0
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
using System.Text.RegularExpressions;
using LibCommon;
using LibBusiness;
using LibCommonForm;

namespace _5.WarningManagement
{
    public partial class PreWarningResultTable : Form
    {
        private int _tunnelID = -1;
        public PreWarningResultTable()
        {
            InitializeComponent();
        }

        public PreWarningResultTable(LibEntity.PreWarningResultTable preWarningResultTableEntity, int tunnelID)
        {
            InitializeComponent();
            _tunnelID = tunnelID;
            UpdateTableContents(preWarningResultTableEntity, tunnelID);
        }

        public void UpdateTableContents(LibEntity.PreWarningResultTable preWarningResultTableEntity, int tunnelID)
        {
            if (preWarningResultTableEntity != null)
            {
                _tunnelID = tunnelID;
                //获取巷道实体信息
                Tunnel tunnelEnt = BasicInfoManager.getInstance().getTunnelByID(_tunnelID);
                // 标题
                this.fpPreWarningResultTable.Sheets[0].Cells[0, 0].Text = tunnelEnt.TunnelName + "预警结果表";
                // 预警日期
                this.fpPreWarningResultTable.Sheets[0].Cells[3, 1].Text = preWarningResultTableEntity.PreWarningDate;
                // 预警时间
                this.fpPreWarningResultTable.Sheets[0].Cells[3, 3].Text = preWarningResultTableEntity.PreWarningTime;

                /** 预警结果 **/
                // 超限预警
                this.fpPreWarningResultTable.Sheets[0].Cells[7, 2].Text = preWarningResultTableEntity.PreWarningResultArr[0].UltralimitPreWarning;
                // 超限预警-说明
                this.fpPreWarningResultTable.Sheets[0].Cells[7, 5].Text = preWarningResultTableEntity.PreWarningResultArr[0].UltralimitPreWarningEX;
                // 突出预警
                this.fpPreWarningResultTable.Sheets[0].Cells[9, 2].Text = preWarningResultTableEntity.PreWarningResultArr[0].OutburstPreWarning;
                // 突出预警-说明
                this.fpPreWarningResultTable.Sheets[0].Cells[9, 5].Text = preWarningResultTableEntity.PreWarningResultArr[0].OutburstPreWarningEX;

                /** 预警依据 **/
                for (int i = 1; i < preWarningResultTableEntity.PreWarningResultArr.Length; i++)
                {
                    // 超限预警
                    this.fpPreWarningResultTable.Sheets[0].Cells[9 + i * 4, 2].Text = preWarningResultTableEntity.PreWarningResultArr[i].UltralimitPreWarning;
                    // 超限预警-说明
                    this.fpPreWarningResultTable.Sheets[0].Cells[9 + i * 4, 5].Text = preWarningResultTableEntity.PreWarningResultArr[i].UltralimitPreWarningEX;
                    int count = setCellHigh(preWarningResultTableEntity.PreWarningResultArr[i].UltralimitPreWarningEX);
                    this.fpPreWarningResultTable.Sheets[0].Rows[10 + i * 4].Height = this.fpPreWarningResultTable.Sheets[0].Rows[9 + i * 4].Height * count;
                    // 突出预警
                    this.fpPreWarningResultTable.Sheets[0].Cells[11 + i * 4, 2].Text = preWarningResultTableEntity.PreWarningResultArr[i].OutburstPreWarning;
                    // 突出预警-说明
                    this.fpPreWarningResultTable.Sheets[0].Cells[11 + i * 4, 5].Text = preWarningResultTableEntity.PreWarningResultArr[i].OutburstPreWarningEX;
                    int count2 = setCellHigh(preWarningResultTableEntity.PreWarningResultArr[i].OutburstPreWarningEX);
                    this.fpPreWarningResultTable.Sheets[0].Rows[12 + i * 4].Height = this.fpPreWarningResultTable.Sheets[0].Rows[11 + i * 4].Height * count2;
                }
            }
        }

        /// <summary>
        /// 设置行高
        /// </summary>
        private int setCellHigh(string str)
        {
            string[] arr = Regex.Split(str, Const_WM.WARNING_REASON_SEPERATOR_RETURN);

            return arr.Length;
        }

        private void toolStripBtnExport_Click(object sender, EventArgs e)
        {

        }

        private void toolStripBtnPrint_Click(object sender, EventArgs e)
        {
            FilePrint.CommonPrint(fpPreWarningResultTable, 0);
        }
    }
}
