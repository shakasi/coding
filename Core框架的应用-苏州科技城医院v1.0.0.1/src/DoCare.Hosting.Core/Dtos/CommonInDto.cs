using System;
using VNext.Data;

namespace DoCare.Hosting.Dtos
{
    /// <summary>
    /// 查询：通用入参
    /// </summary>
    public class CommonInDto
    {
        /// <summary>
        /// 患者病历ID
        /// </summary>
        public string PatientId { get; set; }

        /// <summary>
        /// 患者就诊ID
        /// </summary>
        public string VisitId { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdNo { get; set; }

        /// <summary>
        /// 挂号时间
        /// </summary>
        public DateTime? RegistTime { get; set; }
    }

    public class SoapDto: IInputDto<Guid>
    {
        public Guid Id { get; set; }

        public string Context { get; set; }

        public SoapType Type { get; set; }
    }
}
