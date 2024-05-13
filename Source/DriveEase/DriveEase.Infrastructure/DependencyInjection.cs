using DriveEase.Domain.Abstraction;
using DriveEase.Infrastructure.Authentication;
using DriveEase.Infrastructure.BackgroundJobs;
using DriveEase.SharedKernel.Util;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

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

        services.AddQuartz(o =>
        {
            o.UseMicrosoftDependencyInjectionJobFactory();

            var jobKey = JobKey.Create(nameof(LoggingBackgroundJob));


            o.AddJob<LoggingBackgroundJob>(jobKey)
                .AddTrigger(trigger =>
                            trigger.ForJob(jobKey)
                                    .WithSimpleSchedule(schedule =>
                                        schedule.WithIntervalInSeconds(5).RepeatForever()));
        });
        services.AddQuartzHostedService(o =>
        {
            o.WaitForJobsToComplete = true;
        });

        services.AddTransient<IPasswordHasher, PasswordHasher>();
        services.AddTransient<IPasswordHashChecker, PasswordHasher>();


        return services;
    }
}
