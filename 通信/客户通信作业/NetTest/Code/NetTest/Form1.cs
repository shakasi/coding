using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Threading;
using log4net;

using System.Net;
using SnmpSharpNet;//add 0318
namespace NetTest
{
    public partial class Form1 : Form
    {
        //日志
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public Form1()
        {
            InitializeComponent();
            _log.Info(true);
        }

        /// <summary>
        /// 协议测试开始
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            uint threadNumber = 1;
            #region FTP测试

            #endregion

            #region Telnet测试
            if (chkTelnetEnable.Checked)
            {
                //线程数，默认：1
                if (nupdTelnetTDNumber.Value > 1)
                {
                    threadNumber = Convert.ToUInt32(nupdTelnetTDNumber.Value);
                }
                //CreateThread(TelnetTest, threadNumber, nupdTelnetRunTime.Value);
            }
            #endregion
        }

        /// <summary>
        /// Telnet测试
        /// </summary>
        /// <param name="telnetTime">执行时间</param>
        private void TelnetTest(object telnetTime)
        {
            double timeAdd = Convert.ToDouble(telnetTime);
            DateTime dtNow = DateTime.Now;
            DateTime dtOver = dtNow.AddSeconds(timeAdd);
            while (timeAdd == 0 || dtNow <= dtOver)
            {
                /*
                 * TelnetTest
                 * 1、关防火墙
                 * 2、开服务Telnet、Secondary Logon
                 * 3、用户加入组TelnetClients
                 * 4、win7的“打开或关闭WINDOWS功能”中开启TELNET
                 */
                string ip = tbAddress.Text.Trim();
                string userID = tbUserName.Text.Trim();
                string passWord = tbPWD.Text.Trim();
                try
                {
                    Telnet p = new Telnet(ip, 23, 50);

                    if (p.Connect() == false)
                    {
                        string msg = "telnet链接失败";
                        _log.Info(msg);
                        return;
                    }
                    else
                    {
                        string msg = "telnet链接成功";
                        _log.Info(msg);
                    }
                    //等待指定字符返回后才执行下一命令  
                    p.WaitFor("login:");
                    p.Send(userID);
                    p.WaitFor("password:");
                    p.Send(passWord);
                    p.WaitFor(">");
                    _log.Info(p.WorkingData);
                }
                catch (Exception ex)
                {
                    _log.Error(ex.Message);
                }
                dtNow = DateTime.Now;
            }
        }

        private void PingTest(object telnetTime)
        {
            PingTest pt = new PingTest(tbAddress.Text, "ping");
            pt.Test();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FTPTest ft = new FTPTest("ftp://" + tbAddress.Text, "admin", "12345678!", "ls");
            ft.Test();
        }

        private void button8_Click(object sender, EventArgs e)
        {  
            //PingTest(null);
            //CreateThread(PingTest,Convert.ToUInt32(nupdPingTDNumber.Value), nupdPingRunTime.Value);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            SshTest ssht = new SshTest(tbAddress.Text, "admin", "12345678!", "dbg lwip", null);
            ssht.Test();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Telnet tlt = new Telnet(tbAddress.Text,23,500);
            tlt.Connect();

        }

        private void button11_Click(object sender, EventArgs e)
        {
            //SnmpTest snt = new SnmpTest(tbAddress.Text, "public", ".1.3.6.1.4.1.38446");
            //string[] t=  {".1.3.6.1.4.1.38446"};
            //snt.Test();

            


            OctetString community = new OctetString("public");
            AgentParameters param = new AgentParameters(community);
            param.Version = SnmpVersion.Ver2;
            IpAddress agent = new IpAddress("192.168.1.120");
            UdpTarget target = new UdpTarget((IPAddress)agent, 161, 2000, 1);

            Pdu pdu2 = new Pdu(PduType.GetBulk);
            pdu2.VbList.Add(".1.3.6.1.4.1.38446.1.1.2.1.13");

            SnmpV2Packet result2 = (SnmpV2Packet)target.Request(pdu2, param);

            int i=result2.Pdu.VbCount;
            
            string str= SnmpConstants.GetTypeName(result2.Pdu.VbList[0].Value.Type);
            string str2 = result2.Pdu.VbList[0].Value.ToString();
            return;











            // Define Oid that is the root of the MIB
            //  tree you wish to retrieve
            Oid rootOid = new Oid(".1.3.6.1.4.1.38446.1.5.9.1.9"); // ifDescr

            // This Oid represents last Oid returned by
            //  the SNMP agent
            Oid lastOid = (Oid)rootOid.Clone();

            // Pdu class used for all requests
            Pdu pdu = new Pdu(PduType.GetBulk);

            // In this example, set NonRepeaters value to 0
            pdu.NonRepeaters = 0;
            // MaxRepetitions tells the agent how many Oid/Value pairs to return
            // in the response.
            pdu.MaxRepetitions = 5;

            // Loop through results
            while (lastOid != null)
            {
                // When Pdu class is first constructed, RequestId is set to 0
                // and during encoding id will be set to the random value
                // for subsequent requests, id will be set to a value that
                // needs to be incremented to have unique request ids for each
                // packet
                if (pdu.RequestId != 0)
                {
                    pdu.RequestId += 1;
                }
                // Clear Oids from the Pdu class.
                pdu.VbList.Clear();
                // Initialize request PDU with the last retrieved Oid
                pdu.VbList.Add(lastOid);
                // Make SNMP request
                SnmpV2Packet result = (SnmpV2Packet)target.Request(pdu, param);
                // You should catch exceptions in the Request if using in real application.

                // If result is null then agent didn't reply or we couldn't parse the reply.
                if (result != null)
                {
                    // ErrorStatus other then 0 is an error returned by 
                    // the Agent - see SnmpConstants for error definitions
                    if (result.Pdu.ErrorStatus != 0)
                    {
                        // agent reported an error with the request
                        Console.WriteLine("Error in SNMP reply. Error {0} index {1}",
                            result.Pdu.ErrorStatus,
                            result.Pdu.ErrorIndex);
                        lastOid = null;
                        break;
                    }
                    else
                    {
                        // Walk through returned variable bindings
                        foreach (Vb v in result.Pdu.VbList)
                        {
                            // Check that retrieved Oid is "child" of the root OID
                            if (rootOid.IsRootOf(v.Oid))
                            {
                                Console.WriteLine("{0} ({1}): {2}",
                                    v.Oid.ToString(),
                                    SnmpConstants.GetTypeName(v.Value.Type),
                                    v.Value.ToString());
                                lastOid = v.Oid;
                            }
                            else
                            {
                                // we have reached the end of the requested
                                // MIB tree. Set lastOid to null and exit loop
                                lastOid = null;
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("No response received from SNMP agent.");
                }
            }
            target.Dispose();
        }

        private void btnHttpTest_Click(object sender, EventArgs e)
        {
            HttpTest httpTest = new HttpTest(@"http://192.168.1.103/xhrlogin.jsp", "admin", "12345678!",
                "http://192.168.1.103/login.htm");
            bool httpResult = false;
            try
            {
                httpResult = httpTest.Test();
            }
            catch (Exception ex)
            {
                _log.Info(httpResult, ex);
            }
            _log.Info(httpResult);
        }
    }
}