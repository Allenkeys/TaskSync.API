namespace TaskSync.Domain.Entities;

public class Project
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public virtual ICollection<Ticket> Tickets { get; set; }
}
