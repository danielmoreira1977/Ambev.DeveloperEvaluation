namespace Ambev.DeveloperEvaluation.Common.Security
{
    public interface IJwtTokenGenerator
    {
        string GenerateRefreshToken();

        string GenerateToken(Guid id, string username, string role);
    }
}
