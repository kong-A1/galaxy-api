using System.ComponentModel.DataAnnotations;

namespace galaxy_api.Dtos.Requests;

// ทั้งคำขอ
public record CreateRolesRequest : IValidatableObject
{
    [Required]
    [MinLength(1, ErrorMessage = "At least 1 role is required")]
    [MaxLength(50, ErrorMessage = "At most 50 roles per request")]
    public List<CreateRoleItem> Roles { get; init; } = [];

    public IEnumerable<ValidationResult> Validate(ValidationContext context)
    {
        if (Roles.Any(x => x is null))
        {
            yield return new ValidationResult(
                "Roles cannot contain null", [nameof(Roles)]);
            yield break;
        }

        var repeated = Roles
            .GroupBy(x => x.Code)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key)
            .ToList();

        if (repeated.Count > 0)
        {
            yield return new ValidationResult(
                $"Repeated codes in request: {string.Join(", ", repeated)}",
                [nameof(Roles)]);
        }
    }
}

// หนึ่งรายการในลิสต์
public record CreateRoleItem
{
    [Required]
    [StringLength(20)]
    [RegularExpression("^[a-z0-9_]+$",
        ErrorMessage = "Code can only contain a-z, 0-9 and _")]
    public string Code { get; init; } = "";

    [Required]
    [StringLength(50)]
    public string Name { get; init; } = "";
}