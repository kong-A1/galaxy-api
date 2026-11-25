namespace galaxy_api.Entities;

public class User : AuditableBase
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTimeOffset? LastLoginAt { get; set; }
    public int FailedLoginCount { get; set; }
    public DateTimeOffset? LockedUntil { get; set; }
    public DateTimeOffset? PasswordChangedAt { get; set; }
    public bool MustChangePassword { get; set; }
}