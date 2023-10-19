using GroupApiProject.Services.Race;
using Microsoft.AspNetCore.Mvc;

namespace GroupApiProject.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RaceController : ControllerBase
    {
        private readonly IRaceService _raceService;
        public RaceController(IRaceService raceService)
        {
            _raceService = raceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRaces()
        {
            var races = await _raceService.GetAllRacesAsync();
            return Ok(races);
        }
    }
}