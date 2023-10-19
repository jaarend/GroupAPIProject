using GroupApiProject.Models.Race;

namespace GroupApiProject.Services.Race
{
    public interface IRaceService
    {
        Task<IEnumerable<RaceRequest>> GetAllRacesAsync();
    }
}