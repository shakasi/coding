using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using VNext.Data;
using VNext.Extensions;

namespace VNext.Options
{
    /// <summary>
    /// VNext框架配置选项信息
    /// </summary>
    public class VNextOptions
    {
        /// <summary>
        /// 初始化一个<see cref="VNextOptions"/>类型的新实例
        /// </summary>
        public VNextOptions()
        {
            DbContexts = new ConcurrentDictionary<string, DbContextOption>(StringComparer.OrdinalIgnoreCase);
            WebApis = new List<WebApiOption>();
            SqlOptions = new List<SqlOption>();
            WebServiceOptions = new List<WebServiceOption>();
        }

        #region 数据属性

        /// <summary>
        /// 获取或设置 Swagger选项
        /// </summary>
        public SwaggerOption Swagger { get; set; }

        /// <summary>
        /// 获取或设置 Redis选项
        /// </summary>
        public RedisOption Redis { get; set; }

        /// <summary>
        /// 获取或设置 JWT身份认证选项
        /// </summary>
        public JwtOption Jwt { get; set; }

        /// <summary>
        /// 获取或设置 Cookie身份认证选项
        /// </summary>
        public CookieOption Cookie { get; set; }

        /// <summary>
        /// 获取或设置 邮件发送选项
        /// </summary>
        public MailSenderOption MailSender { get; set; }

        /// <summary>
        /// 获取 数据库上下文配置信息
        /// </summary>
        public IDictionary<string, DbContextOption> DbContexts { get; }

        /// <summary>
        /// WebApi配置选项
        /// </summary>
        public List<WebApiOption> WebApis { get; }

        /// <summary>
        /// WebApi配置选项
        /// </summary>
        public List<SqlOption> SqlOptions { get; }
        /// <summary>
        /// WebService配置选项
        /// </summary>
        public List<WebServiceOption> WebServiceOptions { get; }

        #endregion

        /// <summary>
        /// 获取指定上下文类和指定数据库类型的上下文配置信息
        /// </summary>
        public DbContextOption GetDbContextOptions(Type dbContextType)
        {
            return DbContexts.Values.SingleOrDefault(m => m.DbContextType == dbContextType);
        }

        /// <summary>
        /// 获取指定id的webapi配置信息
        /// </summary>
        /// <param name="apiId"></param>
        /// <returns></returns>
        public WebApiOption GetWebApiOption(string apiId)
        {
            Check.NotNullOrEmpty(apiId, nameof(apiId));
            return WebApis?.SingleOrDefault(m => m.Id.ToLower() == apiId.ToLower());
        }

        /// <summary>
        /// 获取指定id的sql配置信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SqlOption GetSqlOption(string id)
        {
            Check.NotNullOrEmpty(id, nameof(id));
            return SqlOptions?.SingleOrDefault(m => string.Equals(m.Id, id, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// 获取指定id的webservice配置信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public WebServiceOption GetWebServiceOption(string id)
        {
            Check.NotNullOrEmpty(id, nameof(id));
            return WebServiceOptions?.SingleOrDefault(m => string.Equals(m.Id, id, StringComparison.OrdinalIgnoreCase));
        }
    }
}