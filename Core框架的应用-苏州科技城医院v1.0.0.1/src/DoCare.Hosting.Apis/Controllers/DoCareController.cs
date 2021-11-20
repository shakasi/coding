using DoCare.Hosting.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VNext.Data;

namespace DoCare.Hosting.Apis.Controllers
{
    /// <summary>
    /// 发热门诊接口定义
    /// </summary>
    public class DoCareController : SiteApiControllerBase
    {
        private readonly DoCareService doCareService;
        public DoCareController(DoCareService doCareService)
        {
            this.doCareService = doCareService;
        }

        /// <summary>
        /// 获取患者信息
        /// </summary>
        /// <param name="dto">患者查询入参信息</param>
        [HttpPost]
        public async Task<AjaxResult<PatientOutDto>> GetPatient(PatientInDto dto)
        {
            AjaxResult<PatientOutDto> result = null;
            PatientOutDto data = null;

            try
            {
                data = await doCareService.GetPatient(dto);
                result = AjaxResult<PatientOutDto>.Success(data);
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
                result = new AjaxResult<PatientOutDto>($"{e.Message}", AjaxResultType.Error);
            }
            return result;
        }

        /// <summary>
        /// 查询门诊患者登记列表
        /// </summary>
        /// <param name="dto">患者登记查询入参信息</param>
        [HttpPost]
        public async Task<AjaxResult<List<PatientRegistOutDto>>> QueryRegist(PatientRegistInDto dto)
        {
            AjaxResult<List<PatientRegistOutDto>> result = null;
            List<PatientRegistOutDto> data = null;

            try
            {
                data = await doCareService.QueryRegist(dto);
                result = new AjaxResult<List<PatientRegistOutDto>>("数据读取成功", AjaxResultType.Success, data);
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
                result = new AjaxResult<List<PatientRegistOutDto>>($"{e.Message}", AjaxResultType.Error);
            }
            return result;
        }

        /// <summary>
        /// 查询患者检查信息
        /// </summary>
        [HttpPost]
        public async Task<AjaxResult<List<ExamReportOutDto>>> QueryPacs(CommonInDto dto)
        {
            AjaxResult<List<ExamReportOutDto>> result = null;
            List<ExamReportOutDto> data = null;

            try
            {
                data = await doCareService.QueryPacs(dto);
                result = new AjaxResult<List<ExamReportOutDto>>("数据读取成功", AjaxResultType.Success, data);
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
                result = new AjaxResult<List<ExamReportOutDto>>($"{e.Message}", AjaxResultType.Error);
            }
            return result;
        }

        /// <summary>
        /// 查询患者检验信息
        /// </summary>
        [HttpPost]
        public async Task<AjaxResult<List<LisReportOutDto>>> QueryLab(CommonInDto dto)
        {
            AjaxResult<List<LisReportOutDto>> result = null;
            List<LisReportOutDto> data = null;

            try
            {
                data = await doCareService.QueryLab(dto);
                result = new AjaxResult<List<LisReportOutDto>>("数据读取成功", AjaxResultType.Success, data);
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
                result = new AjaxResult<List<LisReportOutDto>>($"{e.Message}", AjaxResultType.Error);
            }
            return result;
        }
    }
}
