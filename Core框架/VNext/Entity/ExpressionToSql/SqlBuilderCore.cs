using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using VNext.Extensions;

namespace VNext.Entity
{
    /// <summary>
    /// SQL构建核心
    /// </summary>
    /// <typeparam name="T">泛型类型</typeparam>
    public class SqlBuilderCore<T> where T : class, IEntityDapper
    {
        private SqlCreator sqlCreator;

        public SqlBuilderCore() : this(DatabaseType.SqlServer)
        {

        }

        public SqlBuilderCore(DatabaseType databaseType, ILogger logger = null, Action<string, object, ILogger> sqlIntercept = null)
        {
            this.sqlCreator = new SqlCreator(typeof(T), databaseType);
            this.SqlIntercept = sqlIntercept;
            this.Logger = logger;
        }

        #region 属性
        public ILogger Logger { get; set; } = null;

        /// <summary>
        /// SQL拦截委托
        /// </summary>
        public Action<string, object, ILogger> SqlIntercept { get; set; }

        /// <summary>
        /// SQL语句
        /// </summary>
        public string Sql
        {
            get
            {
                var sql = this.sqlCreator.ToString();
                this.SqlIntercept?.Invoke(sql, this.sqlCreator.Parameters, Logger);
                //添加sql日志拦截
                return sql;
            }
        }

        /// <summary>
        /// SQL格式化参数
        /// </summary>
        public Dictionary<string, object> Parameters => this.sqlCreator.Parameters;

        private string As
        {
            get
            {
                if (this.sqlCreator.DatabaseType == DatabaseType.Oracle)
                    return " ";
                else
                    return " AS ";
            }
        }
        #endregion

        #region 方法
        #region Clear
        /// <summary>
        /// Clear
        /// </summary>
        public void Clear()
        {
            this.sqlCreator.Clear();
        }
        #endregion

        #region Select
        /// <summary>
        /// SelectParser
        /// </summary>
        /// <param name="array">可变数量参数</param>
        /// <returns>string</returns>
        private string SelectParser(params Type[] array)
        {
            this.sqlCreator.Clear();
            this.sqlCreator.SetTypeDictionary(array);

            var entityInfo = this.sqlCreator.GetEntityInfo(typeof(T));
            return $"SELECT {{0}} FROM {entityInfo.tableName}{As}{entityInfo.tableAliasName}";
        }

        /// <summary>
        /// Select
        /// </summary>
        /// <param name="expression">表达式树</param>
        /// <returns>SqlBuilderCore</returns>
        public SqlBuilderCore<T> Select(Expression<Func<T, object>> expression = null)
        {
            var sql = SelectParser(typeof(T));
            if (expression != null) this.sqlCreator.Select(expression.Body);
            this.sqlCreator.SqlString.AppendFormat(sql, this.sqlCreator.SelectFieldsStr);
            return this;
        }

        /// <summary>
        /// Select
        /// </summary>
        /// <typeparam name="T2">泛型类型2</typeparam>
        /// <param name="expression">表达式树</param>
        /// <returns>SqlBuilderCore</returns>
        public SqlBuilderCore<T> Select<T2>(Expression<Func<T, T2, object>> expression = null)
            where T2 : class
        {
            var sql = SelectParser(typeof(T), typeof(T2));
            if (expression != null) this.sqlCreator.Select(expression.Body);

            this.sqlCreator.SqlString.AppendFormat(sql, this.sqlCreator.SelectFieldsStr);
            return this;
        }

        /// <summary>
        /// Select
        /// </summary>
        /// <typeparam name="T2">泛型类型2</typeparam>
        /// <typeparam name="T3">泛型类型3</typeparam>
        /// <param name="expression">表达式树</param>
        /// <returns>SqlBuilderCore</returns>
        public SqlBuilderCore<T> Select<T2, T3>(Expression<Func<T, T2, T3, object>> expression = null)
            where T2 : class
            where T3 : class
        {
            var sql = SelectParser(typeof(T), typeof(T2), typeof(T3));
            if (expression != null) this.sqlCreator.Select(expression.Body);
         
            this.sqlCreator.SqlString.AppendFormat(sql, this.sqlCreator.SelectFieldsStr);
            return this;
        }

        /// <summary>
        /// Select
        /// </summary>
        /// <typeparam name="T2">泛型类型2</typeparam>
        /// <typeparam name="T3">泛型类型3</typeparam>
        /// <typeparam name="T4">泛型类型4</typeparam>
        /// <param name="expression">表达式树</param>
        /// <returns>SqlBuilderCore</returns>
        public SqlBuilderCore<T> Select<T2, T3, T4>(Expression<Func<T, T2, T3, T4, object>> expression = null)
            where T2 : class
            where T3 : class
            where T4 : class
        {
            var sql = SelectParser(typeof(T), typeof(T2), typeof(T3), typeof(T4));
            if (expression != null) this.sqlCreator.Select(expression.Body);

            this.sqlCreator.SqlString.AppendFormat(sql, this.sqlCreator.SelectFieldsStr);
            return this;
        }
        #endregion

        #region Join
        /// <summary>
        /// JoinParser
        /// </summary>
        /// <typeparam name="T2">泛型类型2</typeparam>
        /// <param name="expression">表达式树</param>
        /// <param name="leftOrRightJoin">左连接或者右连接</param>
        /// <returns>SqlBuilderCore</returns>
        private SqlBuilderCore<T> JoinParser<T2>(Expression<Func<T, T2, bool>> expression, string leftOrRightJoin = "")
            where T2 : class
        {
            var entityInfo = this.sqlCreator.GetEntityInfo(typeof(T2));
            this.sqlCreator.SqlString.Append($"{(string.IsNullOrEmpty(leftOrRightJoin) ? "" : " " + leftOrRightJoin)} JOIN {entityInfo.tableName}{As}{entityInfo.tableAliasName} ON ");
            this.sqlCreator.Join(expression.Body);
            return this;
        }

        /// <summary>
        /// JoinParser2
        /// </summary>
        /// <typeparam name="T2">泛型类型2</typeparam>
        /// <typeparam name="T3">泛型类型3</typeparam>
        /// <param name="expression">表达式树</param>
        /// <param name="leftOrRightJoin">左连接或者右连接</param>
        /// <returns>SqlBuilderCore</returns>
        private SqlBuilderCore<T> JoinParser2<T2, T3>(Expression<Func<T2, T3, bool>> expression, string leftOrRightJoin = "")
            where T2 : class
            where T3 : class
        {
            var entityInfo = this.sqlCreator.GetEntityInfo(typeof(T3));
            this.sqlCreator.SqlString.Append($"{(string.IsNullOrEmpty(leftOrRightJoin) ? "" : " " + leftOrRightJoin)} JOIN {entityInfo.tableName}{As}{entityInfo.tableAliasName} ON ");
            this.sqlCreator.Join(expression.Body);
            return this;
        }

        /// <summary>
        /// Join
        /// </summary>
        /// <typeparam name="T2">泛型类型2</typeparam>
        /// <param name="expression">表达式树</param>
        /// <returns>SqlBuilderCore</returns>
        public SqlBuilderCore<T> Join<T2>(Expression<Func<T, T2, bool>> expression)
            where T2 : class
        {
            return JoinParser(expression);
        }

        /// <summary>
        /// Join
        /// </summary>
        /// <typeparam name="T2">泛型类型2</typeparam>
        /// <typeparam name="T3">泛型类型3</typeparam>
        /// <param name="expression">表达式树</param>
        /// <returns>SqlBuilderCore</returns>
        public SqlBuilderCore<T> Join<T2, T3>(Expression<Func<T2, T3, bool>> expression)
            where T2 : class
            where T3 : class
        {
            return JoinParser2(expression);
        }

        /// <summary>
        /// InnerJoin
        /// </summary>
        /// <typeparam name="T2">泛型类型2</typeparam>
        /// <param name="expression">表达式树</param>
        /// <returns>SqlBuilderCore</returns>
        public SqlBuilderCore<T> InnerJoin<T2>(Expression<Func<T, T2, bool>> expression)
            where T2 : class
        {
            return JoinParser(expression, "INNER");
        }

        /// <summary>
        /// InnerJoin
        /// </summary>
        /// <typeparam name="T2">泛型类型2</typeparam>
        /// <typeparam name="T3">泛型类型3</typeparam>
        /// <param name="expression">表达式树</param>
        /// <returns>SqlBuilderCore</returns>
        public SqlBuilderCore<T> InnerJoin<T2, T3>(Expression<Func<T2, T3, bool>> expression)
            where T2 : class
            where T3 : class
        {
            return JoinParser2(expression, "INNER");
        }

        /// <summary>
        /// LeftJoin
        /// </summary>
        /// <typeparam name="T2">泛型类型2</typeparam>
        /// <param name="expression">表达式树</param>
        /// <returns>SqlBuilderCore</returns>
        public SqlBuilderCore<T> LeftJoin<T2>(Expression<Func<T, T2, bool>> expression)
            where T2 : class
        {
            return JoinParser(expression, "LEFT");
        }

        /// <summary>
        /// LeftJoin
        /// </summary>
        /// <typeparam name="T2">泛型类型2</typeparam>
        /// <typeparam name="T3">泛型类型3</typeparam>
        /// <param name="expression">表达式树</param>
        /// <returns>SqlBuilderCore</returns>
        public SqlBuilderCore<T> LeftJoin<T2, T3>(Expression<Func<T2, T3, bool>> expression)
            where T2 : class
            where T3 : class
        {
            return JoinParser2(expression, "LEFT");
        }

        /// <summary>
        /// RightJoin
        /// </summary>
        /// <typeparam name="T2">泛型类型2</typeparam>
        /// <param name="expression">表达式树</param>
        /// <returns>SqlBuilderCore</returns>
        public SqlBuilderCore<T> RightJoin<T2>(Expression<Func<T, T2, bool>> expression)
            where T2 : class
        {
            return JoinParser(expression, "RIGHT");
        }

        /// <summary>
        /// RightJoin
        /// </summary>
        /// <typeparam name="T2">泛型类型2</typeparam>
        /// <typeparam name="T3">泛型类型3</typeparam>
        /// <param name="expression">表达式树</param>
        /// <returns>SqlBuilderCore</returns>
        public SqlBuilderCore<T> RightJoin<T2, T3>(Expression<Func<T2, T3, bool>> expression)
            where T2 : class
            where T3 : class
        {
            return JoinParser2(expression, "RIGHT");
        }

        /// <summary>
        /// FullJoin
        /// </summary>
        /// <typeparam name="T2">泛型类型2</typeparam>
        /// <param name="expression">表达式树</param>
        /// <returns>SqlBuilderCore</returns>
        public SqlBuilderCore<T> FullJoin<T2>(Expression<Func<T, T2, bool>> expression)
            where T2 : class
        {
            return JoinParser(expression, "FULL");
        }

        /// <summary>
        /// FullJoin
        /// </summary>
        /// <typeparam name="T2">泛型类型2</typeparam>
        /// <typeparam name="T3">泛型类型3</typeparam>
        /// <param name="expression">表达式树</param>
        /// <returns>SqlBuilderCore</returns>
        public SqlBuilderCore<T> FullJoin<T2, T3>(Expression<Func<T2, T3, bool>> expression)
            where T2 : class
            where T3 : class
        {
            return JoinParser2(expression, "FULL");
        }
        #endregion

        #region Where
        /// <summary>
        /// Where
        /// </summary>
        /// <param name="expression">表达式树</param>
        /// <returns>SqlBuilderCore</returns>
        public SqlBuilderCore<T> Where(Expression<Func<T, bool>> expression)
        {
            if (!(expression.Body.NodeType == ExpressionType.Constant && expression.Body.ToObject() is bool b && b))
            {
                this.sqlCreator += " WHERE ";
                this.sqlCreator.Where(expression.Body);
            }
            return this;
        }
        #endregion

        #region AndWhere
        /// <summary>
        /// AndWhere
        /// </summary>
        /// <param name="expression">表达式树</param>
        /// <returns>SqlBuilderCore</returns>
        public SqlBuilderCore<T> AndWhere(Expression<Func<T, bool>> expression)
        {
            var sql = this.sqlCreator.ToString();
            if (sql.Contains("WHERE") && !string.IsNullOrEmpty(sql.Substring("WHERE").Trim()))
            {
                this.sqlCreator += " AND ";
            }
            else
            {
                this.sqlCreator += " WHERE ";
            }
            this.sqlCreator += "(";
            this.sqlCreator.Where(expression.Body);
            this.sqlCreator += ")";
            return this;
        }
        #endregion

        #region OrWhere
        /// <summary>
        /// OrWhere
        /// </summary>
        /// <param name="expression">表达式树</param>
        /// <returns>SqlBuilderCore</returns>
        public SqlBuilderCore<T> OrWhere(Expression<Func<T, bool>> expression)
        {
            var sql = this.sqlCreator.ToString();
            if (sql.Contains("WHERE") && !string.IsNullOrEmpty(sql.Substring("WHERE").Trim()))
            {
                this.sqlCreator += " OR ";
            }
            else
            {
                this.sqlCreator += " WHERE ";
            }
            this.sqlCreator += "(";
            this.sqlCreator.Where(expression.Body);
            this.sqlCreator += ")";
            return this;
        }
        #endregion

        #region GroupBy
        /// <summary>
        /// GroupBy
        /// </summary>
        /// <param name="expression">表达式树</param>
        /// <returns>SqlBuilderCore</returns>
        public SqlBuilderCore<T> GroupBy(Expression<Func<T, object>> expression)
        {
            this.sqlCreator += " GROUP BY ";
            this.sqlCreator.GroupBy(expression.Body);
            return this;
        }
        #endregion

        #region OrderBy
        /// <summary>
        /// OrderBy
        /// </summary>
        /// <param name="expression">表达式树</param>
        /// <param name="orders">排序方式</param>
        /// <returns>SqlBuilderCore</returns>
        public SqlBuilderCore<T> OrderBy(Expression<Func<T, object>> expression, params OrderType[] orders)
        {
            this.sqlCreator += " ORDER BY ";
            this.sqlCreator.OrderBy(expression.Body, orders);
            return this;
        }
        #endregion

        #region Page
        /// <summary>
        /// Page
        /// </summary>
        /// <param name="pageSize">每页数量</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="orderField">排序字段</param>
        /// <param name="sql">自定义sql语句</param>
        /// <param name="parameters">自定义sql格式化参数</param>
        /// <returns>SqlBuilderCore</returns>
        public SqlBuilderCore<T> Page(int pageSize, int pageIndex, string orderField, string sql = null, Dictionary<string, object> parameters = null)
        {
            var sb = new StringBuilder();
            if (!orderField.ToUpper().Contains(" ASC") && !orderField.ToUpper().Contains(" DESC"))
                orderField = this.sqlCreator.GetFormatName(orderField);
            if (!string.IsNullOrEmpty(sql))
            {
                this.sqlCreator.Parameters.Clear();
                if (parameters != null) this.sqlCreator.Parameters = parameters;
            }
            sql = string.IsNullOrEmpty(sql) ? this.sqlCreator.SqlString.ToString().TrimEnd(';') : sql.TrimEnd(';');
            //SqlServer
            if (this.sqlCreator.DatabaseType == DatabaseType.SqlServer)
            {
                if (Regex.IsMatch(sql, "WITH", RegexOptions.IgnoreCase))
                    sb.Append($"IF OBJECT_ID(N'TEMPDB..#TEMPORARY') IS NOT NULL DROP TABLE #TEMPORARY;{sql} SELECT * INTO #TEMPORARY FROM T;SELECT COUNT(1) AS Total FROM #TEMPORARY;WITH R AS (SELECT ROW_NUMBER() OVER (ORDER BY {orderField}) AS RowNumber,* FROM #TEMPORARY) SELECT * FROM R  WHERE RowNumber BETWEEN {((pageIndex - 1) * pageSize + 1)} AND {(pageIndex * pageSize)};DROP TABLE #TEMPORARY;");
                else
                    sb.Append($"IF OBJECT_ID(N'TEMPDB..#TEMPORARY') IS NOT NULL DROP TABLE #TEMPORARY;SELECT * INTO #TEMPORARY FROM ({sql}) AS T;SELECT COUNT(1) AS Total FROM #TEMPORARY;SELECT * FROM (SELECT ROW_NUMBER() OVER (ORDER BY {orderField}) AS RowNumber, * FROM #TEMPORARY) AS N WHERE RowNumber BETWEEN {((pageIndex - 1) * pageSize + 1)} AND {(pageIndex * pageSize)};DROP TABLE #TEMPORARY;");
            }
            //Oracle，注意Oracle需要分开查询总条数和分页数据
            if (this.sqlCreator.DatabaseType == DatabaseType.Oracle)
            {
                if (Regex.IsMatch(sql, "WITH", RegexOptions.IgnoreCase))
                    sb.Append($"{sql},R AS (SELECT ROWNUM AS RowNumber,T.* FROM T WHERE ROWNUM <= {pageSize * pageIndex} ORDER BY {orderField}) SELECT * FROM R WHERE RowNumber>={pageSize * (pageIndex - 1) + 1}");
                else
                    sb.Append($"SELECT * FROM (SELECT X.*,ROWNUM AS RowNumber FROM ({sql} ORDER BY {orderField}) X WHERE ROWNUM <= {pageSize * pageIndex}) T WHERE T.RowNumber >= {pageSize * (pageIndex - 1) + 1}");
            }
            //MySql，注意8.0版本才支持WITH语法
            if (this.sqlCreator.DatabaseType == DatabaseType.MySql)
            {
                if (Regex.IsMatch(sql, "WITH", RegexOptions.IgnoreCase))
                    sb.Append($"{sql} SELECT COUNT(1) AS Total FROM T;{sql} SELECT * FROM T ORDER BY {orderField} LIMIT {pageSize} OFFSET {(pageSize * (pageIndex - 1))};");
                else
                    sb.Append($"DROP TEMPORARY TABLE IF EXISTS $TEMPORARY;CREATE TEMPORARY TABLE $TEMPORARY SELECT * FROM ({sql}) AS T;SELECT COUNT(1) AS Total FROM $TEMPORARY;SELECT * FROM $TEMPORARY AS X ORDER BY {orderField} LIMIT {pageSize} OFFSET {(pageSize * (pageIndex - 1))};DROP TABLE $TEMPORARY;");
            }
            //PostgreSql
            if (this.sqlCreator.DatabaseType == DatabaseType.PostgreSql)
            {
                if (Regex.IsMatch(sql, "WITH", RegexOptions.IgnoreCase))
                    sb.Append($"{sql} SELECT COUNT(1) AS Total FROM T;{sql} SELECT * FROM T ORDER BY {orderField} LIMIT {pageSize} OFFSET {(pageSize * (pageIndex - 1))};");
                else
                    sb.Append($"DROP TABLE IF EXISTS TEMPORARY_TABLE;CREATE TEMPORARY TABLE TEMPORARY_TABLE AS SELECT * FROM ({sql}) AS T;SELECT COUNT(1) AS Total FROM TEMPORARY_TABLE;SELECT * FROM TEMPORARY_TABLE AS X ORDER BY {orderField} LIMIT {pageSize} OFFSET {(pageSize * (pageIndex - 1))};DROP TABLE TEMPORARY_TABLE;");
            }
            //SqLite
            if (this.sqlCreator.DatabaseType == DatabaseType.Sqlite)
            {
                if (Regex.IsMatch(sql, "WITH", RegexOptions.IgnoreCase))
                    sb.Append($"{sql} SELECT COUNT(1) AS Total FROM T;{sql} SELECT * FROM T ORDER BY {orderField} LIMIT {pageSize} OFFSET {(pageSize * (pageIndex - 1))};");
                else
                    sb.Append($"SELECT COUNT(1) AS Total FROM ({sql}) AS T;SELECT * FROM ({sql}) AS X ORDER BY {orderField} LIMIT {pageSize} OFFSET {(pageSize * (pageIndex - 1))};");
            }
            this.sqlCreator.SqlString.Clear().Append(sb);
            return this;
        }

        /// <summary>
        /// PageByWith
        /// </summary>
        /// <param name="pageSize">每页数量</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="orderField">排序字段</param>
        /// <param name="sql">自定义sql语句</param>
        /// <param name="parameters">自定义sql格式化参数</param>
        /// <returns>SqlBuilderCore</returns>
        public SqlBuilderCore<T> PageByWith(int pageSize, int pageIndex, string orderField, string sql = null, Dictionary<string, object> parameters = null)
        {
            var sb = new StringBuilder();
            if (!orderField.ToUpper().Contains(" ASC") && !orderField.ToUpper().Contains(" DESC"))
                orderField = this.sqlCreator.GetFormatName(orderField);
            if (!string.IsNullOrEmpty(sql))
            {
                this.sqlCreator.Parameters.Clear();
                if (parameters != null) this.sqlCreator.Parameters = parameters;
            }
            sql = string.IsNullOrEmpty(sql) ? this.sqlCreator.SqlString.ToString().TrimEnd(';') : sql.TrimEnd(';');
            //SqlServer
            if (this.sqlCreator.DatabaseType == DatabaseType.SqlServer)
            {
                if (Regex.IsMatch(sql, "WITH", RegexOptions.IgnoreCase))
                    sb.Append($"IF OBJECT_ID(N'TEMPDB..#TEMPORARY') IS NOT NULL DROP TABLE #TEMPORARY;{sql} SELECT * INTO #TEMPORARY FROM T;SELECT COUNT(1) AS Total FROM #TEMPORARY;WITH R AS (SELECT ROW_NUMBER() OVER (ORDER BY {orderField}) AS RowNumber,* FROM #TEMPORARY) SELECT * FROM R  WHERE RowNumber BETWEEN {((pageIndex - 1) * pageSize + 1)} AND {(pageIndex * pageSize)};DROP TABLE #TEMPORARY;");
                else
                    sb.Append($"IF OBJECT_ID(N'TEMPDB..#TEMPORARY') IS NOT NULL DROP TABLE #TEMPORARY;WITH T AS ({sql}) SELECT * INTO #TEMPORARY FROM T;SELECT COUNT(1) AS Total FROM #TEMPORARY;WITH R AS (SELECT ROW_NUMBER() OVER (ORDER BY {orderField}) AS RowNumber,* FROM #TEMPORARY) SELECT * FROM R  WHERE RowNumber BETWEEN {((pageIndex - 1) * pageSize + 1)} AND {(pageIndex * pageSize)};DROP TABLE #TEMPORARY;");
            }
            //Oracle，注意Oracle需要分开查询总条数和分页数据
            if (this.sqlCreator.DatabaseType == DatabaseType.Oracle)
            {
                if (Regex.IsMatch(sql, "WITH", RegexOptions.IgnoreCase))
                    sb.Append($"{sql},R AS (SELECT ROWNUM AS RowNumber,T.* FROM T WHERE ROWNUM <= {pageSize * pageIndex} ORDER BY {orderField}) SELECT * FROM R WHERE RowNumber>={pageSize * (pageIndex - 1) + 1}");
                else
                    sb.Append($"WITH T AS ({sql}),R AS (SELECT ROWNUM AS RowNumber,T.* FROM T WHERE ROWNUM <= {pageSize * pageIndex} ORDER BY {orderField}) SELECT * FROM R WHERE RowNumber>={pageSize * (pageIndex - 1) + 1}");
            }
            //MySql，注意8.0版本才支持WITH语法
            if (this.sqlCreator.DatabaseType == DatabaseType.MySql)
            {
                if (Regex.IsMatch(sql, "WITH", RegexOptions.IgnoreCase))
                    sb.Append($"{sql} SELECT COUNT(1) AS Total FROM T;{sql} SELECT * FROM T ORDER BY {orderField} LIMIT {pageSize} OFFSET {(pageSize * (pageIndex - 1))};");
                else
                    sb.Append($"WITH T AS ({sql}) SELECT COUNT(1) AS Total FROM T;WITH T AS ({sql}) SELECT * FROM T ORDER BY {orderField} LIMIT {pageSize} OFFSET {(pageSize * (pageIndex - 1))};");
            }
            //PostgreSql
            if (this.sqlCreator.DatabaseType == DatabaseType.PostgreSql)
            {
                if (Regex.IsMatch(sql, "WITH", RegexOptions.IgnoreCase))
                    sb.Append($"{sql} SELECT COUNT(1) AS Total FROM T;{sql} SELECT * FROM T ORDER BY {orderField} LIMIT {pageSize} OFFSET {(pageSize * (pageIndex - 1))};");
                else
                    sb.Append($"WITH T AS ({sql}) SELECT COUNT(1) AS Total FROM T;WITH T AS ({sql}) SELECT * FROM T ORDER BY {orderField} LIMIT {pageSize} OFFSET {(pageSize * (pageIndex - 1))};");
            }
            //SqLite
            if (this.sqlCreator.DatabaseType == DatabaseType.Sqlite)
            {
                if (Regex.IsMatch(sql, "WITH", RegexOptions.IgnoreCase))
                    sb.Append($"{sql} SELECT COUNT(1) AS Total FROM T;{sql} SELECT * FROM T ORDER BY {orderField} LIMIT {pageSize} OFFSET {(pageSize * (pageIndex - 1))};");
                else
                    sb.Append($"WITH T AS ({sql}) SELECT COUNT(1) AS Total FROM T;WITH T AS ({sql}) SELECT * FROM T ORDER BY {orderField} LIMIT {pageSize} OFFSET {(pageSize * (pageIndex - 1))};");
            }
            this.sqlCreator.SqlString.Clear().Append(sb);
            return this;
        }
        #endregion

        #region Top
        /// <summary>
        /// Top
        /// </summary>
        /// <param name="topNumber">top数量</param>
        /// <returns>SqlBuilderCore</returns>
        public SqlBuilderCore<T> Top(long topNumber)
        {
            if (this.sqlCreator.DatabaseType == DatabaseType.SqlServer)
            {
                if (this.sqlCreator.SqlString.ToString().ToUpper().Contains("DISTINCT"))
                {
                    this.sqlCreator.SqlString.Replace("DISTINCT", $"DISTINCT TOP {topNumber}", this.sqlCreator.SqlString.ToString().IndexOf("DISTINCT"), 8);
                }
                else
                {
                    this.sqlCreator.SqlString.Replace("SELECT", $"SELECT TOP {topNumber}", this.sqlCreator.SqlString.ToString().IndexOf("SELECT"), 6);
                }
            }
            else if (this.sqlCreator.DatabaseType == DatabaseType.Oracle)
            {
                if (this.sqlCreator.SqlString.ToString().ToUpper().Contains("WHERE"))
                {
                    this.sqlCreator.SqlString.Append($" AND ROWNUM <= {topNumber}");
                }
                else
                {
                    this.sqlCreator.SqlString.Append($" WHERE ROWNUM <= {topNumber}");
                }
            }
            else if (this.sqlCreator.DatabaseType == DatabaseType.MySql || this.sqlCreator.DatabaseType == DatabaseType.Sqlite || this.sqlCreator.DatabaseType == DatabaseType.PostgreSql)
            {
                this.sqlCreator.SqlString.Append($" LIMIT {topNumber} OFFSET 0");
            }
            return this;
        }
        #endregion

        #region Distinct
        /// <summary>
        /// Distinct
        /// </summary>
        /// <returns>SqlBuilderCore</returns>
        public SqlBuilderCore<T> Distinct()
        {
            this.sqlCreator.SqlString.Replace("SELECT", $"SELECT DISTINCT", this.sqlCreator.SqlString.ToString().IndexOf("SELECT"), 6);
            return this;
        }
        #endregion

        #region GetTableName
        /// <summary>
        /// 获取实体对应的表名
        /// </summary>
        /// <returns></returns>
        public string GetTableName()
        {
            return this.sqlCreator.GetTableName(typeof(T));
        }
        #endregion
        #endregion
    }
}
