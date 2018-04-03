using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Xml;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace Cuscapi.Utils
{
    public abstract class DBBase
    {
        /// <summary>
        /// model和数据库表的关系
        /// </summary>
        private static ConcurrentDictionary<Type, List<ORMInfo>> _ormInfoDic = null;

        static DBBase()
        {
            _ormInfoDic = new ConcurrentDictionary<Type, List<ORMInfo>>();
        }

        protected List<ORMInfo> GetORM(Type t)
        {
            if (!_ormInfoDic.Keys.Contains(t))
            {
                if (!_ormInfoDic.TryAdd(t, GetORMInfo(t)))
                {
                    throw new Exception("加载Mode映射实体失败！");
                }
            }
            return _ormInfoDic[t];
        }

        private List<ORMInfo> GetORMInfo(Type t)
        {
            List<ORMInfo> ormInfoList = new List<ORMInfo>();
            XmlHelper xml = new XmlHelper(t.Name + ".orm.xml");
            try
            {
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
            }
            catch (Exception ex)
            {

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
        @smallint,
        @decimal,
        @numeric,
        @money,
        @bit,
        @image,
        @datetime,
        @nvarchar,
        @varchar,
        @char,
        @nchar,
        @text
    }
}
