using System.Reflection;
using VNext.Dependency;

namespace VNext.Finders
{
    /// <summary>
    /// 定义程序集查找器
    /// </summary>
    [IgnoreDependencyAttribute]
    public interface IAssemblyFinder : IFinder<Assembly>
    { }
}