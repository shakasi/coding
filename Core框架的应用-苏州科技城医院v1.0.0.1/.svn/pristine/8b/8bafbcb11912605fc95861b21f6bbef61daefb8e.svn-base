using DoCare.Hosting.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using VNext.Entity;
using VNext.EventBuses;

namespace DoCare.Hosting
{
    /// <summary>
    /// 发热门诊：业务实现基类
    /// </summary>
    public abstract partial class DoCareServiceBase
    {
        /// <summary>
        /// 初始化一个<see cref="DoCareServiceBase"/>类型的新实例
        /// </summary>
        protected DoCareServiceBase(IServiceProvider provider)
        {
            ServiceProvider = provider;
            Logger = provider.GetLogger(GetType());
        }

        #region 属性

        /// <summary>
        /// 获取或设置 服务提供者对象
        /// </summary>
        protected IServiceProvider ServiceProvider { get; }

        /// <summary>
        /// 获取或设置 日志对象
        /// </summary>
        protected ILogger Logger { get; }

        /// <summary>
        /// 获取 事件总线
        /// </summary>
        protected IEventBus EventBus => ServiceProvider.GetService<IEventBus>();

        /// <summary>
        /// 获取或设置 患者挂号信息仓储对象
        /// </summary>
        protected IRepository<PatientRegist, Guid> PatientRegistRepository => ServiceProvider.GetService<IRepository<PatientRegist, Guid>>();

        /// <summary>
        /// 获取或设置 检查报告仓储对象
        /// </summary>
        protected IRepository<ExamReport, Guid> ExamReportRepository => ServiceProvider.GetService<IRepository<ExamReport, Guid>>();

        /// <summary>
        /// 获取或设置 检验报告单仓储对象
        /// </summary>
        protected IRepository<LisReport, Guid> LabReportRepository => ServiceProvider.GetService<IRepository<LisReport, Guid>>();

        #endregion
    }
}
