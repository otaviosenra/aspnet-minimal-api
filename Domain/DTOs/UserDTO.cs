using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
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
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ProfileType Profile { get; set; } = ProfileType.USER; //  "ADMIN", "USER"
}