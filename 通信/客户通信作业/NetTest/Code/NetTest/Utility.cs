using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace NetTest
{
    public delegate void CallbackFunction(TestType type, bool result);

    public class Utility
    {
        public static void RunTest(TestType type, int ThreadCount, CallbackFunction callBack, ITest target)
        {
            for (int i = 0; i < ThreadCount; i++)
            {
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += (object sender, DoWorkEventArgs e) =>
                {
                    bool result = target.Test();
                    e.Result = result;

                };

                worker.RunWorkerCompleted += (object sender, RunWorkerCompletedEventArgs e) =>
                    {
                        if (callBack != null)
                        {
                            if (e.Error != null)
                            {
                                callBack(type, (bool)e.Result);
                            }
                            else
                            {
                                callBack(type, false);
                            }
                        }
                    };
                worker.RunWorkerAsync();
            }
        }
    }

    public enum TestType
    {
        Ftp,
        Http,
        Https,
        Ping,
        Snmp,
        Ssh,
        Telnet
    }
}