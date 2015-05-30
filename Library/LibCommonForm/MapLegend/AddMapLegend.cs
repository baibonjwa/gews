// ******************************************************************
// 概  述：图例信息的添加，保存到数据库
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
namespace LibCommonForm
{
    public partial class AddMapLegend : Form
    {
        ManageDataBase TheManage = new ManageDataBase(LibDatabase.DATABASE_TYPE.WarningManagementDB);
        byte[] pbFilep;
        string tempFilePath="";
        Stream ms;
        public AddMapLegend()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dgvLegendList.RowTemplate.Height = 120;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openfile;
            openfile = new OpenFileDialog();
            openfile.Multiselect = true;
            string [] filelist;
            if (openfile.ShowDialog() == DialogResult.OK)
            {
                filelist = openfile.FileNames;
                for (int i = 0; i < filelist.Length;i++ )
                {
                    string [] temp=filelist[i].Split('\\');
                    dgvLegendList.Rows.Add(1);
                    dgvLegendList[1, dgvLegendList.Rows.Count - 1].Value = temp[temp.Length -1].ToString ();
                    dgvLegendList[2, dgvLegendList.Rows.Count - 1].Value = Image.FromFile(filelist[i].ToString ());
                    dgvLegendList[3, dgvLegendList.Rows.Count - 1].Value = filelist[i].ToString();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //检查图例是否重复
            bool isHas = false;
            for (int i = 0; i < dgvLegendList.Rows.Count; i++)
            {
                if ((bool)dgvLegendList[0, i].Value == true)
                {
                    string tempname = dgvLegendList[1, i].Value.ToString().Split('.')[0].ToString();;
                    DataSet tempds = TheManage.ReturnDS ("select * from T_MAP_LEGEND where LegendName='"+tempname +"'");
                    if (tempds.Tables[0].Rows.Count > 0)
                    {
                        dgvLegendList[0, i].Value = false;
                    }
                }
            }
            for (int i = 0; i < dgvLegendList.Rows.Count; i++)
            {
                if ((bool)dgvLegendList[0, i].Value == true)
                {
                    tempFilePath = dgvLegendList[3, i].Value.ToString();
                    if (System.IO.File.Exists(tempFilePath))
                    {
                        ms = File.OpenRead(tempFilePath);
                        pbFilep = new byte[ms.Length];
                        ms.Position = 0;
                        ms.Read(pbFilep, 0, Convert.ToInt32(ms.Length));
                    }
                    string tempTitle = dgvLegendList[1, i].Value.ToString().Split('.')[0].ToString();
                    TheManage.OperateDB ("insert into T_MAP_LEGEND (LegendName,LegendPic) values ('" + tempTitle + "',@LegendPic)", pbFilep, "@LegendPic");
                    
                }
            }
            MessageBox.Show("保存成功！");
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
    }
}
