using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace BusinessAccount.Security.Jwt
{
    public class CryptographyService : ICryptographyService
    {
        private readonly string _key;

        public CryptographyService(string key)
        {
            _key = key;
        }

        public string Decrypt(string value, string salt)
        {
            using (SymmetricAlgorithm symmetricAlgorithm = Algorithm(salt))
            {
                return Encoding.Default.GetString(Transform(Convert.FromBase64String(value), symmetricAlgorithm.CreateDecryptor()));
            }
        }

        public string Encrypt(string value, string salt)
        {
            using (SymmetricAlgorithm symmetricAlgorithm = Algorithm(salt))
            {
                return Convert.ToBase64String(Transform(Encoding.Default.GetBytes(value), symmetricAlgorithm.CreateEncryptor()));
            }
        }

        private static byte[] Transform(byte[] bytes, ICryptoTransform cryptoTransform)
        {
            using (cryptoTransform)
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(bytes, 0, bytes.Length);
                        cryptoStream.Close();
                        return memoryStream.ToArray();
                    }
                }
            }
        }

        private SymmetricAlgorithm Algorithm(string salt)
        {
            using (DeriveBytes deriveBytes = KeyGenerator.Generate(_key, salt))
            {
                RijndaelManaged rijndaelManaged = new RijndaelManaged();
                rijndaelManaged.Key = deriveBytes.GetBytes(rijndaelManaged.KeySize / 8);
                rijndaelManaged.IV = deriveBytes.GetBytes(rijndaelManaged.BlockSize / 8);
                return rijndaelManaged;
            }
        }
    }
}
