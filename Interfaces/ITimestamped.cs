namespace galaxy_api.Interfaces;

public interface ITimestamped
{
    DateTimeOffset CreatedAt { get; set; }
    DateTimeOffset UpdatedAt { get; set; }
}