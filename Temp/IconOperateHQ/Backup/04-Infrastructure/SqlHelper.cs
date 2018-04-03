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
    public class SqlDBHelper
    {
        #region 数据库对象
        private SqlConnection _conn;
        private SqlTransaction _sqlTran;
        #endregion

        public SqlDBHelper()
            : this(ConfigurationManager.ConnectionStrings["conn"].ToString())
        {
 
        }

        public SqlDBHelper(string connStr)
        {
            _conn = new SqlConnection(connStr);
        }

        public List<T> Reader<T>(string cmdText, params SqlParameter[] pars) where T : class, new()
        {
            return this.Reader<T>(cmdText, CommandType.Text, pars);
        }

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <typeparam name="T"> 结果集元素类型 </typeparam>
        /// <returns> List结果集</returns>
        public List<T> Reader<T>(string cmdText, CommandType cmdType, params SqlParameter[] pars) where T : class, new()
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

                if (dr.FieldCount == 1)
                {
                    while (dr.Read())
                    {
                        tList.Add(dr[0] as T);
                    }
                    return tList;
                }

                PropertyInfo[] tPropertys = typeof(T).GetProperties();
                List<string> drCols = dr.GetSchemaTable().AsEnumerable().Select(r =>
                    r.Field<string>("ColumnName").ToUpper()).ToList();
                while (dr.Read())
                {
                    T t = new T();
                    foreach (PropertyInfo tPt in tPropertys)
                    {
                        if (drCols.Contains(tPt.Name.ToUpper()) && tPt.CanWrite)
                        {
                            object tValue = dr[tPt.Name.ToUpper()];
                            if (tValue != DBNull.Value)
                            {
                                tPt.SetValue(t, Convert.ChangeType(tValue, tPt.PropertyType), null);
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

        public int ExecuteNonQuery(string cmdStr, params SqlParameter[] pars)
        {
            return this.ExecuteNonQuery(cmdStr, CommandType.Text, pars);
        }

        /// <summary>
        /// 使用事务执行多条 Sql语句( 有多少条就执行多少次 ,手动处理事务)
        /// </summary>
        /// <param name="cmdStr"> 执行语句</param>
        /// <param name="pars"> 参数</param>
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
            finally
            {
                CloseConn();
            }
            return obj;
        }

        /// <summary>
        /// 配置 SqlCommand
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

        public static SqlParameter GetPar(string name, SqlDbType dbType,
             object value)
        {
            return GetPar(name, dbType, value, 0, ParameterDirection.Input);
        }

        /// <summary>
        /// 添加参数 (供业务层调用)
        /// </summary>
        /// <param name="name"> 参数名</param>
        /// <param name="dbType"> 数据类型</param>
        /// <param name="dir"> 参数方向</param>
        /// <param name="size"> 参数值长度</param>
        /// <param name="value"> 参数值</param>
        public static SqlParameter GetPar(string name, SqlDbType dbType,
             object value, int size, ParameterDirection dir)
        {
            SqlParameter sqlPar = new SqlParameter();
            sqlPar.ParameterName = name;
            sqlPar.SqlDbType = dbType;
            sqlPar.Direction = dir;
            if (size != 0)
            {
                sqlPar.Size = size;
            }
            sqlPar.Value = value ?? DBNull.Value;
            return sqlPar;
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
                    _conn.Close(); //针对Broken 状态，应该比较少见
                    _conn.Open();
                }
                catch (SqlException ex)
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