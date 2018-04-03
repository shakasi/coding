using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace Artech.WcfServices.Service.Interface
{
    public interface ICallBackServices
    {
        /// <summary>
        /// 服务像客户端发送信息(异步)
        /// </summary>
        /// <param name="Message"></param>
        [OperationContract(IsOneWay = true)]
        void SendMessage(string message);
    }
}
