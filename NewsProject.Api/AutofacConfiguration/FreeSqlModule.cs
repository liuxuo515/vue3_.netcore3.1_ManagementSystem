using Autofac;
using FreeSql;
using FreeSql.Internal;
using Microsoft.Extensions.Configuration;
using Serilog;
using System.Diagnostics;
using System.Threading;
using System;
using BusinessAccount.Entities;
using BusinessAccount.FreeSql;

namespace BusinessAccount.Configuration
{
    public class FreeSqlModule : Autofac.Module
    {
        private readonly IConfiguration _configuration;

        public FreeSqlModule(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            IFreeSql fsql = new FreeSqlBuilder()
                .UseConnectionDb1String(_configuration)
                .UseNameConvert(NameConvertType.None)
                .UseAutoSyncStructure(false) //是否把实体同步至数据库
                .UseNoneCommandParameter(true)
                .UseMonitorCommand(cmd =>
                {
                    Trace.WriteLine(cmd.CommandText + ";");
                })
                .Build()
                .SetDbContextOptions(opt => opt.EnableCascadeSave = false);//联级保存功能开启（默认为关闭）


            builder.RegisterInstance(fsql).SingleInstance();

            fsql.Aop.CurdAfter += (s, e) =>
            {
                Log.Debug($"ManagedThreadId:{Thread.CurrentThread.ManagedThreadId}: FullName:{e.EntityType.FullName}" +
                          $" ElapsedMilliseconds:{e.ElapsedMilliseconds}ms, {e.Sql}");


                if (e.ElapsedMilliseconds > 200)
                {
                    //记录日志
                    //发送短信给负责人
                }
            };

            builder.RegisterInstance(fsql).SingleInstance();


            builder.RegisterType(typeof(UnitOfWorkManager)).InstancePerLifetimeScope();

            fsql.GlobalFilter.Apply<IDeleteAduitEntity>("IsDeleted", a => a.IsDeleted == false);

            try
            {
                using var objPool = fsql.Ado.MasterPool.Get();
            }
            catch (Exception e)
            {
                Log.Logger.Error(e + e.StackTrace + e.Message + e.InnerException);
                return;
            }
        }
    }
}
