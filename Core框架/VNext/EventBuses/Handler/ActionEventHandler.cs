using System;
using System.Threading;
using System.Threading.Tasks;
using VNext.Data;
using VNext.Extensions;

namespace VNext.EventBuses
{
    /// <summary>
    /// 支持<see cref="Action"/>的事件处理器
    /// </summary>
    internal class ActionEventHandler<TEventData> : EventHandlerBase<TEventData> where TEventData : IEventData
    {
        /// <summary>
        /// 初始化一个<see cref="ActionEventHandler{TEventData}"/>类型的新实例
        /// </summary>
        public ActionEventHandler(Action<TEventData> action)
        {
            Action = action;
        }

        /// <summary>
        /// 获取 事件执行的委托
        /// </summary>
        public Action<TEventData> Action { get; }
        
        /// <summary>
        /// 事件处理
        /// </summary>
        /// <param name="eventData">事件源数据</param>
        public override void Handle(TEventData eventData)
        {
            Check.NotNull(eventData, nameof(eventData));
            Action(eventData);
        }

    }
}