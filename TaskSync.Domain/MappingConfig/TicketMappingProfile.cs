using AutoMapper;
using TaskSync.Domain.Dtos.Request;
using TaskSync.Domain.Entities;

namespace TaskSync.Domain.MappingConfig;

public class TicketMappingProfile : Profile
{
    public TicketMappingProfile()
    {
        CreateMap<CreateTicketRequest, Ticket>();
        CreateMap<UpdateTicketRequest, Ticket>();
    }
}
