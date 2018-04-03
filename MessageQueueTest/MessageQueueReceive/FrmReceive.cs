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
using System.Reflection;
using log4net;

using MessageQueueUtils;

namespace MessageQueueReceive
{
    public partial class FrmReceive : Form
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public FrmReceive()
        {
            InitializeComponent();
        }

        private void FrmReceive_Load(object sender, EventArgs e)
        {

        }

        private void btnReceive_Click(object sender, EventArgs e)
        {
            MQHelper mqHelper = new MQHelper(@".\Private$\PosSyncCheckOver");
            _log.Info("消息队列接收开始");
            mqHelper.Receive<IndexJob>(MessageReceiveCallBack);
        }

        private void MessageReceiveCallBack(IndexJob job)
        {

        }
    }

    [Serializable]
    public class IndexJob
    {
        public string Id { get; set; }

        public object Obj { get; set; }
    }
}