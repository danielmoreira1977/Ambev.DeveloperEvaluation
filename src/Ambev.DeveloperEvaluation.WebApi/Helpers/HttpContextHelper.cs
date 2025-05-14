using System.Security.Claims;

namespace Ambev.DeveloperEvaluation.WebApi.Helpers;

public sealed class HttpContextHelper(IHttpContextAccessor httpContextAccessor) : IHttpContextHelper
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public string GetCurrentUserEmail()
    {
        var email = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;
        if (string.IsNullOrEmpty(email))
        {
            throw new UnauthorizedAccessException("Email not found in token.");
        }
        return email;
    }

    public Guid GetCurrentUserId()
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!Guid.TryParse(userId, out var validId))
        {
            throw new UnauthorizedAccessException("User ID not found in token.");
        }

        return validId;
    }
}
