using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace BusinessAccount.Api.Extensions
{
    public static class AuthConfigure
    {
        public static void ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
        {
            if (bool.Parse(configuration["Authentication:JwtBearer:IsEnabled"]))
            {
                services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false; // 设置为 true 强制使用 HTTPS
                    options.SaveToken = true;
                    // 配置 Token 验证参数
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // 验证发行者
                        ValidateIssuer = true,
                        // 验证受众
                        ValidateAudience = true,
                        // 验证令牌有效期
                        ValidateLifetime = true,
                        // 验证签名密钥
                        ValidateIssuerSigningKey = true,
                        // 发行者
                        ValidIssuer = configuration["Authentication:JwtBearer:Issuer"],
                        // 受众
                        ValidAudience = configuration["Authentication:JwtBearer:Audience"],
                        // 签名密钥
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Authentication:JwtBearer:SecurityKey"]))
                    };
                });

            }
        }
    }
}
