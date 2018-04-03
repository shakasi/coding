using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace Shaka.Infrastructure
{
    public class CommonHelper
    {
        public static void GetFiles(string foldPath,string regexStr, ref List<FileInfo> fileList)
        {
            DirectoryInfo dir = new DirectoryInfo(foldPath);
            DirectoryInfo[] dirs = dir.GetDirectories();

            foreach (FileInfo file in dir.GetFiles())
            {
                if(Regex.IsMatch(file.Name.ToLower(),regexStr))
                {
                    fileList.Add(file);
                }
            }

            if (dirs != null && dirs.Length > 0)
            {
                foreach (DirectoryInfo dirSun in dirs)
                {
                    GetFiles(dirSun.FullName, regexStr, ref fileList);
                }
            }
        }
    }
}
