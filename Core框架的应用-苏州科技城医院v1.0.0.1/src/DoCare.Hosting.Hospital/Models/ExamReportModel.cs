using System;
using System.ComponentModel;
using System.Xml.Serialization;
using VNext.Entity;

namespace DoCare.Hosting.IContracts
{
    [Description("3.1-患者检查报告表")]
    public class ExamReportModel : IEntityDapper
    {
        ///<summary>
        /// 获取或设置 患者编号
        ///</summary>
        [XmlElement(ElementName = "VISIT_NO")]
        public string PatientId { get; set; }

        ///<summary>
        /// 获取或设置 就诊编号
        ///</summary>
        [XmlElement(ElementName = "VISIT_ID")]
        public string VisitId { get; set; }

        ///<summary>
        /// 获取或设置 检查单号
        ///</summary>
        [XmlElement(ElementName = "EXAM_NO")]
        public string ExamNo { get; set; }

        ///<summary>
        /// 获取或设置 检查项目类别
        ///</summary>
        [XmlElement(ElementName = "EXAM_CLASS")]
        public string ExamClass { get; set; }

        ///<summary>
        /// 获取或设置 检查项目子类
        ///</summary>
        [XmlElement(ElementName = "EXAM_SUB_CLASS")]
        public string ExamSubClass { get; set; }

        ///<summary>
        /// 获取或设置 临床诊断
        ///</summary>
        [XmlElement(ElementName = "CLIN_DIAG")]
        public string ClinDiag { get; set; }

        ///<summary>
        /// 获取或设置 检查方式
        ///</summary>
        [XmlElement(ElementName = "EXAM_MODE")]
        public string ExamMode { get; set; }

        ///<summary>
        /// 获取或设置 使用仪器
        ///</summary>
        [XmlElement(ElementName = "DEVICE")]
        public string Device { get; set; }

        ///<summary>
        /// 获取或设置 执行科室
        ///</summary>
        [XmlElement(ElementName = "PERFORMED_BY")]
        public string PerformedBy { get; set; }

        ///<summary>
        /// 获取或设置 申请日期及时间
        ///</summary>
        [XmlElement(ElementName = "REQ_DATE_TIME")]
        public string ReqDateTime { get; set; }

        ///<summary>
        /// 获取或设置 申请科室
        ///</summary>
        [XmlElement(ElementName = "REQ_DEPT")]
        public string ReqDept { get; set; }

        ///<summary>
        /// 获取或设置 申请医生
        ///</summary>
        [XmlElement(ElementName = "REQ_PHYSICIAN")]
        public string ReqPhysician { get; set; }

        ///<summary>
        /// 获取或设置 申请备注
        ///</summary>
        [XmlElement(ElementName = "REQ_MEMO")]
        public string ReqMemo { get; set; }

        ///<summary>
        /// 获取或设置 注意事项
        ///</summary>
        [XmlElement(ElementName = "NOTICE")]
        public string Notice { get; set; }

        ///<summary>
        /// 获取或设置 检查开始日期时间
        ///</summary>
        [XmlElement(ElementName = "EXAM_START_DATE")]
        public string ExamStartDate { get; set; }

        ///<summary>
        /// 获取或设置 检查结束日期时间
        ///</summary>
        [XmlElement(ElementName = "EXAM_END_DATE")]
        public string ExamEndDate { get; set; }

        ///<summary>
        /// 获取或设置 报告日期及时间
        ///</summary>
        [XmlElement(ElementName = "REPORT_DATE_TIME")]
        public string ReportDateTime { get; set; }

        ///<summary>
        /// 获取或设置 检查参数（部位）
        ///</summary>
        [XmlElement(ElementName = "EXAM_PARA")]
        public string ExamPara { get; set; }

        ///<summary>
        /// 获取或设置 检查所见
        ///</summary>
        [XmlElement(ElementName = "DESCRIPTION")]
        public string Description { get; set; }

        ///<summary>
        /// 获取或设置 检查结论或建议
        ///</summary>
        [XmlElement(ElementName = "RECOMMENDATION")]
        public string Recommendation { get; set; }

        ///<summary>
        /// 获取或设置 操作者
        ///</summary>
        [XmlElement(ElementName = "TECHNICIAN")]
        public string Technician { get; set; }

        ///<summary>
        /// 获取或设置 报告者
        ///</summary>
        [XmlElement(ElementName = "REPORTER")]
        public string Reporter { get; set; }

        ///<summary>
        /// 获取或设置 检查结果状态
        ///</summary>
        [XmlElement(ElementName = "RESULT_STATUS")]
        public string ResultStatus { get; set; }

        ///<summary>
        /// 获取或设置 审核者
        ///</summary>
        [XmlElement(ElementName = "VERIFIED_BY")]
        public string VerifiedBy { get; set; }

        ///<summary>
        /// 获取或设置 审核时间
        ///</summary>
        [XmlElement(ElementName = "VERIFIED_DATE_TIME")]
        public string VerifiedDateTime { get; set; }

    }
}