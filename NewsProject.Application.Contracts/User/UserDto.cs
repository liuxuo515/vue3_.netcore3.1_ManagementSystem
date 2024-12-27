using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewsProject.Application.Contracts.User
{
    public class UserDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public string account { get; set; }
        public string password { get; set; }
        public string phone { get; set; }
        public string role { get; set; }
        public int roleId { get; set; }
        public string email { get; set; }
        public DateTime? date { get; set; }
    }


    /// <summary>
    /// 登录Query
    /// </summary>
    public class UserLoginDto
    {
        public string username { get; set; }
        public string password { get; set; }
    }

    /// <summary>
    /// 登录返回Dto
    /// </summary>
    public class LoginResultDto
    {
        public string name { get; set; }
        public string account { get; set; }
        public string username { get; set; }
        public string token { get; set; }
    }


    /// <summary>
    /// 用户新增Query
    /// </summary>
    public class SaveUserQuery
    {
        public int id { get; set; }
        public string name { get; set; }
        public string account { get; set; }
        public string password { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public int CreateUserId { get; set; }
        public string CreateUserName { get; set; }
        public DateTime? CreateDate { get; set; }

        public int roleId { get; set; }
    }
}
