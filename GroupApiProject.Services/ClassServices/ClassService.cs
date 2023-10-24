using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using GroupApiProject.Data;
using GroupApiProject.Data.Entities;
using GroupApiProject.Models.Class;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GroupApiProject.Services.ClassServices
{
    public class ClassService : IClassService
    {
       private ApplicationDbContext _context;
       public ClassService(ApplicationDbContext context)
       {
        _context = context;
       } 
       public async Task<ClassListItem?> CreateClassAsync(ClassCreate request)
       {
        ClassEntity entity = new()
        {
            Name = request.Name,
            Description = request.Description,
            AttackSlot_1 = request.AttackSlot_1,
            AttackSlot_2 = request.AttackSlot_2,
            WeaponId = request.WeaponId,
            ArmorId = request.ArmorId,
            DateCreated = DateTime.Now
        };

        _context.Classes.Add(entity);
        var numberOfChanges = await _context.SaveChangesAsync();

        if (numberOfChanges != 1)
            return null;
        
        ClassListItem response = new()
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            AttackSlot_1 = entity.AttackSlot_1,
            AttackSlot_2 = entity.AttackSlot_2,
            WeaponId = entity.WeaponId,
            ArmorId = entity.ArmorId,
            DateCreated = entity.DateCreated
        };
        return response;
       }
       public async Task<IEnumerable<ClassListItem>> GetAllClassesAsync()
       {
        List<ClassListItem> classes = await _context.Classes
//        .Where(entity => entity.OwnerId == _userId)
        .Select(entity => new ClassListItem
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            WeaponId = entity.WeaponId,
            ArmorId = entity.ArmorId,
            AttackSlot_1 = entity.AttackSlot_1,
            AttackSlot_2 = entity.AttackSlot_2,
            DateCreated = entity.DateCreated
        }).ToListAsync();

        return classes;
       }

        public async Task<ClassDetail?> GetClassByIdAsync(int classId)
        {
            ClassEntity? entity = await _context.Classes.FirstOrDefaultAsync(e => e.Id == classId 
            //&& e.OwnerId == _userId
            );

            return entity is null? null : new ClassDetail
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                DateCreated = entity.DateCreated,
            };
        }
        public async Task<bool> UpdateClassAsync(ClassUpdate request)
        {
            ClassEntity? entity = await _context.Classes.FindAsync(request.Id);

            if (entity == null!)
            return false;

            entity.Name = request.Name;
            entity.Description = request.Description;
            entity.AttackSlot_1 = request.AttackSlot_1;
            entity.AttackSlot_2 = request.AttackSlot_2;
            entity.ArmorId = request.ArmorId;
            entity.WeaponId = request.WeaponId;

            int numberOfChanges = await _context.SaveChangesAsync();
            return numberOfChanges == 1;
        }

        public async Task<bool> DeleteClassAsync(int classId)
        {
            var classEntity = await _context.Classes.FindAsync(classId);
            if (classEntity == null)
            return false;

            _context.Classes.Remove(classEntity);
            return await _context.SaveChangesAsync()==1;
        }
    }
}