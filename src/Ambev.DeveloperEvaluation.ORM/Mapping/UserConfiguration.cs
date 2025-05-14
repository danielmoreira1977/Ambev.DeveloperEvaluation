using Ambev.DeveloperEvaluation.Common.Constants;
using Ambev.DeveloperEvaluation.Common.Primitives;
using Ambev.DeveloperEvaluation.Domain.Entities.Users;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);

        builder.Property(b => b.Id)
            .HasConversion(
                id => id.Value,
                value => new UserId(value)
            ).HasColumnName("Id");

        builder.Property(b => b.Email)
        .HasConversion(
            email => email.Value,
            value => new Email(value)
        )
        .IsRequired().HasMaxLength(DatabaseSizes.EmailSize);

        builder.OwnsOne(u => u.Name);

        builder.Property(b => b.Password)
        .HasConversion(
            password => password.Value,
            value => new Password(value)
        )
        .IsRequired().HasMaxLength(DatabaseSizes.PasswordSize);

        builder.Property(b => b.Phone)
        .HasConversion(
            phone => phone.Value,
            value => new Phone(value)
        )
        .IsRequired().HasMaxLength(DatabaseSizes.PhoneSize);

        builder.Property(b => b.Username)
        .HasConversion(
            username => username.Value,
            value => new Username(value)
        )
        .IsRequired().HasMaxLength(DatabaseSizes.UsernameSize);

        builder.Property(u => u.Role)
        .HasConversion(
            p => p.Value,
            value => UserRole.FromValue(value)
        );

        builder.Property(u => u.Status)
        .HasConversion(
            p => p.Value,
            value => UserStatus.FromValue(value)
        );

        builder.OwnsOne(u => u.Address, address =>
        {
            address.OwnsOne(a => a.Geolocation);
            address.OwnsOne(a => a.ZipCode);
        });
    }
}
