using System;
using System.ComponentModel;

namespace VNext.Entity
{
    /// <summary>
    /// 定义创建时间
    /// </summary>
    public interface ICreatedTime
    {
        /// <summary>
        /// 获取或设置 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        DateTime CreatedTime { get; set; }
    }
}