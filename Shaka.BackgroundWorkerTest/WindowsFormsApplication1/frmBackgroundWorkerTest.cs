using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Shaka.BackgroundWorkerTest
{
    public partial class frmBackgroundWorkerTest : Form
    {
        private BackgroundWorkerHelper _workHelper;

        public frmBackgroundWorkerTest()
        {
            InitializeComponent();
            progressBar1.Maximum = 50;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //CPU 的核数,应该是最佳线程数
            nudThreadCount.Value = Environment.ProcessorCount;

            _workHelper = new BackgroundWorkerHelper();
            _workHelper.OnCall += TestCall;
            _workHelper.OnProgress += TestProgress;
            _workHelper.OnCallback += TestResultCallBack;
        }

        private void btnGo_Click(object sender, EventArgs e)
        {

            _workHelper.RunTest(Convert.ToInt32(nudThreadCount.Value));
        }

        private void TestCall()
        {

        }

        List<int> aaList = new List<int>();
        private void TestProgress(int proValue)
        {
            aaList.Add(proValue);
            progressBar1.Value = proValue;
        }

        private void TestResultCallBack(int result, string msg)
        {
            this.nudResult.Value = result;
            this.lblResult.Text = msg;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _workHelper.CancelWork();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            _workHelper.PauseWork();
        }

        private void btnGoOn_Click(object sender, EventArgs e)
        {
            _workHelper.GoOnWork();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}