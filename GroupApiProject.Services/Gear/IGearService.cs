using GroupApiProject.Data.Entities;
using GroupApiProject.Models.Gear;

namespace GroupApiProject.Services.Gear
{
    public interface IGearService
    {
        Task<IEnumerable<GearRequest>> GetGearsAsync();
        Task<bool> CreateGearAsync(GearCreate request);
        Task<GearDetail?> GetGearByIdAsync(int gearId);
        Task<bool> DeleteGearAsync(GearDelete gearId);
        Task<bool> UpdateGearByIdAsync(GearEdit request);
        
    }
}