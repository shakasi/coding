using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Utility.HelperCollection.DataAccess
{
    public static class Extend
    {
        public static void TryOpen(this DbConnection conn)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
        }

        public static void ExcuteInOrder(this DbConnection conn, DbCommand cmd)
        {
            lock (conn)
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                cmd.ExecuteNonQuery();
            }
        }

        public static string GetPropertyName<T>(this Expression<Func<T,object>> expr)
        {
            var rtn = "";
            //if (expr.Body is UnaryExpression)
            //{
            //    rtn = ((MemberExpression)((UnaryExpression)expr.Body).Operand).Member.Name;
            //}
            if (expr.Body is MemberExpression)
            {
                rtn = ((MemberExpression)expr.Body).Member.Name;
            }
            //else if (expr.Body is ParameterExpression)
            //{
            //    rtn = ((ParameterExpression)expr.Body).Type.Name;
            //}
            return rtn;
      }

    }
}
