using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace Shaka.Utils
{
    public class WebServiceHttpWebRequesHelper
    {
        private HttpWebRequest _myRequest = null;

        public HttpWebRequest MyRequest
        {
            get { return this._myRequest; }
        }

        public WebServiceHttpWebRequesHelper(string url, string methodType, X509Certificate x509Certificate)
        {
            _myRequest = (HttpWebRequest)HttpWebRequest.Create(url);
            _myRequest.Method = methodType;
            _myRequest.ClientCertificates.Add(x509Certificate);
        }

        /// <summary>
        /// 获取请求返回的数据
        /// </summary>
        /// <param name="myResponse"></param>
        public string GetResponseToStr(string paramsStr)
        {
            string responseString = string.Empty;

            byte[] paramsBytes = Encoding.UTF8.GetBytes(paramsStr);
            int paramsLenth = paramsBytes.Length;
            _myRequest.ContentLength = paramsLenth;

            try
            {
                using (Stream requestStream = _myRequest.GetRequestStream())
                {
                    requestStream.Write(paramsBytes, 0, paramsLenth);
                }
                using (HttpWebResponse myResponse = (HttpWebResponse)_myRequest.GetResponse())
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
                        throw new Exception("连接错误");
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
    }

    public struct WebServiceParams
    {
        /// <summary>
        /// Http post 方式
        /// </summary>
        public const string contentTypePost = "application/x-www-form-urlencoded";
        /// <summary>
        /// SOAP1.1协议
        /// </summary>
        public const string contentTypeSOAP = "text/xml;charset=utf-8";
        /// <summary>
        /// SOAP1.2 协议
        /// </summary>
        public const string contentTypeSOAP12 = "application/soap+xml; charset=utf-8";
    }
}
