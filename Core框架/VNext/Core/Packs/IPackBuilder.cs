using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace VNext.Packs
{
    /// <summary>
    /// 定义VNext构建器
    /// </summary>
    public interface IPackBuilder
    {
        /// <summary>
        /// 获取 服务集合
        /// </summary>
        IServiceCollection Services { get; }

        /// <summary>
        /// 获取 加载的模块集合
        /// </summary>
        IEnumerable<Pack> LoadPacks { get; }

        /// <summary>
        /// 添加指定模块
        /// </summary>
        /// <typeparam name="TPack">要添加的模块类型</typeparam>
        IPackBuilder AddPack<TPack>() where TPack : Pack;

        /// <summary>
        /// 添加加载的所有Pack，并可排除指定的Pack类型
        /// </summary>
        /// <param name="exceptPackTypes">要排除的Pack类型</param>
        /// <returns></returns>
        IPackBuilder AddPacks(params Type[] exceptPackTypes);
    }
}