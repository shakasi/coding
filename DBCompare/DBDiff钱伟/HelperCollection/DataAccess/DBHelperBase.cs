using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.HelperCollection.DataAccess.DataBase;
using System.Data.SqlClient;
using System.Data.Common;
using System.ComponentModel;
using System.Threading;
using Utility.HelperCollection.AOP;

namespace Utility.HelperCollection.DataAccess
{
    public class DBHelperBase<T> where T : EntityBase, new()
    {
        private IDataBase _DataBase;

        private string _strConn;
        public DBHelperBase(DataBaseType db, string connectionString)
        {
            _DataBase = DataBaseFactory.CreateDataBase(db);
            _strConn = connectionString;
        }
        public DBHelperBase(string dbTypeConfig, string connectionString)
        {
            _DataBase = DataBaseFactory.CreateDataBase(dbTypeConfig);
            _strConn = connectionString;
        }
        public DBHelperBase(IDataBase db, string connectionString)
        {
            _DataBase = db;
            _strConn = connectionString;
        }
        public Dictionary<string, object> ConvertEntityToDictionary(T entity)
        {
            return SqlMap<T>.GetEntityToDictionaryConverter()(entity);
        }
        public void Insert(T entity)
        {
            using (DbConnection conn = _DataBase.CreateConnection())
            {
                conn.ConnectionString = _strConn;
                SqlMap<T>.Insert(entity, conn, _DataBase);
            }
        }
        public void Insert(ICollection<T> entities)
        {
           using (DbConnection conn = _DataBase.CreateConnection())
            {
               conn.ConnectionString = _strConn;
             //  conn.Open();
               if (conn.GetType() == typeof(SqlConnection))
               {
                   SqlMap<T>.SqlBulkInsert(entities, (SqlConnection)conn);
               }
               else
               {
                   SqlMap<T>.Insert(entities, conn, _DataBase);
               }
            }
        }
        public void Delete(T entity)
        {
            using (DbConnection conn = _DataBase.CreateConnection())
            {
                conn.ConnectionString = _strConn;
                SqlMap<T>.Delete(entity, conn, _DataBase);
            }
        }
        public void Delete(ICollection<T> entities)
        {
            using (DbConnection conn = _DataBase.CreateConnection())
            {
                conn.ConnectionString = _strConn;
                SqlMap<T>.Delete(entities, conn, _DataBase);
            }
        }
        public void Update(T entity)
        {
            using (DbConnection conn = _DataBase.CreateConnection())
            {
                conn.ConnectionString = _strConn;
                SqlMap<T>.Update(entity, conn, _DataBase);
            }
        }
        public void Update(ICollection<T> entities)
        {
            using (DbConnection conn = _DataBase.CreateConnection())
            {
                conn.ConnectionString = _strConn;
                SqlMap<T>.Update(entities, conn, _DataBase);
            }
        }
        public EntityCollection<T> SelectAll(bool useAop)
        {
            EntityCollection<T> result = new EntityCollection<T>(this);
            result.IsReadOnly = !useAop;
            using (DbConnection conn = _DataBase.CreateConnection())
            {
                conn.ConnectionString = _strConn;
                List<T> data = SqlMap<T>.GetAllData(conn, useAop, _DataBase);
                result.Add(data);
            }
            return result;
        }
        public EntityCollection<T> SelectDataByCondition(T condition, bool useAop)
        {
            EntityCollection<T> result = new EntityCollection<T>(this);
            result.IsReadOnly = !useAop;
            using (DbConnection conn = _DataBase.CreateConnection())
            {
                conn.ConnectionString = _strConn;
                List<T> data = SqlMap<T>.GetDataByCondition (condition, conn, useAop, _DataBase);
                result.Add(data);
            }
            result.AcceptChanges();
            return result;
        }

        public EntityCollection<T> SelectDataByCondition(DbSearchConditionCollection<T> condition, bool useAop)
        {
            EntityCollection<T> result = new EntityCollection<T>(this);
            result.IsReadOnly = !useAop;
            using (DbConnection conn = _DataBase.CreateConnection())
            {
                conn.ConnectionString = _strConn;
                List<T> data = SqlMap<T>.GetDataByCondition(condition, conn, useAop, _DataBase);
                result.Add(data);
            }
            result.AcceptChanges();
            return result;
        }
        public T CreateSimpleSeachCondition()
        {
             return InstanceBuilder.CreateInstance<T>();
        }
        public DbSearchConditionCollection<T> CreateComplexSeachCondition()
        {
            return new DbSearchConditionCollection<T>();
        }
   
    }
}
