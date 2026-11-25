namespace galaxy_api.Entities;

public class Role : AuditableBase
{
    public required string Code { get; set; }
    public required string Name { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsSuperAdmin { get; set; }
    public bool IsSystem { get; set; }
}