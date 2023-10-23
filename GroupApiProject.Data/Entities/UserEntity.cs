using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using Azure.Identity;
using Microsoft.AspNetCore.Identity;

namespace GroupApiProject.Data.Entities;

public class UserEntity : IdentityUser<int>
{
    [Required]
    [MaxLength(100)]
    public string? FirstName { get; set; }

    [Required]
    [MaxLength(100)]
    public string? LastName { get; set; }

    [ForeignKey(nameof(UserTypeId))]
    public int UserRole { get; set; } 
    public UserTypesEntity? UserTypeId { get; set; }

    [Required]
    public DateTime DateCreated { get; set; }
}