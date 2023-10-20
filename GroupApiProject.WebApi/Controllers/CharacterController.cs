using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using GroupApiProject.Models.Character;
using GroupApiProject.Models.Responses;
using GroupApiProject.Services.Character;
using Microsoft.AspNetCore.Mvc;

namespace GroupApiProject.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;
        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCharacters()
        {
            var characters = await _characterService.GetAllCharactersAsync();
            return Ok(characters);
        }

        [HttpGet("/api/Character/{ownerId:int}/{characterId:int}")]
        public async Task<IActionResult> GetCharacterById(int ownerId, int characterId)
        {
            var character = await _characterService.GetCharacterByIdAsync(characterId, ownerId);
            return Ok(character);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCharacter ([FromBody] CreateCharacter model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var registerCreateResult = await _characterService.CreateCharacterAsync(model);

            if(registerCreateResult is null)
            {
                return BadRequest(new TextResponse("Could not initizalize character."));
            }

            //else continue to grab raceId, then apply race modifiers to new character
            // var raceId = model.RaceId;
            // var registerFinalResult = await _characterService.UpdateRaceStatsOfNewCharacter(registerCreateResult, raceId);
            //if(registerFinalResult)
            if(registerCreateResult is not null)
            {
                var TextResponse = "Character is created!";
                return Ok(TextResponse);
            }

            return BadRequest(new TextResponse("Unable to create Character."));
        }

        [HttpPut("/api/Character/{ownerId:int}")]
        public async Task<IActionResult> UpdateCharacterByIdAsync ([FromBody] EditCharacter model, int ownerId)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var registerResult = await _characterService.UpdateCharacterByIdAsync(model, ownerId);
            if(registerResult == true)
            {
                var TextResponse = "Character has been updated!";
                return Ok(TextResponse);
            }
            
            return BadRequest();
        }

        [HttpPut("/api/UpdateCharacterRace/{characterId:int}/{raceId:int}")]
        public async Task<bool> UpdateRaceStatsOfNewCharacter(int characterId, int raceId)
        {
            var registerFinalResult = await _characterService.UpdateRaceStatsOfNewCharacter(characterId, raceId);
            if(registerFinalResult == true)
            {
                return true;
            }
            return false;
        }
        [HttpPut("/api/UpdateCharacterArmor/{characterId:int}/{classId:int}")]
        public async Task<bool> UpdateArmorStatsOfNewCharacter(int characterId, int classId)
        {
            var registerFinalResult = await _characterService.UpdateArmorStatsOfNewCharacter(characterId, classId);
            if(registerFinalResult == true)
            {
                return true;
            }
            return false;
        }

        [HttpDelete("/api/Character/{ownerId:int}/{characterId:int}")]
        public async Task<IActionResult> DeleteCharacterAsync(int ownerId, int characterId)
        {
            return await _characterService.DeleteCharacterAsync(ownerId,characterId)
                ? Ok($"Character {characterId} was deleted successfully.")
                : BadRequest($"Note {characterId} was not deleted.");
        }
    }
}