using Ambev.DeveloperEvaluation.Common.HttpResults;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Users.GetUser;

/// <summary>
/// Command for retrieving a user by their ID
/// </summary>
public record struct GetUserCommand(int Id) : IRequest<Result<GetUserResult>>;
