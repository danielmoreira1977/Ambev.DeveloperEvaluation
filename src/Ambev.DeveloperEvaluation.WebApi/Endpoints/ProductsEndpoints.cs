namespace Ambev.DeveloperEvaluation.WebApi.Endpoints;

public static class ProductsEndpoints
{
    public static void MapProductEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/products", () => "Hello from products!");
        app.MapGet("/products/{id}", () => "Hello from products!");
        app.MapGet("/products/categories", () => "Hello from products");
        app.MapGet("/products/categories/{category}", () => "Hello from products!");

        app.MapPost("/products", () => "Hello from products!");

        app.MapPut("/products/{id}", () => "Hello from products!");

        app.MapDelete("/products/{id}", () => "Hello from products!");
    }
}
