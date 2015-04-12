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

namespace sys2
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
            Log.Debug("Starting ......");
            RuntimeManager.Bind(ProductCode.EngineOrDesktop); //RuntimeManager.Bind
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Log.Debug("[MS]...Constructing Main Form......");
            var mf = new MainForm_MS();
            var lf = new LoginForm(mf);
            Log.Debug("[MS]...Begin Login......");
            Application.Run(lf);
        }
    }
}