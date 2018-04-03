using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Threading;
using FilesContract.Demo;
using System.Configuration;

namespace FilesService.Demo
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class FilesService : FilesContract.Demo.IFilesService
    {
        public static List<CusFileInfo> files = new List<CusFileInfo>();

       
        #region IFilesService 成员
        public Stream DownloadFileStream(string serverfile)
        {
            CusFileInfo file = new CusFileInfo();
            file.filepath = ConfigurationManager.AppSettings["ServerDownLoadURL"] + serverfile;

            if (File.Exists(file.filepath))
            {
                var incomingRequest = WebOperationContext.Current.IncomingRequest;
                var outgoingResponse = WebOperationContext.Current.OutgoingResponse;
                long offset = 0, count = file.FileLength;
                if (incomingRequest.Headers.AllKeys.Contains("Range"))
                {
                    var match = System.Text.RegularExpressions.Regex.Match(incomingRequest.Headers["Range"], @"(?<=bytes\b*=)(\d*)-(\d*)");
                    if (match.Success)
                    {
                        outgoingResponse.StatusCode = System.Net.HttpStatusCode.PartialContent;
                        string v1 = match.Groups[1].Value;
                        string v2 = match.Groups[2].Value;
                        if (!match.NextMatch().Success)
                        {
                            if (v1 == "" && v2 != "")
                            {
                                var r2 = long.Parse(v2);
                                offset = count - r2;
                                count = r2;
                            }
                            else if (v1 != "" && v2 == "")
                            {
                                var r1 = long.Parse(v1);
                                offset = r1;
                                count -= r1;
                            }
                            else if (v1 != "" && v2 != "")
                            {
                                var r1 = long.Parse(v1);
                                var r2 = long.Parse(v2);
                                offset = r1;
                                count -= r2 - r1 + 1;
                            }
                            else
                            {
                                outgoingResponse.StatusCode = System.Net.HttpStatusCode.OK;
                            }
                        }
                    }

                }
                outgoingResponse.ContentType = "application/force-download";
                outgoingResponse.ContentLength = count;

                Console.WriteLine("开始下载：" + serverfile + "(" + offset + "--" + count + ")");
                CusStreamReader fs = new CusStreamReader(new FileStream(file.filepath, FileMode.Open, FileAccess.Read, FileShare.Read), offset, count);
                //fs.Reading += (t) =>
                //    {
                //        //限速代码,实际使用时可以去掉，或者精确控制
                //        Thread.Sleep(300);
                //        Console.WriteLine(t);
                //    };
                return fs;
            }
            else
            {
                throw new FaultException("没找到文件：" + serverfile);
            }

        }

        public Stream DownloadFileStream(string serverfile, long offset = 0, long count = 0)
        {
            CusFileInfo file = new CusFileInfo();
            file.filepath = ConfigurationManager.AppSettings["ServerDownLoadURL"] + serverfile;
            file.FileName = serverfile;

            //string[] strlist1 = Directory.GetFiles(ConfigurationManager.AppSettings["ServerDownLoadURL"]);
            FileInfo ff = new FileInfo(file.filepath);

            if (File.Exists(file.filepath))
            {
                if (count == 0)
                {
                    count = ff.Length - offset;
                }
                var outgoingResponse = WebOperationContext.Current.OutgoingResponse;
                outgoingResponse.ContentType = "application/force-download";

                Console.WriteLine("开始下载：" + serverfile + "(" + offset + "--" + count + ")");
                CusStreamReader fs = new CusStreamReader(new FileStream(file.filepath, FileMode.Open, FileAccess.Read, FileShare.Read), offset, count);
                fs.Reading += (t) =>
                {
                    //限速代码,实际使用时可以去掉，或者精确控制
                    Thread.Sleep(10);
                    Console.WriteLine(t);
                };

                return fs;
            }
            else
            {
                throw new FaultException("没找到文件：" + serverfile);
            }

        }

        public List<CusFileInfo> GetFiles()
        {
            return files;
        }

        public string GetFile(string serverfile)
        {
            string strMD5 = "|MD5";
            if (serverfile.EndsWith(strMD5))
                return GetMD5HashFromFile(serverfile.Remove(serverfile.IndexOf(strMD5)));

            string path = ConfigurationManager.AppSettings["ServerDownLoadURL"] + serverfile;
            if (Directory.Exists(path))
            {
                string result = "";
                //获取目录内文件名
                DirectoryInfo directoryInfo = new DirectoryInfo(path);
                FileInfo[] fileList = directoryInfo.GetFiles();
                foreach (FileInfo file in fileList)
                {
                    result = result + @"|" + file.Name;
                }
                if (result.StartsWith(@"|"))
                {
                    result = result.Substring(1);
                }
                return result;
            }
            else
            {
                //获取文件长度
                CusFileInfo file = new CusFileInfo();
                file.filepath = ConfigurationManager.AppSettings["ServerDownLoadURL"] + serverfile;
                file.FileName = serverfile;

                FileInfo ff = new FileInfo(file.filepath);
                if (File.Exists(file.filepath))
                {
                    return ff.Length.ToString();
                }
                else
                {
                    throw new FaultException("没找到文件：" + serverfile);
                }
            }
        }

        public string GetMD5HashFromFile(string serverfile)
        {
            string filePath= ConfigurationManager.AppSettings["ServerDownLoadURL"] + serverfile;
            if (!File.Exists(filePath))
            {
                throw new FaultException("没找到文件：" + serverfile);
            }
           
            try
            {
                FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(file);
                file.Close();

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("GetMD5HashFromFile() fail, error:" + ex.Message);
            }
        }
        #endregion
    }
}
