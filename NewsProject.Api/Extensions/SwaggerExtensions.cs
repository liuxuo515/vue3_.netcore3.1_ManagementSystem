using BusinessAccount.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;
using NewsProject.Api;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace BusinessAccount.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwaggerGen(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传输) 参数结构: \"Authorization: Bearer {token}\"",
                    Name = "Authorization", //jwt默认的参数名称
                    In = ParameterLocation.Header, //jwt默认存放Authorization信息的位置(请求头中)
                    Type = SecuritySchemeType.ApiKey
                });
                // 让 Swagger 使用该安全定义
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });

                // 可选：设置 Swagger 文档的基本信息（如标题、版本等）
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Swagger API",
                    Version = "v1",
                    Description = "News ASP.NET Core Web API"
                });

                // 可选：为 API 控制器提供 XML 注释，帮助生成更好的文档
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);

                //options.AddServer(new OpenApiServer()
                //{
                //    Url = "http://localhost:26332",
                //    Description = "本地"
                //}); ;
                //options.AddServer(new OpenApiServer()
                //{
                //    Url = "https://commercialapi.yingkelawyer.com",
                //    Description = "服务端"
                //});

            });
            return services;
        }
    }
}
