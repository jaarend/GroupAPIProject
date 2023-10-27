using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using GroupApiProject.Data;
using GroupApiProject.Data.Entities;
using GroupApiProject.Models.Attack;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GroupApiProject.Services.AttackServices
{
    public class AttackService : IAttackService
    {
        private ApplicationDbContext _context;
        public AttackService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<AttackListItem?> CreateAttackAsync(AttackCreate request)
        {
            AttackEntity entity = new()
            {
                Name = request.Name,
                Description = request.Description,
                Type = request.Type,
                HitValue = request.HitValue,
                APCost = request.APCost,
                DateCreated = DateTime.Now
            };
            _context.Attacks.Add(entity);
            var numberOfChanges = await _context.SaveChangesAsync();
            if (numberOfChanges != 1)
                return null;

            AttackListItem response = new()
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Type = entity.Type,
                HitValue = entity.HitValue,
                APCost = entity.APCost,
                DateCreated = entity.DateCreated
            };
            return response;
        }
        public async Task<IEnumerable<AttackListItem>> GetAllAttacksAsync()
        {
            List<AttackListItem> attacks = await _context.Attacks
            .Select(entity => new AttackListItem
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Type = entity.Type,
                HitValue = entity.HitValue,
                APCost = entity.APCost,
                DateCreated = entity.DateCreated
            }).ToListAsync();
            return attacks;
        }
        public async Task<AttackDetail?> GetAttackByIdAsync(int attackId)
        {
            AttackEntity? entity = await _context.Attacks.FirstOrDefaultAsync(e => e.Id == attackId);

            return entity is null ? null : new AttackDetail
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                DateCreated = entity.DateCreated
            };
        }
        public async Task<bool> UpdateAttackAsync(AttackUpdate request)
        {
            AttackEntity? entity = await _context.Attacks.FindAsync(request.Id);

            if (entity == null!)
                return false;

            entity.Name = request.Name;
            entity.Description = request.Description;
            entity.Type = request.Type;
            entity.HitValue = request.HitValue;
            entity.APCost = request.APCost;

            int numberOfChanges = await _context.SaveChangesAsync();
            return numberOfChanges == 1;
        }
        public async Task<bool> DeleteAttackAsync(AttackDelete attackId)
        {
            var attackEntity = await _context.Attacks.FindAsync(attackId.Id);
            if (attackEntity == null)
                return false;

            _context.Attacks.Remove(attackEntity);
            return await _context.SaveChangesAsync() == 1;
        }
    }
}