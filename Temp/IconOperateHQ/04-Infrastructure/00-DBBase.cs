using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using System.Xml;
using System.Data;
using System.Data.SqlClient;

namespace Shaka.Infrastructure
{
    public abstract class DBBase
    {
        /// <summary>
        /// model和数据库表的关系
        /// </summary>
        public static ConcurrentDictionary<Type, List<ORMInfo>> OrmInfoDic { get; }

        static DBBase()
        {
            OrmInfoDic = new ConcurrentDictionary<Type, List<ORMInfo>>();
        }

        protected void InitORM<T>()
        {
            Type t = typeof(T);
            if (!OrmInfoDic.Keys.Contains(t))
            {
                if (!OrmInfoDic.TryAdd(t, GetORMInfo(t)))
                {
                    throw new Exception("加载Mode映射实体失败！");
                }
            }
        }

        private List<ORMInfo> GetORMInfo(Type t)
        {
            List<ORMInfo> ormInfoList = new List<ORMInfo>();
            XmlHelper xml = new XmlHelper(t.Name + ".orm.xml");
            //表节点
            foreach (XmlNode node in xml.GetXmlNodesByName(xml.GetXmlNodeByName("orm-mapping"), "class"))
            {
                string tableName = xml.GetAttrValueByNode(node, "table");
                //属性节点
                foreach (XmlNode proNode in xml.GetXmlNodesByName(node, "property"))
                {
                    ORMInfo ormInfo = new ORMInfo();
                    ormInfo.TableName = tableName;
                    ormInfo.ProName = xml.GetAttrValueByNode(proNode, "name");
                    //对应列节点
                    XmlNode tbNode = xml.GetXmlNodeByName(proNode, "column");
                    ormInfo.ColumnName = xml.GetAttrValueByNode(tbNode, "name");
                    ormInfo.Length = xml.GetAttrValueByNode(tbNode, "length").To<int>();//暂时没用上
                    ormInfo.SqlType = xml.GetAttrValueByNode(tbNode, "sql-type").To<SqlTypeE>();
                    ormInfo.NotNull = xml.GetAttrValueByNode(tbNode, "not-null").To<bool>();//暂时没用上
                    ormInfo.Primaykey = xml.GetAttrValueByNode(tbNode, "primaykey").To<bool>();
                    ormInfo.IsIdentity = xml.GetAttrValueByNode(tbNode, "isIdentity").To<bool>();
                    ormInfo.IsComputedColumn = xml.GetAttrValueByNode(tbNode, "isComputedColumn").To<bool>();

                    ormInfoList.Add(ormInfo);
                }
            }
            return ormInfoList;
        }

        public abstract List<T> Reader<T>(string cmdText, params SqlParameter[] pars) where T : class, new();

        public abstract List<T> Reader<T>(string cmdText, CommandType cmdType, params SqlParameter[] pars) where T : new();


        public static SqlParameter GetPar(string name, object value = null,
            SqlDbType dbType = SqlDbType.NVarChar, int size = 0, ParameterDirection dir = ParameterDirection.Input)
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
    }

    public class ORMInfo
    {
        public static ConcurrentDictionary<string, bool> QuoteDic { get; set; }
        static ORMInfo()
        {
            QuoteDic = new ConcurrentDictionary<string, bool>();
        }

        public string ProName { get; set; }
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public int Length { get; set; }
        public SqlTypeE SqlType { get; set; }
        public bool NotNull { get; set; }
        public bool Primaykey { get; set; }

        private bool _isIdentity = false;
        public bool IsIdentity
        {
            get { return this._isIdentity; }
            set { this._isIdentity = value; }
        }

        private bool _isComputedColumn = false;
        public bool IsComputedColumn
        {
            get { return this._isComputedColumn; }
            set { this._isComputedColumn = value; }
        }

        public bool IsQuote
        {
            get
            {
                bool isQuote = true;
                if (!QuoteDic.Keys.Contains(this.ProName))
                {
                    isQuote = GetIsQuote(this.SqlType);
                    if (isQuote)
                    {
                        if (!QuoteDic.TryAdd(this.ProName, isQuote))
                        {
                            throw new Exception("加载Mode映射实体失败！");
                        }
                    }
                }
                return isQuote;
            }
        }

        private bool GetIsQuote(SqlTypeE sqlType)
        {
            bool isIsQuote = true;
            if (
                    sqlType == SqlTypeE.@int || sqlType == SqlTypeE.numeric ||
                     sqlType == SqlTypeE.@money || sqlType == SqlTypeE.@bit
                )
            {
                isIsQuote = false;
            }
            return isIsQuote;
        }
    }

    public enum SqlTypeE
    {
        @int,
        @numeric,
        @money,
        @bit,
        @image,
        @datetime,
        @nvarchar,
        @varchar,
        @char,
        @text
    }

    public class SearchParamInfo
    {
        private string _tempTableStr;

        private string _sqlStr;

        private string _whereStr = string.Empty;

        private string _orderStr = string.Empty;

        private int _pageIndex { get; set; }

        private int _pageSize { get; set; }

        public SearchParamInfo(string tempTableStr, string sqlStr, string whereStr, string orderStr,
            int pageIndex, int pageSize)
        {
            this._tempTableStr = tempTableStr;
            this._sqlStr = sqlStr;
            this._whereStr = whereStr;
            this._orderStr = orderStr;
            this._pageIndex = pageIndex;
            this._pageSize = pageSize;
        }

        public SqlParameter[] GetPars()
        {
            List<SqlParameter> parList = new List<SqlParameter>();
            parList.Add(DBBase.GetPar("tempTableStr", _tempTableStr));
            parList.Add(DBBase.GetPar("sqlStr", _sqlStr));
            //如果 TempTableStr 不空，那么条件是加到 TempTableStr 的
            parList.Add(DBBase.GetPar("whereStr", _whereStr));
            parList.Add(DBBase.GetPar("orderStr", _orderStr));
            parList.Add(DBBase.GetPar("pageIndex", _pageIndex, SqlDbType.Int));
            parList.Add(DBBase.GetPar("pageSize", _pageSize, SqlDbType.Int));

            int PageCount = 0, recordsCount = 0;
            parList.Add(DBBase.GetPar("pageCount", PageCount, dbType: SqlDbType.Int, dir: ParameterDirection.Output));
            parList.Add(DBBase.GetPar("recordsCount", recordsCount, dbType: SqlDbType.Int, dir: ParameterDirection.Output));

            return parList.ToArray();
        }
    }
}