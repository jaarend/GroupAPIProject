using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupApiProject.Models.Class;
using GroupApiProject.Models.Responses;
using GroupApiProject.Services.ClassServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GroupApiProject.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClassController : ControllerBase
    {
        private readonly IClassService _classService;

        public ClassController(IClassService classService)
        {
            _classService = classService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateClass([FromBody] ClassCreate request)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _classService.CreateClassAsync(request);
            if (response is not null)
                return Ok(response);

            return BadRequest(new TextResponse("Could Not Create Class"));
        }

        [HttpGet("{classId:int}")]
        public async Task<IActionResult> GetClassById([FromRoute] int classId)
        {
            ClassDetail? detail = await _classService.GetClassByIdAsync(classId);

            return detail is not null
                ? Ok(detail)
                : NotFound();
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateClassById([FromBody] ClassUpdate request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return await _classService.UpdateClassAsync(request)
                ? Ok("Class updated.")
                : BadRequest("Request Failed.");
        }

        [Authorize]
        [HttpDelete("{classId:int}")]
        public async Task<IActionResult> DeleteClass([FromBody] ClassDelete classId)
        {
            return await _classService.DeleteClassAsync(classId)
                ? Ok($"Class {classId} was Deleted.")
                : BadRequest($"Delete Failed.");
        }
    }
}