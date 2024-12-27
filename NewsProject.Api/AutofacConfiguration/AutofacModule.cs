using Autofac;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using NewsProject.Api.Extensions;

namespace BusinessAccount.Configuration
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().SingleInstance(); 
            builder.RegisterType<CalendarHelper>().SingleInstance();

            // 自定义控制器激活器
            builder.RegisterType<CustomControllerActivator>().As<IControllerActivator>().SingleInstance();
        }
    }
}

