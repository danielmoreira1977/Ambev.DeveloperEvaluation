namespace Ambev.DeveloperEvaluation.WebApi.Endpoints;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this IEndpointRouteBuilder app) => app.MapGet("/users", () => "Hello from users!");
}
