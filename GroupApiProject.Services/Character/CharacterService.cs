using GroupApiProject.Data.Entities;
using GroupApiProject.Models.Character;

namespace GroupApiProject.Services.Character;

public class CharacterService : ICharacterService
{

    public async Task<ListCharacter?> CreateCharacterAsync(CreateCharacter request)
    {
        CharacterEntity entity = new()
        {


        };
    }

}