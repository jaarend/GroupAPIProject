using Azure.Core;
using GroupApiProject.Data;
using GroupApiProject.Data.Entities;
using GroupApiProject.Models.Gear;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GroupApiProject.Services.Gear;

public class GearService : IGearService
{
    private readonly ApplicationDbContext _dbcontext;
    public GearService(ApplicationDbContext dbContext)
    {
        _dbcontext = dbContext;
    }


    public async Task<IEnumerable<GearRequest>> GetGearsAsync()
    {
        List<GearRequest> gears = await _dbcontext.Gear.Select(entity => new GearRequest
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            DateCreated = entity.DateCreated
        }).ToListAsync();

        return gears;
    }
    public async Task<bool> CreateGearAsync(GearCreate request)
    {
        GearEntity entity = new()
        {
            Name = request.Name,
            Description = request.Description,
            Type = request.Type,
            Value = request.Value,
            DateCreated = DateTime.Now
        };
        _dbcontext.Gear.Add(entity);
        await _dbcontext.SaveChangesAsync();
        return true;
    }
    public async Task<GearDetail?> GetGearByIdAsync(int gearId)
    {
        GearEntity? entity = await _dbcontext.Gear.FindAsync(gearId);
        if (entity == null)
        {
            return null;
        }
        GearDetail gearDetail = new()
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            Type = entity.Type,
            Value = entity.Value,
            DateCreated = entity.DateCreated
        };
        return gearDetail;
    }
    // ========================================================================
    public async Task<bool> UpdateGearByIdAsync(GearEdit request)
    {
        GearEntity? entity = await _dbcontext.Gear.FindAsync(request.Id);

        if (entity is null)
            return false;

        entity.Name = request.Name;
        entity.Description = request.Description;
        entity.Type = request.Type;
        entity.Value = request.Value;

        int numberOfChanges = await _dbcontext.SaveChangesAsync();
        return numberOfChanges == 1;


    }
    public Task<bool> UpdateGearByIdAsync(GearCreate model, object ownerId)
    {
        throw new NotImplementedException();
    }

    // ============================================================================

    public async Task<bool> DeleteGearAsync(GearDelete gearId)
    {
        GearEntity? gearEntity = await _dbcontext.Gear.FindAsync(gearId.Id);
        _dbcontext.Gear.Remove(gearEntity);
        await _dbcontext.SaveChangesAsync();
        return true;
    }

}
