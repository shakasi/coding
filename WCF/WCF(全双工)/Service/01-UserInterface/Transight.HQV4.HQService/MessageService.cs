using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using Transight.HQV4.HQService.Contracts;

namespace Transight.HQV4.HQService
{
    /// <summary>
    ///  实例使用Single，共享一个
    ///  并发使用Mutiple, 支持多线程访问(一定要加锁)
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class MessageService : IMessageService
    {
        /// <summary>
        /// 客户端注册
        /// </summary>
        public void Register(string data)
        {
            ICallBackServices client = OperationContext.Current.GetCallbackChannel<ICallBackServices>();

            //获取当前机器Sessionid ：适用于多个客户端在同一台机器。
            string sessionid = OperationContext.Current.SessionId;
            //获取当前机器名称: 适用于一个客户端在一台机器上。
            string ClientHostName = OperationContext.Current.Channel.RemoteAddress.Uri.Host;

            ConsoleFrm.GetInstance().SetDisplayMessage(string.Format("客户端上线:{0}\r\n{1}\r\n{2}", data, sessionid, ClientHostName));
            ConsoleFrm.GetInstance().ListClient.Add(sessionid, client);

            ClientInfo user = new ClientInfo();
            user.SessionID = sessionid;
            user.ClientHostName = ClientHostName;
            user.IsRun = true;
            user.StoreNo = data;

            ConsoleFrm.GetInstance().clientList.Add(user);

            OperationContext.Current.Channel.Closed += new EventHandler(Channel_Closed);
            ConsoleFrm.GetInstance().SetClientNum();
        }

        /// <summary>
        /// 客户端发送消息
        /// </summary>
        /// <param name="message"></param>
        public void ClientSendMessage(string message)
        {
            ICallBackServices client = OperationContext.Current.GetCallbackChannel<ICallBackServices>();

            //获取当前机器Sessionid ：适用于多个客户端在同一台机器。
            string sessionid = OperationContext.Current.SessionId;
            //获取当前机器名称: 适用于一个客户端在一台机器上。
            string ClientHostName = OperationContext.Current.Channel.RemoteAddress.Uri.Host;

            string storeno= ConsoleFrm.GetInstance().clientList.Where(item => item.SessionID.Equals(sessionid)).FirstOrDefault().StoreNo;

            ConsoleFrm.GetInstance().SetDisplayMessage(string.Format("客户端[{0}]:{1}\r\n{2}\r\n{3}", storeno, message, sessionid, ClientHostName));

            //client.SendMessage("服务已受理");
        }

        /// <summary>
        /// 客户端关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Channel_Closed(object sender, EventArgs e)
        {
            ICallBackServices client = sender as ICallBackServices;

            if (ConsoleFrm.GetInstance().ListClient != null && ConsoleFrm.GetInstance().ListClient.Count > 0)
            {
                foreach (var d in ConsoleFrm.GetInstance().ListClient)
                {
                    if (d.Value == (ICallBackServices)sender)//删除此关闭的客户端信息
                    {
                        ConsoleFrm.GetInstance().ListClient.Remove(d.Key);

                        ClientInfo cl = ConsoleFrm.GetInstance().clientList.Find(item => item.SessionID.Equals(d.Key));
                        if (cl != null)
                        {
                            cl.IsRun = false;
                        }
                        ConsoleFrm.GetInstance().SetClientNum();

                        break;
                    }
                }
            }
        }
    }

    public class ClientInfo
    {
        /// <summary>
        /// 门店号
        /// </summary>
        public string StoreNo { get; set; } 
        /// <summary>
        /// 客户端机器名
        /// </summary>
        public string ClientHostName { get; set; }
        /// <summary>
        /// 客户端SessionID
        /// </summary>
        public string SessionID { get; set; }
        /// <summary>
        /// 是否在线
        /// </summary>
        public bool IsRun { get; set; }
    }
}
