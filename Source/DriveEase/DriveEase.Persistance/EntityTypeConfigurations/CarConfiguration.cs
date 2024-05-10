using DriveEase.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriveEase.Persistance.EntityTypeConfigurations;

/// <summary>
/// Car entity configuration
/// </summary>
/// <seealso cref="Microsoft.EntityFrameworkCore.IEntityTypeConfiguration&lt;DriveEase.Domain.Entities.Car&gt;" />
internal sealed class CarConfiguration : IEntityTypeConfiguration<Car>
{
    /// <summary>
    /// Configures the entity of type <typeparamref name="TEntity" />.
    /// </summary>
    /// <param name="builder">The builder to be used to configure the entity type.</param>
    public void Configure(EntityTypeBuilder<Car> builder)
    {
        builder.HasKey(car => car.Id);

        builder.Property(car => car.Make)
            .HasMaxLength(20)
            .IsRequired();
        builder.Property(car => car.Model)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(car => car.CreatedDate).IsRequired();

        builder.Property(car => car.UpdatedDate);

        builder.Property(car => car.DeletedDate);

        builder.Property(car => car.IsDeleted).HasDefaultValue(false);

        builder.HasQueryFilter(car => !car.IsDeleted);
    }
}
