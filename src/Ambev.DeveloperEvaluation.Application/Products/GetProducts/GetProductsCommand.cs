using Ambev.DeveloperEvaluation.Common.HttpResults;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProducts;

public record struct GetProductsCommand(int pageNumber, int pageSize, string? order, Dictionary<string, string>? parameters) : IRequest<Result<GetProductsResult>>;
