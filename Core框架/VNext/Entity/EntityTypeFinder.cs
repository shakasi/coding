using System;
using System.Linq;
using System.Reflection;
using VNext.Finders;
using VNext.Extensions;

namespace VNext.Entity
{
    /// <summary>
    /// 实体类型查找器
    /// </summary>
    public class EntityTypeFinder : FinderBase<Type>, IEntityTypeFinder
    {
        private readonly IAppAssemblyFinder _allAssemblyFinder;

        /// <summary>
        /// 初始化一个<see cref="VNext.Entity.EntityTypeFinder"/>类型的新实例
        /// </summary>
        public EntityTypeFinder(IAppAssemblyFinder allAssemblyFinder)
        {
            _allAssemblyFinder = allAssemblyFinder;
        }

        /// <summary>
        /// 重写以实现所有项的查找
        /// </summary>
        /// <returns></returns>
        protected override Type[] FindAllItems()
        {
            Type baseType = typeof(IEntity<>);
            Assembly[] assemblies = _allAssemblyFinder.FindAll(true);
            return assemblies.SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.IsDeriveClassFrom(baseType)).Distinct().ToArray();
        }
    }

    /// <summary>
    /// 实体类型查找器
    /// </summary>
    public class OutEntityTypeFinder : FinderBase<Type>, IOutEntityTypeFinder
    {
        private readonly IAppAssemblyFinder _allAssemblyFinder;

        /// <summary>
        /// 初始化一个<see cref="VNext.Entity.EntityTypeFinder"/>类型的新实例
        /// </summary>
        public OutEntityTypeFinder(IAppAssemblyFinder allAssemblyFinder)
        {
            _allAssemblyFinder = allAssemblyFinder;
        }

        /// <summary>
        /// 重写以实现所有项的查找
        /// </summary>
        /// <returns></returns>
        protected override Type[] FindAllItems()
        {
            Type baseType = typeof(IEntityDapper);
            Assembly[] assemblies = _allAssemblyFinder.FindAll(true);
            return assemblies.SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.IsDeriveClassFrom(baseType)).Distinct().ToArray();
        }
    }
}