using System.ComponentModel.DataAnnotations;

namespace GroupApiProject.Data.Entities;

public class RaceEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MinLength(1), MaxLength(100)]
    public string Name { get; set; } = null!;

    [MinLength(1), MaxLength(1000)]
    public string? Description { get; set; }

    public int StrengthModifier { get; set; }
    public int ConstitutionModifier { get; set; }
    public int IntelligenceModifier { get; set; }

    [Required]
    public DateTime DateCreated { get; set; }
}