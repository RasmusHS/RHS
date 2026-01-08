using EnsureThat;
using RHS.Application.CQRS.DTO.Project.Query;
using RHS.Application.Data;
using RHS.Domain.Resume.ValueObjects;

namespace RHS.Application.CQRS.Query.Project;

public class GetProjectQuery : IQuery<QueryProjectDto>
{
    public GetProjectQuery(ProjectId id)
    {
        Ensure.That(id, nameof(id)).IsNotNull();
        
        Id = id;
    }
    
    public ProjectId Id { get; private set; }
}