using Microsoft.EntityFrameworkCore;
using System;

namespace VNext.Entity
{
    /// <summary>
    /// 默认EntityFramework数据上下文
    /// </summary>
    public class DefaultDbContext : DbContextBase
    {
        /// <summary>
        /// 初始化一个<see cref="DefaultDbContext"/>类型的新实例
        /// </summary>
        public DefaultDbContext(DbContextOptions<DefaultDbContext> options, IServiceProvider serviceProvider)
            : base(options, serviceProvider)
        { }
    }
}