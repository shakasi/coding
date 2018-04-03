using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography.X509Certificates;

namespace Shaka.Utils
{
    public class CommonHelper
    {
        public static X509Certificate[] GetX509CertificateArray(params string[] pathArray)
        {
            X509Certificate[] x509CertificateArray = new X509Certificate[pathArray.Length];
            try
            {
                for (int i = 0; i < pathArray.Length; i++)
                {
                    x509CertificateArray[i] = new X509Certificate(pathArray[i],"changeit");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return x509CertificateArray;
        }
    }
}
