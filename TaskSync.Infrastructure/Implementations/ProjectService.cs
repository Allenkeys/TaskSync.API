using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TaskSync.Application.Repository;
using TaskSync.Domain.Dtos.Request;
using TaskSync.Domain.Entities;
using TaskSync.Infrastructure.Interfaces;
using TaskSync.Infrastructure.ValidationResponse;

namespace TaskSync.Infrastructure.Implementations;

public class ProjectService : IProjectService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<Project> _projectRepo;
    private readonly UserManager<ApplicationUser> _userManager;
    public ProjectService(IMapper mapper, IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _projectRepo = _unitOfWork.GetRepository<Project>();
        _userManager = userManager;

    }
    public async Task<SuccessResponse> CreateProject(string userId, CreateProjectRequest request)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));
        var user = _userManager.FindByIdAsync(userId);
        if (user == null) throw new ArgumentException("User not found");

        var project = _mapper.Map<Project>(request);
        project.UserId = userId;
        
        var result = _projectRepo.Create(project);

        return new SuccessResponse { Success = true, Data = $"{result.Name} has been created successfully" };

    }

    public async Task DeleteProject(string userId, int projectId)
    {
        var user = _userManager.FindByIdAsync(userId);
        if (user == null) throw new ArgumentException("User not found");

        var existingProject = _projectRepo.FindSingleBy(x => x.Id.Equals(projectId) 
            && x.UserId.Equals(userId), trackChanges: true);

        if (existingProject == null) throw new ArgumentException("Not Found!");

        _projectRepo.Delete(existingProject);
    }

    public async Task<IEnumerable<Project>> GetAllProjectsAsync(string userId)
    {
        var user = _userManager.FindByIdAsync(userId);
        if (user == null) throw new ArgumentException("User not found");

        var existingProjects = _projectRepo.FindBy(x => x.UserId.Equals(userId) 
            && x.UserId.Equals(userId), trackChanges: false);

        if (existingProjects == null)
            return Enumerable.Empty<Project>();

        return existingProjects;
    }

    public async Task<Project> GetProject(string userId, int projectId)
    {
        var user = _userManager.FindByIdAsync(userId);
        if (user == null) throw new ArgumentException("User not found");

        var existingProject = _projectRepo.FindSingleBy(x => x.Id.Equals(projectId) 
            && x.UserId.Equals(userId), trackChanges: false);

        if (existingProject == null) throw new ArgumentException("Not Found!");

        return existingProject;
    }

    public async Task UpdateProject(string userId, int projectId, UpdateProjectRequest request)
    {
        var user = _userManager.FindByIdAsync(userId);
        if (user == null) throw new ArgumentException("User not found");

        var existingProject = _projectRepo.FindSingleBy(x => x.Id.Equals(projectId)
            && x.UserId.Equals(userId), trackChanges: true);

        if (existingProject == null) throw new ArgumentException("Not Found!");

        _mapper.Map(request, existingProject);
        existingProject.LastUpdated = DateTime.Now;
        _projectRepo.Update(existingProject);
    }
}
