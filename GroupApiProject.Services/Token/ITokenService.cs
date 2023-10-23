using GroupApiProject.Models.Token;

namespace GroupApiProject.Services.Token
{
    public interface ITokenService
    {
        Task<TokenResponse?> GetTokenAsync(TokenRequest model);
    }
}