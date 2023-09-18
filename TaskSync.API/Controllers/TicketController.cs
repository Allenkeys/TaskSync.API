using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TaskSync.Domain.Dtos.Request;
using TaskSync.Domain.Entities;
using TaskSync.Infrastructure.Interfaces;
using TaskSync.Infrastructure.ValidationResponse;

namespace TaskSync.API.Controllers;

[Route("api/project/{projectId}/[controller]")]
[ApiController]
public class TicketController : ControllerBase
{
    private readonly ITicketService _ticketService;
    private readonly string _userId;

    public TicketController(ITicketService ticketService)
    {
        _ticketService = ticketService;
        _userId = HttpContext.User.FindFirstValue("Id");
    }

    [HttpPost("create-ticket", Name = "create-a-ticket")]
    [SwaggerOperation(Summary = "Create a new ticket")]
    [SwaggerResponse(StatusCodes.Status200OK, Description = "returns a success message", Type = typeof(SuccessResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "You did something wrong!", Type = typeof(BadRequestResult))]
    public async Task<IActionResult> Create([FromForm] CreateTicketRequest request)
    {
        var response = _ticketService.CreateTicket(_userId, request);
        return Ok(response);
    }

    [HttpGet("get-ticket", Name = "get-ticket")]
    [SwaggerOperation(Summary = "Get a ticket by Id")]
    [SwaggerResponse(StatusCodes.Status200OK, Description = "returns ticket details", Type = typeof(Ticket))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "You did something wrong!", Type = typeof(BadRequestResult))]
    [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Not found!", Type = typeof(NotFoundResult))]
    public async Task<IActionResult> Get([FromForm]GetTicketRequest request)
    {
        var response = _ticketService.GetTicket(_userId, request);
        if (response == null)
            return NotFound();
        return Ok(response);
    }

    [HttpGet("get-tickets", Name = "get-tickets")]
    [SwaggerOperation(Summary = "Get a collection of tickets")]
    [SwaggerResponse(StatusCodes.Status200OK, Description = "returns a collection of tickets", Type = typeof(IEnumerable<Ticket>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "You did something wrong!", Type = typeof(BadRequestResult))]
    [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Sorry no records!", Type = typeof(EmptyResult))]
    public async Task<IActionResult> GetAll(int projectId)
    {
        var response = _ticketService.GetAllProjectTicketsAsync(_userId, projectId);
        return Ok(response);
    }

    [HttpGet("get-tickets-by-status", Name = "get-tickets-by-status")]
    [SwaggerOperation(Summary = "Get a collection of tickets by status")]
    [SwaggerResponse(StatusCodes.Status200OK, Description = "returns a collection of tickets", Type = typeof(IEnumerable<Ticket>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "You did something wrong!", Type = typeof(BadRequestResult))]
    [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Sorry no records!", Type = typeof(EmptyResult))]
    public async Task<IActionResult> GetAll(int projectId, int statusId)
    {
        var response = _ticketService.GetTicketByStatus(_userId, projectId, statusId);
        return Ok(response);
    }

    [HttpGet("get-tickets-by-date", Name = "get-tickets-by-date")]
    [SwaggerOperation(Summary = "Get a collection of tickets by date")]
    [SwaggerResponse(StatusCodes.Status200OK, Description = "returns a collection of tickets", Type = typeof(IEnumerable<Ticket>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "You did something wrong!", Type = typeof(BadRequestResult))]
    [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Sorry no records!", Type = typeof(EmptyResult))]
    public async Task<IActionResult> GetAll(int projectId, DateTime date)
    {
        var response = _ticketService.GetTicketByDueDate(_userId, projectId, date);
        return Ok(response);
    }

    [HttpDelete("delete-ticket", Name = "delete-ticket")]
    [SwaggerOperation(Summary = "Delete a user ticket")]
    [SwaggerResponse(StatusCodes.Status200OK, Description = "delete a ticket", Type = typeof(string))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "You did something wrong!", Type = typeof(ErrorResponse))]
    [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Sorry no records!", Type = typeof(EmptyResult))]
    public async Task<IActionResult> Delete([FromForm] DeleteTicketRequest request)
    {
        var response = _ticketService.DeleteTicket(_userId, request);
        return Ok(response);
    }

    [HttpPut("update-ticket", Name = "update-ticket")]
    [SwaggerOperation(Summary = "Update a ticket")]
    [SwaggerResponse(StatusCodes.Status200OK, Description = "returns nothing", Type = typeof(string))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "You did something wrong!", Type = typeof(BadRequestResult))]
    [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Sorry no records!", Type = typeof(EmptyResult))]
    public async Task<IActionResult> Update([FromForm] UpdateTicketRequest request)
    {
        var response = _ticketService.UpdateTicket(_userId, request);
        return Ok(response);
    }

    [HttpPut("move-ticket", Name = "move-ticket")]
    [SwaggerOperation(Summary = "Update a ticket")]
    [SwaggerResponse(StatusCodes.Status200OK, Description = "returns nothing", Type = typeof(string))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "You did something wrong!", Type = typeof(BadRequestResult))]
    [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Sorry no records!", Type = typeof(EmptyResult))]
    public async Task<IActionResult> MoveTicket([FromForm] MoveTicketRequest request)
    {
        var response = _ticketService.MovedTicket(_userId, request);
        return Ok(response);
    }
}
