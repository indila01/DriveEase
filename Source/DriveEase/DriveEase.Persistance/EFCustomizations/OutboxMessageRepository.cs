using DriveEase.Domain.Entities;
using DriveEase.Domain.Repositories;
using DriveEase.Persistance.EFCustomizations;
using Microsoft.EntityFrameworkCore;

namespace DriveEase.Persistance;

public class OutboxMessageRepository(DriveEaseDbContext dbContext) : IOutboxMessageRepository
{
    public async Task<List<OutboxMessage>> GetOutboxMessages()
    {
        return await dbContext
            .Set<OutboxMessage>()
            .Where(x => x.ProccessedDate == null)
            .Take(20)
            .ToListAsync();
    }
}
