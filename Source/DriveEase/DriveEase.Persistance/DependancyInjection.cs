using DriveEase.Domain.Repositories;
using DriveEase.Persistance.Repositories;
using DriveEase.SharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DriveEase.Persistance;

/// <summary>
/// DI.
/// </summary>
public static class DependancyInjection
{
    /// <summary>
    /// Registers the persistence services.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns>extension method.</returns>
    public static IServiceCollection RegisterPersistenceServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(Connectionstring.DriveEaseDbConnectionKey);
        services.AddDbContext<DriveEaseDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        // register repositories
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<ICarRepository, CarRepository>();

        return services;
    }
}