using System.ComponentModel.DataAnnotations;

namespace TaskSync.Domain.Dtos.Request;

public class GetTicketRequest
{
    [Required]
    public int ProjectId { get; set; }
    [Required]
    public int TicketId { get; set; }
    public DateTime? Date { get; set; }
    public int TicketStatusId { get; set; }
}
