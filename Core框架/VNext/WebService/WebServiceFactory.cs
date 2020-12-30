using System;
using System.Collections.Generic;
using System.Text;
using VNext.Entity;

namespace VNext.WebService
{
    public class WebServiceFactory
    {
        private string _soapXml1_1 = Enum.GetName(typeof(ContentType), ContentType.SoapXml1_1);
        private string _soapXml1_2 = Enum.GetName(typeof(ContentType), ContentType.SoapXml1_2);
        private string _textXml = Enum.GetName(typeof(ContentType), ContentType.TextXml);
        public IWebService CreateWebService(string type)
        {
            IWebService iWebService = null;
            if (type.Equals(_soapXml1_1))
                iWebService= new SoapXml1_1();
            else if (type.Equals(_soapXml1_2))
            {
                iWebService= new SoapXml1_2();
            }
            else if (type.Equals(_textXml))
            {
                iWebService= new TextXml();
            }
            return iWebService;
        }
    }
}
