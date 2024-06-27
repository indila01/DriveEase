using DriveEase.Domain.Entities;
using DriveEase.Domain.Repositories;
using DriveEase.SharedKernel;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Quartz;

namespace DriveEase.Infrastructure;

[DisallowConcurrentExecution]
public class ProccessOutboxMessagesJob(
    IOutboxMessageRepository OutboxMessageRepository,
    IUnitOfWork unitOfWork,
    IPublisher Publisher,
    ILogger<ProccessOutboxMessagesJob> Logger) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        Logger.LogInformation("Started proccessing outbox messages");

        var outboxMessages = await OutboxMessageRepository.GetOutboxMessages();
        if (!outboxMessages.Any())
        {
            Logger.LogInformation("No Messages to process - Completed proccessing outbox messages");
            return;
        }

        foreach (var outboxMessage in outboxMessages)
        {
            Exception? exception = null;
            try
            {
                IDomainEvent? domainEvent = JsonConvert
                    .DeserializeObject<IDomainEvent>(outboxMessage.Content, new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All
                    });

                if (domainEvent is null)
                {
                    Logger.LogError("Domain event for outbox message ID {MessageId} is empty", outboxMessage.Id);
                    continue;
                }

                await Publisher.Publish(domainEvent, context.CancellationToken);
                Logger.LogInformation("Completed proccessing outbox messages");

            }
            catch (Exception caughtException)
            {
                Logger.LogError("Error while proccessing the outbox message {MessageId}", outboxMessage.Id);
                exception = caughtException;
            }

            await updateOutboxMessage(outboxMessage, exception);
        }
    }

    /// <summary>
    /// Update outbox message as proccesses
    /// </summary>
    /// <param name="outboxMessage"></param>
    /// <param name="exception"></param>
    /// <returns></returns>
    private async Task updateOutboxMessage(OutboxMessage outboxMessage, Exception? exception)
    {
        outboxMessage.ProccessedDate = DateTime.UtcNow;
        outboxMessage.Error = exception?.ToString();
        await unitOfWork.SaveChangesAsync();
    }
}
