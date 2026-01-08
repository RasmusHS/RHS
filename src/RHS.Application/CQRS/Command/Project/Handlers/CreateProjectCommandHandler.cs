using RHS.Application.Data;
using RHS.Application.Data.Infrastructure;
using RHS.Domain.Common;
using RHS.Domain.Resume.Entities;

namespace RHS.Application.CQRS.Command.Project.Handlers;

public class CreateProjectCommandHandler : ICommandHandler<CreateProjectCommand>
{
    private readonly IProjectRepository _projectRepository;
    
    public CreateProjectCommandHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }
    
    public async Task<Result> Handle(CreateProjectCommand command, CancellationToken cancellationToken = default)
    {
        if (command.ResumeId == null)
        {
            return Result.Fail(Errors.General.ValueIsRequired(nameof(command.ResumeId)));
        }
        Result<ProjectEntity> projectResult = ProjectEntity.Create(
            command.ResumeId,
            command.ProjectTitle,
            command.Description,
            command.ProjectUrl,
            command.DemoGif,
            command.IsFeatured
        );
        if (projectResult.Failure) return projectResult;
        
        await _projectRepository.AddAsync(projectResult.Value, cancellationToken);
        
        return Result.Ok();
    }
}