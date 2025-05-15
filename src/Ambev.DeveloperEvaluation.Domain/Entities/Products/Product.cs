using Ambev.DeveloperEvaluation.Common.Primitives;

namespace Ambev.DeveloperEvaluation.Domain.Entities.Products
{
    public sealed class Product : AggregateRoot<ProductId>, IProduct
    {
        public Product(string category, string description, Price price, Rating rating, string title, string? image = null)
        {
            Category = category;
            Description = description;
            Price = price;
            Rating = rating;
            Title = title;
            Image = image;
        }

        public Product()
        {
            Price = Price.Empty();
        }

        public string? Category { get; init; }
        public string? Description { get; init; }
        public string? Image { get; init; }
        public Price Price { get; init; }
        public Rating? Rating { get; init; }
        public string? Title { get; init; }
    }
}
