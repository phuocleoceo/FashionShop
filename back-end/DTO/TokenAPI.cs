using System.ComponentModel.DataAnnotations;

namespace back_end.DTO;

public class TokenAPI
{
    [Required]
    public string AccessToken { get; set; }

    [Required]
    public string RefreshToken { get; set; }
}
