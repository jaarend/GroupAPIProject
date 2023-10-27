using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GroupApiProject.Models.AttackType
{
    public class AttackTypeCreate
    {
        [Required]
        [MinLength(1, ErrorMessage = "{0} must be at least {1} character long."), MaxLength(100, ErrorMessage = "{0} must contain no more than {1} characters.")]
        public string Name { get; set; } = null!;
        [Required]
        [MinLength(1, ErrorMessage = "{0} must be at least {1} character long."), MaxLength(1000, ErrorMessage = "{0} must contain no more than {1} characters.")]
        public string? Description { get; set; }
    }
}