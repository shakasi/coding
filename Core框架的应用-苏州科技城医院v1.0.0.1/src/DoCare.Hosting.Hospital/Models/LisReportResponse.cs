using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using DoCare.Hosting.IContracts;

namespace DoCare.Hosting.Models
{
    [XmlRoot("Response")]
    public class LisReportResponse1
    {
        [XmlElement(ElementName = "ResultContent")]
        public string ResultContent { get; set; }

        [XmlElement(ElementName = "ResultCode")]
        public string ResultCode { get; set; }

        [XmlElement(ElementName = "LabMasterList")]
        public LisReportList1 LabMasterList { get; set; }
    }

    public class LisReportList1
    {
        [XmlElement(ElementName = "LabMaster")]
        public List<LisReporttModel> LabMaster { get; set; }
    }

    [XmlRoot("Response")]
    public class LisReportResponse2
    {
        [XmlElement(ElementName = "ResultContent")]
        public string ResultContent { get; set; }

        [XmlElement(ElementName = "ResultCode")]
        public string ResultCode { get; set; }

        [XmlElement(ElementName = "LabResultList")]
        public LisReportList2 LabResultList { get; set; }
    }

    public class LisReportList2
    {
        [XmlElement(ElementName = "LabResult")]
        public List<LisReporttModel> LabResult { get; set; }
    }
}
