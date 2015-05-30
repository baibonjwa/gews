// ******************************************************************
// 概  述：从数据库选择图例，添加到出图
// 作  者：张柏林
// 创建日期：2014/08/09
// 版本号：V1.0
// 版本信息:
// V1.0 新建
// ******************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LibDatabase;
using LibBusiness;
using LibEntity;
using LibCommon;
using System.IO;
using System.Collections;

namespace LibCommonForm
{
    public partial class SelectMapLegend : Form
    {
        ArrayList selectedListGUID = new ArrayList();
        ManageDataBase TheManage = new ManageDataBase(LibDatabase.DATABASE_TYPE.WarningManagementDB);
        public SelectMapLegend()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)

        {
            dgvLegendList.RowTemplate.Height = 120;
            DataSet temds = TheManage.ReturnDS ("select * from T_MAP_LEGEND");

            for (int i = 0; i < temds.Tables[0].Rows.Count; i++)
            {
                dgvLegendList.Rows.Add(1);
                byte[] by = (byte[])temds.Tables[0].Rows[i]["LegendPic"];
                MemoryStream mst = new MemoryStream(by);
                mst.Write(by, 0, by.Length);
                Bitmap image = new Bitmap(mst);

                dgvLegendList[1, i].Value = temds.Tables[0].Rows[i]["LegendName"].ToString();
                dgvLegendList[2, i].Value = image;
                dgvLegendList[4, i].Value = temds.Tables[0].Rows[i]["BID"].ToString();

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvLegendList.Rows.Count; i++)
            {
                dgvLegendList[0, i].Value = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvLegendList.Rows.Count; i++)
            {
                dgvLegendList[0, i].Value = false ;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            selectedListGUID.Clear();
            for (int i = 0; i < dgvLegendList.Rows.Count; i++)
            {
                if ((bool)dgvLegendList[0, i].Value == true)
                {
                    selectedListGUID.Add(dgvLegendList [4,i].Value .ToString ());
                }
            }


            ////经纬完成代码
            //selectedListGUID保存了要添加到地图中的图例的guid，对应表的名字是T_MAP_LEGEND
        }
    }
}
