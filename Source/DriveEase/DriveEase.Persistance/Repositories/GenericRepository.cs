using DriveEase.Domain.Entities;
using DriveEase.Domain.Repositories;
using DriveEase.SharedKernel.Primitives.Result;

namespace DriveEase.Persistance.Repositories;

/// <inheritdoc/>
public class GenericRepository<T>
    : IGenericRepository<T>
    where T : BaseEntity
{
    /// <summary>
    /// The database context.
    /// </summary>
    protected DriveEaseDbContext dbContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="GenericRepository{T}"/> class.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    public GenericRepository(DriveEaseDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    /// <inheritdoc/>
    public async Task<Result<int>> CreateAsync(T entity)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public async Task<Result> DeleteAsync(T entity)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public async Task<Result<IList<T>>> GetAsync()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public async Task<Result<T>> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public async Task<Result<int>> UpdateAsync(T entity)
    {
        throw new NotImplementedException();
    }
}
