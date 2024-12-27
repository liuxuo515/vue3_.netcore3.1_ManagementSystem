using BusinessAccount.Api.Models;
using BusinessAccount.Core.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using NewsProject.Application.Contracts.Login;
using NewsProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace BusinessAccount.Extensions
{
    public static class JwtExtensions
    {
        /// <summary>
        /// 获取jwt中的登录值
        /// </summary>
        /// <param name="request"></param>
        /// <param name="ClaimsName"></param>
        /// <returns></returns>
        public static claimsConfig GetJwtUserInfo(HttpRequest request)
        {
            try
            {
                //下上文
                var Identity = (ClaimsIdentity)request.HttpContext.User.Identity;

                // 获取对象的所有属性
                claimsConfig claims = new claimsConfig();
                Type type = claims.GetType(); // 获取对象的类型信息
                PropertyInfo[] properties = type.GetProperties(); // 获取所有公共属性

                foreach (var property in properties)
                {
                    //用对象中的属性名称获取Claims中的值
                    var valueClaims = Identity.Claims.Where(o => o.Type == property.Name.ToString()).FirstOrDefault() ?? null;
                    if (valueClaims != null)
                    {
                        property.SetValue(claims, valueClaims.Value.ToString());
                    }
                    else
                    {
                        property.SetValue(claims, null);
                    }
                }

                return claims;
            }
            catch (Exception ex)
            {
                return new claimsConfig();
            }

        }

        /// <summary>
        /// 返回token
        /// </summary>
        /// <returns></returns>
        public static string GenerateTokens(JwtConfig jwtConfig, Claim[] claims)
        {
            try
            {
                // 获取签名密钥
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.SecurityKey));
                // 创建签名凭据
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                DateTime now = DateTime.UtcNow;
                // 创建 JWT 令牌
                var token = new JwtSecurityToken(
                    jwtConfig.Issuer,
                    jwtConfig.Audience,
                    claims,
                    expires: now.Add(TimeSpan.FromMinutes(jwtConfig.Expiration)),
                    signingCredentials: credentials);  // 使用签名凭据对令牌签名

                // 返回令牌字符串
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        /// <summary>
        /// 返回claims
        /// </summary>
        /// <returns></returns>
        public static Claim[] Generateclaims(LoginDto entity)
        {
            try
            {
                // 获取对象的所有属性
                claimsConfig claims = new claimsConfig()
                {
                    userid = entity.Id.ToString(),
                    username = entity.account??"",
                    userphone = entity.phone??"",
                    useremail = entity.email??"",
                };
                Type type = claims.GetType(); // 获取对象的类型信息
                PropertyInfo[] properties = type.GetProperties(); // 获取所有公共属性

                Claim[] claimsList = new Claim[properties.Length];
                int i = 0;
                foreach (var property in properties)
                {
                    claimsList[i] = new Claim(property.Name.ToString(), property.GetValue(claims).ToString());
                    i++;
                }

                return claimsList;
            }
            catch (Exception ex)
            {
                return new Claim[] { };
            }

        }
    }
}
