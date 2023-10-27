using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GroupApiProject.Data.Entities;
using GroupApiProject.Models.Token;
using GroupApiProject.Services.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace GroupApiProject.Services.Token
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly UserManager<UserEntity> _userManager;

        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly ILogger<TokenService> _logger;

        public TokenService(IConfiguration configuration, UserManager<UserEntity> userManager, IUserService userService, IHttpContextAccessor httpContextAccessor, ILogger<TokenService> logger)
        {
            _configuration = configuration;
            _userManager = userManager;
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public async Task<TokenResponse?> GetTokenAsync(TokenRequest model)
        {
            UserEntity? entity = await GetValidUserAsync(model);

            if (entity is null)
                return null;

            return await GenerateTokenAsync(entity);
        }

        public async Task<TokenResponse> GenerateTokenAsync(UserEntity entity)
        {
            List<Claim> claims = await GetUserClaimsAsync(entity);
            SecurityTokenDescriptor tokenDescriptor = GetTokenDescriptor(claims);

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            TokenResponse response = new()
            {
                Token = tokenHandler.WriteToken(token),
                IssuedAt = token.ValidFrom,
                Expires = token.ValidTo
            };

            SetJwtTokenInCookie(response.Token);

            return response;
        }

        private async Task<List<Claim>> GetUserClaimsAsync(UserEntity entity)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.NameIdentifier, entity.Id.ToString()),
                new Claim(ClaimTypes.Name, entity.UserName!),
                new Claim(ClaimTypes.Email, entity.Email!)
            };

            return claims;
        }

        private SecurityTokenDescriptor GetTokenDescriptor(List<Claim> claims)
        {
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!);
            var secret = new SymmetricSecurityKey(key);
            var signingCredentials = new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);

            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Issuer = _configuration["Jwt:Issuer"]!,
                Audience = _configuration["Jwt:Audience"]!,
                Subject = new ClaimsIdentity(claims),
                IssuedAt = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddDays(14),
                SigningCredentials = signingCredentials
            };
            return tokenDescriptor;
        }

        private async Task<UserEntity?> GetValidUserAsync(TokenRequest model)
        {
            var userEntity = await _userManager.FindByNameAsync(model.UserName);

            if (userEntity is null)
                {_logger.LogWarning("User not found for username: {model.UserName}", model.UserName);
                return null;}


            var isValidPassword = await _userManager.CheckPasswordAsync(userEntity, model.Password);
            if (isValidPassword == false)
                {_logger.LogWarning("Password not found for username: {model.UserName}", model.UserName);
                return null;}

            return userEntity;
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
    }
}