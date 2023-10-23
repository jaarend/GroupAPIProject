using System.ComponentModel.DataAnnotations;

namespace GroupApiProject.Data.Entities;

public class CharacterTypeEntity
{

    [Key]
    public int Id {get; set;}

    [Required]
    [MinLength(1), MaxLength(100)]
    public string? Name { get; set; }
    
    [MinLength(1), MaxLength(1000)]
    public string? Description { get; set; }

    [Required]
    public DateTime DateCreated { get; set; }
}