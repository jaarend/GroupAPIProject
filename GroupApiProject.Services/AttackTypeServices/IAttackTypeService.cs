using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupApiProject.Models.AttackType;

namespace GroupApiProject.Services.AttackTypeServices
{
    public interface IAttackTypeService
    {
        Task<AttackTypeListItem?> CreateAttackTypeAsync(AttackTypeCreate request);
        Task<IEnumerable<AttackTypeListItem>> GetAllAttackTypesAsync();
        Task<AttackTypeDetail?> GetAttackTypeByIdAsync(int attackTypeId);
        Task<bool> UpdateAttackTypeAsync(AttackTypeUpdate request);
        Task<bool> DeleteAttackTypeAsync(int attackTypeId);
    }
}