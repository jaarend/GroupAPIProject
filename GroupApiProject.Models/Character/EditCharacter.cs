namespace GroupApiProject.Models.Character;

public class EditCharacter
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }

    public int Type { get; set; }

    public int? RaceId { get; set; } //should changing this reset their earned stats? XP

    public int ClassId { get; set; }
    public int Armor { get; set; }
    public int Strength { get; set; }
    public int Constitution { get; set; }
    public int Intelligence { get; set; }

    public int OwnerId { get; set; }


}