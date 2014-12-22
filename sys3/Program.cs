using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using LibLoginForm;
using LibCommon;

namespace _3.GeologyMeasure
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Log.Debug("Starting ......");
            ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.EngineOrDesktop);//RuntimeManager.Bind
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            DevExpress.UserSkins.BonusSkins.Register();
            DevExpress.UserSkins.OfficeSkins.Register();
            //窗体风格改变
            //DXSeting.formSkinsSet();
            DXSeting.SetFormSkin(DXSkineNames.LILIAN);

            Log.Debug("[GM] ......Constructing Main Form....");
            MainForm_GM mf = new MainForm_GM();
            LoginForm lf = new LoginForm(mf);
            Log.Debug("Logging ......");
            Application.Run(lf);
        }
    }
}
