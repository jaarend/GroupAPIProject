using System.ComponentModel.DataAnnotations;

namespace GroupApiProject.Models.User;

public class RegisterUser
{
    [Required]
    [MaxLength(100)]
    public string? FirstName { get; set; }

    [Required]
    [MaxLength(100)]
    public string? LastName { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MinLength(4)]
    public string UserName { get; set; } = string.Empty;

    public int UserRole {get; set;}

    [Required]
    [MinLength(4)]
    public string Password { get; set; } = string.Empty;

    [Compare(nameof(Password))]
    public string ConfirmPassword { get; set; } = string.Empty;

}