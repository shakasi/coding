using System;
using System.Linq;
using VNext.Finders;

namespace VNext.Packs
{
    /// <summary>
    /// VNext模块类型查找器
    /// </summary>
    public class PackTypeFinder : BaseTypeFinderBase<Pack>, IPackTypeFinder
    {
        /// <summary>
        /// 初始化一个<see cref="PackTypeFinder"/>类型的新实例
        /// </summary>
        public PackTypeFinder(IAppAssemblyFinder allAssemblyFinder)
            : base(allAssemblyFinder)
        { }

        /// <summary>
        /// 重写以实现所有项的查找
        /// </summary>
        /// <returns></returns>
        protected override Type[] FindAllItems()
        {
            Type[] types = base.FindAllItems();

            //排除被继承的Pack实类【基类是类,不是抽象类】
            Type[] basePackTypes = types.Select(m => m.BaseType).Where(m => m != null && m.IsClass && !m.IsAbstract).ToArray();
            return types.Except(basePackTypes).ToArray();
        }
    }
}