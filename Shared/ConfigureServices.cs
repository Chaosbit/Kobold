using Microsoft.Extensions.DependencyInjection;

namespace Kobold.Shared;

public static class ConfigureServices
{
    public static IServiceCollection AddShared(this IServiceCollection services)
    {
        services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(typeof(ConfigureServices).Assembly); });
        return services;
    }
}