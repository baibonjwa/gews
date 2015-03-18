using System;
using System.Reflection;
using System.Windows.Forms;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.ActiveRecord.Framework.Config;
using DevExpress.XtraBars;
using LibLoginForm;

namespace sys1
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


            var mf = new MainFormGe(new BarButtonItem());
            var lf = new LoginForm(mf);
            Application.Run(lf);
        }
    }
}
