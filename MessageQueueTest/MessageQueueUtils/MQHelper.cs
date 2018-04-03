using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Messaging;
using System.Threading;
using System.Reflection;
using log4net;

namespace MessageQueueUtils
{
    /// <summary>
    /// 该类实现从消息对列中发送和接收消息的主要功能 
    /// </summary>
    public class MQHelper : IDisposable
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly object _lockHelper = new object();

        //private static readonly string _queuePath = @".\Private$\PosSyncQueue";
        //private static int _queueTimeout = 20;

        //指定消息队列事务的类型，Automatic 枚举值允许发送外部事务和从处部事务接收
        protected MessageQueueTransactionType _transactionType = MessageQueueTransactionType.Single;
        protected MessageQueue _queue = null;
        protected TimeSpan _timeout;

        //实现构造函数
        public MQHelper(string queuePath)
        {
            Createqueue(queuePath);
            _queue = new MessageQueue(queuePath);
            _queue.Formatter = new XmlMessageFormatter();

            //设置当应用程序向消息对列发送消息时默认情况下使用的消息属性值
            _queue.DefaultPropertiesToSend.AttachSenderId = false;
            _queue.DefaultPropertiesToSend.UseAuthentication = false;
            _queue.DefaultPropertiesToSend.UseEncryption = false;
            _queue.DefaultPropertiesToSend.AcknowledgeType = AcknowledgeTypes.None;
            _queue.DefaultPropertiesToSend.UseJournalQueue = false;
        }

        /// <summary>
        /// 继承类将从自身的Receive方法中调用以下方法，该方法用于实现消息接收
        /// </summary>
        public virtual void Receive<T>(Action<T> action) where T : class
        {
            Thread td = new Thread(new ThreadStart(
                () =>
                {
                    lock (_lockHelper)
                    {
                        while (true)
                        {
                            try
                            {
                                var myEnumerator = _queue.GetMessageEnumerator2();
                                if (myEnumerator.MoveNext())
                                {
                                    var message = myEnumerator.Current;
                                    message.Formatter = new XmlMessageFormatter(new Type[] { typeof(T) });
                                    T t = message.Body as T;
                                    //to do
                                    action(t);

                                    myEnumerator.RemoveCurrent();
                                }
                            }
                            catch (MessageQueueException mqex)
                            {
                                if (mqex.MessageQueueErrorCode == MessageQueueErrorCode.IOTimeout)
                                {
                                    _log.Fatal(string.Format("MessageQueue接收超时：{0}", mqex.Message));
                                    //throw new TimeoutException();
                                }
                                else
                                {
                                    _log.Fatal(string.Format("MessageQueue接收错误：{0}", mqex.Message));
                                    //throw mqex;
                                }
                            }
                            Thread.Sleep(1000);
                        }
                    }
                }
                ));
            td.IsBackground = true;
            td.Start();
        }

        /// <summary>
        /// 继承类将从自身的Send方法中调用以下方法，该方法用于实现消息发送
        /// </summary>
        public virtual void Send(object msg, string label = "")
        {
            _queue.Send(msg, label, _transactionType);
        }

        /// <summary>
        /// 通过Create方法创建使用指定路径的新消息队列
        /// </summary>
        /// <param name="queuePath"></param>
        public static void Createqueue(string queuePath)
        {
            try
            {
                if (!MessageQueue.Exists(queuePath))
                {
                    MessageQueue.Create(queuePath, true);  //创建事务性的专用消息队列
                    //logger.Debug("创建队列成功！");
                }
            }
            catch (MessageQueueException ex)
            {
                throw ex;
            }
        }

        #region 实现 IDisposable 接口成员
        public void Dispose()
        {
            _queue.Dispose();
        }
        #endregion
    }
}