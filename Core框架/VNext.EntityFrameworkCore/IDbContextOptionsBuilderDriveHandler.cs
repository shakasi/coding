﻿using System.Data.Common;

using Microsoft.EntityFrameworkCore;


namespace VNext.Entity
{
    /// <summary>
    /// 定义<see cref="DbContextOptionsBuilder"/> 数据库驱动差异处理器
    /// </summary>
    public interface IDbContextOptionsBuilderDriveHandler
    {
        /// <summary>
        /// 获取 数据库类型名称，如 SQLSERVER，MYSQL，SQLITE等
        /// </summary>
        DatabaseType Type { get; }

        /// <summary>
        /// 处理<see cref="DbContextOptionsBuilder"/>驱动差异
        /// </summary>
        /// <param name="builder">创建器</param>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="existingConnection">已存在的连接对象</param>
        /// <returns></returns>
        DbContextOptionsBuilder Handle(DbContextOptionsBuilder builder, string connectionString, DbConnection existingConnection);
    }
}