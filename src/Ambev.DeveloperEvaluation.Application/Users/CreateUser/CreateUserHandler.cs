using Ambev.DeveloperEvaluation.Application.Products.GetProducts;
using Ambev.DeveloperEvaluation.Common.Errors;
using Ambev.DeveloperEvaluation.Common.HttpResults;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Entities.Users;
using Ambev.DeveloperEvaluation.ORM;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Data;

namespace Ambev.DeveloperEvaluation.Application.Users.CreateUser;

/// <summary>
/// Handler for processing CreateUserCommand requests
/// </summary>
public class CreateUserHandler
    (
        DefaultContext defaultContext,
        ILogger<GetProductsHandler> logger,
        IPasswordHasher passwordHasher

    ) : IRequestHandler<CreateUserCommand, Result<CreateUserResult>>
{
    private readonly DefaultContext _defaultContext = defaultContext;
    private readonly ILogger<GetProductsHandler> _logger = logger;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;

    /// <summary>
    /// Handles the CreateUserCommand request
    /// </summary>
    /// <param name="command">The CreateUser command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created user details</returns>
    public async Task<Result<CreateUserResult>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateUserCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
        {
            string errorMessages = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));

            _logger.LogError("Validation failed: {ErrorMessage}", errorMessages);
            return Result<CreateUserResult>.Failure(new Error("Invalid parameters", errorMessages));
        }

        var existingUser = await GetByEmailAsync(command.Email, cancellationToken);
        if (existingUser)
        {
            _logger.LogError("Existent user with email: {email}", command.Email);
            return Result<CreateUserResult>.Failure(new Error("Existent user with email", $"Email: {command.Email}"));
        }

        try
        {
            User user = new User()
                .Create
                (
                   command.City,
                   command.Email!,
                   command.Firstname,
                   command.Lastname,
                   command.Number,
                   command.Password!,
                   command.Phone!,
                   command.Role!,
                   command.Status!,
                   command.Street,
                   command.Username!,
                   command.ZipCode,
                   command.Latitude,
                   command.Longitude
                );

            var createdUserId = await CreateUserAsync(user, cancellationToken);

            if (createdUserId is null)
            {
                _logger.LogError("Failed to create user with email: {email}", command.Email);
                return Result<CreateUserResult>.Failure(new Error("Failed to create user", $"Email: {command.Email}"));
            }

            var createUserResult = new CreateUserResult(createdUserId.Value!);
            return Result<CreateUserResult>.Success(createUserResult);
        }
        catch (Exception ex)
        {
            return Result<CreateUserResult>.Failure(new Error("Error on create a new user", ex.Message));
        }
    }

    private async Task<int?> CreateUserAsync(User user, CancellationToken cancellationToken)
    {
        var inserted = await _defaultContext.Users.AddAsync(user, cancellationToken);

        return inserted.Entity.Id.Value;
    }

    private async Task<bool> GetByEmailAsync(string? email, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(email))
        {
            return false;
        }

        return await _defaultContext.Users.AnyAsync(u => u.Email == new Common.Primitives.Email(email), cancellationToken);
    }
}
