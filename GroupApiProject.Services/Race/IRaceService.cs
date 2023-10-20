using GroupApiProject.Models.Race;

namespace GroupApiProject.Services.Race
{
    public interface IRaceService
    {
        Task<RaceDetail?> CreateRaceAsync(RaceCreate request);
        Task<IEnumerable<RaceDetail>> GetAllRacesAsync();
        Task<RaceDetail> GetRaceByIdAsync(int raceId);
        Task<bool> DeleteRaceAsync(int raceId);
    }
}