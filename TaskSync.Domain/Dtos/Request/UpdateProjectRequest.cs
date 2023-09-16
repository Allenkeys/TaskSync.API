using System.ComponentModel.DataAnnotations;

namespace TaskSync.Domain.Dtos.Request;

public class UpdateProjectRequest
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
}
