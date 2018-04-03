using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace NetTest
{
    public class FTPTest : ITest
    {
        string Url;
        string UserName;
        string Password;
        string CommandText;

        public FTPTest(string p_Url, string p_UserName, string p_Password, string p_CommandText)
        {
            Url = p_Url;
            UserName = p_UserName;
            Password = p_Password;
            CommandText = p_CommandText;
        }

        public bool Test()
        {
            try
            {
                System.Net.FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(Url);
                //request.Method = CommandText; WebRequestMethods.Ftp.DeleteFile;
                request.Method = WebRequestMethods.Ftp.ListDirectory;
                request.Credentials = new NetworkCredential(UserName, Password);
                using (System.Net.FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {

                    Console.WriteLine(response.BannerMessage.ToString());
                    Console.WriteLine(response.StatusCode.ToString());
                    Console.WriteLine(response.WelcomeMessage.ToString());
                    return true;
                    //if (response.StatusCode == FtpStatusCode.CommandOK || response.StatusCode == FtpStatusCode.FileActionOK || response.StatusCode == FtpStatusCode.OpeningData)
                    //{

                    //    return true;
                    //}
                    //else
                    //{

                    //    return false;
                    //}

                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}