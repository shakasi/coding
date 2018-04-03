using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Utility.HelperCollection.Cryptography
{
    public class AESHelper
    {
        public static byte[] EncryptData(byte[] data, string key, string iv)
        {
            byte[] byteKey = Convert.FromBase64String(key);
            byte[] byteIV = Convert.FromBase64String(iv);
            Rijndael aes = Rijndael.Create();
            ICryptoTransform transform = aes.CreateEncryptor(byteKey, byteIV);
            byte[] result1 = transform.TransformFinalBlock(data, 0, data.Length); //这句话可以替代下面很多
            return result1;
            //MemoryStream mStream = new MemoryStream();
            //CryptoStream cStream = new CryptoStream(mStream, transform, CryptoStreamMode.Write);
            //cStream.Write(data, 0, data.Length);
            //cStream.Close();
            //byte[] result = mStream.ToArray();
            //mStream.Close();
            //return result;
        }
        public static byte[] DecryptData(byte[] data, string key, string iv)
        {
            byte[] byteKey = Convert.FromBase64String(key);
            byte[] byteIV = Convert.FromBase64String(iv);
            Rijndael aes = Rijndael.Create();
            ICryptoTransform transform = aes.CreateDecryptor(byteKey, byteIV);
            byte[] result1 = transform.TransformFinalBlock(data, 0, data.Length); //这句话可以替代下面很多
            return result1;
            //MemoryStream mStream = new MemoryStream();
            //CryptoStream cStream = new CryptoStream(mStream, transform, CryptoStreamMode.Write);
            //cStream.Write(data, 0, data.Length);
            //cStream.Close();
            //byte[] result = mStream.ToArray();
            //mStream.Close();
            //return result;
        }
        public enum Keys
        {
            Key,
            IV
        }
        public static Dictionary<Keys, string> GenerateKeyAndIV()
        {
            Dictionary<Keys, string> result = new Dictionary<Keys, string>();
            byte[] key = new byte[32];
            byte[] iv = new byte[16];
            Random r = new Random(255);
            r.NextBytes(key);
            r.NextBytes(iv);
            result.Add(Keys.Key, Convert.ToBase64String(key));
            result.Add(Keys.IV, Convert.ToBase64String(iv));
            return result;
        }
    }
}
