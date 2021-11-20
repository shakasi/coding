using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VNext.Entity;

namespace DoCare.Hosting.Entities
{
    /// <summary>
    /// 实体类：患者挂号信息
    /// </summary>
    [Description("患者挂号信息")]
    [TableNamePrefix("MED")]
    public partial class PatientRegist : EntityBase<Guid>
    {
        #region 患者信息
        /// <summary>
        /// 病历档案表主键编号
        /// </summary>
        [DisplayName("病历档案表主键编号"), StringLength(60)]
        public string PatientId { get; set; }

        /// <summary>
        /// 病人医保卡(院内就诊卡)编号
        /// </summary>
        [DisplayName("病人医保卡(院内就诊卡)编号"), StringLength(60)]
        public string CardId { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        [DisplayName("患者姓名"), StringLength(60)]
        public string Name { get; set; }

        /// <summary>
        /// 民族
        /// </summary>
        [DisplayName("民族"), StringLength(10)]
        public string Nation { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [DisplayName("性别"), StringLength(60)]
        public string Sex { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        [DisplayName("出生日期"), StringLength(60)]
        public string DateOfBirth { get; set; }

        /// <summary>
        /// 身份证编号
        /// </summary>
        [DisplayName("身份证号码"), StringLength(60)]
        public string IdNo { get; set; }

        /// <summary>
        /// 常用联系地址、家庭住址
        /// </summary>
        [DisplayName("常用联系地址、家庭住址")]
        public string MailingAddress { get; set; }

        /// <summary>
        /// 常用联系电话
        /// </summary>
        [DisplayName("常用联系电话"), StringLength(60)]
        public string PhoneNumberHome { get; set; }

        /// <summary>
        /// 单位联系电话
        /// </summary>
        [DisplayName("单位联系电话"), StringLength(60)]
        public string PhoneNumberBusiness { get; set; }

        #endregion

        #region 就诊信息
        /// <summary>
        /// 就诊编号
        /// </summary>
        [DisplayName("就诊编号"), StringLength(60)]
        public string VisitId { get; set; }

        /// <summary>
        /// 挂号时间
        /// </summary>
        [DisplayName("挂号时间")]
        public DateTime? REGIST_TIME { get; set; }

        /// <summary>
        /// 挂号科室
        /// </summary>
        [DisplayName("就诊编号"), StringLength(60)]
        public string DeptCode { get; set; }

        /// <summary>
        /// 初步诊断
        /// </summary>
        [DisplayName("初步诊断"), StringLength(2000)]
        public string DiagDesc { get; set; }

        #endregion
    }
}
