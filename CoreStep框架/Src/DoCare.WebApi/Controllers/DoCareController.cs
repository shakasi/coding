using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
//using VNext.Data;

namespace DoCare.Hosting.Apis.Controllers
{
    public class DoCareController : SiteApiControllerBase
    {
        [HttpPost]
        //public async Task<AjaxResult> GetPatient()
        public async Task GetPatient()
        {
            //return null;
        }
    }
}