namespace galaxy_api.Models;

public class AuditLog
{
    public Guid Id { get; set; }
    public required string ActorType { get; set; }
    public Guid? ActorUserId { get; set; }
    public Guid? PlatformActorId { get; set; }
    public required string Action { get; set; }
    public required string EntityType { get; set; }
    public Guid? EntityId { get; set; }
    public string? OldValues { get; set; }
    public string? NewValues { get; set; }
    public string? IpAddress { get; set; }
    public string? UserAgent { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}