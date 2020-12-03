﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sukt.Core.MultiTenancy;
using Sukt.Core.Shared;
using Sukt.Core.Shared.Entity;
using Sukt.Core.Shared.Events;
using Sukt.Core.Shared.Extensions;
using Sukt.Core.Shared.Modules;

namespace Sukt.Core.API.Startups
{
    [SuktDependsOn(
        typeof(EventBusAppModuleBase)
        )]
    public class EntityFrameworkCoreMySqlModule : EntityFrameworkCoreModuleBase
    {
        /// <summary>
        /// 添加仓储
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        protected override IServiceCollection AddRepository(IServiceCollection services)
        {
            services.AddScoped(typeof(IEFCoreRepository<,>), typeof(BaseRepository<,>));
            return services;
        }

        /// <summary>
        /// 添加工作单元
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        protected override IServiceCollection AddUnitOfWork(IServiceCollection services)
        {
            return services.AddScoped<IUnitOfWork, UnitOfWork<DefaultDbContext>>();
        }

        /// <summary>
        /// 重写方法
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        protected override IServiceCollection UseSql(IServiceCollection services)
        {
            //var Dbpath = services.GetConfiguration()["SuktCore:DbContext:MysqlConnectionString"];
            //var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath; //获取项目路径
            //var dbcontext = Path.Combine(basePath, Dbpath);
            //var Assembly = typeof(EntityFrameworkCoreMySqlModule).GetTypeInfo().Assembly.GetName().Name;//获取程序集
            //if (!File.Exists(dbcontext))
            //{
            //    throw new Exception("未找到存放数据库链接的文件");
            //}
            var mysqlconn = services.GetFileByConfiguration("SuktCore:DbContext:MysqlConnectionString", "未找到存放MySql数据库链接的文件");
            services.AddDbContext<DefaultDbContext>((serviceProvider, options) =>
            {
                var resolver = serviceProvider.GetRequiredService<ISuktConnectionStringResolver>();
                var ss = resolver.Resolve();
                options.UseMySql(mysqlconn, assembly => { assembly.MigrationsAssembly("Sukt.Core.Domain.Models"); });
            });
            return services;
        }
    }
}
