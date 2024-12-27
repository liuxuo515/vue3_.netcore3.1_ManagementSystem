using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessAccount.Api.Models
{
    /// <summary>
    /// 登录实体
    /// </summary>
    public class LoginEntity
    {
        /// <summary>
        /// 登录名：admin
        /// </summary>
        public string username { get; set; }
        /// <summary>
        /// 密码：123456
        /// </summary>
        public string password { get; set; }
        /// <summary>
        /// 验证码 获取验证码接口返回的加密串
        /// </summary>
        public string CaptchaToken { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        public string CaptchaValue { get; set; }
        /// <summary>
        /// 角色id 1管理员 2客户
        /// </summary>
        public string RoleId { get; set; }
    }
    /// <summary>
    /// 登录返回实体
    /// </summary>
    public class JwtDto
    {
        public int cdoe { get; set; }
        public string message { get; set; }
        public string token { get; set; }
    }
   
}
