using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupApiProject.Models.Character;

namespace GroupApiProject.Services.Character
{
    public interface ICharacterService
    {
        Task<ListCharacter?> CreateCharacterAsync(CreateCharacter request);
        Task<bool> UpdateRaceStatsOfNewCharacter(int characterId, int raceId);
        Task<bool> UpdateArmorStatsOfNewCharacter(int characterId, int classId);
        Task<IEnumerable<ListCharacter>> GetAllCharactersAsync();

        Task<ListCharacter?> GetCharacterByIdAsync(int characterId);
        Task<bool> UpdateCharacterByIdAsync(EditCharacter request);
        Task<bool> DeleteCharacterAsync(DeleteCharacter character);
    }
}