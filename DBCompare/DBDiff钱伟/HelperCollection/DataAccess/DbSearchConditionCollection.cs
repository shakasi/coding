using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace Utility.HelperCollection.DataAccess
{
    public class DbSearchConditionCollection<T>
    {
        internal List<DbSearchConditionInfo> Conditions { get; set; }
        public DbSearchConditionCollection()
        {
            Conditions = new List<DbSearchConditionInfo>();
        }
        public void Add(Expression<Func<T,object>> expr,DbSearchOperation op,object value)
        {
           string fieldName = expr.GetPropertyName<T>();
           Add(fieldName, op, value);
        }
        public void Add(string fieldName, DbSearchOperation op, object value)
        {
            if (!string.IsNullOrEmpty(fieldName))
            {
                DbSearchConditionInfo info = new DbSearchConditionInfo();
                info.Field = fieldName;
                info.FieldData = value;
                info.SearchOperation = op;
                Conditions.Add(info);
            }
        }
        public int Count()
        {
            return Conditions.Count;
        }
    }
}
