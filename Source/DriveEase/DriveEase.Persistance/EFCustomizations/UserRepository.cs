using DriveEase.Domain.Entities;
using DriveEase.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DriveEase.Persistance.EFCustomizations;

/// <inheritdoc/>
public class UserRepository : BaseRepository<User>, IUserRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserRepository"/> class.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    public UserRepository(DriveEaseDbContext dbContext)
        : base(dbContext)
    {
    }

    /// <inheritdoc/>
    public async Task<User> GetUserByName(string username)
        => await this.dbContext?.Users
        .AsNoTracking()
        .FirstOrDefaultAsync(x => x.FirstName == username);
}