using TaskSync.Domain.Enums;

namespace TaskSync.Domain.Entities;

public class Ticket : BaseEntity
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public Priority Priority { get; set; } = Priority.Low;
    public Status Status { get; set; } = Status.Pending;
    public Project Project { get; set; }
}
