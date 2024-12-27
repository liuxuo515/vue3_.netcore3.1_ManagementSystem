using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace BusinessAccount.Security.Jwt
{
    public class HashService : IHashService
    {
        public string Create(string value, string salt)
        {
            using (DeriveBytes deriveBytes = KeyGenerator.Generate(value, salt))
            {
                return Convert.ToBase64String(deriveBytes.GetBytes(512));
            }
        }
    }
}
