using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq.Expressions;
using System.Security.Claims;
using VNext.Authorization;
using VNext.Dependency;
using VNext.Extensions;
using VNext.Identity;

namespace VNext.Data
{
    /// <summary>
    /// 查询表达式服务
    /// </summary>
    public class FilterService : IFilterService
    {
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// 初始化一个<see cref="FilterService"/>类型的新实例
        /// </summary>
        public FilterService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        #region Implementation of IFilterService

        /// <summary>
        /// 获取指定查询条件组的查询表达式
        /// </summary>
        /// <typeparam name="T">表达式实体类型</typeparam>
        /// <param name="group">查询条件组，如果为null，则直接返回 true 表达式</param>
        /// <returns>查询表达式</returns>
        public virtual Expression<Func<T, bool>> GetExpression<T>(FilterGroup group)
        {
            return FilterHelper.GetExpression<T>(group);

        }

        /// <summary>
        /// 获取指定查询条件的查询表达式
        /// </summary>
        /// <typeparam name="T">表达式实体类型</typeparam>
        /// <param name="rule">查询条件，如果为null，则直接返回 true 表达式</param>
        /// <returns>查询表达式</returns>
        public virtual Expression<Func<T, bool>> GetExpression<T>(FilterRule rule)
        {
            return FilterHelper.GetExpression<T>(rule);
        }

        /// <summary>
        /// 验证指定查询条件组是否能正常转换为表达式
        /// </summary>
        /// <param name="group">查询条件组</param>
        /// <param name="type">实体类型</param>
        /// <returns>验证操作结果</returns>
        public virtual OperationResult CheckFilterGroup(FilterGroup group, Type type)
        {
            return FilterHelper.CheckFilterGroup(group, type);
        }

        #endregion
    }
}