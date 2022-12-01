using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ProductCatalog.Application.Behaviours;
using ProductCatalog.Application.Logging;

namespace ProductCatalog.Application.Configurations;

public static class DependencyInjectionApplicationLayer
{
    public static IServiceCollection GetDependencyInjectionApplicationLayer(this IServiceCollection services)
    {
        services.AddSingleton(typeof(ILoggerManager<>), typeof(LoggerManager<>));

        //PIPELINES
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehaviour<,>));

        return services;
    }

    public static IServiceCollection GetMediatR(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());

        return services;
    }

    public static IServiceCollection GetMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}