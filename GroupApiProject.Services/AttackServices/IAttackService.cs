using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupApiProject.Models.Attack;

namespace GroupApiProject.Services.AttackServices
{
    public interface IAttackService
    {
        Task<AttackListItem?> CreateAttackAsync(AttackCreate request);
        Task<IEnumerable<AttackListItem>> GetAllAttacksAsync();
        Task<AttackDetail?> GetAttackByIdAsync(int attackId);
        Task<bool> UpdateAttackAsync(AttackUpdate request);
        Task<bool> DeleteAttackAsync(AttackDelete attackId);
    }
}