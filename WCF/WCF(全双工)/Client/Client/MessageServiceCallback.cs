using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Artech.WcfServices.Service.Interface;

namespace Artech.WcfServices.Client
{
    //public class MessageServiceCallback : WS.IMessageServiceCallback
    public class MessageServiceCallback : ICallBackServices
    {
        public void SendMessage(string message)
        {

        }
    }
}