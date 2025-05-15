using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.Domain.Entities.Products
{
    [Owned]
    public class Rating(int count, double rate)
    {
        public int Count { get; init; } = count;
        public double Rate { get; init; } = rate;
    }
}
