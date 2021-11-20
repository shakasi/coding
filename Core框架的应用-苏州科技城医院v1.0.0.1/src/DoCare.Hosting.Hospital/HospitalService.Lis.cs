using DoCare.Hosting.Dtos;
using DoCare.Hosting.IContracts;
using DoCare.Hosting.Models;
using DoCare.WebService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VNext.Entity;
using VNext.Extensions;
using VNext.Systems;
using VNext.Mapping;
using VNext.Data;

namespace DoCare.Hosting
{
    public partial class HospitalService : ILis
    {
        protected ISqlExecutor LisSqlExecutor => ServiceProvider.GetSqlExecutor("lis");

        public async Task<List<LisReportOutDto>> QueryLisReport(CommonInDto dto)
        {
            List<LisReportOutDto> result = null;
            PUB0020SoapClient ws = new PUB0020SoapClient();
            try
            {
                //测试
                //try
                //{
                //    System.Xml.XmlDocument xmlaa = new System.Xml.XmlDocument();
                //    xmlaa.Load("aa.xml");
                //    var xmlEnty2 = SerializeHelper.FromXml<LisReportResponse2>(xmlaa.InnerXml);
                //    return null;
                //}
                //catch (Exception ex)
                //{
                //    var errNo = ex.StackTrace.Substring(ex.StackTrace.IndexOf("line") + 5);
                //    return null;
                //}

                Check.NotNull(dto?.VisitId, nameof(dto.VisitId));
                string requestStr1 = "MES0086";
                string requestStr2 = $"<Request><PATIENT_NO>{dto?.VisitId}</PATIENT_NO></Request>";
                Logger.LogWarning($"HospitalService.QueryLisReport 检验Master调用参数1 : {requestStr1}  参数2 : {requestStr2}");
                var response1 = await ws.HIPMessageServerAsync(requestStr1, requestStr2);
                var xmlStr1 = response1?.Body?.HIPMessageServerResult;
                var xmlEnty1 = SerializeHelper.FromXml<LisReportResponse1>(xmlStr1);
                if (xmlEnty1?.ResultCode == "-1")
                {
                    Logger.LogWarning("HospitalService.QueryLisReport 调平台失败1：" + xmlEnty1?.ResultContent);
                    return result;
                }
                else
                {
                    Logger.LogWarning("HospitalService.QueryLisReport 调平台返回1:" + xmlStr1);
                }

                var labMaster = xmlEnty1?.LabMasterList.LabMaster;
                if (labMaster?.Count > 0) result = new List<LisReportOutDto>();
                foreach (var masterModel in labMaster)
                {
                    if (string.IsNullOrWhiteSpace(masterModel.TestNo)) continue;
                    System.Threading.Thread.Sleep(100);
                    string requestStr3 = "MES0085";
                    string requestStr4 = $"<Request><TEST_NO>{masterModel?.TestNo}</TEST_NO></Request>";
                    Logger.LogWarning($"HospitalService.QueryLisReport 检验lab调用参数1 : {requestStr3}  参数2 : {requestStr4}");
                    var response2 = await ws.HIPMessageServerAsync(requestStr3, requestStr4);
                    var xmlStr2 = response2?.Body?.HIPMessageServerResult;
                    var xmlEnty2 = SerializeHelper.FromXml<LisReportResponse2>(xmlStr2);
                    if (xmlEnty2?.ResultCode == "-1")
                    {
                        Logger.LogWarning("HospitalService.QueryLisReport 调平台失败2：" + xmlEnty2?.ResultContent);
                        continue;
                    }
                    else
                    {
                        Logger.LogWarning("HospitalService.QueryLisReport 调平台返回2:" + xmlStr2);
                    }
                    var labResult = xmlEnty2?.LabResultList?.LabResult;
                    var resultItem = labResult?.MapTo<List<LisReportOutDto>>();
                    if (resultItem != null)
                    {
                        foreach (var labModel in resultItem)
                        {
                            labModel.TestNo = masterModel.TestNo;
                            labModel.InspectionClass = masterModel.InspectionClass;
                            labModel.PriorityIndicator = masterModel.PriorityIndicator;
                            labModel.Name = masterModel.Name;
                            labModel.TestCause = masterModel.TestCause;
                            labModel.RelevantClinicDiag = masterModel.RelevantClinicDiag;
                            labModel.Specimen = masterModel.Specimen;
                            labModel.SpcmReceivedDateTime = masterModel.SpcmReceivedDateTime;
                            labModel.ApplyTime = masterModel.ApplyTime;
                            labModel.OrderingDept = masterModel.OrderingDept;
                            labModel.OrderingProvider = masterModel.OrderingProvider;
                            labModel.PerformedBy = masterModel.PerformedBy;
                            labModel.ResultStatus = masterModel.ResultStatus;
                            labModel.ResultRptDateTime = masterModel.ResultsRptDateTime;
                            labModel.Transcriptionist = masterModel.Transcriptionist;
                            labModel.VerifiedBy = masterModel.VerifiedBy;
                        }
                    }
                    result.AddRange(resultItem);
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
