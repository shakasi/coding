using System;
using System.Linq;
using VNext.Finders;
using VNext.Extensions;

namespace VNext.Dependency
{
    /// <summary>
    /// 依赖注入类型查找器
    /// </summary>
    public class DependencyTypeFinder : FinderBase<Type>, IDependencyTypeFinder
    {
        private readonly IAppAssemblyFinder _allAssemblyFinder;

        /// <summary>
        /// 初始化一个<see cref="DependencyTypeFinder"/>类型的新实例
        /// </summary>
        public DependencyTypeFinder(IAppAssemblyFinder allAssemblyFinder)
        {
            _allAssemblyFinder = allAssemblyFinder;
        }

        /// <summary>
        /// 重写以实现所有项的查找
        /// </summary>
        /// <returns></returns>
        protected override Type[] FindAllItems()
        {
            Type[] baseTypes = new[] { typeof(ISingletonDependency), typeof(IScopedDependency), typeof(ITransientDependency) };
            Type[] types = _allAssemblyFinder.FindAll(true).SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.IsClass && !type.IsAbstract && !type.IsInterface && !type.HasAttribute<IgnoreDependencyAttribute>()
                    && (baseTypes.Any(b => b.IsAssignableFrom(type)) || type.HasAttribute<DependencyAttribute>()))
                .ToArray();
            return types;
        }
    }
}