using Ambev.DeveloperEvaluation.Application.Products.Dtos;
using Ambev.DeveloperEvaluation.Common.Errors;
using Ambev.DeveloperEvaluation.Common.HttpResults;
using Ambev.DeveloperEvaluation.Domain.Entities.Products;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.ORM.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProducts;

public class GetProductsHandler
    (
        DefaultContext defaultContext,
        ILogger<GetProductsHandler> logger
    ) : IRequestHandler<GetProductsCommand, Result<GetProductsResult>>
{
    private readonly DefaultContext _defaultContext = defaultContext;
    private readonly ILogger<GetProductsHandler> _logger = logger;

    public async Task<Result<GetProductsResult>> Handle(GetProductsCommand request, CancellationToken cancellationToken)
    {
        var validator = new GetProductsValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            string errorMessages = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));

            _logger.LogError("Validation failed: {ErrorMessage}", errorMessages);
            return Result<GetProductsResult>.Failure(new Error("Invalid parameters", errorMessages));
        }

        var queryResults = await GetProductsAsync(request.pageNumber, request.pageSize, request.order, request.parameters, cancellationToken);

        var dtos = MapToDto(queryResults.products);

        var pagedList = new GetProductsResult(dtos, queryResults.total, queryResults.pageNumber, queryResults.pageSize);

        return Result<GetProductsResult>.Success(pagedList);
    }

    private static List<ProductDto> MapToDto(List<Product>? products)
    {
        var result = new List<ProductDto>();

        if (products is null)
        {
            return result;
        }

        foreach (var product in products)
        {
            var rating = new RatingDto(product.Rating?.Rate ?? 0, product.Rating?.Count ?? 0);
            var dto = new ProductDto(
                product.Id.Value,
                product.Title,
                product.Price.Value,
                product.Description,
                product.Category,
                product.Image,
                rating
            );

            result.Add(dto);
        }

        return result;
    }

    private static IQueryable<Product> ProcessOrdering(string? order, IQueryable<Product> query)
    {
        if (string.IsNullOrWhiteSpace(order)) { return query; }

        var clausuleOrder = order.Replace("order=", "");

        query = query.ApplyOrdering(clausuleOrder);

        return query;
    }

    private static IQueryable<Product> ProcessParameteres(Dictionary<string, string>? parameteres, IQueryable<Product> query)
    {
        if (parameteres is null) { return query; }

        query = query.ApplyParameteres(parameteres);

        return query;
    }

    private async Task<(
        List<Product>? products,
        int total,
        int pageNumber,
        int pageSize
        )> GetProductsAsync(int pageNumber, int pageSize, string? order, Dictionary<string, string>? parameters, CancellationToken cancellationToken)
    {
        var skip = (pageNumber - 1) * pageSize;
        var query = _defaultContext.Products.AsNoTracking();

        query = ProcessParameteres(parameters, query);
        query = ProcessOrdering(order, query);

        // Qdo vc filtra e usa o offset, resultados podem ser omitidos
        var skipOffSet = parameters is not null;

        var total = await query.CountAsync(cancellationToken);
        var items =
            skipOffSet
            ? await query
                .Take(pageSize)
                .ToListAsync(cancellationToken)
            : await query
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

        return (items, total, pageNumber, pageSize);
    }
}
