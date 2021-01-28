using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Text;

namespace VNext.Tools.Http
{
    /// <summary>
    /// Json的HttpContent
    /// </summary>
    public class JsonContent : StringContent
    {
        /// <summary>
        /// 初始化一个<see cref="JsonContent"/>类型的新实例
        /// </summary>
        public JsonContent(object obj)
            : base(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json")
        { }

        public JsonContent(object obj, string mediaType)
            : base(JsonConvert.SerializeObject(obj), Encoding.UTF8, mediaType)
        {
        }
    }

    /// <summary>
    /// File的HttpContent
    /// </summary>
    public class FileContent : MultipartFormDataContent
    {
        public FileContent(string filePath, string apiParamName)
        {
            var filestream = File.Open(filePath, FileMode.Open);
            var filename = Path.GetFileName(filePath);

            Add(new StreamContent(filestream), apiParamName, filename);
        }
    }

    /// <summary>
    /// Patch的HttpContent
    /// </summary>
    public class PatchContent : StringContent
    {
        public PatchContent(object value)
            : base(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json-patch+json")
        {
        }
    }

    /// <summary>
    /// Xml的HttpContent
    /// </summary>
    public class XmlContent : ByteArrayContent
    {
        public XmlContent(string value)
            : base(System.Text.Encoding.UTF8.GetBytes(value))
        {
        }
    }
}
