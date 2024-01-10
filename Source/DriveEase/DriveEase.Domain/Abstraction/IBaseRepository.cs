using DriveEase.Domain.Entities;

namespace DriveEase.Domain.Abstraction;

/// <summary>
/// IBaseReposptory
/// </summary>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
public interface IBaseRepository<TEntity>
    where TEntity : BaseEntity
{
    /// <summary>
    /// Gets the asynchronous.
    /// </summary>
    /// <returns>TEntity.</returns>
    Task<IList<TEntity>> GetAsync();

    /// <summary>
    /// Gets the by identifier asynchronous.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns>TEntity.</returns>
    Task<TEntity> GetByIDAsync(Guid id);

    /// <summary>
    /// Inserts the specified entity.
    /// </summary>
    /// <param name="entity">The entity.</param>
    void Add(TEntity entity);

    /// <summary>
    /// Adds the range.
    /// </summary>
    /// <param name="entities">The entities.</param>
    void AddRange(IList<TEntity> entities);
}
