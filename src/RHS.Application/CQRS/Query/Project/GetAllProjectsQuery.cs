using EnsureThat;
using RHS.Application.CQRS.DTO.Project.Query;
using RHS.Application.Data;
using RHS.Domain.Resume.ValueObjects;

namespace RHS.Application.CQRS.Query.Project;

public class GetAllProjectsQuery : IQuery<CollectionResponseBase<QueryProjectDto>>
{
    public GetAllProjectsQuery(ResumeId resumeId)
    {
        Ensure.That(resumeId, nameof(resumeId)).IsNotNull();
        ResumeId = resumeId;
    }

    public GetAllProjectsQuery() { }
    
    public ResumeId ResumeId { get; }
}