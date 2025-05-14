using Ambev.DeveloperEvaluation.Common.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Entities.Carts
{
    public class Cart : AggregateRoot<CartId>, ICart
    {
        public DateTime Date { get; set; }
        public List<CartProduct> Products { get; set; } = new();
        public UserId UserId { get; set; }
    }
}
