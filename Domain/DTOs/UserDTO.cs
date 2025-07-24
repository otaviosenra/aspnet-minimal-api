using System.ComponentModel.DataAnnotations;
using MinimalApi.Domain.Enums;

namespace MinimalApi.Domain.DTOs;

public class UserDTO
{
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string Password { get; set; } = default!;
    
    [Required]
    [StringLength(50)]
    public string Name { get; set; } = default!;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = default!;


    [Required]
    public ProfileType Profile { get; set; } = default!; // e.g., "ADMIN", "USER"
}