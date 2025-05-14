using Ambev.DeveloperEvaluation.Common.Primitives;

namespace Ambev.DeveloperEvaluation.Domain.Entities.Carts
{
    public interface ICart
    {
        DateTime Date { get; set; }
        List<CartProduct> Products { get; set; }
        UserId UserId { get; set; }
    }
}
