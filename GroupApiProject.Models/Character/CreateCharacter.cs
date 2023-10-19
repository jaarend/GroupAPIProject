namespace GroupApiProject.Models.Character;

public class CreateCharacter
{
    public string? Name { get; set; }
    public string? Description { get; set; }

    public int Type { get; set; }

    public int? RaceId { get; set; }

    public int? ClassId { get; set; }

    //initialize character stats
    //armor, strength, constitution, intelligence are calculated in methods on creation
    public int Level = 1;

    public int Armor { get; set; }
    public int Strength { get; set; }
    public int Constitution { get; set; }
    public int Intelligence { get; set; }

    public int Hp = 100;

    public int Xp = 0;

    public int Ap = 10;

}