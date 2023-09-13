namespace TaskSync.Infrastructure.Interfaces;

public interface ITicketService
{
    Task Create();
    Task Get(string id);
    Task GetAllAsync();
    Task Delete();
    Task Update();
}
