using System;
using System.ComponentModel;
using System.Xml.Serialization;
using VNext.Entity;

namespace DoCare.Hosting.IContracts
{
    [Description("2.1-患者检验报告明细表")]
    public class LisReporttModel : IEntityDapper
    {
        ///<summary>
        /// 获取或设置 病历档案表主键编号
        ///</summary>
        [XmlElement(ElementName = "VISIT_NO"), Column("病历编号")]
        public string PatientId { get; set; }

        ///<summary>
        /// 获取或设置 就诊编号
        ///</summary>
        [XmlElement(ElementName = "PATIENT_NO"), Column("就诊编号")]
        public string VisitId { get; set; }

        ///<summary>
        /// 获取或设置 检验单号
        ///</summary>
        [XmlElement(ElementName = "TEST_NO"), Column("检验单号")]
        public string TestNo { get; set; }

        /// <summary>
        /// 检验项目类别
        /// </summary>
        [Column("检验项目")]
        public string InspectionClass { get; set; }

        ///<summary>
        /// 获取或设置 优先标志
        ///</summary>
        [XmlElement(ElementName = "PRIORITY_INDICATOR"), Column("优先标志")]
        public string PriorityIndicator { get; set; }

        ///<summary>
        /// 获取或设置 病人检验姓名
        ///</summary>
        [XmlElement(ElementName = "NAME"), Column("病人检验姓名")]
        public string Name { get; set; }

        ///<summary>
        /// 获取或设置 检验目的
        ///</summary>
        [XmlElement(ElementName = "TEST_CAUSE"), Column("检验目的")]
        public string TestCause { get; set; }

        ///<summary>
        /// 获取或设置 临床诊断
        ///</summary>
        [XmlElement(ElementName = "RELEVANT_CLINIC_DIAG"), Column("临床诊断")]
        public string RelevantClinicDiag { get; set; }

        ///<summary>
        /// 获取或设置 标本，如血、尿、痰等化验样本
        ///</summary>
        [XmlElement(ElementName = "SPECIMEN"), Column("标本")]
        public string Specimen { get; set; }

        [XmlElement("SPCM_RECEIVED_DATE_TIME")]
        public string SpcmReceivedDateTimeStr
        {
            get { return SpcmReceivedDateTime == null ? "" : ((DateTime)SpcmReceivedDateTime).ToString("yyyy-MM-dd HH:mm:ss"); }
            set { if (string.IsNullOrEmpty(value)) SpcmReceivedDateTime = null; else SpcmReceivedDateTime = DateTime.Parse(value); }
        }
        ///<summary>
        /// 获取或设置 上述样本的采样时间
        ///</summary>
        [XmlIgnore, Column("采样时间")]
        public DateTime? SpcmReceivedDateTime { get; set; }

        [XmlElement("APPLY_TIME")]
        public string ApplyTimeStr
        {
            get { return ApplyTime == null ? "" : ((DateTime)ApplyTime).ToString("yyyy-MM-dd HH:mm:ss"); }
            set { if (string.IsNullOrEmpty(value)) ApplyTime = null; else ApplyTime = DateTime.Parse(value); }
        }

        ///<summary>
        /// 获取或设置 检验申请时间
        ///</summary>
        [XmlIgnore, Column("检验申请时间")]
        public DateTime? ApplyTime { get; set; }

        ///<summary>
        /// 获取或设置 开出该申请单的科室：申请单科室代码
        ///</summary>
        [XmlElement(ElementName = "ORDERING_DEPT"), Column("申请单科室代码")]
        public string OrderingDept { get; set; }

        ///<summary>
        /// 获取或设置 医生工号
        ///</summary>
        [XmlElement(ElementName = "ORDERING_PROVIDER"), Column("医生工号")]
        public string OrderingProvider { get; set; }

        ///<summary>
        /// 获取或设置 执行该申请单的科室：执行科室代码
        ///</summary>
        [Column("执行科室代码")]
        public string PerformedBy { get; set; }

        ///<summary>
        /// 获取或设置 反映申请的执行情况
        ///</summary>
        [XmlElement(ElementName = "RESULT_STATUS"), Column("反映申请的执行情况")]
        public string ResultStatus { get; set; }

        [XmlElement("RESULTS_RPT_DATE_TIME")]
        public string ResultsRptDateTimeStr
        {
            get { return ResultsRptDateTime == null ? "" : ((DateTime)ResultsRptDateTime).ToString("yyyy-MM-dd HH:mm:ss"); }
            set { if (string.IsNullOrEmpty(value)) ResultsRptDateTime = null; else ResultsRptDateTime = DateTime.Parse(value); }
        }
        ///<summary>
        /// 获取或设置 检验报告完成时间
        ///</summary>
        [XmlIgnore, Column("检验报告完成时间")]
        public DateTime? ResultsRptDateTime { get; set; }

        ///<summary>
        /// 获取或设置 报告者
        ///</summary>
        [XmlElement(ElementName = "TRANSCRIPTIONIST"), Column("报告者")]
        public string Transcriptionist { get; set; }

        ///<summary>
        /// 获取或设置 审核者
        ///</summary>
        [XmlElement(ElementName = "VERIFIED_BY"), Column("审核者")]
        public string VerifiedBy { get; set; }

        //----------------------------------------------------------------------------------------

        ///<summary>
        /// 获取或设置 序列号
        ///</summary>
        [XmlElement(ElementName = "ITEM_NO"), Column("序列号")]
        public string ItemNo { get; set; }

        ///<summary>
        /// 获取或设置 检验报告项目名称
        ///</summary>
        [XmlElement(ElementName = "REPORT_ITEM_NAME"), Column("检验报告项目名称")]
        public string ReportItemName { get; set; }

        ///<summary>
        /// 获取或设置 检验报告项目代码
        ///</summary>
        [XmlElement(ElementName = "REPORT_ITEM_CODE"), Column("检验报告项目代码")]
        public string ReportItemCode { get; set; }

        ///<summary>
        /// 获取或设置 结果正常与否标志
        ///</summary>
        [XmlElement(ElementName = "ABNORMAL_INDICATOR"), Column("结果正常与否标志")]
        public string AbnormalIndicator { get; set; }

        ///<summary>
        /// 获取或设置 检验结果
        ///</summary>
        [XmlElement(ElementName = "RESULT"), Column("检验结果")]
        public string Result { get; set; }

        ///<summary>
        /// 获取或设置 结果单位
        ///</summary>
        [XmlElement(ElementName = "UNITS"), Column("结果单位")]
        public string Units { get; set; }

        [XmlElement("RESULT_DATE_TIME")]
        public string ResultDateTimeStr
        {
            get { return ResultDateTime == null ? "" : ((DateTime)ResultDateTime).ToString("yyyy-MM-dd HH:mm:ss"); }
            set { if (string.IsNullOrEmpty(value)) ResultDateTime = null; else ResultDateTime = DateTime.Parse(value); }
        }

        ///<summary>
        /// 获取或设置 检验日期及时间
        ///</summary>
        [XmlIgnore, Column("检验日期及时间")]
        public DateTime? ResultDateTime { get; set; }

        ///<summary>
        /// 获取或设置 检验结果参考值
        ///</summary>
        [XmlElement(ElementName = "REFERENCE_RESULT"), Column("检验结果参考值")]
        public string ReferenceResult { get; set; }

    }
}