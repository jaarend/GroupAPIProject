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
        Task<IEnumerable<ListCharacter>> GetAllCharactersAsync();

        Task<ListCharacter?> GetCharacterByIdAsync(int characterId, int ownerId);
    }
}