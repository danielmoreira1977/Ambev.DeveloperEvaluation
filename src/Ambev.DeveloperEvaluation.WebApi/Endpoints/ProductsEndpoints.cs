using Ambev.DeveloperEvaluation.Application.Auth.AuthenticateUser;
using Ambev.DeveloperEvaluation.Application.Products.GetProducts;
using Ambev.DeveloperEvaluation.Common.HttpResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Endpoints;

public static class ProductsEndpoints
{
    private const string ProductsTag = "Products";

    public static void MapProductEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/products",
        async (
            HttpRequest request,
            [FromQuery] int? _page,
            [FromQuery] int? _size,
            [FromQuery] string? _order,
            [FromServices] IMediator mediator,
            CancellationToken cancellationToken = default
            ) =>
        {
            var pageNumber = _page ?? 1;
            var pageSize = _size ?? 20;
            string? order = _order;

            var queryParams = request.Query;

            var parameters = queryParams.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToString());

            var result = await mediator.Send(new GetProductsCommand(pageNumber, pageSize, order, parameters), cancellationToken);

            return result.Match(
                success => Results.Ok(result),
                error => Results.BadRequest(error)
            );
        })
        .WithName("Get products")
        .WithTags(ProductsTag)
        .Produces<Result<AuthenticateUserResult>>(StatusCodes.Status200OK)
        .Produces<Result<AuthenticateUserResult>>(StatusCodes.Status400BadRequest)
        .Produces<Result<AuthenticateUserResult>>(StatusCodes.Status500InternalServerError)
        .WithOpenApi();

        app.MapGet("/products/{id}", () => "Hello from products!");
        app.MapGet("/products/categories", () => "Hello from products");
        app.MapGet("/products/categories/{category}", () => "Hello from products!");

        app.MapPost("/products", () => "Hello from products!");

        app.MapPut("/products/{id}", () => "Hello from products!");

        app.MapDelete("/products/{id}", () => "Hello from products!");
    }
}
