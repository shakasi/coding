using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

using MessageQueueUtils;

namespace MessageQueueSend
{
    public partial class FrmSend : Form
    {
        public FrmSend()
        {
            InitializeComponent();
        }

        private void FrmSend_Load(object sender, EventArgs e)
        {

        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            SendMsg();
            MessageBox.Show("发送成功！");
            this.Close();
        }

        public void SendMsg()
        {
            string label = "PosSyncCheckOver";
            MQHelper orderJob = new MQHelper(string.Format(@".\Private$\{0}", "PosSyncCheckOver"));
            MessageQueueSend.IndexJob job1 = new MessageQueueSend.IndexJob();
            job1.Obj = "11,22";
            orderJob.Send(job1);//把任务加入消息队列

            MessageQueueSend.IndexJob job2 = new MessageQueueSend.IndexJob();
            job2.Obj = "22,33";
            orderJob.Send(job2);//把任务加入消息队列
        }
    }

    [Serializable]
    public class IndexJob
    {
        public string Id { get; set; }

        public object Obj { get; set; }
    }
}
