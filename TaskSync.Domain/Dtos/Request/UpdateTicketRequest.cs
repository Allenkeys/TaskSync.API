using System.ComponentModel.DataAnnotations;

namespace TaskSync.Domain.Dtos.Request;

public class UpdateTicketRequest
{
    [Required]
    public int ProjectId { get; set; }
    [Required]
    public int TicketId { get; set; }
}
