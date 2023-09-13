using Microsoft.AspNetCore.Identity;

namespace TaskSync.Domain.Entities;

public class ApplicationRole : IdentityRole
{
    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime LastUpdated { get; set; }
}
