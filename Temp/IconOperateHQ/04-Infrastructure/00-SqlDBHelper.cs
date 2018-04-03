using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Reflection;

namespace Shaka.Infrastructure
{
    /// <summary>
    /// Copyright      : 
    /// Remarks        : 外部启动事务后，必须调用提交回滚
    /// </summary>
    /// <version v="1.0">
    /// Completion Date: 2013-05-09
    /// Author         : Shaka
    /// </version>
    public class SqlDBHelper : DBBase
    {
        #region 数据库对象
        private SqlConnection _conn;
        private SqlTransaction _sqlTran;
        private static readonly string _connStr = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        #endregion

        /// <summary>
        /// 空构造 默认用配置文件的connStr
        /// </summary>
        public SqlDBHelper() : this(ConfigurationManager.ConnectionStrings["conn"].ConnectionString)
        { }

        public SqlDBHelper(string connStr)
        {
            _conn = new SqlConnection(connStr);
        }

        public override List<T> Reader<T>(string cmdText, params SqlParameter[] pars)
        {
            return this.Reader<T>(cmdText, CommandType.Text, pars);
        }

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <typeparam name="T">结果集元素类型</typeparam>
        /// <returns>List结果集</returns>
        public override List<T> Reader<T>(string cmdText, CommandType cmdType, params SqlParameter[] pars)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, cmdText, cmdType, pars);
            SqlDataReader dr = null;
            try
            {
                dr = cmd.ExecuteReader();

                List<T> tList = new List<T>();
                if (dr.FieldCount == 0)
                {
                    return tList;
                }
                List<string> drCols = dr.GetSchemaTable().AsEnumerable().Select(r =>
                    r.Field<string>("ColumnName").ToUpper()).ToList();

                base.InitORM<T>();
                while (dr.Read())
                {
                    T t = new T();
                    List<ORMInfo> ormList = DBBase.OrmInfoDic[typeof(T)];

                    foreach (ORMInfo orm in ormList)
                    {
                        PropertyInfo pro = typeof(T).GetProperty(orm.ProName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                        string colName = orm.ColumnName.ToUpper();
                        if (pro != null && pro.CanWrite && drCols.Contains(colName))
                        {
                            object tValue = dr[colName];
                            if (tValue != DBNull.Value)
                            {
                                pro.SetValue(t, Convert.ChangeType(tValue, pro.PropertyType), null);
                            }
                        }
                    }
                    tList.Add(t);
                }
                return tList;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dr != null)
                {
                    dr.Close();
                    dr.Dispose();
                }
                CloseConn();
            }
        }

        /// <summary>
        /// 实体类插入数据库 必须手动加事务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool InsertOrUpdateTList<T>(T t) where T : class
        {
            List<T> tList = new List<T>();
            tList.Add(t);
            return InsertOrUpdateTList(tList);
        }

        /// <summary>
        /// 实体类插入数据库 必须手动加事务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tList"></param>
        /// <returns>成功失败</returns>
        public bool InsertOrUpdateTList<T>(List<T> tList) where T : class
        {
            base.InitORM<T>();
            StringBuilder insertSB = new StringBuilder();
            //列名
            StringBuilder colsSB = new StringBuilder();
            StringBuilder colsMergeWithoutIdentitySB = new StringBuilder();
            StringBuilder colsMergeAssignmentSB = new StringBuilder();
            StringBuilder colsMergeOnSB = new StringBuilder();
            StringBuilder colsValueSB = new StringBuilder();
            //列值
            List<ORMInfo> ormList = DBBase.OrmInfoDic[typeof(T)];
            string tableName = ormList[0].TableName;
            ORMInfo.QuoteDic = new System.Collections.Concurrent.ConcurrentDictionary<string, bool>();
            List<SqlParameter> pars = new List<SqlParameter>();

            int a = 0, b = 0, c = 0;
            bool hasIdentity = false;
            List<string> computedColumnList = new List<string>();
            //创建临时表
            string createTempStr = string.Format(" select * into [#{0}] from [{0}] where 1!=1", tableName);
            insertSB.Append(createTempStr);
            foreach (T t in tList)
            {
                colsSB.Clear();
                colsValueSB.Clear();

                foreach (ORMInfo orm in ormList)
                {
                    if (orm.IsIdentity)
                    {
                        hasIdentity = true;
                    }
                    if (orm.IsComputedColumn)
                    {
                        if (!computedColumnList.Contains(orm.ColumnName))
                        {
                            computedColumnList.Add(orm.ColumnName);
                        }
                        continue;
                    }
                    colsSB.Append(string.Format(",[{0}]", orm.ColumnName));
                    PropertyInfo pro = typeof(T).GetProperty(orm.ProName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                    var proValue = pro.GetValue(t, null);

                    if (orm.SqlType == SqlTypeE.datetime)
                    {
                        colsValueSB.Append(string.Format(",@{0}{1}", orm.ColumnName, a));
                        pars.Add(GetPar(orm.ColumnName + a, proValue, SqlDbType.DateTime));
                        a++;
                    }
                    else if (orm.SqlType == SqlTypeE.bit)
                    {
                        colsValueSB.Append(string.Format(",@{0}{1}", orm.ColumnName, b));
                        pars.Add(GetPar(orm.ColumnName + b, proValue, SqlDbType.Bit));
                        b++;
                    }
                    else if (orm.SqlType == SqlTypeE.image)
                    {
                        colsValueSB.Append(string.Format(",@{0}{1}", orm.ColumnName, c));
                        pars.Add(GetPar(orm.ColumnName + c, proValue, SqlDbType.Image));
                        c++;
                    }
                    else
                    {
                        if (orm.IsQuote)
                        {
                            colsValueSB.Append(string.Format(",N'{0}'", proValue));
                        }
                        else
                        {
                            colsValueSB.Append(string.Format(",{0}", proValue));
                        }
                    }
                }
                insertSB.Append(string.Format("; \r\n insert into [#{0}]({1}) values({2}) ", tableName,
                    colsSB.Remove(0, 1).ToString(), colsValueSB.Remove(0, 1).ToString()));
            }
            if (hasIdentity)
            {
                insertSB.Insert(createTempStr.Length, string.Format("; \r\n set IDENTITY_INSERT [#{0}] on", tableName));
            }
            foreach (string col in computedColumnList)
            {
                insertSB.Insert(createTempStr.Length, string.Format("; \r\n alter table [#{0}] drop column [{1}]", tableName, col));
            }
            //Merge数据
            insertSB.Append(string.Format("; \r\n declare @countrow int; \r\n select @countrow = count(1) from [{0}] ", tableName));
            foreach (ORMInfo orm in ormList)
            {
                if (orm.IsComputedColumn)
                {
                    continue;
                }
                if (!orm.IsIdentity)
                {
                    colsMergeWithoutIdentitySB.Append(string.Format(",[{0}]", orm.ColumnName));
                    if (!orm.Primaykey)
                    {
                        colsMergeAssignmentSB.Append(string.Format(",[{0}]=[#{1}].[{0}] ", orm.ColumnName, tableName));
                    }
                }
                if (orm.Primaykey)
                {
                    colsMergeOnSB.Append(string.Format(" and [#{0}].[{1}] =[{0}].[{1}] ", tableName, orm.ColumnName));
                }
            }

            insertSB.Append(";  \r\n  if(@countrow!=0) \r\n begin");
            insertSB.Append(string.Format(" ; \r\n merge into [{0}] using [#{0}] on 1=1 ", tableName));
            insertSB.Append(colsMergeOnSB.ToString());
            insertSB.Append("\r\n when not matched ");
            insertSB.Append(string.Format("\r\n then insert({0}) values({0}) ", colsMergeWithoutIdentitySB.Remove(0, 1).ToString()));
            insertSB.Append("\r\n when matched ");
            insertSB.Append(string.Format("\r\n then update set {0} ", colsMergeAssignmentSB.Remove(0, 1).ToString()));
            insertSB.Append(";\r\n end");

            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, insertSB.ToString(), CommandType.Text, pars.ToArray());
            int line = 0;
            try
            {
                line = cmd.ExecuteNonQuery();
                return true;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConn();
            }
        }

        public int ExecuteNonQuery(string cmdStr, params SqlParameter[] pars)
        {
            return this.ExecuteNonQuery(cmdStr, CommandType.Text, pars);
        }

        /// <summary>
        /// 使用事务执行多条Sql语句(有多少条就执行多少次,手动处理事务)
        /// </summary>
        /// <param name="cmdStr">执行语句</param>
        /// <param name="pars">参数</param>
        public int ExecuteNonQuery(string cmdStr, CommandType cmdType, params SqlParameter[] pars)
        {
            int line = -1;
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, cmdStr, cmdType, pars);
            try
            {
                line = cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConn();
            }
            return line;
        }

        public object ExecuteScalar(string cmdStr, params SqlParameter[] pars)
        {
            return this.ExecuteScalar(cmdStr, CommandType.Text, pars);
        }

        public object ExecuteScalar(string cmdStr, CommandType cmdType, params SqlParameter[] pars)
        {
            object obj = null;
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, cmdStr, cmdType, pars);
            try
            {
                obj = cmd.ExecuteScalar();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConn();
            }
            return obj;
        }

        /// <summary>
        /// 配置SqlCommand
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="cmdType"></param>
        /// <param name="pars"></param>
        private void PrepareCommand(SqlCommand cmd, string cmdText, CommandType cmdType, params SqlParameter[] pars)
        {
            OpenConn();
            cmd.Connection = _conn;
            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;
            cmd.Parameters.Clear();
            if (pars != null && pars.Length > 0)
            {
                cmd.Parameters.AddRange(pars);
            }
            if (_sqlTran != null)
            {
                cmd.Transaction = _sqlTran;
            }
        }

        /// <summary>
        /// 打开连接
        /// </summary>
        public void OpenConn()
        {
            if (_conn.State != ConnectionState.Open)
            {
                try
                {
                    _conn.Close();//针对Broken状态，应该比较少见
                    _conn.Open();
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 关闭连接
        /// </summary>
        private void CloseConn()
        {
            if (_conn.State != ConnectionState.Closed && _sqlTran == null)
            {
                _conn.Close();
            }
        }

        /// <summary>
        /// 开始事务
        /// </summary>
        public void BeginTran()
        {
            OpenConn();
            _sqlTran = _conn.BeginTransaction();
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        public void CommitTran()
        {
            _sqlTran.Commit();
            _conn.Close();
        }

        /// <summary>
        /// 回滚事务
        /// </summary>
        public void RollBackTran()
        {
            _sqlTran.Rollback();
            _conn.Close();
        }
    }
}