using Ambev.DeveloperEvaluation.Common.Primitives;

namespace Ambev.DeveloperEvaluation.Domain.Entities.Products
{
    public sealed class Product : AggregateRoot<ProductId>, IProduct
    {
        public string Category { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public Price Price { get; set; }
        public Rating Rating { get; set; } = new();
        public string Title { get; set; } = string.Empty;
    }
}
