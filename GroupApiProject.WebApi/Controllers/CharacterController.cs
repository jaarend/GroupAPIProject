using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using GroupApiProject.Models.Character;
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

            var registerResult = await _characterService.CreateCharacterAsync(model);
            if(registerResult is not null)
            {
                var TextResponse = "Character is created!";
                return Ok(TextResponse);
            }

            return BadRequest();
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

        [HttpDelete("/api/Character/{ownerId:int}/{characterId:int}")]
        public async Task<IActionResult> DeleteCharacterAsync(int ownerId, int characterId)
        {
            return await _characterService.DeleteCharacterAsync(ownerId,characterId)
                ? Ok($"Character {characterId} was deleted successfully.")
                : BadRequest($"Note {characterId} was not deleted.");
        }
    }
}