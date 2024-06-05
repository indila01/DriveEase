using DriveEase.Domain.Abstraction;
using DriveEase.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DriveEase.Persistance.EFCustomizations;

/// <inheritdoc/>
public class BaseRepository<TEntity> : IBaseRepository<TEntity>
    where TEntity : BaseEntity
{
    protected readonly DriveEaseDbContext dbContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseRepository{TEntity}"/> class.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    public BaseRepository(DriveEaseDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    /// <inheritdoc/>
    public async Task<IList<TEntity>> GetAsync()
     => await this.dbContext.Set<TEntity>()
        .AsNoTracking()
        .ToListAsync();

    /// <inheritdoc/>
    public async Task<TEntity> GetByIDAsync(Guid id)
     => await this.dbContext.Set<TEntity>()
        .AsNoTracking()
        .FirstOrDefaultAsync(x => x.Id == id);

    /// <inheritdoc/>
    public void Add(TEntity entity)
        => this.dbContext.Set<TEntity>().Add(entity);

    /// <inheritdoc/>
    public void AddRange(IList<TEntity> entities)
     => this.dbContext.Set<TEntity>().AddRange(entities);

    /// <inheritdoc/>
    public void Remove(TEntity entity)
     => this.dbContext.Remove(entity);
}
