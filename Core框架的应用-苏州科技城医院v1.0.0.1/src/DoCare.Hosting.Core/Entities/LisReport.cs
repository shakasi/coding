using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VNext.Entity;

namespace DoCare.Hosting.Entities
{
    /// <summary>
    /// 实体类：患者检验报告单信息信息
    /// </summary>
    [Description("患者检验报告单信息信息")]
    [TableNamePrefix("MED")]
    public partial class LisReport : EntityBase<Guid>
    {
        ///<summary>
        /// 获取或设置 患者编号
        ///</summary>
        [DisplayName("病历档案表主键编号"), StringLength(60)]
        public string PatientId { get; set; }

        ///<summary>
        /// 获取或设置 就诊编号
        ///</summary>
        [DisplayName("就诊编号"), StringLength(60)]
        public string VisitId { get; set; }

        ///<summary>
        /// 获取或设置 检验单号
        ///</summary>
        [DisplayName("检验单号"), StringLength(60)]
        public string TestNo { get; set; }

        ///<summary>
        /// 获取或设置 优先标志
        ///</summary>
        [DisplayName("优先标志"), StringLength(60)]
        public string PriorityIndicator { get; set; }

        ///<summary>
        /// 获取或设置 病人检验姓名
        ///</summary>
        [DisplayName("病人检验姓名"), StringLength(60)]
        public string Name { get; set; }

        ///<summary>
        /// 获取或设置 检验目的
        ///</summary>
        [DisplayName("检验目的"), StringLength(500)]
        public string TestCause { get; set; }

        ///<summary>
        /// 获取或设置 临床诊断
        ///</summary>
        [DisplayName("临床诊断")]
        public string RelevantClinicDiag { get; set; }

        ///<summary>
        /// 获取或设置 标本，如血、尿、痰等化验样本
        ///</summary>
        [DisplayName("标本，如血、尿、痰等化验样本")]
        public string Specimen { get; set; }

        ///<summary>
        /// 获取或设置 上述样本的采样时间
        ///</summary>
        [DisplayName("上述样本的采样时间")]
        public DateTime? SpcmReceivedDateTime { get; set; }

        ///<summary>
        /// 获取或设置 检验申请时间
        ///</summary>
        [DisplayName("检验申请时间")]
        public DateTime? ApplyTime { get; set; }

        ///<summary>
        /// 获取或设置 开出该申请单的科室：科室代码
        ///</summary>
        [DisplayName("开出该申请单的科室：科室代码"), StringLength(60)]
        public string OrderingDept { get; set; }

        ///<summary>
        /// 获取或设置 医生工号
        ///</summary>
        [DisplayName("医生工号"), StringLength(60)]
        public string OrderingProvider { get; set; }

        ///<summary>
        /// 获取或设置 执行该申请单的科室：科室代码
        ///</summary>
        [DisplayName("执行该申请单的科室：科室代码"), StringLength(60)]
        public string PerformedBy { get; set; }

        ///<summary>
        /// 获取或设置 反映申请的执行情况
        ///</summary>
        [DisplayName("反映申请的执行情况"), StringLength(60)]
        public string ResultStatus { get; set; }

        ///<summary>
        /// 获取或设置 检验报告完成时间
        ///</summary>
        [DisplayName("检验报告完成时间"), StringLength(60)]
        public DateTime? ResultRptDateTime { get; set; }

        ///<summary>
        /// 获取或设置 报告者
        ///</summary>
        [DisplayName("报告者"), StringLength(60)]
        public string Transcriptionist { get; set; }

        ///<summary>
        /// 获取或设置 审核者
        ///</summary>
        [DisplayName("审核者"), StringLength(60)]
        public string VerifiedBy { get; set; }

        ///<summary>
        /// 获取或设置 序列号
        ///</summary>
        [DisplayName("序列号"), StringLength(60)]
        public string ItemNo { get; set; }

        ///<summary>
        /// 获取或设置 检验报告项目名称
        ///</summary>
        [DisplayName("检验报告项目名称"), StringLength(100)]
        public string ReportItemName { get; set; }

        ///<summary>
        /// 获取或设置 检验报告项目代码
        ///</summary>
        [DisplayName("检验报告项目代码"), StringLength(100)]
        public string ReportItemCode { get; set; }

        ///<summary>
        /// 获取或设置 结果正常与否标志
        ///</summary>
        [DisplayName("结果正常与否标志"), StringLength(60)]
        public string AbnormalIndicator { get; set; }

        ///<summary>
        /// 获取或设置 检验结果
        ///</summary>
        [DisplayName("检验结果")]
        public string Result { get; set; }

        ///<summary>
        /// 获取或设置 结果单位
        ///</summary>
        [DisplayName("结果单位"), StringLength(40)]
        public string Units { get; set; }

        ///<summary>
        /// 获取或设置 检验日期及时间
        ///</summary>
        [DisplayName("检验日期及时间")]
        public DateTime? ResultDateTime { get; set; }

        ///<summary>
        /// 获取或设置 检验结果参考值
        ///</summary>
        [DisplayName("检验结果参考值"), StringLength(500)]
        public string ReferenceResult { get; set; }
    }
}
