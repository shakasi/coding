using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Utility.HelperCollection.Cryptography
{
    public class RSAHelper
    {
        /// <summary>
        /// 生成密钥
        /// </summary>
        public static Dictionary<Keys,string> GenerateKeys()
        {
            Dictionary<Keys, string> keys = new Dictionary<Keys, string>();
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(1024);
            string publicKey = rsa.ToXmlString(false);
            string privateKeys = rsa.ToXmlString(true);
            keys.Add(Keys.PrivateKey, privateKeys);
            keys.Add(Keys.PublicKey, publicKey);
            return keys;
        }
        /// <summary>
        /// 加密数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="publicKey"></param>
        /// <returns></returns>
        public static byte[] EncryptData(byte[] data, string publicKey)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(1024);
            rsa.FromXmlString(publicKey);
            return rsa.Encrypt(data, false);
        }

        /// <summary>
        /// 加密数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="publicKey"></param>
        /// <returns></returns>
        public static string EncryptData(string data, string publicKey)
        {
            byte[] byteData= Encoding.Default.GetBytes(data);
            byte[] newByteData = EncryptData(byteData, publicKey);
            return Convert.ToBase64String(newByteData);
        }
        /// <summary>
        /// 解密数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="PrivateKey"></param>
        /// <returns></returns>
        public static byte[] DecryptData(byte[] data, string PrivateKey)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(1024);
            rsa.FromXmlString(PrivateKey);
            return  rsa.Decrypt(data, false);
        }
        /// <summary>
        /// 解密数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="PrivateKey"></param>
        /// <returns></returns>
        public static string DecryptData(string data, string PrivateKey)
        {
            byte[] byteData = Convert.FromBase64String(data);
            byte[] newByteData = DecryptData(byteData, PrivateKey);
            return Encoding.Default.GetString(newByteData);
        }

        public enum Keys
        {
            PrivateKey,
            PublicKey
        }
       
    }
}
