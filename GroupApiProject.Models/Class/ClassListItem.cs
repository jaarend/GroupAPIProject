using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupApiProject.Models.Class
{
    public class ClassListItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int WeaponId { get; set; }
        public int ArmorId { get; set; }
        public int AttackSlot_1 { get; set; }
        public int AttackSlot_2 { get; set; }
        public DateTime DateCreated { get; set; }
    }
}