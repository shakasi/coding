using Microsoft.Extensions.DependencyInjection;

namespace VNext.Dependency
{
    /// <summary>
    /// <see cref="IServiceScope"/>工厂包装一下
    /// </summary>
    public interface IHybridServiceScopeFactory
    {
        /// <summary>
        /// 创建依赖注入服务的作用域，如果当前操作处于HttpRequest作用域中，直接使用HttpRequest的作用域，否则创建新的作用域
        /// </summary>
        /// <returns></returns>
        IServiceScope CreateScope();
    }
}