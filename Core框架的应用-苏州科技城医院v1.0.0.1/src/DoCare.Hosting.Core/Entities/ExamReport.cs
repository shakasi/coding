using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VNext.Entity;

namespace DoCare.Hosting.Entities
{
    /// <summary>
    /// 实体类：患者挂号信息
    /// </summary>
    [Description("检查报告")]
    [TableNamePrefix("MED")]
    public class ExamReport : EntityBase<Guid>
    {
        ///<summary>
        /// 获取或设置 患者编号
        ///</summary>
        [DisplayName("患者编号"), StringLength(60)]
        public string PatientId { get; set; }

        ///<summary>
        /// 获取或设置 就诊编号
        ///</summary>
        [DisplayName("就诊编号"), StringLength(60)]
        public string VisitId { get; set; }

        ///<summary>
        /// 获取或设置 检查单号
        ///</summary>
        [DisplayName("检查单号"), StringLength(100)]
        public string ExamNo { get; set; }

        ///<summary>
        /// 获取或设置 检查项目类别
        ///</summary>
        [DisplayName("检查项目类别"), StringLength(100)]
        public string ExamClass { get; set; }

        ///<summary>
        /// 获取或设置 检查项目子类
        ///</summary>
        [DisplayName("检查项目子类"), StringLength(100)]
        public string ExamSubClass { get; set; }

        ///<summary>
        /// 获取或设置 临床诊断
        ///</summary>
        [DisplayName("临床诊断"), StringLength(500)]
        public string ClinDiag { get; set; }

        ///<summary>
        /// 获取或设置 检查方式
        ///</summary>
        [DisplayName("检查方式"), StringLength(100)]
        public string ExamMode { get; set; }

        ///<summary>
        /// 获取或设置 使用仪器
        ///</summary>
        [DisplayName("使用仪器"), StringLength(100)]
        public string Device { get; set; }

        ///<summary>
        /// 获取或设置 执行科室
        ///</summary>
        [DisplayName("执行科室"), StringLength(100)]
        public string PerformedBy { get; set; }

        ///<summary>
        /// 获取或设置 申请日期及时间
        ///</summary>
        [DisplayName("申请日期及时间")]
        public DateTime? ReqDateTime { get; set; }

        ///<summary>
        /// 获取或设置 申请科室
        ///</summary>
        [DisplayName("申请科室"), StringLength(100)]
        public string ReqDept { get; set; }

        ///<summary>
        /// 获取或设置 申请医生
        ///</summary>
        [DisplayName("申请医生"), StringLength(100)]
        public string ReqPhysician { get; set; }

        ///<summary>
        /// 获取或设置 申请备注
        ///</summary>
        [DisplayName("申请备注"), StringLength(1000)]
        public string ReqMemo { get; set; }

        ///<summary>
        /// 获取或设置 注意事项
        ///</summary>
        [DisplayName("注意事项"), StringLength(60)]
        public string Notice { get; set; }

        ///<summary>
        /// 获取或设置 检查开始日期时间
        ///</summary>
        [DisplayName("检查开始日期时间")]
        public DateTime? ExamStartDate { get; set; }

        ///<summary>
        /// 获取或设置 检查结束日期时间
        ///</summary>
        [DisplayName("检查结束日期时间")]
        public DateTime? ExamEndDate { get; set; }

        ///<summary>
        /// 获取或设置 报告日期及时间
        ///</summary>
        [DisplayName("报告日期及时间"), StringLength(60)]
        public DateTime? ReportDateTime { get; set; }

        ///<summary>
        /// 获取或设置 检查参数（部位）
        ///</summary>
        [DisplayName("检查参数（部位）"), StringLength(100)]
        public string ExamPara { get; set; }

        ///<summary>
        /// 获取或设置 检查所见
        ///</summary>
        [DisplayName("检查所见")]
        public string Description { get; set; }

        ///<summary>
        /// 获取或设置 检查结论或建议
        ///</summary>
        [DisplayName("检查结论或建议")]
        public string Recommendation { get; set; }

        ///<summary>
        /// 获取或设置 操作者
        ///</summary>
        [DisplayName("操作者"), StringLength(100)]
        public string Technician { get; set; }

        ///<summary>
        /// 获取或设置 报告者
        ///</summary>
        [DisplayName("报告者"), StringLength(100)]
        public string Reporter { get; set; }

        ///<summary>
        /// 获取或设置 检查结果状态
        ///</summary>
        [DisplayName("检查结果状态"), StringLength(100)]
        public string resultStatus { get; set; }

        ///<summary>
        /// 获取或设置 审核者
        ///</summary>
        [DisplayName("审核者"), StringLength(100)]
        public string VerifiedBy { get; set; }

        ///<summary>
        /// 获取或设置 审核时间
        ///</summary>
        [DisplayName("审核时间")]
        public DateTime? VerifiedDateTime { get; set; }
    }
}