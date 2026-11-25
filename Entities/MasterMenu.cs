namespace galaxy_api.Entities;

public class MasterMenu : AuditableBase
{
    public Guid? ParentId { get; set; }
    public required string Code { get; set; }
    public required string NameTh { get; set; }
    public required string NameEn { get; set; }
    public string? Route { get; set; }
    public string? Icon { get; set; }
    public int SortOrder { get; set; }
    public bool IsActive { get; set; } = true;
}