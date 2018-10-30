using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LiveStory
{
    class LSEncode
    {
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetMd5(string str)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string a = BitConverter.ToString(md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(str)));
            a = a.Replace("-", "");
            return a.ToLower();
        }
        #region RSA加密解密
        /// <summary>
        /// 生成RSA密钥
        /// </summary>
        /// <param name="xmlPrivateKey">私钥</param>
        /// <param name="xmlPublicKey">公钥</param>
        /// <returns></returns>
        public static void CreateRSAPublicKey(out string xmlPrivateKey, out string xmlPublicKey)
        {
            try
            {
                RSACryptoServiceProvider rSACryptoServiceProvider = new RSACryptoServiceProvider();
                xmlPrivateKey = rSACryptoServiceProvider.ToXmlString(true);//私钥
                xmlPublicKey = rSACryptoServiceProvider.ToXmlString(false);//公钥
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        /// <summary>
        /// RSA加密
        /// </summary>
        /// <param name="xmlPublicKey">加载公钥</param>
        /// <param name="encString">钥加密的字符串</param>
        /// <returns>加密之后的字符串</returns>
        public static string RSAEncString(string xmlPublicKey, string encString)
        {
            try
            {
                byte[] vs1;
                byte[] vs2;
                string result;
                RSACryptoServiceProvider cryptoServiceProvider = new RSACryptoServiceProvider();
                cryptoServiceProvider.FromXmlString(xmlPublicKey);//初始化公钥
                vs1 = (new UnicodeEncoding()).GetBytes(encString);//将要加密的字符编码塞进数组
                vs2 = cryptoServiceProvider.Encrypt(vs1, false);//开始加密
                result = Convert.ToBase64String(vs2);
                return result;
            }
            catch (Exception e)
            {

                throw;
            }
        }
        /// <summary>
        /// RSA加密数组字节
        /// </summary>
        /// <param name="xmlPublicKey">加载公钥</param>
        /// <param name="encByte">要加密的字节数组</param>
        /// <returns>加密之后的字节数组</returns>
        public static byte[] RSAEncByte(string xmlPublicKey, byte[] encByte)
        {
            try
            {
                byte[] result;
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(xmlPublicKey);//初始化公钥
                result = rsa.Encrypt(encByte, false);
                return result;
            }
            catch (Exception e)
            {

                throw;
            }
        }
        /// <summary>
        /// RSA解密字符串
        /// </summary>
        /// <param name="xmlPrivateKey">加载私钥</param>
        /// <param name="decString">要解密的字符串</param>
        /// <returns>已经解密的字符串</returns>
        public static string RSADecString(string xmlPrivateKey, string decString)
        {
            try
            {  
                byte[] vs1;
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(xmlPrivateKey);//初始化私钥
                vs1 = Convert.FromBase64String(decString);
                return (new UnicodeEncoding()).GetString(rsa.Decrypt(vs1, false));//解密，编码，返回
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static byte[] RSADecByte(string xmlPrivateKey, byte[] decByte)
        {
            try
            {
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(xmlPrivateKey);
                return rsa.Decrypt(decByte, false);
            }
            catch (Exception e)
            {

                throw;
            }
        }
        #endregion
    }
}
