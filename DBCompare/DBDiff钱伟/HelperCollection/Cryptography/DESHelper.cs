using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Utility.HelperCollection.Cryptography
{
    public class DESHelper
    {
        /// <summary>
        /// Des加密
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static byte[] EncryptData(byte[] data, string key, string iv)
        {
            byte[] byteKey = Convert.FromBase64String(key);
            byte[] byteIV = Convert.FromBase64String(iv);
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            ICryptoTransform transform = des.CreateEncryptor(byteKey, byteIV);
            byte[] encryptData = transform.TransformFinalBlock(data, 0, data.Length);
            //MemoryStream mStream = new MemoryStream();
            //CryptoStream cStream = new CryptoStream(mStream, transform, CryptoStreamMode.Write);
            //cStream.Write(data, 0, data.Length);
            //cStream.Close();
            //byte[] encryptData = mStream.ToArray();
            //mStream.Close();
            return encryptData;
        }

        public static byte[] DecryptData(byte[] data, string key, string iv)
        {
            byte[] byteKey = Convert.FromBase64String(key);
            byte[] byteIV = Convert.FromBase64String(iv);
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            ICryptoTransform transform = des.CreateDecryptor(byteKey, byteIV);
            byte[] encryptData = transform.TransformFinalBlock(data, 0, data.Length);
            //MemoryStream mStream = new MemoryStream();
            //CryptoStream cStream = new CryptoStream(mStream, transform, CryptoStreamMode.Write);
            //cStream.Write(data, 0, data.Length);
            //cStream.Close();
            //byte[] encryptData = mStream.ToArray();
            //mStream.Close();
            return encryptData;
        }

        public static Dictionary<Keys,string>  GenerateKeyAndIV()
        {
            Dictionary<Keys, string> result = new Dictionary<Keys, string>();
            byte[] key = new byte[8];
            byte[] iv = new byte[8];
            Random r = new Random(255);
            r.NextBytes(key);
            r.NextBytes(iv);
            result.Add(Keys.Key, Convert.ToBase64String(key));
            result.Add(Keys.IV, Convert.ToBase64String(iv));
            return result;
        }

        public enum Keys
        {
            Key,
            IV 
        }

    }
}
