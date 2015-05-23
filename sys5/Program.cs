using System;
using System.Windows.Forms;
using ESRI.ArcGIS;
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
            RuntimeManager.Bind(ProductCode.EngineOrDesktop);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var mf = new MainFormWm();
            var lf = new SelectCoalSeam(mf, "LoginBackground5.bmp");
            Application.Run(lf);
        }
    }
}