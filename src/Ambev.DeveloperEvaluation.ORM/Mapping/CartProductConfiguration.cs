using Ambev.DeveloperEvaluation.Common.Primitives;
using Ambev.DeveloperEvaluation.Domain.Entities.Carts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class CartProductConfiguration : IEntityTypeConfiguration<CartProduct>
{
    public void Configure(EntityTypeBuilder<CartProduct> builder)
    {
        builder.ToTable("CartProducts");

        builder.HasKey(u => u.Id);

        builder.Property(b => b.Id)
            .ValueGeneratedOnAdd()
            .HasColumnName("Id");

        builder.Property(b => b.CartId)
        .HasConversion(
            id => id.Value,
            value => new CartId(value)
        ).HasColumnName("CartId");

        builder.Property(b => b.Quantity)
        .HasConversion(
            id => id.Value,
            value => new Quantity(value)
        ).HasColumnName("Quantity");

        builder.Property(b => b.ProductId)
        .HasConversion(
            id => id.Value,
            value => new ProductId(value)
        ).HasColumnName("ProductId");
    }
}
