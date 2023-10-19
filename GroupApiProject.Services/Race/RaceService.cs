using GroupApiProject.Data;
using GroupApiProject.Models.Race;
using Microsoft.EntityFrameworkCore;

namespace GroupApiProject.Services.Race
{
    public class RaceService : IRaceService
    {
        private readonly ApplicationDbContext _dbcontext;
        public RaceService(ApplicationDbContext dbContext)
        {
            _dbcontext = dbContext;
        }

        public async Task<IEnumerable<RaceRequest>> GetAllRacesAsync()
        {
            List<RaceRequest> races = await _dbcontext.Races.Select(entity => new RaceRequest
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                StrengthModifier = entity.StrengthModifier,
                ConstitutionModifier = entity.ConstitutionModifier,
                IntelligenceModifier = entity.IntelligenceModifier,
                DateCreated = entity.DateCreated
            }).ToListAsync();

            return races;
        }
    }
}