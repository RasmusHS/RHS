using RHS.Application.CQRS.Resume.Project.Command;
using RHS.Application.Data;
using RHS.Application.Data.Infrastructure;
using RHS.Domain.Common;
using RHS.Domain.Common.ValueObjects;
using RHS.Domain.Resume;
using RHS.Domain.Resume.Entities;
using RHS.Domain.Resume.ValueObjects;

namespace RHS.Application.CQRS.Command.Resume.Handlers;

public class CreateResumeCommandHandler : ICommandHandler<CreateResumeCommand>
{
    private readonly IResumeRepository _resumeRepository;
    
    
    public CreateResumeCommandHandler(IResumeRepository resumeRepository)
    {
        _resumeRepository = resumeRepository;
    }
    
    public async Task<Result> Handle(CreateResumeCommand command, CancellationToken cancellationToken = default)
    {
        Result<FullName> fullNameResult = FullName.Create(command.FirstName, command.LastName);
        if (fullNameResult.Failure) return fullNameResult;
        
        Result<Address> addressResult = Address.Create(command.Street, command.ZipCode, command.City);
        if (addressResult.Failure) return addressResult;
        
        Result<Email> emailResult = Email.Create(command.Email);
        if (emailResult.Failure) return emailResult;
        
        if (command.Projects != null)
        {
            Result<ResumeEntity> resumeResult = ResumeEntity.Create(
                command.Introduction,
                fullNameResult.Value,
                addressResult.Value,
                emailResult.Value,
                command.GitHubLink,
                command.LinkedInLink,
                command.Photo
            );
            if (resumeResult.Failure) return resumeResult;
            
            List<ProjectEntity> projects = new List<ProjectEntity>();
            foreach (var project in command.Projects)
            {
                Result<ProjectEntity> projectResult = ProjectEntity.Create( 
                    project.ResumeId = resumeResult.Value.Id,
                    project.ProjectTitle,
                    project.Description,
                    project.ProjectUrl,
                    project.DemoGif,
                    project.IsFeatured
                );
                if (projectResult.Failure) return projectResult;
                
                projects.Add(projectResult.Value);
            }
            resumeResult.Value.AddRangeProjects(projects);
            await _resumeRepository.AddAsync(resumeResult.Value, cancellationToken);
        }
        else
        {
            Result<ResumeEntity> resumeResult = ResumeEntity.Create(
                command.Introduction,
                fullNameResult.Value,
                addressResult.Value,
                emailResult.Value,
                command.GitHubLink,
                command.LinkedInLink,
                command.Photo
            );
            if (resumeResult.Failure) return resumeResult;
            await _resumeRepository.AddAsync(resumeResult.Value, cancellationToken);
        }
        
        return Result.Ok();
    }
}