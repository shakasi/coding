using System;

namespace DoCare.Hosting.Dtos
{
    /// <summary>
    /// 患者登记查询入参信息
    /// </summary>
    public class PatientRegistInDto
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? BeginTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
    }
}
