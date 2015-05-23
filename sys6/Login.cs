using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using LibCommon;
using LibConfig;
using LibEntity;
using LibSocket;
using Microsoft.Win32;

namespace UnderTerminal
{
    public partial class Login : Form
    {
        public delegate int HookHandlerDelegate(int nCode, IntPtr wparam, ref KBDLLHOOKSTRUCT lparam);

        public const int WM_KEYDOWN = 0x0100;
        public const int WH_KEYBOARD_LL = 13;
        public const int WM_SYSKEYDOWN = 0x0104;
        public static ClientSocket _clientSocket;
        private HookHandlerDelegate proc;
        private readonly UserLogin[] ents;

        public Login()
        {
            doInitilization();
            InitializeComponent();
            ents = UserLogin.FindAll();
            //doSecuritySetting(false);
        }

        /// <summary>
        ///     登录成功
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        private bool LoginSuccess(string userName, string password)
        {
            //定义记录登录成功与否的值
            var isLogin = false;
            var n = ents.Length;
            for (var i = 0; i < n; i++)
            {
                //验证帐号密码是否正确
                if (ents[i].LoginName == userName && ents[i].PassWord == password)
                {
                    CurrentUser.CurLoginUserInfo = ents[i];
                    //记录最后一次登录用户
                    var sw = new StreamWriter(Application.StartupPath + "\\DefaultUser.txt", false);
                    sw.WriteLine(userName);
                    sw.Close();

                    //记住密码,登录成功，修改用户“尚未登录”为False；根据是否记住密码设定相应的值
                    var userLogin = UserLogin.FindOneByLoginName(userName);
                    userLogin.IsSavePassWord = 0;
                    userLogin.Save();
                    isLogin = true;
                    break;
                }
            }
            return isLogin;
        }

        private void btnSure_Click(object sender, EventArgs e)
        {
            var status = false;

            var userName = tbPeople.Text;
            var password = tbCode.Text;

            //验证帐号密码是否正确
            if (LoginSuccess(userName, password))
            {
                status = true;
                Hide();

                var a = new UnderMessageWindow();
                a.ShowDialog();
            }
            else
            {
                Alert.alert(Const.USER_NAME_OR_PWD_ERROR_MSG, Const.LOGIN_FAILED_TITLE, MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            tbPeople.Text = "";
            tbCode.Text = "";
        }

        /// <summary>
        /// </summary>
        /// <param name="isSecure"></param>
        private void doSecuritySetting(bool isSecure)
        {
            // 设置开机自动运行
            registerRunWhenStart(isSecure, Application.ProductName, Application.StartupPath + @"\井下瓦斯预警录入终端.exe");
            // 屏蔽热键和task manager.
            //new HookHelper().HookStart();

            //int rwl = SysHelper.FindWindow("Shell_TrayWnd", null);   ////获得任务栏句柄
            //SysHelper.ShowWindow(rwl, SW_HIDE);     //当nCmdShow=0：隐藏；=1：显示 
            //// ShowWindow(FindWindow(“Shell_TrayWnd”, null), 1);
            ////隐藏开始按钮
            //int rwl1 = SysHelper.FindWindow("Button", null);
            //SysHelper.ShowWindow(rwl1, SW_HIDE);

            Taskbar.Visible = !isSecure;

            //int desk = FindWindow(“ProgMan”, null);   //获得桌面句柄
            //ShowWindow(desk, 0);
            //int desk = FindWindow(“ProgMan”, null);
            //ShowWindow(desk, 1);

            //屏蔽Ctrl+Del+Alt
            proc = HookCallback;
            using (var curPro = Process.GetCurrentProcess())
            using (var curMod = curPro.MainModule)
            {
                SetWindowsHookExW(WH_KEYBOARD_LL, proc, GetModuleHandle(curMod.ModuleName), 0);
            }

            SysHelper.TaskmgrHide();
        }

        /// <summary>
        ///     设置程序开机启动
        ///     或取消开机启动
        /// </summary>
        /// <param name="started">设置开机启动，或者取消开机启动</param>
        /// <param name="exeName">注册表中程序的名字</param>
        /// <param name="path">开机启动的程序路径</param>
        /// <returns>开启或则停用是否成功</returns>
        public static bool registerRunWhenStart(bool started, string exeName, string path)
        {
            var key =
                Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true); //打开注册表子项

            if (key == null) //如果该项不存在的话，则创建该子项 
            {
                key = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run");
            }
            if (started)
            {
                try
                {
                    key.SetValue(exeName, path); //设置为开机启动 
                    key.Close();
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                try
                {
                    key.DeleteValue(exeName); //取消开机启动 
                    key.Close();
                }
                catch
                {
                    return false;
                }
            }

            /*
Microsoft.Win32.RegistryKey keyq = Microsoft.Win32.Registry.CurrentUser;
Microsoft.Win32.RegistryKey key1 = keyq.CreateSubKey("\\Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\Explorer");
key1.SetValue("NoClose", 1); // 屏蔽“关闭系统”。
key1.SetValue("NoFind", 1); // 屏蔽“查找”。
key1.SetValue("NoRun", 1); // 屏蔽“运行”。

key1.SetValue("NoSetFolders", 1); // 屏蔽“设置”菜单中的“控制面板”和“打印机”。
key1.SetValue("NoLogOff", 1); // 屏蔽“注销”。
key1.SetValue("NoSetTaskBar", 1); // 屏蔽“设置”菜单中的“任务栏和开始菜单”。
key1.SetValue("NoFolderOptions", 1); // 屏蔽“开始”菜单>>设置>>文件夹选项
key1.SetValue("NoRecentDocsMenu", 1); // 屏蔽“开始”菜单>>文档
key1.SetValue("NoFavoritesMenu", 1); // 屏蔽“开始”菜单>>收藏夹
key1.SetValue("NoStartMenuSubFolders", 1); // 屏蔽“开始”菜单最上面的文件夹
key1.SetValue("NoChangeStartMenu", 1); // 禁止对“开始”菜单重新排序
key1.Close();
* */

            return true;
        }

        /// <summary>
        ///     设置开机自动运行
        /// </summary>
        /// <param name="executablePath">Application.ExecutablePath</param>
        /// <param name="isAutoRun">是否开机自动运行</param>
        public static void SetAutoRun(string executablePath, bool isAutoRun)
        {
            RegistryKey reg = null;
            try
            {
                var name = executablePath.Substring(executablePath.LastIndexOf(@"\") + 1);
                var subkey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
                reg = Registry.LocalMachine.OpenSubKey(subkey, true);
                if (reg == null)
                    reg = Registry.LocalMachine.CreateSubKey(subkey);
                if (isAutoRun)
                    reg.SetValue(name, executablePath);
                else
                    reg.SetValue(name, false);
            }
            catch
            {
            }
            finally
            {
                if (reg != null)
                    reg.Close();
            }
        }

        /// <summary>
        ///     判断应用程序是否开机自动运行
        /// </summary>
        /// <param name="executablePath"></param>
        /// <returns></returns>
        public static bool IsAppAutoRun(string executablePath)
        {
            var ret = false;
            RegistryKey reg = null;
            try
            {
                if (!File.Exists(executablePath))
                    throw new Exception("该文件不存在!");
                var name = executablePath.Substring(executablePath.LastIndexOf(@"\") + 1);
                var subkey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
                reg = Registry.LocalMachine.OpenSubKey(subkey, true);
                if (reg == null)
                {
                    ret = false;
                }
                else
                {
                    var strValue = reg.GetValue(name).ToString();
                    if (strValue == executablePath)
                    {
                        ret = true;
                    }
                    else
                    {
                        ret = false;
                    }
                }
            }
            catch
            {
                ret = false;
            }
            finally
            {
                if (reg != null)
                    reg.Close();
            }
            return ret;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr SetWindowsHookExW(int idHook, HookHandlerDelegate lpfn, IntPtr hmod, uint dwThreadID);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetModuleHandle(String modulename);

        private int HookCallback(int nCode, IntPtr wparam, ref KBDLLHOOKSTRUCT lparam)
        {
            if (nCode >= 0 && (wparam == (IntPtr) WM_KEYDOWN || wparam == (IntPtr) WM_SYSKEYDOWN))
            {
                if (lparam.vkCode == 91 || lparam.vkCode == 164 || lparam.vkCode == 9 || lparam.vkCode == 115 ||
                    lparam.vkCode == 92 || lparam.vkCode == 27)
                {
                    // 91: RWin   92:LWin  9:Tab   115:F4  27:Esc
                    return 1;
                }
                return 0;
            }
            return 0;
        }

        public void doInitilization()
        {
            // Initialize configuration manager.
            var cfgMgr = ConfigManager.Instance;
            cfgMgr.load(Application.StartupPath);
            var msg = cfgMgr.init();

            if (msg != string.Empty)
            {
                MessageBox.Show(msg);
                Application.Exit();
            }

            //初始化客户端Socket
            InitClientSocket();
        }

        public void InitClientSocket()
        {
            var serverIp = ConfigManager.Instance.getValueByKey(ConfigConst.CONFIG_SERVER_IP);
            var port = int.Parse(ConfigManager.Instance.getValueByKey(ConfigConst.CONFIG_PORT));

            //初始化客户端Socket，连接服务器
            var errorMsg = SocketHelper.InitClientSocket(serverIp, port, out _clientSocket);
            if (errorMsg != "")
            {
                Alert.alert(Const.CONNECT_SOCKET_ERROR, Const.NOTES, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Log.Error(errorMsg);
            }
            else
            {
                //连接服务器成功
                Log.Info(Const.LOG_MSG_CONNECT_SUCCEED);
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
        }

        public struct KBDLLHOOKSTRUCT
        {
            public int dwExtraInfo;
            public int flags;
            public int scanCode;
            public int time;
            public int vkCode;
        }
    }
}