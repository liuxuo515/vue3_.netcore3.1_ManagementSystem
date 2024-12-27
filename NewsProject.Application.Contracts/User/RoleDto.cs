using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewsProject.Application.Contracts.User
{
    internal class RoleDto
    {
    }


    /// <summary>
    /// 登录查询返回RoleDto
    /// </summary>
    public class LoginRoleDto
    {
        public string rolename { get; set; }
        public int roleid { get; set; }
        public int userId { get; set; }
    }

    /// <summary>
    /// 角色下拉框返回Dto
    /// </summary>
    public class RoleOptsDto
    {
        public int id { get; set; }
        public string name { get; set; }
    }


    /// <summary>
    /// 角色权限返回Dto
    /// </summary>
    public class RolePermissDto
    {
        public int id { get; set; }
        public string key { get; set; }
        public string name { get; set; }
        public string permissStr { get; set; }
        public List<string> permiss { get; set; } = new List<string>();
        public bool status { get; set; }

    }

    /// <summary>
    /// 角色权限Query
    /// </summary>
    public class RolePermissQuery
    {
        public int id { get; set; }
        public List<string> permiss { get; set; }

    }


    /// <summary>
    /// 角色query
    /// </summary>
    public class RoleQuery
    {
        public int Id { get; set; }
        public string key { get; set; }
        public string name { get; set; }
        public bool status { get; set; }
        public int CreateUserId { get; set; }
        public string CreateUserName { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
