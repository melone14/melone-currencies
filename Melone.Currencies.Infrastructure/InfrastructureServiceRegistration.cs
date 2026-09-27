using Melone.Currencies.Application.Common.Interfaces;
using Melone.Currencies.Infrastructure.BackgroundServices;
using Melone.Currencies.Infrastructure.Services.Holidays;
using Melone.Currencies.Infrastructure.Services.NBP;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Melone.Currencies.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<NbpApiOptions>(configuration.GetSection(NbpApiOptions.SectionName));

        services.AddHttpClient<INbpApiClient, NbpApiClient>((sp, client) =>
        {
            var options = sp.GetRequiredService<IOptions<NbpApiOptions>>().Value;

            if (!string.IsNullOrEmpty(options.BaseAddress))
            {
                client.BaseAddress = new Uri(options.BaseAddress);
            }
        });

        services.Configure<NbpSyncOptions>(configuration.GetSection(NbpSyncOptions.SectionName));
        services.AddSingleton<IPolishHolidayCalendar, PolishHolidayCalendar>();
        services.AddHostedService<NbpSyncBackgroundService>();

        return services;
    }
}