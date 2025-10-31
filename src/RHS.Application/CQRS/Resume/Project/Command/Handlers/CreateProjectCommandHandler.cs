using RHS.Application.Data;
using RHS.Application.Data.Infrastructure;
using RHS.Domain.Common;

namespace RHS.Application.CQRS.Resume.Project.Command.Handlers;

public class CreateProjectCommandHandler : ICommandHandler<CreateProjectCommand>
{
    private readonly IProjectRepository _projectRepository;
    
    public CreateProjectCommandHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }
    
    public Task<Result> Handle(CreateProjectCommand command, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}