using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using VNext.Data;
using VNext.Extensions;

namespace VNext.Options
{
    /// <summary>
    /// VNext配置选项创建器
    /// </summary>
    public class VNextOptionsSetup : IConfigureOptions<VNextOptions>
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// 初始化一个<see cref="VNextOptionsSetup"/>类型的新实例
        /// </summary>
        public VNextOptionsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(VNextOptions vNextOptions)
        {
            //简单对象构建
            SetSwagger(vNextOptions);
            SetJwt(vNextOptions);
            SetCookie(vNextOptions);
            SetMailSender(vNextOptions);
            SetRedis(vNextOptions);

            //复杂对象构建
            SetDbContextOptions(vNextOptions);
            WebApiOptionsesInit(vNextOptions);
            SetSqlOptionsInit(vNextOptions);
            SetWebServiceOptionsInit(vNextOptions);
        }

        #region privare Setup Methods

        private void SetSwagger(VNextOptions vNextOptions)
        {
            SwaggerOption swagger = _configuration.GetSection("VNext:Swagger")
                                    ?.Get<SwaggerOption>();

            if (swagger != null)
            {
                if (swagger.Endpoints.IsNullOrEmpty())
                {
                    throw new Exception("配置文件中Swagger节点的EndPoints不能为空");
                }

                if (swagger.RoutePrefix == null)
                {
                    swagger.RoutePrefix = "swagger";
                }
                vNextOptions.Swagger = swagger;
            }
        }

        private void SetJwt(VNextOptions vNextOptions)
        {
            JwtOption jwt = _configuration.GetSection("VNext:Jwt")
                                        ?.Get<JwtOption>();

            if (jwt != null)
            {
                if (jwt.Secret == null)
                {
                    jwt.Secret = _configuration["VNext:Jwt:Secret"];
                }
                vNextOptions.Jwt = jwt;
            }
        }

        private void SetCookie(VNextOptions vNextOptions)
        {

            CookieOption cookie = _configuration.GetSection("VNext:Cookie")
                                        ?.Get<CookieOption>();

            if (cookie != null)
            {
                vNextOptions.Cookie = cookie;
            }
        }

        private void SetRedis(VNextOptions vNextOptions)
        {
            RedisOption redis = _configuration.GetSection("VNext:Redis")
                                ?.Get<RedisOption>();

            if (redis != null)
            {
                if (redis.Configuration.IsMissing())
                {
                    throw new Exception("配置文件中Redis节点的Configuration不能为空");
                }
                vNextOptions.Redis = redis;
            }
        }

        private void SetMailSender(VNextOptions vNextOptions)
        {
            MailSenderOption sender = _configuration.GetSection("VNext:MailSender")
                                    ?.Get<MailSenderOption>();

            if (sender != null)
            {
                if (sender.Password == null)
                {
                    sender.Password = _configuration["VNext:MailSender:Password"];
                }
                vNextOptions.MailSender = sender;
            }
        }

        /// <summary>
        /// 初始化上下文配置信息，首先以VNext配置节点中的为准，
        /// 不存在VNext节点，才使用ConnectionStrings的数据连接串来构造SqlServer的配置，
        /// 保证同一上下文类型只有一个配置节点
        /// </summary>
        /// <param name="options"></param>
        private void SetDbContextOptions(VNextOptions options)
        {
            IConfigurationSection section = _configuration.GetSection("VNext:DbContexts");
            IDictionary<string, DbContextOption> dict = section.Get<Dictionary<string, DbContextOption>>();

            if (dict?.Values != null && dict.Values.Count > 0)
            {
                var repeated = dict.Values.GroupBy(m => m.DbContextType).FirstOrDefault(m => m.Count() > 1);
                if (repeated != null)
                {
                    throw new Exception($"数据上下文配置中存在多个配置节点指向同一个上下文类型：{repeated.First().DbContextTypeName}");
                }

                foreach (KeyValuePair<string, DbContextOption> pair in dict)
                {
                    options.DbContexts.Add(pair.Key, pair.Value);
                }
            }
        }

        /// <summary>
        /// 初始化webapi配置信息
        /// 保证不存在重复配置节点
        /// </summary>
        /// <param name="options"></param>
        private void WebApiOptionsesInit(VNextOptions options)
        {
            IConfigurationSection section = _configuration.GetSection("WebApis");
            List<WebApiOption> listWebapi = section?.Get<List<WebApiOption>>();

            if ((listWebapi?.Count ?? 0) > 0)
            {
                var repeated = listWebapi.GroupBy(m => m.Id).FirstOrDefault(m => m.Count() > 1);
                if (repeated != null)
                {
                    throw new Exception($"WebApis配置中存在相同Id的信息：{repeated.First().Id}");
                }

                options.WebApis.Clear();
                options.WebApis.AddRange(listWebapi);
            }
        }

        /// <summary>
        /// 初始化Sql配置信息
        /// 保证不存在重复配置节点
        /// </summary>
        /// <param name="options"></param>
        private void SetSqlOptionsInit(VNextOptions options)
        {
            IConfigurationSection section = _configuration.GetSection("Sqls");
            List<SqlOption> listSqlOption = section?.Get<List<SqlOption>>();

            if ((listSqlOption?.Count ?? 0) > 0)
            {
                var repeated = listSqlOption.GroupBy(m => m.Id).FirstOrDefault(m => m.Count() > 1);
                if (repeated != null)
                {
                    throw new Exception($"Sqls配置中存在相同Id的信息：{repeated.First().Id}");
                }

                options.SqlOptions.Clear();
                options.SqlOptions.AddRange(listSqlOption);
            }
        }

        /// <summary>
        /// 初始化Sql配置信息
        /// 保证不存在重复配置节点
        /// </summary>
        /// <param name="options"></param>
        private void SetWebServiceOptionsInit(VNextOptions options)
        {
            IConfigurationSection section = _configuration.GetSection("WebServices");
            List<WebServiceOption> listSqlOption = section?.Get<List<WebServiceOption>>();

            if ((listSqlOption?.Count ?? 0) > 0)
            {
                var repeated = listSqlOption.GroupBy(m => m.Id).FirstOrDefault(m => m.Count() > 1);
                if (repeated != null)
                {
                    throw new Exception($"WebService配置中存在相同Id的信息：{repeated.First().Id}");
                }

                options.WebServiceOptions.Clear();
                options.WebServiceOptions.AddRange(listSqlOption);
            }
        }
        #endregion
    }
}