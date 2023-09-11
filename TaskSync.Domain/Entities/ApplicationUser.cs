namespace TaskSync.Domain.Entities
{
    public class ApplicationUser
    {
        public string Firstname { get; set; }
        public string? Middlename { get; set; }
        public string Lastname { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime LastUpdated { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
    }
}
