using AutoMapper.Configuration;
using DoCare.Hosting.IContracts;
using System;
using VNext.AutoMapper;

namespace DoCare.Hosting.Dtos
{
    public class AutoMapperConfiguration : IAutoMapperConfiguration
    {
        public void CreateMaps(MapperConfigurationExpression mapper)
        {
            #region 患者信息
            mapper.CreateMap<PatientModel, PatientOutDto>()
                  .Include<PatientRegistModel, PatientRegistOutDto>()
           .ForMember(rn => rn.PatientId, opt => opt.MapFrom(r => r.PatientId))
           .ForMember(rn => rn.CardId, opt => opt.MapFrom(r => r.CardId))
           .ForMember(rn => rn.Name, opt => opt.MapFrom(r => r.Name))
           .ForMember(rn => rn.Nation, opt => opt.MapFrom(r => r.Nation))
           .ForMember(rn => rn.Sex, opt => opt.MapFrom(r => ConvertSex(r.Sex).ToString("d")))
           .ForMember(rn => rn.DateOfBirth, opt => opt.MapFrom(r => r.DateOfBirth))
           .ForMember(rn => rn.IdNo, opt => opt.MapFrom(r => r.IdNo))
           .ForMember(rn => rn.MailingAddress, opt => opt.MapFrom(r => r.MailingAddress))
           .ForMember(rn => rn.PhoneNumberHome, opt => opt.MapFrom(r => r.PhoneNumberHome))
           .ForMember(rn => rn.PhoneNumberBusiness, opt => opt.MapFrom(r => r.PhoneNumberBusiness));

            mapper.CreateMap<PatientRegistModel, PatientRegistOutDto>()
            .ForMember(rn => rn.VisitId, opt => opt.MapFrom(r => r.VisitId))
            .ForMember(rn => rn.REGIST_TIME, opt => opt.MapFrom(r => r.RegistTime))
            .ForMember(rn => rn.DeptCode, opt => opt.MapFrom(r => r.DeptCode));

            #endregion

            #region 检查信息

            mapper.CreateMap<ExamReportModel, ExamReportOutDto>()
                .ForMember(rn => rn.ExamNo, opt => opt.MapFrom(r => r.ExamNo))
                .ForMember(rn => rn.ExamClass, opt => opt.MapFrom(r => r.ExamClass))
                .ForMember(rn => rn.ExamSubClass, opt => opt.MapFrom(r => r.ExamSubClass))
                .ForMember(rn => rn.ClinDiag, opt => opt.MapFrom(r => r.ClinDiag))
                .ForMember(rn => rn.ExamMode, opt => opt.MapFrom(r => r.ExamMode))
                .ForMember(rn => rn.Device, opt => opt.MapFrom(r => r.Device))
                .ForMember(rn => rn.PerformedBy, opt => opt.MapFrom(r => r.PerformedBy))
                .ForMember(rn => rn.ReqDateTime, opt => opt.MapFrom(r => r.ReqDateTime))
                .ForMember(rn => rn.ReqDept, opt => opt.MapFrom(r => r.ReqDept))
                .ForMember(rn => rn.ReqPhysician, opt => opt.MapFrom(r => r.ReqPhysician))
                .ForMember(rn => rn.ReqMemo, opt => opt.MapFrom(r => r.ReqMemo))
                .ForMember(rn => rn.Notice, opt => opt.MapFrom(r => r.Notice))
                .ForMember(rn => rn.ExamStartDate, opt => opt.MapFrom(r => r.ExamStartDate))
                .ForMember(rn => rn.ExamEndDate, opt => opt.MapFrom(r => r.ExamEndDate))
                .ForMember(rn => rn.ReportDateTime, opt => opt.MapFrom(r => r.ReportDateTime))
                .ForMember(rn => rn.ExamPara, opt => opt.MapFrom(r => r.ExamPara))
                .ForMember(rn => rn.Description, opt => opt.MapFrom(r => r.Description))
                .ForMember(rn => rn.Recommendation, opt => opt.MapFrom(r => r.Recommendation))
                .ForMember(rn => rn.Technician, opt => opt.MapFrom(r => r.Technician))
                .ForMember(rn => rn.Reporter, opt => opt.MapFrom(r => r.Reporter))
                .ForMember(rn => rn.resultStatus, opt => opt.MapFrom(r => r.ResultStatus))
                .ForMember(rn => rn.VerifiedBy, opt => opt.MapFrom(r => r.VerifiedBy))
                .ForMember(rn => rn.VerifiedDateTime, opt => opt.MapFrom(r => r.VerifiedDateTime));

            #endregion

            #region 检验信息

            mapper.CreateMap<LisReporttModel, LisReportOutDto>()
                .ForMember(rn => rn.TestNo, opt => opt.MapFrom(r => r.TestNo))
                .ForMember(rn => rn.InspectionClass, opt => opt.MapFrom(r => r.InspectionClass))
                .ForMember(rn => rn.PriorityIndicator, opt => opt.MapFrom(r => r.PriorityIndicator))
                .ForMember(rn => rn.Name, opt => opt.MapFrom(r => r.Name))
                .ForMember(rn => rn.TestCause, opt => opt.MapFrom(r => r.TestCause))
                .ForMember(rn => rn.RelevantClinicDiag, opt => opt.MapFrom(r => r.RelevantClinicDiag))
                .ForMember(rn => rn.Specimen, opt => opt.MapFrom(r => r.Specimen))
                .ForMember(rn => rn.SpcmReceivedDateTime, opt => opt.MapFrom(r => r.SpcmReceivedDateTime))
                .ForMember(rn => rn.ApplyTime, opt => opt.MapFrom(r => r.ApplyTime))
                .ForMember(rn => rn.OrderingDept, opt => opt.MapFrom(r => r.OrderingDept))
                .ForMember(rn => rn.OrderingProvider, opt => opt.MapFrom(r => r.OrderingProvider))
                .ForMember(rn => rn.PerformedBy, opt => opt.MapFrom(r => r.PerformedBy))
                .ForMember(rn => rn.ResultStatus, opt => opt.MapFrom(r => r.ResultStatus))
                .ForMember(rn => rn.ResultRptDateTime, opt => opt.MapFrom(r => r.ResultsRptDateTime))
                .ForMember(rn => rn.Transcriptionist, opt => opt.MapFrom(r => r.Transcriptionist))
                .ForMember(rn => rn.VerifiedBy, opt => opt.MapFrom(r => r.VerifiedBy))
                .ForMember(rn => rn.ItemNo, opt => opt.MapFrom(r => r.ItemNo))
                .ForMember(rn => rn.ReportItemName, opt => opt.MapFrom(r => r.ReportItemName))
                .ForMember(rn => rn.ReportItemCode, opt => opt.MapFrom(r => r.ReportItemCode))
                .ForMember(rn => rn.AbnormalIndicator, opt => opt.MapFrom(r => r.AbnormalIndicator))
                .ForMember(rn => rn.Result, opt => opt.MapFrom(r => r.Result))
                .ForMember(rn => rn.Units, opt => opt.MapFrom(r => r.Units))
                .ForMember(rn => rn.ResultDateTime, opt => opt.MapFrom(r => r.ResultDateTime))
                .ForMember(rn => rn.ReferenceResult, opt => opt.MapFrom(r => r.ReferenceResult));

            #endregion
        }

        private SexType ConvertSex(string sex)
        {
            SexType result = SexType.UnKnow;
            if (string.Equals(sex, "M", StringComparison.OrdinalIgnoreCase))
            {
                result = SexType.Male;
            }
            else if (string.Equals(sex, "F", StringComparison.OrdinalIgnoreCase))
            {
                result = SexType.Female;
            }
            return result;
        }
    }
}
