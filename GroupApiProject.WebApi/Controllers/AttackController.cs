using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupApiProject.Models.Attack;
using GroupApiProject.Models.Responses;
using GroupApiProject.Services.AttackServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GroupApiProject.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttackController : ControllerBase
    {
        private readonly IAttackService _attackService;
        public AttackController(IAttackService attackService)
        {
            _attackService = attackService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateAttack([FromBody] AttackCreate request)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _attackService.CreateAttackAsync(request);
            if (response is not null)
                return Ok(response);

            return BadRequest(new TextResponse("Could Not Create Attack"));
        }
        [HttpGet("{attackId:int}")]
        public async Task<IActionResult> GetAttackById([FromRoute] int attackId)
        {
            AttackDetail? detail = await _attackService.GetAttackByIdAsync(attackId);

            return detail is not null
                ? Ok(detail)
                : NotFound();
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateAttackById([FromBody] AttackUpdate request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return await _attackService.UpdateAttackAsync(request)
                ? Ok("Attack updated.")
                : BadRequest("Request Failed.");
        }

        [Authorize]
        [HttpDelete("{attackId:int}")]
        public async Task<IActionResult> DeleteAttack([FromBody] AttackDelete attackId)
        {
            return await _attackService.DeleteAttackAsync(attackId)
                ? Ok($"Attack {attackId} was Deleted.")
                : BadRequest($"Delete Failed.");
        }
    }
}