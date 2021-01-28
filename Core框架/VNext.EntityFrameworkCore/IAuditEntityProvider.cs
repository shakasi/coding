using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using VNext.Systems;

namespace VNext.Entity
{
    /// <summary>
    /// 定义数据审计信息提供者
    /// </summary>
    public interface IAuditEntityProvider
    {
        /// <summary>
        /// 从指定上下文中获取数据审计信息
        /// </summary>
        /// <param name="context">数据上下文</param>
        /// <returns>数据审计信息的集合</returns>
        IEnumerable<AuditEntityEntry> GetAuditEntities(DbContext context);
    }
}