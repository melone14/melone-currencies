using Microsoft.Extensions.DependencyInjection;
using Melone.Currencies.Application.Common.Interfaces;
using Melone.Currencies.Infrastructure.Services.NBP;

namespace Melone.Currencies.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddHttpClient<INbpApiClient, NbpApiClient>();

        return services;
    }
}