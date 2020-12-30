using Microsoft.Extensions.DependencyInjection;
using System;
using VNext.Dependency;

namespace VNext.EventBuses
{
    /// <summary>
    /// 一个事件总线，当有消息被派发到消息总线时，消息总线将不做任何处理与路由，而是直接将消息推送到订阅方
    /// </summary>
    internal class EventBusPassThrough : EventBusBase
    {
        /// <summary>
        /// 初始化一个<see cref="EventBusPassThrough"/>类型的新实例
        /// </summary>
        public EventBusPassThrough(IServiceScopeFactory serviceScopeFactory, IServiceProvider serviceProvider)
            : base(serviceScopeFactory, serviceProvider)
        { }
    }
}