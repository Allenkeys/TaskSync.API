using TaskSync.Domain.Dtos.Request;
using TaskSync.Domain.Entities;

namespace TaskSync.Infrastructure.Interfaces;

public interface IProjectService
{
    Task<string> CreateProject(string userId, CreateProjectRequest request);
    Task<Project> GetProject(string userId, int id);
    Task<IEnumerable<Project>> GetAllProjectsAsync(string userId);
    Task DeleteProject(string userId, int projectId);
    Task UpdateProject(string userId, int projectId, UpdateProjectRequest request);
}
