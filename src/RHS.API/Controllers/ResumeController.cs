using Microsoft.AspNetCore.Mvc;
using RHS.Application.CQRS.DTO.Resume.Command;
using RHS.Application.CQRS.Resume.Command;
using RHS.Application.CQRS.Resume.Project.Command;
using RHS.Application.CQRS.Resume.Query;
using RHS.Application.Data;
using RHS.Domain.Resume.ValueObjects;

namespace RHS.API.Controllers;

[Route("api/resume")]
[ApiController]
public class ResumeController : BaseController
{
    private IDispatcher _dispatcher;
    
    public ResumeController(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }
    
    [HttpPost]
    [Route("createResume")]
    public async Task<IActionResult> CreateResume(CreateResumeDto request)
    {
        bool hasProjects = request.Projects != null && request.Projects.Any();
        CreateResumeDto.Validator validator = new CreateResumeDto.Validator(hasProjects);
        var result = await validator.ValidateAsync(request);
        
        if (result.IsValid)
        {
            if (hasProjects == true)
            {
                CreateResumeCommand command = new CreateResumeCommand(
                    request.Introduction,
                    request.FirstName,
                    request.LastName,
                    request.Street,
                    request.ZipCode,
                    request.City,
                    request.Email,
                    request.GitHubLink,
                    request.LinkedInLink,
                    request.Photo,
                    request.Projects!.Select(p => new CreateProjectCommand(
                        ResumeId.GetExisting(p.ResumeId!.Value).Value, 
                        p.ProjectTitle,
                        p.Description,
                        p.ProjectUrl,
                        p.DemoGif,
                        p.IsFeatured)).ToList());
                
                var commandResult = await _dispatcher.Dispatch(command);
                if (commandResult.Success)
                {                
                    return Ok(commandResult);
                }
            }
            else
            {
                CreateResumeCommand command = new CreateResumeCommand(
                    request.Introduction,
                    request.FirstName,
                    request.LastName,
                    request.Street,
                    request.ZipCode,
                    request.City,
                    request.Email,
                    request.GitHubLink,
                    request.LinkedInLink,
                    request.Photo,
                    null);
                
                var commandResult = await _dispatcher.Dispatch(command);
                if (commandResult.Success)
                {                
                    return Ok(commandResult);
                }
            }
        }
        
        return BadRequest(result.Errors);
    }
    
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetResumeById(Guid id)
    {
        var result = await _dispatcher.Dispatch(new GetResumeQuery(ResumeId.GetExisting(id).Value)) ?? throw new KeyNotFoundException($"Resume with ID {id} not found.");
        if (result.Success)
        {
            return Ok(result.Value);
        }
        return BadRequest(result.Error.Code);
    }

    [HttpPut]
    [Route("updateResume")]
    public async Task<IActionResult> UpdateResume(UpdateResumeDto request)
    {
        UpdateResumeDto.Validator validator = new UpdateResumeDto.Validator();
        var result = await validator.ValidateAsync(request);
        if (result.IsValid)
        {
            UpdateResumeCommand command = new UpdateResumeCommand(
                ResumeId.GetExisting(request.Id).Value,
                request.Introduction,
                request.FirstName,
                request.LastName,
                request.Street,
                request.ZipCode,
                request.City,
                request.Email,
                request.GitHubLink,
                request.LinkedInLink,
                request.Photo,
                request.Created,
                request.LastModified
                ); 
            
            var commandResult =  await _dispatcher.Dispatch(command);
            if (commandResult.Success)           
            {
                return Ok(commandResult);
            }
            return BadRequest(commandResult.Error.Code);
        }
        return BadRequest(result.Errors);
    }
}