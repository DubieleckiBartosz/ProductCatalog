using ProductCatalog.Application.Configurations;
using ProductCatalog.Application.Options;
using ProductCatalog.Infrastructure.Configurations;
using Serilog;

namespace ProductCatalog.API.Configurations;

public static class ConfigurationLayers
{
    public static WebApplicationBuilder SetOptions(this WebApplicationBuilder builder)
    {
        var configuration = builder.Configuration;

        builder.Services.Configure<ConnectionStrings>(configuration.GetSection("ConnectionStrings"));

        return builder;
    }

    public static WebApplicationBuilder SetDependencyInjection(this WebApplicationBuilder builder)
    {
        builder.Services.GetDependencyInjectionApplicationLayer();
        builder.Services.GetDependencyInjectionInfrastructureLayer();

        return builder;
    }

    public static WebApplicationBuilder SetLogging(this WebApplicationBuilder builder)
    {
        var isActive = builder.Configuration.GetValue<bool>("Serilog:IsActive");
        builder.Host.UseSerilog((ctx, lc) => lc.LogConfigurationService(isActive));

        return builder;
    }

    public static WebApplicationBuilder RegisterExternalPackages(this WebApplicationBuilder builder)
    {
        builder.Services.GetMediatR();
        builder.Services.GetMapper();

        return builder;
    }
}