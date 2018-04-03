using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;

namespace ConsoleApplication6
{
    public class DownLoadFile
    {
        private string _serverfile = "http://localhost/FileDownLoadService/11.mp4?offset=0&count=0";

        public DownLoadFile(string serverfile)
        {
            if (string.IsNullOrEmpty(serverfile))
            {
                this._serverfile = serverfile;
            }
        }
        
        ///
        /// 下载文件方法
        ///
        /// 文件保存路径和文件名
        /// 返回服务器文件名
        ///
        public bool DeownloadFile(string strFileName)
        {
            bool flag = false;
            //打开上次下载的文件
            long sPosition = 0;
            //实例化流对象
            FileStream fStream;
            //判断要下载的文件夹是否存在
            if (File.Exists(strFileName))
            {
                //打开要下载的文件
                fStream = File.OpenWrite(strFileName);
                //获取已经下载的长度
                sPosition = fStream.Length;
                fStream.Seek(sPosition, SeekOrigin.Current);
            }
            else
            {
                //无文件则创建一个文件
                fStream = new FileStream(strFileName, FileMode.Create);
                sPosition = 0;
            }
            try
            {
                //打开网络连接
                HttpWebRequest myRequest = (HttpWebRequest)HttpWebRequest.Create(_serverfile);
                if (sPosition > 0)
                    myRequest.AddRange((int)sPosition);             //设置Range值
                //向服务器请求,获得服务器的回应数据流
                Stream myStream = myRequest.GetResponse().GetResponseStream();
                //定义一个字节数据
                byte[] btContent = new byte[512];
                int intSize = 0;
                intSize = myStream.Read(btContent, 0, 512);
                while (intSize > 0)
                {
                    fStream.Write(btContent, 0, intSize);
                    intSize = myStream.Read(btContent, 0, 512);
                }
                //关闭流
                fStream.Close();
                myStream.Close();
                flag = true;        //返回true下载成功
            }
            catch (Exception ex)
            {
                fStream.Close();
                flag = false;       //返回false下载失败
            }
            return flag;
        }
    }
}
