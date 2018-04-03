using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace ESTM.Utility
{
    /// <summary>
    /// 服务实例化工厂
    /// 客户端调用，服务端不适用
    /// </summary>
    /// <typeparam name="IService"></typeparam>
    public static class WCFClientFactory
    {
        #region WCF Client

        /// <summary>
        /// 创建WCF服务对象
        /// </summary>
        /// <param name="serviceAddress">服务地址</param>
        /// <returns>服务对象</returns>
        public static IService CreateWCFServiceClient<IService>(string strUrl)
            where IService : class
        {
            //string serviceAddress = ConfigurationManagerHelper.GetInstance().GetAddressByKey(typeof(IService));
            //if (string.IsNullOrEmpty(serviceAddress))
            //    return null;
            try
            {
                string account = GetClientAddress();
                AddressHeader accountHeader = AddressHeader.CreateAddressHeader("ClientAddress", "Address", account);
                var endpointAddr = new EndpointAddress(new Uri(strUrl), accountHeader);

                BasicHttpBinding binding = new BasicHttpBinding();
                binding.MaxBufferSize = 2147483647;
                binding.MaxReceivedMessageSize = 2147483647;
                binding.Security.Mode = BasicHttpSecurityMode.None;
                binding.ReceiveTimeout = new TimeSpan(0, 25, 0);
                binding.OpenTimeout = new TimeSpan(0, 25, 0);
                binding.SendTimeout = new TimeSpan(0, 25, 0);
                binding.CloseTimeout = new TimeSpan(0, 25, 0);

                ChannelFactory<IService> factory = new ChannelFactory<IService>(binding);
                IService service = factory.CreateChannel(endpointAddr);
                return service;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取客户端相关信息
        /// </summary>
        /// <returns></returns>
        private static string GetClientAddress()
        {
            string result = "";
            IPAddress[] arrIPAddresses = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress ip in arrIPAddresses)
            {
                if (ip.AddressFamily.Equals(System.Net.Sockets.AddressFamily.InterNetwork))
                {
                    result = System.Environment.UserDomainName + "," + System.Environment.UserDomainName + "," + System.Environment.UserName + "," + ip.ToString();
                }
            }
            return result;
        }

        /// <summary>
        /// 创建WCF服务对象(双工通讯)
        /// </summary>
        /// <param name="instanceContext">双工返回上下文</param>
        /// <returns></returns>
        public static IService CreateWCFServiceClient<IService>(InstanceContext instanceContext)
            where IService : class
        {
            string serviceAddress = ConfigurationManagerHelper.GetInstance().GetAddressByKey(typeof(IService));
            if (string.IsNullOrEmpty(serviceAddress))
                return null;
            try
            {
                string account = GetClientAddress();
                AddressHeader accountHeader = AddressHeader.CreateAddressHeader("ClientAddress", "Address", account);
                var endpointAddr = new EndpointAddress(new Uri(serviceAddress));

                NetTcpBinding binding = new NetTcpBinding();
                binding.MaxBufferSize = 2147483647;
                binding.MaxReceivedMessageSize = 2147483647;
                binding.Security.Mode = SecurityMode.None;
                binding.ReceiveTimeout = new TimeSpan(0, 25, 0);
                binding.OpenTimeout = new TimeSpan(0, 25, 0);
                binding.SendTimeout = new TimeSpan(0, 25, 0);
                binding.CloseTimeout = new TimeSpan(0, 25, 0);

                //var ctor = typeof(TServiceClient).GetConstructor(new Type[] { typeof(InstanceContext), typeof(System.ServiceModel.Channels.Binding), typeof(EndpointAddress) });
                //if (ctor == null)
                //    return null;
                //return (TServiceClient)ctor.Invoke(new object[] { instanceContext, binding, endpointAddr });

                DuplexChannelFactory<IService> factory = new DuplexChannelFactory<IService>(instanceContext, binding);
                IService service = factory.CreateChannel(endpointAddr);
                return service;
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion
    }
}
