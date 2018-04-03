using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Threading;

namespace Shaka.BackgroundWorkerTest
{
    public class BackgroundWorkerHelper
    {
        public delegate void CallEventHandler();
        public event CallEventHandler OnCall;
        public delegate void ProgressEventHandler(int proValue);
        public event ProgressEventHandler OnProgress;
        public delegate void CallbackEventHandler(int result, string msg);
        public event CallbackEventHandler OnCallback;

        private List<BackgroundWorker> _backgroundWorkerLst;
        private ManualResetEvent _manualReset;

        public BackgroundWorkerHelper()
        {
            _backgroundWorkerLst = new List<BackgroundWorker>();
            _manualReset = new ManualResetEvent(true);
        }

        public void RunTest(int threadCount)
        {
            for (int i = 0; i < threadCount; i++)
            {
                BackgroundWorker worker = new BackgroundWorker(); 
                worker.WorkerReportsProgress = true;
                worker.WorkerSupportsCancellation = true;

                worker.DoWork += new DoWorkEventHandler(DoWork);
                worker.ProgressChanged += new ProgressChangedEventHandler(ProgressChanged);
                worker.RunWorkerCompleted += (object sender, RunWorkerCompletedEventArgs e) =>
                {
                    if (OnCallback != null)
                    {
                        string msg = string.Empty;
                        if (e.Error != null)
                        {
                            msg = "Error: " + e.Error.Message;
                            OnCallback((int)e.Result, msg);
                        }
                        else if (e.Cancelled)
                        {
                            msg = "Canceled!";
                            OnCallback(0, msg);
                        }
                        else
                        {
                            msg = "Done!";
                            OnCallback((int)e.Result, msg);
                        }
                    }
                };
                _backgroundWorkerLst.Add(worker);
                worker.RunWorkerAsync();
            }
        }

        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            OnProgress(e.ProgressPercentage);
        }

        private void DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker work = sender as BackgroundWorker;
            int i = 0;
            while (i < 5)
            {
                if (work.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    _manualReset.WaitOne();
                    Thread.Sleep(1000);
                    i++;
                    if (OnCall != null)
                    {
                        OnCall();
                    }
                    work.ReportProgress(i * 10);
                    e.Result = i;
                }
            }
        }

        public void PauseWork()
        {
            _manualReset.Reset();
        }

        public void GoOnWork()
        {
            _manualReset.Set();
        }

        public void CancelWork()
        {
            foreach (BackgroundWorker work in _backgroundWorkerLst)
            {
                if (work.WorkerSupportsCancellation)
                {
                    work.CancelAsync();
                }
            }
        }
    }
}