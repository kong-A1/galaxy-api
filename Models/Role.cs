namespace galaxy_api.Models;

public class Role : AuditableEntity
{
    public required string Code { get; set; }
    public required string Name { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsSuperAdmin { get; set; }
    public bool IsSystem { get; set; }
}