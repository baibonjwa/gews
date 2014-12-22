using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;
using LibLoginForm;
using LibCommonForm;
using LibCommon;

namespace _1.GasEmission
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //窗体风格
            DXSeting.formSkinsSet();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
					
            MainFormGe mf = new MainFormGe(new BarButtonItem());
            LoginForm lf = new LoginForm(mf);
            Application.Run(lf);
        }
    }
}
