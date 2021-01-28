using Newtonsoft.Json;
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
        /// Http请求结果信息转换实体
        /// </summary>
        public static async Task<TResult> ContentAsType<TResult>(this HttpResponseMessage response)
              where TResult : class
        {
            if (!response.IsSuccessStatusCode) return default(TResult);

            string json = await response.Content.ReadAsStringAsync();
            if (typeof(TResult) == typeof(string))
            {
                return json as TResult;
            }
            return JsonConvert.DeserializeObject<TResult>(json);
        }
    }
}
