using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Diagnostics;
using Shaka.Utils;

namespace MessageCommunicationA
{
    class Program
    {
        private static string _messageTo = "MessageCommunicationB";
        private static string _messageStr = "123";

        static void Main(string[] args)
        {
            string CurrentDirectory = System.Environment.CurrentDirectory.Replace("MessageCommunicationA", _messageTo);

            bool isStart = false;
            foreach (Process p in Process.GetProcessesByName(_messageTo))
            {
                isStart = true;
                WinMessageHelper.Send(p.MainWindowHandle.ToInt32(), _messageStr);
            }
            
            if (!isStart)
            {
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo.FileName = _messageTo + ".exe";
                process.StartInfo.WorkingDirectory = CurrentDirectory;
                process.StartInfo.CreateNoWindow = false;
                process.Start();
                process.WaitForInputIdle();
                System.Threading.Thread.Sleep(2000); //增加了延时2秒，这样就可以获取计算器的窗口句柄 

                foreach (Process p in Process.GetProcessesByName(_messageTo))
                {
                    WinMessageHelper.Send(p.MainWindowHandle.ToInt32(), _messageStr);
                }
            }
        }
    }
}