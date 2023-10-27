using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupApiProject.Models.AttackType;
using GroupApiProject.Models.Responses;
using GroupApiProject.Services.AttackTypeServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GroupApiProject.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttackTypeController : ControllerBase
    {
        private readonly IAttackTypeService _attackTypeService;
        public AttackTypeController(IAttackTypeService attackTypeService)
        {
            _attackTypeService = attackTypeService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateAttackType([FromBody] AttackTypeCreate request)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _attackTypeService.CreateAttackTypeAsync(request);
            if (response is not null)
                return Ok(response);

            return BadRequest(new TextResponse("Could Not Create Attack Type"));
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAttackTypes()
        {
            var attackTypes = await _attackTypeService.GetAllAttackTypesAsync();
            return attackTypes is not null ? Ok(attackTypes) : NotFound();
        }

        [HttpGet("{attackTypeId:int}")]
        public async Task<IActionResult> GetAttackTypeById([FromRoute] int attackTypeId)
        {
            AttackTypeDetail? detail = await _attackTypeService.GetAttackTypeByIdAsync(attackTypeId);

            return detail is not null
                ? Ok(detail)
                : NotFound();
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateAttackTypeById([FromBody] AttackTypeUpdate request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return await _attackTypeService.UpdateAttackTypeAsync(request)
                ? Ok("Attack Type updated.")
                : BadRequest("Request Failed.");
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteAttackTypeAsync([FromBody] AttackTypeDelete attackType)
        {
            return await _attackTypeService.DeleteAttackTypeAsync(attackType)
                ? Ok($"Attack Type {attackType} was Deleted.")
                : BadRequest($"Delete Failed.");
        }
    }
}