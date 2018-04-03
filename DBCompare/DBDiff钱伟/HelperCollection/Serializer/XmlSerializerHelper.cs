using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace Utility.HelperCollection.Serializer
{
    public class XmlSerializerHelper
    {
        public static byte[] Serialize<T>(T data)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (MemoryStream mStream = new MemoryStream())
            {
                xmlSerializer.Serialize(mStream, data);
                byte[] result = mStream.ToArray();
                return result;
            }
        }

        public static T Deserialize<T>(byte[] data)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (MemoryStream mStream = new MemoryStream())
            {
                mStream.Write(data, 0, data.Length);
                mStream.Position = 0;
                object o= xmlSerializer.Deserialize(mStream);
                if (o.GetType() == typeof(T))
                {
                    return (T)o;
                }
                else
                {
                    throw new NotSupportedException("无法转化类型");
                }
            }
        }
    }
}
