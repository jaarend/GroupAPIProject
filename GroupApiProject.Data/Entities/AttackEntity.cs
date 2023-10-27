using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GroupApiProject.Data.Entities
{
    public class AttackEntity
    {
    [Key]
    public int Id { get; set; }
    [Required]
    [MinLength(1), MaxLength(100)]
    public string Name { get; set; } = null!;
    [MinLength(1), MaxLength(1000)]
    public string? Description { get; set; }
    [ForeignKey(nameof(attackType))]
    public int Type { get; set; }
    public AttackTypeEntity? attackType { get; set; }
    [Required]
    public int HitValue { get; set; }
    [Required]
    public int APCost { get; set; }
    [Required]
    public DateTime DateCreated { get; set; }
    }
}