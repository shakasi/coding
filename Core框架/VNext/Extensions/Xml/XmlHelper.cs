using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace VNext.Extensions
{
    public static class XmlHelper
    {
        public static T To<T>(this string xmlString) where T : class
        {
            T model = null;
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true;
            var serializer = new XmlSerializer(typeof(T));
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(xmlString)))
            {
                XmlReader reader = XmlReader.Create(stream, settings);
                model = (T)serializer.Deserialize(reader);
                stream.Close();
            }
            return model;
        }
    }
}
