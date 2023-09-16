namespace TaskSync.Infrastructure.Interfaces;

public interface ITicketService
{
    Task CreateProject();
    Task GetProject(string id);
    Task GetAllProjectsAsync();
    Task DeleteProject(string id);
    Task UpdateProject(string id);
}
