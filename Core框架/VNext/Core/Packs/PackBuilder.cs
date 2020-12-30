using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VNext.Data;
using VNext.Extensions;
using VNext.Finders;
using VNext.Options;

namespace VNext.Packs
{
    /// <summary>
    /// VNext构建器
    /// </summary>
    public class PackBuilder : IPackBuilder
    {
        private List<Pack> loadPacks;
        private readonly List<Pack> sourcePacks;

        /// <summary>
        /// 初始化一个<see cref="PackBuilder"/>类型的新实例
        /// </summary>
        public PackBuilder(IServiceCollection services)
        {
            Check.NotNull(services, nameof(services));
            Services = services;

            loadPacks = new List<Pack>();
            sourcePacks = GetAllPacks(services);
        }

        #region 接口属性

        /// <summary>
        /// 获取 服务集合
        /// </summary>
        public IServiceCollection Services { get; }

        /// <summary>
        /// 获取 加载的模块集合
        /// </summary>
        public IEnumerable<Pack> LoadPacks => loadPacks;

        #endregion

        #region 接口方法
        /// <summary>
        /// 添加指定模块
        /// </summary>
        /// <typeparam name="TPack">要添加的模块类型</typeparam>
        public IPackBuilder AddPack<TPack>() where TPack : Pack
        {
            Type type = typeof(TPack);
            return AddPack(type);
        }

        /// <summary>
        /// 添加加载的所有Pack
        /// </summary>
        /// <param name="exceptPackTypes">要排除的Pack类型</param>
        /// <returns></returns>
        public IPackBuilder AddPacks(params Type[] exceptPackTypes)
        {
            Pack[] source = sourcePacks.ToArray();
            Pack[] exceptPacks = source.Where(m => exceptPackTypes.Contains(m.GetType())).ToArray();
            source = source.Except(exceptPacks).ToArray();

            foreach (Pack pack in source)
            {
                AddPack(pack.GetType());
            }

            return this;
        }
        #endregion

        /// <summary>
        /// 获取相关程序集中所有Pack
        /// </summary>
        private static List<Pack> GetAllPacks(IServiceCollection services)
        {
            IPackTypeFinder packTypeFinder = services.GetOrAddTypeFinder<IPackTypeFinder>(assemblyFinder =>
                            new PackTypeFinder(assemblyFinder));

            Type[] packTypes = packTypeFinder.FindAll();
            return packTypes.Select(m => (Pack)Activator.CreateInstance(m))
               .OrderBy(m => m.Level)
               .ThenBy(m => m.Order)
               .ThenBy(m => m.GetType().FullName)
               .ToList();
        }

        private IPackBuilder AddPack(Type type)
        {
            if (!type.IsBaseOn(typeof(Pack)))
            {
                throw new Exception($"要加载的Pack类型“{type}”不派生于基类 Pack");
            }

            //如果已存在，则过滤直接返回
            if (loadPacks.Any(m => m.GetType() == type))
            {
                return this;
            }

            Pack[] tmpPacks = new Pack[loadPacks.Count];
            loadPacks.CopyTo(tmpPacks);

            Pack pack = sourcePacks.FirstOrDefault(m => m.GetType() == type);
            if (pack == null)
            {
                throw new Exception($"类型为“{type.FullName}”的模块实例无法找到");
            }
            loadPacks.AddIfNotExist(pack);

            // 添加依赖模块
            Type[] dependTypes = pack.GetDependPackTypes();
            foreach (Type dependType in dependTypes)
            {
                Pack dependPack = sourcePacks.Find(m => m.GetType() == dependType);
                if (dependPack == null)
                {
                    throw new Exception($"加载模块{pack.GetType().FullName}时无法找到依赖模块{dependType.FullName}");
                }
                loadPacks.AddIfNotExist(dependPack);
            }

            // 按先层级后顺序的规则进行排序
            loadPacks = loadPacks.OrderBy(m => m.Level).ThenBy(m => m.Order).ToList();

            string logName = typeof(PackBuilder).FullName;
            tmpPacks = loadPacks.Except(tmpPacks).ToArray();
            foreach (Pack tmpPack in tmpPacks)
            {
                Type packType = tmpPack.GetType();
                string packName = packType.GetDescription();
                Services.LogInfo(logName, $"添加模块 “{packName} ({packType.Name})” 的服务");

                ServiceDescriptor[] tmp = Services.ToArray();
                AddPack(Services, tmpPack);

                Services.LogServices(packType.FullName, tmp);
                Services.LogInfo(logName, $"模块 “{packName} ({packType.Name})” 的服务添加完毕，添加了 {Services.Count - tmp.Length} 个服务\n");
            }

            return this;
        }

        private static IServiceCollection AddPack(IServiceCollection services, Pack pack)
        {
            Type type = pack.GetType();
            Type serviceType = typeof(Pack);

            if (type.BaseType?.IsAbstract == false)
            {
                //移除多重继承的模块
                ServiceDescriptor[] descriptors = services.Where(m =>
                    m.Lifetime == ServiceLifetime.Singleton && m.ServiceType == serviceType
                    && m.ImplementationInstance?.GetType() == type.BaseType).ToArray();
                foreach (var descriptor in descriptors)
                {
                    services.Remove(descriptor);
                }
            }

            if (!services.Any(m => m.Lifetime == ServiceLifetime.Singleton && m.ServiceType == serviceType && m.ImplementationInstance?.GetType() == type))
            {
                services.AddSingleton(typeof(Pack), pack);
                pack.AddServices(services);
            }

            return services;
        }
    }
}