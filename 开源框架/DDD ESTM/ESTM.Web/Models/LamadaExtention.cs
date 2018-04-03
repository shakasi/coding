using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace ESTM.Web
{
    public class LamadaExtention<Dto> where Dto : new()
    {
        private List<Expression> m_lstExpression = null;
        private ParameterExpression m_Parameter = null;

        public LamadaExtention()
        {
            m_lstExpression = new List<Expression>();
            m_Parameter = Expression.Parameter(typeof(Dto), "x");
        }

        public void GetExpression(string strPropertyName, object strValue, ExpressionType expressType)
        {
            Expression expRes = null;
            MemberExpression member = Expression.PropertyOrField(m_Parameter, strPropertyName);
            if (expressType == ExpressionType.Contains)
            {
                expRes = Expression.Call(member, typeof(string).GetMethod("Contains"), Expression.Constant(strValue));
            }
            else if (expressType == ExpressionType.Equal)
            {
                expRes = Expression.Equal(member, Expression.Constant(strValue, member.Type));
            }
            else if (expressType == ExpressionType.LessThan)
            {
                expRes = Expression.LessThan(member, Expression.Constant(strValue, member.Type));
            }
            else if (expressType == ExpressionType.LessThanOrEqual)
            {
                expRes = Expression.LessThanOrEqual(member, Expression.Constant(strValue, member.Type));
            }
            else if (expressType == ExpressionType.GreaterThan)
            {
                expRes = Expression.GreaterThan(member, Expression.Constant(strValue, member.Type));
            }
            else if (expressType == ExpressionType.GreaterThanOrEqual)
            {
                expRes = Expression.GreaterThanOrEqual(member, Expression.Constant(strValue, member.Type));
            }
            //return expRes;
            m_lstExpression.Add(expRes);
        }

        //针对Or条件的表达式
        public void GetExpression(string strPropertyName, List<object> lstValue)
        {
            Expression expRes = null;
            MemberExpression member = Expression.PropertyOrField(m_Parameter, strPropertyName);
            foreach (var oValue in lstValue)
            {
                if (expRes == null)
                {
                    expRes = Expression.Equal(member, Expression.Constant(oValue, member.Type));
                }
                else
                {
                    expRes = Expression.Or(expRes, Expression.Equal(member, Expression.Constant(oValue, member.Type)));
                }
            }


            m_lstExpression.Add(expRes);
        }

        public Expression<Func<Dto, bool>> GetLambda()
        {
            Expression whereExpr = null;
            foreach (var expr in this.m_lstExpression)
            {
                if (whereExpr == null) whereExpr = expr;
                else whereExpr = Expression.And(whereExpr, expr);
            }
            if (whereExpr == null)
                return null;
            return Expression.Lambda<Func<Dto, Boolean>>(whereExpr, m_Parameter);
        }
    }

    public enum ExpressionType
    {
        Contains,//like
        Equal,//等于
        LessThan,//小于
        LessThanOrEqual,//小于等于
        GreaterThan,//大于
        GreaterThanOrEqual//大于等于
    }
}