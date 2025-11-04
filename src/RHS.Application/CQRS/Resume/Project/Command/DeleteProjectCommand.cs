using RHS.Application.Data;
using RHS.Domain.Resume.ValueObjects;

namespace RHS.Application.CQRS.Resume.Project.Command;

public class DeleteProjectCommand : ICommand
{
    public DeleteProjectCommand(ProjectId id)
    {
        Id = id;
    }

    public DeleteProjectCommand() { }
    
    public ProjectId Id { get; set; }
}