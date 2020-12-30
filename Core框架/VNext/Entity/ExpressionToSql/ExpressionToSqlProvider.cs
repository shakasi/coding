using System;
using System.Linq.Expressions;

namespace VNext.Entity
{
    /// <summary>
    /// 表达式释放
    /// </summary>
    public static class ExpressionToSqlProvider
    {
        /// <summary>
        /// IExpressionToSql
        /// </summary>
        /// <param name="expression">表达式树</param>
        /// <returns>IExpressionToSql</returns>
        private static IExpressionToSql ExpressionToSqlResolve(this Expression expression)
        {
            //null
            if (expression == null)
            {
                throw new ArgumentNullException("expression", "不能为null");
            }
            //表示具有常数值的表达式
            else if (expression is ConstantExpression)
            {
                return new ConstantExpressionProvider();
            }
            //表示具有二进制运算符的表达式
            else if (expression is BinaryExpression)
            {
                return new BinaryExpressionProvider();
            }
            //表示访问字段或属性
            else if (expression is MemberExpression)
            {
                return new MemberExpressionProvider();
            }
            //表示对静态方法或实例方法的调用
            else if (expression is MethodCallExpression)
            {
                return new MethodCallExpressionProvider();
            }
            //表示创建一个新数组，并可能初始化该新数组的元素
            else if (expression is NewArrayExpression)
            {
                return new NewArrayExpressionProvider();
            }
            //表示一个构造函数调用
            else if (expression is NewExpression)
            {
                return new NewExpressionProvider();
            }
            //表示具有一元运算符的表达式
            else if (expression is UnaryExpression)
            {
                return new UnaryExpressionProvider();
            }
            //表示包含集合初始值设定项的构造函数调用
            else if (expression is ListInitExpression)
            {
                return new ListInitExpressionProvider();
            }
            //表示将委托或lambda表达式应用于参数表达式列表的表达式
            else if (expression is InvocationExpression)
            {
                return new InvocationExpressionProvider();
            }
            //描述一个lambda表达式
            else if (expression is LambdaExpression)
            {
                return new LambdaExpressionProvider();
            }
            //表示命名参数表达式
            else if (expression is ParameterExpression)
            {
                return new ParameterExpressionProvider();
            }
            else
            {

                throw new NotImplementedException("未实现的IExpressionToSql");
            }
        }

        /// <summary>
        /// Select
        /// </summary>
        /// <param name="sqlCreator">sql打包对象</param>
        /// <param name="expression">表达式树</param>
        public static void Select(this SqlCreator sqlCreator, Expression expression)
            => expression.ExpressionToSqlResolve().Select(sqlCreator, expression);

        /// <summary>
        /// Join
        /// </summary>
        /// <param name="sqlCreator">sql打包对象</param>
        /// <param name="expression">表达式树</param>
        public static void Join(this SqlCreator sqlCreator, Expression expression)
            => expression.ExpressionToSqlResolve().Join(sqlCreator, expression);

        /// <summary>
        /// Where
        /// </summary>
        /// <param name="sqlCreator">sql打包对象</param>
        /// <param name="expression">表达式树</param>
        public static void Where(this SqlCreator sqlCreator, Expression expression)
            => expression.ExpressionToSqlResolve().Where(sqlCreator, expression);

        /// <summary>
        /// In
        /// </summary>
        /// <param name="sqlCreator">sql打包对象</param>
        /// <param name="expression">表达式树</param>
        public static void In(this SqlCreator sqlCreator, Expression expression)
            => expression.ExpressionToSqlResolve().In(sqlCreator, expression);

        /// <summary>
        /// GroupBy
        /// </summary>
        /// <param name="sqlCreator">sql打包对象</param>
        /// <param name="expression">表达式树</param>
        public static void GroupBy(this SqlCreator sqlCreator, Expression expression)
            => expression.ExpressionToSqlResolve().GroupBy(sqlCreator, expression);

        /// <summary>
        /// OrderBy
        /// </summary>
        /// <param name="sqlCreator">sql打包对象</param>
        /// <param name="expression">表达式树</param>
        /// <param name="orders">排序方式</param>
        public static void OrderBy(this SqlCreator sqlCreator, Expression expression, params OrderType[] orders)
            => expression.ExpressionToSqlResolve().OrderBy(sqlCreator, expression, orders);
    }
}
