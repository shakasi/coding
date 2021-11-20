using DoCare.Hosting.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VNext.AspNetCore.Mvc.Filters;
using VNext.Data;

namespace DoCare.Hosting.Apis.Controllers
{
    /// <summary>
    /// 平台消息
    /// </summary>
    public class SoapController : SiteApiControllerBase
    {
        private readonly DoCareService doCareService;
        public SoapController(DoCareService doCareService)
        {
            this.doCareService = doCareService;
        }

        /// <summary>
        /// 平台消息推送处理
        /// </summary>
        [HttpPost]
        [UnitOfWork]
        public async Task<AjaxResult> MessageRecive(SoapDto dto)
        {
            AjaxResult result = null;
            try
            {
                result = (await doCareService.MessageProcess(dto)).ToAjaxResult();
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
