using GroupApiProject.Data;
using GroupApiProject.Data.Entities;
using GroupApiProject.Models.Character;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;

namespace GroupApiProject.Services.Character;

public class CharacterService : ICharacterService
{
    private readonly ApplicationDbContext _dbContext;
    // private readonly int _userId; //can add this when we want tokens

    public CharacterService(UserManager<UserEntity> userManager,
                            SignInManager<UserEntity> signInManager,
                            ApplicationDbContext dbContext)
            {
                // var currentUser = signInManager.Context.User;
                // var userIdClaim = userManager.GetUserId(currentUser);
                // var hasValidClaim = int.TryParse(userIdClaim, out _userId);
                
                _dbContext = dbContext;
            }

    public async Task<ListCharacter?> CreateCharacterAsync(CreateCharacter request)
    {
        // var userId = _userId;
        //create the new entity
        CharacterEntity entity = new()
        {
            Name = request.Name,
            Description = request.Description,
            Type = request.Type,
            RaceId = request.RaceId,
            ClassId = request.ClassId,
            Level = request.Level,
            Hp = request.Hp,
            Xp = request.Xp,
            Ap = request.Ap,
            OwnerId = request.OwnerId,
            DateCreated = DateTime.Now
        };

        //update the stats of character based on selected Race and Class



        //return response if successful
        _dbContext.Characters.Add(entity);
        var numberOfChanges = await _dbContext.SaveChangesAsync();

        if (numberOfChanges != 1)
            return null;

        ListCharacter response = new()
        {
            Name = entity.Name,
            Description = entity.Description,
            RaceId = entity.RaceId,
            ClassId = entity.ClassId,
            Level = entity.Level,
            Hp = entity.Hp,
            Xp = entity.Xp,
            Ap = entity.Ap,

        };
        return response;
    }


    public async Task<IEnumerable<ListCharacter>> GetAllCharactersAsync()
    {
        List<ListCharacter> characters = await _dbContext.Characters
            // .Where(entity => entity.OwnerId == _userId) this can be added to specific logged in user
            .Select(entity => new ListCharacter
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Type = entity.Type,
                RaceId = entity.RaceId,
                ClassId = entity.ClassId,
                Level = entity.Level,
                Hp = entity.Hp,
                Xp = entity.Xp,
                Ap = entity.Ap,
                OwnerId = entity.OwnerId
            })
            .ToListAsync();

        return characters;
    }

    public async Task<ListCharacter?> GetCharacterByIdAsync(int characterId, int ownerId)
    {
        CharacterEntity? entity = await _dbContext.Characters
            .FirstOrDefaultAsync(e =>
                e.Id == characterId && e.OwnerId == ownerId
            );

        return entity is null ? null : new ListCharacter
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            Type = entity.Type,
            RaceId = entity.RaceId,
            ClassId = entity.ClassId,
            Level = entity.Level,
            Hp = entity.Hp,
            Xp = entity.Xp,
            Ap = entity.Ap
        };
    }

    public async Task<bool> UpdateCharacterByIdAsync(EditCharacter request, int ownerId)
    {
        
        //could use User context later to match _userId with ownerId
        CharacterEntity? entity = await _dbContext.Characters
            .FirstOrDefaultAsync(e =>
                e.Id == request.Id && e.OwnerId == ownerId
            );
        
        if(entity?.OwnerId != request.OwnerId)
            return false;
        
        entity.Name = request.Name;
        entity.Description = request.Description;
        entity.Type = request.Type;
        entity.ClassId = request.ClassId;

        int numberOfChanges = await _dbContext.SaveChangesAsync();
        
        return numberOfChanges == 1;


    }

}