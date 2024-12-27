using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace BusinessAccount.Security.Jwt
{
    public class KeyGenerator
    {
        public static DeriveBytes Generate(string password, string salt)
        {
            return new Rfc2898DeriveBytes(password, Encoding.Default.GetBytes(salt), 10000, HashAlgorithmName.SHA512);
        }
    }
}
