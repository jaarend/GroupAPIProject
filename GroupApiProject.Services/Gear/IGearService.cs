using GroupApiProject.Models.Gear;

namespace GroupApiProject.Services.Gear
{
    public interface IGearService
    {
        Task<IEnumerable<GearRequest>> GetGearsAsync();
    }
}