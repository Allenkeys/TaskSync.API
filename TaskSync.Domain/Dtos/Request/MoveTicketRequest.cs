namespace TaskSync.Domain.Dtos.Request;

public class MoveTicketRequest
{
    public int CurrentProjectId { get; set; }
    public int TargetProjectId { get; set; }
    public int TicketId { get; set;}
}
