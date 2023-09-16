using TaskSync.Domain.Dtos.Request;
using TaskSync.Domain.Entities;
using TaskSync.Infrastructure.ValidationResponse;

namespace TaskSync.Infrastructure.Interfaces;

public interface ITicketService
{
    Task<SuccessResponse> CreateTicket(string userId, CreateTicketRequest request);
    Task<Ticket> GetTicket(string userId, UpdateTicketRequest request);
    Task<IEnumerable<Ticket>> GetAllProjectTicketsAsync(string userId, int projectId);
    Task DeleteTicket(string userId, UpdateTicketRequest request);
    Task UpdateTicket(string userId, UpdateTicketRequest request);
    Task ToggleTicketStatus(string userId, UpdateTicketRequest request);
    Task ToggleTickectPriority(string userId, UpdateTicketRequest request);
}
