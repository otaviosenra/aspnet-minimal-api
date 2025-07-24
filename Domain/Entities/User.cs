using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MinimalApi.Domain.Enums;

namespace MinimalApi.Domain.Entities;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Name { get; set; } = default!;

    [EmailAddress]
    [Required]
    public string Email { get; set; } = default!;

    [StringLength(50, MinimumLength = 3)]
    public string Password { get; set; } = default!;

    [StringLength(10)]
    public string Profile { get; set; } = ProfileType.USER.ToString(); //  "ADMIN", "USER"
}
