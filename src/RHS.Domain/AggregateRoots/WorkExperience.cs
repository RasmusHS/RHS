using RHS.Domain.Common;
using RHS.Domain.ValueObjects;

namespace RHS.Domain.AggregateRoots;

public class WorkExperience : AggregateRoot
{
    internal WorkExperience()
    {
        
    }

    public WorkExperience(int resumeId, string title, string company, Address location, DateTime startDate, DateTime endDate, string description)
    {
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
    
    public int ResumeId { get; private set; }
    public string Title { get; private set; }
    public string Company { get; private set; } // Refactor to strong type?
    public Address Location { get; private set; } // Refactor to strong type
    public DateTime StartDate { get; private set; }
    public DateTime? EndDate { get; private set; }
    public string Description { get; private set; }
    
    // navigation properties
    public Resume Resume { get; private set; }
}