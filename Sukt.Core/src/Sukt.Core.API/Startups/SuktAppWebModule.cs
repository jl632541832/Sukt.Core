﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Sukt.Core.Aop;
using Sukt.Core.AspNetCore.Filters;
using Sukt.Core.AutoMapper;
using Sukt.Core.Domain.Models;
using Sukt.Core.Redis;
using Sukt.Core.Shared.AppOption;
using Sukt.Core.Shared.Events;
using Sukt.Core.Shared.Extensions;
using Sukt.Core.Shared.Modules;
using Sukt.Core.Shared.SuktDependencyAppModule;
using Sukt.Core.Swagger;
using System;
using System.Linq;

namespace Sukt.Core.API.Startups
{
    [SuktDependsOn(
        typeof(AopModule),
        typeof(SuktAutoMapperModuleBase),
        typeof(CSRedisModuleBase),
        typeof(IdentityServerAuthModule),//如果是用户及角色等通用功能使用IdentityModule   作为微服务架构则使用IdentityServerAuthModule
                                         //typeof(ConsulModuleBase),
                                         //typeof(IdentityModule), 
        typeof(SwaggerModule),
        typeof(DependencyAppModule),
        typeof(EventBusAppModuleBase),
        typeof(EntityFrameworkCoreMySqlModule),
        typeof(MongoDBModule),
        typeof(MultiTenancyModule),
        typeof(MigrationModuleBase)
        )]
    public class SuktAppWebModule : SuktAppModule
    {
        private string _corePolicyName = string.Empty;

        public override void ConfigureServices(ConfigureServicesContext context)
        {
            var service = context.Services;
            service.AddControllers(x =>
            {
                x.SuppressAsyncSuffixInActionNames = false;
                x.Filters.Add<PermissionAuthorizationFilter>();
                x.Filters.Add<AuditLogFilter>();
            }).AddNewtonsoftJson(options =>
            {
                //options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            });
            var configuration = service.GetConfiguration();
            var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath; //获取项目路径
            context.Services.AddSingleton<IFileProvider>(new PhysicalFileProvider(basePath));
            service.Configure<AppOptionSettings>(configuration.GetSection("SuktCore"));
            var settings = service.GetAppSettings();
            if (!settings.Cors.PolicyName.IsNullOrEmpty() && !settings.Cors.Url.IsNullOrEmpty()) //添加跨域
            {
                _corePolicyName = settings.Cors.PolicyName;
                service.AddCors(c =>
                {
                    c.AddPolicy(settings.Cors.PolicyName, policy =>
                    {
                        policy.WithOrigins(settings.Cors.Url
                          .Split(",", StringSplitOptions.RemoveEmptyEntries).ToArray())
                        //policy.WithOrigins("http://localhost:5001")//支持多个域名端口，注意端口号后不要带/斜杆：比如localhost:8000/，是错的
                        .AllowAnyHeader().AllowAnyMethod().AllowCredentials();//允许cookie;
                    });
                });
            }
        }

        public override void ApplicationInitialization(ApplicationContext context)
        {
            var applicationBuilder = context.GetApplicationBuilder();
            applicationBuilder.UseRouting();
            if (!_corePolicyName.IsNullOrEmpty())
            {
                applicationBuilder.UseCors(_corePolicyName); //添加跨域中间件
            }
            applicationBuilder.UseAuthentication();//授权
            applicationBuilder.UseAuthorization();//认证
            applicationBuilder.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
