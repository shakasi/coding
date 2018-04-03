using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel;
using System.Threading;
using System.Reflection;
using log4net;
using Cuscapi.Utils;
using Transight.HQV4.HQService.JsonService;
using Transight.HQV4.HQService.Contracts;
using System.Net;
using System.ServiceModel.Description;
using static System.Windows.Forms.ListView;

namespace Transight.HQV4.HQService
{
    public partial class ConsoleFrm : Form
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private const string _systemVersion = "Version: HQService V2.00.001.000_20170216";


        private bool isServerRun = true;
        private int ServerPort = 9800;
        private static ConsoleFrm instance = null;


        public List<ClientInfo> clientList = new List<ClientInfo>();
        public Dictionary<string,ICallBackServices> ListClient = new Dictionary<string, ICallBackServices>();

        /// <summary>
        /// 获取单例
        /// </summary>
        /// <returns></returns>
        public static ConsoleFrm GetInstance()
        {
            if (instance == null)
            {
                instance = new ConsoleFrm();
            }
            return instance;
        }

        private ConsoleFrm()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            InitializeComponent();
        }

        private void ConsoleFrm_Load(object sender, EventArgs e)
        {
            this.Text = string.Concat("Transight HQV4 HQService Console - ", _systemVersion);
            //Cuscapi.Cache.CacheManager.Init();

            HostWebService();

            //消息推送服务
            HostTcpService();
        }

        private void HostTcpService()
        {
            StartServer();//开启服务
            SetClientNum();
        }

        #region //消息推送服务 设置
        /// <summary>
        /// wcf 宿主服务线程 启动wcf服务
        /// </summary>
        private void StartServer()
        {
            System.Threading.ThreadPool.QueueUserWorkItem(delegate
            {
                try
                {
                    isServerRun = true;
                    //获取本机IP
                    string ip = GetIP();
                    string tcpaddress = string.Format("net.tcp://{0}:{1}/", ip, ServerPort);
                    //定义服务主机
                    ServiceHost host = new ServiceHost(typeof(MessageService), new Uri(tcpaddress));
                    //设置netTCP协议
                    NetTcpBinding tcpBinding = new NetTcpBinding();
                    tcpBinding.MaxBufferPoolSize = 2147483647;
                    tcpBinding.MaxReceivedMessageSize = 2147483647;
                    tcpBinding.MaxBufferSize = 2147483647;
                    //提供安全传输
                    tcpBinding.Security.Mode = SecurityMode.None;
                    //需要提供证书
                    tcpBinding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;
                    //添加多个服务终结点
                    //使用指定的协定、绑定和终结点地址将服务终结点添加到承载服务中
                    //netTcp协议终结点
                    host.AddServiceEndpoint(typeof(IMessageService), tcpBinding, tcpaddress);

                    #region 添加行为
                    //元数据发布行为
                    ServiceMetadataBehavior behavior = new ServiceMetadataBehavior();
                    //支持get请求
                    behavior.HttpGetEnabled = false;

                    //设置到Host中
                    host.Description.Behaviors.Add(behavior);
                    #endregion

                    //设置数据交换类型
                    host.AddServiceEndpoint(typeof(IMetadataExchange), MetadataExchangeBindings.CreateMexTcpBinding(), "mex");
                    //启动服务
                    host.Open();
                    SetDisplayMessage(string.Format("服务启动成功,正在运行...\r\n{0}", tcpaddress));
                }
                catch (Exception ex)
                {
                    SetDisplayMessage("服务启动失败");
                    MessageBox.Show(ex.Message, "服务启动失败，请检测配置中IP地址");
                    Environment.Exit(0);
                }
            });
        }

        /// <summary>
        /// 获取本机IP地址
        /// </summary>
        /// <returns></returns>
        public string GetIP()
        {
            string name = Dns.GetHostName();
            IPHostEntry me = Dns.GetHostEntry(name);
            IPAddress[] ips = me.AddressList;
            foreach (IPAddress ip in ips)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return (ips != null && ips.Length > 0 ? ips[0] : new IPAddress(0x0)).ToString();
        }

        /// <summary>
        /// 设置运行内容
        /// </summary>
        /// <param name="msg"></param>
        public void SetDisplayMessage(string msg)
        {
            //在线程里以安全方式调用控件
            if (this.rtxtMsg.InvokeRequired)
            {
                rtxtMsg.BeginInvoke(new MethodInvoker(delegate
                {
                    SetDisplayMessage(msg);
                }));
            }
            else
            {
                if (rtxtMsg.Lines.Length >= 200)
                {
                    rtxtMsg.Text = "";
                }
                rtxtMsg.AppendText(string.Format("{0}\r\n{1}\r\n\r\n", msg, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                rtxtMsg.SelectionStart = rtxtMsg.Text.Length;
                rtxtMsg.ScrollToCaret();
            }
        }

        /// <summary>
        /// 设置在线客户端数量
        /// </summary>
        public void SetClientNum()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new MethodInvoker(delegate {
                    SetClientNum();
                }));
            }
            else
            {
                this.Text = string.Format("服务已启动，当前客户端数量{0}", ListClient.Count);
                BindListView();
            }
        }
        #endregion

        private void HostWebService()
        {
            TheadHelper.RunThread(WCFGo);
        }

        private bool WCFGo()
        {
            try
            {
                ServiceHost host = new ServiceHost(typeof(Operations));
                host.Opened += delegate
                {
                    _log.Info("Service已经启动！");
                };

                host.Open();
                return true;
            }
            catch (Exception ex)
            {
                _log.Fatal("Service启动失败！:" + ex.Message);
                throw ex;
            }
        }

        #region //广播消息
        private void WinGo(string method)
        {
            try
            {
                if (this.lvList.CheckedItems.Count > 0)
                {
                    for (int i = 0; i < this.lvList.Items.Count; i++)
                    {
                        foreach (var d in this.lvList.CheckedItems)
                        {
                            if (this.lvList.Items[i] == d)
                            {
                                string id = (this.lvList.Items[i]).SubItems[2].Text;

                                foreach (var kvp in ListClient)
                                {
                                    if (kvp.Key.Equals(id))
                                    {
                                        kvp.Value.SendMessage(method);
                                    }
                                }
                                break;
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("请选择要推送的门店？", "提示", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void btnIendl_Click(object sender, EventArgs e)
        {
            WinGo("IenDownload");
        }

        private void btnIenImport_Click(object sender, EventArgs e)
        {
            WinGo("IenImport");
        }

        private void btnUpdImport_Click(object sender, EventArgs e)
        {
            WinGo("UpdImport");
        }

        private void btnCallBack_Click(object sender, EventArgs e)
        {
            WinGo("OpIenCallBack");
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            WinGo("FilesDownload");
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            WinGo("SalesDataUpload");
        }

        private void btnAutoUpdte_Click(object sender, EventArgs e)
        {
            WinGo("ReleaseUpdate");
         
        }

        private void btnOtherUpload_Click(object sender, EventArgs e)
        {
            WinGo("DataSync");
        }

        private void btnUPBizDate_Click(object sender, EventArgs e)
        {
            WinGo("UPBizDate");
        }

        #endregion

        void BindListView()
        {
            this.lvList.Clear();
            this.lvList.View = View.Details;

            lvList.LabelEdit = true;

            lvList.AllowColumnReorder = true;
            //是否显示复选框
            lvList.CheckBoxes = true;
            //是否选中整行
            lvList.FullRowSelect = true;
            // 是否显示网格
            lvList.GridLines = true;
            // 升序还是降序
            lvList.Sorting = SortOrder.Ascending;
            this.lvList.Columns.Add(" ", 30, HorizontalAlignment.Center);
            this.lvList.Columns.Add("门店号", 60, HorizontalAlignment.Center);
            this.lvList.Columns.Add("客户端编号", 200, HorizontalAlignment.Center);
            this.lvList.Columns.Add("客户端机器名", 100, HorizontalAlignment.Center);
            this.lvList.Columns.Add("是否运行", 50, HorizontalAlignment.Center);
           
            this.lvList.Visible = true;

            int i = 0;
            foreach (ClientInfo t in clientList)
            {
                i++;
                ListViewItem li = new ListViewItem();
                //li.SubItems[0].Text = "";
                li.SubItems.Add(t.StoreNo);
                li.SubItems.Add(t.SessionID);
                li.SubItems.Add(t.ClientHostName);
                li.SubItems.Add(t.IsRun.ToString());

                if (t.IsRun)
                    this.lvList.Items.Add(li);

            }
        }
    }
}
