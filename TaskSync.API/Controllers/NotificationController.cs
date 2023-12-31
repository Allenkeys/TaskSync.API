﻿using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TaskSync.Domain.Entities;
using TaskSync.Infrastructure.Interfaces;
using TaskSync.Infrastructure.ValidationResponse;

namespace TaskSync.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NotificationController : ControllerBase
{
    private readonly INoticeEngagementService _noticeEngagementService;
    private readonly string _userId;

    public NotificationController(INoticeEngagementService noticeEngagementService)
    {
        _noticeEngagementService = noticeEngagementService;
        _userId = HttpContext.User.FindFirstValue("Id");
    }

    [HttpPost("mark-as-read/{noticeId:int}", Name = "mark-notice-as-read")]
    [SwaggerOperation(Summary = "Mark a notification as read or unread")]
    [SwaggerResponse(StatusCodes.Status200OK, Description = "returns a success message", Type = typeof(SuccessResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "You did something wrong!", Type = typeof(BadRequestResult))]
    public async Task<IActionResult> Read(int noticeId)
    {
        var response = _noticeEngagementService.ToggleRead(_userId, noticeId);
        return Ok();
    }

    [HttpPost("get-notification", Name = "get-notification")]
    [SwaggerOperation(Summary = "Get all notifications")]
    [SwaggerResponse(StatusCodes.Status200OK, Description = "returns a success message", Type = typeof(IEnumerable<Notification>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "You did something wrong!", Type = typeof(BadRequestResult))]
    public async Task<IActionResult> GetAll(int noticeId)
    {
        var response = _noticeEngagementService.GetAllNotifications(_userId);
        return Ok(response);
    }

    [HttpPost("get-notifications/{noticeId:int}", Name = "get-all-notifications")]
    [SwaggerOperation(Summary = "Get a notification")]
    [SwaggerResponse(StatusCodes.Status200OK, Description = "returns a success message", Type = typeof(Notification))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "You did something wrong!", Type = typeof(BadRequestResult))]
    public async Task<IActionResult> Get(int noticeId)
    {
        var response = _noticeEngagementService.GetNotification(_userId, noticeId);
        return Ok(response);
    }
}
