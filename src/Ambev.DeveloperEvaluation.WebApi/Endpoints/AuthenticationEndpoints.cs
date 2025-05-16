using Ambev.DeveloperEvaluation.Application.Auth.AuthenticateUser;
using Ambev.DeveloperEvaluation.Common.HttpResults;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.WebApi.Features.Auth.AuthenticateUserFeature;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Endpoints;

public static class AuthenticationEndpoints
{
    private const string AuthenticationTag = "Authentication";

    public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        /// <summary>
        /// Gera um token JWT de acesso e um refresh token para testes.
        /// </summary>
        /// <remarks>
        /// Este endpoint é utilizado para gerar tokens JWT válidos de forma fictícia, com dados de
        /// usuário simulados. Pode ser útil para testes em ambientes de desenvolvimento ou
        /// validação de fluxo de autenticação.
        /// </remarks>
        /// <returns>Um objeto [TokenResponse] contendo o access token e o refresh token</returns>
        /// <response code="200">Tokens gerados com sucesso</response>
        app.MapGet("/auth/generate-test-token", (IJwtTokenGenerator _jwtTokenGenerator) =>
        {
            //Fake user information for testing purposes
            var userId = 100;
            var email = "user@email.com";
            var nome = "Daniel Moreira";

            var accessToken = _jwtTokenGenerator.GenerateToken(userId, email, nome);
            var refreshToken = _jwtTokenGenerator.GenerateRefreshToken();

            return Results.Ok(new { accessToken, refreshToken });
        })
        .WithName("Generate Test Token")
        .WithTags(AuthenticationTag)
        .WithOpenApi();

        app.MapPost("/auth/login",
            async (
                [FromBody] AuthenticateUserRequest request,
                [FromServices] IMediator mediator,
                CancellationToken cancellationToken
            ) =>
        {
            var result = await mediator.Send(new AuthenticateUserCommand(request.Email, request.Password), cancellationToken);

            return result.Match(
                success => Results.NoContent(),
                error => Results.BadRequest(error)
            );
        })
        .WithName("Authenticate User")
        .WithTags(AuthenticationTag)
        .Produces<Result<AuthenticateUserResult>>(StatusCodes.Status200OK)
        .Produces<Result<AuthenticateUserResult>>(StatusCodes.Status400BadRequest)
        .Produces<Result<AuthenticateUserResult>>(StatusCodes.Status500InternalServerError)
        .WithOpenApi();
    }
}
