using System;

namespace DoCare.Hosting.Dtos
{
    /// <summary>
    /// 患者登记输出信息
    /// </summary>
    public class PatientRegistOutDto: PatientOutDto
    {
        /// <summary>
        /// 就诊编号
        /// </summary>
        public string VisitId { get; set; }

        /// <summary>
        /// 挂号时间
        /// </summary>
        public DateTime? REGIST_TIME { get; set; }

        /// <summary>
        /// 挂号科室
        /// </summary>
        public string DeptCode { get; set; }

    }
}
