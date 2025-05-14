using Ambev.DeveloperEvaluation.Common.Primitives;
using Ambev.DeveloperEvaluation.Domain.Entities.Carts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class CartConfiguration : IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> builder)
    {
        builder.ToTable("Cart");

        builder.HasKey(u => u.Id);

        builder.Property(b => b.Id)
            .HasConversion(
                id => id.Value,
                value => new CartId(value)
            ).HasColumnName("Id");

        builder.Property(x => x.Date).IsRequired();

        builder.Property(b => b.UserId)
        .HasConversion(
            id => id.Value,
            value => new UserId(value)
        ).HasColumnName("UserId");

        builder
            .HasMany(c => c.Products)
            .WithOne(cp => cp.Cart)
            .HasForeignKey(cp => cp.CartId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
