using TaskSync.Domain.Dtos.Request;
using TaskSync.Domain.Entities;
using TaskSync.Infrastructure.ValidationResponse;

namespace TaskSync.Infrastructure.Interfaces;

public interface ITicketService
{
    Task<SuccessResponse> CreateTicket(string userId, CreateTicketRequest request);
    Task<Ticket> GetTicket(string userId, GetTicketRequest request);
    Task<IEnumerable<Ticket>> GetTicketByDueDate(string userId, int projectId, DateTime date);
    Task<IEnumerable<Ticket>> GetTicketByStatus(string userId, int projectId, int statusId);
    Task<IEnumerable<Ticket>> GetAllProjectTicketsAsync(string userId, int projectId);
    Task DeleteTicket(string userId, DeleteTicketRequest request);
    Task UpdateTicket(string userId, UpdateTicketRequest request);
    Task ToggleTicketStatus(string userId, ToggleStatusRequest request);
    Task ToggleTickectPriority(string userId, TogglePriorityRequest request);
    Task MovedTicket(string userId, MoveTicketRequest request);
}
