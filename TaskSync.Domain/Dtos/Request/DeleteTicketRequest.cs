namespace TaskSync.Domain.Dtos.Request;

public class DeleteTicketRequest
{
    public int ProjectId { get; set; }
    public int TicketId { get; set; }
}
