using DriveEase.Domain.Entities;

namespace DriveEase.Domain.Repositories;

public interface IOutboxMessageRepository
{
    Task<List<OutboxMessage>> GetOutboxMessages();
}
