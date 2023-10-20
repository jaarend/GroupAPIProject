namespace GroupApiProject.Models.Gear
{
    public class GearUpdate
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public int Type { get; set; }

        public int? GearId { get; set; }

        public int OwnerId { get; set; }
    }
}