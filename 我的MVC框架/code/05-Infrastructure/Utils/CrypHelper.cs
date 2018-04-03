using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Shaka.Utils
{
    public class CrypHelper
    {
        /// <summary>
        /// MD5加密字符串
        /// </summary>
        public static string EncryptMD5(string pToEncrypt)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.Default.GetBytes(pToEncrypt);
            byte[] md5data = md5.ComputeHash(data);
            md5.Clear();

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < md5data.Length - 1; i++)
            {
                builder.Append(md5data[i].ToString("X2"));
            }
            return builder.ToString();
        }

        #region 可逆
        /// <summary>
        /// DES的秘钥
        /// </summary>
        public static string DESKey
        {
            /// 生成方法可以是：
            /// DESCryptoServiceProvider desCrypto = (DESCryptoServiceProvider)DESCryptoServiceProvider.Create();
            /// string _desKey= ASCIIEncoding.ASCII.GetString(desCrypto.Key);
            get { return "?t??h? "; }
        }

        /// <param name="pToEncrypt">要加密的字符串。</param>
        /// <param name="sKey">密钥，且必须为8位。</param>
        /// <returns>以Base64格式返回的加密字符串。</returns>
        public static string EncryptDES(string pToEncrypt, string sKey)
        {
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                byte[] inputByteArray = Encoding.UTF8.GetBytes(pToEncrypt);
                des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
                des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    cs.Close();
                }
                string str = Convert.ToBase64String(ms.ToArray());
                ms.Close();
                return str;
            }
        }

        /// <param name="pToDecrypt">要解密的以Base64</param>
        /// <param name="sKey">密钥，且必须为8位。</param>
        /// <returns>已解密的字符串。</returns>
        public static string DecryptDES(string pToDecrypt, string sKey)
        {
            byte[] inputByteArray = Convert.FromBase64String(pToDecrypt);
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
                des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    cs.Close();
                }
                string str = Encoding.UTF8.GetString(ms.ToArray());
                ms.Close();
                return str;
            }
        }
        #endregion
    }
}
