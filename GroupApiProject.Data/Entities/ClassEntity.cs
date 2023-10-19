using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroupApiProject.Data.Entities;

public class ClassEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MinLength(1), MaxLength(100)]
    public string Name { get; set; } = null!;

    [MinLength(1), MaxLength(1000)]
    public string? Description { get; set; }

    [ForeignKey(nameof(weaponId))]
    public int WeaponId { get; set; }
    public GearEntity? weaponId { get; set; }

    [ForeignKey(nameof(armorId))]
    public int ArmorId { get; set; }
    public GearEntity? armorId { get; set; }

    public int AttackSlot_1 { get; set; }
    public int AttackSlot_2 { get; set; }

    [Required]
    public DateTime DateCreated { get; set; }
}