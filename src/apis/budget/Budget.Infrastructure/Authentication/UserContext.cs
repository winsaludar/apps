namespace Budget.Infrastructure.Authentication;

public sealed class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
    public Guid UserId => httpContextAccessor.HttpContext?.User.GetUserId() ?? throw new ApplicationException("User context is unavailable");
}
