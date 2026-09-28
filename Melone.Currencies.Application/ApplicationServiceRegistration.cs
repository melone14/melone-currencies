using Microsoft.Extensions.DependencyInjection;

namespace Melone.Currencies.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(ApplicationServiceRegistration).Assembly));

        return services;
    }
}
