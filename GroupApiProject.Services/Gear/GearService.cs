using GroupApiProject.Data;
using GroupApiProject.Models.Gear;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GroupApiProject.Services.Gear
{
    public class GearService : IGearService
    {
        private readonly ApplicationDbContext _dbcontext;
        public GearService(ApplicationDbContext dbContext)
        {
            _dbcontext = dbContext;
        }

        [HttpGet]
        public async Task<IEnumerable<GearRequest>> GetGearsAsync()
        {
            List<GearRequest> gears = await _dbcontext.Gear.Select(entity => new GearRequest
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                DateCreated = entity.DateCreated
            }).ToListAsync();

            return gears;
        }
    }
}