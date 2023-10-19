using GroupApiProject.Models.Race;
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

        [HttpGet("getall")]
        public async Task<IActionResult> GetAllRaces()
        {
            var races = await _raceService.GetAllRacesAsync();
            return races is not null ? Ok(races) : NotFound();
        }

        [HttpGet("{raceId:int}")]
        public async Task<IActionResult> GetRaceById([FromRoute] int raceId)
        {
            RaceRequest request = await _raceService.GetRaceByIdAsync(raceId);
            return request is not null ? Ok(request) : NotFound();
        }
    }
}