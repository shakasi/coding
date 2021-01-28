using System;
using VNext.Dependency;
using VNext.Systems;

namespace VNext.EventBuses
{
    /// <summary>
    /// <see cref="IEventHandler"/>事件处理器的可释放包装
    /// </summary>
    public class EventHandlerDisposeWrapper : Disposable
    {
        private readonly Action _disposeAction;

        /// <summary>
        /// 初始化一个<see cref="EventHandlerDisposeWrapper"/>类型的新实例
        /// </summary>
        public EventHandlerDisposeWrapper(IEventHandler eventHandler, Action disposeAction = null)
        {
            _disposeAction = disposeAction;
            EventHandler = eventHandler;
        }

        /// <summary>
        /// 获取或设置 事件处理器对象
        /// </summary>
        public IEventHandler EventHandler { get; set; }

        protected override void Dispose(bool disposing)
        {
            if (!Disposed)
            {
                _disposeAction?.Invoke();
            }
            base.Dispose(disposing);
        }
    }
}