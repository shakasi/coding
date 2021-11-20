using DoCare.Hosting.Dtos;
using DoCare.Hosting.Entities;
using DoCare.Hosting.IContracts;
using DoCare.Hosting.Models;
using DoCare.WebService;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VNext.Data;
using VNext.Dependency;
using VNext.Entity;
using VNext.Extensions;
using VNext.Systems;
using VNext.Mapping;

namespace DoCare.Hosting
{
    [Dependency(ServiceLifetime.Scoped, AddSelf = false)]
    public partial class HospitalService : IPatient
    {
        /// <summary>
        /// 初始化一个<see cref="PatientService"/>类型的新实例
        /// </summary>
        public HospitalService(IServiceProvider provider)
        {
            ServiceProvider = provider;
            Logger = provider.GetLogger(this.GetType());
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
        /// 获取或设置 患者挂号信息
        /// </summary>
        protected IRepository<PatientRegist, Guid> PatientRegistRepository => ServiceProvider.GetService<IRepository<PatientRegist, Guid>>();

        #endregion

        #region 接口实现        

        protected ISqlExecutor HisSqlExecutor => ServiceProvider.GetSqlExecutor("his");

        /// <summary>
        /// 获取患者信息
        /// </summary>
        public async Task<PatientOutDto> GetPatient(PatientInDto dto)
        {
            PatientOutDto result = null;
            var cardType = dto.CardType.ToEnum<CardType>();
            if (cardType != CardType.IdNo || string.IsNullOrEmpty(dto?.CardNo)) return result;

            PUB0020SoapClient ws = new PUB0020SoapClient();
            string requestStr1 = "MES0081";
            string requestStr2 = $"<Request><BeginTime>{DateTime.Now.AddYears(-5).ToString("yyyy-MM-dd")}</BeginTime><EndTime>{DateTime.Now.ToString("yyyy-MM-dd")}</EndTime><PatientType>O</PatientType><PatientName></PatientName><DiagnosisDesc></DiagnosisDesc><IdCard>{dto.CardNo}</IdCard><VisitNo></VisitNo></Request>";
            Logger.LogWarning($"HospitalService.GetPatient 调用参数1 : {requestStr1}  参数2 : {requestStr2}");
            try
            {
                //测试
                //try
                //{
                //    System.Xml.XmlDocument xmlaa = new System.Xml.XmlDocument();
                //    xmlaa.Load("aa.xml");
                //    var listaa = SerializeHelper.FromXml<PatientInfoResponse>(xmlaa.InnerXml);
                //    return null;
                //}
                //catch (Exception ex)
                //{
                //    return null;
                //}

                var response = await ws.HIPMessageServerAsync(requestStr1, requestStr2);
                var xmlStr = response?.Body?.HIPMessageServerResult;
                var xmlEnty = SerializeHelper.FromXml<PatientInfoResponse>(xmlStr);

                if (xmlEnty?.ResultCode == "-1")
                {
                    Logger.LogWarning("HospitalService.GetPatientInfo 调平台失败：" + xmlEnty?.ResultContent);
                    return result;
                }
                else
                {
                    Logger.LogWarning("HospitalService.GetPatientInfo 调平台返回:" + xmlStr);
                }

                var model = xmlEnty?.PatientInfo?.PatientInfo?.FirstOrDefault();
                if (model != null)
                {
                    result = model.MapTo<PatientOutDto>();
                }
            }
            catch (Exception ex)
            {
                //Logger.LogError($"HospitalService.GetPatientInfo 报错：{ex.Message}", ex);
                var errNo = ex.StackTrace.Substring(ex.StackTrace.IndexOf("line") + 5);
                Logger.LogWarning($"行号：{errNo},{ex.FormatMessage()}");
            }
            return result;

            //var sql = HospitalOption.SqlOptions.First(a => string.Equals(a.Id, "pacs", StringComparison.OrdinalIgnoreCase)).ToString();
            //Logger.LogWarning($"[SQL]:{sql}");
            //var examReportEntities = await ExamSqlExecutor.FromSqlAsync<ExamReportModel>(sql, new { PatientId = dto.PatientId, BeginTime = dto.RegistTime.Value.ToStringYMDHMS(), EndTime = dto.RegistTime.Value.AddDays(HospitalOption.Days.Value).ToStringYMDHMS() });
            //if (examReportEntities != null && examReportEntities.Count() > 0)
            //{
            //    examDtos = examReportEntities.MapTo<List<ExamReportOutDto>>();
            //}
        }

        /// <summary>
        /// 查询对应条件的患者基本信息
        /// </summary>
        public async Task<List<PatientRegistOutDto>> QueryRegist(PatientRegistInDto dto)
        {
            List<PatientRegistOutDto> result = null;
            PUB0020SoapClient ws = new PUB0020SoapClient();
            try
            {
                Check.NotNull(dto?.BeginTime, nameof(dto.BeginTime));
                Check.NotNull(dto?.EndTime, nameof(dto.EndTime));

                string requestStr1 = "MES0081";
                string requestStr2 = $"<Request><BeginTime>{((DateTime)dto.BeginTime).ToString("yyyy-MM-dd")}</BeginTime><EndTime>{((DateTime)dto.EndTime).ToString("yyyy-MM-dd")}</EndTime><PatientType>O</PatientType><PatientName></PatientName><DiagnosisDesc></DiagnosisDesc><IdCard></IdCard><VisitNo></VisitNo></Request>";
                Logger.LogWarning($"HospitalService.QueryRegist 调用参数1 : {requestStr1}  参数2 : {requestStr2}");
                var response = await ws.HIPMessageServerAsync(requestStr1, requestStr2);
                var xmlStr = response?.Body?.HIPMessageServerResult;
                var xmlEnty = SerializeHelper.FromXml<PatientRegistResponse>(xmlStr);
                if (xmlEnty?.ResultCode == "-1")
                {
                    Logger.LogWarning("HospitalService.QueryRegist 调平台失败：" + xmlEnty?.ResultContent);
                    return result;
                }
                else
                {
                    Logger.LogWarning("HospitalService.QueryRegist 调平台返回:" + xmlStr);
                }
                var modelList = xmlEnty?.PatientInfo?.PatientRegist;
                if (modelList != null && modelList.Count > 0)
                {
                    result = modelList.MapTo<List<PatientRegistOutDto>>();
                }
            }
            catch (Exception ex)
            {
                var errNo = ex.StackTrace.Substring(ex.StackTrace.IndexOf("line") + 5);
                Logger.LogWarning($"行号：{errNo}，{ex.FormatMessage()}");
            }
            return result;
        }
        #endregion
        #region 辅助方法
        #endregion
    }
}
