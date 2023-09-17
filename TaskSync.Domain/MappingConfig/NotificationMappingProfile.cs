using AutoMapper;
using TaskSync.Domain.Dtos.Request;
using TaskSync.Domain.Entities;

namespace TaskSync.Domain.MappingConfig;

public class NotificationMappingProfile : Profile
{
    public NotificationMappingProfile()
    {
        CreateMap<CreateNotificationRequest, Notification>();
    }
}
