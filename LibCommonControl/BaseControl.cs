using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LibCommonControl
{
    public class BaseControl : UserControl
    {
        private MainFrm mainForm;

        public MainFrm MainForm
        {
            get
            {
                return this.mainForm;
            }

            set { this.mainForm = value; }
        }
    }
}
