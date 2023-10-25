using GroupApiProject.Models.Gear;
using GroupApiProject.Models.Responses;
using GroupApiProject.Services.Gear;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GroupApiProject.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GearController : ControllerBase
{
    private readonly IGearService _gearService;


    public GearController(IGearService gearService)
    {
        _gearService = gearService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllGear()
    {
        var gear = await _gearService.GetGearsAsync();
        return Ok(gear);
    }

    [HttpGet("{gearId:int}")]
    public async Task<IActionResult> GetGearByAsyncId([FromRoute] int gearId)
    {
        var gear = await _gearService.GetGearByIdAsync(gearId);
        return Ok(gear);
    }
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> GearCreate([FromBody] GearCreate model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var registerResult = await _gearService.CreateGearAsync(model);
        {
            var TextResponse = "Gear is created!";
            return Ok(TextResponse);
        }

    }
    [Authorize]
    [HttpPut]
    public async Task<IActionResult> UpdateGearByIdAsync([FromBody] GearEdit request)
    {
        if (ModelState.IsValid)
        {
            return await _gearService.UpdateGearByIdAsync(request)
            ? Ok("Gear has been updated.")
            : BadRequest("Gear could not be updated. :()");
        }
        return BadRequest(ModelState);

    }


    [Authorize]
    [HttpDelete]
    public async Task<IActionResult> DeleteGearAsync([FromBody] GearDelete gearId)
    {
        return await _gearService.DeleteGearAsync(gearId)
        ? Ok($"Gear {gearId} was deleted successfully.")
        : BadRequest($"Note {gearId} was not deleted.");
    }
}


