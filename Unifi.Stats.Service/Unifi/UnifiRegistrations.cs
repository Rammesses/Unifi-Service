using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using UniFiSharp;

namespace Unifi.Stats.Service.Unifi
{
    public static class UnifiRegistrations
    {
        public static IServiceCollection AddUnifi(this IServiceCollection services, Action<UnifiClientOptions> configure)
        {
            services.AddTransient<IUnifiClient, UnifiClient>();
            services.AddOptions<UnifiClientOptions>();
            services.Configure<UnifiClientOptions>(configure);

            services.AddTransient<IUniFiRestClient, UniFiRestClient>(p =>
            {
                var options = p.GetRequiredService<IOptions<UnifiClientOptions>>().Value;
                return new UniFiRestClient(options.ControllerUri, options.Username, options.Password, options.IgnoreSslValidation);
            });

            services.AddTransient<UniFiApi>(p =>
            {
                var options = p.GetRequiredService<IOptions<UnifiClientOptions>>().Value;
                var restClient = p.GetRequiredService<IUniFiRestClient>();
                return new UniFiApi(restClient, options.SiteName);
            });

            return services;
        }
    }
}
