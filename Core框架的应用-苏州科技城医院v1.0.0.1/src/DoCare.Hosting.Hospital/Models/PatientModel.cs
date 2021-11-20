using System;
using System.ComponentModel;
using System.Xml.Serialization;
using VNext.Entity;

namespace DoCare.Hosting.IContracts
{
    [Description("1.1-患者病历档案信息")]
    public class PatientModel : IEntityDapper
    {
        /// <summary>
        /// 病历档案表主键编号
        /// </summary>
        [XmlElement(ElementName = "VISIT_NO"), Column("病历编号")]
        public string PatientId { get; set; }

        /// <summary>
        /// 病人医保卡(院内就诊卡)编号
        /// </summary>
        [XmlElement(ElementName = "CARD_ID"), Column("病人医保卡")]
        public string CardId { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        [XmlElement(ElementName = "NAME"), Column("患者姓名")]
        public string Name { get; set; }

        /// <summary>
        /// 民族
        /// </summary>
        [XmlElement(ElementName = "NATION"), Column("民族")]
        public string Nation { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [XmlElement(ElementName = "SEX"), Column("性别")]
        public string Sex { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        [XmlElement(ElementName = "DATE_OF_BIRTH"), Column("出生日期")]
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// 身份证编号
        /// </summary>
        [XmlElement(ElementName = "ID_NO"), Column("身份证号码")]
        public string IdNo { get; set; }

        /// <summary>
        /// 常用联系地址、家庭住址
        /// </summary>
        [XmlElement(ElementName = "MAILING_ADDRESS"), Column("常用联系地址")]
        public string MailingAddress { get; set; }

        /// <summary>
        /// 常用联系电话
        /// </summary>
        [XmlElement(ElementName = "PHONE_NUMBER_HOME"), Column("常用联系电话")]
        public string PhoneNumberHome { get; set; }

        /// <summary>
        /// 单位联系电话
        /// </summary>
        [XmlElement(ElementName = "PHONE_NUMBER_BUSINESS"), Column("单位联系电话")]
        public string PhoneNumberBusiness { get; set; }

    }
}