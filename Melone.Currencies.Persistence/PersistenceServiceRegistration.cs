using Melone.Currencies.Application.Contracts.Persistence;
using Melone.Currencies.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Melone.Currencies.Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistanceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<MeloneDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("MeloneConnectionString"))
        );

        services.AddScoped<ICurrencyRateRepository, CurrencyRateRepository>();

        return services;
    }
}
