using System.ComponentModel.DataAnnotations;

namespace back_end.DTO;

public class ProductUpsertDTO
{
    [Required]
    public string Name { get; set; }

    [Required]
    [Range(1, 9999999)]
    public double Price { get; set; }

    public string Description { get; set; }

    [Required]
    public string ImagePath { get; set; }

    [Required]
    public int CategoryId { get; set; }
}
