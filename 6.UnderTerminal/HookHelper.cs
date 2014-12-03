using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

//PS：也可以通过将[HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Policies\System] 下的DisableTaskmgr项的值设为"1”来屏蔽任务管理器。
namespace UnderTerminal
{
    /// <summary>
    /// Description: Hook Helper类，可以屏蔽一些热键并屏蔽任务管理器
    /// Author: ZhangRongHua
    /// Create DateTime: 2009-6-19 20:21
    /// UpdateHistory:
    /// </summary>
    public class HookHelper
    {
        #region Delegates

        public delegate int HookProc(int nCode, Int32 wParam, IntPtr lParam);

        #endregion

        #region 变量声明

        private HookProc KeyboardHookProcedure;
        private FileStream MyFs; // 用流来屏蔽ctrl alt delete

        private const byte LLKHF_ALTDOWN = 0x20;
        private const byte VK_CAPITAL = 0x14;
        private const byte VK_ESCAPE = 0x1B;
        private const byte VK_F4 = 0x73;
        private const byte VK_LCONTROL = 0xA2;
        private const byte VK_NUMLOCK = 0x90;
        private const byte VK_RCONTROL = 0xA3;
        private const byte VK_SHIFT = 0x10;
        private const byte VK_TAB = 0x09;
        public const int WH_KEYBOARD = 13;
        private const int WH_KEYBOARD_LL = 13;
        private const int WH_MOUSE = 7;
        private const int WH_MOUSE_LL = 14;
        private const int WM_KEYDOWN = 0x100;
        private const int WM_KEYUP = 0x101;
        private const int WM_LBUTTONDBLCLK = 0x203;
        private const int WM_LBUTTONDOWN = 0x201;
        private const int WM_LBUTTONUP = 0x202;
        private const int WM_MBUTTONDBLCLK = 0x209;
        private const int WM_MBUTTONDOWN = 0x207;
        private const int WM_MBUTTONUP = 0x208;
        private const int WM_MOUSEMOVE = 0x200;
        private const int WM_MOUSEWHEEL = 0x020A;
        private const int WM_RBUTTONDBLCLK = 0x206;
        private const int WM_RBUTTONDOWN = 0x204;
        private const int WM_RBUTTONUP = 0x205;
        private const int WM_SYSKEYDOWN = 0x104;
        private const int WM_SYSKEYUP = 0x105;
        private static int hKeyboardHook = 0;

        #endregion

        #region 函数转换

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId);

        //   卸载钩子  

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern bool UnhookWindowsHookEx(int idHook);

        //   继续下一个钩子  

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int CallNextHookEx(int idHook, int nCode, Int32 wParam, IntPtr lParam);

        //   取得当前线程编号  

        [DllImport("kernel32.dll")]
        private static extern int GetCurrentThreadId();

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern short GetKeyState(int vKey);

        #endregion

        #region 方法

        /// <summary>
        /// 钩子回调函数，在这里屏蔽热键。
        /// <remark> 
        /// Author:ZhangRongHua 
        /// Create DateTime: 2009-6-19 20:19
        /// Update History:     
        ///  </remark>
        /// </summary>
        /// <param name="nCode">The n code.</param>
        /// <param name="wParam">The w param.</param>
        /// <param name="lParam">The l param.</param>
        /// <returns></returns>
        private int KeyboardHookProc(int nCode, Int32 wParam, IntPtr lParam)
        {
            KeyMSG m = (KeyMSG)Marshal.PtrToStructure(lParam, typeof(KeyMSG));

            if (((Keys)m.vkCode == Keys.LWin) || ((Keys)m.vkCode == Keys.RWin) ||
                ((m.vkCode == VK_TAB) && ((m.flags & LLKHF_ALTDOWN) != 0)) ||
                ((m.vkCode == VK_ESCAPE) && ((m.flags & LLKHF_ALTDOWN) != 0)) ||
                ((m.vkCode == VK_F4) && ((m.flags & LLKHF_ALTDOWN) != 0)) ||
                (m.vkCode == VK_ESCAPE) && ((GetKeyState(VK_LCONTROL) & 0x8000) != 0) ||
                (m.vkCode == VK_ESCAPE) && ((GetKeyState(VK_RCONTROL) & 0x8000) != 0)
                )
            {
                return 1;
            }

            return CallNextHookEx(hKeyboardHook, nCode, wParam, lParam);
        }


        /// <summary>
        /// 启动Hook，并用流屏蔽任务管理器
        /// <remark> 
        /// Author:ZhangRongHua 
        /// Create DateTime: 2009-6-19 20:20
        /// Update History:     
        ///  </remark>
        /// </summary>
        public void HookStart()
        {
            if (hKeyboardHook == 0)
            {
                //   创建HookProc实例  

                KeyboardHookProcedure = new HookProc(KeyboardHookProc);

                hKeyboardHook = SetWindowsHookEx(WH_KEYBOARD,
                                                 KeyboardHookProcedure,
                                                 Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().GetModules()[0]),
                                                 0);

                //   如果设置钩子失败  

                if (hKeyboardHook == 0)
                {
                    HookStop();

                    //throw new Exception("SetWindowsHookEx   failedeeeeeeee.");
                }

                //用二进制流的方法打开任务管理器。而且不关闭流.这样任务管理器就打开不了
                MyFs = new FileStream(Environment.ExpandEnvironmentVariables("%windir%\\system32\\taskmgr.exe"),
                                      FileMode.Open);
                byte[] MyByte = new byte[(int)MyFs.Length];
                MyFs.Write(MyByte, 0, (int)MyFs.Length);
            }
        }



        /// <summary>
        /// 卸载hook,并关闭流，取消屏蔽任务管理器。
        /// <remark> 
        /// Author:ZhangRongHua 
        /// Create DateTime: 2009-6-19 20:21
        /// Update History:     
        ///  </remark>
        /// </summary>
        public void HookStop()
        {
            bool retKeyboard = true;

            if (hKeyboardHook != 0)
            {
                retKeyboard = UnhookWindowsHookEx(hKeyboardHook);

                hKeyboardHook = 0;
            }

            if (null != MyFs)
            {
                MyFs.Close();
            }

            if (!(retKeyboard))
            {
                throw new Exception("UnhookWindowsHookEx     failedsssss.");
            }
        }

        #endregion

        #region Nested type: KeyMSG

        public struct KeyMSG
        {
            public int dwExtraInfo;
            public int flags;
            public int scanCode;

            public int time;
            public int vkCode;
        }

        #endregion
    }
}
