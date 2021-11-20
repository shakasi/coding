using System;

namespace DoCare.Hosting.Dtos
{
    /// <summary>
    /// 检查输出信息
    /// </summary>
    public class ExamReportOutDto
    {
        ///<summary>
        /// 获取或设置 检查单号
        ///</summary>
        public string ExamNo { get; set; }

        ///<summary>
        /// 获取或设置 检查项目类别
        ///</summary>
        public string ExamClass { get; set; }

        ///<summary>
        /// 获取或设置 检查项目子类
        ///</summary>
        public string ExamSubClass { get; set; }

        ///<summary>
        /// 获取或设置 临床诊断
        ///</summary>
        public string ClinDiag { get; set; }

        ///<summary>
        /// 获取或设置 检查方式
        ///</summary>
        public string ExamMode { get; set; }

        ///<summary>
        /// 获取或设置 使用仪器
        ///</summary>
        public string Device { get; set; }

        ///<summary>
        /// 获取或设置 执行科室
        ///</summary>
        public string PerformedBy { get; set; }

        ///<summary>
        /// 获取或设置 申请日期及时间
        ///</summary>
        public DateTime? ReqDateTime { get; set; }

        ///<summary>
        /// 获取或设置 申请科室
        ///</summary>
        public string ReqDept { get; set; }

        ///<summary>
        /// 获取或设置 申请医生
        ///</summary>
        public string ReqPhysician { get; set; }

        ///<summary>
        /// 获取或设置 申请备注
        ///</summary>
        public string ReqMemo { get; set; }

        ///<summary>
        /// 获取或设置 注意事项
        ///</summary>
        public string Notice { get; set; }

        ///<summary>
        /// 获取或设置 检查开始日期时间
        ///</summary>
        public DateTime? ExamStartDate { get; set; }

        ///<summary>
        /// 获取或设置 检查结束日期时间
        ///</summary>
        public DateTime? ExamEndDate { get; set; }

        ///<summary>
        /// 获取或设置 报告日期及时间
        ///</summary>
        public DateTime? ReportDateTime { get; set; }

        ///<summary>
        /// 获取或设置 检查参数（部位）
        ///</summary>
        public string ExamPara { get; set; }

        ///<summary>
        /// 获取或设置 检查所见
        ///</summary>
        public string Description { get; set; }

        ///<summary>
        /// 获取或设置 检查结论或建议
        ///</summary>
        public string Recommendation { get; set; }

        ///<summary>
        /// 获取或设置 操作者
        ///</summary>
        public string Technician { get; set; }

        ///<summary>
        /// 获取或设置 报告者
        ///</summary>
        public string Reporter { get; set; }

        ///<summary>
        /// 获取或设置 检查结果状态
        ///</summary>
        public string resultStatus { get; set; }

        ///<summary>
        /// 获取或设置 审核者
        ///</summary>
        public string VerifiedBy { get; set; }

        ///<summary>
        /// 获取或设置 审核时间
        ///</summary>
        public DateTime? VerifiedDateTime { get; set; }
    }
}
