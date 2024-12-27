using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace System.YingKe.Common
{
    public class Encrypt
    {
        /// <summary>
        /// 用于加密用户密码用使用的key
        /// </summary>
        public static string AccountPwdEncryptKey { get { return "EasyFastEasyFastEasyFastEasyFast"; } }
        public static string OtherEncryptKey { get { return "LOOPLOOPLOOPLOOPLOOPLOOPLOOPLOOP"; } }

        #region AES加密解密

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="pToDencrypt"></param>
        /// <param name="skey"></param>
        /// <returns></returns>
        public static string AESDecrypt(string pToDencrypt, string skey)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(skey);
            byte[] array = System.Convert.FromBase64String(pToDencrypt);
            System.Security.Cryptography.ICryptoTransform cryptoTransform = new System.Security.Cryptography.RijndaelManaged
            {
                Key = bytes,
                Mode = System.Security.Cryptography.CipherMode.ECB,
                Padding = System.Security.Cryptography.PaddingMode.PKCS7
            }.CreateDecryptor();
            byte[] bytes2 = cryptoTransform.TransformFinalBlock(array, 0, array.Length);
            return System.Text.Encoding.UTF8.GetString(bytes2);
        }
        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="pToEncrypt"></param>
        /// <param name="skey"></param>
        /// <returns></returns>
        public static string AESEncrypt(string pToEncrypt, string skey)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(skey);
            byte[] bytes2 = System.Text.Encoding.UTF8.GetBytes(pToEncrypt);
            System.Security.Cryptography.ICryptoTransform cryptoTransform = new System.Security.Cryptography.RijndaelManaged
            {
                Key = bytes,
                Mode = System.Security.Cryptography.CipherMode.ECB,
                Padding = System.Security.Cryptography.PaddingMode.PKCS7
            }.CreateEncryptor();
            byte[] array = cryptoTransform.TransformFinalBlock(bytes2, 0, bytes2.Length);
            return System.Convert.ToBase64String(array, 0, array.Length);
        }
        #endregion
        #region DES加密解密
        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="pToDecrypt"></param>
        /// <param name="sKey"></param>
        /// <returns></returns>
        public static string DESDecrypt(string pToDecrypt, string sKey)
        {
            System.Security.Cryptography.DESCryptoServiceProvider dESCryptoServiceProvider = new System.Security.Cryptography.DESCryptoServiceProvider();
            int num = pToDecrypt.Length / 2 - 1;
            byte[] array = new byte[num + 1];
            for (int i = 0; i <= num; i++)
            {
                int value = System.Convert.ToInt32(pToDecrypt.Substring(i * 2, 2), 16);
                array[i] = System.Convert.ToByte(value);
            }
            dESCryptoServiceProvider.Key = System.Text.Encoding.ASCII.GetBytes(sKey);
            dESCryptoServiceProvider.IV = System.Text.Encoding.ASCII.GetBytes(sKey);
            System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
            System.Security.Cryptography.CryptoStream cryptoStream = new System.Security.Cryptography.CryptoStream(memoryStream, dESCryptoServiceProvider.CreateDecryptor(), System.Security.Cryptography.CryptoStreamMode.Write);
            cryptoStream.Write(array, 0, array.Length);
            cryptoStream.FlushFinalBlock();
            return System.Text.Encoding.Default.GetString(memoryStream.ToArray());
        }
        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="pToEncrypt"></param>
        /// <param name="sKey"></param>
        /// <returns></returns>
        public static string DESEncrypt(string pToEncrypt, string sKey)
        {
            System.Security.Cryptography.DESCryptoServiceProvider dESCryptoServiceProvider = new System.Security.Cryptography.DESCryptoServiceProvider();
            byte[] bytes = System.Text.Encoding.Default.GetBytes(pToEncrypt);
            dESCryptoServiceProvider.Key = System.Text.Encoding.ASCII.GetBytes(sKey);
            dESCryptoServiceProvider.IV = System.Text.Encoding.ASCII.GetBytes(sKey);
            System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
            System.Security.Cryptography.CryptoStream cryptoStream = new System.Security.Cryptography.CryptoStream(memoryStream, dESCryptoServiceProvider.CreateEncryptor(), System.Security.Cryptography.CryptoStreamMode.Write);
            cryptoStream.Write(bytes, 0, bytes.Length);
            cryptoStream.FlushFinalBlock();
            System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
            byte[] array = memoryStream.ToArray();
            for (int i = 0; i < array.Length; i++)
            {
                byte b = array[i];
                byte b2 = b;
                stringBuilder.AppendFormat("{0:X2}", b2);
            }
            return stringBuilder.ToString();
        }

        #endregion
        #region GB2312加密解密
        /// <summary>  
        /// Des 加密 GB2312
        /// </summary>  
        /// <param name="str">Desc string</param>  
        /// <param name="key">Key ,必须为8位</param>  
        /// <param name="iv">偏移值</param>
        /// <returns></returns>  
        public static string Encode(string str, string key, string iv)
        {
            if (string.IsNullOrEmpty(str)) return "";
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider
            {
                Key = Encoding.ASCII.GetBytes(key.Substring(0, 8)),
                IV = Encoding.ASCII.GetBytes(iv.Substring(0, 8))
            };
            byte[] bytes = Encoding.GetEncoding("GB2312").GetBytes(str);
            MemoryStream stream = new MemoryStream();
            try
            {
                CryptoStream stream2 = new CryptoStream(stream, provider.CreateEncryptor(), CryptoStreamMode.Write);
                stream2.Write(bytes, 0, bytes.Length);
                stream2.FlushFinalBlock();
                StringBuilder builder = new StringBuilder();
                foreach (byte num in stream.ToArray())
                {
                    builder.AppendFormat("{0:X2}", num);
                }
                stream.Close();
                return builder.ToString().ToLower();
            }
            catch (Exception)
            {
                stream.Close();
                return "";
            }

        }

        /// <summary>  
        /// Des 解密 GB2312
        /// </summary>  
        /// <param name="str">Desc string</param>  
        /// <param name="key">Key ,必须为8位 </param>
        /// <param name="iv">偏移值</param>
        /// <returns></returns>  
        public static string Decode(string str, string key, string iv)
        {
            if (string.IsNullOrEmpty(str)) return "";
            str = str.ToUpper();
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider
            {
                Key = Encoding.ASCII.GetBytes(key.Substring(0, 8)),
                IV = Encoding.ASCII.GetBytes(iv.Substring(0, 8))
            };
            byte[] buffer = new byte[str.Length / 2];
            for (int i = 0; i < (str.Length / 2); i++)
            {
                int num2 = Convert.ToInt32(str.Substring(i * 2, 2), 0x10);
                buffer[i] = (byte)num2;
            }
            MemoryStream stream = new MemoryStream();

            try
            {
                CryptoStream stream2 = new CryptoStream(stream, provider.CreateDecryptor(), CryptoStreamMode.Write);
                stream2.Write(buffer, 0, buffer.Length);
                stream2.FlushFinalBlock();
                stream.Close();
                return Encoding.GetEncoding("GB2312").GetString(stream.ToArray());
            }
            catch (Exception)
            {
                stream.Close();
                return "";
            }

        }

        /// <summary>  
        /// If don't input key , Use default key  
        /// Des 加密 GB2312 :  
        /// </summary>  
        /// <param name="str"></param>  
        /// <returns></returns>  
        public static string Encode(string str)
        {
            string defaultKey = "Zj@#yk12";
            string defaultIv = "yingke@#";
            return Encode(str, defaultKey, defaultIv);
        }
        /// <summary>  
        /// 解密  
        /// </summary>  
        /// <param name="str"></param>  
        /// <returns></returns>  
        public static string Decode(string str)
        {
            string defaultKey = "Zj@#yk12";
            string defaultIv = "yingke@#";
            return Decode(str, defaultKey, defaultIv);
        }
        #endregion

        #region Base64加密解密
        /// <summary>
        /// Base64加密
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static string Base64Code(string message)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(message);
            return Convert.ToBase64String(bytes);
        }
        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static string Base64Decode(string message)
        {
            byte[] bytes = Convert.FromBase64String(message);
            return Encoding.UTF8.GetString(bytes);
        }
        #endregion

        #region MD5加密
        public static string MD5Encode(string estr)
        {
            //MD5类是抽象类
            MD5 md5 = MD5.Create();
            //需要将字符串转成字节数组
            byte[] buffer = Encoding.Default.GetBytes(estr);
            //加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择
            byte[] md5buffer = md5.ComputeHash(buffer);
            string str = null;
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            foreach (byte b in md5buffer)
            {
                //得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
                //但是在和对方测试过程中，发现我这边的MD5加密编码，经常出现少一位或几位的问题；
                //后来分析发现是 字符串格式符的问题， X 表示大写， x 表示小写， 
                //X2和x2表示不省略首位为0的十六进制数字；
                str += b.ToString("x2");
            }
            return str;
        }
        #endregion

        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <param name="bflag">bflag : true 秒 ， false 毫秒</param>
        /// <returns></returns>
        public static string GetTimeStamp(bool bflag)
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            string ret = string.Empty;
            if (bflag)
                ret = Convert.ToInt64(ts.TotalSeconds).ToString();
            else
                ret = Convert.ToInt64(ts.TotalMilliseconds).ToString();

            return ret;
        }

        #region 解决JAVA和C#编码结果不同
        public static string UrlEncode(string str)
        {
            var encoding = UTF8Encoding.UTF8;
            byte[] bytes = encoding.GetBytes(str);
            int IsSafe = 0;
            int NoSafe = 0;
            for (int i = 0; i < bytes.Length; i++)
            {
                char ch = (char)bytes[i];
                if (ch == ' ')
                {
                    IsSafe++;
                }
                else if (!IsSafeChar(ch))
                {
                    NoSafe++;
                }
            }
            if (IsSafe == 0 && NoSafe == 0)
            {
                return str;
            }
            byte[] buffer = new byte[bytes.Length + (NoSafe * 2)];
            int num1 = 0;
            for (int j = 0; j < bytes.Length; j++)
            {
                byte num2 = bytes[j];
                char ch2 = (char)num2;
                if (IsSafeChar(ch2))
                {
                    buffer[num1++] = num2;
                }
                else if (ch2 == ' ')
                {
                    buffer[num1++] = 0x2B;
                }
                else
                {
                    buffer[num1++] = 0x25;
                    buffer[num1++] = (byte)IntToHex((num2 >> 4) & 15);
                    buffer[num1++] = (byte)IntToHex(num2 & 15);
                }
            }
            return encoding.GetString(buffer);
        }

        private static bool IsSafeChar(char ch)
        {
            if ((((ch < 'a') || (ch > 'z')) && ((ch < 'A') || (ch > 'Z'))) && ((ch < '0') || (ch > '9')))
            {

                switch (ch)
                {
                    case '-':
                    case '.':
                        break;  //安全字符
                    case '+':
                    case ',':
                        return false;  //非安全字符
                    default:   //非安全字符
                        if (ch != '_')
                        {
                            return false;
                        }
                        break;
                }
            }
            return true;
        }
        private static char IntToHex(int n)
        {
            if (n <= 9)
            {
                return (char)(n + 0x30);
            }
            return (char)((n - 10) + 0x41);
        }
        #endregion
    }
}
