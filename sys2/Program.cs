using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using LibLoginForm;
using LibCommon;
using sys2;

namespace _2.MiningScheduling
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Log.Debug("[MS]...Starting......");
            ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.EngineOrDesktop);

            //DX皮肤
            //DXSeting.formSkinsSet();
            DXSeting.SetFormSkin(DXSkineNames.CARAMEL);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Log.Debug("[MS]...Constructing Main Form......");
            MainForm_MS mf = new MainForm_MS();
            LoginForm lf = new LoginForm(mf);
            Log.Debug("[MS]...Begin Login......");
            Application.Run(lf);
        }
    }
}
