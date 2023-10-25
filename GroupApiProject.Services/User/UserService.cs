using System.Runtime.CompilerServices;
using GroupApiProject.Data;
using GroupApiProject.Data.Entities;
using GroupApiProject.Models.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace GroupApiProject.Services.User;

public class UserService : IUserService
{

    private readonly ApplicationDbContext _context;
    private readonly UserManager<UserEntity> _userManager;
    private readonly SignInManager<UserEntity> _signManager;

    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(ApplicationDbContext context,
                        UserManager<UserEntity> userManager,
                        SignInManager<UserEntity> signInManager,
                        IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _userManager = userManager;
        _signManager = signInManager;
        _httpContextAccessor = httpContextAccessor;
        
    }

    public async Task<bool> RegisterUserAsync(RegisterUser model)
    {
        UserEntity entity = new()
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            UserName = model.UserName,
            UserRole = model.UserRole,
            DateCreated = DateTime.Now
        };

        IdentityResult registerResult = await _userManager.CreateAsync(entity, model.Password);

        return registerResult.Succeeded;
    }

    private async Task SetJwtTokenInCookie(string token)
    {
        var options = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
        };

        _httpContextAccessor.HttpContext.Response.Cookies.Append("jwtToken", token, options);
    }

    public async Task<UserDetail?> GetUserByIdAsync(int userId)
    {
        UserEntity? entity = await _context.Users.FindAsync(userId);
        if (entity is null)
            return null;

        UserDetail detail = new()
        {
            Id = entity.Id,
            Email = entity.Email!,
            UserName = entity.UserName!,
            FirstName = entity.FirstName!,
            LastName = entity.LastName,
            DateCreated = entity.DateCreated
        };
        return detail;
    }

}