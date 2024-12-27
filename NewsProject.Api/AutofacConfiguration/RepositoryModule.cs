using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using BusinessAccount.IRepositories;
using BusinessAccount.Repositories;

namespace BusinessAccount.Configuration
{
    public class RepositoryModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            Assembly assemblysRepository = Assembly.Load("NewsProject.Infrastructure");
            builder.RegisterAssemblyTypes(assemblysRepository)
                    .Where(a => a.Name.EndsWith("Repository")) 
                    .AsImplementedInterfaces()
                    .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(AuditBaseRepository<>)).As(typeof(IAuditBaseRepository<>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(AuditBaseRepository<,>)).As(typeof(IAuditBaseRepository<,>)).InstancePerLifetimeScope();
        }
    }
}
