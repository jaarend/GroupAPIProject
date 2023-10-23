using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupApiProject.Models.Attack
{
    public class AttackRead
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int Type { get; set; }
        public int HitValue { get; set; }
        public int APCost { get; set; }
        public DateTime DateCreated { get; set; }
    }
}