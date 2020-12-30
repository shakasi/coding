using System;
using System.Net.Http;
using System.Net.Http.Headers;
using VNext.Data;
using VNext.Extensions;

namespace VNext.Tools.Http
{
    /// <summary>
    /// HttpRequest构建器
    /// </summary>
    public class HttpRequestMessageBuilder
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public HttpRequestMessageBuilder()
        {
            this.acceptHeader = "application/json";
        }

        //http请求方式
        private HttpMethod httpMethod = null;
        public HttpRequestMessageBuilder SetMethod(HttpMethod method)
        {
            this.httpMethod = method;
            return this;
        }

        //http请求相关内容
        private HttpContent httpContent = null;
        public HttpRequestMessageBuilder SetHttpContent(HttpContent httpContent)
        {
            this.httpContent = httpContent;
            return this;
        }

        //http请求地址
        private string requestUri = string.Empty;
        public HttpRequestMessageBuilder SetRequestUri(string requestUri)
        {
            this.requestUri = requestUri;
            return this;
        }

        //http请求信息格式
        private string acceptHeader = string.Empty;
        public HttpRequestMessageBuilder SetAcceptHeader(string acceptHeader)
        {
            this.acceptHeader = acceptHeader;
            return this;
        }

        //http请求授权模式:BasicAuth
        private string basicAuth = null;
        //http请求授权模式:BearToken
        private string bearerToken = null;
        public HttpRequestMessageBuilder SetAuth(HttpAuth auth)
        {
            if (auth == null) return this;

            switch (auth.AuthType)
            {
                case AuthType.BasicAuth:
                    this.basicAuth = auth.AuthValue;
                    this.bearerToken = null;
                    break;
                case AuthType.BearerToken:
                    this.bearerToken = auth.AuthValue;
                    this.basicAuth = null;
                    break;
                default:
                    break;
            }
            return this;
        }

        /// <summary>
        /// 转换HttpRequestMessage
        /// </summary>
        public HttpRequestMessage ToHttpRequestMessage()
        {
            Check.NotNull(this.httpMethod, nameof(this.httpMethod));
            Check.NotNullOrEmpty(this.requestUri, nameof(this.requestUri));

            var request = new HttpRequestMessage
            {
                Method = this.httpMethod,
                RequestUri = new Uri(this.requestUri)
            };

            if (this.httpContent != null)
                request.Content = this.httpContent;

            if (!string.IsNullOrEmpty(this.basicAuth))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Basic", this.basicAuth);
            }
            if (!string.IsNullOrEmpty(this.bearerToken))
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", this.bearerToken);

            request.Headers.Accept.Clear();
            if (!string.IsNullOrEmpty(this.acceptHeader))
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(this.acceptHeader));

            return request;
        }
    }
}
