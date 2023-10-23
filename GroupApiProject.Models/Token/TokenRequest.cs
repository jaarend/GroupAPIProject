using System.ComponentModel.DataAnnotations;

namespace GroupApiProject.Models.Token
{
    public class TokenRequest
    {
        [Required]
        public string UserName { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}