namespace GroupApiProject.Models.Race
{
    public class RaceCreate
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int StrengthModifier { get; set; }
        public int ConstitutionModifier { get; set; }
        public int IntelligenceModifier { get; set; }
    }
}