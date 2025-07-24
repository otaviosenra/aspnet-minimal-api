using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinimalApi.Domain.Entities;

public class Cheese
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Type { get; set; } = default!;

    [Required]
    public int Quantity { get; set; } = default!;

    [Required]
    public decimal Price { get; set; } = default!;
}
