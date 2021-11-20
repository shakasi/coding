using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using DoCare.Hosting.IContracts;

namespace DoCare.Hosting.Models
{
    [XmlRoot("Response")]
    public class PatientRegistResponse
    {
        [XmlElement(ElementName = "ResultContent")]
        public string ResultContent { get; set; }

        [XmlElement(ElementName = "ResultCode")]
        public string ResultCode { get; set; }

        [XmlElement(ElementName = "PatientInfo")]
        public PatientRegistModelList PatientInfo { get; set; }
    }

    public class PatientRegistModelList
    {
        [XmlElement(ElementName = "PatientInfo")]
        public List<PatientRegistModel> PatientRegist { get; set; }
    }
}
