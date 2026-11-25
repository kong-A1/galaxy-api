namespace galaxy_api.Interfaces;

public interface ICurrentUser
{
    Guid? UserId { get; }
}