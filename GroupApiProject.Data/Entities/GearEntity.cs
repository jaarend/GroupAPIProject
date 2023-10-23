using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroupApiProject.Data.Entities;

public class GearEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MinLength(1), MaxLength(100)]
    public string Name { get; set; } = null!;

    [MinLength(1), MaxLength(1000)]
    public string? Description { get; set; }

    [Required]
    [ForeignKey(nameof(typeId))]
    public int Type { get; set; } // 1 is weapon 2 is armor
    public GearTypeEntity? typeId { get; set; }
    [Required]
    public int Value { get; set; } //modifier for attack/armor calculation

    [Required]
    public DateTime DateCreated { get; set; }
}