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
        [HttpDelete("{attackTypeId:int}")]
        public async Task<IActionResult> DeleteAttackType([FromBody] AttackTypeDelete attackTypeId)
        {
            return await _attackTypeService.DeleteAttackTypeAsync(attackTypeId)
                ? Ok($"Attack Type {attackTypeId} was Deleted.")
                : BadRequest($"Delete Failed.");
        }
    }
}