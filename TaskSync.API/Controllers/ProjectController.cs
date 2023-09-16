using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TaskSync.Domain.Dtos.Request;
using TaskSync.Domain.Entities;
using TaskSync.Infrastructure.Interfaces;

namespace TaskSync.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly string _userId;
        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
            _userId = HttpContext.User.FindFirstValue("Id");
        }

        [HttpPost("create-a-project", Name = "create-a-project")]
        [SwaggerOperation(Summary = "Create a new project")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "returns a success message", Type = typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "You did something wrong!", Type = typeof(BadRequestResult))]
        public async Task<IActionResult> Create(CreateProjectRequest request)
        {
            var response = await _projectService.CreateProject(_userId, request);
            return Ok(response);
        }

        [HttpGet("get-project/{projectId:int}", Name = "get-project")]
        [SwaggerOperation(Summary = "Get a project by Id")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "returns project details", Type = typeof(Project))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "You did something wrong!", Type = typeof(BadRequestResult))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Not found!", Type = typeof(NotFoundResult))]
        public async Task<IActionResult> Get(int projectId)
        {
            var response = await _projectService.GetProject(_userId, projectId);
            if (response == null)
                return NotFound();
            return Ok(response);
        }

        [HttpGet("get-projects", Name = "get-projects")]
        [SwaggerOperation(Summary = "Get a collection of fruits")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "returns a collection of projects", Type = typeof(IEnumerable<Project>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "You did something wrong!", Type = typeof(BadRequestResult))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Sorry no records!", Type = typeof(EmptyResult))]
        public async Task<IActionResult> GetAll()
        {
            var response = await _projectService.GetAllProjectsAsync(_userId);
            return Ok(response);
        }

        [HttpDelete("delete-project/{projectId:int}", Name = "delete-project")]
        [SwaggerOperation(Summary = "Delete a user project")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "delete a project", Type = typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "You did something wrong!", Type = typeof(BadRequestResult))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Sorry no records!", Type = typeof(EmptyResult))]
        public async Task<IActionResult> Delete(int projectId)
        {
            await _projectService.DeleteProject(_userId, projectId);
            return Ok();
        }

        [HttpPut("update-project/{projectId:int}", Name = "update-project")]
        [SwaggerOperation(Summary = "Update a project")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "returns projects", Type = typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "You did something wrong!", Type = typeof(BadRequestResult))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "Sorry no records!", Type = typeof(EmptyResult))]
        public async Task<IActionResult> Update(int projectId, UpdateProjectRequest request)
        {
            await _projectService.UpdateProject(_userId, projectId, request);
            return Ok();
        }
    }
}
