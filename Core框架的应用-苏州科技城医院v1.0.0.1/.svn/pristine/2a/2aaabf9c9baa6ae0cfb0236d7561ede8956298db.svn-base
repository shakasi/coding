using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using DoCare.Hosting.IContracts;

namespace DoCare.Hosting.Models
{
    //[XmlRoot("Response")]
    //public class PatientInfoResponse
    //{

    //    [JsonProperty("VIEW_PATIENT_INFO")]
    //    public List<PatientModel> PatientInfoList { get; set; }
    //}

    [XmlRoot("Response")]
    public class PatientInfoResponse
    {
        [XmlElement(ElementName = "ResultContent")]
        public string ResultContent { get; set; }

        [XmlElement(ElementName = "ResultCode")]
        public string ResultCode { get; set; }

        [XmlElement(ElementName = "PatientInfo")]
        public PatientModelList PatientInfo { get; set; }
    }

    public class PatientModelList
    {
        [XmlElement(ElementName = "PatientInfo")]
        public List<PatientModel> PatientInfo { get; set; }
    }
}
