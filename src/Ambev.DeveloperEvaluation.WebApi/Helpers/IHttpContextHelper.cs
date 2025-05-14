namespace Ambev.DeveloperEvaluation.WebApi.Helpers;

public interface IHttpContextHelper
{
    string GetCurrentUserEmail();

    Guid GetCurrentUserId();
}
