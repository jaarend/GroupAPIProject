namespace GroupApiProject.Models.Gear
{
    public class GearRequest
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int StrengthModifier { get; set; }
        public int ConstitutionModifier { get; set; }
        public int IntelligenceModifier { get; set; }
        public DateTime DateCreated { get; set; }
    }
}