using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FilesContract.Demo;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Windows.Forms;

namespace FilesClient.Demo
{
    public static class DataManager
    {
        #region 私有变量
        static IFilesService channel;
        static string baseAddress = "http://localhost:12251/";
        #endregion

        #region 属性
        public static string BaseAddress
        {
            get { return baseAddress; }
            set
            {
                channel = null;
                baseAddress = value;
            }
        }

        public static IFilesService Channel
        {
            get
            {
                if (channel == null)
                {
                    WebHttpBinding binding = new WebHttpBinding() { TransferMode = TransferMode.Streamed, MaxReceivedMessageSize = 2147483647 };
                    binding.Security.Mode = WebHttpSecurityMode.None;
                    EndpointAddress address = new EndpointAddress(new Uri(baseAddress));
                    var channelFactory = new ChannelFactory<IFilesService>(binding, address);
                    channelFactory.Endpoint.Behaviors.Add(new WebHttpBehavior() { DefaultBodyStyle = System.ServiceModel.Web.WebMessageBodyStyle.Wrapped, FaultExceptionEnabled = true });
                    channel = channelFactory.CreateChannel();
                }
                return channel;
            }
        }
        #endregion

        public static void SetControlText(Control col, string text)
        {
            if (col.InvokeRequired && col.IsHandleCreated)
                col.BeginInvoke(new Action<Control, string>(SetControlText), col, text);
            else
                col.Text = text;
        }
    }
}
