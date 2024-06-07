using DriveEase.SharedKernel;

namespace DriveEase.Domain;

/// <summary>
/// user registered domain event
/// </summary>
public sealed record UserRegisteredDomainEvent(Guid userId) : IDomainEvent
{

}
