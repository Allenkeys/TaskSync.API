using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskSync.Domain.Entities;

namespace TaskSync.Application.Context;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }

    public virtual DbSet<Project> Projects { get; set; }
    public virtual DbSet<Ticket> Tickets { get; set; }
    public virtual DbSet<Notification> Notifications { get; set; }
}
