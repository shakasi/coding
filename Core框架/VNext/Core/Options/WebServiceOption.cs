using System;
using System.Collections.Generic;
using System.Text;

namespace VNext.Options
{
    public class WebServiceOption
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public string MethodName { get; set; }
        public string SoapAction { get; set; }
        /// <summary>
        /// application/soap+xml; charset=utf-8 or text/xml; charset=utf-8
        /// </summary>
        public string ContentType { get; set; }
    }
}
