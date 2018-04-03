using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.IO.Compression;

namespace Cuscapi.Utils
{
    public class CompressionHelper
    {
        public static byte[] Decompress(byte[] data)
        {
            try
            {
                MemoryStream ms = new MemoryStream(data);
                Stream zipStream = null;
                zipStream = new GZipStream(ms, CompressionMode.Decompress);
                byte[] dc_data = null;
                dc_data = EtractBytesFormStream(zipStream, data.Length);
                return dc_data;
            }
            catch
            {
                return null;
            }
        }

        public static byte[] EtractBytesFormStream(Stream zipStream, int dataBlock)
        {
            try
            {
                byte[] data = null;
                int totalBytesRead = 0;
                while (true)
                {
                    Array.Resize(ref data, totalBytesRead + dataBlock + 1);
                    int bytesRead = zipStream.Read(data, totalBytesRead, dataBlock);
                    if (bytesRead == 0)
                    {
                        break;
                    }
                    totalBytesRead += bytesRead;
                }
                Array.Resize(ref data, totalBytesRead);
                return data;
            }
            catch
            {
                return null;
            }
        }

        public static string Compress(string value)
        {
            return Compress(value, CompressType.CompressByZip);
        }

        public static string Compress(string value, CompressType type)
        {
            try
            {
                if (value.Length <= GetDeflateDefaultValue())
                {
                    return AddCompressMark(value, CompressType.Decompress);
                }
                byte[] buffer = Encoding.UTF8.GetBytes(value);
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    Stream compressStream;

                    if (type == CompressType.CompressByZip)
                    {
                        compressStream = new GZipStream(memoryStream, CompressionMode.Compress, true);
                    }
                    else
                    {
                        compressStream = new DeflateStream(memoryStream, CompressionMode.Compress, true);
                    }

                    using (compressStream)
                    {
                        compressStream.Write(buffer, 0, buffer.Length);
                    }

                    return AddCompressMark(Convert.ToBase64String(memoryStream.ToArray()), type);
                }
            }
            catch (InvalidDataException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static string Decompress(string value)
        {
            string result = string.Empty;

            string mark = value != null && value.Length > 10 ? value.Substring(0, 10) : string.Empty;

            if (!MatchValue(mark))
            {
                return value;
            }

            value = value.Remove(0, 10);

            try
            {
                byte[] bytes = Convert.FromBase64String(value);
                using (MemoryStream outStream = new MemoryStream())
                {
                    using (MemoryStream inStream = new MemoryStream(bytes))
                    {
                        // Skip first two bits for uncompress success.
                        inStream.ReadByte();
                        inStream.ReadByte();

                        Stream decompressStream;

                        if (mark.Equals("[Type = 1]"))
                        {
                            decompressStream = new DeflateStream(inStream, CompressionMode.Decompress, true);
                        }
                        else
                        {
                            decompressStream = new GZipStream(inStream, CompressionMode.Decompress, true);
                        }

                        using (decompressStream)
                        {
                            int readLength = 0;
                            byte[] buffer = new byte[1024];
                            while ((readLength = decompressStream.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                outStream.Write(buffer, 0, readLength);
                            }
                        }
                    }
                    result = Encoding.UTF8.GetString(outStream.ToArray());
                }
            }
            catch (InvalidDataException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        private static string AddCompressMark(string value, CompressType dataType)
        {
            return dataType == CompressType.Decompress ? value : value.Insert(0, string.Format("[Type = {0}]", System.Convert.ToInt32(dataType)));
        }

        private static bool MatchValue(string mark)
        {
            const string compareValue = "^[Type = {+[0-9]*]";
            Match match = Regex.Match(mark, compareValue);

            return match.Success;
        }

        /// <summary>
        /// Update by Jeffrey Li on [2013-09-10].
        /// get default compress length from app.Config.
        /// </summary>
        /// <returns>get default compress length from app.Config,if got an error,set a defalute value</returns>
        private static int GetDeflateDefaultValue()
        {
            int deflateDefaultValue = 0;
            bool isConvertFail = int.TryParse(System.Configuration.ConfigurationManager.AppSettings["DeflateDefaultValue"], out deflateDefaultValue);
            if (!isConvertFail)
            {
                deflateDefaultValue = 10000;
            }
            return deflateDefaultValue;
        }
    }

    public enum CompressType
    {
        Decompress = 0,
        CompressByDef = 1,
        CompressByZip = 2
    }
}
