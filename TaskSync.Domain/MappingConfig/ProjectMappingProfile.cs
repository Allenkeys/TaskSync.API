using AutoMapper;
using TaskSync.Domain.Dtos.Request;
using TaskSync.Domain.Entities;

namespace TaskSync.Domain.MappingConfig;

public class ProjectMappingProfile : Profile
{
    public ProjectMappingProfile()
    {
        CreateMap<UpdateProjectRequest, Project>();
    }
}
