using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Reflection;
using log4net;


namespace Shaka.Infrastructure
{
    public class CommonHelper
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public static bool GetFiles(string foldPath,string regexStr, ref List<FileInfo> fileList)
        {
            DirectoryInfo dir = new DirectoryInfo(foldPath);
            DirectoryInfo[] dirs = dir.GetDirectories();

            foreach (FileInfo file in dir.GetFiles())
            {
                string fileName = file.Name.ToLower();
                if (Regex.IsMatch(fileName, regexStr))
                {
                    if (fileName.Contains(" "))
                    {
                        _log.Fatal(string.Format("文件：{0} 有空格", fileName));
                        return false;
                    }
                    else if (fileName.Contains("detail"))
                    {
                        if (!fileName.Contains("detail."))
                        {
                            _log.Fatal(string.Format("文件：{0} detail不在结尾", fileName));
                            return false;
                        }
                    }
                    else if (fileName.Contains("display"))
                    {
                        if (!fileName.Contains("display."))
                        {
                            _log.Fatal(string.Format("文件：{0} display不在结尾", fileName));
                            return false;
                        }
                    }
                    else if (fileName.Split('.').Length>2)
                    {
                        _log.Fatal(string.Format("文件：{0} display不在结尾", fileName));
                        return false;
                    }
                    fileList.Add(file);
                }
                else
                {
                    _log.Fatal(string.Format("文件：{0} 不符合正则{1}", file.Name, regexStr));
                    return false;
                }
            }

            if (dirs != null && dirs.Length > 0)
            {
                foreach (DirectoryInfo dirSun in dirs)
                {
                    if (!GetFiles(dirSun.FullName, regexStr, ref fileList))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
