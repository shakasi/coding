using System;

namespace DoCare.Hosting.Dtos
{
    /// <summary>
    /// 检验申请单信息
    /// </summary>
    public class LisReportOutDto
    {
        ///<summary>
        /// 获取或设置 检验单号
        ///</summary>
        public string TestNo { get; set; }

        /// <summary>
        /// 检验类别
        /// </summary>
        public string InspectionClass { get; set; }

        ///<summary>
        /// 获取或设置 优先标志
        ///</summary>
        public string PriorityIndicator { get; set; }

        ///<summary>
        /// 获取或设置 病人检验姓名
        ///</summary>
        public string Name { get; set; }

        ///<summary>
        /// 获取或设置 检验目的
        ///</summary>
        public string TestCause { get; set; }

        ///<summary>
        /// 获取或设置 临床诊断
        ///</summary>
        public string RelevantClinicDiag { get; set; }

        ///<summary>
        /// 获取或设置 标本，如血、尿、痰等化验样本
        ///</summary>
        public string Specimen { get; set; }

        ///<summary>
        /// 获取或设置 上述样本的采样时间
        ///</summary>
        public DateTime? SpcmReceivedDateTime { get; set; }

        ///<summary>
        /// 获取或设置 检验申请时间
        ///</summary>
        public DateTime? ApplyTime { get; set; }

        ///<summary>
        /// 获取或设置 开出该申请单的科室：申请单科室代码
        ///</summary>
        public string OrderingDept { get; set; }

        ///<summary>
        /// 获取或设置 医生工号
        ///</summary>
        public string OrderingProvider { get; set; }

        ///<summary>
        /// 获取或设置 执行该申请单的科室：执行科室代码
        ///</summary>
        public string PerformedBy { get; set; }

        ///<summary>
        /// 获取或设置 反映申请的执行情况
        ///</summary>
        public string ResultStatus { get; set; }

        ///<summary>
        /// 获取或设置 检验报告完成时间
        ///</summary>
        public DateTime? ResultRptDateTime { get; set; }

        ///<summary>
        /// 获取或设置 报告者
        ///</summary>
        public string Transcriptionist { get; set; }

        ///<summary>
        /// 获取或设置 审核者
        ///</summary>
        public string VerifiedBy { get; set; }

        ///<summary>
        /// 获取或设置 序列号
        ///</summary>
        public string ItemNo { get; set; }

        ///<summary>
        /// 获取或设置 检验报告项目名称
        ///</summary>
        public string ReportItemName { get; set; }

        ///<summary>
        /// 获取或设置 检验报告项目代码
        ///</summary>
        public string ReportItemCode { get; set; }

        ///<summary>
        /// 获取或设置 结果正常与否标志
        ///</summary>
        public string AbnormalIndicator { get; set; }

        ///<summary>
        /// 获取或设置 检验结果
        ///</summary>
        public string Result { get; set; }

        ///<summary>
        /// 获取或设置 结果单位
        ///</summary>
        public string Units { get; set; }

        ///<summary>
        /// 获取或设置 检验日期及时间
        ///</summary>
        public DateTime? ResultDateTime { get; set; }

        ///<summary>
        /// 获取或设置 检验结果参考值
        ///</summary>
        public string ReferenceResult { get; set; }
    }
}
