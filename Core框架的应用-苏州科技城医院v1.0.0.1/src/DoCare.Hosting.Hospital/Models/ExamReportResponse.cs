using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using DoCare.Hosting.IContracts;

namespace DoCare.Hosting.Models
{
    [XmlRoot("Response")]
    public class ExamReportResponse
    {
        [XmlElement(ElementName = "ResultContent")]
        public string ResultContent { get; set; }

        [XmlElement(ElementName = "ResultCode")]
        public string ResultCode { get; set; }

        [XmlElement(ElementName = "ExamReportList")]
        public ExamReportList ExamReportList { get; set; }
    }

    public class ExamReportList
    {
        [XmlElement(ElementName = "ExamReport")]
        public List<ExamReportModel> ExamReports { get; set; }
    }
}
