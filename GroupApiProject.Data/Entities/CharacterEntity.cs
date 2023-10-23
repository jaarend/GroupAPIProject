using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroupApiProject.Data.Entities;

public class CharacterEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MinLength(1), MaxLength(100)]
    public string? Name { get; set; }

    [MinLength(1), MaxLength(1000)]
    public string? Description { get; set; }

    [ForeignKey(nameof(typeId))]
    public int Type { get; set; }
    public CharacterTypeEntity? typeId { get; set; }

    [ForeignKey(nameof(raceId))]
    public int RaceId { get; set; }
    public RaceEntity? raceId { get; set; }

    [ForeignKey(nameof(classId))]
    public int ClassId { get; set; }
    public ClassEntity? classId { get; set; }

    public int Level { get; set; }
    public int Armor { get; set; }
    public int Strength { get; set; }
    public int Constitution { get; set; }
    public int Intelligence { get; set; }
    public int Hp { get; set; }
    public int Xp { get; set; }
    public int Ap { get; set; }

    [Required]
    [ForeignKey(nameof(userId))]
    public int OwnerId { get; set; }
    public UserEntity? userId { get; set; }

    [Required]
    public DateTime DateCreated { get; set; }
}