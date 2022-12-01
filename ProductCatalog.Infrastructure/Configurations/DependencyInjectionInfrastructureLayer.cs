using Microsoft.Extensions.DependencyInjection;
using ProductCatalog.Application.Interfaces.Repositories;
using ProductCatalog.Infrastructure.Repositories;

namespace ProductCatalog.Infrastructure.Configurations;

public static class DependencyInjectionInfrastructureLayer
{
    public static IServiceCollection GetDependencyInjectionInfrastructureLayer(this IServiceCollection services)
    {
        services.AddScoped<IProductRepository, ProductRepository>();

        return services;
    }
}