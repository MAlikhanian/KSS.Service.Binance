using KSS.Service.Application.Interfaces.Services;
using KSS.Service.Infrastructure.ExternalServices;
using Microsoft.Extensions.DependencyInjection;

namespace KSS.Service.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IFuturesOrderService, FuturesOrderService>();
        
        return services;
    }
}
