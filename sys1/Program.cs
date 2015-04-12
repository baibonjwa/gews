using System;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.ActiveRecord.Framework.Config;
using DevExpress.XtraBars;
using LibLoginForm;

namespace sys1
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

            var mf = new MainFormGe(new BarButtonItem());
            var lf = new LoginForm(mf);
            Application.Run(lf);
        }
    }
}