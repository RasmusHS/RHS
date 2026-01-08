using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RHS.Application.CQRS.Command.Project;
using RHS.Application.CQRS.DTO.Project.Command;
using RHS.Application.CQRS.Query.Project;
using RHS.Application.Data;
using RHS.Domain.Resume.ValueObjects;

namespace RHS.API.Controllers;

[Route("api/project")]
[ApiController]
public class ProjectController : BaseController
{
    private IDispatcher _dispatcher;
    
    public ProjectController(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    [Authorize]
    [HttpPost]
    [Route("createProject")]
    public async Task<IActionResult> CreateProject(CreateProjectDto request)
    {
        CreateProjectDto.Validator validator = new CreateProjectDto.Validator();
        var result = await validator.ValidateAsync(request);

        CreateProjectCommand command = new CreateProjectCommand(
            ResumeId.GetExisting(request.ResumeId!.Value).Value, 
            request.ProjectTitle,
            request.Description,
            request.ProjectUrl,
            request.DemoGif,
            request.IsFeatured);
        
        var commandResult = await _dispatcher.Dispatch(command);
        if (commandResult.Success)
        {                
            return Ok(commandResult);
        }
        return BadRequest(commandResult.Error.Code);
    }
    
    [HttpGet]
    [Route("getProject/{projectId}")]
    public async Task<IActionResult> GetProject(Guid projectId)
    {
        var result = await _dispatcher.Dispatch(new GetProjectQuery(ProjectId.GetExisting(projectId).Value)) ?? throw new KeyNotFoundException($"Project with ID {projectId} not found.");
        if (result.Success)
        {
            return Ok(result.Value);
        }
        return BadRequest(result.Error.Code);
    }
    
    [HttpGet]
    [Route("{resumeId}")]
    public async Task<IActionResult> GetProjectsByResumeId(Guid resumeId)
    {
        var result = await _dispatcher.Dispatch(new GetAllProjectsQuery(ResumeId.GetExisting(resumeId).Value)) ?? throw new KeyNotFoundException($"Projects for Resume ID {resumeId} not found.");
        if (result.Success)
        {
            return Ok(result.Value);
        }
        return BadRequest(result.Error.Code);
    }

    [Authorize]
    [HttpPut]
    [Route("updateProject")]
    public async Task<IActionResult> UpdateProject(UpdateProjectDto request)
    {
        UpdateProjectDto.Validator validator = new UpdateProjectDto.Validator();
        var result = await validator.ValidateAsync(request);
        if (result.IsValid)
        {
            UpdateProjectCommand command = new UpdateProjectCommand(
                ProjectId.GetExisting(request.Id).Value, 
                ResumeId.GetExisting(request.ResumeId).Value, 
                request.ProjectTitle,
                request.Description,
                request.ProjectUrl,
                request.DemoGif,
                request.IsFeatured,
                request.Created,
                request.LastModified);
            
            var commandResult =  await _dispatcher.Dispatch(command);
            if (commandResult.Success)           
            {
                return Ok(commandResult);
            }
            return BadRequest(commandResult.Error.Code);
        }
        return BadRequest(result.Errors);
    }

    [Authorize]
    [HttpDelete]
    [Route("deleteProject")]
    public async Task<IActionResult> DeleteProject(DeleteProjectDto request)
    {
        DeleteProjectDto.Validator validator = new DeleteProjectDto.Validator();
        var result = await validator.ValidateAsync(request);
        if (result.IsValid)
        {
            DeleteProjectCommand command = new DeleteProjectCommand(ProjectId.GetExisting(request.Id).Value);
            var commandResult = await _dispatcher.Dispatch(command);
            if (commandResult.Success)
            {
                return Ok(commandResult);
            }
            return BadRequest(commandResult.Error.Code);
        }
        return BadRequest(result.Errors);
    }
}