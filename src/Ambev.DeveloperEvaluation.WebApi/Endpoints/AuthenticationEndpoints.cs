namespace Ambev.DeveloperEvaluation.WebApi.Endpoints;

public static class AuthenticationEndpoints
{
    public static void MapAuthEndpoints(this IEndpointRouteBuilder app) => app.MapGet("/authentication", () => "Hello from users!");
}
