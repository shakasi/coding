using System;
using System.Linq.Expressions;
using VNext.Authorization;

namespace VNext.Data
{
    /// <summary>
    /// 定义过滤表达式功能
    /// </summary>
    public interface IFilterService
    {
        /// <summary>
        /// 获取指定查询条件组的查询表达式
        /// </summary>
        /// <typeparam name="T">表达式实体类型</typeparam>
        /// <param name="group">查询条件组，如果为null，则直接返回 true 表达式</param>
        /// <returns>查询表达式</returns>
        Expression<Func<T, bool>> GetExpression<T>(FilterGroup group);

        /// <summary>
        /// 获取指定查询条件的查询表达式
        /// </summary>
        /// <typeparam name="T">表达式实体类型</typeparam>
        /// <param name="rule">查询条件，如果为null，则直接返回 true 表达式</param>
        /// <returns>查询表达式</returns>
        Expression<Func<T, bool>> GetExpression<T>(FilterRule rule);

        /// <summary>
        /// 验证指定查询条件组是否能正常转换为表达式
        /// </summary>
        /// <param name="group">查询条件组</param>
        /// <param name="type">实体类型</param>
        /// <returns>验证操作结果</returns>
        OperationResult CheckFilterGroup(FilterGroup group, Type type);
    }
}