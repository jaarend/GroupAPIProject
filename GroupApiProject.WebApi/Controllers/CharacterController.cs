using System;
using System.Collections.Generic;
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
    }
}