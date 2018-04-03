using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Shaka.Infrastructure
{
    public class BinaryHelper
    {
        public static byte[] FileToBytes(FileInfo fileInfo)
        {
            FileStream fs = fileInfo.OpenRead();
            byte[] byteArray = new byte[fs.Length];
            fs.Read(byteArray, 0, byteArray.Length);
            fs.Flush();
            fs.Close();
            return byteArray;
        }
    }
}
