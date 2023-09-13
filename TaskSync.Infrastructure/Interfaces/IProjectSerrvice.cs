namespace TaskSync.Infrastructure.Interfaces;

public interface IProjectSerrvice
{
    Task Create();
    Task Get(string id);
    Task GetAllAsync();
    Task Delete();
    Task Update();
}
