using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace UdpTest
{
    /// <summary>
    /// 注意源端口是变化的，对方发送用源端口，监听端口也在变
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            //UDP测试
            int recv;
            byte[] data = new byte[1024];


            //得到本机IP，设置TCP端口号        
            IPEndPoint ip = new IPEndPoint(IPAddress.Any, 30303);
            Socket newsock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            //绑定网络地址
            newsock.Bind(ip);

            Console.WriteLine("This is a Server, host name is {0}", Dns.GetHostName());

            //等待客户机连接
            Console.WriteLine("Waiting for a client");

            //得到客户机IP
            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
            EndPoint Remote = (EndPoint)(sender);
            recv = newsock.ReceiveFrom(data, ref Remote);
            Console.WriteLine("Message received from {0}: ", Remote.ToString());
            Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));

            //客户机连接成功后，发送信息
            string welcome = "你好 ! ";

            //字符串与字节数组相互转换
            data = Encoding.ASCII.GetBytes(welcome);

            //发送信息
            newsock.SendTo(data, data.Length, SocketFlags.None, Remote);
            while (true)
            {
                data = new byte[1024];
                //发送接受信息
                recv = newsock.ReceiveFrom(data, ref Remote);
                Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));
                newsock.SendTo(data, recv, SocketFlags.None, Remote);
            }
        }
    }
}