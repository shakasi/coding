using System.ComponentModel;

namespace VNext.Entity
{
    /// <summary>
    /// 表示数据库类型，如SqlServer，Sqlite等
    /// </summary>
    public enum DatabaseType
    {
        /// <summary>
        /// SqlServer数据库类型
        /// </summary>
        SqlServer = 0,
        /// <summary>
        /// Sqlite数据库类型
        /// </summary>
        Sqlite,
        /// <summary>
        /// MySql数据库类型
        /// </summary>
        MySql,
        /// <summary>
        /// PostgreSql数据库类型
        /// </summary>
        PostgreSql,
        /// <summary>
        /// Oracle数据库类型
        /// </summary>
        Oracle,
    }

    /// <summary>
    /// 排序方式
    /// </summary>
    public enum OrderType
    {
        /// <summary>
        /// 升序
        /// </summary>
        Ascending,

        /// <summary>
        /// 降序
        /// </summary>
        Descending
    }
    /// <summary>
    /// 请求发送数据解析类型
    /// </summary>
    public enum ContentType
    {
        /// <summary>
        /// application/soap+xml; charset=utf-8
        /// </summary>
        [Description("application/soap+xml; charset=utf-8")]
        SoapXml1_1,
        /// <summary>
        /// application/soap+xml; charset=utf-8
        /// </summary>
        [Description("application/soap+xml; charset=utf-8")]
        SoapXml1_2,
        /// <summary>
        /// text/xml; charset=utf-8
        /// </summary>
        [Description("text/xml; charset=utf-8")]
        TextXml
    }
    /// <summary>
    /// 请求方式
    /// </summary>
    public enum HttpWebRequestMethodEnum
    {
        GET,
        POST
    }
}