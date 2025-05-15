using Ambev.DeveloperEvaluation.Common.Primitives;

namespace Ambev.DeveloperEvaluation.Domain.Entities.Carts
{
    public class Cart : AggregateRoot<CartId>, ICart
    {
        public Cart()
        { }

        public Cart(UserId userId)
        {
            UserId = userId;
            Date = DateTime.UtcNow;
            Products = [];
        }

        public DateTime Date { get; set; }
        public List<CartProduct> Products { get; set; } = new();
        public UserId UserId { get; set; }
    }
}
