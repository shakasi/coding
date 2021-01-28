using System;
using System.Collections.Generic;
using System.Text;
using VNext.Entity;
using VNext.Options;

namespace VNext.WebService
{
    public interface IWebService
    {
        string PostService(WebServiceOption type, string xml);
    }
}
