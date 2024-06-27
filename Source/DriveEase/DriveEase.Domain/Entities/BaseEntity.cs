using DriveEase.SharedKernel;
using DriveEase.SharedKernel.Util;

namespace DriveEase.Domain.Entities;

/// <summary>
/// Base entity
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    /// Gets or sets the domain events.
    /// </summary>
    /// <value>
    /// Domain Events.
    /// </value>
    private readonly List<IDomainEvent> domainEvents = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseEntity"/> class.
    /// </summary>
    /// <param name="id">The entity identifier.</param>
    protected BaseEntity(Guid id)
        : this()
    {
        Ensure.NotEmpty(id, "The identifier is required.", nameof(id));

        this.Id = id;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseEntity"/> class.
    /// </summary>
    protected BaseEntity()
    {
    }

    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    /// <value>
    /// The identifier.
    /// </value>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets the domain events. This collection is readonly.
    /// </summary>
    public IReadOnlyCollection<IDomainEvent> DomainEvents => this.domainEvents.AsReadOnly();

    /// <summary>
    /// Clears all the domain events from the <see cref="AggregateRoot"/>.
    /// </summary>
    public void ClearDomainEvents() => this.domainEvents.Clear();

    /// <summary>
    /// raise domain events
    /// </summary>
    /// <param name="domainEvent"> domain event</param>
    protected void RaiseDomainEvent(IDomainEvent domainEvent)
        => this.domainEvents.Add(domainEvent);
}
