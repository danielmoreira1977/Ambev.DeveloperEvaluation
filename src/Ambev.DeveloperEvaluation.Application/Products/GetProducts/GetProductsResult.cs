using Ambev.DeveloperEvaluation.Application.Products.Dtos;
using Ambev.DeveloperEvaluation.Common.HttpResults;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProducts;

/// <summary>
/// Response model for GetUser operation
/// </summary>
public class GetProductsResult : PaginatedList<ProductDto>
{
    public GetProductsResult(List<ProductDto> items, int count, int pageNumber, int pageSize)
        : base(items, count, pageNumber, pageSize)
    {
    }
}
