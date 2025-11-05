using RHS.Application.CQRS.DTO.Resume.Project.Query;
using RHS.Application.Data;
using RHS.Domain.Resume.ValueObjects;

namespace RHS.Application.CQRS.Resume.Project.Query;

public class GetAllProjectsQuery : IQuery<CollectionResponseBase<QueryProjectDto>>
{
    public GetAllProjectsQuery(ResumeId resumeId)
    {
        ResumeId = resumeId;
    }

    public GetAllProjectsQuery() { }
    
    public ResumeId ResumeId { get; }
}