using RHS.Application.Data;
using RHS.Application.Data.Infrastructure;
using RHS.Domain.Common;
using RHS.Domain.Resume.Entities;

namespace RHS.Application.CQRS.Resume.Command.Handlers;

public class UpdateResumeCommandHandler : ICommandHandler<UpdateResumeCommand>
{
    private readonly IResumeRepository _resumeRepository;
    private readonly IProjectRepository _projectRepository;
    
    public UpdateResumeCommandHandler(IResumeRepository resumeRepository, IProjectRepository projectRepository)
    {
        _resumeRepository = resumeRepository;
        _projectRepository = projectRepository;
    }
    
    public async Task<Result> Handle(UpdateResumeCommand command, CancellationToken cancellationToken = default)
    {
        var resumeResult = await _resumeRepository.GetByIdAsync(command.Id);

        if (command.Projects.Any())
        {
            resumeResult.Update(
                command.Introduction,
                command.FirstName,
                command.LastName,
                command.Street,
                command.ZipCode,
                command.City,
                command.Email,
                command.GitHubLink,
                command.LinkedInLink,
                command.Photo
            );
            //if (resumeResult) return resumeResult;
            
            List<ProjectEntity> projects = new List<ProjectEntity>();
            foreach (var project in command.Projects)
            {
                Result<ProjectEntity> projectResult = ProjectEntity.Create( 
                    project.ResumeId = resumeResult.Id,
                    project.ProjectTitle,
                    project.Description,
                    project.ProjectUrl,
                    project.DemoGif,
                    project.IsFeatured
                );
                if (projectResult.Failure) return projectResult;
                
                projects.Add(projectResult.Value);
            }
            resumeResult.AddRangeProjects(projects);
            
            await _resumeRepository.UpdateAsync(resumeResult, cancellationToken);
            await _projectRepository.AddRangeAsync(projects, cancellationToken);
            
            _projectRepository.Save(cancellationToken);
        }
        else
        {
            resumeResult.Update(
                command.Introduction,
                command.FirstName,
                command.LastName,
                command.Street,
                command.ZipCode,
                command.City,
                command.Email,
                command.GitHubLink,
                command.LinkedInLink,
                command.Photo
            );
            //if (resumeResult.Failure) return resumeResult;
        }
        
        _resumeRepository.Save(cancellationToken);
        return Result.Ok();
    }
}