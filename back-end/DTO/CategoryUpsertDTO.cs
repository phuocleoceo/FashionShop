using System.ComponentModel.DataAnnotations;

namespace back_end.DTO;

public class CategoryUpsertDTO
{
    [Required]
    public string Name { get; set; }
}
