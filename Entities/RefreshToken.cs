namespace galaxy_api.Entities;

public class RefreshToken
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public required string TokenHash { get; set; }
    public Guid FamilyId { get; set; }
    public DateTimeOffset ExpiresAt { get; set; }
    public DateTimeOffset? RevokedAt { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public string? CreatedIp { get; set; }
    public string? UserAgent { get; set; }
}