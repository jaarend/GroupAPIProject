using System.Reflection.Metadata.Ecma335;

namespace GroupApiProject.Models.Gear
{
    public class GearDetail
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; } 
        public int Type { get; set; }
        public int Value { get; set; }
        public DateTime DateCreated { get; set; }

    }
}