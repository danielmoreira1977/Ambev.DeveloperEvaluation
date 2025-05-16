using Ambev.DeveloperEvaluation.Common.Errors;
using Ambev.DeveloperEvaluation.Common.HttpResults;
using Ambev.DeveloperEvaluation.Common.Primitives;
using Ambev.DeveloperEvaluation.Domain.Entities.Users;
using Ambev.DeveloperEvaluation.ORM;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Users.GetUser;

/// <summary>
/// Handler for processing GetUserCommand requests
/// </summary>
public class GetUserHandler
    (
        DefaultContext defaultContext,
        ILogger<GetUserHandler> logger
    ) : IRequestHandler<GetUserCommand, Result<GetUserResult>>
{
    private readonly DefaultContext _defaultContext = defaultContext;
    private readonly ILogger<GetUserHandler> _logger = logger;

    /// <summary>
    /// Handles the GetUserCommand request
    /// </summary>
    /// <param name="request">The GetUser command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The user details if found</returns>
    public async Task<Result<GetUserResult>> Handle(GetUserCommand request, CancellationToken cancellationToken)
    {
        var validator = new GetUserValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            var errorMessage = validationResult?.Errors?.First().ErrorMessage;

            _logger.LogError("Validation failed: {ErrorMessage}", errorMessage);
            return Result<GetUserResult>.Failure(Error.UserIdRequiredError);
        }

        var user = await GetByIdAsync(request.Id, cancellationToken);
        if (user == null)
        {
            _logger.LogError("User with ID {UserId} not found", request.Id);
            return Result<GetUserResult>.Failure(Error.UserNotFoundError(request.Id));
        }

        var dto = new GetUserResult(user);

        return Result<GetUserResult>.Success(dto);
    }

    private async Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _defaultContext.Users.Where(u => u.Id == new UserId(id))
                      .AsNoTracking()
                      .FirstOrDefaultAsync(cancellationToken);
    }
}
