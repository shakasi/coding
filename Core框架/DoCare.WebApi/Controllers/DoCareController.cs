using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using VNext.Data;

namespace DoCare.Hosting.Apis.Controllers
{
    /// <summary>
    /// 发热门诊接口定义
    /// </summary>
    public class DoCareController : SiteApiControllerBase
    {
        //private readonly DoCareService doCareService;
        //public DoCareController(DoCareService doCareService)
        //{
        //    this.doCareService = doCareService;
        //}

        /// <summary>
        /// 获取患者信息
        /// </summary>
        /// <param name="dto">患者查询入参信息</param>
        [HttpPost]
        public async Task<AjaxResult> GetPatient()
        {
            AjaxResult result = null;

            try
            {
                result = AjaxResult.Success(null);
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
                result = new AjaxResult($"{e.Message}", AjaxResultType.Error);
            }
            return result;
        }
    }
}