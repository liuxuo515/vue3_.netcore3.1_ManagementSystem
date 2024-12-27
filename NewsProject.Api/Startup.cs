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
            //dtoת��ʵ����
            services.AddAutoMapper(typeof(AutoMappperProfile).Assembly);
            //redis
            services.AddCsRedisCore(Configuration);

            #region �������ʱ���T������
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
                    //.AllowCredentials()//ָ������cookie
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod(); //�����κ���Դ����������
                });
            });

            //ע��JWT�����ļ�
            services.Configure<JwtConfig>(Configuration.GetSection("Authentication:JwtBearer"));
            //����jwt,��������Ҳ����ʹ����������ע�͵��Ĵ���ķ�ʽ��ȡ����
            services.ConfigureJwt(Configuration);
            services.AddControllers();

            //Swagger ��չ��������Swagger
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
            // ���� Swagger �м��
            app.UseSwagger();

            // ���� Swagger UI �м����ָ�� Swagger JSON �� URL
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                c.RoutePrefix = string.Empty;
            });

            // ���þ�̬�ļ��м��
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
