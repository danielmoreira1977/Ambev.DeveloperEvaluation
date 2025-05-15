namespace Ambev.DeveloperEvaluation.WebApi.Endpoints;

public static class CartsEndpoints
{
    public static void MapCartsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/carts", () => "Hello from users!");
        app.MapGet("/carts/{Id}", () => "Hello from users!");

        app.MapPost("/carts", () => "Hello from users!");

        app.MapPut("/carts/{Id}", () => "Hello from users!");

        app.MapDelete("/carts/{Id}", () => "Hello from users!");
    }
}
