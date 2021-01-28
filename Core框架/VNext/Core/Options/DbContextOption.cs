﻿using System;
using VNext.Entity;

namespace VNext.Options
{
    /// <summary>
    /// 数据上下文配置节点
    /// </summary>
    public class DbContextOption
    {
        /// <summary>
        /// 初始化一个<see cref="DbContextOption"/>类型的新实例
        /// </summary>
        public DbContextOption()
        {
            LazyLoadingProxiesEnabled = false;
            AutoMigrationEnabled = false;
        }

        /// <summary>
        /// 获取 上下文类型
        /// </summary>
        public Type DbContextType => string.IsNullOrEmpty(DbContextTypeName) ? null : Type.GetType(DbContextTypeName);

        /// <summary>
        /// 获取或设置 上下文类型全名
        /// </summary>
        public string DbContextTypeName { get; set; }

        /// <summary>
        /// 获取或设置 连接字符串
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// 获取或设置 数据库类型
        /// </summary>
        public DatabaseType DatabaseType { get; set; }

        /// <summary>
        /// 获取或设置 是否启用延迟加载代理
        /// </summary>
        public bool LazyLoadingProxiesEnabled { get; set; }

        /// <summary>
        /// 获取或设置 是否自动迁移
        /// </summary>
        public bool AutoMigrationEnabled { get; set; }
    }
}