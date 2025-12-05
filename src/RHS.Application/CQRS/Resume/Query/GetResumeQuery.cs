using EnsureThat;
using RHS.Application.CQRS.DTO.Resume.Query;
using RHS.Application.Data;
using RHS.Domain.Resume.ValueObjects;

namespace RHS.Application.CQRS.Resume.Query;

public record GetResumeQuery : IQuery<QueryResumeDto>
{
    public GetResumeQuery(ResumeId id)
    {
        Ensure.That(id, nameof(id)).IsNotNull();
        
        Id = id;
    }
    
    public ResumeId Id { get; private set; }
}