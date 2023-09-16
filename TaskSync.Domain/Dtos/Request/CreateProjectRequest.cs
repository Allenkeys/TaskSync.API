using System.ComponentModel.DataAnnotations;

namespace TaskSync.Domain.Dtos.Request;

public class CreateProjectRequest
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
}
