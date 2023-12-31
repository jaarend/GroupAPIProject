using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using GroupApiProject.Models.Character;
using GroupApiProject.Models.Responses;
using GroupApiProject.Models.Token;
using GroupApiProject.Services.Character;
using Microsoft.AspNetCore.Authorization;
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
        [HttpGet("/api/Character/All/")]
        public async Task<IActionResult> GetAllCharactersNoLoginAsync()
        {
            var characters = await _characterService.GetAllCharactersNoLoginAsync();
            return Ok(characters);
        }

        // [HttpGet("/api/Character/{characterId:int}")]
        // public async Task<IActionResult> GetCharacterById(int characterId)
        // {
        //     var character = await _characterService.GetCharacterByIdAsync(characterId);
        //     return Ok(character);
        // }
        [HttpGet("/api/Character/Find/{characterId:int}")]
        public async Task<IActionResult> GetCharacterByIdNoLoginAsync(int characterId)
        {
            var character = await _characterService.GetCharacterByIdNoLoginAsync(characterId);
            return Ok(character);
        }

        [Authorize]
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

            if(registerCreateResult is not null)
            {
                var TextResponse = $"Your {model.Name} is created!";
                return Ok(new TextResponse (TextResponse));
            }

            return BadRequest(new TextResponse("Unable to create Character."));
        }

        [Authorize]
        [HttpPut("/api/Character/")]
        public async Task<IActionResult> UpdateCharacterByIdAsync ([FromBody] EditCharacter model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var registerResult = await _characterService.UpdateCharacterByIdAsync(model);
            if(registerResult == true)
            {
                var TextResponse = $"{model.Name} has been updated!";
                return Ok(new TextResponse (TextResponse));
            }
            
            return BadRequest();
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteCharacterAsync([FromBody] DeleteCharacter character)
        {
            return await _characterService.DeleteCharacterAsync(character)
                ? Ok(new TextResponse ($"Character ID {character.Id} was deleted successfully."))
                : BadRequest($"Note {character.Id} was not deleted.");
        }
    }
}