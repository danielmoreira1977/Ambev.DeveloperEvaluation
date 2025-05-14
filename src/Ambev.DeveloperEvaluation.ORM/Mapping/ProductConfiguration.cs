using Ambev.DeveloperEvaluation.Common.Primitives;
using Ambev.DeveloperEvaluation.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(u => u.Id);

        builder.Property(b => b.Id)
            .HasConversion(
                id => id.Value,
                value => new ProductId(value)
            ).HasColumnName("Id");

        builder.Property(x => x.Category).IsRequired();
        builder.Property(x => x.Description).IsRequired();
        builder.Property(x => x.Image);

        builder.Property(b => b.Price)
            .HasConversion(
                id => id.Value,
                value => new Price(value)
            ).HasColumnName("Price");

        builder.OwnsOne(u => u.Rating);

        builder.Property(x => x.Title).IsRequired();
    }
}
