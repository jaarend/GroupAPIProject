using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupApiProject.Models.User;

namespace GroupApiProject.Services.User
{
    public interface IUserService
    {
        Task<bool> RegisterUserAsync(RegisterUser model);
    }
}