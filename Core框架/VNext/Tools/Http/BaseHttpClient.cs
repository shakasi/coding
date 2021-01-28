using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Net.Http;
using VNext.Data;
using VNext.Options;

namespace VNext.Tools.Http
{
    public abstract class BaseHttpClient
    {
        public BaseHttpClient(HttpClient httpClient, IServiceProvider serviceProvider)
        {
            var id = this.GetType().Name;
            var apiOption = serviceProvider?.GetVNextOptions()?.WebApis?.FirstOrDefault(m => m.Id == id);

            if (apiOption == null)
                throw new Exception($"HttpClient类型:{id},缺少相关配置,请检查");

            var hostUri = string.IsNullOrEmpty(apiOption.RootPath) ?
                apiOption.Host : $"{apiOption.Host}/{apiOption.RootPath}";
            httpClient.BaseAddress = new Uri(hostUri);
            httpClient.Timeout = new TimeSpan(0, 0, apiOption.TimeOut);
            httpClient.MaxResponseContentBufferSize = apiOption.ContentSize;

            ApiOption = apiOption;
            ApiClient = httpClient;
        }

        public virtual HttpClient ApiClient { get; }

        public virtual WebApiOption ApiOption { get; }

        public virtual string BasicAuth => Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(
                                  string.Format("{0}:{1}", ApiOption.ClientId, ApiOption.ClientSecurity)));
    }
}
