using System.ComponentModel.DataAnnotations;

namespace TaskSync.Domain.Dtos.Request;

public class LoginRequest
{
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}
