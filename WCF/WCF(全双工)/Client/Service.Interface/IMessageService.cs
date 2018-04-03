using System.ServiceModel;

namespace Artech.WcfServices.Service.Interface
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
}
