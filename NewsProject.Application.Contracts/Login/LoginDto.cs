using System;
using System.Collections.Generic;
using System.Text;

namespace NewsProject.Application.Contracts.Login
{
    public class LoginDto
    {
        public int Id { get; set; }
        public string account { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string roleaccount { get; set; }
    }
}
