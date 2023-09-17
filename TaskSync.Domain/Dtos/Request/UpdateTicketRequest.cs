using System.ComponentModel.DataAnnotations;

namespace TaskSync.Domain.Dtos.Request;

public class UpdateTicketRequest
{
    [Required]
    public int ProjectId { get; set; }
    [Required]
    public int TicketId { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public DateTime DueDate { get; set; }
}

public class TogglePriorityRequest
{
    [Required]
    public int ProjectId { get; set; }
    [Required]
    public int TicketId { get; set; }
    [Required]
    public int PriorityId { get; set; }
}

public class ToggleStatusRequest
{
    [Required]
    public int ProjectId { get; set; }
    [Required]
    public int TicketId { get; set; }
    [Required]
    public int StatusId { get; set; }
}
