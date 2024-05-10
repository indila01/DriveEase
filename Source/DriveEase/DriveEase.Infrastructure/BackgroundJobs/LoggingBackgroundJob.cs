using Microsoft.Extensions.Logging;
using Quartz;
namespace DriveEase.Infrastructure.BackgroundJobs;
public class LoggingBackgroundJob(ILogger<LoggingBackgroundJob> logger) : IJob
{
    public Task Execute(IJobExecutionContext context)
    {
        logger.LogWarning("{UtcNow}", DateTime.UtcNow);

        return Task.CompletedTask;
    }
}
