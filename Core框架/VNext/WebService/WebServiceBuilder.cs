using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using VNext.Entity;
using VNext.Options;

namespace VNext.WebService
{
    public class WebServiceBuilder
    {
        private readonly WebServiceFactory _factory;
        private readonly ILogger<WebServiceBuilder> _logger;
        public WebServiceBuilder(WebServiceFactory factory, ILoggerFactory loggerFactory)
        {
            _factory = factory;
            _logger = loggerFactory.CreateLogger<WebServiceBuilder>();
        }

        public string SendMessage(WebServiceOption webOption, string xml)
        {
            if (string.IsNullOrEmpty(webOption.Url) || string.IsNullOrEmpty(webOption.MethodName) || string.IsNullOrEmpty(webOption.ContentType))
            { 
                _logger.LogError($"post request param not empty");
                return $"post request param not empty";
            }
            IWebService iWebService = _factory.CreateWebService(webOption.ContentType);
            return iWebService.PostService(webOption, xml);
        }
    }

    public class SoapXml1_1 : IWebService
    {
        public string PostService(WebServiceOption option, string xml)
        {
            option.ContentType = "application/soap+xml; charset=utf-8";
            //发送请求
            return SoapXmlRequest.RequestSoap(option, xml);
        }
    }

    public class SoapXml1_2 : IWebService
    {
        public string PostService(WebServiceOption option, string xml)
        {
            option.ContentType = "application/soap+xml; charset=utf-8";
            //发送请求
            return SoapXmlRequest.RequestSoap(option, xml);
        }
    }

    public class TextXml : IWebService
    {
        public string PostService(WebServiceOption option, string xml)
        {
            option.ContentType = "text/xml; charset=utf-8";
            //发送请求
            return SoapXmlRequest.RequestSoap(option, xml);
        }
    }


    public class SoapXmlRequest
    {
        public static string RequestSoap(WebServiceOption option, string xml)
        {
            HttpWebRequest request = null;

            try
            {
                request = WebRequest.Create(option.Url + "/" + option.MethodName) as HttpWebRequest;
                //request.UserAgent = "Mozilla/6.0 (MSIE 6.0; Windows NT 5.1; Natas.Robot)";
                request.Timeout = 60000;
                request.Method = HttpWebRequestMethodEnum.POST.ToString();

                byte[] bdata = Encoding.UTF8.GetBytes(xml);
                request.ContentType = option.ContentType;
                request.ContentLength = bdata.Length;
                if (!string.IsNullOrEmpty(option.SoapAction))
                {
                    request.Headers.Add("SOAPAction", option.SoapAction);
                }
                Stream streamOut = request.GetRequestStream();
                streamOut.Write(bdata, 0, bdata.Length);
                streamOut.Close();
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream streamIn = response.GetResponseStream();

                StreamReader reader = new StreamReader(streamIn, Encoding.UTF8);
                string result = reader.ReadToEnd();
                reader.Close();
                streamIn.Close();
                response.Close();

                return result;
            }
            catch (Exception ex)
            {
                return $"Http请求异常{ex.Message + ex.StackTrace + ex.InnerException + ex.Source + ex.TargetSite}";
                throw;
            }
            finally
            {
            }
        }
    }
}
