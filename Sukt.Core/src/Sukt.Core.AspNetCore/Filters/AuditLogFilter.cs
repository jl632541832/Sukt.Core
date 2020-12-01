﻿using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Sukt.Core.Shared.Audit;
using Sukt.Core.Shared.Extensions;
using Sukt.Core.Shared.SuktDependencyAppModule;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Sukt.Core.AspNetCore.Filters
{
    /// <summary>
    /// AuditLogFilter执行完成过滤器用来记录审计日志
    /// </summary>
    public class AuditLogFilter : IActionFilter, IResultFilter
    {
        private Stopwatch _stopwatch = new Stopwatch();

        /// <summary>
        /// 执行行动时
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        /// <summary>
        /// 方法执行中
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            _stopwatch.Start();
        }

        /// <summary>
        /// 方法返回完成后
        /// </summary>
        /// <param name="context"></param>
        public void OnResultExecuted(ResultExecutedContext context)
        {
            _stopwatch.Stop();
            var action = context.ActionDescriptor as ControllerActionDescriptor;
            var actionname = action.MethodInfo.ToDescription();//获取控制器特性
            IServiceProvider provider = context.HttpContext.RequestServices;
            var dic = provider.GetService<DictionaryAccessor>();
            dic.TryGetValue("audit", out object auditEntry);
            if (action.EndpointMetadata.Any(x => x is AuditLogAttribute) && auditEntry != null)
            {
                AuditLog auditLog = new AuditLog
                {
                    BrowserInformation = context.HttpContext.Request.Headers["User-Agent"].ToString(),
                    ExecutionDuration = _stopwatch.ElapsedMilliseconds,
                    Ip = context.HttpContext.GetClientIP(),
                    FunctionName = $"{context.Controller.GetType().ToDescription()}-{action.MethodInfo.ToDescription()}",
                    Action = context.HttpContext.Request.Path
                };
                provider.GetService<IAuditStore>()?.SaveAudit(auditLog, (auditEntry as List<AuditEntryInputDto>)).GetAwaiter().GetResult(); //不用异步，或则用异步IResultFilterAsync
            }
        }

        /// <summary>
        /// 方法返回中
        /// </summary>
        /// <param name="context"></param>
        public void OnResultExecuting(ResultExecutingContext context)
        {
        }
    }
}