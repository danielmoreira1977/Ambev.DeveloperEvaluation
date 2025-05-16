using Ambev.DeveloperEvaluation.Application.Auth.AuthenticateUser;
using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using Ambev.DeveloperEvaluation.Common.HttpResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Endpoints;

public static class UserEndpoints
{
    private const string UsersTag = "Users";

    public static void MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/users", (
            HttpRequest request,
            [FromQuery] int _page,
            [FromQuery] int _size,
            [FromQuery] string _order
            ) =>
        {
            var queryParams = request.Query;

            var result = queryParams.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToString());

            return Results.Json(result);
        })
        .WithName("Get users")
        .WithTags(UsersTag)
        .Produces<Result<AuthenticateUserResult>>(StatusCodes.Status200OK)
        .Produces<Result<AuthenticateUserResult>>(StatusCodes.Status400BadRequest)
        .Produces<Result<AuthenticateUserResult>>(StatusCodes.Status500InternalServerError)
        .WithOpenApi();

        app.MapGet("/users/{id}",
            async (
                int id,
                [FromServices] IMediator mediator,
                CancellationToken cancellationToken
            ) =>
            {
                var result = await mediator.Send(new GetUserCommand(id), cancellationToken);

                return result.Match(
                    success => Results.Ok(result),
                    error => Results.BadRequest(error)
                );
            })
        .RequireAuthorization()
        .WithName("Get user by ID")
        .WithTags(UsersTag)
        .Produces<Result<AuthenticateUserResult>>(StatusCodes.Status200OK)
        .Produces<Result<AuthenticateUserResult>>(StatusCodes.Status400BadRequest)
        .Produces<Result<AuthenticateUserResult>>(StatusCodes.Status500InternalServerError)
        .WithOpenApi();

        app.MapPost("/users", () => "Hello from users!");

        app.MapPut("/users/{id}", () => "Hello from users!");

        app.MapDelete("/users/{id}", () => "Hello from users!");
    }
}
