using DriveEase.Domain.Abstraction;
using DriveEase.Domain.Repositories;
using DriveEase.Persistance.EFCustomizations;
using DriveEase.Persistance.Interceptors;
using DriveEase.SharedKernel;
using DriveEase.SharedKernel.Util;
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
        Ensure.NotEmpty(connectionString, "DbConnection string is empty", nameof(connectionString));

        services.AddSingleton<UpdateAuditableInterceptor>();
        services.AddSingleton<SoftDeleteInterceptor>();
        services.AddSingleton<PublishDomainEventsInterceptor>();

        services.AddDbContext<DriveEaseDbContext>(
            (sp, options) => options
            .UseSqlServer(connectionString)
            .AddInterceptors(
                sp.GetRequiredService<UpdateAuditableInterceptor>(),
                sp.GetRequiredService<SoftDeleteInterceptor>(),
                sp.GetRequiredService<PublishDomainEventsInterceptor>()));

        // register repositories
        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<DriveEaseDbContext>());
        services.AddScoped<ICarRepository, CarRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}