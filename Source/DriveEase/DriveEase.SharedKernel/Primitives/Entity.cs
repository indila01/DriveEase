﻿using DriveEase.SharedKernel.Util;

namespace DriveEase.SharedKernel.Primitives;

/// <summary>
/// Represents the base class that all entities derive from.
/// </summary>
public abstract class Entity
    : IEquatable<Entity>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Entity"/> class.
    /// </summary>
    /// <param name="id">The entity identifier.</param>
    protected Entity(Guid id)
        : this()
    {
        Ensure.NotEmpty(id, "The identifier is required.", nameof(id));

        this.Id = id;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Entity"/> class.
    /// </summary>
    /// <remarks>
    /// Required by EF Core.
    /// </remarks>
    protected Entity()
    {
    }

    /// <summary>
    /// Gets the entity identifier.
    /// </summary>
    public Guid Id { get; private set; }

    public static bool operator ==(Entity a, Entity b)
    {
        if (a is null && b is null)
        {
            return true;
        }

        if (a is null || b is null)
        {
            return false;
        }

        return a.Equals(b);
    }

    public static bool operator !=(Entity a, Entity b) => !(a == b);

    /// <inheritdoc />
    public bool Equals(Entity other)
    {
        if (other is null)
        {
            return false;
        }

        return ReferenceEquals(this, other) || this.Id == other.Id;
    }

    /// <inheritdoc />
    public override bool Equals(object obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (obj.GetType() != this.GetType())
        {
            return false;
        }

        if (!(obj is Entity other))
        {
            return false;
        }

        if (this.Id == Guid.Empty || other.Id == Guid.Empty)
        {
            return false;
        }

        return this.Id == other.Id;
    }

    /// <inheritdoc />
    public override int GetHashCode() => this.Id.GetHashCode() * 41;
}