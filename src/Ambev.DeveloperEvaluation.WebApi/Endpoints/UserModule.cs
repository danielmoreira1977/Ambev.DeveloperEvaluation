namespace Ambev.DeveloperEvaluation.WebApi.Endpoints;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/users", () => "Hello from users!");
        app.MapGet("/users/{id}", () => "Hello from users!");

        app.MapPost("/users", () => "Hello from users!");

        app.MapPut("/users/{id}", () => "Hello from users!");

        app.MapDelete("/users/{id}", () => "Hello from users!");
    }
}
