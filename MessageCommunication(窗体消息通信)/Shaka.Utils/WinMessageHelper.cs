using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Shaka.Utils
{
    /// <summary>
    /// WinForm跨进程通信
    /// </summary>
    public class WinMessageHelper
    {
        #region //初始化
        private struct COPYDATASTRUCT
        {
            public IntPtr dwData;
            public int cbData;
            [MarshalAs(UnmanagedType.LPStr)]
            public string lpData;
        }

        //使用COPYDATA进行跨进程通信
        public const int WM_COPYDATA = 0x004A;

        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        private static extern int SendMessage(
        int hWnd,
        int Msg,
        int wParam,
        ref COPYDATASTRUCT lParam);

        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        private static extern int FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", EntryPoint = "ShowWindow", SetLastError = true)]
        static extern bool ShowWindow(IntPtr hWnd, uint nCmdShow);
        #endregion

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="windowReceiveTitle">接收方窗体标题名称</param>
        /// <param name="strData">要发送的数据</param>
        public static void Send(string windowReceiveTitle, string strData)
        {
            int winHandler = FindWindow(null, windowReceiveTitle);
            if (winHandler != 0)
            {
                byte[] sarr = System.Text.Encoding.Default.GetBytes(strData);
                int len = sarr.Length + 1;
                COPYDATASTRUCT cds;
                cds.dwData = (IntPtr)100;
                cds.lpData = strData;
                cds.cbData = len;
                SendMessage(winHandler, WM_COPYDATA, 0, ref cds);
            }
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="WINDOW_HANDLER">接收方窗体的WINDOW_HANDLER</param>
        /// <param name="strData">要发送的数据</param>
        public static void Send(int WINDOW_HANDLER, string strData)
        {
            if (WINDOW_HANDLER != 0)
            {
                byte[] sarr = System.Text.Encoding.Default.GetBytes(strData);
                int len = sarr.Length;
                COPYDATASTRUCT cds;
                cds.dwData = (IntPtr)100;
                cds.lpData = strData;
                cds.cbData = len + 1;
                SendMessage(WINDOW_HANDLER, WM_COPYDATA, 0, ref cds);
            }
        }

        /// <summary>
        /// 接收的到数据
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static string Receive(ref System.Windows.Forms.Message m)
        {
            COPYDATASTRUCT cds = new COPYDATASTRUCT();
            Type cdsType = cds.GetType();
            cds = (COPYDATASTRUCT)m.GetLParam(cdsType);
            return cds.lpData;
        }

        /// <summary>
        /// 接收消息
        /// </summary>
        /// <example>
        /// 在窗体中覆盖接收消息函数
        //protected override void DefWndProc(ref System.Windows.Forms.Message m)
        //{
        //    switch (m.Msg)
        //    {
        //        case WinMessageHelper.WM_COPYDATA:
        //            string str = WinMessageHelper.Receive(ref m);
        //            break;
        //        default:
        //            base.DefWndProc(ref m);
        //            break;

        //    }
        //}
    }

    public class MainWindowHandleHelper
    {
        private bool haveMainWindow = false;
        private IntPtr mainWindowHandle = IntPtr.Zero;
        private int processId = 0;

        delegate bool EnumThreadWindowsCallback(IntPtr hWnd, IntPtr lParam);

        public IntPtr GetMainWindowHandle(int processId)
        {
            if (!this.haveMainWindow)
            {
                this.mainWindowHandle = IntPtr.Zero;
                this.processId = processId;
                EnumThreadWindowsCallback callback = new EnumThreadWindowsCallback(this.EnumWindowsCallback);
                EnumWindows(callback, IntPtr.Zero);
                GC.KeepAlive(callback);

                this.haveMainWindow = true;
            }
            return this.mainWindowHandle;
        }

        bool EnumWindowsCallback(IntPtr handle, IntPtr extraParameter)
        {
            int num;
            GetWindowThreadProcessId(new HandleRef(this, handle), out num);

            //if ((num == this.processId) && this.IsMainWindow(handle))
            //{
            //    this.mainWindowHandle = handle;
            //    return false;
            //}

            if ((num == this.processId))
            {
                this.mainWindowHandle = handle;
                return false;
            }
            return true;
        }

        bool IsMainWindow(IntPtr handle)
        {
            return (!(GetWindow(new HandleRef(this, handle), 4) != IntPtr.Zero) && IsWindowVisible(new HandleRef(this, handle)));
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern bool EnumWindows(EnumThreadWindowsCallback callback, IntPtr extraData);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern int GetWindowThreadProcessId(HandleRef handle, out int processId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        static extern IntPtr GetWindow(HandleRef hWnd, int uCmd);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern bool IsWindowVisible(HandleRef hWnd);
    }
}