using System.Text;
using DriveEase.Domain.Abstraction;
using DriveEase.Infrastructure.Authentication;
using DriveEase.Infrastructure.BackgroundJobs;
using DriveEase.Infrastructure.Cryptography;
using DriveEase.SharedKernel.Util;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
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
    public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddUtils();
        services.AddBackgroundJobs();
        services.AddAuthServices(config);
        return services;
    }

    private static void AddAuthServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = config["Jwt:Issuer"],
                    ValidAudience = config["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:Key"]))
                };
            });
        services.AddTransient<IPasswordHasher, PasswordHasher>();
        services.AddTransient<IPasswordHashChecker, PasswordHasher>();
        services.AddScoped<IJwtProvider, JwtProvider>();
    }

    private static void AddUtils(this IServiceCollection services)
    {
        services.AddTransient<IDateTime, DateTimeProvider>();
    }

    private static void AddBackgroundJobs(this IServiceCollection services)
    {
        services.AddQuartz(o =>
      {
          var jobKey = JobKey.Create(nameof(LoggingBackgroundJob));

          o.AddJob<ProccessOutboxMessagesJob>(jobKey)
              .AddTrigger(trigger =>
                          trigger.ForJob(jobKey)
                                  .WithSimpleSchedule(schedule =>
                                      schedule.WithIntervalInSeconds(60).RepeatForever()));
      });
        services.AddQuartzHostedService(o =>
        {
            o.WaitForJobsToComplete = true;
        });
    }
}
