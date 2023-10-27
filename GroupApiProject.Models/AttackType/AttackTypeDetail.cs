using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupApiProject.Models.AttackType
{
    public class AttackTypeDetail
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime DateCreated { get; set; }
    }
}