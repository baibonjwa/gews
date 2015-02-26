using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using LibLoginForm;
using LibCommon;
using LibConfig;
using sys4;

namespace _4.OutburstPrevention
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.EngineOrDesktop);

            //窗体风格改变
            DXSeting.formSkinsSet();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
			
            MainForm_OP mf = new MainForm_OP();
            LoginForm lf = new LoginForm(mf);
            Application.Run(lf);
        }
    }
}
