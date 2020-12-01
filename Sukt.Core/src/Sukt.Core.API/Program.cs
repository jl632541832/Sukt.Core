using AspectCore.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Sukt.Core.SeriLog;

namespace Sukt.Core.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Log.Logger = new LoggerConfiguration()
            //    .MinimumLevel.Information()
            //    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            //    .Enrich.FromLogContext()
            //    .WriteTo.Console()
            //    .WriteTo.File(Path.Combine("logs", @"log.txt"), rollingInterval: RollingInterval.Day)
            //    .CreateLogger();
            SeriLogLogger.SetSeriLoggerToFile("logs");
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                    .UseSerilog()//ע��Serilog��־�м��//����������log��
                    .ConfigureLogging((hostingContext, builder) =>
                    {
                        builder.ClearProviders();
                        builder.SetMinimumLevel(LogLevel.Information);
                        builder.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                        builder.AddConsole();
                        builder.AddDebug();
                    });
                })
            .UseDynamicProxy();//ʹ�ö�̬������Ҫ��Program���ô˷���
    }
}