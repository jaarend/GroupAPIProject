namespace GroupApiProject.Models.Character;

public class EditCharacter
{
    public int Id {get; set;}
    public string? Name { get; set; }
    public string? Description { get; set; }

    public int Type { get; set; }

    public int? RaceId { get; set; } //should changing this reset their earned stats? XP

    public int ClassId { get; set; }

    public int OwnerId {get; set;}


}