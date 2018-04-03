using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using System.Security.Authentication;
using Shaka.Utils;

namespace GloryTP
{
    /// <summary>
    /// ע��
    /// 1��֤��ע����"X509Certificate"����"X509Certificate2",��˳��
    /// 2������
    /// 3��options.setUserName("abcd");options.setPassword("abcd123456");
    /// 4��rampart
    /// 5��IE��װ֤�飨(hosts��ӣ�IE������վ���192.168.20.139 CarrefourStoreClient)
    /// 6�������ε�֤��䷢����
    /// 7��https://10.132.250.3/axis2/services/TPLCashierMediaWebService/getCashierMediaXML��
    ///    https://10.132.250.3/axis2/services/TPLCashierMediaWebService/getCashierMediaXML.wsdl
    ///    ����
    /// 8��X509Certificate.CreateFromCertFile(pfxPath)
    /// 9����֤���"changeit"
    /// 10��request.EnableSsl = true;
    /// 11��hwrRequest.KeepAlive = true;
    /// 12������IE��ȫ
    /// </summary>
    class Program
    {
        private static WebServiceHttpWebRequesHelper _webServiceHelper = null;

        static void Main(string[] args)
        {
            //��֤������֤��ص�����,GetRequestStream()����ʱ����
            ServicePointManager.ServerCertificateValidationCallback = new
                RemoteCertificateValidationCallback(CheckValidationResult);

            #region ����
            //url (http post��ʽҪ������)
            string url = "https://10.132.250.3/axis2/services/TPLCashierMediaWebService/getCashierMediaXML";
            url = "https://10.132.250.3/axis2/services/TPLCashierMediaWebService?wsdl";
            url = "https://10.132.250.3:8443/axis2/services/TPLCashierMediaWebService?wsdl";

            //֤��
            //string certificateBasePath = AppDomain.CurrentDomain.BaseDirectory;
            X509Certificate x509Certificate = new X509Certificate("CarrefourStoreServer.cer", "changeit");
            //X509Certificate sssss= X509Certificate.CreateFromCertFile("CarrefourStoreServer.cer");
            string aa= x509Certificate.GetSerialNumberString();
            string bb=x509Certificate.GetRawCertDataString();
            string cc=x509Certificate.GetPublicKeyString();
            //string dd = (Convert.ToBase64String(x509Certificate.RawData));
            //���󷽷�
            string methodType = "POST";

            //ִ�з���
            _webServiceHelper = new WebServiceHttpWebRequesHelper(url, methodType, x509Certificate);
            //_webServiceHelper.MyRequest.UserAgent = "Client Cert Sample"; 

            TestWebServiceReference(x509Certificate);
            //TestSoap12();
            //TestJavaSoap();
            //TestDynamic(url, x509CertificateArray);
            #endregion
        }

        /// <summary>
        /// vs������� ����һ
        /// </summary>
        public static void TestWebServiceReference(X509Certificate x509Certificate)
        {
            string operation = "1";
            string empNmbr = "1001";
            string businessDate = "2014110600";
            string rtnMessage = null;
            string cashID = null;
            string cashCount = null;
            string cashAmount = null;
            string chequeID = null;
            string chequeCount = null;
            string chequeAmount = null;
            string ACSID = null;
            string ACSCount = null;
            string ACSAmount = null;
            string onlinePaymentID = null;
            string onlinePaymentCount = null;
            string onlinePaymentAmount = null;
            string offlinePaymentID = null;
            string offlinePaymentCount = null;
            string offlinePaymentAmount = null;
            //WebReference1.TPLCashierMediaWebService wc = new GloryTP.WebReference1.
            //        TPLCashierMediaWebService();
            WebReference8843.TPLCashierMediaWebService wc = new GloryTP.WebReference8843.TPLCashierMediaWebService();
            wc.Url = "https://10.132.250.3:8443/axis2/services/TPLCashierMediaWebService?wsdl";
            wc.ClientCertificates.Add(x509Certificate);
            wc.Credentials = new NetworkCredential("abcd", "abcd123456");
            try
            {
                object a = wc.getCashierMediaXML(ref operation, ref empNmbr, ref businessDate, out rtnMessage,
                     out cashID, out cashCount, out cashAmount, out chequeID, out chequeCount, out chequeAmount,
                     out ACSID, out ACSCount, out ACSAmount, out onlinePaymentID, out onlinePaymentCount, out onlinePaymentAmount,
                     out offlinePaymentID, out offlinePaymentCount, out offlinePaymentAmount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Http post ������(��Է�������ref out�ؼ��֣��о��о�����)
        /// </summary>
        public static string TestHttpPost(string url)
        {
            HttpWebRequest myRequest = _webServiceHelper.MyRequest;
            myRequest.ContentType = WebServiceParams.contentTypePost;
            myRequest.Accept = "text/xml";
            myRequest.Headers.Add("SOAPAction", url);
            myRequest.Timeout = 5000;
            string paramsStr = string.Format("operation=1&empNmbr=1001&businessDate=2014110600");
            return _webServiceHelper.GetResponseToStr(paramsStr);
        }

        /// <summary>
        /// SOAP Э�� ������
        /// </summary>
        public static string TestSoap()
        {
            HttpWebRequest myRequest = _webServiceHelper.MyRequest;
            myRequest.ContentType = WebServiceParams.contentTypeSOAP;
            myRequest.Headers.Add("SOAPAction", "http://tempuri.org/HelloWorld");
            string paramsStr = @"<?xml version=""1.0"" encoding=""utf-8""?>
                                        <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                                          <soap:Body>
                                            <Test xmlns=""http://tempuri.org/"">
                                                <name>��11</name>
                                            </Test>
                                          </soap:Body>
                                        </soap:Envelope>";
            return _webServiceHelper.GetResponseToStr(paramsStr);
        }

        /// <summary>
        /// SOAP1.2 Э�� ������
        /// </summary>
        public static string TestSoap12()
        {
            HttpWebRequest myRequest = _webServiceHelper.MyRequest;
            myRequest.ContentType = WebServiceParams.contentTypeSOAP12;
            string paramsStr = @"<?xml version=""1.0"" encoding=""utf-8""?>
                                <soap12:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap12=""http://www.w3.org/2003/05/soap-envelope"">
                                 <soap12:Body>
                                    <Test xmlns=""http://tempuri.org/"">
                                        <name>��12</name>
                                    </Test>
                                </soap12:Body>
                            </soap12:Envelope>";

            return _webServiceHelper.GetResponseToStr(paramsStr);
        }

        /// <summary>
        /// SOAP Э�� �����壨Axis2+Rampartʵ���û���&���뼶��
        /// </summary>
        public static string TestJavaSoap()
        {
            _webServiceHelper.MyRequest.ContentType = WebServiceParams.contentTypeSOAP12;
            //_webServiceHelper.MyRequest.KeepAlive = true;

            string paramsStr = @"<?xml version='1.0' encoding='utf-8'?>
                            <soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"">
                              <soapenv:Header>
                                <wsse:Security xmlns:wsse=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd"" soapenv:mustUnderstand=""1"">
                                  <wsse:UsernameToken xmlns:wsu=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd"" wsu:Id=""UsernameToken-1"">
                                    <wsse:Username>abcd</wsse:Username>
                                    <wsse:Password Type=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-username-token-profile-1.0#PasswordDigest"">5iAF9iwKXrn6HjcWh806wLzKQ1Y=</wsse:Password>
                                    <wsse:Nonce EncodingType=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Base64Binary"">SampoGPKPx60qqfCXtRAWw==</wsse:Nonce>
                                    <wsu:Created>2014-12-15T06:18:49.507Z</wsu:Created>
                                  </wsse:UsernameToken>
                                </wsse:Security>
                              </soapenv:Header>
                              <soapenv:Body>
                                <ns1:tplCashierMediaXML xmlns:ns1=""http://cashiermedia.webapp.tplinux.wincor_nixdorf.com"">
                                  <ns1:operation>1</ns1:operation>
                                  <ns1:empNmbr>1002</ns1:empNmbr>
                                  <ns1:businessDate>2014120900</ns1:businessDate>
                                </ns1:tplCashierMediaXML>
                              </soapenv:Body>
                            </soapenv:Envelope>";

            return _webServiceHelper.GetResponseToStr(paramsStr);
        }

        /// <summary>
        /// ��̬���ɴ����� ������
        /// </summary>
        public static void TestDynamic(string url, X509Certificate[] x509CertificateArray)
        {
            try
            {
                WebServiceProxy wsp = new WebServiceProxy(url, x509CertificateArray);
                object[] objArray = { "С��", "" };
                //object[] objArray =  { "1", "1001", "2014110600","","","","","","","","",
                //    "","","","","","","",""};
                object responseString = wsp.ExecuteQuery("Test", objArray);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool CheckValidationResult(object sender, X509Certificate certificate,
            X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == SslPolicyErrors.None)
            {
                return true;
            }
            else
            {
                if ((SslPolicyErrors.RemoteCertificateNameMismatch &
                    sslPolicyErrors) == SslPolicyErrors.RemoteCertificateNameMismatch)
                {
                    //tracing.WarnFmt("֤�����Ʋ�ƥ��{0}", sslPolicyErrors);

                }


                if ((SslPolicyErrors.RemoteCertificateChainErrors &
                sslPolicyErrors) == SslPolicyErrors.RemoteCertificateChainErrors)
                {
                    string msg = "";
                    foreach (X509ChainStatus status in chain.ChainStatus)
                    {
                        msg += "status code ={0} " + status.Status;
                        msg += "Status info = " + status.StatusInformation + " ";
                    }


                    //tracing.WarnFmt("֤��������{0}", msg);

                }
                //tracing.WarnFmt("֤����֤ʧ��{0}", sslPolicyErrors);
            }
            return true;
        }
    }
}