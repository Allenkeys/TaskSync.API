namespace TaskSync.Domain.Entities;

public class BaseEntity
{
    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime LastUpdated { get; set; }
}
