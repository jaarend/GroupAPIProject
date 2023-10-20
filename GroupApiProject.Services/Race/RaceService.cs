using GroupApiProject.Data;
using GroupApiProject.Data.Entities;
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

        public async Task<RaceDetail?> CreateRaceAsync(RaceCreate request)
        {
            RaceEntity entity = new()
            {
                Name = request.Name,
                Description = request.Description,
                StrengthModifier = request.StrengthModifier,
                ConstitutionModifier = request.ConstitutionModifier,
                IntelligenceModifier = request.IntelligenceModifier,
                DateCreated = DateTime.Now
            };

            _dbcontext.Races.Add(entity);
            var numberOfChanges = await _dbcontext.SaveChangesAsync();

            if (numberOfChanges == 1)
            {
                RaceDetail detail = new()
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Description = entity.Description,
                    StrengthModifier = entity.StrengthModifier,
                    ConstitutionModifier = entity.ConstitutionModifier,
                    IntelligenceModifier = entity.IntelligenceModifier,
                    DateCreated = entity.DateCreated
                };
                return detail;
            }
            return null;
        }

        public async Task<IEnumerable<RaceDetail>> GetAllRacesAsync()
        {
            List<RaceDetail> races = await _dbcontext.Races.Select(entity => new RaceDetail
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

        public async Task<RaceDetail?> GetRaceByIdAsync(int raceId)
        {
            RaceEntity? entity = await _dbcontext.Races.FindAsync(raceId);
            return entity is null
                ? null
                : new RaceDetail
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Description = entity.Description,
                    StrengthModifier = entity.StrengthModifier,
                    ConstitutionModifier = entity.ConstitutionModifier,
                    IntelligenceModifier = entity.IntelligenceModifier,
                    DateCreated = entity.DateCreated
                };
        }

        public async Task<bool> UpdateRaceAsync(RaceUpdate request)
        {
            RaceEntity? entity = await _dbcontext.Races.FindAsync(request.Id);

            if (entity is null)
                return false;

            entity.Name = request.Name;
            entity.Description = request.Description;
            entity.StrengthModifier = request.StrengthModifier;
            entity.ConstitutionModifier = request.ConstitutionModifier;
            entity.IntelligenceModifier = request.IntelligenceModifier;

            int numberOfChanges = await _dbcontext.SaveChangesAsync();

            return numberOfChanges == 1;
        }

        public async Task<bool> DeleteRaceAsync(RaceDelete raceId)
        {
            var raceEntity = await _dbcontext.Races.FindAsync(raceId.Id);

            if (raceEntity is null)
                return false;

            _dbcontext.Races.Remove(raceEntity);

            return await _dbcontext.SaveChangesAsync() == 1;
        }
    }
}