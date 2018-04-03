using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.DirectoryServices;
using System.Security.Principal;
using System.Runtime.InteropServices;
using System.Data;
using System.Configuration;
using System.Web;

namespace ConsoleApplication1
{
    public class IISHelper
    {
        public static void TestDirectoryEntry()
        {
            try
            {
                //string path = "IIsWebService://" + System.Environment.MachineName + "/W3SVC";
                System.Collections.ArrayList webSite = new System.Collections.ArrayList();
                DirectoryEntry iis = new DirectoryEntry("IIS://localhost/W3SVC");
                if (iis != null)
                {
                    foreach (DirectoryEntry entry in iis.Children)
                    {
                        if (string.Compare(entry.SchemaClassName, "IIsWebServer") == 0)
                        {
                            Console.WriteLine(entry.Name);
                            Console.WriteLine(entry.Properties["ServerComment"].Value);
                            Console.WriteLine(entry.Path);

                            //if (entry.Properties["ServerComment"].Value.ToString()== "REVDB")
                            {
                                //entry.Properties["LogFileDirectory"].Value= @"C:\inetpub\logs\aa";//改路径测试有效
                                //entry.Properties["LogFileDirectory"].Clear();//作用不明白
                                entry.Properties["LogExtFileFlags"][0] =1;//作用不明白，测试不是控制日志大小
                                entry.CommitChanges();
                            }
                        }
                        Console.WriteLine();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            Console.Read();
        }
    }
}