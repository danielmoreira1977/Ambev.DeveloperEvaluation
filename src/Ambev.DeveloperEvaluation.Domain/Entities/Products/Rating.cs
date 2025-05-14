using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.Domain.Entities.Products
{
    [Owned]
    public class Rating
    {
        public int Count { get; set; }
        public double Rate { get; set; }
    }
}
