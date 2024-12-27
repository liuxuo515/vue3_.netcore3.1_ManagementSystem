using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessAccount.Security.Jwt
{
    public interface ICryptographyService
    {
        string Decrypt(string value, string salt);

        string Encrypt(string value, string salt);
    }


}
