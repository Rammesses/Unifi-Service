using System.Runtime.InteropServices;

using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Unifi.Stats.Service.Behaviours;
using Unifi.Stats.Service.Unifi;

namespace Unifi.Stats.Service
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {

            var hostBuilder = Host.CreateDefaultBuilder(args);

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                hostBuilder.UseWindowsService();
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                hostBuilder.UseSystemd();
            }

            hostBuilder.ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });

            hostBuilder.ConfigureServices((hostContext, services) =>
                {
                    var config = hostContext.Configuration;

                    services.AddMediatR(typeof(Program).Assembly);
                    services.AddTransient(typeof(IRequestExceptionAction<,>), typeof(ExceptionHandlingBehaviour<,>));

                    services.AddUnifi(options =>
                    {
                        config.GetSection("Unifi").Bind(options);
                    });

                    services.AddHostedService<Worker>();
                });

            return hostBuilder;
        }
    }
}
