using EnsureThat;
using RHS.Domain.Common;
using RHS.Domain.Institution;
using RHS.Domain.Institution.ValueObjects;
using RHS.Domain.Resume.ValueObjects;

namespace RHS.Domain.Resume.Entities;

public sealed class Education : JoinEntity<ResumeId, Degree>
{
    internal Education() { } // For ORM

    private Education(ResumeId resumeId, Degree degree, DateTime startDate, DateTime? endDate) : base(resumeId, degree)
    {
        Id1 = resumeId;
        Id2 = degree;
        StartDate = startDate;
        EndDate = endDate;
        
        Created = DateTime.Now;
        LastModified = DateTime.Now;
    }
    
    public static Result<Education> Create(ResumeId resumeId, Degree degree, DateTime startDate, DateTime? endDate)
    {
        Ensure.That(resumeId, nameof(resumeId)).IsNotNull();
        Ensure.That(degree, nameof(degree)).IsNotNull();
        Ensure.That(startDate, nameof(startDate));
        Ensure.That(endDate, nameof(endDate));
        
        return Result.Ok<Education>(new Education(resumeId, degree, startDate, endDate));
    }
    
    public ResumeId ResumeId { get; private set; }
    public Degree Degree { get; private set; }
    public DateTime StartDate { get; private set; } 
    public DateTime? EndDate { get; private set; } 
    
    // navigation properties
    public Resume Resume { get; private set; }
    public InstitutionEntity Institution { get; private set; }
}