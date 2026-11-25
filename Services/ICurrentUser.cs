namespace galaxy_api.Services;

public interface ICurrentUser
{
    Guid? UserId { get; }
}