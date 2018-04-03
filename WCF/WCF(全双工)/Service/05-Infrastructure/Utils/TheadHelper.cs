using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Cuscapi.Utils
{
    public class TheadHelper
    {
        public delegate bool CallFunction();
        public delegate void CallbackFunction(bool result);
        public static void RunThread(CallFunction call, int ThreadCount = 1, CallbackFunction callBack = null)
        {
            for (int i = 0; i < ThreadCount; i++)
            {
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += (object sender, DoWorkEventArgs e) =>
                {
                    bool result = call();
                    e.Result = result;
                };

                worker.RunWorkerCompleted += (object sender, RunWorkerCompletedEventArgs e) =>
                {
                    if (callBack != null)
                    {
                        if (e.Error != null)
                        {
                            callBack((bool)e.Result);
                        }
                        else
                        {
                            callBack(false);
                        }
                    }
                };
                worker.RunWorkerAsync();
            }
        }
    }
}
