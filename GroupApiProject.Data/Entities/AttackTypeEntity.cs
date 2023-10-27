using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GroupApiProject.Data.Entities
{
    public class AttackTypeEntity
    {
    [Key]
    public int Id { get; set; }
    [Required]
    [MinLength(1), MaxLength(100)]
    public string Name { get; set; } = null!;
    [MinLength(1), MaxLength(1000)]
    public string? Description { get; set; }
    [Required]
    public DateTime DateCreated { get; set; }
    }
}