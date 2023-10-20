using System.Runtime.CompilerServices;
using GroupApiProject.Data;
using GroupApiProject.Data.Entities;
using GroupApiProject.Models.User;
using Microsoft.AspNetCore.Identity;

namespace GroupApiProject.Services.User;

public class UserService : IUserService
{

    private readonly ApplicationDbContext _context;

    private readonly UserManager<UserEntity> _userManager;

    private readonly SignInManager<UserEntity> _signManager;

    public UserService(ApplicationDbContext context,
                        UserManager<UserEntity> userManager,
                        SignInManager<UserEntity> signInManager)
    {
        _context = context;
        _userManager = userManager;
        _signManager = signInManager;
    }

    public async Task<bool> RegisterUserAsync(RegisterUser model)
    {
        UserEntity entity = new()
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            UserName = model.UserName,
            DateCreated = DateTime.Now
        };

        IdentityResult registerResult = await _userManager.CreateAsync(entity, model.Password);

        return registerResult.Succeeded;
    }

}