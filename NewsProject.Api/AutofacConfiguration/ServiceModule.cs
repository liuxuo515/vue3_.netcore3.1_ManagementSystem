using Autofac;
using Autofac.Extras.DynamicProxy;
//using BusinessAccount.FreeSql.Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
//using Guanghualu.Account;
//using Guanghualu.Entities;

namespace BusinessAccount.Configuration
{
    public class ServiceModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            Assembly servicesDllFile = Assembly.Load("NewsProject.Application");
            builder.RegisterAssemblyTypes(servicesDllFile)
                .Where(a => a.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope()
                .PropertiesAutowired()// 属性注入
                //.InterceptedBy(interceptorServiceTypes.ToArray())
                .EnableInterfaceInterceptors();
        }
    }
}
