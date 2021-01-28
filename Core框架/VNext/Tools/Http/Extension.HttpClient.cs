using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace VNext.Tools.Http
{
    public static partial class HttpExtension
    {
        /// <summary>
        /// 基于Json的Get请求
        /// </summary>
        public static Task<HttpResponseMessage> GetAsync(this HttpClient client, string requestUri,
            HttpAuth auth = null)
        {
            var builder = new HttpRequestMessageBuilder();
            builder.SetMethod(HttpMethod.Get);
            builder.SetRequestUri(requestUri);
            builder.SetAuth(auth);
            return client.SendAsync(builder);
        }

        /// <summary>
        /// 基于Json的Post请求
        /// </summary>
        public static Task<HttpResponseMessage> PostAsync(this HttpClient client, string requestUri, object value,
             HttpAuth auth = null)
        {
            var builder = new HttpRequestMessageBuilder();
            builder.SetMethod(HttpMethod.Post);
            builder.SetRequestUri(requestUri);
            builder.SetHttpContent(new JsonContent(value));
            builder.SetAuth(auth);
            return client.SendAsync(builder);
        }

        /// <summary>
        /// 基于Xml的Post请求
        /// </summary>
        public static Task<HttpResponseMessage> PostXmlAsync(this HttpClient client, string requestUri, string value,
            HttpAuth auth = null)
        {
            var builder = new HttpRequestMessageBuilder();
            builder.SetMethod(HttpMethod.Post);
            builder.SetRequestUri(requestUri);
            builder.SetHttpContent(new XmlContent(value));
            builder.SetAuth(auth);
            return client.SendAsync(builder);
        }

        /// <summary>
        /// 基于Json的Put请求
        /// </summary>
        public static Task<HttpResponseMessage> PutAsync(this HttpClient client, string requestUri, object value,
            HttpAuth auth = null)
        {
            var builder = new HttpRequestMessageBuilder();
            builder.SetMethod(HttpMethod.Put);
            builder.SetRequestUri(requestUri);
            builder.SetHttpContent(new JsonContent(value));
            builder.SetAuth(auth);
            return client.SendAsync(builder);
        }

        /// <summary>
        /// 基于Http的Patch请求
        /// </summary>
        public static Task<HttpResponseMessage> PatchAsync(this HttpClient client, string requestUri, object value,
            HttpAuth auth = null)
        {
            var builder = new HttpRequestMessageBuilder();
            builder.SetMethod(HttpMethod.Patch);
            builder.SetRequestUri(requestUri);
            builder.SetHttpContent(new PatchContent(value));
            builder.SetAuth(auth);
            return client.SendAsync(builder);
        }

        /// <summary>
        /// 基于Http的Delete请求
        /// </summary>
        public static Task<HttpResponseMessage> DeleteAsync(this HttpClient client, string requestUri, HttpAuth auth = null)
        {
            var builder = new HttpRequestMessageBuilder();
            builder.SetMethod(HttpMethod.Delete);
            builder.SetRequestUri(requestUri);
            builder.SetAuth(auth);
            return client.SendAsync(builder);
        }

        /// <summary>
        /// 基于Http的PostFile
        /// </summary>
        public static Task<HttpResponseMessage> PostFileAsync(this HttpClient client, string requestUri,
            string filePath, string apiParamName, HttpAuth auth = null)
        {
            var builder = new HttpRequestMessageBuilder();
            builder.SetMethod(HttpMethod.Post);
            builder.SetRequestUri(requestUri);
            builder.SetHttpContent(new FileContent(filePath, apiParamName));
            builder.SetAuth(auth);
            return client.SendAsync(builder);
        }

        /// <summary>
        /// 基于HttpClient以及HttpRequestMessageBuilder配置信息，发送请求 
        /// </summary>
        public static Task<HttpResponseMessage> SendAsync(this HttpClient client, HttpRequestMessageBuilder httpRequestMessageBuilder)
        {
            if (httpRequestMessageBuilder == null) return null;
            var httpRequest = httpRequestMessageBuilder.ToHttpRequestMessage();

            if (client.Timeout == null)
                client.Timeout = new TimeSpan(0, 0, 10);

            return client.SendAsync(httpRequest);
        }
    }
}
