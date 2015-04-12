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

namespace sys5
{
    internal static class Program
    {
        /// <summary>
        ///     应用程序的主入口点。
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Thread.CurrentThread.CurrentUICulture =
                new CultureInfo("zh-Hans");

            // The following line provides localization for data formats. 
            Thread.CurrentThread.CurrentCulture =
                new CultureInfo("zh-Hans");


            IConfigurationSource config = new XmlConfigurationSource("ARConfig.xml");

            var asm = Assembly.Load("LibEntity");

            ActiveRecordStarter.Initialize(asm, config);
            Log.Debug("[WM]....Starting...");
            RuntimeManager.Bind(ProductCode.EngineOrDesktop);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var mf = new MainFormWm();
            Log.Debug("[WM]....Main Form Construction Finished...");
            var lf = new LoginForm(mf);
            Application.Run(lf);
        }
    }
}