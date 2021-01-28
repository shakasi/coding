using System;
using System.Linq.Expressions;

namespace VNext.Entity
{
    /// <summary>
    /// 表示具有二进制运算符的表达式
    /// </summary>
	public class BinaryExpressionProvider : ExpressionToSqlBase<BinaryExpression>
    {
        /// <summary>
        /// OperatorParser
        /// </summary>
        /// <param name="expressionNodeType">表达式树节点类型</param>
        /// <param name="operatorIndex">操作符索引</param>
        /// <param name="sqlCreator">sql打包对象</param>
        /// <param name="useIs">是否使用is</param>
        private void OperatorParser(ExpressionType expressionNodeType, int operatorIndex, SqlCreator sqlCreator, bool useIs = false)
        {
            switch (expressionNodeType)
            {
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                    sqlCreator.SqlString.Insert(operatorIndex, " AND ");
                    break;
                case ExpressionType.Equal:
                    if (useIs)
                    {
                        sqlCreator.SqlString.Insert(operatorIndex, " IS ");
                    }
                    else
                    {
                        sqlCreator.SqlString.Insert(operatorIndex, " = ");
                    }
                    break;
                case ExpressionType.GreaterThan:
                    sqlCreator.SqlString.Insert(operatorIndex, " > ");
                    break;
                case ExpressionType.GreaterThanOrEqual:
                    sqlCreator.SqlString.Insert(operatorIndex, " >= ");
                    break;
                case ExpressionType.NotEqual:
                    if (useIs)
                    {
                        sqlCreator.SqlString.Insert(operatorIndex, " IS NOT ");
                    }
                    else
                    {
                        sqlCreator.SqlString.Insert(operatorIndex, " <> ");
                    }
                    break;
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                    sqlCreator.SqlString.Insert(operatorIndex, " OR ");
                    break;
                case ExpressionType.LessThan:
                    sqlCreator.SqlString.Insert(operatorIndex, " < ");
                    break;
                case ExpressionType.LessThanOrEqual:
                    sqlCreator.SqlString.Insert(operatorIndex, " <= ");
                    break;
                case ExpressionType.Add:
                    sqlCreator.SqlString.Insert(operatorIndex, " + ");
                    break;
                case ExpressionType.Subtract:
                    sqlCreator.SqlString.Insert(operatorIndex, " - ");
                    break;
                case ExpressionType.Multiply:
                    sqlCreator.SqlString.Insert(operatorIndex, " * ");
                    break;
                case ExpressionType.Divide:
                    sqlCreator.SqlString.Insert(operatorIndex, " / ");
                    break;
                case ExpressionType.Modulo:
                    sqlCreator.SqlString.Insert(operatorIndex, " % ");
                    break;
                default:
                    throw new NotImplementedException("未实现的节点类型" + expressionNodeType);
            }
        }

        #region Override Base Class Methods
        /// <summary>
        /// Join
        /// </summary>
        /// <param name="sqlCreator">sql打包对象</param>
        /// <param name="expression">表达式树</param>
        /// <returns>sqlCreator</returns>
        public override SqlCreator Join(SqlCreator sqlCreator, BinaryExpression expression)
        {
            //左侧嵌套
            var leftBinary = expression.Left as BinaryExpression;
            var isBinaryLeft = leftBinary?.Left is BinaryExpression;
            var isBoolMethodCallLeft = (leftBinary?.Left as MethodCallExpression)?.Method.ReturnType == typeof(bool);
            var isBinaryRight = leftBinary?.Right is BinaryExpression;
            var isBoolMethodCallRight = (leftBinary?.Right as MethodCallExpression)?.Method.ReturnType == typeof(bool);
            var leftNested = (isBinaryLeft || isBoolMethodCallLeft) && (isBinaryRight || isBoolMethodCallRight);
            if (leftNested)
            {
                sqlCreator += "(";
            }
            sqlCreator.Join(expression.Left);
            if (leftNested)
            {
                sqlCreator += ")";
            }

            var operatorIndex = sqlCreator.SqlString.Length;

            //右侧嵌套
            var rightBinary = expression.Right as BinaryExpression;
            isBinaryLeft = rightBinary?.Left is BinaryExpression;
            isBoolMethodCallLeft = (rightBinary?.Left as MethodCallExpression)?.Method.ReturnType == typeof(bool);
            isBinaryRight = rightBinary?.Right is BinaryExpression;
            isBoolMethodCallRight = (rightBinary?.Right as MethodCallExpression)?.Method.ReturnType == typeof(bool);
            var rightNested = (isBinaryLeft || isBoolMethodCallLeft) && (isBinaryRight || isBoolMethodCallRight);
            if (rightNested)
            {
                sqlCreator += "(";
            }
            sqlCreator.Where(expression.Right);
            if (rightNested)
            {
                sqlCreator += ")";
            }

            var sqlLength = sqlCreator.SqlString.Length;
            if (sqlLength - operatorIndex == 5 && sqlCreator.ToString().ToUpper().EndsWith("NULL"))
            {
                OperatorParser(expression.NodeType, operatorIndex, sqlCreator, true);
            }
            else
            {
                OperatorParser(expression.NodeType, operatorIndex, sqlCreator);
            }
            return sqlCreator;
        }

        /// <summary>
        /// Where
        /// </summary>
        /// <param name="sqlCreator">sql打包对象</param>
        /// <param name="expression">表达式树</param>
        /// <returns>sqlCreator</returns>
        public override SqlCreator Where(SqlCreator sqlCreator, BinaryExpression expression)
        {
            var startIndex = sqlCreator.Length;

            //左侧嵌套
            var leftBinary = expression.Left as BinaryExpression;
            var isBinaryLeft = leftBinary?.Left is BinaryExpression;
            var isBoolMethodCallLeft = (leftBinary?.Left as MethodCallExpression)?.Method.ReturnType == typeof(bool);
            var isBinaryRight = leftBinary?.Right is BinaryExpression;
            var isBoolMethodCallRight = (leftBinary?.Right as MethodCallExpression)?.Method.ReturnType == typeof(bool);
            var leftNested = (isBinaryLeft || isBoolMethodCallLeft) && (isBinaryRight || isBoolMethodCallRight);
            if (leftNested)
            {
                sqlCreator += "(";
            }
            sqlCreator.Where(expression.Left);
            if (leftNested)
            {
                sqlCreator += ")";
            }

            var signIndex = sqlCreator.Length;

            //右侧嵌套
            var rightBinary = expression.Right as BinaryExpression;
            isBinaryLeft = rightBinary?.Left is BinaryExpression;
            isBoolMethodCallLeft = (rightBinary?.Left as MethodCallExpression)?.Method.ReturnType == typeof(bool);
            isBinaryRight = rightBinary?.Right is BinaryExpression;
            isBoolMethodCallRight = (rightBinary?.Right as MethodCallExpression)?.Method.ReturnType == typeof(bool);
            var rightNested = (isBinaryLeft || isBoolMethodCallLeft) && (isBinaryRight || isBoolMethodCallRight);
            if (rightNested)
            {
                sqlCreator += "(";
            }
            sqlCreator.Where(expression.Right);
            if (rightNested)
            {
                sqlCreator += ")";
            }

            //表达式左侧为bool类型常量且为true时，不进行Sql拼接
            if (!(expression.Left.NodeType == ExpressionType.Constant && expression.Left.ToObject() is bool b && b))
            {
                //若表达式右侧为bool类型，且为false时，条件取非
                if ((expression.Right.NodeType == ExpressionType.Constant
                    || (expression.Right.NodeType == ExpressionType.Convert
                    && expression.Right is UnaryExpression unary
                    && unary.Operand.NodeType == ExpressionType.Constant))
                    && expression.Right.ToObject() is bool r)
                {
                    if (!r)
                    {
                        var subString = sqlCreator.ToString().Substring(startIndex, sqlCreator.ToString().Length - startIndex).ToUpper();

                        //IS NOT、IS                      
                        if (subString.Contains("IS NOT"))
                        {
                            var index = sqlCreator.ToString().LastIndexOf("IS NOT");
                            if (index != -1) sqlCreator.SqlString.Replace("IS NOT", "IS", index, 6);
                        }
                        if (subString.Contains("IS") && subString.LastIndexOf("IS") != subString.LastIndexOf("IS NOT"))
                        {
                            var index = sqlCreator.ToString().LastIndexOf("IS");
                            if (index != -1) sqlCreator.SqlString.Replace("IS", "IS NOT", index, 2);
                        }

                        //NOT LIKE、LIKE
                        if (subString.Contains("NOT LIKE"))
                        {
                            var index = sqlCreator.ToString().LastIndexOf("NOT LIKE");
                            if (index != -1) sqlCreator.SqlString.Replace("NOT LIKE", "LIKE", index, 8);
                        }
                        if (subString.Contains("LIKE") && subString.LastIndexOf("LIKE") != (subString.LastIndexOf("NOT LIKE") + 4))
                        {
                            var index = sqlCreator.ToString().LastIndexOf("LIKE");
                            if (index != -1) sqlCreator.SqlString.Replace("LIKE", "NOT LIKE", index, 4);
                        }

                        //NOT IN、IN
                        if (subString.Contains("NOT IN"))
                        {
                            var index = sqlCreator.ToString().LastIndexOf("NOT IN");
                            if (index != -1) sqlCreator.SqlString.Replace("NOT IN", "IN", index, 6);
                        }
                        if (subString.Contains("IN") && subString.LastIndexOf("IN") != (subString.LastIndexOf("NOT IN") + 4))
                        {
                            var index = sqlCreator.ToString().LastIndexOf("IN");
                            if (index != -1) sqlCreator.SqlString.Replace("IN", "NOT IN", index, 2);
                        }

                        //AND、OR
                        if (subString.Contains("AND"))
                        {
                            var index = sqlCreator.ToString().LastIndexOf("AND");
                            if (index != -1) sqlCreator.SqlString.Replace("AND", "OR", index, 3);
                        }
                        if (subString.Contains("OR"))
                        {
                            var index = sqlCreator.ToString().LastIndexOf("OR");
                            if (index != -1) sqlCreator.SqlString.Replace("OR", "AND", index, 2);
                        }

                        //=、<>
                        if (subString.Contains(" = "))
                        {
                            var index = sqlCreator.ToString().LastIndexOf(" = ");
                            if (index != -1) sqlCreator.SqlString.Replace(" = ", " <> ", index, 3);
                        }
                        if (subString.Contains("<>"))
                        {
                            var index = sqlCreator.ToString().LastIndexOf("<>");
                            if (index != -1) sqlCreator.SqlString.Replace("<>", "=", index, 2);
                        }

                        //>、<
                        if (subString.Contains(" > "))
                        {
                            var index = sqlCreator.ToString().LastIndexOf(" > ");
                            if (index != -1) sqlCreator.SqlString.Replace(" > ", " <= ", index, 3);
                        }
                        if (subString.Contains(" < "))
                        {
                            var index = sqlCreator.ToString().LastIndexOf(" < ");
                            if (index != -1) sqlCreator.SqlString.Replace(" < ", " >= ", index, 3);
                        }

                        //>=、<=
                        if (subString.Contains(" >= "))
                        {
                            var index = sqlCreator.ToString().LastIndexOf(" >= ");
                            if (index != -1) sqlCreator.SqlString.Replace(" >= ", " < ", index, 4);
                        }
                        if (subString.Contains(" <= "))
                        {
                            var index = sqlCreator.ToString().LastIndexOf(" <= ");
                            if (index != -1) sqlCreator.SqlString.Replace(" <= ", " > ", index, 4);
                        }
                    }
                }
                else
                {
                    if (sqlCreator.ToString().ToUpper().EndsWith("NULL"))
                        OperatorParser(expression.NodeType, signIndex, sqlCreator, true);
                    else
                        OperatorParser(expression.NodeType, signIndex, sqlCreator);
                }
            }
            return sqlCreator;
        }
        #endregion
    }
}