using Ambev.DeveloperEvaluation.Common.Primitives;
using Ambev.DeveloperEvaluation.Domain.Entities.Products;

namespace Ambev.DeveloperEvaluation.Domain.Entities.Carts
{
    public class CartProduct : BaseEntity<Guid>
    {
        public Cart Cart { get; set; } = null!;
        public CartId CartId { get; set; }
        public Product Product { get; set; } = null!;
        public ProductId ProductId { get; set; }
        public Quantity Quantity { get; set; }
    }
}
