using System;
using System.Text;
using System.Net;
using System.Net.Cache;
using System.IO;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using NetTest;

namespace NetTest
{
    public class HttpTest:ITest
    {
        private string _url;
        private string _userName;
        private string _password;
        private string _referer;

        public HttpTest(string url, string UserName, string Password,string sReferer)
        {
            this._url = url;
            this._userName = UserName;
            this._password = Password;
            this._referer = sReferer;
        }

        public bool Test()
        {
            MyRequest reqCls = new MyRequest();
            //string strPost = string.Format("username={0}&password={1}", _userName, _password);
            string strPost =@"{""cookie"":0,""username"":""admin"",""password"":""12345678!""}";
            string body = reqCls.GoRequest(_url, MyRequest.HttpEnum.POST, strPost,true,_referer);
            //string body = reqCls.GoRequest(_url, MyRequest.HttpEnum.GET, "");

            if (!string.IsNullOrEmpty(reqCls.LastError)) { return false; }
            return true;
        }
    }

    public class HttpsTest:ITest
    {
        private string _url;
        private string _userName;
        private string _password;
        private string _referer;
        X509Certificate2 _cert;

        public HttpsTest(string url, string UserName, string Password, string certPath,string sReferer)
        {
            this._url = url;
            this._userName = UserName;
            this._password = Password;
            this._cert = new X509Certificate2(certPath);
            this._referer = sReferer;
        }

        public bool Test()
        {
            MyRequest reqCls = new MyRequest();
            reqCls.MyCert = _cert;

            string strPost = string.Format("cookies=0&username={0}&password={1}", _userName, _password);
            string body = reqCls.GoRequest(_url, MyRequest.HttpEnum.POST, strPost,true,_referer);

            if (!string.IsNullOrEmpty(reqCls.LastError)) { return false; }
            return true;
        }
    }

    public class MyRequest
    {
        private Encoding _ReqEncoding = Encoding.UTF8;	// 
        private Encoding _ResEncoding = Encoding.UTF8;	// 回应的编码.
        private string _lastError = null;
        private X509Certificate2 _cert = null;

        public X509Certificate2 MyCert
        {
            get { return _cert; }
            set { _cert = value; }
        }

        public string LastError
        {
            get { return _lastError; }
            set { _lastError = value; }
        }

        public enum HttpEnum
        {
            GET = 0,
            POST = 1
        }

        public string GoRequest(string sURL, HttpEnum eMode, string postData,bool bKeepAlive=false,string sReferer="")
        {
            LastError = null;
            string Resbody = null;
            
            HttpWebRequest req;
            req = HttpWebRequest.Create(sURL) as HttpWebRequest;
            req.Method = eMode.ToString();
            req.Accept = "*/*";
            req.KeepAlive = bKeepAlive;
            //req.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
            if (!string.IsNullOrEmpty(sReferer)) req.Referer = sReferer;
            Encoding myEncoding = _ReqEncoding;
            string sContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            req.ContentType = sContentType;
            //req.UseDefaultCredentials = true;
            //req.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; WOW64; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E)";
            //req.Headers.Add("x-requested-with", "XMLHttpRequest");
            if (sURL.IndexOf("https") == 0 && _cert!=null) 
            {
                req.ClientCertificates.Add(MyCert);
                Util.SetCertificatePolicy(); 
            }

            try
            {
                if (eMode == HttpEnum.POST)
                {
                    byte[] bufPost = myEncoding.GetBytes(postData);

                    req.ContentLength = bufPost.Length;
                    Stream newStream = req.GetRequestStream();
                    newStream.Write(bufPost, 0, bufPost.Length);
                    newStream.Close();
                    
                }

                HttpWebResponse res = req.GetResponse() as HttpWebResponse;

                try
                {
                    #region resMsg
                    //string.Format("Response.ContentLength:\t{0}", res.ContentLength);
                    //string.Format("Response.ContentType:\t{0}", res.ContentType);
                    //string.Format("Response.CharacterSet:\t{0}", res.CharacterSet);
                    //string.Format("Response.ContentEncoding:\t{0}", res.ContentEncoding);
                    //string.Format("Response.IsFromCache:\t{0}", res.IsFromCache);
                    //string.Format("Response.IsMutuallyAuthenticated:\t{0}", res.IsMutuallyAuthenticated);
                    //string.Format("Response.LastModified:\t{0}", res.LastModified);
                    //string.Format("Response.Method:\t{0}", res.Method);
                    //string.Format("Response.ProtocolVersion:\t{0}", res.ProtocolVersion);
                    //string.Format("Response.ResponseUri:\t{0}", res.ResponseUri);
                    //string.Format("Response.Server:\t{0}", res.Server);
                    //string.Format("Response.StatusCode:\t{0}\t# {1}", res.StatusCode, (int)res.StatusCode);
                    //string.Format("Response.StatusDescription:\t{0}", res.StatusDescription);

                    //// header
                    //for (int i = 0; i < res.Headers.Count; ++i)
                    //{
                    //    string.Format("[{2}] {0}:\t{1}", res.Headers.Keys[i], res.Headers[i], i);
                    //}
                    #endregion

                    Encoding encoding = _ResEncoding;

                    using (Stream resStream = res.GetResponseStream())
                    {
                        using (StreamReader resStreamReader = new StreamReader(resStream, encoding))
                        {
                            Resbody = (resStreamReader.ReadToEnd());
                        }
                    }
                }
                finally
                {
                    res.Close();
                }
            }
            catch (Exception ex)
            {
                LastError = ex.ToString();
            }
            return Resbody;
        }
    }

    public static class Util
    {
        /// <summary>  
        /// Sets the cert policy.  
        /// </summary>  
        public static void SetCertificatePolicy()
        {
            ServicePointManager.ServerCertificateValidationCallback
                       += RemoteCertificateValidate;
        }

        /// <summary>  
        /// Remotes the certificate validate.  
        /// </summary>  
        private static bool RemoteCertificateValidate(
           object sender, X509Certificate cert,
            X509Chain chain, SslPolicyErrors error)
        {
            //// trust any certificate!!!  
            //System.Console.WriteLine("Warning, trust any certificate");
            return true;
        }
    }  
}
