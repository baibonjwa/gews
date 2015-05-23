using System;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.ActiveRecord.Framework.Config;
using ESRI.ArcGIS;
using LibCommon;
using LibLoginForm;

namespace sys3
{
    internal static class Program
    {
        /// <summary>
        ///     应用程序的主入口点。
        /// </summary>
        [STAThread]
        private static void Main()
        {
            RuntimeManager.Bind(ProductCode.EngineOrDesktop); //RuntimeManager.Bind
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var mf = new MainForm_GM();
            var lf = new SelectCoalSeam(mf, "LoginBackground3.bmp");
            Application.Run(lf);
        }
    }
}