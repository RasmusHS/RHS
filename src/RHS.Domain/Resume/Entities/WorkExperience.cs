using EnsureThat;
using RHS.Domain.Common;
using RHS.Domain.Common.ValueObjects;
using RHS.Domain.Resume.ValueObjects;

namespace RHS.Domain.Resume.Entities;

public sealed class WorkExperience : Entity<WorkExpId>
{
    internal WorkExperience() { } // For ORM

    private WorkExperience(WorkExpId id, ResumeId resumeId, string title, string company, Address location, DateTime startDate, DateTime? endDate, string description) : base(id)
    {
        Id = id;
        ResumeId = resumeId;
        Title = title;
        Company = company;
        Location = location;
        StartDate = startDate;
        EndDate = endDate;
        Description = description;
        
        Created = DateTime.Now;
        LastModified = DateTime.Now;
    }

    public static Result<WorkExperience> Create(ResumeId resumeId, string title, string company, Address location, DateTime startDate, DateTime? endDate, string description)
    {
        Ensure.That(resumeId, nameof(resumeId)).IsNotNull();
        Ensure.That(title, nameof(title)).IsNotNullOrEmpty();
        Ensure.That(company, nameof(company)).IsNotNullOrEmpty();
        Ensure.That(location, nameof(location)).IsNotNull();
        Ensure.That(startDate, nameof(startDate));
        Ensure.That(endDate, nameof(endDate));
        Ensure.That(description, nameof(description)).IsNotNullOrEmpty();
        
        return Result.Ok<WorkExperience>(new WorkExperience(WorkExpId.Create(), resumeId, title, company, location, startDate, endDate, description));
    }
    
    public ResumeId ResumeId { get; private set; }
    public string Title { get; private set; }
    public string Company { get; private set; }
    public Address Location { get; private set; } 
    public DateTime StartDate { get; private set; }
    public DateTime? EndDate { get; private set; }
    public string Description { get; private set; }
    
    // navigation properties
    public Resume Resume { get; private set; }
}