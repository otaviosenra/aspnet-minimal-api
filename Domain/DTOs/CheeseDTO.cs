using System.ComponentModel.DataAnnotations;

namespace MinimalApi.Domain.DTOs;

public class CheeseDTO
{
    [Required]
    [StringLength(50)]
    public string Type { get; set; } = default!;

    [Required]
    public int Quantity { get; set; } = default!;

    [Required]
    public decimal Price { get; set; } = default!;
}
