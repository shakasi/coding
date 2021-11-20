using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VNext.Entity;
using VNext.Data;
using VNext.Extensions;
using VNext.Mapping;
using VNext.Systems;
using DoCare.WebService;
using DoCare.Hosting.Dtos;
using DoCare.Hosting.IContracts;
using DoCare.Hosting.Models;

namespace DoCare.Hosting
{
    public partial class HospitalService : IExam
    {
        protected ISqlExecutor ExamSqlExecutor => ServiceProvider.GetSqlExecutor("pacs");

        public async Task<List<ExamReportOutDto>> QueryExam(CommonInDto dto)
        {
            List<ExamReportOutDto> result = null;
            PUB0020SoapClient ws = new PUB0020SoapClient();
            try
            {
                Check.NotNull(dto?.VisitId, nameof(dto.VisitId));
                string requestStr1 = "MES0084";
                string requestStr2 = $"<Request><PATIENT_NO>{dto.VisitId}</PATIENT_NO></Request>";
                Logger.LogWarning($"HospitalService.QueryExam 调用参数1 : {requestStr1}  参数2 : {requestStr2}");
                var response = await ws.HIPMessageServerAsync(requestStr1, requestStr2);
                var xmlStr = response?.Body?.HIPMessageServerResult;
                var xmlEnty = SerializeHelper.FromXml<ExamReportResponse>(xmlStr);
                if (xmlEnty?.ResultCode == "-1")
                {
                    Logger.LogWarning("HospitalService.QueryExam 调平台失败：" + xmlEnty?.ResultContent);
                    return result;
                }
                else
                {
                    Logger.LogWarning("HospitalService.QueryExam 调平台返回:" + xmlStr);
                }
                var list = xmlEnty?.ExamReportList?.ExamReports;
                if (list != null && list.Count() > 0)
                {
                    result = list.MapTo<List<ExamReportOutDto>>();
                }
            }
            catch (Exception ex)
            {
                var errNo = ex.StackTrace.Substring(ex.StackTrace.IndexOf("line") + 5);
                Logger.LogWarning($"行号：{errNo}，{ex.FormatMessage()}");
            }
            return result;
        }
    }
}