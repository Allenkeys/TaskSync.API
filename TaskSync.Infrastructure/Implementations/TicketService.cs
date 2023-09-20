using System;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TaskSync.Application.Repository;
using TaskSync.Domain.Dtos.Request;
using TaskSync.Domain.Entities;
using TaskSync.Domain.Enums;
using TaskSync.Infrastructure.CustomExceptions;
using TaskSync.Infrastructure.Interfaces;
using TaskSync.Infrastructure.ValidationResponse;

namespace TaskSync.Infrastructure.Implementations;

public class TicketService : ITicketService
{
    private readonly IProjectService _projectService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<Ticket> _ticketRepo;
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;

    public TicketService(IProjectService project, IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager)
    {
        _unitOfWork = unitOfWork;
        _projectService = project;
        _ticketRepo = _unitOfWork.GetRepository<Ticket>();
        _mapper = mapper;
        _userManager = userManager;
    }
    public async Task<SuccessResponse> CreateTicket(string userId, CreateTicketRequest request)
    {
        var user = _userManager.FindByIdAsync(userId) ?? throw new UserNotFoundException(userId);
        var project = _projectService.GetProject(userId, request.ProjectId) ?? throw new ArgumentException("Project does not exist");

        var ticket = _mapper.Map<Ticket>(request);
        _ticketRepo.Create(ticket);

        return new SuccessResponse { Success = true, Data = "Created" };
    }

    public async Task DeleteTicket(string userId, DeleteTicketRequest request)
    {
        var user = _userManager.FindByIdAsync(userId) ?? throw new UserNotFoundException(userId);
        var project = _projectService.GetProject(userId, request.ProjectId) ?? throw new ArgumentException("Project does not exist");

        var ticket = _ticketRepo.FindSingleBy(t =>
            t.ProjectId.Equals(request.ProjectId)
            && t.Id.Equals(request.TicketId),
            trackChanges: true);

        _ticketRepo.Delete(ticket);
    }

    public async Task<IEnumerable<Ticket>> GetAllProjectTicketsAsync(string userId, GetTicketRequest request)
    {
        var user = _userManager.FindByIdAsync(userId) ?? throw new UserNotFoundException(userId);
        var project = _projectService.GetProject(userId, request.ProjectId) ?? throw new ArgumentException("Project does not exist");

        var tickets = _ticketRepo.FindBy(t => t.ProjectId.Equals(request.ProjectId), trackChanges: false);
        if (tickets == null) return Enumerable.Empty<Ticket>();

        return tickets;
    }

    public async Task<Ticket> GetTicket(string userId, GetTicketRequest request)
    {
        var user = _userManager.FindByIdAsync(userId) ?? throw new UserNotFoundException(userId);
        var project = _projectService.GetProject(userId, request.ProjectId) ?? throw new ArgumentException("Project does not exist");

        var ticket = _ticketRepo.FindSingleBy(t => 
            t.ProjectId.Equals(request.ProjectId) 
            && t.Id.Equals(request.TicketId), 
            trackChanges: false) ?? throw new ArgumentException("Ticket not found");
        return ticket;
    }

    public async Task<IEnumerable<Ticket>> GetTicketByDate(string userId, GetTicketRequest request)
    {
        var user = _userManager.FindByIdAsync(userId) ?? throw new UserNotFoundException(userId);
        var project = _projectService.GetProject(userId, request.ProjectId) ?? throw new ArgumentException("Project does not exist");

        var tickets = _ticketRepo.FindBy(t => t.ProjectId.Equals(request.ProjectId) 
        && t.Created.Equals(request.Date), 
        trackChanges: false);

        if (tickets == null) return Enumerable.Empty<Ticket>();

        return tickets.OrderBy(t => t.Created);
    }

    public async Task<IEnumerable<Ticket>> GetTicketByStatus(string userId, GetTicketRequest request)
    {
        var user = _userManager.FindByIdAsync(userId) ?? throw new UserNotFoundException(userId);
        var project = _projectService.GetProject(userId, request.ProjectId) ?? throw new ArgumentException("Project does not exist");

        var tickets = _ticketRepo.FindBy(t => t.ProjectId.Equals(request.ProjectId)
        && t.Status.Equals(request.TicketStatusId),
        trackChanges: false);

        if (tickets == null) return Enumerable.Empty<Ticket>();

        return tickets;
    }

    public async Task<IEnumerable<Ticket>> GetTicketDueInCurrentWeek(string userId, GetTicketRequest request)
    {
        var user = _userManager.FindByIdAsync(userId) ?? throw new UserNotFoundException(userId);
        var project = _projectService.GetProject(userId, request.ProjectId) ?? throw new ArgumentException("Project does not exist");

        var tickets = _ticketRepo.FindBy(t => t.ProjectId.Equals(request.ProjectId)
        && t.DueDate >= t.DueDate && t.DueDate <= DateTime.Now.AddDays(7),
        trackChanges: false);

        if (tickets == null) return Enumerable.Empty<Ticket>();

        return tickets.OrderBy(t => t.DueDate);
    }

    public async Task MovedTicket(string userId, MoveTicketRequest request)
    {
        var user = _userManager.FindByIdAsync(userId) ?? throw new UserNotFoundException(userId);
        var project = _projectService.GetProject(userId, request.CurrentProjectId) ?? throw new ArgumentException("Project does not exist");

        var ticket = _ticketRepo.FindSingleBy(t =>
            t.ProjectId.Equals(request.CurrentProjectId)
            && t.Id.Equals(request.TicketId),
            trackChanges: true);

        ticket.ProjectId = request.TargetProjectId;
        _ticketRepo.Update(ticket);
    }

    public async Task ToggleTickectPriority(string userId, TogglePriorityRequest request)
    {
        var user = _userManager.FindByIdAsync(userId) ?? throw new UserNotFoundException(userId);
        var project = _projectService.GetProject(userId, request.ProjectId) ?? throw new ArgumentException("Project does not exist");

        var ticket = _ticketRepo.FindSingleBy(t =>
            t.ProjectId.Equals(request.ProjectId)
            && t.Id.Equals(request.TicketId),
            trackChanges: true);

        ticket.Priority = (Priority)request.PriorityId;
        _ticketRepo.Update(ticket);
    }

    public async Task ToggleTicketStatus(string userId, ToggleStatusRequest request)
    {
        var user = _userManager.FindByIdAsync(userId) ?? throw new UserNotFoundException(userId);
        var project = _projectService.GetProject(userId, request.ProjectId) ?? throw new ArgumentException("Project does not exist");

        var ticket = _ticketRepo.FindSingleBy(t =>
            t.ProjectId.Equals(request.ProjectId)
            && t.Id.Equals(request.TicketId),
            trackChanges: true);
        ticket.Status = (Status)request.StatusId;
        _ticketRepo.Update(ticket);
    }

    public async Task UpdateTicket(string userId, UpdateTicketRequest request)
    {
        var user = _userManager.FindByIdAsync(userId) ?? throw new UserNotFoundException(userId);
        var project = _projectService.GetProject(userId, request.ProjectId) ?? throw new ArgumentException("Project does not exist");

        var ticket = _ticketRepo.FindSingleBy(t =>
            t.ProjectId.Equals(request.ProjectId)
            && t.Id.Equals(request.TicketId),
            trackChanges: true);

        var updatedTicket = _mapper.Map(request, ticket);
        _ticketRepo.Update(updatedTicket);
    }
}
