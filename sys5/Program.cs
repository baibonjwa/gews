using System;
using System.Reflection;
using System.Windows.Forms;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.ActiveRecord.Framework.Config;
using LibCommon;
using LibLoginForm;
using _5.WarningManagement;

namespace sys5
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture =
              new System.Globalization.CultureInfo("zh-Hans");

            // The following line provides localization for data formats. 
            System.Threading.Thread.CurrentThread.CurrentCulture =
                new System.Globalization.CultureInfo("zh-Hans");


            IConfigurationSource config = new XmlConfigurationSource("ARConfig.xml");

            Assembly asm = Assembly.Load("LibEntity");

            ActiveRecordStarter.Initialize(asm, config);
            Log.Debug("[WM]....Starting...");
            ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.EngineOrDesktop);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var mf = new MainFormWm();
            Log.Debug("[WM]....Main Form Construction Finished...");
            var lf = new LoginForm(mf);
            Application.Run(lf);
        }
    }
}
