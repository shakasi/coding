using System;
using System.Net;
using System.ServiceModel;
using Artech.WcfServices.Service.Interface;

namespace Artech.WcfServices.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ICallBackServices callBack = new MessageServiceCallback();
                InstanceContext context = new InstanceContext(callBack);

                string ip = GetIP();
                ip = "180.167.31.26";
                string serverPort = "9800";
                string tcpAddress = string.Format("net.tcp://{0}:{1}/", ip, serverPort);
                EndpointAddress endpoint = new EndpointAddress(tcpAddress);
                
                using (DuplexChannelFactory<IMessageService> channelFactory = new DuplexChannelFactory<IMessageService>(context, "NetTcpBinding_IMessageService", endpoint))
                {
                    IMessageService proxy = channelFactory.CreateChannel();
                    proxy.Register("99999");
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// 获取本机IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetIP()
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
    }
}
