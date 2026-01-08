using RHS.Application.Data;
using RHS.Application.Data.Infrastructure;
using RHS.Domain.Common;

namespace RHS.Application.CQRS.Command.Project.Handlers;

public class DeleteProjectCommandHandler : ICommandHandler<DeleteProjectCommand>
{
    private readonly IProjectRepository _projectRepository;
    
    public DeleteProjectCommandHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }
    
    public async Task<Result> Handle(DeleteProjectCommand command, CancellationToken cancellationToken = default)
    {
        await _projectRepository.DeleteAsync(command.Id, cancellationToken);
        
        return Result.Ok();
    }
}