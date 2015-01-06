using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
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
            //窗体风格

            IConfigurationSource config = new XmlConfigurationSource("ARConfig.xml");

            Assembly asm = Assembly.Load("LibEntity");

            ActiveRecordStarter.Initialize(asm, config);

            MainFormGe mf = new MainFormGe(new BarButtonItem());
            LoginForm lf = new LoginForm(mf);
            Application.Run(lf);
        }
    }
}
