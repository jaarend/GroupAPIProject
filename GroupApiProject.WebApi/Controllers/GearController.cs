using GroupApiProject.Services.Gear;
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
}