namespace galaxy_api.Services;

public class AnonymousCurrentUser : ICurrentUser
{
    public Guid? UserId => null;
}