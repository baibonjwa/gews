using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;
using LibCommon;
using LibLoginForm;
using LibConfig;

namespace _5.WarningManagement
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Log.Debug("[WM]....Starting...");
            ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.EngineOrDesktop);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MainForm_WM mf = new MainForm_WM();
            Log.Debug("[WM]....Main Form Construction Finished...");
            LoginForm lf = new LoginForm(mf);
            Application.Run(lf);
        }
    }
}
