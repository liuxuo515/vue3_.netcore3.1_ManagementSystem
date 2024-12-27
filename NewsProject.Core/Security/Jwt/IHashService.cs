using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessAccount.Security.Jwt
{
    public interface IHashService
    {
        string Create(string value, string salt);
    }
}
