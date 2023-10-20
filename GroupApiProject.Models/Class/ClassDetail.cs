using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupApiProject.Models.Class
{
    public class ClassDetail
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTimeOffset CreatedUtc { get; set; }
        public DateTimeOffset? ModifiedUtc { get; set; }
    }
}