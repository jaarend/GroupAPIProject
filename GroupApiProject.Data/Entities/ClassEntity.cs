namespace GroupApiProject.Data.Entities;

public class ClassEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int WeaponId { get; set; }
    public int ArmorId { get; set; }
    public int AttackSlot_1 { get; set; }
    public int AttackSlot_2 { get; set; }
    public DateTime DateCreated { get; set; }
}