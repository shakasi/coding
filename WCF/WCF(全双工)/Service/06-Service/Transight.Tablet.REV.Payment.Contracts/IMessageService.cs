using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Transight.HQV4.HQService.Contracts
{

    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(ICallBackServices))]
    public interface IMessageService
    {
        /// <summary>
        /// 客户端注册上线
        /// </summary>
        /// <param name="message"></param>
        [OperationContract(IsOneWay = true)]
        void Register(string data);

        /// <summary>
        /// 客户端发送消息
        /// </summary>
        /// <param name="message">消息内容</param>
        [OperationContract(IsOneWay = true)]
        void ClientSendMessage(string message);
    }
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
