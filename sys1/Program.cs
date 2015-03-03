using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using AutoUpdaterDotNET;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.ActiveRecord.Framework.Config;
using DevExpress.XtraBars;
using LibLoginForm;
using LibCommonForm;
using LibCommon;
using sys1;

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
            System.Threading.Thread.CurrentThread.CurrentUICulture =
                new System.Globalization.CultureInfo("zh-Hans");

            // The following line provides localization for data formats. 
            System.Threading.Thread.CurrentThread.CurrentCulture =
                new System.Globalization.CultureInfo("zh-Hans");

            IConfigurationSource config = new XmlConfigurationSource("ARConfig.xml");

            Assembly asm = Assembly.Load("LibEntity");

            ActiveRecordStarter.Initialize(asm, config);

            //AutoUpdater.Start("http://rbsoft.org/updates/right-click-enhancer.xml");

            MainFormGe mf = new MainFormGe(new BarButtonItem());
            LoginForm lf = new LoginForm(mf);
            Application.Run(lf);
        }
    }
}
