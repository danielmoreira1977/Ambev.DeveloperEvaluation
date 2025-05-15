using Ambev.DeveloperEvaluation.Common.Primitives;

namespace Ambev.DeveloperEvaluation.Domain.Entities.Products
{
    public interface IProduct
    {
        string? Category { get; }
        string? Description { get; }
        string? Image { get; }
        Price Price { get; }
        Rating? Rating { get; }
        string? Title { get; }
    }
}
