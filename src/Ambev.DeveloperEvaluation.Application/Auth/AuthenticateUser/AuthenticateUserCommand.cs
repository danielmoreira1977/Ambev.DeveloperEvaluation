using Ambev.DeveloperEvaluation.Common.HttpResults;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Auth.AuthenticateUser;

/// <summary>
/// Command for authenticating a user in the system. Implements IRequest for mediator pattern handling.
/// </summary>
public record struct AuthenticateUserCommand(string Email, string Password) : IRequest<Result<AuthenticateUserResult>>;
