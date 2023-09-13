using AutoMapper;
using TaskSync.Domain.Dtos.Request;
using TaskSync.Domain.Entities;

namespace TaskSync.Domain.MappingConfig;

public class UserMappingProfile : Profile
{
    UserMappingProfile()
    {
        CreateMap<CreateUserRequest, ApplicationUser>();
    }
}
