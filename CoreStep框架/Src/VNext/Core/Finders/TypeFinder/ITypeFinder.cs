using System;
using VNext.Dependency;

namespace VNext.Finders
{
    /// <summary>
    /// 定义类型查找行为
    /// </summary>
    [IgnoreDependencyAttribute]
    public interface ITypeFinder : IFinder<Type>
    { }
}