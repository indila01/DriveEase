using DriveEase.Domain;
using MediatR;

namespace DriveEase.Application;

/// <summary>
/// user register event handler
/// </summary>
public class UserRegisteredEventHandler : INotificationHandler<UserRegisteredDomainEvent>
{
    /// <inheritdoc/>>
    public Task Handle(UserRegisteredDomainEvent notification, CancellationToken cancellationToken)
    {
        Console.Write("event caputred");
        return Task.CompletedTask;
    }
}
