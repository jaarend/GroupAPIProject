namespace GroupApiProject.Models.Character;

public class ListCharacter
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }

    public int Type { get; set; }

    public int? RaceId { get; set; }

    public int? ClassId { get; set; }

    //gets all current stats of character
    public int Level { get; set; }
    public int Armor { get; set; }
    public int Strength { get; set; }
    public int Constitution { get; set; }
    public int Intelligence { get; set; }

    public int Hp { get; set; }

    public int Xp { get; set; }

    public int Ap { get; set; }

    public int OwnerId { get; set; }
}