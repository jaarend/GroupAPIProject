namespace GroupApiProject.Data.Entities;

public class RaceEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int StrengthModifier { get; set; }
    public int ConstitutionModifier { get; set; }
    public int IntelligenceModifier { get; set; }
    public DateTime DateCreated { get; set; }
}