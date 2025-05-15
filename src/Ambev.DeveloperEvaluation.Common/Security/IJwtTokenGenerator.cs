namespace Ambev.DeveloperEvaluation.Common.Security
{
    public interface IJwtTokenGenerator
    {
        string GenerateRefreshToken();

        string GenerateToken(int id, string username, string role);
    }
}
