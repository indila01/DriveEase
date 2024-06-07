using DriveEase.Domain;
using MediatR;

namespace DriveEase.Application;

public class UserRegisteredEventHandler : INotificationHandler<UserRegisteredDomainEvent>
{
    public async Task Handle(UserRegisteredDomainEvent notification, CancellationToken cancellationToken)
    {
        Console.Write("event caputred");
    }
}
