using Microsoft.AspNetCore.Mvc;
using RHS.Application.CQRS.DTO.Resume.Project.Command;
using RHS.Application.CQRS.Resume.Project.Command;
using RHS.Application.CQRS.Resume.Project.Query;
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

    [HttpPost]
    [Route("createProject")]
    public async Task<IActionResult> CreateProject(CreateProjectDto request)
    {
        CreateProjectDto.Validator validator = new CreateProjectDto.Validator();
        var result = await validator.ValidateAsync(request);

        CreateProjectCommand command = new CreateProjectCommand(
            request.ResumeId,
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
    public async Task<IActionResult> GetProject(ProjectId projectId)
    {
        var result = await _dispatcher.Dispatch(new GetProjectQuery(projectId));
        if (result.Success)
        {
            return Ok(result.Value);
        }
        return BadRequest(result.Error.Code);
    }
    
    [HttpGet]
    [Route("{resumeId}")]
    public async Task<IActionResult> GetProjectsByResumeId(ResumeId resumeId)
    {
        GetAllProjectsQuery query = new GetAllProjectsQuery(resumeId);
        var result = await _dispatcher.Dispatch(query);
        if (result.Success)
        {
            return Ok(result.Value);
        }
        return BadRequest(result.Error.Code);
    }

    [HttpPut]
    [Route("updateProject")]
    public async Task<IActionResult> UpdateProject(UpdateProjectDto request)
    {
        UpdateProjectDto.Validator validator = new UpdateProjectDto.Validator();
        var result = await validator.ValidateAsync(request);
        if (result.IsValid)
        {
            UpdateProjectCommand command = new UpdateProjectCommand(
                request.Id,
                request.ResumeId,
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

    [HttpDelete]
    [Route("deleteProject/{projectId}")]
    public async Task<IActionResult> DeleteProject(DeleteProjectDto request)
    {
        DeleteProjectDto.Validator validator = new DeleteProjectDto.Validator();
        var result = await validator.ValidateAsync(request);
        if (result.IsValid)
        {
            DeleteProjectCommand command = new DeleteProjectCommand(request.Id);
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