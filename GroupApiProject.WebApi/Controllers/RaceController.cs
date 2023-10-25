using GroupApiProject.Models.Race;
using GroupApiProject.Models.Responses;
using GroupApiProject.Services.Race;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateRace([FromBody] RaceCreate request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
                
            var response = await _raceService.CreateRaceAsync(request);
            if (response is not null)
                return Ok(response);

            return BadRequest(new TextResponse("Could not create Race."));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRaces()
        {
            var races = await _raceService.GetAllRacesAsync();
            return races is not null ? Ok(races) : NotFound();
        }

        [HttpGet("{raceId:int}")]
        public async Task<IActionResult> GetRaceById([FromRoute] int raceId)
        {
            RaceDetail? request = await _raceService.GetRaceByIdAsync(raceId);
            return request is not null ? Ok(request) : NotFound();
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateRaceById([FromBody] RaceUpdate request)
        {
            if (ModelState.IsValid)
            {
                return await _raceService.UpdateRaceAsync(request)
                    ? Ok(new TextResponse("Race updated successfully."))
                    : BadRequest(new TextResponse("Race could not be updated."));
            }
            return BadRequest(ModelState);
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteRace([FromBody] RaceDelete raceId)
        {
            return await _raceService.DeleteRaceAsync(raceId)
                ? Ok(new TextResponse("Race was deleted successfully."))
                : BadRequest(new TextResponse("Race could not be deleted."));
        }
    }
}