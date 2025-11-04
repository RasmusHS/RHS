using RHS.Application.Data;
using RHS.Application.Data.Infrastructure;
using RHS.Domain.Common;

namespace RHS.Application.CQRS.Resume.Project.Command.Handlers;

public class UpdateProjectCommandHandler : ICommandHandler<UpdateProjectCommand>
{
    private readonly IProjectRepository _projectRepository;
    
    public UpdateProjectCommandHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }
    
    public async Task<Result> Handle(UpdateProjectCommand command, CancellationToken cancellationToken = default)
    {
        var projectResult = await _projectRepository.GetByIdAsync(command.Id) ?? throw new KeyNotFoundException($"Project with Id {command.Id} was not found.");
        
        projectResult.Update(
            command.ResumeId,
            command.ProjectTitle,
            command.Description,
            command.ProjectUrl,
            command.DemoGif,
            command.IsFeatured);
        
        await _projectRepository.UpdateAsync(projectResult, cancellationToken);
        _projectRepository.Save(cancellationToken);
        
        return Result.Ok();
    }
}