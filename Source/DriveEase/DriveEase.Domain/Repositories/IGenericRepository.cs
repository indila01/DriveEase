using DriveEase.Domain.Entities;
using DriveEase.SharedKernel.Primitives.Result;

namespace DriveEase.Domain.Repositories;

/// <summary>
/// Generic repository.
/// </summary>
/// <typeparam name="T"> generic entity type.</typeparam>
public interface IGenericRepository<T>
    where T : BaseEntity
{
    /// <summary>
    /// Gets the asynchronous.
    /// </summary>
    /// <returns>list of generic entity</returns>
    Task<Result<IList<T>>> GetAsync();

    /// <summary>
    /// Gets the by identifier asynchronous.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns>Generic entity.</returns>
    Task<Result<T>> GetByIdAsync(int id);

    /// <summary>
    /// Creates the asynchronous.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns>Generic entity.</returns>
    Task<Result<int>> CreateAsync(T entity);

    /// <summary>
    /// Updates the asynchronous.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns>Generic entity. </returns>
    Task<Result<int>> UpdateAsync(T entity);

    /// <summary>
    /// Deletes the asynchronous.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns>Generic entity.</returns>
    Task<Result> DeleteAsync(T entity);
}
