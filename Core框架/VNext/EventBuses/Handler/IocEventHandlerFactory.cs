using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using VNext.Dependency;
using VNext.Entity;

namespace VNext.EventBuses
{
    /// <summary>
    /// 依赖注入事件处理器实例获取方式
    /// </summary>
    internal class IocEventHandlerFactory : IEventHandlerFactory
    {
        private readonly ILogger _logger;
        private readonly IHybridServiceScopeFactory _serviceScopeFactory;
        private readonly Type _handlerType;

        /// <summary>
        /// 初始化一个<see cref="IocEventHandlerFactory"/>类型的新实例
        /// </summary>
        /// <param name="provider">服务提供者</param>
        /// <param name="handlerType">事件处理器类型</param>
        public IocEventHandlerFactory(IServiceProvider provider, Type handlerType)
        {
            _logger = provider.GetLogger<IocEventHandlerFactory>();
            _serviceScopeFactory = provider.GetService<IHybridServiceScopeFactory>();
            _handlerType = handlerType;
        }

        /// <summary>
        /// 获取事件处理器实例
        /// </summary>
        /// <returns></returns>
        public EventHandlerDisposeWrapper GetHandler()
        {
            string token = Guid.NewGuid().ToString();
            IServiceScope scope = _serviceScopeFactory.CreateScope();
            _logger.LogDebug($"创建处理器“{_handlerType}”的执行作用域，标识：{token}");
            IServiceProvider scopeProvider = scope.ServiceProvider;
            IEventHandler eventHandler = (IEventHandler)scopeProvider.GetService(_handlerType);
            _logger.LogDebug($"创建处理器“{_handlerType}”的实例，标识：{token}");
            return new EventHandlerDisposeWrapper(eventHandler,
                () =>
                {
                    IUnitOfWorkManager unitOfWorkManager = scopeProvider.GetService<IUnitOfWorkManager>();
                    unitOfWorkManager.Commit();
                    scope.Dispose();
                    _logger.LogDebug($"释放处理器“{_handlerType}”的执行作用域，标识： {token}");
                });
        }
    }
}