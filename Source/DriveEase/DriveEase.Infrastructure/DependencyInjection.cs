using DriveEase.SharedKernel.Util;
using Microsoft.Extensions.DependencyInjection;

namespace DriveEase.Infrastructure;

/// <summary>
/// DI
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds the infrastructure.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <returns></returns>
    public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services)
    {
        services.AddTransient<IDateTime, DateTimeProvider>();
        return services;
    }
}
