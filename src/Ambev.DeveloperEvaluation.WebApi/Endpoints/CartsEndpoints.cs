namespace Ambev.DeveloperEvaluation.WebApi.Endpoints;

public static class CartsEndpoints
{
    public static void MapCartsEndpoints(this IEndpointRouteBuilder app) => app.MapGet("/carts", () => "Hello from users!");
}
