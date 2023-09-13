namespace TaskSync.Infrastructure.Interfaces;

public interface IUserService
{
    Task Create();
    Task Get(string id);
    Task GetAllAsync();
    Task Delete();
    Task Update();
}
