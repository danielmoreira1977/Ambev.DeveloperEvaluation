using Ambev.DeveloperEvaluation.Common.Primitives;

namespace Ambev.DeveloperEvaluation.Domain.Entities.Products
{
    public interface IProduct
    {
        string Category { get; set; }
        string Description { get; set; }
        string Image { get; set; }
        Price Price { get; set; }
        Rating Rating { get; set; }
        string Title { get; set; }
    }
}
