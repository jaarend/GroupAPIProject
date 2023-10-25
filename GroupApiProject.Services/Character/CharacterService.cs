using System.Drawing;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using GroupApiProject.Data;
using GroupApiProject.Data.Entities;
using GroupApiProject.Models.Character;
using GroupApiProject.Models.Gear;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;

namespace GroupApiProject.Services.Character;

public class CharacterService : ICharacterService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly int _userId;

    public CharacterService(UserManager<UserEntity> userManager,
                            SignInManager<UserEntity> signInManager,
                            ApplicationDbContext dbContext)
    {
        var currentUser = signInManager.Context.User;
        var userIdClaim = userManager.GetUserId(currentUser);
        var hasValidClaim = int.TryParse(userIdClaim, out _userId);

        if(hasValidClaim == false)
            throw new Exception("Attempted to build character without an account Id");

        _dbContext = dbContext;
    }

    public async Task<ListCharacter?> CreateCharacterAsync(CreateCharacter request)
    {
        //grabs RaceId to input modifier into the entity
        var raceId = request.RaceId;
        var raceDetail = await _dbContext.Races.FindAsync(raceId);

        //get ArmorId
        var classId = request.ClassId;
        var classDetail = await _dbContext.Classes.FindAsync(classId);
        //get Gear Value
        var gearId = classDetail.ArmorId;
        var gearDetail = await _dbContext.Gear.FindAsync(gearId);

        var armorMod = gearDetail.Value;

        
        CharacterEntity entity = new()
        {
            Name = request.Name,
            Description = request.Description,
            Type = request.Type,
            RaceId = request.RaceId,
            ClassId = request.ClassId,
            Level = request.Level,
            Armor = request.Armor + armorMod,
            Strength = request.Strength + raceDetail.StrengthModifier,
            Constitution = request.Constitution + raceDetail.ConstitutionModifier,
            Intelligence = request.Intelligence + raceDetail.IntelligenceModifier,
            Hp = request.Hp,
            Xp = request.Xp,
            Ap = request.Ap,
            OwnerId = _userId,
            DateCreated = DateTime.Now
        };


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
            Armor = entity.Armor,
            Strength = entity.Strength,
            Constitution = entity.Constitution,
            Intelligence = entity.Intelligence,
            Hp = entity.Hp,
            Xp = entity.Xp,
            Ap = entity.Ap,

        };
        return response;
    }

    public async Task<bool> UpdateRaceStatsOfNewCharacter(int characterId, int raceId)
    {
        var raceDetail = await _dbContext.Races.FindAsync(raceId);

        if (raceDetail is null)
        {
            return false;
        }

        CharacterEntity? entity = await _dbContext.Characters
            .FirstOrDefaultAsync(e =>
                e.Id == characterId && e.RaceId == raceId
            );

        if (entity?.RaceId != raceId)
            return false;

        entity.Strength += raceDetail.StrengthModifier;
        entity.Constitution += raceDetail.ConstitutionModifier;
        entity.Intelligence += raceDetail.IntelligenceModifier;

        int numberOfChanges = await _dbContext.SaveChangesAsync();

        return numberOfChanges == 1;

    }
    public async Task<bool> UpdateArmorStatsOfNewCharacter(int characterId, int classId)
    {
        var classDetail = await _dbContext.Classes.FindAsync(classId);

        if (classDetail is null)
        {
            return false;
        }

        CharacterEntity? entity = await _dbContext.Characters
            .FirstOrDefaultAsync(e =>
                e.Id == characterId && e.ClassId == classId
            );

        if (entity?.ClassId != classId)
            return false;

        //now I want to grab the gear type and value out of gear to calculate armor
        var gearId = classDetail.ArmorId;
        var gearDetail = await _dbContext.Gear.FindAsync(gearId);

        var armorMod = gearDetail.Value;

        // create if to determine what type of gear it is and apply to Armor if it is

        entity.Armor += armorMod;
        

        int numberOfChanges = await _dbContext.SaveChangesAsync();

        return numberOfChanges == 1;

    }


    public async Task<IEnumerable<ListCharacter>> GetAllCharactersAsync()
    {
        List<ListCharacter> characters = await _dbContext.Characters
            .Where(entity => entity.OwnerId == _userId)
            .Select(entity => new ListCharacter
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Type = entity.Type,
                RaceId = entity.RaceId,
                ClassId = entity.ClassId,
                Level = entity.Level,
                Armor = entity.Armor,
                Strength = entity.Strength,
                Constitution = entity.Constitution,
                Intelligence = entity.Intelligence,
                Hp = entity.Hp,
                Xp = entity.Xp,
                Ap = entity.Ap,
                OwnerId = _userId
            })
            .ToListAsync();

        return characters;
    }

    public async Task<ListCharacter?> GetCharacterByIdAsync(int characterId)
    {
        CharacterEntity? entity = await _dbContext.Characters
            .FirstOrDefaultAsync(e =>
                e.Id == characterId && e.OwnerId == _userId
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
            Armor = entity.Armor,
            Strength = entity.Strength,
            Constitution = entity.Constitution,
            Intelligence = entity.Intelligence,
            Hp = entity.Hp,
            Xp = entity.Xp,
            Ap = entity.Ap
        };
    }

    public async Task<bool> UpdateCharacterByIdAsync(EditCharacter request)
    {

        //could use User context later to match _userId with ownerId
        CharacterEntity? entity = await _dbContext.Characters
            .FirstOrDefaultAsync(e =>
                e.Id == request.Id && e.OwnerId == _userId
            );

        if (entity?.OwnerId != request.OwnerId)
            return false;

        entity.Name = request.Name;
        entity.Description = request.Description;
        entity.Type = request.Type;
        entity.ClassId = request.ClassId;

        int numberOfChanges = await _dbContext.SaveChangesAsync();

        return numberOfChanges == 1;


    }

    public async Task<bool> DeleteCharacterAsync(DeleteCharacter character)
    {
        var characterEntity = await _dbContext.Characters.FindAsync(character.Id);

        if (characterEntity?.OwnerId != _userId)
            return false;

        _dbContext.Characters.Remove(characterEntity);
        return await _dbContext.SaveChangesAsync() == 1;
    }


    #region UI/Console Logic
    //!Note this probably needs to go into a separate ProgramUI file
    /*
    public void FightMode(AttackCharacter player, AttackCharacter target) //this would get selected in the UI
    {
        Console.WriteLine("Select your Fighter!");
        Console.WriteLine(GetAllCharactersAsync()); //might want this to call the current userId
        PressAnyKey();
        var playerId = Console.ReadLine();

        Console.WriteLine("Select your Opponent!");
        Console.WriteLine(GetAllCharactersAsync()); //want this to grab all characters besides the player character (could be other characters owned by the player)
        PressAnyKey();
        var targetId = Console.ReadLine();
        


        //* while loop to check if the fight is over?
        var fightCheck = true;

        while (fightCheck)
        {
            var fightCheck = Fight(player, target);
        
        }

    }

    //* might need to create a "fight" method that puts the game into fight state which ends when the player wins or dies.
    //* do while loop that checks if the player or target is alive
    public bool Fight(AttackCharacter player, AttackCharacter target)
    {
        //* player selects their character and the target character
        Console.WriteLine("Select your character by Id: ");
        var selectedPlayerId = Console.ReadLine();
        Console.WriteLine("Select the character you want to fight by Id: ");
        var targetPlayerId = Console.ReadLine();
        player = GetCharacterByIdAsync(selectedPlayerId, _userId); //_userId will be used when sign in is complete
        target = GetCharacterByIdAsync(targetPlayerId,) //might have to create a new method that just uses character Id, not ownerId

        var wonFight = false; //check if the player defeated the target
        var runAway = false; //have a check if the player runs away
        while (CheckIfDead(player) || runAway || wonFight) //checks if the player is alive, runs away, or if the player defeated the target
        {
            
            Console.Write("Would you like to do?\n" +
                            "1. Attack"+
                            "2. Run Away");
            string userInput = Console.ReadLine()!;
            switch (userInput)
            {
                case "1":
                    Attack(player, target);
                break;

                case "2":
                    runAway = RunAway();
                break;
                
                default:
                    Console.WriteLine("Invalid Selection.");
                    break;
            }
        }
    }

*/ 
//!Note above probably needs to go into a separate ProgramUI file

//* Below here are helper methods that could be called into the ProgramUI

    public void Attack(AttackCharacter player, AttackCharacter target)
    {
        int damage = CalculateDamage(player);
        target.Hp = target.Hp - (damage - target.Armor/2); //could play around with how armor affects damage taken

        Console.WriteLine($"{player.Name} attacks {target.Name} for {damage} damage. Your HP: {player.Hp}, Enemy HP: {target.Hp}");
        PressAnyKey();

    }

    public bool RunAway()
    {
        Console.WriteLine("You ran away!");
        PressAnyKey();
        return true;
    }

    private int CalculateDamage(AttackCharacter player)
    {
        // logic for calculating how strong an attack is
        var classId = player.ClassId;
        var classDetail = _dbContext.Classes.Find(classId);

        var gearId = classDetail.WeaponId; //get the weapon id

        var gearDetail = _dbContext.Gear.Find(gearId);
        var weaponMod = gearDetail.Value; // get value

        // create if to determine what type of gear it is and apply to Armor if it is
        //switch statement to determine attack?

        var AttackPower = player.Strength + weaponMod;
        return AttackPower;
    }

    public void TakeDamage(AttackCharacter character, int damage)
    {
        
    }

    public bool CheckIfDead(AttackCharacter character)
    {
        if(character.Hp > 0)
        {
            return false;
        }
        else
            return true;
        
    }

    public void PressAnyKey()
    {
        Console.WriteLine("Press any key to continue.");
        Console.ReadKey();
        Console.Clear();
    }
    #endregion


    
}