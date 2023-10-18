namespace GroupApiProject.Data.Entities;

public class GearEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int Type { get; set; }
    public int Value { get; set; }
    public DateTime DateCreated { get; set; }
}