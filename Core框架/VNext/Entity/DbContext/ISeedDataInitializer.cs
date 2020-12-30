using System;
using VNext.Dependency;

namespace VNext.Entity
{
    /// <summary>
    /// 定义种子数据初始化
    /// </summary>
    [MultipleDependencyAttribute]
    public interface ISeedDataInitializer
    {
        /// <summary>
        /// 获取 种子数据初始化的顺序
        /// </summary>
        int Order { get; }

        /// <summary>
        /// 获取 所属实体类型
        /// </summary>
        Type EntityType { get; }

        /// <summary>
        /// 初始化种子数据
        /// </summary>
        void Initialize();
    }
}
