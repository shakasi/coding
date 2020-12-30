using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using VNext.Extensions;
//Table
using CusTableAttribute = VNext.Entity.TableAttribute;
using SysTableAttribute = System.ComponentModel.DataAnnotations.Schema.TableAttribute;

namespace VNext.Entity
{
    /// <summary>
    /// Sql语句创建器
    /// </summary>
    public class SqlCreator
    {
        #region 内部字段
        /// <summary>
        /// 表别名
        /// </summary>
        private static readonly List<string> tableAlias = new List<string> { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

        /// <summary>
        /// 表别名队列(先进先出)
        /// </summary>
        private Queue<string> tableAliasQueue = null;

        /// <summary>
        /// 实体类型信息字典
        /// </summary>
        private readonly ConcurrentDictionary<Type, (string tableName, string tableAliasName)> entityInfoDic = null;
        #endregion

        public SqlCreator(Type creatorType, DatabaseType databaseType)
        {
            this.tableAliasQueue = new Queue<string>(tableAlias);
            this.entityInfoDic = new ConcurrentDictionary<Type, (string tableName, string tableAliasName)>();
            this.CreatorType = creatorType;
            this.DatabaseType = databaseType;
        }

        #region 元数据属性
        /// <summary>
        /// 实体类型
        /// </summary>
        public Type CreatorType { get;  }

        /// <summary>
        /// 数据库类型
        /// </summary>
        public DatabaseType DatabaseType { get; }

        /// <summary>
        /// 查询的字段信息
        /// </summary>
        public List<string> SelectFields { get; set; } = new List<string>();

        /// <summary>
        /// Sql语句
        /// </summary>
        public StringBuilder SqlString { get; set; } = new StringBuilder();

        /// <summary>
        /// 参数字典
        /// </summary>
        public Dictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();
        #endregion

        #region 计算型属性
        public string SelectFieldsStr => this.SelectFields.Count == 0 ? "*" : string.Join(",", this.SelectFields);

        public int Length => this.SqlString.Length;

        /// <summary>
        /// SQl参数前缀
        /// </summary>
        public string DbParamPrefix
        {
            get
            {
                switch (this.DatabaseType)
                {
                    case DatabaseType.Sqlite: return "@";
                    case DatabaseType.SqlServer: return "@";
                    case DatabaseType.MySql: return "?";
                    case DatabaseType.Oracle: return ":";
                    case DatabaseType.PostgreSql: return ":";
                    default: return "";
                }
            }
        }

        /// <summary>
        /// 格式模板
        /// </summary>
        public string FormatTemp
        {
            get
            {
                switch (this.DatabaseType)
                {
                    case DatabaseType.Sqlite: return "\"{0}\"";
                    case DatabaseType.SqlServer: return "[{0}]";
                    case DatabaseType.MySql: return "`{0}`";
                    case DatabaseType.Oracle: return "\"{0}\"";
                    case DatabaseType.PostgreSql: return "\"{0}\"";
                    default: return "{0}";
                }
            }
        }
        #endregion

        #region 方法
        /// <summary>
        /// Clear
        /// </summary>
        public void Clear()
        {
            this.entityInfoDic.Clear();
            this.tableAliasQueue = new Queue<string>(tableAlias);

            this.SelectFields.Clear();
            this.SqlString.Clear();
            this.Parameters.Clear();
        }

        /// <summary>
        /// operator +
        /// </summary>
        public static SqlCreator operator +(SqlCreator sqlCreator, string sql)
        {
            sqlCreator.SqlString.Append(sql);
            return sqlCreator;
        }

        /// <summary>
        /// ToString
        /// </summary>
        public override string ToString() => this.SqlString.ToString();

        public void AddDbParameter(object parameterValue)
        {
            if (parameterValue == null || parameterValue == DBNull.Value)
            {
                this.SqlString.Append("NULL");
            }
            else
            {
                var name = $"{this.DbParamPrefix}Param{this.Parameters.Count}";
                this.Parameters.Add(name, parameterValue);
                this.SqlString.Append(name);
            }
        }

        #region Entity相关信息
        /// <summary>
        /// 获取实体相关信息
        /// </summary>
        /// <returns></returns>
        public void SetTypeDictionary(params Type[] array)
        {
            if (array == null) return;
            foreach (var type in array)
            {
                if (this.entityInfoDic.Keys.Contains(type)) return;
                this.entityInfoDic.TryAdd(type, (this.GetTableName(type), this.tableAliasQueue.Dequeue()));
            }
        }

        /// <summary>
        /// 获取表名
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>string</returns>
        public string GetTableName(Type type)
        {
            var cusTable = type.GetAttribute<CusTableAttribute>(true);
            var sysTable = type.GetAttribute<SysTableAttribute>(true);

            var schema = cusTable?.Schema ?? sysTable?.Schema;
            var tableName = cusTable?.Name ?? sysTable?.Name ?? type.Name;

            if (string.IsNullOrEmpty(schema))
            {
                return cusTable != null ? $"{tableName}" : $"{this.GetFormatName(tableName)}";
            }

            return cusTable != null ? $"{schema}.{tableName}" : $"{this.GetFormatName(schema)}.{this.GetFormatName(tableName)}";
        }

        /// <summary>
        /// 获取实体相关信息
        /// </summary>
        /// <returns></returns>
        public (string tableName, string tableAliasName) GetEntityInfo(Type type)
        {
            if (type == null) return (null, null);
            if (!this.entityInfoDic.Keys.Contains(type)) SetTypeDictionary(type);

            return this.entityInfoDic[type];
        }
        #endregion

        #region Column信息
        /// <summary>
        /// 获取列相关信息
        /// </summary>
        /// <param name="type">当前类型</param>
        /// <param name="member">成员定义所在类型</param>
        /// <returns></returns>
        public string GetColumnInfo(Type type, MemberInfo member)
        {
            var prop = type.GetProperties().Where(x => x.Name == member?.Name).FirstOrDefault();
            var column = prop?.GetAttribute<ColumnAttribute>() ?? member?.GetAttribute<ColumnAttribute>();
            string columnName = column?.Name ?? member?.Name;
            return this.GetFormatName(columnName);
        }

        /// <summary>
        /// 获取格式化名称
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns></returns>
        public string GetFormatName(string name)
        {
            if (string.IsNullOrEmpty(name)) return string.Empty;

            if (!name.StartsWith("[", "`", "\""))
            {
                name = string.Format(this.FormatTemp, name);
            }
            return name;
        }  
        #endregion

        #endregion
    }
}
