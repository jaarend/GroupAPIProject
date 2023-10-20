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

        [HttpPost("create")]
        public async Task<IActionResult> CreateRace([FromBody] RaceCreate request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
                
            var response = await _raceService.CreateRaceAsync(request);
            if (response is not null)
                return Ok(response);

            return BadRequest();
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAllRaces()
        {
            var races = await _raceService.GetAllRacesAsync();
            return races is not null ? Ok(races) : NotFound();
        }

        [HttpGet("get/{raceId:int}")]
        public async Task<IActionResult> GetRaceById([FromRoute] int raceId)
        {
            RaceDetail? request = await _raceService.GetRaceByIdAsync(raceId);
            return request is not null ? Ok(request) : NotFound();
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateRaceById([FromBody] RaceUpdate request)
        {
            if (ModelState.IsValid)
            {
                return await _raceService.UpdateRaceAsync(request)
                    ? Ok("Race updated successfully.")
                    : BadRequest("Race could not be updated.");
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteRace([FromBody] RaceDelete raceId)
        {
            return await _raceService.DeleteRaceAsync(raceId)
                ? Ok($"Race Id: {raceId} was deleted successfully.")
                : BadRequest($"Note Id: {raceId} could not be deleted.");
        }
    }
}