using DriveEase.Domain.Entities;
using DriveEase.Domain.Repositories;
using DriveEase.Domain.ValueObjects;
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
    public async Task<User> GetUserByEmail(Email email, CancellationToken cancellationToken = default)
        => await this.dbContext?.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email.Value == email.Value, cancellationToken);

    /// <inheritdoc/>
    public async Task<User> GetUserByName(string username, CancellationToken cancellationToken = default)
        => await this.dbContext?.Users
        .AsNoTracking()
        .FirstOrDefaultAsync(x => x.FirstName == username, cancellationToken);

    /// <inheritdoc/>
    public async Task<bool> IsEmailUniqueAsync(Email email, CancellationToken cancellationToken = default)
    {
        var user = await this.dbContext?.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email.Value == email.Value, cancellationToken);

        return user is null;
    }
}