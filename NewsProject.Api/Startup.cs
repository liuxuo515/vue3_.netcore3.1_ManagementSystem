using Autofac;
using BusinessAccount.Api.Extensions;
using BusinessAccount.Api.Models;
using BusinessAccount.Configuration;
using BusinessAccount.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NewsProject.Api.Extensions;

namespace NewsProject.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            //dto转换实体类
            services.AddAutoMapper(typeof(AutoMappperProfile).Assembly);
            //redis
            services.AddCsRedisCore(Configuration);

            #region 解决返回时间带T的问题
            services.AddControllers().AddJsonOptions(configure =>
            {
                configure.JsonSerializerOptions.Converters.Add(new DatetimeJsonConverter());

            });
            #endregion

            services.AddCors(options =>
            {
                options.AddPolicy("any", builder =>
                {
                    builder.WithMethods("GET", "POST", "HEAD", "PUT", "DELETE", "OPTIONS")
                    //.AllowCredentials()//指定处理cookie
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod(); //允许任何来源的主机访问
                });
            });

            //注入JWT配置文件
            services.Configure<JwtConfig>(Configuration.GetSection("Authentication:JwtBearer"));
            //配置jwt,函数里面也可以使用上面两行注释掉的代码的方式获取配置
            services.ConfigureJwt(Configuration);
            services.AddControllers();

            //Swagger 扩展方法配置Swagger
            services.AddSwaggerGen();
        }
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new RepositoryModule());
            builder.RegisterModule(new ServiceModule());
            builder.RegisterModule(new AutofacModule());
            builder.RegisterModule(new DependencyModule());
            builder.RegisterModule(new FreeSqlModule(Configuration));
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // 启用 Swagger 中间件
            app.UseSwagger();

            // 启用 Swagger UI 中间件，指定 Swagger JSON 的 URL
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                c.RoutePrefix = string.Empty;
            });

            // 启用静态文件中间件
            app.UseStaticFiles();

            app.UseCors("any");

            app.UseRouting();
            
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
