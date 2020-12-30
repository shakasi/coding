using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using VNext.Dependency;
using VNext.Extensions;

namespace VNext.Entity
{
    /// <summary>
    /// 上下文数据模型缓存
    /// </summary>
    public class DbContextModelCache
    {
        private readonly ConcurrentDictionary<Type, IModel> _dict = new ConcurrentDictionary<Type, IModel>();
        private readonly ILogger _logger;

        /// <summary>
        /// 初始化一个<see cref="DbContextModelCache"/>类型的新实例
        /// </summary>
        public DbContextModelCache(IServiceProvider provider)
        {
            _logger = provider.GetLogger(GetType());
        }

        /// <summary>
        /// 获取指定上下文类型的模型
        /// </summary>
        /// <param name="dbContextType">上下文类型</param>
        /// <returns>数据模型</returns>
        public IModel Get(Type dbContextType)
        {
            IModel model = _dict.GetOrDefault(dbContextType);
            _logger.LogDebug($"从 DbContextModelCache 中获取数据上下文 {dbContextType} 的Model缓存，结果：{model != null}");
            return model;
        }

        /// <summary>
        /// 设置指定上下文类型的模型
        /// </summary>
        /// <param name="dbContextType">上下文类型</param>
        /// <param name="model">模型</param>
        public void Set(Type dbContextType, IModel model)
        {
            _logger.LogDebug($"在 DbContextModelCache 中存入数据上下文 {dbContextType} 的Model缓存");
            _dict[dbContextType] = model;
        }

        /// <summary>
        /// 移除指定上下文类型的模型
        /// </summary>
        public void Remove(Type dbContextType)
        {
            _logger.LogDebug($"从 DbContextModelCache 中移除数据上下文 {dbContextType} 的Model缓存");
            _dict.TryRemove(dbContextType, out IModel model);
        }
    }
}