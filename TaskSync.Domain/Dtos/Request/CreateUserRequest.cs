using System.ComponentModel.DataAnnotations;
using TaskSync.Domain.Enums;

namespace TaskSync.Domain.Dtos.Request;

public class CreateUserRequest
{
    [Required]
    public string Firstname { get; set; }
    [Required]
    public string Lastname { get; set; }
    [Required]
    public string Username { get; set; }
    public string? Middlename { get; set; }
    [Required]
    public string Password { get; set; }
    [EmailAddress]
    [Required]
    public string Email { get; set; }
    [Required]
    public UserType UserTypeId { get; set; }
}
