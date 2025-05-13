namespace Ambev.DeveloperEvaluation.WebApi.Endpoints;

public static class ProductsEndpoints
{
    public static void MapProductEndpoints(this IEndpointRouteBuilder app) => app.MapGet("/products", () => "Hello from users!");
}
