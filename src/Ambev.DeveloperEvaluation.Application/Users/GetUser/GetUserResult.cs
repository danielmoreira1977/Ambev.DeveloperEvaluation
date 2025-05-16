using Ambev.DeveloperEvaluation.Application.Users.Dtos;
using Ambev.DeveloperEvaluation.Domain.Entities.Users;

namespace Ambev.DeveloperEvaluation.Application.Users.GetUser;

/// <summary>
/// Response model for GetUser operation
/// </summary>
public class GetUserResult : UserDto
{
    public GetUserResult(User user) : base(user)
    {
    }
}
