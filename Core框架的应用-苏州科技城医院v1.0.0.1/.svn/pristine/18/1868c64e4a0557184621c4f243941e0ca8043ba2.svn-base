using DoCare.Hosting.Dtos;
using DoCare.Hosting.IContracts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VNext.Data;
using VNext.Dependency;

namespace DoCare.Hosting
{
    /// <summary>
    /// 发热门诊业务实现类
    /// </summary>
    [Dependency(ServiceLifetime.Scoped, AddSelf = true)]
    public class DoCareService : DoCareServiceBase
    {
        /// <summary>
        /// 初始化一个<see cref="DoCareService"/>类型的新实例
        /// </summary>
        public DoCareService(IServiceProvider provider)
            : base(provider)
        { }

        #region 患者相关

        /// <summary>
        /// 获取患者信息
        /// </summary>
        public async Task<PatientOutDto> GetPatient(PatientInDto dto)
        {
            Check.NotNullOrEmpty(dto?.CardNo, nameof(dto.CardNo));

            var impl = ServiceProvider.GetService<IPatient>();
            var result = await impl?.GetPatient(dto);
            return result;
        }

        /// <summary>
        /// 查询 患者登记信息
        /// </summary>
        public async Task<List<PatientRegistOutDto>> QueryRegist(PatientRegistInDto dto)
        {
            Check.NotNull(dto?.BeginTime, nameof(dto.BeginTime));
            Check.NotNull(dto?.EndTime, nameof(dto.EndTime));

            var impl = ServiceProvider.GetService<IPatient>();
            var result = await impl?.QueryRegist(dto);
            return result;
        }

        #endregion

        #region 检查信息

        /// <summary>
        /// 检查信息查询
        /// </summary>
        public async Task<List<ExamReportOutDto>> QueryPacs(CommonInDto dto)
        {
            Check.NotNull(dto?.PatientId, nameof(dto.PatientId));

            var impl = ServiceProvider.GetService<IExam>();
            var result = await impl?.QueryExam(dto);
            return result;
        }

        #endregion

        #region 检验信息

        /// <summary>
        /// 检查信息查询
        /// </summary>
        public async Task<List<LisReportOutDto>> QueryLab(CommonInDto dto)
        {
            Check.NotNull(dto?.PatientId, nameof(dto.PatientId));

            var impl = ServiceProvider.GetService<ILis>();

            var labReports = await impl?.QueryLisReport(dto);
            return labReports;
        }
        #endregion

        #region 平台消息处理
        public async Task<OperationResult> MessageProcess(SoapDto dto)
        {
            if (dto.Type == SoapType.PatientRegist)
            {

            }
            else if (dto.Type == SoapType.Pacs)
            {

            }
            else if (dto.Type == SoapType.Lab)
            {

            }
            return new OperationResult(OperationResultType.Success, "数据入库成功");
        }

        #endregion
    }
}