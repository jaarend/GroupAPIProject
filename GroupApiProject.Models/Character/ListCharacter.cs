namespace GroupApiProject.Models.Character;

public class ListCharacter
{
    public string? Name { get;}
    public string? Description { get;}

    public int Type { get;}

    public int? RaceId {get;}

    public int? ClassId { get;}

    //gets all current stats of character
    public int Level {get;}

    public int Hp {get;}

    public int Xp {get;}

    public int Ap {get;}
}