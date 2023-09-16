using System.ComponentModel.DataAnnotations;
using TaskSync.Domain.Enums;

namespace TaskSync.Domain.Dtos.Request;

public class CreateTicketRequest
{
    [Required]
    public int ProjectId { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public DateTime DueDate { get; set; }
    [Required]
    public Priority Priority { get; set; }
}
