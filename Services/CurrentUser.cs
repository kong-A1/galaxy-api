using galaxy_api.Interfaces;

namespace galaxy_api.Services;

public class CurrentUser : ICurrentUser
{
    public Guid? UserId => null;
}