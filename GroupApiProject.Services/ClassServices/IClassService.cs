using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupApiProject.Models.Class;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GroupApiProject.Services.ClassServices
{
    public interface IClassService
    {
        Task<ClassListItem?> CreateClassAsync(ClassCreate reqeust);
        Task<IEnumerable<ClassListItem>> GetAllClassesAsync();
        Task<ClassDetail?> GetClassByIdAsync(int classId);
        Task<bool> UpdateClassAsync(ClassUpdate request);
        Task<bool> DeleteClassAsync(int classId);
    }
}