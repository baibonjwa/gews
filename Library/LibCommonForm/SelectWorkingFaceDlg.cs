using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LibEntity;

namespace LibCommonForm
{
    public partial class SelectWorkingFaceDlg : Form
    {
        /** 存放矿井名称，水平，采区，工作面，巷道编号的数组  **/
        private int[] _intArr = new int[5];

        public int workFaceId;
        public string workFaceName;
        public WorkingfaceTypeEnum workFaceType;

        public SelectWorkingFaceDlg()
        {
            InitializeComponent();
            this.selectWorkingFaceControl1.loadMineName();
        }

        public SelectWorkingFaceDlg(params WorkingfaceTypeEnum[] workingfaceTypes)
        {
            InitializeComponent();
            SetFilterOn(workingfaceTypes);
            this.selectWorkingFaceControl1.loadMineName();
        }

        //private void SetFilterOn(WorkingfaceTypeEnum workingfaceType)
        //{
        //    this.selectWorkingFaceControl1.SetFilterOn(workingfaceType);
        //}

        private void SetFilterOn(params WorkingfaceTypeEnum[] types)
        {
            this.selectWorkingFaceControl1.SetFilterOn(types);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;

            // 巷道编号
            workFaceId = this.selectWorkingFaceControl1.IWorkingFaceId;
            workFaceName = this.selectWorkingFaceControl1.SWorkingFaceName;
            workFaceType = this.workFaceType = selectWorkingFaceControl1.workingfaceType;

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.None;
            // 关闭窗口
            this.Close();
        }


    }
}
