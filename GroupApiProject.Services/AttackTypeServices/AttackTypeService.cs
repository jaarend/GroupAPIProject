using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupApiProject.Data;
using GroupApiProject.Data.Entities;
using GroupApiProject.Models.AttackType;
using Microsoft.EntityFrameworkCore;

namespace GroupApiProject.Services.AttackTypeServices
{
    public class AttackTypeService:IAttackTypeService
    {
        private ApplicationDbContext _context;
        public AttackTypeService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<AttackTypeListItem?> CreateAttackTypeAsync(AttackTypeCreate request)
        {
            AttackTypeEntity entity = new()
            {
                Name = request.Name,
                Description = request.Description,
            };
            _context.AttackTypes.Add(entity);
            var numberOfChanges = await _context.SaveChangesAsync();
            if (numberOfChanges != 1)
                return null;

            AttackTypeListItem response = new()
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                DateCreated = DateTime.Now
            };
            return response;
        }
        public async Task<IEnumerable<AttackTypeListItem>> GetAllAttackTypesAsync()
        {
            List<AttackTypeListItem> attackTypes = await _context.AttackTypes
            .Select(entity => new AttackTypeListItem
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                DateCreated = entity.DateCreated
            }).ToListAsync();
            return attackTypes;
        }
        public async Task<AttackTypeDetail?> GetAttackTypeByIdAsync(int attackTypeId)
        {
            AttackTypeEntity? entity = await _context.AttackTypes.FirstOrDefaultAsync(e => e.Id == attackTypeId);

            return entity is null ? null : new AttackTypeDetail
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                DateCreated = entity.DateCreated
            };
        }
        public async Task<bool> UpdateAttackTypeAsync(AttackTypeUpdate request)
        {
            AttackTypeEntity? entity = await _context.AttackTypes.FindAsync(request.Id);

            if (entity == null!)
                return false;

            entity.Name = request.Name;
            entity.Description = request.Description;

            int numberOfChanges = await _context.SaveChangesAsync();
            return numberOfChanges == 1;
        }
        public async Task<bool> DeleteAttackTypeAsync(AttackTypeDelete attackTypeId)
        {
            var attackTypeEntity = await _context.AttackTypes.FindAsync(attackTypeId.Id);
            if (attackTypeEntity == null)
                return false;

            _context.AttackTypes.Remove(attackTypeEntity);
            return await _context.SaveChangesAsync() == 1;
        }
    }
}