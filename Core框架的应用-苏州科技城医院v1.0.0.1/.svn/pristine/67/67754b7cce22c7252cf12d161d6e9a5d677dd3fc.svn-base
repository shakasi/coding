using System;
using System.ComponentModel;
using System.Xml.Serialization;
using VNext.Entity;

namespace DoCare.Hosting.IContracts
{
    [Description("1.2-门诊挂号就诊信息表")]
    public class PatientRegistModel : PatientModel
    {
        /// <summary>
        /// 就诊编号
        /// </summary>
        [XmlElement(ElementName = "PATIENT_NO"), Column("就诊编号")]
        public string VisitId { get; set; }

        /// <summary>
        /// 挂号时间
        /// </summary>
        [XmlElement(ElementName = "REGIST_TIME"), Column("挂号时间")]
        public DateTime RegistTime { get; set; }

        /// <summary>
        /// 挂号科室
        /// </summary>
        [XmlElement(ElementName = "DEPT_CODE"), Column("挂号科室")]
        public string DeptCode { get; set; }

        /// <summary>
        /// 1:三香路院区;2:许墅关院区
        /// </summary>
        [XmlElement(ElementName = "OrgCode"), Column("院区编号")]
        public int OrgCode { get; set; }
    }
}