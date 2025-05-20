using Ambev.DeveloperEvaluation.Common.Errors;
using Ambev.DeveloperEvaluation.Common.HttpResults;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Entities.Users;
using Ambev.DeveloperEvaluation.Domain.Specifications;
using Ambev.DeveloperEvaluation.ORM;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Data;

namespace Ambev.DeveloperEvaluation.Application.Auth.AuthenticateUser
{
    public class AuthenticateUserHandler
    (
        DefaultContext defaultContext,
        IJwtTokenGenerator jwtTokenGenerator,
        ILogger<AuthenticateUserHandler> logger,
        IPasswordHasher passwordHasher
    ) : IRequestHandler<AuthenticateUserCommand, Result<AuthenticateUserResult>>
    {
        private readonly DefaultContext _defaultContext = defaultContext;
        private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;
        private readonly ILogger<AuthenticateUserHandler> _logger = logger;
        private readonly IPasswordHasher _passwordHasher = passwordHasher;

        public async Task<Result<AuthenticateUserResult>> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await GetByEmailAsync(request.Email, cancellationToken);

            if (user is null)
            {
                _logger.LogError("Invalid credentials for user with email: {Email}", request.Email);
                return Result<AuthenticateUserResult>.Failure(Error.UnauthorizedAccessCredentialsError);
            }

            if (!_passwordHasher.VerifyPassword(request.Password, user?.Password.Value))
            {
                _logger.LogError("Invalid password for user with email: {Email}", request.Email);
                return Result<AuthenticateUserResult>.Failure(Error.UnauthorizedAccessCredentialsError);
            }

            var activeUserSpec = new ActiveUserSpecification();
            if (!activeUserSpec.IsSatisfiedBy(user!))
            {
                _logger.LogError("Inactive user attempted to authenticate: {Email}", request.Email);
                return Result<AuthenticateUserResult>.Failure(Error.UnauthorizedAccessUserInactiveError);
            }

            var token = _jwtTokenGenerator.GenerateToken(user!.Id.Value, user.Username.Value, user.Role.Name);

            return Result<AuthenticateUserResult>.Success(new AuthenticateUserResult(token));
        }

        private async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return await (from u in _defaultContext.Users
                          where u.Email == new Common.Primitives.Email(email)
                          select new User(u.Id, u.Status, u.Username, u.Role))
                          .AsNoTracking()
                          .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
