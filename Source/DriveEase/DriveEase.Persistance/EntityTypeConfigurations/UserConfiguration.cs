using DriveEase.Domain.Entities;
using DriveEase.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriveEase.Persistance.EntityTypeConfigurations;

/// <summary>
/// User entity configuration
/// </summary>
/// <seealso cref="Microsoft.EntityFrameworkCore.IEntityTypeConfiguration&lt;DriveEase.Domain.Entities.User&gt;" />
internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    /// <summary>
    /// Configures the entity of type <typeparamref name="TEntity" />.
    /// </summary>
    /// <param name="builder">The builder to be used to configure the entity type.</param>
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(user => user.Id);

        builder.OwnsOne(user => user.FirstName, firstNameBuilder =>
        {
            firstNameBuilder.WithOwner();

            firstNameBuilder.Property(firstName => firstName.Value)
                .HasColumnName(nameof(User.FirstName))
                .HasMaxLength(FirstName.MaxLength)
                .IsRequired();
        });

        builder.OwnsOne(user => user.LastName, lastNameBuilder =>
        {
            lastNameBuilder.WithOwner();

            lastNameBuilder.Property(lastName => lastName.Value)
                .HasColumnName(nameof(User.LastName))
                .HasMaxLength(LastName.MaxLength)
                .IsRequired();
        });

        builder.OwnsOne(user => user.Email, emailBuilder =>
        {
            emailBuilder.WithOwner();

            emailBuilder.Property(email => email.Value)
                .HasColumnName(nameof(User.Email))
                .HasMaxLength(Email.MaxLength)
                .IsRequired();
        });

        builder.Property<string>("passwordHash")
               .HasField("passwordHash")
               .HasColumnName("PasswordHash")
               .IsRequired();

        builder.Property(user => user.CreatedDate).IsRequired();

        builder.Property(user => user.UpdatedDate);

        builder.Property(user => user.DeletedDate);

        builder.Property(user => user.IsDeleted).HasDefaultValue(false);

        builder.HasQueryFilter(user => !user.IsDeleted);

        builder.Ignore(user => user.FullName);
    }
}
