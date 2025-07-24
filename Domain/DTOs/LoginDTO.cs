using System.ComponentModel.DataAnnotations;

namespace MinimalApi.Domain.DTOs;

    public class LoginDTO
    {
        [Required]
        public string Username { get; set; } = default!;

        [Required]
        [StringLength(50, MinimumLength = 3)]
        [MinLength(3)]
        public string Password { get; set; } = default!;
    }