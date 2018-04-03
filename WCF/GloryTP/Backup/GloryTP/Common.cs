using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace GloryTP
{
    public class Common
    {
        /// <summary>
        /// 获取请求返回的数据
        /// </summary>
        /// <param name="myResponse"></param>
        public static string GetResponseToStr(HttpWebRequest myRequest, string paramsStr, X509Certificate[] x509CertificateArray)
        {
            string responseString = string.Empty;

            byte[] paramsBytes = Encoding.UTF8.GetBytes(paramsStr);
            int paramsLenth = paramsBytes.Length;
            myRequest.ContentLength = paramsLenth;

            myRequest.ClientCertificates.AddRange(x509CertificateArray);
            myRequest.Credentials = new NetworkCredential("sijsh", "Sky456852");
            //myRequest.Credentials = new NetworkCredential("abcd", "abcd123456");

            using (Stream requestStream = myRequest.GetRequestStream())
            {
                requestStream.Write(paramsBytes, 0, paramsLenth);
            }

            try
            {
                using (HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse())
                {
                    if (myResponse.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        Stream resStream = myResponse.GetResponseStream();
                        StreamReader sr = new StreamReader(resStream, Encoding.UTF8);
                        responseString = sr.ReadToEnd();
                        sr.Close();
                        resStream.Close();
                    }
                    else
                    {
                        responseString = "连接错误";
                    }
                }
            }
            catch (System.Net.WebException ex)
            {
                string responseFromServer = ex.Message.ToString() + " ";
                if (ex.Response != null)
                {
                    Stream resStream = ex.Response.GetResponseStream();
                    StreamReader sr = new StreamReader(resStream);
                    responseFromServer += sr.ReadToEnd();
                    sr.Close();
                    resStream.Close();
                    responseString = responseFromServer;
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return responseString;
        }

        public static X509Certificate[] GetX509CertificateArray(params string[] pathArray)
        {
            X509Certificate[] x509CertificateArray = new X509Certificate[pathArray.Length];
            try
            {
                for (int i = 0; i < pathArray.Length; i++)
                {
                    x509CertificateArray[i] = new X509Certificate(pathArray[i]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return x509CertificateArray;
        }
    }
}