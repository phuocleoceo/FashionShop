using System.ComponentModel.DataAnnotations;

namespace back_end.DTO;

public class UserForAuthenticationDTO
{
    [Required]
    public string UserName { get; set; }

    [Required]
    public string Password { get; set; }
}
