// ******************************************************************
// 概  述：
// 作  者：
// 创建日期：
// 版本号：V1.0
// 版本信息:
// V1.0 新建
// ******************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LibCommonControl
{
    public partial class TextBoxMustFill : UserControl
    {
        public TextBoxMustFill()
        {
            InitializeComponent();
        }

        public override string Text
        {
            get
            {
                return _txtBox.Text;
            }
            set
            {
                _txtBox.Text = value;
            }
        }
    }
}
