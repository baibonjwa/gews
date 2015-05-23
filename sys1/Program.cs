using System;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.ActiveRecord.Framework.Config;
using DevExpress.XtraBars;
using LibConfig;
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
            var mf = new MainFormGe(new BarButtonItem());
            var lf = new SelectCoalSeam(mf, "LoginBackground1.bmp");
            Application.Run(lf);
        }
    }
}