using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication6
{
    class Program
    {
        static void Main(string[] args)
        {
            string localDownloadFile = @"d:\11.mp4";

            try
            {
                DownLoadFile down = new DownLoadFile("http://localhost/FileDownLoadService/11.mp4?offset=0&count=0");
                if (down.DeownloadFile(localDownloadFile))
                {
                    Console.WriteLine("下载成功");
                }
                else
                {
                    Console.WriteLine("下载失败");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("下载失败："+ex.Message);
            }
            Console.ReadKey();
        }
    }
}
